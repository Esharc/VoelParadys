using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;

namespace VoelParadys
{
    class VoelParadysXmlParser
    {
        public struct SSaleDetails
        {
            List<SItemDetails> lsItemDetailList;   // A list of items sold
            float fCashReceived;                  // The cash received
            float fSaleTotal;                     // The total of the sale
            string sTypeOfPayment;                // Normal cash, EFT or bank transfer
            string sTime;                         // The time when the transaction took place

            public SSaleDetails(float _fCashReceived, float _fSaleTotal, string _sTypeOfPayment, string _sTime)
            {
                lsItemDetailList = new List<SItemDetails>();
                fCashReceived = _fCashReceived;
                fSaleTotal = _fSaleTotal;
                sTypeOfPayment = _sTypeOfPayment;
                sTime = _sTime;
            }

            // Getters
            public string GetSaleItemCodeAt(int iIndex) { return lsItemDetailList[iIndex].GetItemCode(); }
            public int GetSaleItemQuantitySoldAt(int iIndex) { return lsItemDetailList[iIndex].GetItemQuantitySold(); }
            public float GetSaleCashReceived() { return fCashReceived; }
            public float GetSaleTotal() { return fSaleTotal; }
            public string GetSalePaymentType() { return sTypeOfPayment; }
            public string GetSaleTime() { return sTime; }
            public int GetSaleItemsCount() { return lsItemDetailList.Count; }
            // Setters
            public void SetSaleItemDetail(string sCode, int iQuantity) { SItemDetails SItem = new SItemDetails(sCode, iQuantity); lsItemDetailList.Add(SItem); }
            public void SetSaleCashReceived(float fCash) { fCashReceived = fCash; }
            public void SetSaleTotal(float fTotal) { fSaleTotal = fTotal; }
            public void SetSalePaymentType(string sType) { sTypeOfPayment = sType; }
            public void SetSaleTime(string sTheTime) { sTime = sTheTime; }
        }

