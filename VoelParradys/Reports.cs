using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel; 

namespace VoelParadys
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            // Determine how to export the data to an excel file
            ExportCashUpToExcel();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            // Print the data here
            PrintCashUp();
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            // Open the form to view all the customers, and possibly print them
            // Make so that only one form is opened, with arguments defining either customers or suppliers
        }

        private void SuppliersButton_Click(object sender, EventArgs e)
        {
            // Open the form to view all the customers, and possibly print them
            // Make so that only one form is opened, with arguments defining either customers or suppliers
        }

        private void CashUpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CashUpRadioButton.Checked)
                ToDateTimePicker.Enabled = false;
            else
                ToDateTimePicker.Enabled = true;
        }

        private void PrintCashUp()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sSelectedDateFile = FromDateTimePicker.Value.ToString("dd_MM_yy");
            string sPrintDate = FromDateTimePicker.Value.ToString("dd MMM yy");

            if (rDataController.DoesFileExist(sSelectedDateFile, "Daily"))
            {
                List<VoelParadysXmlParser.SSaleDetails> theSaleDetailsList = new List<VoelParadysXmlParser.SSaleDetails>();
                rDataController.ReadDailySaleDataFromDB(sSelectedDateFile, ref theSaleDetailsList, true);
                
                // Setup the form for printing
                // Reformat the list view so that has a standard column width. It does not matter that it will be going over the page border, we just shrink to fit it,
                // and then we are sure that the form is uniformed.
                System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
                theListView.View = View.Details;
                theListView.GridLines = true;
                theListView.FullRowSelect = true;

                theListView.Columns.Add("Item Code", 120);
                theListView.Columns.Add("Qty", 55);
                theListView.Columns.Add("Sell Price", 120);
                theListView.Columns.Add("Total", 86);
                theListView.Columns.Add("Cash Received", 120);
                theListView.Columns.Add("Payment Type", 120);

                string[] arr = new string[6];

                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    ListViewGroup SaleGroup = new ListViewGroup(theSaleDetailsList[i].GetSaleTime());
                    theListView.Groups.Add(SaleGroup);
                    for (int k = 0; k < theSaleDetailsList[i].GetSaleItemsCount(); ++k)
                    {
                        arr[0] = theSaleDetailsList[i].GetSaleItemCodeAt(k);
                        arr[1] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k).ToString();
                        arr[2] = theSaleDetailsList[i].GetSaleItemSellPriceAt(k).ToString("F2");
                        arr[3] = (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k) * theSaleDetailsList[i].GetSaleItemSellPriceAt(k)).ToString("F2");
                        arr[4] = "";
                        arr[5] = "";
                        theListView.Items.Add(new ListViewItem(arr, SaleGroup));
                    }
                    arr[0] = "";
                    arr[1] = "";
                    arr[2] = "";
                    arr[3] = theSaleDetailsList[i].GetSaleTotal().ToString("F2");
                    arr[4] = theSaleDetailsList[i].GetSaleCashReceived().ToString("F2");
                    arr[5] = theSaleDetailsList[i].GetSalePaymentType();
                    theListView.Items.Add(new ListViewItem(arr, SaleGroup));
                }

                for (int j = 0; j < theListView.Columns.Count; ++j)
                    theListView.Columns[j].Width = 150;

                // Create the ListViewPrinter, and assign the list to it
                ListViewPrinter lListPrinter = new ListViewPrinter();
                lListPrinter.ListView = theListView;
                // Give the page a heading and set the ShrinkToFit true
                lListPrinter.Header = "Voel Paradys Cash-up for " + sPrintDate;
                lListPrinter.IsShrinkToFit = true;
                // Remove the standard gradient effects on the header background, and draw a line at the bottom of the header
                lListPrinter.HeaderFormat = BlockFormat.Header();
                lListPrinter.HeaderFormat.TextBrush = Brushes.Black;
                lListPrinter.HeaderFormat.BackgroundBrush = null;
                lListPrinter.HeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));
                // Set the grid pens colour and the header border colour
                lListPrinter.ListHeaderFormat = BlockFormat.ListHeader();
                lListPrinter.ListHeaderFormat.BackgroundBrush = null;
                lListPrinter.ListHeaderFormat.SetBorderPen(Sides.All, new Pen(Color.Black, 0.5f));
                lListPrinter.ListGridPen = new Pen(Color.Black);
                lListPrinter.GroupHeaderFormat = BlockFormat.GroupHeader();
                lListPrinter.GroupHeaderFormat.BackgroundBrush = null;
                lListPrinter.GroupHeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));

                // lListPrinter.PrintPreview();
                // TODO: Once the product is complete, uncomment this to actually print the page, and comment out the PrintPreview to print directly without first viewing it
                lListPrinter.PrintWithDialog();
            }
            else
            {
                // Return an error message
                string sMessage = "Sale data for " + sPrintDate + " does not exist in the database";
                string sCaption = "Error!!!";
                MessageBox.Show(sMessage, sCaption);
            }
        }
        private void ExportCashUpToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sSelectedDateFile = FromDateTimePicker.Value.ToString("dd_MM_yy");
            string sPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");

            if (rDataController.DoesFileExist(sSelectedDateFile, "Daily"))
            {
                
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    string sMessage = "Excel is not installed on this machine!!";
                    string sCaption = "Error!!!";
                    MessageBox.Show(sMessage, sCaption);
                }

                List<VoelParadysXmlParser.SSaleDetails> theSaleDetailsList = new List<VoelParadysXmlParser.SSaleDetails>();
                rDataController.ReadDailySaleDataFromDB(sSelectedDateFile, ref theSaleDetailsList, true);
                int iRowCounter = 1;

                object misValue = System.Reflection.Missing.Value;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                xlWorksheet.Cells.ColumnWidth = 13;
                xlWorksheet.Cells[iRowCounter, 3] = "Voel Paradys Cash-up for " + sPrintDate;
                iRowCounter = 3;
                xlWorksheet.Cells[iRowCounter, 1] = "Item Code";
                xlWorksheet.Cells[iRowCounter, 2] = "Qty";
                xlWorksheet.Cells[iRowCounter, 3] = "Sell Price";
                xlWorksheet.Cells[iRowCounter, 4] = "Total";
                xlWorksheet.Cells[iRowCounter, 5] = "Cash Received";
                xlWorksheet.Cells[iRowCounter, 6] = "Payment Type";

                iRowCounter = 4;
                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleTime();
                    iRowCounter += 1;
                    for (int j = 0; j < theSaleDetailsList[i].GetSaleItemsCount(); ++j)
                    {
                        xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleItemCodeAt(j);
                        xlWorksheet.Cells[iRowCounter, 2] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j).ToString();
                        xlWorksheet.Cells[iRowCounter, 3] = theSaleDetailsList[i].GetSaleItemSellPriceAt(j).ToString("F2");
                        xlWorksheet.Cells[iRowCounter, 4] = (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j) * theSaleDetailsList[i].GetSaleItemSellPriceAt(j)).ToString("F2");
                        iRowCounter += 1;
                    }
                    xlWorksheet.Cells[iRowCounter, 4] = theSaleDetailsList[i].GetSaleTotal().ToString("F2");
                    xlWorksheet.Cells[iRowCounter, 5] = theSaleDetailsList[i].GetSaleCashReceived().ToString("F2");
                    xlWorksheet.Cells[iRowCounter, 6] = theSaleDetailsList[i].GetSalePaymentType();
                    iRowCounter += 1;
                }
                
               
                string sXlFilePathName = GetRelativePath() + sSelectedDateFile + ".xls";
                // "VoelParadys/Data/Exports/" + sSelectedDateFile + ".xls";
                xlWorkbook.SaveAs(sXlFilePathName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkbook.Close(true, misValue, misValue);
                xlApp.Quit();
                ReleaseObject(xlWorksheet);
                ReleaseObject(xlWorkbook);
                ReleaseObject(xlApp);
            }
            else
            {
                // Return an error message
                string sMessage = "Sale data for " + sPrintDate + " does not exist in the database";
                string sCaption = "Error!!!";
                MessageBox.Show(sMessage, sCaption);
            }
        }
        private string GetRelativePath()
        {
            string sCurrentDir = Environment.CurrentDirectory;
            DirectoryInfo theDirectory = new DirectoryInfo(sCurrentDir);
            string sFullDirectory = theDirectory.FullName.ToString();
            sFullDirectory = sFullDirectory.Replace("\\Release", "\\Data\\Exports\\");
            return sFullDirectory;
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
