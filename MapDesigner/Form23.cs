﻿using System;
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
    public partial class Form23 : Form
    {
        Form3 fm;
        public Form23(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("item({0},{1})", textBox1.Text, textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("item({0},{1})", textBox1.Text, textBox2.Text);
        }
    }
}
