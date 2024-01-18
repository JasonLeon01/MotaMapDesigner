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
    public partial class Form5 : Form
    {
        Form3 fm;
        int kind;
        string val;
        public Form5(Form3 fm)
        {
            kind = -1;
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            kind = 0;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            kind = 1;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            kind = 2;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            kind = 3;
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            kind = 4;
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            kind = 5;
        }
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            kind = 6;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            val = textBox1.Text;
            if (kind != -1)
            {
                fm.evOrder = string.Format("bonus({0},{1})", kind, val);
            }
        }
    }
}
