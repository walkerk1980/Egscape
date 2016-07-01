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
            if (String.Equals(scanButton.Text,"Scan"))
            {
                if (eg.InputIsNotNullOrVoid(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text))
                {
<<<<<<< HEAD
                    scanButton.Text = "Scanning...";
                    eg.RunScan(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text);
=======
                    scanButton.Text = "Cancel Scan...";
                    Thread runScanThread = new Thread(() => eg.RunScan(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text));
                    runScanThread.Start();
                    //eg.RunScan(protocolComboBox.Text, hostTextBox.Text, portTextBox.Text, portTypeComboBox.Text);
>>>>>>> 02b1bee3b9dc22d5d2dbf315a55ecd708b33470f
                    //MessageBox.Show("True");
                }
                else
                {
                    //MessageBox.Show("False");
                }
            }
            else
            {
                MessageBox.Show("Scan will be cancelled when you press OK.");
                scanButton.Text = "Scan";
            }

            
        }

    }
}
