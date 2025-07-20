using System.Diagnostics;

namespace mpv_yt_dlp_wrapper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Enable form-level key handling
            this.KeyDown += MainForm_KeyDown;
            launchButton.Enabled = !string.IsNullOrWhiteSpace(urlTextBox.Text);
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

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
                return;

            string url = urlTextBox.Text.Trim();
            string quality = qualityComboBox.SelectedItem?.ToString() ?? "best";
            bool audioOnly = audioOnlyCheckBox.Checked;

            List<string> mpvArgs = new();

            if (audioOnly)
            {
                mpvArgs.Add("--no-video");
            }

            // Embed into panel1
            mpvArgs.Add($"--wid={splitContainer.Panel1.Handle}");

            // Optional: Set ytdl-format (controls video/audio quality)
            if (!audioOnly && int.TryParse(quality, out int q))
            {
                mpvArgs.Add($"--ytdl-format=bestvideo[height<={q}]+bestaudio/best");
            }

            mpvArgs.Add("--force-window=yes"); // Ensures window is created if no video
            mpvArgs.Add("--autofit=100%x100%"); // Fit to panel

            mpvArgs.Add(url);

            try
            {
                // Clear output box
                consoleTextBox.Clear();

                Process mpvProcess = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "mpv",
                        Arguments = string.Join(" ", mpvArgs.Select(arg => $"\"{arg}\"")),
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                // Capture both output and error
                mpvProcess.OutputDataReceived += (s, ea) =>
                {
                    if (!string.IsNullOrEmpty(ea.Data))
                        AppendOutput(ea.Data);
                };
                mpvProcess.ErrorDataReceived += (s, ea) =>
                {
                    if (!string.IsNullOrEmpty(ea.Data))
                        AppendOutput(ea.Data);
                };

                mpvProcess.Start();
                mpvProcess.BeginOutputReadLine();
                mpvProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch MPV:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendOutput(string text)
        {
            if (consoleTextBox.InvokeRequired)
            {
                consoleTextBox.Invoke(() => consoleTextBox.AppendText(text + Environment.NewLine));
            }
            else
            {
                consoleTextBox.AppendText(text + Environment.NewLine);
            }
        }
    }
}
