using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoltageControl.src
{
    public partial class MultiSendConfirmDiag : Form
    {
        public MultiSendConfirmDiag(string showstring)
        {
            InitializeComponent();
            richTextBox1.Text = showstring;
            // init return value
            this.DialogResult = DialogResult.Cancel;
        }

        private void ConfirmSendData(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelSendData(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
