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
    public partial class Form16 : Form
    {
        Form3 fm;
        public Form16(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("switch({0})", textBox1.Text);
        }
    }
}
