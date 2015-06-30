using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
    class VoelParadysInventoryData
    {
        private List<VoelParadysDataStructures.SStockItemDetails> m_lStockItems;

        public VoelParadysInventoryData()
        {
            m_lStockItems = new List<VoelParadysDataStructures.SStockItemDetails>();
        }

        public int GetStockListSize()
        {
            return m_lStockItems.Count;
        }
        public void AddNewStockItemToList(string sCode, string sName, int iQuantity, float fBuyPrice, float fSellPrice)
        {
            VoelParadysDataStructures.SStockItemDetails pStockDetails = new VoelParadysDataStructures.SStockItemDetails();
            pStockDetails.SetStockItemCode(sCode);
            pStockDetails.SetStockItemName(sName);
            pStockDetails.SetStockItemQuantityBought(iQuantity);
            pStockDetails.SetStockItemQuantitySold(0);
            pStockDetails.SetStockItemQuantityUsed(0);
            pStockDetails.SetStockItemCostPrice(fBuyPrice);
            pStockDetails.SetStockItemSellPrice(fSellPrice);

            m_lStockItems.Add(pStockDetails);
            VoelParadysDataController.GetInstance().WriteStockDataToDB(m_lStockItems);
        }
        public bool DoesItemExistInDatabase(string sItemCode)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemCode() == sItemCode)
                    return true;
            }
            return false;
        }
        public bool DoesItemExistInDatabase(string sName, ref string rsItemCode)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemName() == sName)
                {
                    rsItemCode = m_lStockItems[i].GetStockItemCode();
                    return true;
                }
            }
            return false;
        }
        public VoelParadysDataStructures.SStockItemDetails GetStockItem(string sCode)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemCode() == sCode)
                {
                    return m_lStockItems[i];
                }
            }
            return new VoelParadysDataStructures.SStockItemDetails();
        }
        public VoelParadysDataStructures.SStockItemDetails GetStockItemAt(int iIndex)
        {
            if (iIndex < m_lStockItems.Count)
                return m_lStockItems[iIndex];
            return new VoelParadysDataStructures.SStockItemDetails();
        }
        public void UpdateStockItem(VoelParadysDataStructures.SStockItemDetails UpdatedDetails)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemCode() == UpdatedDetails.GetStockItemCode())
                {
                    m_lStockItems.RemoveAt(i);
                    m_lStockItems.Add(UpdatedDetails);
                    VoelParadysDataController.GetInstance().WriteStockDataToDB(m_lStockItems);
                }
            }
        }
        public void DeleteStockItem(string sItemCode)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemCode() == sItemCode)
                {
                    m_lStockItems.RemoveAt(i);
                    VoelParadysDataController.GetInstance().WriteStockDataToDB(m_lStockItems);
                }
            }
        }
        // This function should only be used by the controller to populate the list
        public List<VoelParadysDataStructures.SStockItemDetails> GetStockItemList()
        {
            return m_lStockItems;
        }
    }
}
