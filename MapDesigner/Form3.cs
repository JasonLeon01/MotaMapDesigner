using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapDesigner
{
    public partial class Form3 : Form
    {
        public string evOrder { get; set; }
        public bool finished;
        Form currentForm;
        public Form3()
        {
            finished = false;
            evOrder = "";
            currentForm = null;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            finished = true;
            this.Close();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
                panel1.Controls.Remove(currentForm);
            }
            bool flag = true;
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    currentForm = new Form4(this);
                    break;
                case 1:
                    currentForm = new Form5(this);
                    break;
                case 2:
                    currentForm = new Form6(this);
                    break;
                default:
                    flag = false;
                    break;
            }
            if (flag)
            {
                currentForm.TopLevel = false;
                currentForm.Dock = DockStyle.Fill;
                panel1.Controls.Add(currentForm);
                currentForm.Show();
            }
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finished)
                evOrder = "";
        }
    }
}
