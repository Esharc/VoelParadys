using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    public class VoelParadysCustomerData
    {
        List<VoelParadysDataStructures.CCustomerDetails> m_lCustomerList;

        public VoelParadysCustomerData()
        {
            m_lCustomerList = new List<VoelParadysDataStructures.CCustomerDetails>();
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
                    if (m_lCustomerList[i].GetCustomerID() >= iTempID)
                        iTempID = m_lCustomerList[i].GetCustomerID() + 1;
                }
                else
                    break;
            }
            iUniqueId = iTempID;
        }
        // This function should only be used by the controller to populate the list
        public List<VoelParadysDataStructures.CCustomerDetails> GetCustomerList()
        {
            return m_lCustomerList;
        }
        // Add a new customer to the database
        public void AddNewCustomerToList(VoelParadysDataStructures.CCustomerDetails theNewCustomer)
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
        public VoelParadysDataStructures.CCustomerDetails GetCustomerAt(int iIndex)
        {
            return m_lCustomerList[iIndex];
        }
        // Get the customer from a given ID
        public VoelParadysDataStructures.CCustomerDetails GetCustomerFromID(int iCustomerID)
        {
            for (int i = 0; i < m_lCustomerList.Count; ++i)
            {
                if (m_lCustomerList[i].GetCustomerID() == iCustomerID)
                    return m_lCustomerList[i];
            }
            return new VoelParadysDataStructures.CCustomerDetails(-1, "-1", "-1", new string[5] { "-1", "-1", "-1", "-1", "-1" }, "-1", -1);
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
        public void UpdateCustomerDetails(VoelParadysDataStructures.CCustomerDetails UpdatedDetails)
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
        public List<VoelParadysDataStructures.CIntStringMap> GetAllCustomersNameAndID()
        {
            List<VoelParadysDataStructures.CIntStringMap> lsTempNameID = new List<VoelParadysDataStructures.CIntStringMap>();
            for (int i = 0; i < m_lCustomerList.Count; ++i)
                lsTempNameID.Add(new VoelParadysDataStructures.CIntStringMap(m_lCustomerList[i].GetCustomerID(), m_lCustomerList[i].GetCustomerName()));
            return lsTempNameID;
        }
    }
}
