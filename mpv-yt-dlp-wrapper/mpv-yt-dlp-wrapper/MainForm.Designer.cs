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
            SuspendLayout();
            // 
            // urlTextBox
            // 
            urlTextBox.Location = new Point(144, 12);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.Size = new Size(382, 27);
            urlTextBox.TabIndex = 1;
            urlTextBox.TextChanged += UrlTextBox_TextChanged;
            // 
            // pasteButton
            // 
            pasteButton.Location = new Point(532, 10);
            pasteButton.Name = "pasteButton";
            pasteButton.Size = new Size(60, 29);
            pasteButton.TabIndex = 0;
            pasteButton.Text = "Paste";
            pasteButton.Click += PasteButton_Click;
            // 
            // pasteAndGoButton
            // 
            pasteAndGoButton.Location = new Point(598, 10);
            pasteAndGoButton.Name = "pasteAndGoButton";
            pasteAndGoButton.Size = new Size(110, 29);
            pasteAndGoButton.TabIndex = 0;
            pasteAndGoButton.Text = "Paste and Go";
            pasteAndGoButton.Click += PasteAndGoButton_Click;
            // 
            // qualityComboBox
            // 
            qualityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            qualityComboBox.FormattingEnabled = true;
            qualityComboBox.Items.AddRange(new object[] { "2160", "1440", "1080", "720", "480", "360", "240", "144" });
            qualityComboBox.Location = new Point(144, 42);
            qualityComboBox.Name = "qualityComboBox";
            qualityComboBox.Size = new Size(121, 28);
            qualityComboBox.TabIndex = 3;
            // 
            // audioOnlyCheckBox
            // 
            audioOnlyCheckBox.AutoSize = true;
            audioOnlyCheckBox.Location = new Point(144, 76);
            audioOnlyCheckBox.Name = "audioOnlyCheckBox";
            audioOnlyCheckBox.Size = new Size(105, 24);
            audioOnlyCheckBox.TabIndex = 4;
            audioOnlyCheckBox.Text = "Audio Only";
            // 
            // launchButton
            // 
            launchButton.Location = new Point(12, 106);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(696, 126);
            launchButton.TabIndex = 5;
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
            qualityLabel.Location = new Point(12, 45);
            qualityLabel.Name = "qualityLabel";
            qualityLabel.Size = new Size(114, 20);
            qualityLabel.TabIndex = 2;
            qualityLabel.Text = "Max Quality (p):";
            // 
            // MainForm
            // 
            ClientSize = new Size(717, 244);
            Controls.Add(urlLabel);
            Controls.Add(urlTextBox);
            Controls.Add(pasteButton);
            Controls.Add(pasteAndGoButton);
            Controls.Add(qualityLabel);
            Controls.Add(qualityComboBox);
            Controls.Add(audioOnlyCheckBox);
            Controls.Add(launchButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MPV YouTube Launcher";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}