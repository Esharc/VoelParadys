using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    public class CCustomerDetails : IEquatable<CCustomerDetails>, IComparable<CCustomerDetails>
    {
        int iCustomerID;                            // The ID of the customer (Unique and auto generated)
        string sCustomerName;                       // The name of the customer (Required)
        string sCustomerSurname;                    // The surname of the customer (Optional)
        string sCustomerAddress;                    // The address of the customer (Optional)
        string sCustomerPhoneNumber;                // The phone number of the customer (Optional)   
        long lCustomerIDNumber;                     // The ID number of the customer (Optional)
        List<string> lsCustomerWishList;            // A list of items that the customer has requested             

        public CCustomerDetails()
        {
            lsCustomerWishList = new List<string>();
            iCustomerID = -1;
            sCustomerName = "-1";
            sCustomerSurname = "-1";
            sCustomerAddress = "-1";
            sCustomerPhoneNumber = "-1";
            lCustomerIDNumber = -1;
        }

        public CCustomerDetails(int _iCode, string _sName, string _sSurname, string _sAddress, string _sPhoneNumber, long _lIdNumber)
        {
            lsCustomerWishList = new List<string>();
            iCustomerID = _iCode;
            sCustomerName = _sName;
            sCustomerSurname = _sSurname;
            sCustomerAddress = _sAddress;
            sCustomerPhoneNumber = _sPhoneNumber;
            lCustomerIDNumber = _lIdNumber;
        }

        // Getters
        public int GetCustomerID() { return iCustomerID; }
        public string GetCustomerName() { return sCustomerName; }
        public string GetCustomerSurname() { return sCustomerSurname; }
        public string GetCustomerAddress() { return sCustomerAddress; }
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
        public void SetCustomerAddress(string sAddress) 
        {
            string sTheAddress = sAddress == null ? "" : sAddress;
            sCustomerAddress = sTheAddress; 
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

    public class VoelParadysCustomerData
    {
        List<CCustomerDetails> m_lCustomerList;

        public VoelParadysCustomerData()
        {
            m_lCustomerList = new List<CCustomerDetails>();
        }

        private bool HasIDBeenUsed(int iCustomerID)
        {
            // Here we want to check if the ID has been used in the database. So that if a customer is deleted, that ID can be reused for a new customer
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (iCustomerID == m_lCustomerList[i].GetCustomerID())
                    return true;
            }
            return false;
        }

        public void GetUniqueCustomerID(ref int iUniqueId)
        {
            int iTempID = -1;

            if (m_lCustomerList.Count == 0)
            {
                iUniqueId = 1;
                return;
            }

            iTempID = m_lCustomerList[0].GetCustomerID()+1;

            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (HasIDBeenUsed(iTempID))
                {
                    if (m_lCustomerList[i].GetCustomerID() > iTempID)
                        iTempID = m_lCustomerList[i].GetCustomerID() + 1;
                }
                else
                    break;
            }
            iUniqueId = iTempID;
        }
        // This function should only be used by the controller to populate the list
        public List<CCustomerDetails> GetCustomerList()
        {
            return m_lCustomerList;
        }
        // Add a new customer to the database
        public void AddNewCustomerToList(CCustomerDetails theNewCustomer)
        {
            m_lCustomerList.Add(theNewCustomer);
            VoelParadysDataController.GetInstance().WriteCustomerDataToDB();
        }
        // Get the number of customers currently in the database
        public int GetCustomerListSize()
        {
            return m_lCustomerList.Count;
        }
        // Get a customer at a given index
        public CCustomerDetails GetCustomerAt(int iIndex)
        {
            return m_lCustomerList[iIndex];
        }
        // Get the customer from a given ID
        public CCustomerDetails GetCustomerFromID(int iCustomerID)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerID() == iCustomerID)
                    return m_lCustomerList[i];
            }
            return new CCustomerDetails(-1, "-1", "-1", "-1", "-1", -1);
        }
        // Determine if a customer exists in the database with the given ID
        public bool DoesCustomerExistInDatabase(int iCustomerID)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerID() == iCustomerID)
                    return true;
            }
            return false;
        }
        // Update the customers details with the given details
        public void UpdateCustomerDetails(CCustomerDetails UpdatedDetails)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerID() == UpdatedDetails.GetCustomerID())
                {
                    m_lCustomerList.RemoveAt(i);
                    m_lCustomerList.Add(UpdatedDetails);
                    VoelParadysDataController.GetInstance().WriteCustomerDataToDB();
                }
            }
        }
        // Delete a customer from the database
        public void DeleteCustomerFromDatabase(int iCustomerID)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerID() == iCustomerID)
                {
                    m_lCustomerList.RemoveAt(i);
                    VoelParadysDataController.GetInstance().WriteCustomerDataToDB();
                }
            }
        }
        // Sort the customer list by ID
        public void SortCustomerList()
        {
            m_lCustomerList.Sort();
        }
        // Get a customers ID given his name
        public int GetCustomerIDFromName(string sName)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerName() == sName)
                    return m_lCustomerList[i].GetCustomerID();
            }
            return -1;
        }
        // Get a list of all the customers names
        public List<string> GetAllCustomerNames()
        {
            List<string> lsTempNames = new List<string>();
            for (int i = 0; i < m_lCustomerList.Count; ++i)
                lsTempNames.Add(m_lCustomerList[i].GetCustomerName());
            return lsTempNames;
        }
    }
}
