using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    public class VoelParadysDataStructures
    {
        // The required usage data required for each item
        public struct UsageData
        {
            public string sDate { get; set; }
            public string sItemCode { get; set; }
            public int iItemQuantity { get; set; }
        }
        // A structure to store a value containing two string variables
        public struct CStringStringMap
        {
            public string sString1 { get; set; }
            public string sString2 { get; set; }
        }
        // The details for each sale
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
            public float GetSaleItemSellPriceAt(int iIndex) { return lsItemDetailList[iIndex].GetItemSellPrice(); }
            public float GetSaleCashReceived() { return fCashReceived; }
            public float GetSaleTotal() { return fSaleTotal; }
            public string GetSalePaymentType() { return sTypeOfPayment; }
            public string GetSaleTime() { return sTime; }
            public int GetSaleItemsCount() { return lsItemDetailList.Count; }
            // Setters
            public void SetSaleItemDetail(string sCode, int iQuantity, float fTheSellPrice) { SItemDetails SItem = new SItemDetails(sCode, iQuantity, fTheSellPrice); lsItemDetailList.Add(SItem); }
            public void SetSaleCashReceived(float fCash) { fCashReceived = fCash; }
            public void SetSaleTotal(float fTotal) { fSaleTotal = fTotal; }
            public void SetSalePaymentType(string sType) { sTypeOfPayment = sType; }
            public void SetSaleTime(string sTheTime) { sTime = sTheTime; }
        }
        // A data structure that can be used to create a map of int and string values 
        public class CIntStringMap
        {
            public int m_iInteger { get; set; }
            public string m_sString { get; set; }
            public CIntStringMap(int iInt, string sString)
            {
                m_iInteger = iInt;
                m_sString = sString;
            }
        }
        // A data structure for the customers details
        public class CCustomerDetails : IEquatable<CCustomerDetails>, IComparable<CCustomerDetails>
        {
            int iCustomerID;                            // The ID of the customer (Unique and auto generated)
            string sCustomerName;                       // The name of the customer (Required)
            string sCustomerSurname;                    // The surname of the customer (Optional)
            string[] saCustomerAddress;                 // The address of the customer (Optional)
            string sCustomerPhoneNumber;                // The phone number of the customer (Optional)   
            long lCustomerIDNumber;                     // The ID number of the customer (Optional)
            List<string> lsCustomerWishList;            // A list of items that the customer has requested             

            public CCustomerDetails()
            {
                lsCustomerWishList = new List<string>();
                iCustomerID = -1;
                sCustomerName = "-1";
                sCustomerSurname = "-1";
                saCustomerAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                sCustomerPhoneNumber = "-1";
                lCustomerIDNumber = -1;
            }

            public CCustomerDetails(int _iCode, string _sName, string _sSurname, string[] _saAddress, string _sPhoneNumber, long _lIdNumber)
            {
                lsCustomerWishList = new List<string>();
                iCustomerID = _iCode;
                sCustomerName = _sName;
                sCustomerSurname = _sSurname;
                saCustomerAddress = _saAddress;
                sCustomerPhoneNumber = _sPhoneNumber;
                lCustomerIDNumber = _lIdNumber;
            }

            // Getters
            public int GetCustomerID() { return iCustomerID; }
            public string GetCustomerName() { return sCustomerName; }
            public string GetCustomerSurname() { return sCustomerSurname; }
            public string[] GetCustomerAddress() { return saCustomerAddress; }
            public string GetCustomerPhone() { return sCustomerPhoneNumber; }
            public long GetCustomerIdNumber() { return lCustomerIDNumber; }
            public int GetCustomerWishListCount() { return lsCustomerWishList == null ? 0 : lsCustomerWishList.Count; }
            public string GetCustomerWishListItemAt(int iIndex) { return lsCustomerWishList[iIndex]; }
            // Setters
            public void SetCustomerID(int iID)
            {
                iCustomerID = iID;
            }
            public void SetCustomerName(string sName)
            {
                string sTheCustomerName = sName == null ? "" : sName;
                sCustomerName = sTheCustomerName;
            }
            public void SetCustomerSurname(string sSurname)
            {
                string sTheSurname = sSurname == null ? "" : sSurname;
                sCustomerSurname = sTheSurname;
            }
            public void SetCustomerAddress(string[] saAddress)
            {
                string[] sTheAddress = saAddress == null ? new string[5] { "", "", "", "", "" } : saAddress;
                saCustomerAddress = sTheAddress;
            }
            public void SetCustomerPhoneNumber(string sPhone)
            {
                string sThePhoneNumber = sPhone == null ? "" : sPhone;
                sCustomerPhoneNumber = sThePhoneNumber;
            }
            public void SetCustomerIdNumber(long IDNumber)
            {
                lCustomerIDNumber = IDNumber;
            }
            public void AddCustomerWishListItem(string sItemName)
            {
                lsCustomerWishList.Add(sItemName);
            }
            public void DeleteCustomerWishListItem(string sItemName)
            {
                lsCustomerWishList.Remove(sItemName);
            }

            // Comparison overrides
            // Override the Equals function
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                CCustomerDetails ObjAsCustomerDetails = obj as CCustomerDetails;
                if (ObjAsCustomerDetails == null)
                    return false;
                else
                    return Equals(ObjAsCustomerDetails);
            }
            // Default comparer for CCustomerType
            public int CompareTo(CCustomerDetails CustomerToCompare)
            {
                // A null value means that this object is greater
                if (CustomerToCompare == null)
                    return 1;
                else
                    return this.iCustomerID.CompareTo(CustomerToCompare.iCustomerID);
            }
            // Override the GetHashCode function to return the customer ID
            public override int GetHashCode()
            {
                return iCustomerID;
            }
            // My equals function
            public bool Equals(CCustomerDetails other)
            {
                if (other == null)
                    return false;
                return (this.iCustomerID.Equals(other.iCustomerID));
            }
        }
        // A data class for the suppliers details with comparable member functions
        public class CSupplierDetails : IEquatable<CSupplierDetails>, IComparable<CSupplierDetails>
        {
            int iSupplierID;                            // The ID of the supplier (Unique and auto generated)
            string sSupplierName;                       // The name of the supplier (Required)
            string sRepName;                            // The name of the representative for the supplier (Optional)
            string sRepSurname;                         // The surname of the representative for the supplier (Optional)
            string[] saSupplierAddress;                 // The address of the supplier (Optional)
            string sSupplierPhoneNumber;                // The phone number of the supplier (Optional)   
            List<VoelParadysDataStructures.CIntStringMap> lsSuppliedItemsList;           // A list of all the items bought from the supplier              

            public CSupplierDetails()
            {
                lsSuppliedItemsList = new List<VoelParadysDataStructures.CIntStringMap>();
                iSupplierID = -1;
                sSupplierName = "-1";
                sRepName = "-1";
                sRepSurname = "-1";
                saSupplierAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                sSupplierPhoneNumber = "-1";
            }

            public CSupplierDetails(int _iCode, string _sSupName, string _sRepName, string _sRepSurname, string[] _saAddress, string _sPhoneNumber)
            {
                lsSuppliedItemsList = new List<VoelParadysDataStructures.CIntStringMap>();
                iSupplierID = _iCode;
                sSupplierName = _sSupName;
                sRepName = _sRepName;
                sRepSurname = _sRepSurname;
                saSupplierAddress = _saAddress;
                sSupplierPhoneNumber = _sPhoneNumber;
            }

            // Getters
            public int GetSupplierID() { return iSupplierID; }
            public string GetSupplierName() { return sSupplierName; }
            public string GetRepName() { return sRepName; }
            public string GetRepSurname() { return sRepSurname; }
            public string[] GetSupplierAddress() { return saSupplierAddress; }
            public string GetSupplierPhone() { return sSupplierPhoneNumber; }
            public int GetSuppliedItemsListCount() { return lsSuppliedItemsList == null ? 0 : lsSuppliedItemsList.Count; }
            public void GetSuppliedItemsListItemAt(int iIndex, ref string sCode, ref int iQuantity)
            {
                VoelParadysDataStructures.CIntStringMap TempItem = lsSuppliedItemsList[iIndex];
                sCode = TempItem.m_sString;
                iQuantity = TempItem.m_iInteger;
            }
            // Setters
            public void SetSupplierID(int iID)
            {
                iSupplierID = iID;
            }
            public void SetSupplierName(string sName)
            {
                string sTheSupplierName = sName == null ? "" : sName;
                sSupplierName = sTheSupplierName;
            }
            public void SetRepName(string sName)
            {
                string sTheRepName = sName == null ? "" : sName;
                sRepName = sTheRepName;
            }
            public void SetRepSurname(string sSurname)
            {
                string sTheSurname = sSurname == null ? "" : sSurname;
                sRepSurname = sTheSurname;
            }
            public void SetSupplierAddress(string[] saAddress)
            {
                saSupplierAddress = saAddress;
            }
            public void SetSupplierPhoneNumber(string sPhone)
            {
                string sThePhoneNumber = sPhone == null ? "" : sPhone;
                sSupplierPhoneNumber = sThePhoneNumber;
            }
            public void AddSuppliedItemsListItem(string sItemCode, int iQuantity)
            {
                VoelParadysDataStructures.CIntStringMap TempItem = new VoelParadysDataStructures.CIntStringMap(iQuantity, sItemCode);
                lsSuppliedItemsList.Add(TempItem);
            }
            public void UpdateSuppliedItemsListItem(string sItemCode, int iQuantity)
            {
                for (int i = 0; i < lsSuppliedItemsList.Count; ++i)
                {
                    // If the item exists in the list, then update it
                    if (lsSuppliedItemsList[i].m_sString == sItemCode)
                    {
                        lsSuppliedItemsList[i].m_iInteger += iQuantity;
                        return;
                    }
                }

                // If the item does not exist in the list, then we add it. This will only be reached if the for loop does not return
                lsSuppliedItemsList.Add(new VoelParadysDataStructures.CIntStringMap(iQuantity, sItemCode));
            }
            public void DeleteSuppliedItemsListItem(string sItemCode)
            {
                for (int i = 0; i < lsSuppliedItemsList.Count; ++i)
                {
                    if (lsSuppliedItemsList[i].m_sString == sItemCode)
                        lsSuppliedItemsList.RemoveAt(i);
                }
            }

            // Comparison overrides
            // Override the Equals function
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                CSupplierDetails ObjAsSupplierDetails = obj as CSupplierDetails;
                if (ObjAsSupplierDetails == null)
                    return false;
                else
                    return Equals(ObjAsSupplierDetails);
            }
            // Default comparer for CSupplierDetails Type
            public int CompareTo(CSupplierDetails SupplierToCompare)
            {
                // A null value means that this object is greater
                if (SupplierToCompare == null)
                    return 1;
                else
                    return this.iSupplierID.CompareTo(SupplierToCompare.iSupplierID);
            }
            // Override the GetHashCode function to return the supplier ID
            public override int GetHashCode()
            {
                return iSupplierID;
            }
            // My equals function
            public bool Equals(CSupplierDetails other)
            {
                if (other == null)
                    return false;
                return (this.iSupplierID.Equals(other.iSupplierID));
            }
        }
        // A data structure for the details of the item
        public struct SItemDetails
        {
            string sItemCode;           // The code of the item sold
            int iItemQuantitySold;      // The amount of the item sold
            float fSellPrice;           // The price that the item is sold at

            public SItemDetails(string _sItemCode, int _iItemQuantity, float _fSellPrice)
            {
                string sTheItemCode = _sItemCode == null ? "" : _sItemCode;
                sItemCode = sTheItemCode;
                iItemQuantitySold = _iItemQuantity;
                fSellPrice = _fSellPrice;
            }

            public string GetItemCode() { return sItemCode; }
            public int GetItemQuantitySold() { return iItemQuantitySold; }
            public float GetItemSellPrice() { return fSellPrice; }
            // Setters
            public void SetSaleItemDetails(string sCode, int iQuantitySold, float fTheSellPrice) { sItemCode = sCode; iItemQuantitySold = iQuantitySold; fSellPrice = fTheSellPrice; }
        }
        // A data structure for the details of the stock items 
        public struct SStockItemDetails
        {
            string sItemName;               // The name of the stock item
            string sItemCode;                  // The code assigned to the stock item
            int iItemQuantitySold;          // The amount of the item that has been sold
            int iItemQuantityBought;       // The amount of the item currently in stock
            int iItemQuantityUsed;          // The amount of the item that has been used in the store
            float fItemCostPrice;           // The cost price of the item
            float fItemSellPrice;           // The sell price of the item

            public SStockItemDetails(string _sItemName, string _sItemCode, int _iItemQuantSold, int _iItemQuantBought, int _iItemQuantUsed, float _fItemCost, float _fItemSell)
            {
                sItemName = _sItemName;
                sItemCode = _sItemCode;
                iItemQuantitySold = _iItemQuantSold;
                iItemQuantityBought = _iItemQuantBought;
                iItemQuantityUsed = _iItemQuantUsed;
                fItemCostPrice = _fItemCost;
                fItemSellPrice = _fItemSell;
            }

            // Getters
            public string GetStockItemName() { return sItemName; }
            public string GetStockItemCode() { return sItemCode; }
            public int GetStockItemQuantitySold() { return iItemQuantitySold; }
            public int GetStockItemQuantityBought() { return iItemQuantityBought; }
            public int GetStockItemQuantityUsed() { return iItemQuantityUsed; }
            public float GetStockItemCostPrice() { return fItemCostPrice; }
            public float GetStockItemSellPrice() { return fItemSellPrice; }
            // Setters
            public void SetStockItemName(string sName) { sItemName = sName; }
            public void SetStockItemCode(string sCode) { sItemCode = sCode; }
            public void SetStockItemQuantitySold(int iQuantity) { iItemQuantitySold = iQuantity; }
            public void SetStockItemQuantityBought(int iQuantity) { iItemQuantityBought = iQuantity; }
            public void SetStockItemQuantityUsed(int iQuantity) { iItemQuantityUsed = iQuantity; }
            public void SetStockItemCostPrice(float fCostPrice) { fItemCostPrice = fCostPrice; }
            public void SetStockItemSellPrice(float fSellPrice) { fItemSellPrice = fSellPrice; }
        }
        // A data structure for the current sale item
        public struct SSaleInvoiceData
        {
            string m_sItemCode;                 // The code of the item to be sold
            string m_sItemName;                 // The name of the item to be sold
            int m_iItemQuantity;                // The amount of the item to be sold
            float m_fItemSellPrice;             // The price that the item was sold at

            public SSaleInvoiceData(string sItemCode, string sItemName, int iItemQuantity, float fSellPrice)
            {
                m_sItemCode = sItemCode;
                m_sItemName = sItemName;
                m_iItemQuantity = iItemQuantity;
                m_fItemSellPrice = fSellPrice;
            }

            // Add the item code to the sale item
            public void AddCode(string sCode)
            {
                m_sItemCode = sCode;
            }
            // Add the item name to the sale item
            public void AddName(string sName)
            {
                m_sItemName = sName;
            }
            // Add the quantity to the sale item
            public void AddQuantity(int iQuantity)
            {
                m_iItemQuantity = iQuantity;
            }
            // Add the quantity to the sale item
            public void AddSellPrice(float fPrice)
            {
                m_fItemSellPrice = fPrice;
            }
            // Clear the sale item for a new entry
            public void ClearItem()
            {
                m_sItemCode = "";
                m_sItemName = "";
                m_iItemQuantity = 1;
                m_fItemSellPrice = 0;
            }
            // Get the sale item code
            public string GetItemCode()
            {
                return m_sItemCode;
            }
            // Get the sale item name
            public string GetItemName()
            {
                return m_sItemName;
            }
            // Get the sale item quantity
            public int GetItemQuantity()
            {
                return m_iItemQuantity;
            }
            // Get the sale item sell price
            public float GetItemSellPrice()
            {
                return m_fItemSellPrice;
            }
        }
    }
}
