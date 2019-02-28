using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using ContourAnalysisNS;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AForge.Imaging;
namespace detection
{
    public partial class modelAnswer : Form
    {
        int numberOfQ;
        int numberOfC;
        int nMasterColumns;
        List<CheckBox> listCheckBoxes = new List<CheckBox>();
        public List<NumericUpDown> nQuestionsPerColumnList = new List<NumericUpDown>();
        List<int> answerKey = new List<int>();
        newProcessor processor = new newProcessor();
        newProcessor processorId = new newProcessor();
        mainMain main;
        browse browse;
        public modelAnswer()
        {
            InitializeComponent();
            //drawCheckBoxes();
            //organizeCheckBoxes();
            //drawLabels();
        }
        public void setNumberOfQ(int n)
        {
            numberOfQ = n;
        }
        public void setNumberOfC(int n)
        {
            numberOfC = n;
        }
        public void setMColumns(int d)
        {
            nMasterColumns = d;
        }
        public void setQperColumnList(List<NumericUpDown> list)
        {
            nQuestionsPerColumnList = list;
        }

        public void drawCheckBoxes()
        {
            List<NumericUpDown> xx = new List<NumericUpDown>(3);
            for (int i = 0; i < 3; i++)
            {
                NumericUpDown test = new NumericUpDown();
                test.Value = 40;
                xx.Add(test);
            }
            for (int j = 1 ; j <= numberOfQ ; j++)
            {
                for (int k = 0; k < numberOfC; k++)
                {
                    CheckBox box = new CheckBox();
                    box.CheckAlign = ContentAlignment.MiddleRight;
                    if (k == 0)
                    {
                        box.Text = j.ToString();
                    }
                    modelAnswerPanel.Controls.Add(box);
                    listCheckBoxes.Add(box);
                }
            }
            organizeCheckBoxes();
        }

        public void organizeCheckBoxes()
        {
            int nQuestions = (int)nQuestionsPerColumnList[0].Value;
            int initialPadding = 30;
            int xPadding = 0;
            int yPadding = 0;
            int opCount = 0;
            int questionCount = 0;
            int mColumnCount = 0;
            foreach (var control in modelAnswerPanel.Controls)
            {
                if (control is CheckBox)
                {
                    (control as CheckBox).AutoSize = true;
                    (control as CheckBox).Left = modelAnswerPanel.Left + initialPadding + xPadding;
                    (control as CheckBox).Top = yPadding;
                    if (questionCount < 9)
                    {
                        (control as CheckBox).Left = modelAnswerPanel.Left + initialPadding + xPadding + 7;
                        xPadding = xPadding + 63;
                        goto sharrif;
                    }
                    xPadding = xPadding + 70;
                sharrif:
                    opCount++;
                    if (opCount == numberOfC)
                    {
                        yPadding = yPadding + 30;
                        xPadding = 0;
                        opCount = 0;
                        questionCount++;
                    }
                    if (questionCount == nQuestions)
                    {
                        initialPadding = initialPadding + 300;
                        xPadding = 0;
                        yPadding = 0;
                        mColumnCount++;
                        if (mColumnCount == nMasterColumns)
                            return;
                        nQuestions = nQuestions + (int)nQuestionsPerColumnList[mColumnCount].Value;
                    }
                        
                }
                main.progressBar1.Value++;
            }
        }



        private void modelAnswerDefaultBTN_Click(object sender, EventArgs e)
        {
            int k = 1;
            for (int i = 0; i < listCheckBoxes.Count(); i = i + numberOfC)
            {
                listCheckBoxes[i + k].Checked = true;
                if (k == numberOfC)
                    k = 0;
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            modelAnswerPanel.Controls.Remove(listCheckBoxes[160]);
        }

        private void modelAnswerSubmitBTN_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listCheckBoxes.Count; i = i + numberOfC)
            {
                for (int k = 0; k < numberOfC; k++)
                {
                    if (listCheckBoxes[i + k].Checked == true)
                    {
                        if (k == 0)
                            answerKey.Add(1);
                        else if (k == 1)
                            answerKey.Add(2);
                        else if (k == 2)
                            answerKey.Add(3);
                        else if (k == 3)
                            answerKey.Add(4);
                        else if (k == 4)
                            answerKey.Add(5);
                        else if (k == 5)
                            answerKey.Add(6);
                    }
                }
            }
            browse.setAnswerKey(answerKey);
            browse.setNOptions(numberOfC);
            browse.setProcessor(processor);

            browse.setIdProcessor(processorId);

            browse.Show();
            this.Close();

            Console.WriteLine("Answer Key");
            for (int z = 0; z < answerKey.Count; z++)
                Console.Write("{0} , ",answerKey[z]);
            Console.WriteLine("Answer Key:{0}", answerKey.Count);
        }
        public void setProcessor(newProcessor p)
        {
            processor = p;
        }
        public void setIdProcessor(newProcessor p)
        {
            processorId = p;
        }
        public void setMain(mainMain m)
        {
            main = m;
        }
        
        public void setBrowse(browse b)
        {
            browse = b;
        }
        public List<int> getAnswerKey()
        {
            return answerKey;
        }
    }
}

