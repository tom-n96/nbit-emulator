using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class MessagePopup : Form
    {
        private String message;
        public MessagePopup(String message)
        {
            this.message = message;
            InitializeComponent();
        }

        private void MessagePopup_Load(object sender, EventArgs e)
        {
            errormessage.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
