using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MapDesigner
{
    public partial class Form6 : Form
    {
        Form3 fm;
        public Form6(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("door({0})", textBox1.Text);
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "1";
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "2";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "3";
        }
    }
}
