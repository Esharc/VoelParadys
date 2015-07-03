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
                // MessageBox.Show(sXmlFileName + " is empty. New data must be added");
                TheReader.Close();
                return false;
            }
            TheReader.Close();
            return true;
        }
        public bool DoesFileExist(string sFileName, bool bSaleData)
        {
            string sFilePath = "";
            if (bSaleData)
                sFilePath = "../Data/SaleData/" + sFileName + ".xml";
            else
                sFilePath = "../Data/" + sFileName + ".xml";

            return File.Exists(sFilePath);
        }
        // Write to the stock xml file
        public void VoelParadysStockXmlWriter(List<VoelParadysDataStructures.SStockItemDetails> lStockItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            XmlWriterSettings XmlSettings = new XmlWriterSettings();
            XmlSettings.Indent = true;

            XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysInventory.xml", XmlSettings);

            if (lStockItems.Count > 0)
            {
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
            }

            TheWriter.Flush();
            TheWriter.Close();
        }
        // Read from the stock xml file
        public void VoelParadysStockXmlReader(List<VoelParadysDataStructures.SStockItemDetails> lStockItems)
        {
            if (IsXmlFileValid("../Data/VoelParadysInventory.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysInventory.xml");

                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        VoelParadysDataStructures.SStockItemDetails TempStockItem = new VoelParadysDataStructures.SStockItemDetails();
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
        public void VoelParadysDailySaleDataXmlWriter(List<VoelParadysDataStructures.SSaleInvoiceData> lSaleItems, float fSaleTotal, float fCashReceived, string sTypeOfCash)
        {
            DateTime theDateAndTime = DateTime.Now;
            string theDate = theDateAndTime.ToString("dd_MM_yy");
            string theTime = theDateAndTime.ToString("H:mm:ss");
            string sDailySaleDataFile = GetOrCreateSaleInfoFile(theDate);
            List<VoelParadysDataStructures.SSaleDetails> theSaleDetailsList = new List<VoelParadysDataStructures.SSaleDetails>();
            VoelParadysDataStructures.SSaleDetails theNewSaleData = new VoelParadysDataStructures.SSaleDetails(0, 0, "Cash", "00:00:00");

            // First we populate the list with all the old sale data before adding the new sale data
            VoelParadysDailySaleDataXmlReader(sDailySaleDataFile, ref theSaleDetailsList);

            // Then add the new sale data to the SaleData type
            for (int i = 0; i < lSaleItems.Count; ++i)
            {
                theNewSaleData.SetSaleItemDetail(lSaleItems[i].GetItemCode(), lSaleItems[i].GetItemQuantity(), lSaleItems[i].GetItemSellPrice());
            }
            theNewSaleData.SetSaleTime(theTime);
            theNewSaleData.SetSaleTotal(fSaleTotal);
            theNewSaleData.SetSaleCashReceived(fCashReceived);
            theNewSaleData.SetSalePaymentType(sTypeOfCash);
            // Now add the new sale data to the Sale data array
            theSaleDetailsList.Add(theNewSaleData);

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
                        TheWriter.WriteElementString("SellPrice", theSaleDetailsList[i].GetSaleItemSellPriceAt(j).ToString());
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
        public void VoelParadysDailySaleDataXmlReader(string sSaleFileName, ref List<VoelParadysDataStructures.SSaleDetails> theSaleDataList, bool bFromCashup = false)
        {
            string sFilePath = "";
            if (bFromCashup)
                sFilePath = "../Data/SaleData/" + sSaleFileName + ".xml";
            else
                sFilePath = sSaleFileName;

            if (IsXmlFileValid(sFilePath))
            {
                XmlReader TheReader = XmlReader.Create(sFilePath);
                string sTempCode = "";
                int iTempQuantity = -1;
                float fTempSellPrice = -1;
                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        VoelParadysDataStructures.SSaleDetails TempSaleDetail = new VoelParadysDataStructures.SSaleDetails(0, 0, "Cash", "00:00:00");
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
                                TheReader.Read();
                            }
                            if (TheReader.Name == "SellPrice")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                    {
                                        fTempSellPrice = -1;
                                        fTempSellPrice = float.Parse(TheReader.Value);
                                    }
                                }
                                TempSaleDetail.SetSaleItemDetail(sTempCode, iTempQuantity, fTempSellPrice);
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
        // Create the sale info file if it does not already exist, otherwise just return it
        private string GetOrCreateSaleInfoFile(string sTheDate)
        {
            string sXmlFileName = "../Data/SaleData/" + sTheDate + ".xml";
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
        // append the string array into a single string
        public string AppendAddressArray(string[] saAddress)
        {
            string sRetVal = "";
            for (int i = 0; i < saAddress.Length; ++i)
            {
                if (i == saAddress.Length - 1)
                    sRetVal += saAddress[i] == "" ? "-1" : saAddress[i];
                else
                    sRetVal += saAddress[i] == "" ? "-1" + ";" : saAddress[i] + ";";
            }
            return sRetVal;
        }
        // Write to the customer xml file
        public void VoelParadysCustomerXmlWriter(List<VoelParadysDataStructures.CCustomerDetails> lCustomerItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            XmlWriterSettings XmlSettings = new XmlWriterSettings();
            XmlSettings.Indent = true;
            XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysCustomers.xml", XmlSettings);

            if (lCustomerItems.Count > 0)
            {
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
                    TheWriter.WriteElementString("Address", AppendAddressArray(lCustomerItems[i].GetCustomerAddress()));
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
            }

            TheWriter.Flush();
            TheWriter.Close();
        }
        // Extract the address string into an array of address values
        private string[] ExtractAddressData(string sAddress)
        {
            string[] saRetValue = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            char[] acDelimiterArray = { ';' };
            string[] saAddressLines = sAddress.Split(acDelimiterArray, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < saAddressLines.Length; ++i)
            {
                if (i >= 5)
                    saRetValue[4] += ", " + saAddressLines[i];
                else
                    saRetValue[i] = saAddressLines[i];
            }

            return saRetValue;
        }
        // Read from the customer xml file
        public void VoelParadysCustomerXmlReader(List<VoelParadysDataStructures.CCustomerDetails> lCustomerItems)
        {
            if (IsXmlFileValid("../Data/VoelParadysCustomers.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysCustomers.xml");

                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        VoelParadysDataStructures.CCustomerDetails TempCustomer = new VoelParadysDataStructures.CCustomerDetails(-1, "", "", new string[5] { "", "", "", "", "" }, "", -1);
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
                                        TempCustomer.SetCustomerAddress(ExtractAddressData(TheReader.Value));
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
        public void VoelParadysSupplierXmlWriter(List<VoelParadysDataStructures.CSupplierDetails> lSupplierItems)
        {
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            XmlWriterSettings XmlSettings = new XmlWriterSettings();
            XmlSettings.Indent = true;

            XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysSuppliers.xml", XmlSettings);

            if (lSupplierItems.Count > 0)
            {
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
                    TheWriter.WriteElementString("Address", AppendAddressArray(lSupplierItems[i].GetSupplierAddress()));
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
            }

            TheWriter.Flush();
            TheWriter.Close();
        }
        // Read from the customer xml file
        public void VoelParadysSupplierXmlReader(List<VoelParadysDataStructures.CSupplierDetails> lSupplierItems)
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
                        VoelParadysDataStructures.CSupplierDetails TempSupplier = new VoelParadysDataStructures.CSupplierDetails(-1, "", "", "", new string[5] { "", "", "", "", "" }, "-1");
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
                                        TempSupplier.SetSupplierAddress(ExtractAddressData(TheReader.Value));
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

        // Write to the usage xml file
        public void VoelParadysUsageXmlWriter(string sItemCode, int iQuantity)
        {
            DateTime theDateAndTime = DateTime.Now;
            string sTheDate = theDateAndTime.ToString("dd/MM/yyyy");
            // Get examples for xml reading and writing from http://forum.codecall.net/topic/58239-c-tutorial-reading-and-writing-xml-files/
            List<VoelParadysDataStructures.UsageData> lUsageList = new List<VoelParadysDataStructures.UsageData>();
            VoelParadysUsageXmlReader(lUsageList);
            VoelParadysDataStructures.UsageData TempData = new VoelParadysDataStructures.UsageData();
            TempData.sDate = sTheDate;
            TempData.sItemCode = sItemCode;
            TempData.iItemQuantity = iQuantity;
            lUsageList.Add(TempData);

            XmlWriterSettings XmlSettings = new XmlWriterSettings();
            XmlSettings.Indent = true;

            XmlWriter TheWriter = XmlWriter.Create("../Data/VoelParadysUsage.xml", XmlSettings);
            TheWriter.WriteStartDocument();
            TheWriter.WriteStartElement("Usage");

            for (int i = 0; i < lUsageList.Count; ++i)
            {
                string sStartElement = "ID_"+ lUsageList[i].sItemCode + "_" + lUsageList[i].sDate.Replace("/", "");
                sStartElement = sStartElement.Replace(" ", "");
                TheWriter.WriteComment("This is the usage of " + lUsageList[i].sItemCode + " for " + lUsageList[i].sDate);
                TheWriter.WriteStartElement(sStartElement);
                TheWriter.WriteElementString("Date", lUsageList[i].sDate);
                TheWriter.WriteElementString("ItemCode", lUsageList[i].sItemCode);
                TheWriter.WriteElementString("Quantity", lUsageList[i].iItemQuantity.ToString());
                TheWriter.WriteEndElement();
            }
            TheWriter.WriteEndElement();
            TheWriter.WriteEndDocument();
            TheWriter.Flush();
            TheWriter.Close();
        }
        // Read from the usage xml file
        public void VoelParadysUsageXmlReader(List<VoelParadysDataStructures.UsageData> lUsageList)
        {
            if (IsXmlFileValid("../Data/VoelParadysUsage.xml"))
            {
                XmlReader TheReader = XmlReader.Create("../Data/VoelParadysUsage.xml");

                while (TheReader.Read())
                {
                    if (TheReader.NodeType == XmlNodeType.Element)
                    {
                        VoelParadysDataStructures.UsageData TempUsageItem = new VoelParadysDataStructures.UsageData();
                        while (TheReader.NodeType != XmlNodeType.EndElement)
                        {
                            TheReader.Read();
                            if (TheReader.Name == "Date")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempUsageItem.sDate = TheReader.Value;
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "ItemCode")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempUsageItem.sItemCode = TheReader.Value;
                                }
                                TheReader.Read();
                            }
                            if (TheReader.Name == "Quantity")
                            {
                                while (TheReader.NodeType != XmlNodeType.EndElement)
                                {
                                    TheReader.Read();
                                    if (TheReader.NodeType == XmlNodeType.Text)
                                        TempUsageItem.iItemQuantity = int.Parse(TheReader.Value);
                                }
                                TheReader.Read();
                            }
                        }
                        lUsageList.Add(TempUsageItem);
                    }
                }
                TheReader.Close();
            }
        }
    }
}
