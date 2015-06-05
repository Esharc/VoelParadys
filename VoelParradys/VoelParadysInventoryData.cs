using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoelParadys
{
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

    class VoelParadysInventoryData
    {
        private List<SStockItemDetails> m_lStockItems;

        public VoelParadysInventoryData()
        {
            m_lStockItems = new List<SStockItemDetails>();
        }

        public int GetStockListSize()
        {
            return m_lStockItems.Count;
        }
        public void AddNewStockItemToList(string sCode, string sName, int iQuantity, float fBuyPrice, float fSellPrice)
        {
            SStockItemDetails pStockDetails = new SStockItemDetails();
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
        public SStockItemDetails GetStockItem(string sCode)
        {
            for (int i = 0; i < m_lStockItems.Count; ++i)
            {
                if (m_lStockItems[i].GetStockItemCode() == sCode)
                {
                    return m_lStockItems[i];
                }
            }
            return new SStockItemDetails();
        }
        public SStockItemDetails GetStockItemAt(int iIndex)
        {
            if (iIndex < m_lStockItems.Count)
                return m_lStockItems[iIndex];
            return new SStockItemDetails();
        }
        public void UpdateStockItem(SStockItemDetails UpdatedDetails)
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
        public List<SStockItemDetails> GetStockItemList()
        {
            return m_lStockItems;
        }
    }
}
