using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapDesigner
{
    public partial class Form2 : Form
    {
        public string dsl { get; set; }
        public List<string> dslarray;
        bool finished;
        public Form2()
        {
            dsl = "";
            dslarray = new List<string>();
            finished = false;
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dsl = textBox1.Text;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = dsl;
            if (dsl != "")
            {
                string pattern = @"[^(;]+(?:\([^()]*\))?;";
                MatchCollection matches = Regex.Matches(dsl, pattern);
                foreach (Match match in matches)
                {
                    string command = match.Value.TrimEnd(';');
                    listBox1.Items.Add(command);
                }
            }
        }
        private void refreshList()
        {
            listBox1.Items.Clear();
            foreach (string s in dslarray)
                listBox1.Items.Add(s);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            if (form3.evOrder != "")
            {
                if (listBox1.SelectedIndex != -1)
                    dslarray.Insert(listBox1.SelectedIndex + 1, form3.evOrder);
                else
                    dslarray.Add(form3.evOrder);
                textBox1.ReadOnly = false;
                textBox1.Text = string.Join(";", dslarray);
                if (textBox1.Text[^1] != ';')
                {
                    textBox1.Text += ";";
                }
                textBox1.ReadOnly = true;
                refreshList();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select an event to edit.");
                return;
            }
            Form3 form3 = new Form3();
            form3.ShowDialog();
            if (form3.evOrder != "")
            {
                dslarray[listBox1.SelectedIndex] = form3.evOrder;
                textBox1.ReadOnly = false;
                textBox1.Text = string.Join(";", dslarray);
                if (textBox1.Text[^1] != ';')
                {
                    textBox1.Text += ";";
                }
                textBox1.ReadOnly = true;
                refreshList();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select an event to delete.");
                return;
            }
            dslarray.RemoveAt(listBox1.SelectedIndex);
            refreshList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            finished = true;
            this.Close();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finished)
                dsl = "";
        }
    }
}
