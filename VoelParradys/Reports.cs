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

        //{ The functions for daily cash-up reports
        private void PrintCashUp()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            string sSelectedDateFile = FromDateTimePicker.Value.ToString("dd_MM_yy");
            string sPrintDate = FromDateTimePicker.Value.ToString("dd MMM yyyy");

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

                theListView.Columns.Add("Item Code", 110);
                theListView.Columns.Add("Qty", 110);
                theListView.Columns.Add("Sell Price", 110);
                theListView.Columns.Add("Total", 110);
                theListView.Columns.Add("Cash Received", 160);
                theListView.Columns.Add("Payment Type", 160);

                string[] arr = new string[6];

                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    ListViewGroup SaleGroup = new ListViewGroup(theSaleDetailsList[i].GetSaleTime());
                    theListView.Groups.Add(SaleGroup);
                    for (int k = 0; k < theSaleDetailsList[i].GetSaleItemsCount(); ++k)
                    {
                        arr[0] = theSaleDetailsList[i].GetSaleItemCodeAt(k);
                        arr[1] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k).ToString();
                        arr[2] = "R " + theSaleDetailsList[i].GetSaleItemSellPriceAt(k).ToString("F2");
                        arr[3] = "R " + (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(k) * theSaleDetailsList[i].GetSaleItemSellPriceAt(k)).ToString("F2");
                        arr[4] = "";
                        arr[5] = "";
                        theListView.Items.Add(new ListViewItem(arr, SaleGroup));
                    }
                    arr[0] = "";
                    arr[1] = "";
                    arr[2] = "";
                    arr[3] = "R " + theSaleDetailsList[i].GetSaleTotal().ToString("F2");
                    arr[4] = "R " + theSaleDetailsList[i].GetSaleCashReceived().ToString("F2");
                    arr[5] = theSaleDetailsList[i].GetSalePaymentType();
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

            if (rDataController.DoesFileExist(sSelectedDateFile, "Daily"))
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                    PrintErrorMessage("Excel is not installed on this machine!!");

                List<VoelParadysXmlParser.SSaleDetails> theSaleDetailsList = new List<VoelParadysXmlParser.SSaleDetails>();
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
                xlWorksheet.Cells[iRowCounter, 2] = "Qty";
                xlWorksheet.Cells[iRowCounter, 3] = "Sell Price";
                xlWorksheet.Cells[iRowCounter, 4] = "Total";
                xlWorksheet.Cells[iRowCounter, 5] = "Cash Received";
                xlWorksheet.Cells[iRowCounter, 6] = "Payment Type";

                iRowCounter = 4;
                string sCurrencyFormat = "R 0.00_);[Red](R 0.00)";
                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleTime();
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.Font.Bold = true;
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    iRowCounter += 1;
                    for (int j = 0; j < theSaleDetailsList[i].GetSaleItemsCount(); ++j)
                    {
                        xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        xlWorksheet.Cells[iRowCounter, 1] = theSaleDetailsList[i].GetSaleItemCodeAt(j);
                        xlWorksheet.Cells[iRowCounter, 2] = theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j);
                        xlWorksheet.Cells[iRowCounter, 3] = theSaleDetailsList[i].GetSaleItemSellPriceAt(j);
                        xlWorksheet.Cells[iRowCounter, 3].NumberFormat = sCurrencyFormat;
                        xlWorksheet.Cells[iRowCounter, 4] = (theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j) * theSaleDetailsList[i].GetSaleItemSellPriceAt(j));
                        xlWorksheet.Cells[iRowCounter, 4].NumberFormat = sCurrencyFormat;
                        iRowCounter += 1;
                    }
                    xlWorksheet.Cells[iRowCounter, 1].EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xlWorksheet.Cells[iRowCounter, 4] = theSaleDetailsList[i].GetSaleTotal();
                    xlWorksheet.Cells[iRowCounter, 4].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 5] = theSaleDetailsList[i].GetSaleCashReceived();
                    xlWorksheet.Cells[iRowCounter, 5].NumberFormat = sCurrencyFormat;
                    xlWorksheet.Cells[iRowCounter, 6] = theSaleDetailsList[i].GetSalePaymentType();
                    iRowCounter += 1;
                }
                
               
                string sXlFilePathName = GetRelativePath() + "CashUp\\" + sSelectedDateFile + ".xls";
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
            List<UsageData> theUsageData = new List<UsageData>();

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
            List<UsageData> theUsageData = new List<UsageData>();

            if (rDataController.GetUsageDataList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theUsageData))
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                    PrintErrorMessage("Excel is not installed on this machine!!");

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

                string sName = "", sCurrencyFormat = "R 0.00_);[Red](R 0.00)";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                DateTime TheDateFormatter;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
                DateTimeStyles styles = DateTimeStyles.None;

                iRowCounter = 4;
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
                }

                sFromPrintDate = sFromPrintDate.Replace("/", "_");
                sToPrintDate = sToPrintDate.Replace("/", "_");
                string sXlFilePathName = GetRelativePath() + "Usage\\" + sFromPrintDate + "_To_" + sToPrintDate + ".xls";
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
                PrintErrorMessage("Excel is not installed on this machine!!");

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

            string sCode = "", sName = "";
            int iQtyS = -1, iQtyB = -1, iQtyU = -1;
            float fCost = -1, fSell = -1;

            iRowCounter = 4;
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
            }

            string sFromPrintDate = theDateAndTime.ToString("dd_MM_yyyy");
            string sXlFilePathName = GetRelativePath() + "Inventory\\" + sFromPrintDate + ".xls";
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
            List<UsageData> theUsageList = new List<UsageData>();
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
            List<DailySaleFileData> theDailySaleFileData = new List<DailySaleFileData>();

            if (rDataController.GetDailySaleFileList(FromDateTimePicker.Value.ToString("dd/MM/yyyy"), ToDateTimePicker.Value.ToString("dd/MM/yyyy"), ref theDailySaleFileData))
            {
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
                List<VoelParadysXmlParser.SSaleDetails> theSaleDetails = new List<VoelParadysXmlParser.SSaleDetails>();
                string sName = "";
                int iQtyS = -1, iQtyB = -1, iQtyU = -1;
                float fCost = -1, fSell = -1;

                for (int i = 0; i < theDailySaleFileData.Count; ++i)
                {
                    rDataController.ReadDailySaleDataFromDB(theDailySaleFileData[i].sFileName, ref theSaleDetails, false);

                    for (int j = 0; j < theSaleDetails.Count;  ++j)
                    {
                        for (int k = 0; k < theSaleDetails[j].GetSaleItemsCount(); ++k)
                        {
                            rDataController.GetStockItemData(theSaleDetails[j].GetSaleItemCodeAt(k), ref sName, ref iQtyS, ref iQtyB, ref iQtyU, ref fCost, ref fSell);
                            iQtyU = GetQtyUsedForDate(theDailySaleFileData[i].sDate, theSaleDetails[j].GetSaleItemCodeAt(k));

                            arr[0] = theDailySaleFileData[i].sDate;
                            arr[1] = theSaleDetails[j].GetSaleItemCodeAt(k);
                            arr[2] = sName;
                            arr[3] = theSaleDetails[j].GetSaleItemQuantitySoldAt(k).ToString();
                            arr[4] = iQtyU.ToString();
                            arr[5] = "R " + fCost.ToString("F2");
                            arr[6] = "R " + theSaleDetails[j].GetSaleItemSellPriceAt(k).ToString("F2");
                            arr[7] = "R " + (theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost).ToString("F2");
                            arr[8] = "R " + ((theSaleDetails[j].GetSaleItemSellPriceAt(k) - fCost) * theSaleDetails[j].GetSaleItemQuantitySoldAt(k)).ToString("F2");
                            arr[9] = "R " + (fCost * iQtyU).ToString("F2");
                            theListView.Items.Add(new ListViewItem(arr));
                        }
                    }
                }
                SetupPrinterAndPrintList(theListView, "\tVoel Paradys profit report " + sFromPrintDate + " to " + sToPrintDate, 18);
            }
            else
                PrintErrorMessage("Profit report for " + sFromPrintDate + " to " + sToPrintDate + " does not exist in the database");
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
            return sFullDirectory;
        }
        private void PrintErrorMessage(string sMessage)
        {
            string sCaption = "Error!!!";
            MessageBox.Show(sMessage, sCaption);
        }
    }
}
