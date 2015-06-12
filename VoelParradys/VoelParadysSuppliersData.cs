using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    public class CSupplierDetails : IEquatable<CSupplierDetails>, IComparable<CSupplierDetails>
    {
        struct SSuppliedItem
        {
            string sItemCode;
            int iItemQuantity;

            public SSuppliedItem(string sCode, int iQuantity)
            {
                sItemCode = sCode;
                iItemQuantity = iQuantity;
            }

            // Getters
            public string GetItemCode() { return sItemCode; }
            public int GetItemQuantity() { return iItemQuantity; }

            // Setters
            public void SetItemDetails(string sCode, int iQuantity)
            {
                sItemCode = sCode;
                iItemQuantity = iQuantity;
            }
            public void UpdateItemQuantity(int QuantityIncrement)
            {
                iItemQuantity += QuantityIncrement;
            }
        }

        int iSupplierID;                            // The ID of the supplier (Unique and auto generated)
        string sSupplierName;                       // The name of the supplier (Required)
        string sRepName;                            // The name of the representative for the supplier (Optional)
        string sRepSurname;                         // The surname of the representative for the supplier (Optional)
        string sSupplierAddress;                    // The address of the supplier (Optional)
        string sSupplierPhoneNumber;                // The phone number of the supplier (Optional)   
        List<SSuppliedItem> lsSuppliedItemsList;           // A list of all the items bought from the supplier              

        public CSupplierDetails()
        {
            lsSuppliedItemsList = new List<SSuppliedItem>();
            iSupplierID = -1;
            sSupplierName = "-1";
            sRepName = "-1";
            sRepSurname = "-1";
            sSupplierAddress = "-1";
            sSupplierPhoneNumber = "-1";
        }

        public CSupplierDetails(int _iCode, string _sSupName, string _sRepName, string _sRepSurname, string _sAddress, string _sPhoneNumber)
        {
            lsSuppliedItemsList = new List<SSuppliedItem>();
            iSupplierID = _iCode;
            sSupplierName = _sSupName;
            sRepName = _sRepName;
            sRepSurname = _sRepSurname;
            sSupplierAddress = _sAddress;
            sSupplierPhoneNumber = _sPhoneNumber;
        }

        // Getters
        public int GetSupplierID() { return iSupplierID; }
        public string GetSupplierName() { return sSupplierName; }
        public string GetRepName() { return sRepName; }
        public string GetRepSurname() { return sRepSurname; }
        public string GetSupplierAddress() { return sSupplierAddress; }
        public string GetSupplierPhone() { return sSupplierPhoneNumber; }
        public int GetSuppliedItemsListCount() { return lsSuppliedItemsList == null ? 0 : lsSuppliedItemsList.Count; }
        public void GetSuppliedItemsListItemAt(int iIndex, ref string sCode, ref int iQuantity) 
        { 
            SSuppliedItem TempItem = lsSuppliedItemsList[iIndex];
            sCode = TempItem.GetItemCode();
            iQuantity = TempItem.GetItemQuantity();
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
        public void SetSupplierAddress(string sAddress)
        {
            string sTheAddress = sAddress == null ? "" : sAddress;
            sSupplierAddress = sTheAddress;
        }
        public void SetSupplierPhoneNumber(string sPhone)
        {
            string sThePhoneNumber = sPhone == null ? "" : sPhone;
            sSupplierPhoneNumber = sThePhoneNumber;
        }
        public void AddSuppliedItemsListItem(string sItemCode, int iQuantity)
        {
            SSuppliedItem TempItem = new SSuppliedItem(sItemCode, iQuantity);
            lsSuppliedItemsList.Add(TempItem);
        }
        public void UpdateSuppliedItemsListItem(string sItemCode, int iQuantity)
        {
            for (int i = 0; i < lsSuppliedItemsList.Count; ++i)
            {
                // If the item exists in the list, then update it
                if (lsSuppliedItemsList[i].GetItemCode() == sItemCode)
                {
                    lsSuppliedItemsList[i].UpdateItemQuantity(iQuantity);
                    return;
                }
            }

            // If the item does not exist in the list, then we add it. This will only be reached if the for loop does not return
            lsSuppliedItemsList.Add(new SSuppliedItem(sItemCode, iQuantity));
        }
        public void DeleteSuppliedItemsListItem(string sItemCode)
        {
            for (int i = 0; i < lsSuppliedItemsList.Count; ++i)
            {
                if (lsSuppliedItemsList[i].GetItemCode() == sItemCode)
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

    class VoelParadysSuppliersData
    {
        List<CSupplierDetails> m_lSupplierList;

        public VoelParadysSuppliersData()
        {
            m_lSupplierList = new List<CSupplierDetails>();
        }

        private bool HasIDBeenUsed(int iSupplierID)
        {
            // Here we want to check if the ID has been used in the database. So that if a supplier is deleted, that ID can be reused for a new supplier
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (iSupplierID == m_lSupplierList[i].GetSupplierID())
                    return true;
            }
            return false;
        }

        public void GetUniqueSupplierID(ref int iUniqueId)
        {
            int iTempID = -1;

            if (m_lSupplierList.Count == 0)
            {
                iUniqueId = 1;
                return;
            }

            iTempID = m_lSupplierList[0].GetSupplierID() + 1;

            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (HasIDBeenUsed(iTempID))
                {
                    if (m_lSupplierList[i].GetSupplierID() >= iTempID)
                        iTempID = m_lSupplierList[i].GetSupplierID() + 1;
                }
                else
                    break;
            }
            iUniqueId = iTempID;
        }
        // This function should only be used by the controller to populate the list
        public List<CSupplierDetails> GetSupplierList()
        {
            return m_lSupplierList;
        }
        // Add a new customer to the database
        public void AddNewSupplierToList(CSupplierDetails theNewSupplier)
        {
            m_lSupplierList.Add(theNewSupplier);
            VoelParadysDataController.GetInstance().WriteSupplierDataToDB();
        }
        // Get the number of customers currently in the database
        public int GetSupplierListSize()
        {
            return m_lSupplierList.Count;
        }
        // Get a customer at a given index
        public CSupplierDetails GetSupplierAt(int iIndex)
        {
            return m_lSupplierList[iIndex];
        }
        // Get the customer from a given ID
        public CSupplierDetails GetSupplierFromID(int iSupplierID)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierID() == iSupplierID)
                    return m_lSupplierList[i];
            }
            return new CSupplierDetails(-1, "-1", "-1", "-1", "-1", "-1");
        }
        // Determine if a customer exists in the database with the given ID
        public bool DoesSupplierExistInDatabase(int iSupplierID)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierID() == iSupplierID)
                    return true;
            }
            return false;
        }
        // Update the customers details with the given details
        public void UpdateSupplierDetails(CSupplierDetails UpdatedDetails)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierID() == UpdatedDetails.GetSupplierID())
                {
                    m_lSupplierList.RemoveAt(i);
                    m_lSupplierList.Add(UpdatedDetails);
                    VoelParadysDataController.GetInstance().WriteSupplierDataToDB();
                }
            }
        }
        // Delete a customer from the database
        public void DeleteSupplierFromDatabase(int iSupplierID)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierID() == iSupplierID)
                {
                    m_lSupplierList.RemoveAt(i);
                    VoelParadysDataController.GetInstance().WriteSupplierDataToDB();
                }
            }
        }
        // Sort the customer list by ID
        public void SortSupplierList()
        {
            m_lSupplierList.Sort();
        }
        // Get the supplier ID from the supplier name
        public int GetSupplieIDFromName(string sName)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierName() == sName)
                    return m_lSupplierList[i].GetSupplierID();
            }
            return -1;
        }
        // Get a list of all the suppliers names
        public List<string> GetAllSupplierNames()
        {
            List<string> lsTempList = new List<string>();

            for (int i = 0; i < m_lSupplierList.Count; ++i)
                lsTempList.Add(m_lSupplierList[i].GetSupplierName());
            return lsTempList;
        }
    }
}
