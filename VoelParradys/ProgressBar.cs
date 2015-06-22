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
    public partial class ProgressBar : Form
    {
        public ProgressBar(int iMinimum, int iMaximum, int iStepSize = 1)
        {
            InitializeComponent();
            VPProgressBar.Minimum = iMinimum;
            VPProgressBar.Maximum = iMaximum;
            VPProgressBar.Value = iMinimum;
            VPProgressBar.Step = iStepSize;
        }

        public void UpdateProgressBar()
        {
            VPProgressBar.PerformStep();
        }
        public void CloseProgressBar()
        {
            this.Close();
        }
    }
}
