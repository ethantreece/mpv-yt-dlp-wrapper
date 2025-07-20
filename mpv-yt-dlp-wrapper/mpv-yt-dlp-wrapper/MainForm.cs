using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace mpv_yt_dlp_wrapper
{
    public partial class MainForm : Form
    {
        private Process? mpvProcess;
        private NamedPipeClientStream? pipeClient;
        private double currentTime = 0;
        private double duration = 0;
        private bool isPaused = false;

        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Enable form-level key handling
            this.KeyDown += MainForm_KeyDown;
            launchButton.Enabled = !string.IsNullOrWhiteSpace(urlTextBox.Text);
        }

        private void ShowControls()
        {
            playPauseButton.Visible = true;
            volumeSlider.Visible = true;
            positionSlider.Visible = true;
            timeLabel.Visible = true;
        }

        private void HideControls()
        {
            playPauseButton.Visible = false;
            volumeSlider.Visible = false;
            positionSlider.Visible = false;
            timeLabel.Visible = false;
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (launchButton.Enabled)
                    launchButton.PerformClick();
            }
        }

        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            launchButton.Enabled = !string.IsNullOrWhiteSpace(urlTextBox.Text);
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                urlTextBox.Text = Clipboard.GetText();
            }
        }

        private void PasteAndGoButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                urlTextBox.Text = Clipboard.GetText();
                launchButton.PerformClick();
            }
        }

        private async void launchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
                return;

            // Stop existing process if running
            await StopMpvProcess();

            string url = urlTextBox.Text.Trim();
            string quality = qualityComboBox.SelectedItem?.ToString() ?? "best";
            bool audioOnly = audioOnlyCheckBox.Checked;

            List<string> mpvArgs = new();

            if (audioOnly)
            {
                mpvArgs.Add("--no-video");
            }

            // Embed into panel
            mpvArgs.Add($"--wid={mpvPanel.Handle}");

            // Optional: Set ytdl-format (controls video/audio quality)
            if (!audioOnly && int.TryParse(quality, out int q))
            {
                mpvArgs.Add($"--ytdl-format=bestvideo[height<={q}]+bestaudio/best");
            }

            mpvArgs.Add("--force-window=yes"); // Ensures window is created if no video
            mpvArgs.Add("--autofit=100%x100%"); // Fit to panel

            // IPC server
            string pipeName = @"\\.\pipe\mpv_pipe_" + Guid.NewGuid().ToString();
            mpvArgs.Add($"--input-ipc-server={pipeName}");

            mpvArgs.Add(url);

            try
            {
                // Clear output box
                consoleTextBox.Clear();

                mpvProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "mpv",
                        Arguments = string.Join(" ", mpvArgs),
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                mpvProcess.Exited += (s, ea) =>
                {
                    mpvProcess = null;
                    if (pipeClient != null)
                    {
                        try
                        {
                            pipeClient.Close();
                            pipeClient.Dispose();
                        }
                        catch { }
                        pipeClient = null;
                    }
                    this.Invoke(() => consoleTextBox.AppendText("Playback ended." + Environment.NewLine));
                };

                // Capture both output and error
                mpvProcess.OutputDataReceived += (s, ea) => AppendOutput(ea.Data);
                mpvProcess.ErrorDataReceived += (s, ea) => AppendOutput(ea.Data);

                mpvProcess.Start();
                mpvProcess.BeginOutputReadLine();
                mpvProcess.BeginErrorReadLine();

                // Connect to IPC
                pipeClient = new NamedPipeClientStream(".", pipeName.Replace(@"\\.\pipe\", ""), PipeDirection.InOut, PipeOptions.Asynchronous);
                pipeClient.Connect(3000);

                // Start reading IPC
                _ = ReadIpcAsync();

                // Observe properties
                await SendCommandAsync("observe_property", "1", "percent-pos");
                await SendCommandAsync("observe_property", "2", "pause");
                await SendCommandAsync("observe_property", "3", "time-pos");
                await SendCommandAsync("observe_property", "4", "duration");

                // Get initial volume
                await SendCommandAsync(1, "get_property", "volume");

                ShowControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch MPV:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task StopMpvProcess()
        {
            // Stop timers
            HideControls(); // Ensure controls are hidden

            if (mpvProcess != null && !mpvProcess.HasExited)
            {
                try
                {
                    // Try to gracefully stop via IPC
                    await SendCommandAsync("quit");
                    // Wait briefly for graceful exit
                    await Task.Delay(500);
                    if (mpvProcess != null && !mpvProcess.HasExited)
                    {
                        mpvProcess.Kill();
                        mpvProcess.WaitForExit();
                    }
                }
                catch
                {
                    // Fallback to forceful kill if IPC fails
                    try
                    {
                        if (mpvProcess != null && !mpvProcess.HasExited)
                        {
                            mpvProcess.Kill();
                            mpvProcess.WaitForExit();
                        }
                    }
                    catch { }
                }
                finally
                {
                    if (mpvProcess != null)
                    {
                        mpvProcess.Dispose();
                        mpvProcess = null;
                    }
                }
            }

            if (pipeClient != null)
            {
                try
                {
                    pipeClient.Close();
                    pipeClient.Dispose();
                }
                catch { }
                pipeClient = null;
            }

            // Brief delay to ensure resources are released
            await Task.Delay(100);
        }

        private void AppendOutput(string? text)
        {
            if (string.IsNullOrEmpty(text)) return;

            // Process text once
            text = text.Replace("\u001b[K", "").Replace("\r", Environment.NewLine);

            if (consoleTextBox.InvokeRequired)
            {
                consoleTextBox.Invoke(() =>
                {
                    UpdateTextBox(text);
                });
            }
            else
            {
                UpdateTextBox(text);
            }
        }

        private void UpdateTextBox(string text)
        {
            string[] lines = consoleTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Check if there's a line before the last one and it starts with "AV: "
            if (lines.Length > 1 && lines[lines.Length - 2].StartsWith("AV: "))
            {
                lines[lines.Length - 2] = text;
                consoleTextBox.Text = string.Join(Environment.NewLine, lines);
            }
            else
            {
                consoleTextBox.AppendText(text + Environment.NewLine);
            }
        }

        private async Task ReadIpcAsync()
        {
            if (pipeClient == null) return;

            using var reader = new StreamReader(pipeClient, Encoding.UTF8, true, 4096, true);
            while (true)
            {
                string? line;
                try
                {
                    line = await reader.ReadLineAsync();
                }
                catch { break; }

                if (line == null) break;

                try
                {
                    var json = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(line);
                    if (json == null) continue;

                    if (json.TryGetValue("event", out var eventElem) && eventElem.GetString() == "property-change")
                    {
                        int id = json["id"].GetInt32();
                        JsonElement dataElem;
                        json.TryGetValue("data", out dataElem);

                        switch (id)
                        {
                            case 1: // percent-pos
                                double percent = dataElem.GetDouble();
                                this.Invoke(() => positionSlider.Value = (int)(percent * 100));
                                break;
                            case 2: // pause
                                isPaused = dataElem.GetBoolean();
                                this.Invoke(() => playPauseButton.Text = isPaused ? "▶️" : "⏸");
                                break;
                            case 3: // time-pos
                                currentTime = dataElem.GetDouble();
                                UpdateTimeLabel();
                                break;
                            case 4: // duration
                                duration = dataElem.GetDouble();
                                UpdateTimeLabel();
                                break;
                        }
                    }
                    else if (json.TryGetValue("request_id", out var reqIdElem))
                    {
                        int reqId = reqIdElem.GetInt32();
                        if (json["error"].GetString() == "success")
                        {
                            if (reqId == 1) // volume
                            {
                                double vol = json["data"].GetDouble();
                                this.Invoke(() => volumeSlider.Value = (int)vol);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public async Task SendCommandAsync(params object[] command)
        {
            if (pipeClient == null || !pipeClient.IsConnected) return;

            var obj = new { command = command };
            string json = JsonSerializer.Serialize(obj) + "\n";
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            try
            {
                await pipeClient.WriteAsync(bytes, 0, bytes.Length);
            }
            catch { }
        }

        public async Task SendCommandAsync(int requestId, params string[] command)
        {
            if (pipeClient == null || !pipeClient.IsConnected) return;

            var obj = new { request_id = requestId, command = command };
            string json = JsonSerializer.Serialize(obj) + "\n";
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            try
            {
                await pipeClient.WriteAsync(bytes, 0, bytes.Length);
            }
            catch { }
        }

        private void UpdateTimeLabel()
        {
            string cur = TimeSpan.FromSeconds(currentTime).ToString(@"m\:ss");
            string dur = TimeSpan.FromSeconds(duration).ToString(@"m\:ss");
            this.Invoke(() => timeLabel.Text = $"{cur} / {dur}");
        }

        private async void playPauseButton_Click(object sender, EventArgs e)
        {
            await SendCommandAsync("cycle", "pause");
        }

        private async void volumeSlider_Scroll(object sender, EventArgs e)
        {
            await SendCommandAsync("set_property", "volume", volumeSlider.Value.ToString());
        }

        private async void positionSlider_Scroll(object sender, EventArgs e)
        {
            double percent = positionSlider.Value / 100.0;
            await SendCommandAsync("set_property", "percent-pos", percent.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}