        // Create the xml file if it does not exist
        public void CreateXmlFileIfNotFound(string sXmlFileName)
        {
            bool bFileExists = File.Exists(sXmlFileName);
            if (!bFileExists)
            {
                XmlWriterSettings XmlSettings = new XmlWriterSettings();
                XmlSettings.Indent = true;

                XmlWriter TheWriter = XmlWriter.Create(sXmlFileName, XmlSettings);

                TheWriter.Flush();
                TheWriter.Close();
            }
        }
        // Determine if the xml file is valid
        private bool IsXmlFileValid(string sXmlFileName)
        {
            XmlReader TheReader = XmlReader.Create(sXmlFileName);

            if (TheReader == null)
            {
                TheReader.Close();
                return false;
            }

            try
            {
                TheReader.Read();
            }
            catch (XmlException exception)
            {
                MessageBox.Show(sXmlFileName + " is empty. New data must be added");
                TheReader.Close();
                return false;
            }
            TheReader.Close();
            return true;
        }
        // Write to the stock xml file
        public void VoelParadysStockXmlWriter(List<SStockItemDetails> lStockItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            if (lStockItems.Count > 0)
            {
                XmlWriterSettings XmlSettings = new XmlWriterSettings();
                XmlSettings.Indent = true;

                XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysInventory.xml", XmlSettings);
                TheWriter.WriteStartDocument();
                TheWriter.WriteStartElement("Inventory");
                for (int i = 0; i < lStockItems.Count; ++i)
                {
                    string sName = lStockItems[i].GetStockItemName().Replace(" ", "");
                    sName = sName.Replace("'", "");
                    TheWriter.WriteComment("This is the start of " + lStockItems[i].GetStockItemName());
                    TheWriter.WriteStartElement(sName);
                    TheWriter.WriteElementString("Code", lStockItems[i].GetStockItemCode());
                    TheWriter.WriteElementString("Name", lStockItems[i].GetStockItemName());
                    TheWriter.WriteElementString("QuantitySold", lStockItems[i].GetStockItemQuantitySold().ToString());
                    TheWriter.WriteElementString("QuantityBought", lStockItems[i].GetStockItemQuantityBought().ToString());
                    TheWriter.WriteElementString("QuantityUsed", lStockItems[i].GetStockItemQuantityUsed().ToString());
                    TheWriter.WriteElementString("CostPrice", lStockItems[i].GetStockItemCostPrice().ToString("F2"));
                    TheWriter.WriteElementString("SellPrice", lStockItems[i].GetStockItemSellPrice().ToString("F2"));
                    TheWriter.WriteEndElement();
                }
                TheWriter.WriteEndElement();
                TheWriter.WriteEndDocument();
                TheWriter.Flush();
                TheWriter.Close();
            }
        }
        // Read from the stock xml file
        public void VoelParadysStockXmlReader(List<SStockItemDetails> lStockItems)
        {
            if (IsXmlFileValid("../Data/VoelParadysInventory.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysInventory.xml");

                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        SStockItemDetails TempStockItem = new SStockItemDetails();
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "Code")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemCode(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Name")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemName(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "QuantitySold")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemQuantitySold(int.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "QuantityBought")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemQuantityBought(int.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "QuantityUsed")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemQuantityUsed(int.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "CostPrice")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemCostPrice(float.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "SellPrice")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempStockItem.SetStockItemSellPrice(float.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                        }
                        lStockItems.Add(TempStockItem);
                    }
                }
                TheReader.Close();
            }
        }
        // Write to the sale file
        public void VoelParadysDailySaleDataXmlWriter(List<SSaleInvoiceData> lSaleItems, float fSaleTotal, float fCashReceived, string sTypeOfCash)
        {
            DateTime theDateAndTime = DateTime.Now;
            string theDate = theDateAndTime.ToString("dd_MM_yy");
            string theMonthDate = theDateAndTime.ToString("MM_yyyy");
            string theTime = theDateAndTime.ToString("H:mm:ss");
            string sDailySaleDataFile = GetOrCreateSaleInfoFile(theDate, "Daily");
            List<SSaleDetails> theSaleDetailsList = new List<SSaleDetails>();
            SSaleDetails theNewSaleData = new SSaleDetails(0, 0, "Cash", "00:00:00");

            // First we populate the list with all the old sale data before adding the new sale data
            VoelParadysDailySaleDataXmlReader(sDailySaleDataFile, ref theSaleDetailsList);

            // Then add the new sale data to the SaleData type
            for (int i = 0; i < lSaleItems.Count; ++i)
            {
                theNewSaleData.SetSaleItemDetail(lSaleItems[i].GetItemCode(), lSaleItems[i].GetItemQuantity());
            }
            theNewSaleData.SetSaleTime(theTime);
            theNewSaleData.SetSaleTotal(fSaleTotal);
            theNewSaleData.SetSaleCashReceived(fCashReceived);
            theNewSaleData.SetSalePaymentType(sTypeOfCash);
            // Now add the new sale data to the Sale data array
            theSaleDetailsList.Add(theNewSaleData);
            VoelParadysMonthlySaleDataXmlWriter(theMonthDate, sDailySaleDataFile);

            // Now save it all to the file
            if (theSaleDetailsList.Count > 0)
            {
                XmlWriterSettings XmlSettings = new XmlWriterSettings();
                XmlSettings.Indent = true;

                XmlWriter TheWriter = XmlWriter.Create(sDailySaleDataFile, XmlSettings);
                TheWriter.WriteStartDocument();
                TheWriter.WriteStartElement("DailySaleData");
                for (int i = 0; i < theSaleDetailsList.Count; ++i)
                {
                    TheWriter.WriteComment("This is the start of sale item with time stamp " + theSaleDetailsList[i].GetSaleTime());
                    TheWriter.WriteStartElement("TimeStamp" + theSaleDetailsList[i].GetSaleTime().Replace(":", ""));
                    TheWriter.WriteElementString("Time", theSaleDetailsList[i].GetSaleTime());
                    for (int j = 0; j < theSaleDetailsList[i].GetSaleItemsCount(); ++j)
                    {
                        TheWriter.WriteElementString("ItemCode", theSaleDetailsList[i].GetSaleItemCodeAt(j).ToString());
                        TheWriter.WriteElementString("QuantitySold", theSaleDetailsList[i].GetSaleItemQuantitySoldAt(j).ToString());
                    }
                    TheWriter.WriteElementString("SaleTotal", theSaleDetailsList[i].GetSaleTotal().ToString("F2"));
                    TheWriter.WriteElementString("CashReceived", theSaleDetailsList[i].GetSaleCashReceived().ToString("F2"));
                    TheWriter.WriteElementString("TypeOfPayment", theSaleDetailsList[i].GetSalePaymentType());
                    TheWriter.WriteEndElement();
                }
                TheWriter.WriteEndElement();
                TheWriter.WriteEndDocument();
                TheWriter.Flush();
                TheWriter.Close();
            }
            
        }
        // Reader from the sale file 
        public void VoelParadysDailySaleDataXmlReader(string sSaleFileName, ref List<SSaleDetails> theSaleDataList)
        {
            if (IsXmlFileValid(sSaleFileName))
            {
                XmlReader TheReader = XmlReader.Create(sSaleFileName);
                string sTempCode = "";
                int iTempQuantity = -1;
                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        SSaleDetails TempSaleDetail = new SSaleDetails(0, 0, "Cash", "00:00:00");
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "Time")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSaleDetail.SetSaleTime(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "ItemCode")
                            {
                               while (TheReader.NodeType != XmlNodeType.EndElement)
                               {
                                   TheReader.Read();
                                   if (TheReader.NodeType == XmlNodeType.Text)
                                   {
                                       sTempCode = "";
                                       sTempCode = TheReader.Value;
                                   }
                               }
                               TheReader.Read();
                            }
                            if (TheReader.Name == "QuantitySold")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                   TheReader.Read();
                                   if (TheReader.NodeType == XmlNodeType.Text)
                                   {
                                       iTempQuantity = -1;
                                       iTempQuantity = int.Parse(TheReader.Value);
                                   }
                                }
                                TempSaleDetail.SetSaleItemDetail(sTempCode, iTempQuantity);
                                TheReader.Read();
                            }
                            if (TheReader.Name == "SaleTotal")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSaleDetail.SetSaleTotal(float.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "CashReceived")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSaleDetail.SetSaleCashReceived(float.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "TypeOfPayment")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSaleDetail.SetSalePaymentType(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                        }
                        theSaleDataList.Add(TempSaleDetail);
                    }
                }
                TheReader.Close();
            }
        }
        // Write the daily sale file info to the monthly sale file. This will just include the filenames for the daily sales so that we have a file to iterate
        // through later when we review sales.
        public void VoelParadysMonthlySaleDataXmlWriter(string sTheDateMonthFormat, string sTheSaleFile)
        {
            string sMonthlyDateFile = GetOrCreateSaleInfoFile(sTheDateMonthFormat, "Monthly");
            List<string> lMonthlySaleList = new List<string>();
            VoelParadysMonthlySaleDataXmlReader(sMonthlyDateFile, ref lMonthlySaleList);

            if (!IsStringInList(sTheSaleFile, lMonthlySaleList))
            {
                lMonthlySaleList.Add(sTheSaleFile);
                if (lMonthlySaleList.Count > 0)
                {
                    XmlWriterSettings XmlSettings = new XmlWriterSettings();
                    XmlSettings.Indent = true;

                    XmlWriter TheWriter = XmlWriter.Create(sMonthlyDateFile, XmlSettings);
                    TheWriter.WriteStartDocument();
                    TheWriter.WriteStartElement("MonthlySaleData");
                    TheWriter.WriteComment("This is the start of a list for all the sale data for the month " + sTheDateMonthFormat);
                    for (int j = 0; j < lMonthlySaleList.Count; ++j)
                        TheWriter.WriteElementString("FileName", lMonthlySaleList[j]);
                    TheWriter.WriteEndElement();
                    TheWriter.WriteEndDocument();
                    TheWriter.Flush();
                    TheWriter.Close();
                }
            }
        }
        // Read the daily sale file info from the monthly sale file.
        public void VoelParadysMonthlySaleDataXmlReader(string sTheMonthlySaleFile, ref List<string> lTheMonthlySaleList)
        {
            if (IsXmlFileValid(sTheMonthlySaleFile))
            {
                XmlReader TheReader = XmlReader.Create(sTheMonthlySaleFile);
                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "FileName")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        lTheMonthlySaleList.Add(TheReader.Value);
                                }
                                TheReader.Read();
                            }
           
                        }
                    }
                }
                TheReader.Close();
            }
        }
        // Create the sale info file if it does not already exist, otherwise just return it
        private string GetOrCreateSaleInfoFile(string sTheDate, string sTheSaleData)
        {
            string sXmlFileName = "../Data/SaleData/" + sTheSaleData + "/" + sTheDate + ".xml";
            if (!File.Exists(sXmlFileName))
            {
                FileStream myFileStream = new FileStream(sXmlFileName, FileMode.Create);
                myFileStream.Flush();
                myFileStream.Close();
            }
            return sXmlFileName;
        }
        // Determine if the daily sale file exists in the monthly sale file list
        private bool IsStringInList(string sTheString, List<string> theStringList)
        {
            for (int i = 0; i < theStringList.Count; ++i)
            {
                if (theStringList[i] == sTheString)
                    return true;
            }
            return false;
        }

        // Write to the customer xml file
        public void VoelParadysCustomerXmlWriter(List<CCustomerDetails> lCustomerItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            if (lCustomerItems.Count > 0)
            {
                XmlWriterSettings XmlSettings = new XmlWriterSettings();
                XmlSettings.Indent = true;

                XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysCustomers.xml", XmlSettings);
                TheWriter.WriteStartDocument();
                TheWriter.WriteStartElement("Customers");
                for (int i = 0; i < lCustomerItems.Count; ++i)
                {
                    string sName = lCustomerItems[i].GetCustomerName().Replace(" ", "");
                    sName = sName.Replace("'", "");
                    TheWriter.WriteComment("This is the start of " + lCustomerItems[i].GetCustomerName());
                    TheWriter.WriteStartElement(sName);
                    TheWriter.WriteElementString("ID", lCustomerItems[i].GetCustomerID().ToString());
                    TheWriter.WriteElementString("Name", lCustomerItems[i].GetCustomerName());
                    TheWriter.WriteElementString("Surname", lCustomerItems[i].GetCustomerSurname());
                    TheWriter.WriteElementString("Address", lCustomerItems[i].GetCustomerAddress());
                    TheWriter.WriteElementString("Phone", lCustomerItems[i].GetCustomerPhone());
                    TheWriter.WriteElementString("IdNumber", lCustomerItems[i].GetCustomerIdNumber().ToString());
                    for (int j = 0; j < lCustomerItems[i].GetCustomerWishListCount(); ++j)
                    {
                        TheWriter.WriteElementString("WishListItem", lCustomerItems[i].GetCustomerWishListItemAt(j));
                    }
                    TheWriter.WriteEndElement();
                }
                TheWriter.WriteEndElement();
                TheWriter.WriteEndDocument();
                TheWriter.Flush();
                TheWriter.Close();
            }
        }
        // Read from the customer xml file
        public void VoelParadysCustomerXmlReader(List<CCustomerDetails> lCustomerItems)
        {
            if (IsXmlFileValid("../Data/VoelParadysCustomers.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysCustomers.xml");

                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        CCustomerDetails TempCustomer = new CCustomerDetails(-1, "", "", "", "", -1);
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "ID")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerID(int.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Name")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerName(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Surname")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerSurname(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Address")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerAddress(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Phone")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerPhoneNumber(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "IdNumber")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.SetCustomerIdNumber(long.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "WishListItem")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempCustomer.AddCustomerWishListItem(TheReader.Value); 
                                }
                                TheReader.Read();
                            }
                        }
                        lCustomerItems.Add(TempCustomer);
                    }
                }
                TheReader.Close();
            }
        }

        // Write to the supplier xml file
        public void VoelParadysSupplierXmlWriter(List<CSupplierDetails> lSupplierItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            if (lSupplierItems.Count > 0)
            {
                XmlWriterSettings XmlSettings = new XmlWriterSettings();
                XmlSettings.Indent = true;

                XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysSuppliers.xml", XmlSettings);
                TheWriter.WriteStartDocument();
                TheWriter.WriteStartElement("Suppliers");
                for (int i = 0; i < lSupplierItems.Count; ++i)
                {
                    string sName = lSupplierItems[i].GetSupplierName();
                    sName = sName.Replace(" ", "");
                    sName = sName.Replace("'", "");
                    TheWriter.WriteComment("This is the start of " + lSupplierItems[i].GetSupplierName());
                    TheWriter.WriteStartElement(sName);
                    TheWriter.WriteElementString("ID", lSupplierItems[i].GetSupplierID().ToString());
                    TheWriter.WriteElementString("Name", lSupplierItems[i].GetSupplierName());
                    TheWriter.WriteElementString("RepName", lSupplierItems[i].GetRepName());
                    TheWriter.WriteElementString("RepSurname", lSupplierItems[i].GetRepSurname());
                    TheWriter.WriteElementString("Address", lSupplierItems[i].GetSupplierAddress());
                    TheWriter.WriteElementString("Phone", lSupplierItems[i].GetSupplierPhone());

                    for (int j = 0; j < lSupplierItems[i].GetSuppliedItemsListCount(); ++j)
                    {
                        string sCode = "";
                        int iQuantity = -1;
                        lSupplierItems[i].GetSuppliedItemsListItemAt(j, ref sCode, ref iQuantity);
                        TheWriter.WriteElementString("SuppliedItemCode", sCode);
                        TheWriter.WriteElementString("SuppliedItemQuantity", iQuantity.ToString());
                    }
                    TheWriter.WriteEndElement();
                }
                TheWriter.WriteEndElement();
                TheWriter.WriteEndDocument();
                TheWriter.Flush();
                TheWriter.Close();
            }
        }
        // Read from the customer xml file
        public void VoelParadysSupplierXmlReader(List<CSupplierDetails> lSupplierItems)
        {
            if (IsXmlFileValid("../Data/VoelParadysSuppliers.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysSuppliers.xml");
                string sTempCode = "";
                int iTempQuantity = -1;
                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        CSupplierDetails TempSupplier = new CSupplierDetails(-1, "", "", "", "", "-1");
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "ID")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetSupplierID(int.Parse(TheReader.Value));
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Name")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetSupplierName(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "RepName")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetRepName(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "RepSurname")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetRepSurname(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Address")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetSupplierAddress(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Phone")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempSupplier.SetSupplierPhoneNumber(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "SuppliedItemCode")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                    {
                                        sTempCode = "";
                                        sTempCode = TheReader.Value;
                                    }
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "SuppliedItemQuantity")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                    {
                                        iTempQuantity = -1;
                                        iTempQuantity = int.Parse(TheReader.Value);
                                    }
                                }
                                TempSupplier.AddSuppliedItemsListItem(sTempCode, iTempQuantity);
                                TheReader.Read();
                            }
                        }
                        lSupplierItems.Add(TempSupplier);
                    }
                }
                TheReader.Close();
            }
        }
    }
}
