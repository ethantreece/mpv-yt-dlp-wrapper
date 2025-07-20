namespace mpv_yt_dlp_wrapper
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button pasteAndGoButton;
        private System.Windows.Forms.ComboBox qualityComboBox;
        private System.Windows.Forms.CheckBox audioOnlyCheckBox;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.Label qualityLabel;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel mpvPanel;
        private System.Windows.Forms.TextBox consoleTextBox;

        private System.Windows.Forms.Button playPauseButton;
        private System.Windows.Forms.TrackBar volumeSlider;
        private System.Windows.Forms.TrackBar positionSlider;
        private System.Windows.Forms.Label timeLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Controls
            urlTextBox = new TextBox();
            pasteButton = new Button();
            pasteAndGoButton = new Button();
            qualityComboBox = new ComboBox();
            audioOnlyCheckBox = new CheckBox();
            launchButton = new Button();
            urlLabel = new Label();
            qualityLabel = new Label();

            // New controls
            splitContainer = new SplitContainer();

            mpvPanel = new Panel();
            playPauseButton = new Button();
            volumeSlider = new TrackBar();
            positionSlider = new TrackBar();
            timeLabel = new Label();

            consoleTextBox = new TextBox();

            // MainForm
            ClientSize = new Size(1000, 600);
            Text = "MPV YouTube Launcher";
            Name = "MainForm";

            // URL Label
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(12, 15);
            urlLabel.Size = new Size(99, 20);
            urlLabel.Text = "YouTube URL:";

            // URL TextBox
            urlTextBox.Location = new Point(144, 12);
            urlTextBox.Size = new Size(500, 27);
            urlTextBox.TextChanged += UrlTextBox_TextChanged;

            // Paste Button
            pasteButton.Location = new Point(660, 10);
            pasteButton.Size = new Size(60, 29);
            pasteButton.Text = "Paste";
            pasteButton.Click += PasteButton_Click;

            // Paste and Go Button
            pasteAndGoButton.Location = new Point(726, 10);
            pasteAndGoButton.Size = new Size(110, 29);
            pasteAndGoButton.Text = "Paste and Go";
            pasteAndGoButton.Click += PasteAndGoButton_Click;

            // Quality Label
            qualityLabel.AutoSize = true;
            qualityLabel.Location = new Point(12, 50);
            qualityLabel.Size = new Size(114, 20);
            qualityLabel.Text = "Max Quality (p):";

            // Quality ComboBox
            qualityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            qualityComboBox.Items.AddRange(new object[] { "2160", "1440", "1080", "720", "480", "360", "240", "144" });
            qualityComboBox.Location = new Point(144, 47);
            qualityComboBox.Size = new Size(121, 28);

            // AudioOnly Checkbox
            audioOnlyCheckBox.AutoSize = true;
            audioOnlyCheckBox.Location = new Point(280, 49);
            audioOnlyCheckBox.Text = "Audio Only";

            // Launch Button
            launchButton.Location = new Point(12, 80);
            launchButton.Size = new Size(824, 40);
            launchButton.Text = "Launch";
            launchButton.Click += launchButton_Click;

            // SplitContainer
            splitContainer.Location = new Point(12, 130);
            splitContainer.Size = new Size(960, 450);
            splitContainer.SplitterDistance = 640;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Left panel (for mpv window embedding)
            mpvPanel.Dock = DockStyle.Fill;
            mpvPanel.BackColor = Color.Black;
            mpvPanel.MouseEnter += new System.EventHandler(this.mpvPanel_MouseEnter);
            mpvPanel.MouseLeave += new System.EventHandler(this.mpvPanel_MouseLeave);
            splitContainer.Panel1.Controls.Add(mpvPanel);

            // Play / Pause
            playPauseButton.Text = "⏯";
            playPauseButton.Size = new System.Drawing.Size(40, 30);
            playPauseButton.Location = new System.Drawing.Point(10, 10);
            playPauseButton.Visible = false;
            playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            mpvPanel.Controls.Add(this.playPauseButton);

            // Volume Slider
            volumeSlider.Minimum = 0;
            volumeSlider.Maximum = 100;
            volumeSlider.Value = 100;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Size = new System.Drawing.Size(100, 30);
            volumeSlider.Location = new System.Drawing.Point(60, 10);
            volumeSlider.Visible = false;
            volumeSlider.Scroll += new System.EventHandler(this.volumeSlider_Scroll);
            mpvPanel.Controls.Add(this.volumeSlider);

            // Position Slider
            positionSlider.Minimum = 0;
            positionSlider.Maximum = 10000;
            positionSlider.Value = 0;
            positionSlider.TickStyle = TickStyle.None;
            positionSlider.Size = new System.Drawing.Size(300, 30);
            positionSlider.Location = new System.Drawing.Point(170, 10);
            positionSlider.Visible = false;
            positionSlider.Scroll += new System.EventHandler(this.positionSlider_Scroll);
            mpvPanel.Controls.Add(this.positionSlider);

            // Time Label
            timeLabel.AutoSize = true;
            timeLabel.Location = new System.Drawing.Point(480, 10);
            timeLabel.Size = new System.Drawing.Size(100, 20);
            timeLabel.Text = "0:00 / 0:00";
            timeLabel.ForeColor = Color.White;
            timeLabel.Visible = false;
            mpvPanel.Controls.Add(this.timeLabel);

            // Right panel (for console output)
            consoleTextBox.Dock = DockStyle.Fill;
            consoleTextBox.Multiline = true;
            consoleTextBox.ReadOnly = true;
            consoleTextBox.ScrollBars = ScrollBars.Both;
            consoleTextBox.BackColor = Color.Black;
            consoleTextBox.ForeColor = Color.White;
            consoleTextBox.Font = new Font("Consolas", 10);
            splitContainer.Panel2.Controls.Add(consoleTextBox);

            // Add all to form
            Controls.Add(urlLabel);
            Controls.Add(urlTextBox);
            Controls.Add(pasteButton);
            Controls.Add(pasteAndGoButton);
            Controls.Add(qualityLabel);
            Controls.Add(qualityComboBox);
            Controls.Add(audioOnlyCheckBox);
            Controls.Add(launchButton);
            Controls.Add(splitContainer);
        }

        private void mpvPanel_MouseEnter(object sender, EventArgs e)
        {
            playPauseButton.Visible = true;
            volumeSlider.Visible = true;
            positionSlider.Visible = true;
            timeLabel.Visible = true;
        }

        private void mpvPanel_MouseLeave(object sender, EventArgs e)
        {
            playPauseButton.Visible = false;
            volumeSlider.Visible = false;
            positionSlider.Visible = false;
            timeLabel.Visible = false;
        }
    }
}