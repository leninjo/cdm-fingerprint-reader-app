namespace LectorHuella
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            AppTitle = new Label();
            ReaderConnect = new Button();
            ReadFingeprint = new Button();
            ShowFingerprint = new PictureBox();
            AppCloseButton = new Button();
            AppMinimizeButton = new Button();
            StatusLabel = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            CopyUUID = new Button();
            UUIDLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)ShowFingerprint).BeginInit();
            SuspendLayout();
            // 
            // AppTitle
            // 
            AppTitle.AutoSize = true;
            AppTitle.Font = new Font("DM Sans 14pt", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AppTitle.ForeColor = Color.FromArgb(0, 43, 43, 43);
            AppTitle.Location = new Point(9, 9);
            AppTitle.Margin = new Padding(0);
            AppTitle.Name = "AppTitle";
            AppTitle.Size = new Size(211, 25);
            AppTitle.TabIndex = 0;
            AppTitle.Text = "CDM Lector de Huella";
            // 
            // ReaderConnect
            // 
            ReaderConnect.FlatAppearance.BorderSize = 0;
            ReaderConnect.FlatStyle = FlatStyle.Flat;
            ReaderConnect.Font = new Font("DM Sans 14pt", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ReaderConnect.Location = new Point(207, 77);
            ReaderConnect.Name = "ReaderConnect";
            ReaderConnect.Size = new Size(205, 102);
            ReaderConnect.TabIndex = 9;
            ReaderConnect.Text = "Verificar lector";
            ReaderConnect.UseVisualStyleBackColor = false;
            ReaderConnect.Click += Connect_Click;
            ReaderConnect.EnabledChanged += (s, e) =>
            {
                ReaderConnect.Invalidate();
            };
            ReaderConnect.Paint += (s, e) =>
            {
                Button btn = (Button)s;
                e.Graphics.Clear(btn.BackColor);

                if (btn.Enabled)
                {
                    btn.BackColor = Color.FromArgb(43, 43, 43);
                    ReaderConnect.Cursor = Cursors.Hand;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(70, 70, 70);
                    ReaderConnect.Cursor = Cursors.No;
                }

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle,
                    btn.Enabled ? Color.White : Color.FromArgb(235, 237, 239), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            // 
            // ReadFingeprint
            // 
            ReadFingeprint.FlatAppearance.BorderSize = 0;
            ReadFingeprint.FlatStyle = FlatStyle.Flat;
            ReadFingeprint.Font = new Font("DM Sans 14pt", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ReadFingeprint.ForeColor = Color.White;
            ReadFingeprint.Location = new Point(207, 185);
            ReadFingeprint.Name = "ReadFingeprint";
            ReadFingeprint.Size = new Size(205, 102);
            ReadFingeprint.TabIndex = 3;
            ReadFingeprint.Text = "Leer huella";
            ReadFingeprint.UseVisualStyleBackColor = false;
            ReadFingeprint.Click += ReadFingeprint_Click;
            ReadFingeprint.EnabledChanged += (s, e) =>
            {
                ReadFingeprint.Invalidate();
            };
            ReadFingeprint.Paint += (s, e) =>
            {
                Button btn = (Button)s;
                e.Graphics.Clear(btn.BackColor);

                if (btn.Enabled)
                {
                    btn.BackColor = Color.FromArgb(43, 43, 43);
                    ReadFingeprint.Cursor = Cursors.Hand;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(70, 70, 70);
                    ReadFingeprint.Cursor = Cursors.No;
                }

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle,
                    btn.Enabled ? Color.White : Color.FromArgb(235, 237, 239), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            // 
            // ShowFingerprint
            // 
            ShowFingerprint.BorderStyle = BorderStyle.FixedSingle;
            ShowFingerprint.Location = new Point(12, 77);
            ShowFingerprint.Margin = new Padding(0);
            ShowFingerprint.Name = "ShowFingerprint";
            ShowFingerprint.Size = new Size(189, 210);
            ShowFingerprint.TabIndex = 5;
            ShowFingerprint.TabStop = false;
            // 
            // AppCloseButton
            // 
            AppCloseButton.Cursor = Cursors.Hand;
            AppCloseButton.FlatAppearance.BorderSize = 0;
            AppCloseButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            AppCloseButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            AppCloseButton.FlatStyle = FlatStyle.Flat;
            AppCloseButton.Font = new Font("DM Sans 14pt", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AppCloseButton.Image = (Image)resources.GetObject("AppCloseButton.Image");
            AppCloseButton.Location = new Point(388, 10);
            AppCloseButton.Margin = new Padding(0);
            AppCloseButton.Name = "AppCloseButton";
            AppCloseButton.Size = new Size(25, 25);
            AppCloseButton.TabIndex = 6;
            AppCloseButton.UseVisualStyleBackColor = true;
            AppCloseButton.Click += CloseButton_Click;
            // 
            // AppMinimizeButton
            // 
            AppMinimizeButton.Cursor = Cursors.Hand;
            AppMinimizeButton.FlatAppearance.BorderSize = 0;
            AppMinimizeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            AppMinimizeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            AppMinimizeButton.FlatStyle = FlatStyle.Flat;
            AppMinimizeButton.Image = (Image)resources.GetObject("AppMinimizeButton.Image");
            AppMinimizeButton.Location = new Point(362, 10);
            AppMinimizeButton.Margin = new Padding(0);
            AppMinimizeButton.Name = "AppMinimizeButton";
            AppMinimizeButton.Size = new Size(25, 25);
            AppMinimizeButton.TabIndex = 7;
            AppMinimizeButton.UseVisualStyleBackColor = true;
            AppMinimizeButton.Click += MinimizeButton_Click;
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Font = new Font("DM Sans 14pt", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StatusLabel.ForeColor = Color.FromArgb(0, 43, 43, 43);
            StatusLabel.Location = new Point(7, 54);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(0, 20);
            StatusLabel.TabIndex = 8;
            // 
            // button1
            // 
            CopyUUID.FlatStyle = FlatStyle.Flat;
            CopyUUID.Font = new Font("DM Sans 14pt", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CopyUUID.Location = new Point(300, 355);
            CopyUUID.Name = "CopyUUID";
            CopyUUID.Size = new Size(112, 34);
            CopyUUID.TabIndex = 10;
            CopyUUID.Text = "Copiar ID";
            CopyUUID.UseVisualStyleBackColor = true;
            CopyUUID.Click += CopyUUID_Click;
            // 
            // UUIDLabel
            // 
            UUIDLabel.AutoSize = true;
            UUIDLabel.Font = new Font("DM Sans 14pt", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UUIDLabel.ForeColor = Color.FromArgb(0, 52, 152, 219);
            UUIDLabel.Location = new Point(34, 311);
            UUIDLabel.Name = "UUIDLabel";
            UUIDLabel.Size = new Size(353, 21);
            UUIDLabel.TabIndex = 11;
            UUIDLabel.Text = "2b1de602-eaef-4a05-b832-484dd5acbe45";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(424, 401);
            Controls.Add(UUIDLabel);
            Controls.Add(CopyUUID);
            Controls.Add(StatusLabel);
            Controls.Add(AppMinimizeButton);
            Controls.Add(AppCloseButton);
            Controls.Add(ShowFingerprint);
            Controls.Add(ReadFingeprint);
            Controls.Add(ReaderConnect);
            Controls.Add(AppTitle);
            Font = new Font("DM Sans 14pt", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CDM";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)ShowFingerprint).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AppTitle;
        private Button ReaderConnect;
        private Button ReadFingeprint;
        private PictureBox ShowFingerprint;
        private Button AppCloseButton;
        private Button AppMinimizeButton;
        private Label StatusLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button CopyUUID;
        private Label UUIDLabel;
    }
}
