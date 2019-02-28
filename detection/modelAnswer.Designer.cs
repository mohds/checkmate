namespace detection
{
    partial class modelAnswer
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
            this.modelAnswerPanel = new System.Windows.Forms.Panel();
            this.modelAnswerSubmitBTN = new System.Windows.Forms.Button();
            this.modelAnswerDefaultBTN = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modelAnswerPanel
            // 
            this.modelAnswerPanel.AutoScroll = true;
            this.modelAnswerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modelAnswerPanel.Location = new System.Drawing.Point(123, 12);
            this.modelAnswerPanel.Name = "modelAnswerPanel";
            this.modelAnswerPanel.Size = new System.Drawing.Size(715, 661);
            this.modelAnswerPanel.TabIndex = 0;
            // 
            // modelAnswerSubmitBTN
            // 
            this.modelAnswerSubmitBTN.Location = new System.Drawing.Point(21, 28);
            this.modelAnswerSubmitBTN.Name = "modelAnswerSubmitBTN";
            this.modelAnswerSubmitBTN.Size = new System.Drawing.Size(75, 23);
            this.modelAnswerSubmitBTN.TabIndex = 1;
            this.modelAnswerSubmitBTN.Text = "Submit";
            this.modelAnswerSubmitBTN.UseVisualStyleBackColor = true;
            this.modelAnswerSubmitBTN.Click += new System.EventHandler(this.modelAnswerSubmitBTN_Click);
            // 
            // modelAnswerDefaultBTN
            // 
            this.modelAnswerDefaultBTN.Location = new System.Drawing.Point(21, 74);
            this.modelAnswerDefaultBTN.Name = "modelAnswerDefaultBTN";
            this.modelAnswerDefaultBTN.Size = new System.Drawing.Size(75, 23);
            this.modelAnswerDefaultBTN.TabIndex = 2;
            this.modelAnswerDefaultBTN.Text = "Default";
            this.modelAnswerDefaultBTN.UseVisualStyleBackColor = true;
            this.modelAnswerDefaultBTN.Click += new System.EventHandler(this.modelAnswerDefaultBTN_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // modelAnswer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 685);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.modelAnswerDefaultBTN);
            this.Controls.Add(this.modelAnswerSubmitBTN);
            this.Controls.Add(this.modelAnswerPanel);
            this.Name = "modelAnswer";
            this.Text = "modelAnswer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel modelAnswerPanel;
        private System.Windows.Forms.Button modelAnswerSubmitBTN;
        private System.Windows.Forms.Button modelAnswerDefaultBTN;
        private System.Windows.Forms.Button button1;
    }
}