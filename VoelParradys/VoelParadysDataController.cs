using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace VoelParadys
{
    class VoelParadysDataController
    {
        // Instance of this class that can be viewed by everybody
        private static VoelParadysDataController DataControllerInstance = null;
        public static VoelParadysDataController GetInstance()
        {
            if (DataControllerInstance == null)
                DataControllerInstance = new VoelParadysDataController();

            return DataControllerInstance;
        }

        // Private members to other data classes
        private VoelParadysInventoryData m_InventoryData = null;
        private VoelParadysCustomerData m_CustomerData = null;
        private VoelParadysXmlParser m_XmlParser = null;
        private VoelParadysKeyCodeManager m_KeyManager = null;
        private VoelParadysSuppliersData m_SupplierData = null;
        bool m_bUsePassword;

        // Constructor
        public VoelParadysDataController()
        {
            m_XmlParser = new VoelParadysXmlParser();
            VerifyXmlFiles();
            m_InventoryData = new VoelParadysInventoryData();
            ReadStockDataFromDB(m_InventoryData.GetStockItemList());
            m_CustomerData = new VoelParadysCustomerData();
            ReadCustomerDataFromDB(m_CustomerData.GetCustomerList());
            m_KeyManager = new VoelParadysKeyCodeManager();
            m_KeyManager.VerifyFileExistence("TempUser");
            m_SupplierData = new VoelParadysSuppliersData();
            ReadSupplierDataFromDB(m_SupplierData.GetSupplierList());
            m_bUsePassword = true;
        }

        // Display a warning message box before deleting anything from the database
        public DialogResult DisplayWarningMessageForDelete(string sItemToBeDeleted)
        {
            DialogResult messageBoxResult = MessageBox.Show("Are you sure you want to delete " + sItemToBeDeleted + "?\nThis cannot be undone"
                , "Warning"
                , MessageBoxButtons.YesNo);
            return messageBoxResult;
        }

        // Determine if an entered key character is valid
        public bool IsCharacterValid(char[] cValidCharacters, char cCompareCharacter)
        {
            return m_KeyManager.IsValidCharacter(cValidCharacters, cCompareCharacter);
        }
        // Verify that the entered password is valid
        public bool VerifyPassword(string sEnteredPassword)
        {
            return m_KeyManager.IsPasswordMatch(sEnteredPassword);
        }
        // Add new password
        public void AddNewPassword(string sOldWord, string sNewWord)
        {
            m_KeyManager.VerifyOldAddNew(sOldWord, sNewWord);
        }
        // Determine if the password functionality should be used.
        public bool UsePassword()
        {
            return m_bUsePassword;
        }

        //{ 
        // XML file verification
        private void VerifyXmlFiles()
        {
            // If we do not have any xml files yet, create them
            m_XmlParser.CreateXmlFileIfNotFound("../Data/VoelParadysInventory.xml");
            m_XmlParser.CreateXmlFileIfNotFound("../Data/VoelParadysCustomers.xml");
            m_XmlParser.CreateXmlFileIfNotFound("../Data/VoelParadysSuppliers.xml");
            m_XmlParser.CreateXmlFileIfNotFound("../Data/VoelParadysUsage.xml");
        }
        //}
        //{
        // Inventory data functions
        // Get the amount of inventory items currently in the database 
        public int GetInventoryListSize()
        {
            return m_InventoryData.GetStockListSize();
        }
        // Add a new inventory item to the database
        public void AddNewInventoryItemToList(string sCode, string sName, int iQuantity, float fBuyPrice, float fSellPrice)
        {
            m_InventoryData.AddNewStockItemToList(sCode, sName, iQuantity, fBuyPrice, fSellPrice);
        }
        // Determine if the item exists in the database using the code as a lookup.
        public bool DoesItemExistInDatabase(string sItemCode)
        {
            return m_InventoryData.DoesItemExistInDatabase(sItemCode);
        }
        // Determine if the item exists in the database using the name as a lookup. The item code will be returned
        public bool DoesItemExistInDatabase(string sName, ref string rsItemCode)
        {
            return m_InventoryData.DoesItemExistInDatabase(sName, ref rsItemCode);
        }
        // Determine if a list of items exists in the database
        public bool AllItemsExistInDatabase(List<string> lsNames)
        {
            bool bRetval = true;
            string sTempItemCode = "";

            for (int i = 0; i < lsNames.Count; ++i)
            {
                if (!DoesItemExistInDatabase(lsNames[i], ref sTempItemCode))
                {
                    bRetval = false;
                    break;
                }
            }

            return bRetval;
        }
        // Get an inventory item given the code of the item
        private VoelParadysDataStructures.SStockItemDetails GetInventoryItem(string sCode)
        {
            return m_InventoryData.GetStockItem(sCode);
        }
        // Get an inventory item at a specified index
        private VoelParadysDataStructures.SStockItemDetails GetInventoryItemAt(int iIndex)
        {
            return m_InventoryData.GetStockItemAt(iIndex);
        }
        // Get the data for the stock item with the given code
        public void GetStockItemData(string sCode, ref string sName, ref int iQuantitySold, ref int iQuantityBought, ref int iQuantityUsed, ref float fCostPrice, ref float fSellPrice)
        {
            VoelParadysDataStructures.SStockItemDetails theStockItem = GetInventoryItem(sCode);
            sName = theStockItem.GetStockItemName();
            iQuantitySold = theStockItem.GetStockItemQuantitySold();
            iQuantityBought = theStockItem.GetStockItemQuantityBought();
            iQuantityUsed = theStockItem.GetStockItemQuantityUsed();
            fCostPrice = theStockItem.GetStockItemCostPrice();
            fSellPrice = theStockItem.GetStockItemSellPrice();
        }
        // Get the data for the stock item with the given index
        public void GetStockItemData(int iIndex, ref string sCode, ref string sName, ref int iQuantitySold, ref int iQuantityBought, ref int iQuantityUsed, ref float fCostPrice, ref float fSellPrice)
        {
            VoelParadysDataStructures.SStockItemDetails theStockItem = GetInventoryItemAt(iIndex);
            sCode = theStockItem.GetStockItemCode();
            sName = theStockItem.GetStockItemName();
            iQuantitySold = theStockItem.GetStockItemQuantitySold();
            iQuantityBought = theStockItem.GetStockItemQuantityBought();
            iQuantityUsed = theStockItem.GetStockItemQuantityUsed();
            fCostPrice = theStockItem.GetStockItemCostPrice();
            fSellPrice = theStockItem.GetStockItemSellPrice();
        }
        // Update an inventory item that already exists in the database
        public void UpdateInventoryItem(string sCode, string sName, int iQuantitySold, int iQuantityBought, int iQuantityUsed, float fCostPrice, float fSellPrice)
        {
            VoelParadysDataStructures.SStockItemDetails UpdatedDetails = GetInventoryItem(sCode);
            UpdatedDetails.SetStockItemName(sName);
            UpdatedDetails.SetStockItemQuantitySold(iQuantitySold);
            UpdatedDetails.SetStockItemQuantityBought(iQuantityBought);
            UpdatedDetails.SetStockItemQuantityUsed(iQuantityUsed);
            UpdatedDetails.SetStockItemCostPrice(fCostPrice);
            UpdatedDetails.SetStockItemSellPrice(fSellPrice);
            m_InventoryData.UpdateStockItem(UpdatedDetails);
        }
        // Delete and inventory item from the database
        public void DeleteInventoryItem(string sItemCode)
        {
            m_InventoryData.DeleteStockItem(sItemCode);
        }
        // Read in the stock details from the XML file
        private void ReadStockDataFromDB(List<VoelParadysDataStructures.SStockItemDetails> lStockList)
        {
            m_XmlParser.VoelParadysStockXmlReader(lStockList);
        }
        // Write the stock details to the XML file
        public void WriteStockDataToDB(List<VoelParadysDataStructures.SStockItemDetails> lStockList)
        {
            m_XmlParser.VoelParadysStockXmlWriter(lStockList);
        }
        // Read in the daily sale data from the XML file
        public void ReadDailySaleDataFromDB(string sFileName, ref List<VoelParadysDataStructures.SSaleDetails> lSaleDetails, bool bFromCashup)
        {
            m_XmlParser.VoelParadysDailySaleDataXmlReader(sFileName, ref lSaleDetails, bFromCashup);
        }
        // Write the daily sale data to the XML file
        public void WriteDailySaleDataToDB(List<VoelParadysDataStructures.SSaleInvoiceData> lSaleItems, float fSubTotal, float fCashReceived, string sPaymentType)
        {
            m_XmlParser.VoelParadysDailySaleDataXmlWriter(lSaleItems, fSubTotal, fCashReceived, sPaymentType);
        }
        // Does the daily sale file exist?
        public bool DoesFileExist(string sFileName, bool bDailySaleData)
        {
            return m_XmlParser.DoesFileExist(sFileName, bDailySaleData);
        }
        //}
        //{ Customer data functions
        // Get a unique ID for the new customer
        public void GetUniqueCustomerID(ref int iUniqueID)
        {
            m_CustomerData.GetUniqueCustomerID(ref iUniqueID);
        }
        // Read in the customer details from the XML file
        public void ReadCustomerDataFromDB(List<VoelParadysDataStructures.CCustomerDetails> lCustomerList)
        {
            m_XmlParser.VoelParadysCustomerXmlReader(lCustomerList);
        }
        // Write the customer details to the XML file
        public void WriteCustomerDataToDB()
        {
            m_CustomerData.SortCustomerList();
            m_XmlParser.VoelParadysCustomerXmlWriter(m_CustomerData.GetCustomerList());
        }
        // Add a new customer to the database
        public void AddNewCustomerToList(VoelParadysDataStructures.CCustomerDetails theNewCustomer)
        {
            m_CustomerData.AddNewCustomerToList(theNewCustomer);
        }
        // Get the amount of customers currently in the database 
        public int GetCustomerListSize()
        {
            return m_CustomerData.GetCustomerListSize();
        }
        // Get a customer at a specified index
        private VoelParadysDataStructures.CCustomerDetails GetCustomerAt(int iIndex)
        {
            return m_CustomerData.GetCustomerAt(iIndex);
        }
        // Get the customer from a given ID
        private VoelParadysDataStructures.CCustomerDetails GetCustomerFromID(int iCustomerID)
        {
            return m_CustomerData.GetCustomerFromID(iCustomerID);
        }
        // Get the data for the customer with the given ID code
        public void GetCustomerData(int iID, ref string sName, ref string sSurname, ref string[] saAddress, ref string sPhoneNumber, ref long lIDNumber)
        {
            VoelParadysDataStructures.CCustomerDetails theCustomer = GetCustomerFromID(iID);
            List<string> lsCustAddress = new List<string>();
            saAddress = theCustomer.GetCustomerAddress();
            sName = theCustomer.GetCustomerName();
            sSurname = theCustomer.GetCustomerSurname();
            sPhoneNumber = theCustomer.GetCustomerPhone();
            lIDNumber = theCustomer.GetCustomerIdNumber();

            for (int i = 0; i < saAddress.Length; ++i)
            {
                if (saAddress[i] != "-1")
                    lsCustAddress.Add(saAddress[i]);
            }
            saAddress = lsCustAddress.ToArray();
        }
        // Get the data for the customer with the given index
        public void GetCustomerData(int iIndex, ref int iID, ref string sName, ref string sSurname, ref string[] saAddress, ref string sPhoneNumber, ref long lIDNumber)
        {
            VoelParadysDataStructures.CCustomerDetails theCustomer = GetCustomerAt(iIndex);
            List<string> lsCustAddress = new List<string>();
            saAddress = theCustomer.GetCustomerAddress();
            iID = theCustomer.GetCustomerID();
            sName = theCustomer.GetCustomerName();
            sSurname = theCustomer.GetCustomerSurname();
            sPhoneNumber = theCustomer.GetCustomerPhone();
            lIDNumber = theCustomer.GetCustomerIdNumber();

            for (int i = 0; i < saAddress.Length; ++i)
            {
                if (saAddress[i] != "-1")
                    lsCustAddress.Add(saAddress[i]);
            }
            saAddress = lsCustAddress.ToArray();
        }
        // Determine if the customer with the given ID exists in the database
        public bool DoesCustomerExistInDatabase(int iCustomerID)
        {
            return m_CustomerData.DoesCustomerExistInDatabase(iCustomerID);
        }
        // Update the customers details with the given details
        public void UpdateCustomerDetails(int iID, string sName, string sSurname, string[] saAddress, string sPhoneNumber, long lIDNumber)
        {
            VoelParadysDataStructures.CCustomerDetails UpdatedDetails = GetCustomerFromID(iID);
            UpdatedDetails.SetCustomerName(sName);
            UpdatedDetails.SetCustomerSurname(sSurname);
            UpdatedDetails.SetCustomerAddress(saAddress);
            UpdatedDetails.SetCustomerPhoneNumber(sPhoneNumber);
            UpdatedDetails.SetCustomerIdNumber(lIDNumber);
            m_CustomerData.UpdateCustomerDetails(UpdatedDetails);
        }
        // Delete a customer from the database
        public void DeleteCustomerFromDB(int iCustomerID)
        {
            m_CustomerData.DeleteCustomerFromDatabase(iCustomerID);
        }
        // Add a wish list item to the customer with the given ID
        public void AddCustomerWishListItem(int iCustomerID, string sWishedItem)
        {
            var theSelectedCustomer = GetCustomerFromID(iCustomerID);
            theSelectedCustomer.AddCustomerWishListItem(sWishedItem);
            m_CustomerData.UpdateCustomerDetails(theSelectedCustomer);
        }
        // Remove a wish list item to the customer with the given ID
        public void DeleteCustomerWishListItem(int iCustomerID, string sWishedItem)
        {
            var theSelectedCustomer = GetCustomerFromID(iCustomerID);
            theSelectedCustomer.DeleteCustomerWishListItem(sWishedItem);
            m_CustomerData.UpdateCustomerDetails(theSelectedCustomer);
        }
        // Get the number of wish list items for the customer with the given ID
        public int GetCustomerWishListCount(int iCustomerID)
        {
            return GetCustomerFromID(iCustomerID).GetCustomerWishListCount();
        }
        // Get the wished for item for the given customer ID at the given index
        public string GetCustomerWishListItemAt(int iCustomerID, int iIndex)
        {
            return GetCustomerFromID(iCustomerID).GetCustomerWishListItemAt(iIndex);
        }
        // Get the ID of a customer with the given name
        public int GetCustomerIDFromName(string sName)
        {
            return m_CustomerData.GetCustomerIDFromName(sName);
        }
        // Get a list of all the customer names
        public List<VoelParadysDataStructures.CIntStringMap> GetAllCustomersNameAndID()
        {
            return m_CustomerData.GetAllCustomersNameAndID();
        }
        //}
        //{ Supplier data functions
        // Get a unique ID for the new supplier
        public void GetUniqueSupplierID(ref int iUniqueID)
        {
            m_SupplierData.GetUniqueSupplierID(ref iUniqueID);
        }
        // Read in the supplier details from the XML file
        public void ReadSupplierDataFromDB(List<VoelParadysDataStructures.CSupplierDetails> lSupplierList)
        {
            m_XmlParser.VoelParadysSupplierXmlReader(lSupplierList);
        }
        // Write the customer details to the XML file
        public void WriteSupplierDataToDB()
        {
            m_SupplierData.SortSupplierList();
            m_XmlParser.VoelParadysSupplierXmlWriter(m_SupplierData.GetSupplierList());
        }
        // Add a new Supplier to the database
        public void AddNewSupplierToList(VoelParadysDataStructures.CSupplierDetails theNewSupplier)
        {
            m_SupplierData.AddNewSupplierToList(theNewSupplier);
        }
        // Get the amount of Supplier currently in the database 
        public int GetSupplierListSize()
        {
            return m_SupplierData.GetSupplierListSize();
        }
        // Get a Supplier at a specified index
        private VoelParadysDataStructures.CSupplierDetails GetSupplierAt(int iIndex)
        {
            return m_SupplierData.GetSupplierAt(iIndex);
        }
        // Get the Supplier from a given ID
        private VoelParadysDataStructures.CSupplierDetails GetSupplierFromID(int iSupplierID)
        {
            return m_SupplierData.GetSupplierFromID(iSupplierID);
        }
        // Get the data for the Supplier with the given ID code
        public void GetSupplierData(int iID, ref string sName, ref string sRepName, ref string sRepSurname, ref string[] saAddress, ref string sPhoneNumber)
        {
            VoelParadysDataStructures.CSupplierDetails theSupplier = GetSupplierFromID(iID);
            List<string> lsSuppAddress = new List<string>();
            saAddress = theSupplier.GetSupplierAddress();
            sName = theSupplier.GetSupplierName();
            sRepName = theSupplier.GetRepName();
            sRepSurname = theSupplier.GetRepSurname();
            sPhoneNumber = theSupplier.GetSupplierPhone();

            for (int i = 0; i < saAddress.Length; ++i)
            {
                if (saAddress[i] != "-1")
                    lsSuppAddress.Add(saAddress[i]);
            }
            saAddress = lsSuppAddress.ToArray();
        }
        // Get the data for the Supplier with the given index
        public void GetSupplierData(int iIndex, ref int iID, ref string sName, ref string sRepName, ref string sRepSurname, ref string[] saAddress, ref string sPhoneNumber)
        {
            VoelParadysDataStructures.CSupplierDetails theSupplier = GetSupplierAt(iIndex);
            List<string> lsSuppAddress = new List<string>();
            saAddress = theSupplier.GetSupplierAddress();
            iID = theSupplier.GetSupplierID();
            sName = theSupplier.GetSupplierName();
            sRepName = theSupplier.GetRepName();
            sRepSurname = theSupplier.GetRepSurname();
            sPhoneNumber = theSupplier.GetSupplierPhone();

            for (int i = 0; i < saAddress.Length; ++i)
            {
                if (saAddress[i] != "-1")
                    lsSuppAddress.Add(saAddress[i]);
            }
            saAddress = lsSuppAddress.ToArray();
        }
        // Determine if the Supplier with the given ID exists in the database
        public bool DoesSupplierExistInDatabase(int iSupplierID)
        {
            return m_SupplierData.DoesSupplierExistInDatabase(iSupplierID);
        }
        // Update the Supplier details with the given details
        public void UpdateSupplierDetails(int iID, string sName, string sRepName, string sRepSurname, string[] saAddress, string sPhoneNumber)
        {
            VoelParadysDataStructures.CSupplierDetails UpdatedDetails = GetSupplierFromID(iID);
            UpdatedDetails.SetSupplierName(sName);
            UpdatedDetails.SetRepName(sRepName);
            UpdatedDetails.SetRepSurname(sRepSurname);
            UpdatedDetails.SetSupplierAddress(saAddress);
            UpdatedDetails.SetSupplierPhoneNumber(sPhoneNumber);
            m_SupplierData.UpdateSupplierDetails(UpdatedDetails);
        }
        // Delete a Supplier from the database
        public void DeleteSupplierFromDB(int iSupplierID)
        {
            m_SupplierData.DeleteSupplierFromDatabase(iSupplierID);
        }
        // Add a supplied item to the Supplier with the given ID
        public void AddSuppliedItem(int iSupplierID, string sItemCode, int iItemQuantity)
        {
            var theSelectedSupplier = GetSupplierFromID(iSupplierID);
            theSelectedSupplier.AddSuppliedItemsListItem(sItemCode, iItemQuantity);
            m_SupplierData.UpdateSupplierDetails(theSelectedSupplier);
        }
        // Add a supplied item to the Supplier with the given ID
        public void UpdateSuppliedItem(int iSupplierID, string sItemCode, int iQuantityIncrement)
        {
            var theSelectedSupplier = GetSupplierFromID(iSupplierID);
            theSelectedSupplier.UpdateSuppliedItemsListItem(sItemCode, iQuantityIncrement);
            m_SupplierData.UpdateSupplierDetails(theSelectedSupplier);
        }
        // Remove a supplied item from the supplier with the given ID
        public void DeleteSuppliedItem(int iSupplierID, string sItemCode)
        {
            var theSelectedSupplier = GetSupplierFromID(iSupplierID);
            theSelectedSupplier.DeleteSuppliedItemsListItem(sItemCode);
            m_SupplierData.UpdateSupplierDetails(theSelectedSupplier);
        }
        // Get the number of supplied items for the Supplier with the given ID
        public int GetSuppliedListCount(int iSupplierID)
        {
            return GetSupplierFromID(iSupplierID).GetSuppliedItemsListCount();
        }
        // Get the supplied item for the given Supplier ID at the given index
        public void GetSuppliedListItemAt(int iSupplierID, int iIndex, ref string sItemCode, ref int iItemQuantity)
        {
            GetSupplierFromID(iSupplierID).GetSuppliedItemsListItemAt(iIndex, ref sItemCode, ref iItemQuantity);
        }
        // Get the ID of the supplier with the given name
        public int GetSupplierIDFromName(string sName)
        {
            return m_SupplierData.GetSupplieIDFromName(sName);
        }
        public List<string> GetAllSupplierNames()
        {
            return m_SupplierData.GetAllSupplierNames();
        }
        // }
        //{ Usage functionality
        public void WriteUsageDataToFile(string sItemCode, int iItemQty)
        {
            m_XmlParser.VoelParadysUsageXmlWriter(sItemCode, iItemQty);
        }
        public bool GetUsageDataList(string sStartDate, string sEndDate, ref List<VoelParadysDataStructures.UsageData> theUsageList)
        {
            string sDateFormat = "dd/MM/yyyy";
            DateTime TheDateIncrementer;
            DateTime TheEndDate;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
            DateTimeStyles styles = DateTimeStyles.None;

            if (DateTime.TryParse(sStartDate, culture, styles, out TheDateIncrementer) && DateTime.TryParse(sEndDate, culture, styles, out TheEndDate))
            {
                if (TheDateIncrementer <= TheEndDate)
                {
                    List<VoelParadysDataStructures.UsageData> tempList = new List<VoelParadysDataStructures.UsageData>();
                    m_XmlParser.VoelParadysUsageXmlReader(tempList);

                    while (TheDateIncrementer <= TheEndDate)
                    {
                        for (int i = 0; i < tempList.Count; ++i)
                        {
                            if (tempList[i].sDate == TheDateIncrementer.ToString(sDateFormat))
                                theUsageList.Add(tempList[i]);
                        }
                        TheDateIncrementer = TheDateIncrementer.AddDays(1);
                    }
                }
                else
                    PrintErrorMessage("The end date should be greater than the start date");
            }
            else
            {
                PrintErrorMessage("Error parsing date(s)");
            }
            return theUsageList.Count > 0;
        }
        public bool GetDailySaleFileList(string sStartDate, string sEndDate, ref List<VoelParadysDataStructures.CStringStringMap> theSaleFileList)
        {
            DateTime TheDateIncrementer;
            DateTime TheEndDate;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
            DateTimeStyles styles = DateTimeStyles.None;

            if (DateTime.TryParse(sStartDate, culture, styles, out TheDateIncrementer) && DateTime.TryParse(sEndDate, culture, styles, out TheEndDate))
            {
                if (TheDateIncrementer <= TheEndDate)
                {
                    while (TheDateIncrementer <= TheEndDate)
                    {
                        if (DoesFileExist(TheDateIncrementer.ToString("dd_MM_yy"), true))
                        {
                            VoelParadysDataStructures.CStringStringMap tempData = new VoelParadysDataStructures.CStringStringMap();
                            tempData.sString1 = TheDateIncrementer.ToString("dd/MM/yyyy");
                            tempData.sString2 = "../Data/SaleData/" + TheDateIncrementer.ToString("dd_MM_yy") + ".xml";
                            theSaleFileList.Add(tempData);
                        }
                        TheDateIncrementer = TheDateIncrementer.AddDays(1);
                    }
                }
                else
                    PrintErrorMessage("The end date should be greater than the start date");
            }
            else
                PrintErrorMessage("Error parsing date(s)");

            return theSaleFileList.Count > 0;
        }
        private void PrintErrorMessage(string sMessage)
        {
            // Return an error message
            string sCaption = "Error!!!";
            MessageBox.Show(sMessage, sCaption);
        }
        //}
    }
}
