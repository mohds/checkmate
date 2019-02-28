using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using ContourAnalysisNS;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AForge.Imaging;
using System.ComponentModel;
using System.Threading;
using System.Drawing.Drawing2D;
namespace detection
{
    public partial class mainMain : Form
    {
        modelAnswer model = new modelAnswer();
        List<NumericUpDown> masterColumnQuestionsList = new List<NumericUpDown>();
        List<Label> labelList = new List<Label>();
        mainForm main = new mainForm();
        ImageProcessor processor = new ImageProcessor();
        int oldNumber = 0;
        public mainMain()
        {
            InitializeComponent();
            
        }

        private void nMasterColumnsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            masterColumnQuestionsList.Clear();
            labelList.Clear();
            int initialPadding = 10;
            int padding = 0;
            int m = 0;
            int x = int.Parse(nMasterColumnsComboBox.SelectedItem.ToString());
            while (m < x)
            {
                NumericUpDown newColumn = new NumericUpDown();
                newColumn.Width = (4 * nOptionsPerQuestionNumericUpDown.Width / 5);
                newColumn.Height = nOptionsPerQuestionNumericUpDown.Height;
                newColumn.Left = newColumn.Left + initialPadding + padding;
                newColumn.Maximum = 1000;
                newColumn.Value = 40;
                masterColumnQuestionsList.Add(newColumn);
                Label colLabel = new Label();
                colLabel.AutoSize = true;
                colLabel.Top = newColumn.Top + 3 * newColumn.Height / 2;
                colLabel.Left = newColumn.Left;
                colLabel.Text = "Col " + (m + 1).ToString();
                labelList.Add(colLabel);
                padding = padding + 50;
                m++;
            }
            testPanel.Controls.Clear();
            for (int i = 0; i < masterColumnQuestionsList.Count; i++)
            {
                testPanel.Controls.Add(masterColumnQuestionsList[i]);
                testPanel.Controls.Add(labelList[i]);
            }
            oldNumber = masterColumnQuestionsList.Count;
            Console.WriteLine("masterColumnQuestionsListCount = {0}", masterColumnQuestionsList.Count);
        }

        private void nextBTN_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = int.Parse(totalQuestionsTextbox.Text) * (int)nOptionsPerQuestionNumericUpDown.Value;
            model.setQperColumnList(masterColumnQuestionsList);
            model.setNumberOfQ(int.Parse(totalQuestionsTextbox.Text));
            model.setNumberOfC((int)nOptionsPerQuestionNumericUpDown.Value);
            model.setMColumns(int.Parse(nMasterColumnsComboBox.SelectedItem.ToString()));
            model.drawCheckBoxes();
            main.setModelAnswerForm(model);
            processor.setNumberOfQ(int.Parse(totalQuestionsTextbox.Text));
            processor.setNumberOfC((int)nOptionsPerQuestionNumericUpDown.Value);
            processor.setQperColumnList(masterColumnQuestionsList);
            processor.setNOFM(int.Parse(nMasterColumnsComboBox.Text));
            main.setProcessor(processor);//5edon 3al main
            this.Hide();
            main.Show();
            Console.WriteLine("ok");
        }

        private void mainMain_Load(object sender, EventArgs e)
        {
            this.Text = "CheckMate™";
            examSettingsPanel.BackColor = Color.Transparent;
            progressBar1.Value = 0;
            model.setMain(this);
            examSettingsPanel.Left = 20 ;
            examSettingsPanel.Width = this.Width - this.Width / 10;
            examSettingsPanel.Height = this.Height - this.Height / 8;
            examSettingsLabel.Left =  6 * examSettingsPanel.Width / 15;
            examSettingsLabel.Top = examSettingsPanel.Top + examSettingsPanel.Height / 15;
            totalNumberOfQuestionsLabel.Left = examSettingsPanel.Width / 10;
            totalNumberOfQuestionsLabel.Top = examSettingsLabel.Top + 3 * examSettingsLabel.Height;
            totalQuestionsTextbox.Top = totalNumberOfQuestionsLabel.Top;
            totalQuestionsTextbox.Left = totalNumberOfQuestionsLabel.Width + 2 * totalNumberOfQuestionsLabel.Width / 3 ;
            nOptionsPerQuestionLabel.Top = totalNumberOfQuestionsLabel.Top + 3 * totalNumberOfQuestionsLabel.Height;
            nOptionsPerQuestionLabel.Left = examSettingsPanel.Width / 10;
            nOptionsPerQuestionNumericUpDown.Top = nOptionsPerQuestionLabel.Top;
            nOptionsPerQuestionNumericUpDown.Left = totalQuestionsTextbox.Left ;
            nMasterColumnsLabel.Top = nOptionsPerQuestionLabel.Top + 3 * nOptionsPerQuestionLabel.Height;
            nMasterColumnsLabel.Left = examSettingsPanel.Width / 10;
            nMasterColumnsComboBox.Top = nMasterColumnsLabel.Top;
            nMasterColumnsComboBox.Left = totalQuestionsTextbox.Left;
            nQuestionsPerMasterColumnLabel.Top = nMasterColumnsLabel.Top + 4 * nMasterColumnsLabel.Height;
            nQuestionsPerMasterColumnLabel.Left = 1 * examSettingsPanel.Width / 5;
            testPanel.Top = nQuestionsPerMasterColumnLabel.Top + 2 * nQuestionsPerMasterColumnLabel.Height;
            testPanel.Width = examSettingsPanel.Width;
            testPanel.Height = 7 * examSettingsLabel.Height / 2;
            nextBTN.Top = testPanel.Top + testPanel.Height;
            nextBTN.Left = examSettingsLabel.Left;
            progressBar1.Top = nextBTN.Top + 2 * nextBTN.Height;
            progressBar1.Left = examSettingsPanel.Width / 10;
            progressBar1.Width = examSettingsPanel.Width - 2 * progressBar1.Left;

        }

        private void mainMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainMain_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
