namespace detection
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.open = new System.Windows.Forms.Button();
            this.cropID = new System.Windows.Forms.Button();
            this.circleAreaLabel = new System.Windows.Forms.Label();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maximumAreaTrackBar = new TrackBar.Dotnetrix.Controls.TrackBar();
            this.minimumAreaTrackBar = new TrackBar.Dotnetrix.Controls.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.maxAreaLabel = new System.Windows.Forms.Label();
            this.minAreaLabel = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.zoomIn = new System.Windows.Forms.Button();
            this.zoomOut = new System.Windows.Forms.Button();
            this.croppedImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAreaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAreaTrackBar)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.croppedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.open.Location = new System.Drawing.Point(11, 19);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(79, 23);
            this.open.TabIndex = 1;
            this.open.Text = "open";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // cropID
            // 
            this.cropID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cropID.Location = new System.Drawing.Point(11, 48);
            this.cropID.Name = "cropID";
            this.cropID.Size = new System.Drawing.Size(79, 23);
            this.cropID.TabIndex = 2;
            this.cropID.Text = "Crop";
            this.cropID.UseVisualStyleBackColor = true;
            this.cropID.Click += new System.EventHandler(this.cropButton_Click);
            // 
            // circleAreaLabel
            // 
            this.circleAreaLabel.AutoSize = true;
            this.circleAreaLabel.Location = new System.Drawing.Point(8, 80);
            this.circleAreaLabel.Name = "circleAreaLabel";
            this.circleAreaLabel.Size = new System.Drawing.Size(58, 13);
            this.circleAreaLabel.TabIndex = 9;
            this.circleAreaLabel.Text = "Circle Area";
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.originalPictureBox.Location = new System.Drawing.Point(3, 6);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(315, 323);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.originalPictureBox.TabIndex = 11;
            this.originalPictureBox.TabStop = false;
            this.originalPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.originalPictureBox_Paint);
            this.originalPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.originalPictureBox_MouseDown);
            this.originalPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.originalPictureBox_MouseMove);
            this.originalPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.originalPictureBox_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.maximumAreaTrackBar);
            this.groupBox1.Controls.Add(this.minimumAreaTrackBar);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.maxAreaLabel);
            this.groupBox1.Controls.Add(this.minAreaLabel);
            this.groupBox1.Controls.Add(this.submit);
            this.groupBox1.Controls.Add(this.circleAreaLabel);
            this.groupBox1.Controls.Add(this.cropID);
            this.groupBox1.Controls.Add(this.open);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(884, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 643);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Panel";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // maximumAreaTrackBar
            // 
            this.maximumAreaTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.maximumAreaTrackBar.Location = new System.Drawing.Point(117, 113);
            this.maximumAreaTrackBar.Maximum = 5000;
            this.maximumAreaTrackBar.Name = "maximumAreaTrackBar";
            this.maximumAreaTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.maximumAreaTrackBar.Size = new System.Drawing.Size(45, 477);
            this.maximumAreaTrackBar.TabIndex = 47;
            this.maximumAreaTrackBar.Value = 1400;
            this.maximumAreaTrackBar.Scroll += new System.EventHandler(this.maximumAreaTrackBar_Scroll);
            // 
            // minimumAreaTrackBar
            // 
            this.minimumAreaTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.minimumAreaTrackBar.Location = new System.Drawing.Point(21, 113);
            this.minimumAreaTrackBar.Maximum = 5000;
            this.minimumAreaTrackBar.Name = "minimumAreaTrackBar";
            this.minimumAreaTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.minimumAreaTrackBar.Size = new System.Drawing.Size(45, 477);
            this.minimumAreaTrackBar.TabIndex = 46;
            this.minimumAreaTrackBar.Value = 613;
            this.minimumAreaTrackBar.Scroll += new System.EventHandler(this.minimumAreaTrackBar_Scroll);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(96, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 39);
            this.button1.TabIndex = 55;
            this.button1.Text = "submit ID";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // maxAreaLabel
            // 
            this.maxAreaLabel.AutoSize = true;
            this.maxAreaLabel.Location = new System.Drawing.Point(125, 607);
            this.maxAreaLabel.Name = "maxAreaLabel";
            this.maxAreaLabel.Size = new System.Drawing.Size(35, 17);
            this.maxAreaLabel.TabIndex = 54;
            this.maxAreaLabel.Text = "label1";
            this.maxAreaLabel.UseCompatibleTextRendering = true;
            // 
            // minAreaLabel
            // 
            this.minAreaLabel.AutoSize = true;
            this.minAreaLabel.Location = new System.Drawing.Point(8, 607);
            this.minAreaLabel.Name = "minAreaLabel";
            this.minAreaLabel.Size = new System.Drawing.Size(35, 13);
            this.minAreaLabel.TabIndex = 53;
            this.minAreaLabel.Text = "label1";
            // 
            // submit
            // 
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.submit.Location = new System.Drawing.Point(96, 56);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(62, 45);
            this.submit.TabIndex = 37;
            this.submit.Text = "submit exam";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.originalPictureBox);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(321, 435);
            this.panel3.TabIndex = 43;
            this.panel3.Resize += new System.EventHandler(this.panel3_Resize);
            // 
            // zoomIn
            // 
            this.zoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomIn.Location = new System.Drawing.Point(208, 477);
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zoomIn.Size = new System.Drawing.Size(75, 23);
            this.zoomIn.TabIndex = 44;
            this.zoomIn.Text = "Zoom In";
            this.zoomIn.UseVisualStyleBackColor = true;
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomOut.Location = new System.Drawing.Point(102, 477);
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(75, 23);
            this.zoomOut.TabIndex = 45;
            this.zoomOut.Text = "Zoom Out";
            this.zoomOut.UseVisualStyleBackColor = true;
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // croppedImageBox
            // 
            this.croppedImageBox.Location = new System.Drawing.Point(357, 18);
            this.croppedImageBox.Name = "croppedImageBox";
            this.croppedImageBox.Size = new System.Drawing.Size(75, 23);
            this.croppedImageBox.TabIndex = 2;
            this.croppedImageBox.TabStop = false;
            this.croppedImageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.croppedImageBox_Paint);
            // 
            // mainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1096, 674);
            this.Controls.Add(this.croppedImageBox);
            this.Controls.Add(this.zoomOut);
            this.Controls.Add(this.zoomIn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.mainForm_Paint);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAreaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAreaTrackBar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.croppedImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button cropID;
        private System.Windows.Forms.Label circleAreaLabel;
        public System.Windows.Forms.PictureBox originalPictureBox;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button submit;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button zoomIn;
        public System.Windows.Forms.Button zoomOut;
        public Emgu.CV.UI.ImageBox croppedImageBox;
        private System.Windows.Forms.Label minAreaLabel;
        private System.Windows.Forms.Label maxAreaLabel;
        private System.Windows.Forms.Button button1;
        public TrackBar.Dotnetrix.Controls.TrackBar minimumAreaTrackBar;
        public TrackBar.Dotnetrix.Controls.TrackBar maximumAreaTrackBar;
    }
}

