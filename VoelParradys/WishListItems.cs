using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoelParadys
{
    public partial class WishListItems : Form
    {
        List<string> m_WishListItems;
        Orders m_Parent;

        public WishListItems(Orders theParent, List<string> theWishList)
        {
            InitializeComponent();
            m_Parent = theParent;
            m_WishListItems = theWishList;
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            for (int i = 0; i < m_WishListItems.Count; ++i)
                WishListBox.Items.Add(m_WishListItems[i]);
        }

        private void TheCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TheAcceptButton_Click(object sender, EventArgs e)
        {
            List<string> sTempList = new List<string>();

            for (int i = 0; i < WishListBox.SelectedItems.Count; ++i)
                sTempList.Add(WishListBox.SelectedItems[i].ToString());

            m_Parent.SetWishListItems(sTempList);
            this.Close();
        }
    }
}
