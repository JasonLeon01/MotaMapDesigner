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
    public partial class Form22 : Form
    {
        Form3 fm;
        public Form22(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "ending";
            InitializeComponent();
        }
    }
}
