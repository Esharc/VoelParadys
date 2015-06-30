using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    class VoelParadysSuppliersData
    {
        List<VoelParadysDataStructures.CSupplierDetails> m_lSupplierList;

        public VoelParadysSuppliersData()
        {
            m_lSupplierList = new List<VoelParadysDataStructures.CSupplierDetails>();
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
        public List<VoelParadysDataStructures.CSupplierDetails> GetSupplierList()
        {
            return m_lSupplierList;
        }
        // Add a new customer to the database
        public void AddNewSupplierToList(VoelParadysDataStructures.CSupplierDetails theNewSupplier)
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
        public VoelParadysDataStructures.CSupplierDetails GetSupplierAt(int iIndex)
        {
            return m_lSupplierList[iIndex];
        }
        // Get the customer from a given ID
        public VoelParadysDataStructures.CSupplierDetails GetSupplierFromID(int iSupplierID)
        {
            for (int i = 0; i < m_lSupplierList.Count; ++i)
            {
                if (m_lSupplierList[i].GetSupplierID() == iSupplierID)
                    return m_lSupplierList[i];
            }
            return new VoelParadysDataStructures.CSupplierDetails(-1, "-1", "-1", "-1", new string[5] { "-1", "-1", "-1", "-1", "-1" }, "-1");
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
        public void UpdateSupplierDetails(VoelParadysDataStructures.CSupplierDetails UpdatedDetails)
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
