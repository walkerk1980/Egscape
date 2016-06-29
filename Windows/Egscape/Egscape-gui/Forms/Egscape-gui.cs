using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Egscape_gui
{
    public partial class EgscapeMainForm : Form
    {
        public EgscapeMainForm()
        {
            InitializeComponent();
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            Egscape eg = new Egscape();
            if (eg.InputIsNotNullOrVoid(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text))
            {
                eg.RunScan(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text);
                
                //MessageBox.Show("True");
            }
            else
            {
                //MessageBox.Show("False");
            }
            
        }
    }
}
