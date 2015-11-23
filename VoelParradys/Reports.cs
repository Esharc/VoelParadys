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
using System.Drawing.Printing;
using System.Globalization;

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
            // Export the data to an excel file
            if (CashUpRadioButton.Checked)
                ExportCashUpToExcel();
            else if (UsageRadioButton.Checked)
                ExportUsageToExcel();
            else if (InventoryRadioButton.Checked)
                ExportInventoryToExcel();
            else if (ProfitRadioButton.Checked)
                ExportProfitReportToExcel();
            else if (CustomersRadioButton.Checked)
                ExportCustomerDatabaseToExcel();
            else if (SuppliersRadioButton.Checked)
                ExportSupplierDatabaseToExcel();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            // Print the data here
            if (CashUpRadioButton.Checked)
                PrintCashUp();
            else if (UsageRadioButton.Checked)
                PrintUsageForSelectedDates();
            else if (InventoryRadioButton.Checked)
                PrintInventory();
            else if (ProfitRadioButton.Checked)
                PrintProfitReport();
            else if (CustomersRadioButton.Checked)
                PrintCustomerDatabase();
            else if (SuppliersRadioButton.Checked)
                PrintSupplierDatabase();
        }

        private void CashUpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CashUpRadioButton.Checked)
                ToDateTimePicker.Enabled = false;
            else
                ToDateTimePicker.Enabled = true;
        }

        private void InventoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (InventoryRadioButton.Checked)
            {
                ToDateTimePicker.Enabled = false;
                FromDateTimePicker.Enabled = false;
            }
            else
            {
                ToDateTimePicker.Enabled = true;
                FromDateTimePicker.Enabled = true;
            }
        }

        private void CustomersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomersRadioButton.Checked)
            {
                ToDateTimePicker.Enabled = false;
                FromDateTimePicker.Enabled = false;
            }
            else
            {
                ToDateTimePicker.Enabled = true;
                FromDateTimePicker.Enabled = true;
            }
        }

        private void SuppliersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SuppliersRadioButton.Checked)
            {
                ToDateTimePicker.Enabled = false;
                FromDateTimePicker.Enabled = false;
            }
            else
            {
                ToDateTimePicker.Enabled = true;
                FromDateTimePicker.Enabled = true;
            }
        }

        //{ The functions for daily cash-up reports
        private void PrintCashUp()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sSelectedDateFile = FromDateTimePicker.Value.ToString("dd_MM_yy");
            string sPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");

            if (rDataController.DoesFileExist(sSelectedDateFile, true))
            {
                List<VoelParadysDataStructures.SSaleDetails> theSaleDetailsList = new List<VoelParadysDataStructures.SSaleDetails>();
                rDataController.ReadDailySaleDataFromDB(sSelectedDateFile, ref theSaleDetailsList, true);
                
                // Setup the form for printing
                // Reformat the list view so that has a standard column width. It does not matter that it will be going over the page border, we just shrink to fit it,
                // and then we are sure that the form is uniformed.
                System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
                theListView.View = View.Details;
                theListView.GridLines = true;
                theListView.FullRowSelect = true;

                theListView.Columns.Add("Item Code", 80);
                theListView.Columns.Add("Item Description", 110);
                theListView.Columns.Add("Qty", 50);
                theListView.Columns.Add("Sell Price", 110);
                theListView.Columns.Add("Total", 110);
                theListView.Columns.Add("Cash Received", 150);
                theListView.Columns.Add("Payment Type", 150);

                string[] arr = new string[7];
                string sName = "";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    ListViewGroup SaleGroup = new ListViewGroup(theSaleDetailsList[i].GetSaleTime());
                    theListView.Groups.Add(SaleGroup);
                    for (int k = 0; k < theSaleDetailsList[i].GetSaleItemsCount(); ++k)
                    {
                        rDataController.GetStockItemData(theSaleDetailsList[i].GetSaleItemCodeAt(k), ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                        arr[0] = theSaleDetailsList[i].GetSaleItemCodeAt(k);
                        arr[1] = sName;
                        arr[2] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k).ToString();
                        arr[3] = "R " + theSaleDetailsList[i].GetSaleItemSellPriceAt(k).ToString("F2");
                        arr[4] = "R " + (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k) * theSaleDetailsList[i].GetSaleItemSellPriceAt(k)).ToString("F2");
                        arr[5] = "";
                        arr[6] = "";
                        theListView.Items.Add(new ListViewItem(arr, SaleGroup));
                    }
                    arr[0] = "";
                    arr[1] = "";
                    arr[2] = "";
                    arr[3] = "";
                    arr[4] = "R " + theSaleDetailsList[i].GetSaleTotal().ToString("F2");
                    arr[5] = "R " + theSaleDetailsList[i].GetSaleCashReceived().ToString("F2");
                    arr[6] = theSaleDetailsList[i].GetSalePaymentType();
                    theListView.Items.Add(new ListViewItem(arr, SaleGroup));
                }
                SetupPrinterAndPrintList(theListView, "\tVoel Paradys Cash-up for " + sPrintDate);
            }
            else
            {
                // Return an error message
                PrintErrorMessage("Sale data for " + sPrintDate + " does not exist in the database");
            }
        }
        private void ExportCashUpToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sSelectedDateFile = FromDateTimePicker.Value.ToString("dd_MM_yy");
            string sPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");

            if (rDataController.DoesFileExist(sSelectedDateFile, true))
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    PrintErrorMessage("Excel is not installed on this machine!!");
                    return;
                }

                List<VoelParadysDataStructures.SSaleDetails> theSaleDetailsList = new List<VoelParadysDataStructures.SSaleDetails>();
                rDataController.ReadDailySaleDataFromDB(sSelectedDateFile, ref theSaleDetailsList, true);
                int iRowCounter = 1;

                object misValue = System.Reflection.Missing.Value;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                xlWorksheet.Cells.ColumnWidth = 18;
                xlWorksheet.Cells[iRowCounter, 3] = "Voel Paradys Cash-up for " + sPrintDate;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
                iRowCounter = 3;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = "Item Code";
                xlWorksheet.Cells[iRowCounter, 2] = "Item Description";
                xlWorksheet.Cells[iRowCounter, 3] = "Qty";
                xlWorksheet.Cells[iRowCounter, 4] = "Sell Price";
                xlWorksheet.Cells[iRowCounter, 5] = "Total";
                xlWorksheet.Cells[iRowCounter, 6] = "Cash Received";
                xlWorksheet.Cells[iRowCounter, 7] = "Payment Type";

                xlWorksheet.Cells[iRowCounter, 1].EntireColumn.NumberFormat = "@";                                     // Formatted as text
                string sName = "";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                iRowCounter = 4;
                var theProgressBar = new ProgressBar(1, theSaleDetailsList.Count);
                theProgressBar.Show();
                string sCurrencyFormat = "R ####0.00_);[Red](R ####0.00)";
                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleTime();
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    iRowCounter += 1;
                    for (int j = 0; j < theSaleDetailsList[i].GetSaleItemsCount(); ++j)
                    {
                        rDataController.GetStockItemData(theSaleDetailsList[i].GetSaleItemCodeAt(j), ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                        xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleItemCodeAt(j);
                        xlWorksheet.Cells[iRowCounter, 2] = sName;
                        xlWorksheet.Cells[iRowCounter, 3] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j);
                        xlWorksheet.Cells[iRowCounter, 4] = theSaleDetailsList[i].GetSaleItemSellPriceAt(j);
                        xlWorksheet.Cells[iRowCounter, 4].NumberFormat = sCurrencyFormat;
                        xlWorksheet.Cells[iRowCounter, 5] = (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j) * theSaleDetailsList[i].GetSaleItemSellPriceAt(j));
                        xlWorksheet.Cells[iRowCounter, 5].NumberFormat = sCurrencyFormat;
                        iRowCounter += 1;
                    }
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xlWorksheet.Cells[iRowCounter, 5] = theSaleDetailsList[i].GetSaleTotal();
                    xlWorksheet.Cells[iRowCounter, 5].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 6] = theSaleDetailsList[i].GetSaleCashReceived();
                    xlWorksheet.Cells[iRowCounter, 6].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 7] = theSaleDetailsList[i].GetSalePaymentType();
                    iRowCounter += 1;
                    theProgressBar.UpdateProgressBar();
                }
                
               
                string sXlFilePathName = GetRelativePath() + "CashUp\\" + sSelectedDateFile + ".xls";
                theProgressBar.CloseProgressBar();
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
                PrintErrorMessage("Sale data for " + sPrintDate + " does not exist in the database");
            }
        }
        //}
        //{ The functions for usage reports between selected dates
        private void PrintUsageForSelectedDates()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sFromPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");
            string sToPrintDate = ToDateTimePicker.Value.ToString("dd MMM yyyy");
            List<VoelParadysDataStructures.UsageData> theUsageData = new List<VoelParadysDataStructures.UsageData>();

            if (rDataController.GetUsageDataList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theUsageData))
            {
                System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
                theListView.View = View.Details;
                theListView.GridLines = true;
                theListView.FullRowSelect = true;

                theListView.Columns.Add("Date", 80);
                theListView.Columns.Add("Item Code", 110);
                theListView.Columns.Add("Description", 160);
                theListView.Columns.Add("Qty", 80);
                theListView.Columns.Add("Cost Price", 110);
                theListView.Columns.Add("Sell Price", 110);
                theListView.Columns.Add("Profit Lost", 110);

                string[] arr = new string[7];
                string sName = "";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                for (int i = 0; i < theUsageData.Count; ++i)
                {
                    rDataController.GetStockItemData(theUsageData[i].sItemCode, ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);

                    arr[0] = theUsageData[i].sDate;
                    arr[1] = theUsageData[i].sItemCode;
                    arr[2] = sName;
                    arr[3] = theUsageData[i].iItemQuantity.ToString();
                    arr[4] = "R " + fCost.ToString("F2");
                    arr[5] = "R " + fSell.ToString("F2");
                    arr[6] = "R " + ((fSell - fCost)*theUsageData[i].iItemQuantity).ToString("F2");
                    theListView.Items.Add(new ListViewItem(arr));
                }
                SetupPrinterAndPrintList(theListView, "\tVoel Paradys Usage " + sFromPrintDate + " to " + sToPrintDate);
            }
            else
            {
                // Return an error message
                PrintErrorMessage("Usage data for " + sFromPrintDate + " to " + sToPrintDate + " does not exist in the database");
            }
        }
        private void ExportUsageToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sFromPrintDate = FromDateTimePicker.Value.ToString("dd/MM/yyyy");
            string sToPrintDate = ToDateTimePicker.Value.ToString("dd/MM/yyyy");
            List<VoelParadysDataStructures.UsageData> theUsageData = new List<VoelParadysDataStructures.UsageData>();

            if (rDataController.GetUsageDataList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theUsageData))
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    PrintErrorMessage("Excel is not installed on this machine!!");
                    return;
                }

                int iRowCounter = 1;

                object misValue = System.Reflection.Missing.Value;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                xlWorksheet.Cells.ColumnWidth = 20;
                xlWorksheet.Cells[iRowCounter, 3] = "Voel Paradys Usage " + sFromPrintDate + " to " + sToPrintDate;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
                iRowCounter = 3;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = "Date";
                xlWorksheet.Cells[iRowCounter, 2] = "Item Code";
                xlWorksheet.Cells[iRowCounter, 3] = "Description";
                xlWorksheet.Cells[iRowCounter, 4] = "Qty";
                xlWorksheet.Cells[iRowCounter, 5] = "Cost Price";
                xlWorksheet.Cells[iRowCounter, 6] = "Sell Price";
                xlWorksheet.Cells[iRowCounter, 7] = "Profit Lost";

                xlWorksheet.Cells[iRowCounter, 2].EntireColumn.NumberFormat = "@";                                     // Formatted as text

                string sName = "", sCurrencyFormat = "R ####0.00_);[Red](R ####0.00)";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                DateTime TheDateFormatter;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
                DateTimeStyles styles = DateTimeStyles.None;

                iRowCounter = 4;
                var theProgressBar = new ProgressBar(1, theUsageData.Count);
                theProgressBar.Show();
                for (int i = 0; i < theUsageData.Count; ++i)
                {
                    DateTime.TryParse(theUsageData[i].sDate, culture, styles, out TheDateFormatter);
                    rDataController.GetStockItemData(theUsageData[i].sItemCode, ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xlWorksheet.Cells[iRowCounter, 1] = TheDateFormatter;
                    xlWorksheet.Cells[iRowCounter, 1].NumberFormat = "dd MMM yyyy";
                    xlWorksheet.Cells[iRowCounter, 2] = theUsageData[i].sItemCode;
                    xlWorksheet.Cells[iRowCounter, 3] = sName;
                    xlWorksheet.Cells[iRowCounter, 4] = theUsageData[i].iItemQuantity;
                    xlWorksheet.Cells[iRowCounter, 5] = fCost;
                    xlWorksheet.Cells[iRowCounter, 5].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 6] = fSell;
                    xlWorksheet.Cells[iRowCounter, 6].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 7] = (fSell - fCost) * theUsageData[i].iItemQuantity;
                    xlWorksheet.Cells[iRowCounter, 7].NumberFormat = sCurrencyFormat;
                    iRowCounter += 1;
                    theProgressBar.UpdateProgressBar();
                }

                sFromPrintDate = sFromPrintDate.Replace("/", "_");
                sToPrintDate = sToPrintDate.Replace("/", "_");
                string sXlFilePathName = GetRelativePath() + "Usage\\" + sFromPrintDate + "_To_" + sToPrintDate + ".xls";
                theProgressBar.CloseProgressBar();
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
                PrintErrorMessage("Usage data for " + sFromPrintDate + " to " + sToPrintDate + " does not exist in the database");
            }
        }
        //}
        //{ The functions for inventory database printing or exporting
        private void PrintInventory()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
            theListView.View = View.Details;
            theListView.GridLines = true;
            theListView.FullRowSelect = true;

            theListView.Columns.Add("Item Code", 120);
            theListView.Columns.Add("Description", 160);
            theListView.Columns.Add("Qty Bought", 120);
            theListView.Columns.Add("Qty Sold", 120);
            theListView.Columns.Add("Qty Used", 120);
            theListView.Columns.Add("Qty In Stock", 120);

            string[] arr = new string[6];
            string sCode = "", sName = "";
            int iQtyS = -1, iQtyB = -1, iQtyU = -1;
            float fCost = -1, fSell = -1;

            for (int i = 0; i < rDataController.GetInventoryListSize(); ++i)
            {
                rDataController.GetStockItemData(i, ref sCode, ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);

                arr[0] = sCode;
                arr[1] = sName;
                arr[2] = iQtyB.ToString();
                arr[3] = iQtyS.ToString();
                arr[4] = iQtyU.ToString();
                arr[5] = (iQtyB - (iQtyS + iQtyU)).ToString();
                theListView.Items.Add(new ListViewItem(arr));
            }
            DateTime theDateAndTime = DateTime.Now;
            SetupPrinterAndPrintList(theListView, "\tVoel Paradys Inventory as at " + theDateAndTime.ToString("dd MMM yyyy"));
        }
        private void ExportInventoryToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();
            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                PrintErrorMessage("Excel is not installed on this machine!!");
                return;
            }

            int iRowCounter = 1;

            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
            xlWorksheet.Cells.ColumnWidth = 20;
            DateTime theDateAndTime = DateTime.Now;
            xlWorksheet.Cells[iRowCounter, 2] = "Voel Paradys Inventory as at " + theDateAndTime.ToString("dd MMM yyyy");
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
            iRowCounter = 3;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorksheet.Cells[iRowCounter, 1] = "Item Code";
            xlWorksheet.Cells[iRowCounter, 2] = "Description";
            xlWorksheet.Cells[iRowCounter, 3] = "Qty Bought";
            xlWorksheet.Cells[iRowCounter, 4] = "Qty Sold";
            xlWorksheet.Cells[iRowCounter, 5] = "Qty Used";
            xlWorksheet.Cells[iRowCounter, 6] = "Qty In Stock";

            xlWorksheet.Cells[iRowCounter, 1].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 2].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 3].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 4].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 5].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 6].EntireColumn.NumberFormat = "@";                                     // Formatted as text

            string sCode = "", sName = "";
            int iQtyS = -1, iQtyB = -1, iQtyU = -1;
            float fCost = -1, fSell = -1;

            iRowCounter = 4;
            var theProgressBar = new ProgressBar(1, rDataController.GetInventoryListSize());
            theProgressBar.Show();
            for (int i = 0; i < rDataController.GetInventoryListSize(); ++i)
            {
                rDataController.GetStockItemData(i, ref sCode, ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = sCode;
                xlWorksheet.Cells[iRowCounter, 2] = sName;
                xlWorksheet.Cells[iRowCounter, 3] = iQtyB;
                xlWorksheet.Cells[iRowCounter, 4] = iQtyS;
                xlWorksheet.Cells[iRowCounter, 5] = iQtyU;
                xlWorksheet.Cells[iRowCounter, 6] = iQtyB - (iQtyS + iQtyU);
                iRowCounter += 1;
                theProgressBar.UpdateProgressBar();
            }

            string sFromPrintDate = theDateAndTime.ToString("dd_MM_yyyy");
            string sXlFilePathName = GetRelativePath() + "Inventory\\" + sFromPrintDate + ".xls";
            theProgressBar.CloseProgressBar();
            xlWorkbook.SaveAs(sXlFilePathName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkbook.Close(true, misValue, misValue);
            xlApp.Quit();
            ReleaseObject(xlWorksheet);
            ReleaseObject(xlWorkbook);
            ReleaseObject(xlApp);
        }
        //}
        //{ The functions for profit report printing or exporting
        private int GetQtyUsedForDate(string sTheDate, string sTheItemCode)
        {
            int iRetVal = 0;
            List<VoelParadysDataStructures.UsageData> theUsageList = new List<VoelParadysDataStructures.UsageData>();
            VoelParadysDataController.GetInstance().GetUsageDataList(sTheDate, sTheDate, ref theUsageList);

            for (int i = 0; i < theUsageList.Count; ++i)
            {
                if (theUsageList[i].sItemCode == sTheItemCode)
                {
                    iRetVal = theUsageList[i].iItemQuantity;
                    break;
                }
            }
            return iRetVal;
        }
        private void PrintProfitReport()
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sFromPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");
            string sToPrintDate = ToDateTimePicker.Value.ToString("dd MMM yyyy");
            List<VoelParadysDataStructures.CStringStringMap> theDailySaleFileData = new List<VoelParadysDataStructures.CStringStringMap>();

            if (rDataController.GetDailySaleFileList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theDailySaleFileData))
            {
                float fTotalSalesProfit = 0, fTotalUsageLoss = 0;
                System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
                theListView.View = View.Details;
                theListView.GridLines = true;
                theListView.FullRowSelect = true;

                theListView.Columns.Add("Date", 80);
                theListView.Columns.Add("Item Code", 100);
                theListView.Columns.Add("Description", 150);
                theListView.Columns.Add("Qty Sold", 70);
                theListView.Columns.Add("Qty Used", 70);
                theListView.Columns.Add("Cost Price", 100);
                theListView.Columns.Add("Sell Price", 100);
                theListView.Columns.Add("Profit Per Item", 100);
                theListView.Columns.Add("Total Profit", 100);
                theListView.Columns.Add("Usage Cost Price", 100);

                string[] arr = new string[10];
                List<VoelParadysDataStructures.SSaleDetails> theSaleDetails = new List<VoelParadysDataStructures.SSaleDetails>();
                string sName = "";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                for (int i = 0; i < theDailySaleFileData.Count; ++i)
                {
                    rDataController.ReadDailySaleDataFromDB(theDailySaleFileData[i].sString2, ref theSaleDetails, false);

                    for (int j = 0; j < theSaleDetails.Count;  ++j)
                    {
                        for (int k = 0; k < theSaleDetails[j].GetSaleItemsCount(); ++k)
                        {
                            rDataController.GetStockItemData(theSaleDetails[j].GetSaleItemCodeAt(k), ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                            iQtyU = GetQtyUsedForDate(theDailySaleFileData[i].sString1, theSaleDetails[j].GetSaleItemCodeAt(k));

                            arr[0] = theDailySaleFileData[i].sString1;
                            arr[1] = theSaleDetails[j].GetSaleItemCodeAt(k);
                            arr[2] = sName;
                            arr[3] = theSaleDetails[j].GetSaleItemQuantitySoldAt(k).ToString();
                            arr[4] = iQtyU.ToString();
                            arr[5] = "R " + fCost.ToString("F2");
                            arr[6] = "R " + theSaleDetails[j].GetSaleItemSellPriceAt(k).ToString("F2");
                            arr[7] = "R " + (theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost).ToString("F2");
                            arr[8] = "R " + ((theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost) * theSaleDetails[j].GetSaleItemQuantitySoldAt(k)).ToString("F2");
                            arr[9] = "R " + (fCost * iQtyU).ToString("F2");
                            fTotalSalesProfit += (theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost) * theSaleDetails[j].GetSaleItemQuantitySoldAt(k);
                            fTotalUsageLoss += fCost * iQtyU;
                            theListView.Items.Add(new ListViewItem(arr));
                        }
                    }
                }
                arr[0] = "";
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = "";
                arr[5] = "";
                arr[6] = "";
                arr[7] = "";
                arr[8] = "Total sales profit";
                arr[9] = "R " + fTotalSalesProfit.ToString("F2");
                theListView.Items.Add(new ListViewItem(arr));
                arr[0] = "";
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = "";
                arr[5] = "";
                arr[6] = "";
                arr[7] = "";
                arr[8] = "Total usage loss";
                arr[9] = "R " + fTotalUsageLoss.ToString("F2");
                theListView.Items.Add(new ListViewItem(arr));
                arr[0] = "";
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = "";
                arr[5] = "";
                arr[6] = "";
                arr[7] = "";
                arr[8] = "Total profit";
                arr[9] = "R " + (fTotalSalesProfit - fTotalUsageLoss).ToString("F2");
                theListView.Items.Add(new ListViewItem(arr));
                SetupPrinterAndPrintList(theListView, "\tVoel Paradys profit report " + sFromPrintDate + " to " + sToPrintDate, 18);
            }
            else
                PrintErrorMessage("Profit report for " + sFromPrintDate + " to " + sToPrintDate + " does not exist in the database");
        }
        private void ExportProfitReportToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sFromPrintDate = FromDateTimePicker.Value.ToString("dd/MM/yyyy");
            string sToPrintDate = ToDateTimePicker.Value.ToString("dd/MM/yyyy");
            List<VoelParadysDataStructures.CStringStringMap> theDailySaleFileData = new List<VoelParadysDataStructures.CStringStringMap>();

            if (rDataController.GetDailySaleFileList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theDailySaleFileData))
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    PrintErrorMessage("Excel is not installed on this machine!!");
                    return;
                }
                float fTotalSalesProfit = 0, fTotalUsageLoss = 0;
                int iRowCounter = 1;

                object misValue = System.Reflection.Missing.Value;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                xlWorksheet.Cells.ColumnWidth = 20;
                xlWorksheet.Cells[iRowCounter, 3] = "Voel Paradys Profit Report for " + sFromPrintDate + " to " + sToPrintDate;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
                iRowCounter = 3;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = "Date";
                xlWorksheet.Cells[iRowCounter, 2] = "Item Code";
                xlWorksheet.Cells[iRowCounter, 3] = "Description";
                xlWorksheet.Cells[iRowCounter, 4] = "Qty Sold";
                xlWorksheet.Cells[iRowCounter, 5] = "Qty Used";
                xlWorksheet.Cells[iRowCounter, 6] = "Cost Price";
                xlWorksheet.Cells[iRowCounter, 7] = "Sell Price";
                xlWorksheet.Cells[iRowCounter, 8] = "Profit Per Item";
                xlWorksheet.Cells[iRowCounter, 9] = "Total Profit";
                xlWorksheet.Cells[iRowCounter, 10] = "Usage Cost Price";

                xlWorksheet.Cells[iRowCounter, 2].EntireColumn.NumberFormat = "@";                                     // Formatted as text

                List<VoelParadysDataStructures.SSaleDetails> theSaleDetails = new List<VoelParadysDataStructures.SSaleDetails>();
                string sName = "", sCurrencyFormat = "R ####0.00_);[Red](R ####0.00)";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                DateTime TheDateFormatter;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
                DateTimeStyles styles = DateTimeStyles.None;

                iRowCounter = 4;
                var theProgressBar = new ProgressBar(1, theDailySaleFileData.Count);
                theProgressBar.Show();
                for (int i = 0; i < theDailySaleFileData.Count; ++i)
                {
                    rDataController.ReadDailySaleDataFromDB(theDailySaleFileData[i].sString2, ref theSaleDetails, false);
                    
                    for (int j = 0; j < theSaleDetails.Count; ++j)
                    {
                        for (int k = 0; k < theSaleDetails[j].GetSaleItemsCount(); ++k)
                        {
                            rDataController.GetStockItemData(theSaleDetails[j].GetSaleItemCodeAt(k), ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                            iQtyU = GetQtyUsedForDate(theDailySaleFileData[i].sString1, theSaleDetails[j].GetSaleItemCodeAt(k));
                            DateTime.TryParse(theDailySaleFileData[i].sString1, culture, styles, out TheDateFormatter);

                            xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            xlWorksheet.Cells[iRowCounter, 1] = TheDateFormatter;
                            xlWorksheet.Cells[iRowCounter, 1].NumberFormat = "dd MMM yyyy";
                            xlWorksheet.Cells[iRowCounter, 2] = theSaleDetails[j].GetSaleItemCodeAt(k);
                            xlWorksheet.Cells[iRowCounter, 3] = sName;
                            xlWorksheet.Cells[iRowCounter, 4] = theSaleDetails[j].GetSaleItemQuantitySoldAt(k);
                            xlWorksheet.Cells[iRowCounter, 5] = iQtyU;
                            xlWorksheet.Cells[iRowCounter, 6] = fCost;
                            xlWorksheet.Cells[iRowCounter, 6].NumberFormat = sCurrencyFormat;
                            xlWorksheet.Cells[iRowCounter, 7] = theSaleDetails[j].GetSaleItemSellPriceAt(k);
                            xlWorksheet.Cells[iRowCounter, 7].NumberFormat = sCurrencyFormat;
                            xlWorksheet.Cells[iRowCounter, 8] = theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost;
                            xlWorksheet.Cells[iRowCounter, 8].NumberFormat = sCurrencyFormat;
                            xlWorksheet.Cells[iRowCounter, 9] = (theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost) * theSaleDetails[j].GetSaleItemQuantitySoldAt(k);
                            xlWorksheet.Cells[iRowCounter, 9].NumberFormat = sCurrencyFormat;
                            xlWorksheet.Cells[iRowCounter, 10] = (fCost * iQtyU);
                            xlWorksheet.Cells[iRowCounter, 10].NumberFormat = sCurrencyFormat;

                            fTotalSalesProfit += (theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost) * theSaleDetails[j].GetSaleItemQuantitySoldAt(k);
                            fTotalUsageLoss += (fCost * iQtyU);
                            iRowCounter += 1;
                        }
                        theProgressBar.UpdateProgressBar();
                    }
                }
                iRowCounter += 1;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 8] = "Total sales profit";
                xlWorksheet.Cells[iRowCounter, 8].Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 10] = fTotalSalesProfit;
                xlWorksheet.Cells[iRowCounter, 10].NumberFormat = sCurrencyFormat;
                iRowCounter += 1;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 8] = "Total usage loss";
                xlWorksheet.Cells[iRowCounter, 8].Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 10] = fTotalUsageLoss;
                xlWorksheet.Cells[iRowCounter, 10].NumberFormat = sCurrencyFormat;
                iRowCounter += 1;
                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 8] = "Total profit";
                xlWorksheet.Cells[iRowCounter, 8].Font.Bold = true;
                xlWorksheet.Cells[iRowCounter, 10] = fTotalSalesProfit - fTotalUsageLoss;
                xlWorksheet.Cells[iRowCounter, 10].NumberFormat = sCurrencyFormat;

                sFromPrintDate = sFromPrintDate.Replace("/", "_");
                sToPrintDate = sToPrintDate.Replace("/", "_");
                string sXlFilePathName = GetRelativePath() + "Profit\\" + sFromPrintDate + "_To_" + sToPrintDate + ".xls";
                theProgressBar.CloseProgressBar();
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
                PrintErrorMessage("Profit report for " + sFromPrintDate + " to " + sToPrintDate + " does not exist in the database");
            }
        }
        //}
        //{ The functions for customer database printing or exporting
        private void PrintCustomerDatabase()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
            theListView.View = View.Details;
            theListView.GridLines = true;
            theListView.FullRowSelect = true;

            theListView.Columns.Add("ID", 50);
            theListView.Columns.Add("Name", 80);
            theListView.Columns.Add("Surname", 90);
            theListView.Columns.Add("Address Line 1", 110);
            theListView.Columns.Add("Address Line 2", 90);
            theListView.Columns.Add("Address Line 3", 90);
            theListView.Columns.Add("Address Line 4", 90);
            theListView.Columns.Add("Address Line 5", 90);
            theListView.Columns.Add("Phone", 90);
            theListView.Columns.Add("Identity Number", 110);

            string[] arr = new string[10];
            string sName = "", sSurname = "", sPhone = "";
            string[] saAddress;
            int iID = -1;
            long lIdNumber = -1;

            for (int i = 0; i < rDataController.GetCustomerListSize(); ++i)
            {
                saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                rDataController.GetCustomerData(i, ref iID, ref sName, ref sSurname, ref saAddress, ref sPhone, ref lIdNumber);
                
                arr[0] = iID.ToString();
                arr[1] = sName == "-1" ? "" : sName;
                arr[2] = sSurname == "-1" ? "" : sSurname;
                arr[3] = saAddress.Length >= 1 ? saAddress[0] == "-1" ? "" : saAddress[0] : "";
                arr[4] = saAddress.Length >= 2 ? saAddress[1] == "-1" ? "" : saAddress[1] : "";
                arr[5] = saAddress.Length >= 3 ? saAddress[2] == "-1" ? "" : saAddress[2] : "";
                arr[6] = saAddress.Length >= 4 ? saAddress[3] == "-1" ? "" : saAddress[3] : "";
                arr[7] = saAddress.Length >= 5 ? saAddress[4] == "-1" ? "" : saAddress[4] : "";
                arr[8] = sPhone == "-1" ? "" : sPhone;
                arr[9] = lIdNumber == -1 ? "" : lIdNumber.ToString();
                theListView.Items.Add(new ListViewItem(arr));
            }
            DateTime theDateAndTime = DateTime.Now;
            SetupPrinterAndPrintList(theListView, "\tVoel Paradys Customers as at " + theDateAndTime.ToString("dd MMM yyyy"));
        }
        private void ExportCustomerDatabaseToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();
            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                PrintErrorMessage("Excel is not installed on this machine!!");
                return;
            }

            int iRowCounter = 1;

            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
            xlWorksheet.Cells.ColumnWidth = 20;
            DateTime theDateAndTime = DateTime.Now;
            xlWorksheet.Cells[iRowCounter, 4] = "Voel Paradys Customers as at " + theDateAndTime.ToString("dd MMM yyyy");
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
            iRowCounter = 3;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorksheet.Cells[iRowCounter, 1] = "ID";
            xlWorksheet.Cells[iRowCounter, 1].ColumnWidth = 5;
            xlWorksheet.Cells[iRowCounter, 2] = "Name";
            xlWorksheet.Cells[iRowCounter, 2].ColumnWidth = 10;
            xlWorksheet.Cells[iRowCounter, 3] = "Surname";
            xlWorksheet.Cells[iRowCounter, 3].ColumnWidth = 12;
            xlWorksheet.Cells[iRowCounter, 4] = "Address Line 1";
            xlWorksheet.Cells[iRowCounter, 5] = "Address Line 2";
            xlWorksheet.Cells[iRowCounter, 6] = "Address Line 3";
            xlWorksheet.Cells[iRowCounter, 7] = "Address Line 4";
            xlWorksheet.Cells[iRowCounter, 8] = "Address Line 5";
            xlWorksheet.Cells[iRowCounter, 9] = "Phone Number";
            xlWorksheet.Cells[iRowCounter, 10] = "ID Number";

           
            xlWorksheet.Cells[iRowCounter, 2].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 3].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 4].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 5].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 6].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 7].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 8].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 9].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 10].EntireColumn.NumberFormat = "@";                                    // Formatted as text

            string sName = "", sSurname = "", sPhone = "";
            string[] saAddress;
            int iID = -1;
            long lIdNumber = -1;

            iRowCounter = 4;
            var theProgressBar = new ProgressBar(1, rDataController.GetInventoryListSize());
            theProgressBar.Show();
            for (int i = 0; i < rDataController.GetCustomerListSize(); ++i)
            {
                saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                rDataController.GetCustomerData(i, ref iID, ref sName, ref sSurname, ref saAddress, ref sPhone, ref lIdNumber);

                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = iID;
                xlWorksheet.Cells[iRowCounter, 2] = sName == "-1" ? "" : sName;
                xlWorksheet.Cells[iRowCounter, 3] = sSurname == "-1" ? "" : sSurname;
                xlWorksheet.Cells[iRowCounter, 4] = saAddress.Length >= 1 ? saAddress[0] == "-1" ? "" : saAddress[0] : "";
                xlWorksheet.Cells[iRowCounter, 5] = saAddress.Length >= 2 ? saAddress[1] == "-1" ? "" : saAddress[1] : "";
                xlWorksheet.Cells[iRowCounter, 6] = saAddress.Length >= 3 ? saAddress[2] == "-1" ? "" : saAddress[2] : "";
                xlWorksheet.Cells[iRowCounter, 7] = saAddress.Length >= 4 ? saAddress[3] == "-1" ? "" : saAddress[3] : "";
                xlWorksheet.Cells[iRowCounter, 8] = saAddress.Length >= 5 ? saAddress[4] == "-1" ? "" : saAddress[4] : "";
                xlWorksheet.Cells[iRowCounter, 9] = sPhone == "-1" ? "" : sPhone;
                xlWorksheet.Cells[iRowCounter, 10] = lIdNumber == -1 ? "" : lIdNumber.ToString();
                iRowCounter += 1;
                theProgressBar.UpdateProgressBar();
            }

            string sFromPrintDate = theDateAndTime.ToString("dd_MM_yyyy");
            string sXlFilePathName = GetRelativePath() + "Customers\\" + sFromPrintDate + ".xls";
            theProgressBar.CloseProgressBar();
            xlWorkbook.SaveAs(sXlFilePathName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkbook.Close(true, misValue, misValue);
            xlApp.Quit();
            ReleaseObject(xlWorksheet);
            ReleaseObject(xlWorkbook);
            ReleaseObject(xlApp);
        }
        //}
        //{ The functions for supplier database printing or exporting
        private void PrintSupplierDatabase()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            System.Windows.Forms.ListView theListView = new System.Windows.Forms.ListView();
            theListView.View = View.Details;
            theListView.GridLines = true;
            theListView.FullRowSelect = true;

            theListView.Columns.Add("ID", 50);
            theListView.Columns.Add("Name", 100);
            theListView.Columns.Add("Rep Name", 80);
            theListView.Columns.Add("Rep Surname", 90);
            theListView.Columns.Add("Address Line 1", 110);
            theListView.Columns.Add("Address Line 2", 90);
            theListView.Columns.Add("Address Line 3", 90);
            theListView.Columns.Add("Address Line 4", 90);
            theListView.Columns.Add("Address Line 5", 90);
            theListView.Columns.Add("Phone", 85);

            string[] arr = new string[10];
            string sName = "", sRepName = "", sRepSurname = "", sPhone = "";
            string[] saAddress;
            int iID = -1;

            for (int i = 0; i < rDataController.GetSupplierListSize(); ++i)
            {
                saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                rDataController.GetSupplierData(i, ref iID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhone);

                arr[0] = iID.ToString();
                arr[1] = sName == "-1" ? "" : sName;
                arr[2] = sRepName == "-1" ? "" : sRepName;
                arr[3] = sRepSurname == "-1" ? "" : sRepSurname;
                arr[4] = saAddress.Length >= 1 ? saAddress[0] == "-1" ? "" : saAddress[0] : "";
                arr[5] = saAddress.Length >= 2 ? saAddress[1] == "-1" ? "" : saAddress[1] : "";
                arr[6] = saAddress.Length >= 3 ? saAddress[2] == "-1" ? "" : saAddress[2] : "";
                arr[7] = saAddress.Length >= 4 ? saAddress[3] == "-1" ? "" : saAddress[3] : "";
                arr[8] = saAddress.Length >= 5 ? saAddress[4] == "-1" ? "" : saAddress[4] : "";
                arr[9] = sPhone == "-1" ? "" : sPhone;
                theListView.Items.Add(new ListViewItem(arr));
            }
            DateTime theDateAndTime = DateTime.Now;
            SetupPrinterAndPrintList(theListView, "\tVoel Paradys Suppliers as at " + theDateAndTime.ToString("dd MMM yyyy"));
        }
        private void ExportSupplierDatabaseToExcel()
        {
            var rDataController = VoelParadysDataController.GetInstance();
            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                PrintErrorMessage("Excel is not installed on this machine!!");
                return;
            }

            int iRowCounter = 1;

            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
            xlWorksheet.Cells.ColumnWidth = 20;
            DateTime theDateAndTime = DateTime.Now;
            xlWorksheet.Cells[iRowCounter, 4] = "Voel Paradys Suppliers as at " + theDateAndTime.ToString("dd MMM yyyy");
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 20;
            iRowCounter = 3;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Size = 15;
            xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorksheet.Cells[iRowCounter, 1] = "ID";
            xlWorksheet.Cells[iRowCounter, 1].ColumnWidth = 5;
            xlWorksheet.Cells[iRowCounter, 2] = "Name";
            xlWorksheet.Cells[iRowCounter, 2].ColumnWidth = 15;
            xlWorksheet.Cells[iRowCounter, 3] = "Rep Name";
            xlWorksheet.Cells[iRowCounter, 3].ColumnWidth = 15;
            xlWorksheet.Cells[iRowCounter, 4] = "Rep Surname";
            xlWorksheet.Cells[iRowCounter, 4].ColumnWidth = 18;
            xlWorksheet.Cells[iRowCounter, 5] = "Address Line 1";
            xlWorksheet.Cells[iRowCounter, 6] = "Address Line 2";
            xlWorksheet.Cells[iRowCounter, 7] = "Address Line 3";
            xlWorksheet.Cells[iRowCounter, 8] = "Address Line 4";
            xlWorksheet.Cells[iRowCounter, 9] = "Address Line 5";
            xlWorksheet.Cells[iRowCounter, 10] = "Phone Number";

            xlWorksheet.Cells[iRowCounter, 2].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 3].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 4].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 5].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 6].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 7].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 8].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 9].EntireColumn.NumberFormat = "@";                                     // Formatted as text
            xlWorksheet.Cells[iRowCounter, 10].EntireColumn.NumberFormat = "@";                                    // Formatted as text

            string sName = "", sRepName = "", sRepSurname = "", sPhone = "";
            string[] saAddress;
            int iID = -1;

            iRowCounter = 4;
            var theProgressBar = new ProgressBar(1, rDataController.GetInventoryListSize());
            theProgressBar.Show();
            for (int i = 0; i < rDataController.GetSupplierListSize(); ++i)
            {
                saAddress = new string[5] { "", "", "", "", "" };
                rDataController.GetSupplierData(i, ref iID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhone);

                xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorksheet.Cells[iRowCounter, 1] = iID;
                xlWorksheet.Cells[iRowCounter, 2] = sName == "-1" ? "" : sName;
                xlWorksheet.Cells[iRowCounter, 3] = sRepName == "-1" ? "" : sRepName;
                xlWorksheet.Cells[iRowCounter, 4] = sRepSurname == "-1" ? "" : sRepSurname;
                xlWorksheet.Cells[iRowCounter, 5] = saAddress.Length >= 1 ? saAddress[0] == "-1" ? "" : saAddress[0] : "";
                xlWorksheet.Cells[iRowCounter, 6] = saAddress.Length >= 2 ? saAddress[1] == "-1" ? "" : saAddress[1] : "";
                xlWorksheet.Cells[iRowCounter, 7] = saAddress.Length >= 3 ? saAddress[2] == "-1" ? "" : saAddress[2] : "";
                xlWorksheet.Cells[iRowCounter, 8] = saAddress.Length >= 4 ? saAddress[3] == "-1" ? "" : saAddress[3] : "";
                xlWorksheet.Cells[iRowCounter, 9] = saAddress.Length >= 5 ? saAddress[4] == "-1" ? "" : saAddress[4] : "";
                xlWorksheet.Cells[iRowCounter, 10] = sPhone == "-1" ? "" : sPhone;
                iRowCounter += 1;
                theProgressBar.UpdateProgressBar();
            }

            string sFromPrintDate = theDateAndTime.ToString("dd_MM_yyyy");
            string sXlFilePathName = GetRelativePath() + "Suppliers\\" + sFromPrintDate + ".xls";
            theProgressBar.CloseProgressBar();
            xlWorkbook.SaveAs(sXlFilePathName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkbook.Close(true, misValue, misValue);
            xlApp.Quit();
            ReleaseObject(xlWorksheet);
            ReleaseObject(xlWorkbook);
            ReleaseObject(xlApp);
        }
        //}

        private void SetupPrinterAndPrintList(System.Windows.Forms.ListView theListViewToPrint, string sHeaderText, int iHeaderSize = 20)
        {
            // Setup the form for printing
            // Create the ListViewPrinter, and assign the list to it
            ListViewPrinter lListPrinter = new ListViewPrinter();
            lListPrinter.ListView = theListViewToPrint;
            lListPrinter.DefaultPageSettings.Margins = new Margins(45, 45, 45, 45);
            // Give the page a heading and set the ShrinkToFit true
            lListPrinter.Header = sHeaderText;
            lListPrinter.IsShrinkToFit = true;
            // Remove the standard gradient effects on the header background, and draw a line at the bottom of the header
            lListPrinter.HeaderFormat = BlockFormat.Header();
            lListPrinter.HeaderFormat.Font = new System.Drawing.Font(lListPrinter.HeaderFormat.Font.FontFamily, iHeaderSize);
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

            lListPrinter.PrintPreview();
            // TODO: Once the product is complete, uncomment this to actually print the page, and comment out the PrintPreview to print directly without first viewing it
            // lListPrinter.PrintWithDialog();
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
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private string GetRelativePath()
        {
            string sCurrentDir = Environment.CurrentDirectory;
            DirectoryInfo theDirectory = new DirectoryInfo(sCurrentDir);
            string sFullDirectory = theDirectory.FullName.ToString();
            sFullDirectory = sFullDirectory.Replace("\\Release", "\\Data\\Exports\\");
            sFullDirectory = sFullDirectory.Replace("\\Debug", "\\Data\\Exports\\");
            return sFullDirectory;
        }
        private void PrintErrorMessage(string sMessage)
        {
            string sCaption = "Error!!!";
            MessageBox.Show(sMessage, sCaption);
        }
    }
}
