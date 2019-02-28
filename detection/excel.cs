using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
namespace detection
{
    public partial class excel : Form
    {
        List<int> grades = new List<int>();
        List<double> ids = new List<double>();
        public excel()
        {
            InitializeComponent();
        }

        public void setGrades(List<int> g)
        {
            grades = g;
        }
        public void setIDs(List<double> i)
        {
            ids = i;
        }

        private void excel_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Total Grade";
            for(int i = 0 ; i < grades.Count(); i++)
            {
                string[] row = new string[] { ids[i].ToString(), grades[i].ToString() };
                dataGridView1.Rows.Add(row);
            }
            
        }

        private void exportBTN_Click(object sender, EventArgs e)
        {
            // creating Excel Application

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application

            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            // creating new Excelsheet in workbook

            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program

            //app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.

            // store its reference to worksheet

            worksheet = workbook.Sheets["Sheet1"];

            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet

            worksheet.Name = "Exported from gridview";

            // storing header part in Excel

            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {

                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;

            }

            // storing Each row and column value to excel sheet

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();

                }

            }

            // save the application
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File|*.xlsx;"; 
            sfd.FileName = "Exam Grades";   //default name
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                workbook.SaveAs(sfd.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            // Exit from the application

            app.Quit();
        }
        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
    }
}
