using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class Splash : Form
    {
        private Form mainWind;
        public Splash(Form mainW)
        {
            InitializeComponent();
            mainWind = mainW;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            mainWind.Visible = true;
            //this.Close();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void disclaimer_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
