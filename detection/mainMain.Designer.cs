namespace detection
{
    partial class mainMain
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
            this.examSettingsPanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.nextBTN = new System.Windows.Forms.Button();
            this.nQuestionsPerMasterColumnLabel = new System.Windows.Forms.Label();
            this.nMasterColumnsLabel = new System.Windows.Forms.Label();
            this.testPanel = new System.Windows.Forms.Panel();
            this.nOptionsPerQuestionLabel = new System.Windows.Forms.Label();
            this.nMasterColumnsComboBox = new System.Windows.Forms.ComboBox();
            this.examSettingsLabel = new System.Windows.Forms.Label();
            this.nOptionsPerQuestionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.totalNumberOfQuestionsLabel = new System.Windows.Forms.Label();
            this.totalQuestionsTextbox = new System.Windows.Forms.TextBox();
            this.examSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nOptionsPerQuestionNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // examSettingsPanel
            // 
            this.examSettingsPanel.Controls.Add(this.progressBar1);
            this.examSettingsPanel.Controls.Add(this.nextBTN);
            this.examSettingsPanel.Controls.Add(this.nQuestionsPerMasterColumnLabel);
            this.examSettingsPanel.Controls.Add(this.nMasterColumnsLabel);
            this.examSettingsPanel.Controls.Add(this.testPanel);
            this.examSettingsPanel.Controls.Add(this.nOptionsPerQuestionLabel);
            this.examSettingsPanel.Controls.Add(this.nMasterColumnsComboBox);
            this.examSettingsPanel.Controls.Add(this.examSettingsLabel);
            this.examSettingsPanel.Controls.Add(this.nOptionsPerQuestionNumericUpDown);
            this.examSettingsPanel.Controls.Add(this.totalNumberOfQuestionsLabel);
            this.examSettingsPanel.Controls.Add(this.totalQuestionsTextbox);
            this.examSettingsPanel.Location = new System.Drawing.Point(86, 12);
            this.examSettingsPanel.Name = "examSettingsPanel";
            this.examSettingsPanel.Size = new System.Drawing.Size(268, 260);
            this.examSettingsPanel.TabIndex = 49;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(120, 178);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 50;
            // 
            // nextBTN
            // 
            this.nextBTN.Location = new System.Drawing.Point(99, 232);
            this.nextBTN.Name = "nextBTN";
            this.nextBTN.Size = new System.Drawing.Size(75, 23);
            this.nextBTN.TabIndex = 11;
            this.nextBTN.Text = "Next";
            this.nextBTN.UseVisualStyleBackColor = true;
            this.nextBTN.Click += new System.EventHandler(this.nextBTN_Click);
            // 
            // nQuestionsPerMasterColumnLabel
            // 
            this.nQuestionsPerMasterColumnLabel.AutoSize = true;
            this.nQuestionsPerMasterColumnLabel.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nQuestionsPerMasterColumnLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nQuestionsPerMasterColumnLabel.Location = new System.Drawing.Point(37, 159);
            this.nQuestionsPerMasterColumnLabel.Name = "nQuestionsPerMasterColumnLabel";
            this.nQuestionsPerMasterColumnLabel.Size = new System.Drawing.Size(277, 16);
            this.nQuestionsPerMasterColumnLabel.TabIndex = 6;
            this.nQuestionsPerMasterColumnLabel.Text = "Number of Questions per Master Column";
            // 
            // nMasterColumnsLabel
            // 
            this.nMasterColumnsLabel.AutoSize = true;
            this.nMasterColumnsLabel.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nMasterColumnsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nMasterColumnsLabel.Location = new System.Drawing.Point(27, 112);
            this.nMasterColumnsLabel.Name = "nMasterColumnsLabel";
            this.nMasterColumnsLabel.Size = new System.Drawing.Size(188, 16);
            this.nMasterColumnsLabel.TabIndex = 4;
            this.nMasterColumnsLabel.Text = "Number of Master Columns";
            // 
            // testPanel
            // 
            this.testPanel.Location = new System.Drawing.Point(6, 198);
            this.testPanel.Name = "testPanel";
            this.testPanel.Size = new System.Drawing.Size(257, 19);
            this.testPanel.TabIndex = 9;
            // 
            // nOptionsPerQuestionLabel
            // 
            this.nOptionsPerQuestionLabel.AutoSize = true;
            this.nOptionsPerQuestionLabel.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nOptionsPerQuestionLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nOptionsPerQuestionLabel.Location = new System.Drawing.Point(16, 78);
            this.nOptionsPerQuestionLabel.Name = "nOptionsPerQuestionLabel";
            this.nOptionsPerQuestionLabel.Size = new System.Drawing.Size(223, 16);
            this.nOptionsPerQuestionLabel.TabIndex = 2;
            this.nOptionsPerQuestionLabel.Text = "Number of Options per Question";
            // 
            // nMasterColumnsComboBox
            // 
            this.nMasterColumnsComboBox.FormattingEnabled = true;
            this.nMasterColumnsComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.nMasterColumnsComboBox.Location = new System.Drawing.Point(203, 78);
            this.nMasterColumnsComboBox.MaxLength = 8;
            this.nMasterColumnsComboBox.Name = "nMasterColumnsComboBox";
            this.nMasterColumnsComboBox.Size = new System.Drawing.Size(44, 21);
            this.nMasterColumnsComboBox.TabIndex = 8;
            this.nMasterColumnsComboBox.SelectionChangeCommitted += new System.EventHandler(this.nMasterColumnsComboBox_SelectionChangeCommitted);
            // 
            // examSettingsLabel
            // 
            this.examSettingsLabel.AutoSize = true;
            this.examSettingsLabel.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.examSettingsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.examSettingsLabel.Location = new System.Drawing.Point(100, 9);
            this.examSettingsLabel.Name = "examSettingsLabel";
            this.examSettingsLabel.Size = new System.Drawing.Size(120, 18);
            this.examSettingsLabel.TabIndex = 7;
            this.examSettingsLabel.Text = "Exam Settings";
            // 
            // nOptionsPerQuestionNumericUpDown
            // 
            this.nOptionsPerQuestionNumericUpDown.Location = new System.Drawing.Point(203, 51);
            this.nOptionsPerQuestionNumericUpDown.Name = "nOptionsPerQuestionNumericUpDown";
            this.nOptionsPerQuestionNumericUpDown.Size = new System.Drawing.Size(41, 20);
            this.nOptionsPerQuestionNumericUpDown.TabIndex = 3;
            this.nOptionsPerQuestionNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // totalNumberOfQuestionsLabel
            // 
            this.totalNumberOfQuestionsLabel.AutoSize = true;
            this.totalNumberOfQuestionsLabel.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalNumberOfQuestionsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.totalNumberOfQuestionsLabel.Location = new System.Drawing.Point(16, 30);
            this.totalNumberOfQuestionsLabel.Name = "totalNumberOfQuestionsLabel";
            this.totalNumberOfQuestionsLabel.Size = new System.Drawing.Size(187, 16);
            this.totalNumberOfQuestionsLabel.TabIndex = 1;
            this.totalNumberOfQuestionsLabel.Text = "Total Number Of Questions";
            // 
            // totalQuestionsTextbox
            // 
            this.totalQuestionsTextbox.Location = new System.Drawing.Point(203, 27);
            this.totalQuestionsTextbox.Name = "totalQuestionsTextbox";
            this.totalQuestionsTextbox.Size = new System.Drawing.Size(41, 20);
            this.totalQuestionsTextbox.TabIndex = 0;
            this.totalQuestionsTextbox.Text = "80";
            // 
            // mainMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 470);
            this.Controls.Add(this.examSettingsPanel);
            this.Name = "mainMain";
            this.Text = "mainMain";
            this.Load += new System.EventHandler(this.mainMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.mainMain_Paint);
            this.Resize += new System.EventHandler(this.mainMain_Resize);
            this.examSettingsPanel.ResumeLayout(false);
            this.examSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nOptionsPerQuestionNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel examSettingsPanel;
        private System.Windows.Forms.Panel testPanel;
        private System.Windows.Forms.ComboBox nMasterColumnsComboBox;
        private System.Windows.Forms.Label examSettingsLabel;
        private System.Windows.Forms.Label nQuestionsPerMasterColumnLabel;
        private System.Windows.Forms.Label nMasterColumnsLabel;
        private System.Windows.Forms.NumericUpDown nOptionsPerQuestionNumericUpDown;
        private System.Windows.Forms.Label nOptionsPerQuestionLabel;
        private System.Windows.Forms.Label totalNumberOfQuestionsLabel;
        private System.Windows.Forms.TextBox totalQuestionsTextbox;
        private System.Windows.Forms.Button nextBTN;
        public System.Windows.Forms.ProgressBar progressBar1;
    }
}