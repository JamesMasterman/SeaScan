using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeaScanUAV
{
    public partial class frmApplicationProperties : Form
    {
        public frmApplicationProperties()
        {
            InitializeComponent();
        }

        public void SetProperties(ApplicationPropertyManager props)
        {
            propSettings.SelectedObject = props;
        }        

        private void cmdOK_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
