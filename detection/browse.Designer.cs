namespace detection
{
    partial class browse
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
            this.browseBTN = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.nextBTN = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // browseBTN
            // 
            this.browseBTN.Location = new System.Drawing.Point(12, 53);
            this.browseBTN.Name = "browseBTN";
            this.browseBTN.Size = new System.Drawing.Size(118, 23);
            this.browseBTN.TabIndex = 0;
            this.browseBTN.Text = "Browse";
            this.browseBTN.UseVisualStyleBackColor = true;
            this.browseBTN.Click += new System.EventHandler(this.browseBTN_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.originalPictureBox);
            this.panel3.Location = new System.Drawing.Point(317, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 435);
            this.panel3.TabIndex = 45;
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
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "Show current image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nextBTN
            // 
            this.nextBTN.Location = new System.Drawing.Point(12, 19);
            this.nextBTN.Name = "nextBTN";
            this.nextBTN.Size = new System.Drawing.Size(118, 28);
            this.nextBTN.TabIndex = 48;
            this.nextBTN.Text = "Generate Results";
            this.nextBTN.UseVisualStyleBackColor = true;
            this.nextBTN.Click += new System.EventHandler(this.nextBTN_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 291);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(308, 23);
            this.progressBar1.TabIndex = 49;
            // 
            // browse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 449);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.nextBTN);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.browseBTN);
            this.Name = "browse";
            this.Text = "browse";
            this.Load += new System.EventHandler(this.browse_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button browseBTN;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button nextBTN;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}