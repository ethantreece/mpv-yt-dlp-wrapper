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
            urlTextBox = new TextBox();
            pasteButton = new Button();
            pasteAndGoButton = new Button();
            qualityComboBox = new ComboBox();
            audioOnlyCheckBox = new CheckBox();
            launchButton = new Button();
            urlLabel = new Label();
            qualityLabel = new Label();
            splitContainer = new SplitContainer();
            mpvPanel = new Panel();
            consoleTextBox = new TextBox();
            playPauseButton = new Button();
            volumeSlider = new TrackBar();
            positionSlider = new TrackBar();
            timeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)positionSlider).BeginInit();
            SuspendLayout();
            // 
            // urlTextBox
            // 
            urlTextBox.Location = new Point(144, 12);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.Size = new Size(500, 27);
            urlTextBox.TabIndex = 1;
            urlTextBox.TextChanged += UrlTextBox_TextChanged;
            // 
            // pasteButton
            // 
            pasteButton.Location = new Point(660, 10);
            pasteButton.Name = "pasteButton";
            pasteButton.Size = new Size(60, 29);
            pasteButton.TabIndex = 2;
            pasteButton.Text = "Paste";
            pasteButton.Click += PasteButton_Click;
            // 
            // pasteAndGoButton
            // 
            pasteAndGoButton.Location = new Point(726, 10);
            pasteAndGoButton.Name = "pasteAndGoButton";
            pasteAndGoButton.Size = new Size(110, 29);
            pasteAndGoButton.TabIndex = 3;
            pasteAndGoButton.Text = "Paste and Go";
            pasteAndGoButton.Click += PasteAndGoButton_Click;
            // 
            // qualityComboBox
            // 
            qualityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            qualityComboBox.Items.AddRange(new object[] { "2160", "1440", "1080", "720", "480", "360", "240", "144" });
            qualityComboBox.Location = new Point(144, 47);
            qualityComboBox.Name = "qualityComboBox";
            qualityComboBox.Size = new Size(121, 28);
            qualityComboBox.TabIndex = 5;
            // 
            // audioOnlyCheckBox
            // 
            audioOnlyCheckBox.AutoSize = true;
            audioOnlyCheckBox.Location = new Point(280, 49);
            audioOnlyCheckBox.Name = "audioOnlyCheckBox";
            audioOnlyCheckBox.Size = new Size(105, 24);
            audioOnlyCheckBox.TabIndex = 6;
            audioOnlyCheckBox.Text = "Audio Only";
            // 
            // launchButton
            // 
            launchButton.Location = new Point(12, 80);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(824, 40);
            launchButton.TabIndex = 7;
            launchButton.Text = "Launch";
            launchButton.Click += launchButton_Click;
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(12, 15);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(99, 20);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "YouTube URL:";
            // 
            // qualityLabel
            // 
            qualityLabel.AutoSize = true;
            qualityLabel.Location = new Point(12, 50);
            qualityLabel.Name = "qualityLabel";
            qualityLabel.Size = new Size(114, 20);
            qualityLabel.TabIndex = 4;
            qualityLabel.Text = "Max Quality (p):";
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(12, 188);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(mpvPanel);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(consoleTextBox);
            splitContainer.Size = new Size(824, 392);
            splitContainer.SplitterDistance = 549;
            splitContainer.TabIndex = 8;
            // 
            // mpvPanel
            // 
            mpvPanel.BackColor = Color.Black;
            mpvPanel.Dock = DockStyle.Fill;
            mpvPanel.Location = new Point(0, 0);
            mpvPanel.Name = "mpvPanel";
            mpvPanel.Size = new Size(549, 392);
            mpvPanel.TabIndex = 0;
            // 
            // consoleTextBox
            // 
            consoleTextBox.BackColor = Color.Black;
            consoleTextBox.Dock = DockStyle.Fill;
            consoleTextBox.Font = new Font("Consolas", 10F);
            consoleTextBox.ForeColor = Color.White;
            consoleTextBox.Location = new Point(0, 0);
            consoleTextBox.Multiline = true;
            consoleTextBox.Name = "consoleTextBox";
            consoleTextBox.ReadOnly = true;
            consoleTextBox.ScrollBars = ScrollBars.Both;
            consoleTextBox.Size = new Size(271, 392);
            consoleTextBox.TabIndex = 0;
            // 
            // playPauseButton
            // 
            playPauseButton.Location = new Point(12, 126);
            playPauseButton.Name = "playPauseButton";
            playPauseButton.Size = new Size(40, 30);
            playPauseButton.TabIndex = 0;
            playPauseButton.Text = "⏯";
            playPauseButton.Visible = false;
            playPauseButton.Click += playPauseButton_Click;
            // 
            // volumeSlider
            // 
            volumeSlider.Location = new Point(58, 126);
            volumeSlider.Maximum = 130;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(100, 56);
            volumeSlider.TabIndex = 1;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Value = 100;
            volumeSlider.Visible = false;
            volumeSlider.Scroll += volumeSlider_Scroll;
            // 
            // positionSlider
            // 
            positionSlider.Location = new Point(164, 126);
            positionSlider.Maximum = 10000;
            positionSlider.Name = "positionSlider";
            positionSlider.Size = new Size(589, 56);
            positionSlider.TabIndex = 2;
            positionSlider.TickStyle = TickStyle.None;
            positionSlider.Visible = false;
            positionSlider.Scroll += positionSlider_Scroll;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.ForeColor = Color.Black;
            timeLabel.Location = new Point(759, 126);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(77, 20);
            timeLabel.TabIndex = 3;
            timeLabel.Text = "0:00 / 0:00";
            timeLabel.Visible = false;
            // 
            // MainForm
            // 
            ClientSize = new Size(847, 600);
            Controls.Add(timeLabel);
            Controls.Add(positionSlider);
            Controls.Add(volumeSlider);
            Controls.Add(playPauseButton);
            Controls.Add(urlLabel);
            Controls.Add(urlTextBox);
            Controls.Add(pasteButton);
            Controls.Add(pasteAndGoButton);
            Controls.Add(qualityLabel);
            Controls.Add(qualityComboBox);
            Controls.Add(audioOnlyCheckBox);
            Controls.Add(launchButton);
            Controls.Add(splitContainer);
            Name = "MainForm";
            Text = "MPV YouTube Launcher";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)positionSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}