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
    public partial class Form15 : Form
    {
        Form3 fm;
        public Form15(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            getorder();
        }

        private void getorder()
        {
            string containers = "";
            if (textBox4.ReadOnly)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 1:
                        containers = "hp";
                        break;
                    case 2:
                        containers = "atk";
                        break;
                    case 3:
                        containers = "def";
                        break;
                    case 4:
                        containers = "mdef";
                        break;
                    case 5:
                        containers = "exp";
                        break;
                    case 6:
                        containers = "gold";
                        break;
                }
            }
            else
            {
                containers = textBox4.Text;
            }
            fm.evOrder = string.Format("condition~[{0}]~{1}~{2}~{3}~{4}", containers, comboBox2.SelectedItem, textBox1.Text, textBox2.Text, textBox3.Text);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox4.ReadOnly = false;
            }
            else
            {
                textBox4.ReadOnly = true;
            }
            getorder();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            getorder();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getorder();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            getorder();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            getorder();
        }
    }
}
