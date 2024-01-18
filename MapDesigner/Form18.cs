using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MapDesigner
{
    public partial class Form18 : Form
    {
        Form3 fm;
        public Form18(Form3 fm)
        {
            this.fm = fm;
            this.fm.evOrder = "";
            InitializeComponent();
            this.comboBox1.Items.Clear();
            loadAnimation();
        }
        private void loadAnimation()
        {
            int idx = 0;
            string path = Application.StartupPath + @"..\data\animation\";
            while (File.Exists(path + @"animation_" + idx.ToString() + ".json"))
            {
                string file = @"animation_" + idx.ToString() + ".json";
                string jsonstr = System.IO.File.ReadAllText(path + file);
                Animation tempani = JsonSerializer.Deserialize<Animation>(jsonstr);
                comboBox1.Items.Add(idx.ToString().PadLeft(3, '0') + "：" + tempani.name);
                ++idx;
            }
            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("未检测到动画数据！");
                Application.Exit();
                return;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fm.evOrder = string.Format("animate({0},{1})", comboBox1.SelectedIndex.ToString(), textBox1.Text);
        }
    }
}

class Animation
{
    public string name { get; set; }
    public string SEFile { get; set; }
    public List<string> patterns { get; set; }
    public int SETime { get; set; }
    public Animation()
    {
        name = "";
        patterns = new List<string>();
        SEFile = "";
        SETime = 0;
    }
    public Animation(string name, List<string> patterns, string SEFile, int SETime)
    {
        this.name = name;
        this.SEFile = SEFile;
        this.patterns = patterns;
        this.SETime = SETime;
    }
}