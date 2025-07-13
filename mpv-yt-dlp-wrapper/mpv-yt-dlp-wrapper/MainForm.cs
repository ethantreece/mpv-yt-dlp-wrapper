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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
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
            string url = urlTextBox.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Please enter a YouTube URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string quality = qualityComboBox.SelectedItem?.ToString() ?? "";
            bool audioOnly = audioOnlyCheckBox.Checked;

            var ytdlArgs = new List<string>();
            if (!string.IsNullOrWhiteSpace(quality))
            {
                ytdlArgs.Add($"bestvideo[height<={quality}]+bestaudio/best");
            }

            var mpvArgs = new List<string>();

            if (ytdlArgs.Count() > 0)
            {
                mpvArgs.Add($"--ytdl-format=\"{string.Join(" ", ytdlArgs)}\"");
            }

            if (audioOnly)
            {
                mpvArgs.Add("--vid=no --force-window=yes");
            }


            mpvArgs.Add($"\"{url}\"");

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "mpv",
                    Arguments = string.Join(" ", mpvArgs),
                    UseShellExecute = true,
                    RedirectStandardError = false,
                    RedirectStandardOutput = false
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch mpv: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
