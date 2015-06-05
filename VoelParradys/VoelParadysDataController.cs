﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoelParadys
{
    public struct SItemDetails
    {
        string sItemCode;           // The code of the item sold
        int iItemQuantitySold;      // The amount of the item sold

        public SItemDetails(string _sItemCode, int _iItemQuantity)
        {
            string sTheItemCode = _sItemCode == null ? "" : _sItemCode;
            sItemCode = sTheItemCode;
            iItemQuantitySold = _iItemQuantity;
        }

        public string GetItemCode() { return sItemCode; }
        public int GetItemQuantitySold() { return iItemQuantitySold; }
        // Setters
        public void SetSaleItemDetails(string sCode, int iQuantitySold) { sItemCode = sCode; iItemQuantitySold = iQuantitySold; }
    }

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
            // TODO: Add the other xml files here as they are created 
        }
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
        // Get an inventory item given the code of the item
        private SStockItemDetails GetInventoryItem(string sCode)
        {
            return m_InventoryData.GetStockItem(sCode);
        }
        // Get an inventory item at a specified index
        private SStockItemDetails GetInventoryItemAt(int iIndex)
        {
            return m_InventoryData.GetStockItemAt(iIndex);
        }
        // Get the data for the stock item with the given code
        public void GetStockItemData(string sCode, ref string sName, ref int iQuantitySold, ref int iQuantityBought, ref int iQuantityUsed, ref float fCostPrice, ref float fSellPrice)
        {
            SStockItemDetails theStockItem = GetInventoryItem(sCode);
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
            SStockItemDetails theStockItem = GetInventoryItemAt(iIndex);
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
            SStockItemDetails UpdatedDetails = GetInventoryItem(sCode);
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
        private void ReadStockDataFromDB(List<SStockItemDetails> lStockList)
        {
            m_XmlParser.VoelParadysStockXmlReader(lStockList);
        }
        // Write the stock details to the XML file
        public void WriteStockDataToDB(List<SStockItemDetails> lStockList)
        {
            m_XmlParser.VoelParadysStockXmlWriter(lStockList);
        }
        // Read in the daily sale data from the XML file
        public void ReadDailySaleDataFromDB()
        {
            // Not yet implemented (ETS TODO)
        }
        // Write the daily sale data to the XML file
        public void WriteDailySaleDataToDB(List<SSaleInvoiceData> lSaleItems, float fSubTotal, float fCashReceived, string sPaymentType)
        {
            m_XmlParser.VoelParadysDailySaleDataXmlWriter(lSaleItems, fSubTotal, fCashReceived, sPaymentType);
        }
        // Customer data functions
        // Get a unique ID for the new customer
        public void GetUniqueCustomerID(ref int iUniqueID)
        {
            m_CustomerData.GetUniqueCustomerID(ref iUniqueID);
        }
        // Read in the customer details from the XML file
        public void ReadCustomerDataFromDB(List<CCustomerDetails> lCustomerList)
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
        public void AddNewCustomerToList(CCustomerDetails theNewCustomer)
        {
            m_CustomerData.AddNewCustomerToList(theNewCustomer);
        }
        // Get the amount of customers currently in the database 
        public int GetCustomerListSize()
        {
            return m_CustomerData.GetCustomerListSize();
        }
        // Get a customer at a specified index
        private CCustomerDetails GetCustomerAt(int iIndex)
        {
            return m_CustomerData.GetCustomerAt(iIndex);
        }
        // Get the customer from a given ID
        private CCustomerDetails GetCustomerFromID(int iCustomerID)
        {
            return m_CustomerData.GetCustomerFromID(iCustomerID);
        }
        // Get the data for the customer with the given ID code
        public void GetCustomerData(int iID, ref string sName, ref string sSurname, ref string sAddress, ref string sPhoneNumber, ref long lIDNumber)
        {
            CCustomerDetails theCustomer = GetCustomerFromID(iID);
            sName = theCustomer.GetCustomerName();
            sSurname = theCustomer.GetCustomerSurname();
            sAddress = theCustomer.GetCustomerAddress();
            sPhoneNumber = theCustomer.GetCustomerPhone();
            lIDNumber = theCustomer.GetCustomerIdNumber();
        }
        // Get the data for the customer with the given index
        public void GetCustomerData(int iIndex, ref int iID, ref string sName, ref string sSurname, ref string sAddress, ref string sPhoneNumber, ref long lIDNumber)
        {
            CCustomerDetails theCustomer = GetCustomerAt(iIndex);
            iID = theCustomer.GetCustomerID();
            sName = theCustomer.GetCustomerName();
            sSurname = theCustomer.GetCustomerSurname();
            sAddress = theCustomer.GetCustomerAddress();
            sPhoneNumber = theCustomer.GetCustomerPhone();
            lIDNumber = theCustomer.GetCustomerIdNumber();
        }
        // Determine if the customer with the given ID exists in the database
        public bool DoesCustomerExistInDatabase(int iCustomerID)
        {
            return m_CustomerData.DoesCustomerExistInDatabase(iCustomerID);
        }
        // Update the customers details with the given details
        public void UpdateCustomerDetails(int iID, string sName, string sSurname, string sAddress, string sPhoneNumber, long lIDNumber)
        {
            CCustomerDetails UpdatedDetails = GetCustomerFromID(iID);
            UpdatedDetails.SetCustomerName(sName);
            UpdatedDetails.SetCustomerSurname(sSurname);
            UpdatedDetails.SetCustomerAddress(sAddress);
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
        // Supplier data functions
        // Read in the supplier details from the XML file
        public void ReadSupplierDataFromDB(List<CSupplierDetails> lSupplierList)
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
        public void AddNewSupplierToList(CSupplierDetails theNewSupplier)
        {
            m_SupplierData.AddNewSupplierToList(theNewSupplier);
        }
        // Get the amount of Supplier currently in the database 
        public int GetSupplierListSize()
        {
            return m_SupplierData.GetSupplierListSize();
        }
        // Get a Supplier at a specified index
        private CSupplierDetails GetSupplierAt(int iIndex)
        {
            return m_SupplierData.GetSupplierAt(iIndex);
        }
        // Get the Supplier from a given ID
        private CSupplierDetails GetSupplierFromID(int iSupplierID)
        {
            return m_SupplierData.GetSupplierFromID(iSupplierID);
        }
        // Get the data for the Supplier with the given ID code
        public void GetSupplierData(int iID, ref string sName, ref string sRepName, ref string sRepSurname, ref string sAddress, ref string sPhoneNumber)
        {
            CSupplierDetails theSupplier = GetSupplierFromID(iID);
            sName = theSupplier.GetSupplierName();
            sRepName = theSupplier.GetRepName();
            sRepSurname = theSupplier.GetRepSurname();
            sAddress = theSupplier.GetSupplierAddress();
            sPhoneNumber = theSupplier.GetSupplierPhone();
            
        }
        // Get the data for the Supplier with the given index
        public void GetSupplierData(int iIndex, ref int iID, ref string sName, ref string sRepName, ref string sRepSurname, ref string sAddress, ref string sPhoneNumber)
        {
            CSupplierDetails theSupplier = GetSupplierAt(iIndex);
            iID = theSupplier.GetSupplierID();
            sName = theSupplier.GetSupplierName();
            sRepName = theSupplier.GetRepName();
            sRepSurname = theSupplier.GetRepSurname();
            sAddress = theSupplier.GetSupplierAddress();
            sPhoneNumber = theSupplier.GetSupplierPhone();
        }
        // Determine if the Supplier with the given ID exists in the database
        public bool DoesSupplierExistInDatabase(int iSupplierID)
        {
            return m_SupplierData.DoesSupplierExistInDatabase(iSupplierID);
        }
        // Update the Supplier details with the given details
        public void UpdateSupplierDetails(int iID, string sName, string sRepName, string sRepSurname, string sAddress, string sPhoneNumber)
        {
            CSupplierDetails UpdatedDetails = GetSupplierFromID(iID);
            UpdatedDetails.SetSupplierName(sName);
            UpdatedDetails.SetRepName(sRepName);
            UpdatedDetails.SetRepSurname(sRepSurname);
            UpdatedDetails.SetSupplierAddress(sAddress);
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
        // }
    }
}