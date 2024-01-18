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
    public partial class Form8 : Form
    {
        Form3 fm;
        int shopid;
        public Form8(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            this.shopid = -1;
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("shop({0},{1},{2}", shopid.ToString(), textBox1.Text, textBox2.Text);
            if (textBox3.Text.Length > 0 && int.Parse(textBox3.Text) != 0)
            {
                fm.evOrder += "," + textBox3.Text;
            }
            fm.evOrder += ")";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                shopid = 0;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                shopid = 1;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = "shop/" + shopid.ToString() + "/" + textBox1.Text + "/" + textBox2.Text;
            if (textBox3.Text.Length > 0 && int.Parse(textBox3.Text) != 0)
            {
                fm.evOrder += "/" + textBox3.Text;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = "shop/" + shopid.ToString() + "/" + textBox1.Text + "/" + textBox2.Text;
            if (textBox3.Text.Length > 0 && int.Parse(textBox3.Text) != 0)
            {
                fm.evOrder += "/" + textBox3.Text;
            }
        }
    }
}
