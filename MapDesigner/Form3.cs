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
                    currentForm = new Form23(this);
                    break;
                case 3:
                    currentForm = new Form6(this);
                    break;
                case 4:
                    currentForm = new Form7(this);
                    break;
                case 5:
                    currentForm = new Form8(this);
                    break;
                case 6:
                    currentForm = new Form9(this);
                    break;
                case 7:
                    currentForm = new Form10(this);
                    break;
                case 8:
                    currentForm = new Form11(this);
                    break;
                case 9:
                    currentForm = new Form12(this);
                    break;
                case 10:
                    currentForm = new Form13(this);
                    break;
                case 11:
                    currentForm = new Form14(this);
                    break;
                case 12:
                    currentForm = new Form15(this);
                    break;
                case 13:
                    currentForm = new Form16(this);
                    break;
                case 14:
                    currentForm = new Form17(this);
                    break;
                case 15:
                    currentForm = new Form18(this);
                    break;
                case 16:
                    currentForm = new Form19(this);
                    break;
                case 17:
                    currentForm = new Form20(this);
                    break;
                case 18:
                    currentForm = new Form21(this);
                    break;
                case 19:
                    currentForm = new Form22(this);
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
