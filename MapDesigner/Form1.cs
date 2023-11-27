using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace MapDesigner
{
    public partial class Form1 : Form
    {
        int screenX, screenY, copyEvID, nowMapID, gameTime, pos1, pos2;
        string file;
        List<string> mapName, bgmFile;
        bool redraw, mouseDown;
        List<List<ProjectEvent>> mapEvents;
        Pen mypen;
        Bitmap buffer1, buffer2;
        MouseEventArgs me;
        Graphics g1, g2, g3;
        Image evimg;
        public Form1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ArrayList a = new ArrayList();
            screenX = 0;
            screenY = 0;
            copyEvID = -1;
            nowMapID = 0;
            gameTime = 0;
            pos1 = 0;
            pos2 = 0;
            file = "";
            mapName = new List<string>();
            bgmFile = new List<string>();
            redraw = false;
            mouseDown = false;
            mapEvents = new List<List<ProjectEvent>>();
            InitializeComponent();
            //采用双缓冲技术的控件必需的设置
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.timer1.Start();
            mypen = new Pen(Color.White, 3);
        }
        private void loadMapData()
        {
            int idx = 0;
            bool flag = false;
            while (File.Exists("..\\data\\map\\map_" + idx.ToString() + ".json"))
            {
                file = "map_" + idx.ToString() + ".json";
                string jsonstr = System.IO.File.ReadAllText("..\\data\\map\\" + file);
                Dictionary<string, object> mapdata = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonstr);
                mapName.Add(mapdata["mapName"].ToString());
                bgmFile.Add(mapdata["bgmFile"].ToString());
                string evstr = mapdata["mapEvents"].ToString();
                List<ProjectEvent> temp = JsonSerializer.Deserialize<List<ProjectEvent>>(evstr);
                foreach (var ev in temp)
                {
                    if (!File.Exists("..\\graphics\\character\\" + ev.file))
                    {
                        ev.file = "无";
                        flag = true;
                    }
                }
                mapEvents.Add(temp);
                listBox1.Items.Add(idx.ToString().PadLeft(3, '0') + "：" + mapName[idx]);
                ++idx;
            }
            nowMapID = mapEvents.Count - 1;
            listBox1.SelectedIndex = nowMapID;
            if (nowMapID < 0)
            {
                MessageBox.Show("文件夹中没有可用地图数据");
                Application.Exit();
            }
            if (flag)
                MessageBox.Show("有部分事件的图片文件不存在，请检查！");
        }
        void saveMapData(string savefile = "")
        {
            if (savefile == "") savefile = label2.Text;
            OpenFileDialog ofg = new OpenFileDialog();
            if (!Directory.Exists("..\\data\\map"))
            {
                //创建路径
                Directory.CreateDirectory("..\\data\\map");
            }
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                IncludeFields = true,
                WriteIndented = true
            };
            Dictionary<string, object> savedata = new Dictionary<string, object>();
            savedata.Add("mapName", mapName[listBox1.SelectedIndex]);
            savedata.Add("bgmFile", bgmFile[listBox1.SelectedIndex]);
            savedata.Add("mapEvents", mapEvents[listBox1.SelectedIndex]);
            string jsonstr = JsonSerializer.Serialize(savedata, options);
            System.IO.File.WriteAllText("..\\data\\map\\" + savefile, jsonstr);
            MessageBox.Show(savefile + "保存成功");
        }
        private void drawMapEvents()
        {
            if (nowMapID < 0) return;

            int picsiz = pictureBox1.Width / 4;
            if (redraw)
            {
                if (label6.Text != "" && label6.Text != "无")
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    pictureBox1.Image = Image.FromFile("..\\graphics\\character\\" + label6.Text);
                }
                redraw = false;
            }
            if (label6.Text != "" && label6.Text != "无")
            {
                if (pictureBox2.Image != null)
                {
                    pictureBox2.Image.Dispose();
                    pictureBox2.Image = null;
                }
                if (buffer1 != null)
                {
                    buffer1.Dispose();
                    buffer1 = null;
                }
                buffer1 = new Bitmap(32, 32);
                if (g2 != null)
                {
                    g2.Dispose();
                    g2 = null;
                }
                g2 = Graphics.FromImage(buffer1);
                g2.DrawImage(pictureBox1.Image, new Rectangle(0, 0, 32, 32), new Rectangle(32 * ((pos1 + gameTime * Convert.ToInt32(checkBox1.Checked)) % 4), pos2 * 32, 32, 32), GraphicsUnit.Pixel);
                pictureBox2.Image = buffer1;
            }
            if (buffer2 != null)
            {
                buffer2.Dispose();
                buffer2 = null;
            }
            buffer2 = new Bitmap(352, 352);
            if (g3 != null)
            {
                g3.Dispose();
                g3 = null;
            }
            g3 = Graphics.FromImage(buffer2);
            g3.Clear(this.BackColor);
            g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g3.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            int cnt = 0;
            Image floorimg = Image.FromFile("..\\graphics\\system\\floor.png");
            g3.DrawImage(floorimg, 0, 0, 352, 352);
            floorimg.Dispose();
            foreach (ProjectEvent ev in mapEvents[nowMapID])
            {
                if (evimg != null)
                {
                    evimg.Dispose();
                    evimg = null;
                }
                if (ev.file != "" && ev.file != "无")
                {
                    evimg = Image.FromFile("..\\graphics\\character\\" + ev.file);
                    g3.DrawImage(evimg, new Rectangle(ev.x * 32, ev.y * 32, 32, 32), new Rectangle(32 * ((ev.pos[0] + gameTime * Convert.ToInt32(ev.move)) % 4), 32 * ev.pos[1], 32, 32), GraphicsUnit.Pixel);
                }
                if (!checkBox3.Checked)
                    g3.DrawString(cnt.ToString(), new Font("Arial", 12), new SolidBrush(Color.White), new Point(ev.x * 32, ev.y * 32));
                cnt++;
            }
            if (!checkBox3.Checked)
                g3.DrawRectangle(mypen, screenX * 32 - 2, screenY * 32 - 2, 36, 36);
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose();
                pictureBox3.Image = null;
            }
            pictureBox3.Image = buffer2;
            GC.Collect();
        }
        private void refreshList()
        {
            listBox1.Items.Clear();
            int idx = 0;
            foreach (string str in mapName)
            {
                listBox1.Items.Add(idx.ToString().PadLeft(3, '0') + "：" + mapName[idx]);
                idx++;
            }
            listBox1.SelectedIndex = nowMapID;
        }
        private bool haveAnEvent(int x, int y)
        {
            if (mapEvents[nowMapID].Count == 0) return false;
            foreach (ProjectEvent ev in mapEvents[nowMapID])
                if (ev.x == x && ev.y == y)
                    return true;
            return false;
        }
        private ProjectEvent getCertainEvent(int x, int y)
        {
            foreach (ProjectEvent ev in mapEvents[nowMapID])
                if (ev.x == x && ev.y == y)
                    return ev;
            return new ProjectEvent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadMapData();
            this.label2.Text = file;
            this.label2.Refresh();
            this.textBox1.Text = mapName[nowMapID];
            this.textBox6.Text = bgmFile[nowMapID];
            drawMapEvents();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ++gameTime;
            if (mouseDown) MouseTrigger(sender, e);
            drawMapEvents();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mapName[nowMapID] = textBox1.Text;
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            bgmFile[nowMapID] = textBox6.Text;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point thispoint = pictureBox1.PointToClient(Control.MousePosition);
            int picsize = pictureBox1.Width / 4;
            if (me.Button == MouseButtons.Left)
            {
                redraw = true;
                pos1 = thispoint.X / picsize;
                pos2 = thispoint.Y / picsize;
            }
            else if (me.Button == MouseButtons.Right)
            {
                //创建对象
                OpenFileDialog ofg = new OpenFileDialog();
                //设置默认打开路径
                ofg.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath)) + "\\graphics\\character";
                ofg.RestoreDirectory = true;
                //设置打开标题、后缀
                ofg.Title = "请选择导入png文件";
                ofg.Filter = "png文件|*.png";
                string path = "";
                if (ofg.ShowDialog() == DialogResult.OK)
                {
                    //得到打开的文件路径（包括文件名）
                    string[] names = ofg.FileName.ToString().Split('\\');
                    label6.Text = names[names.Length - 1];
                    label6.Refresh();
                    redraw = true;
                }
                else
                    MessageBox.Show("未选择打开文件！");
            }
            drawMapEvents();
        }
        private void MouseTrigger(object sender, EventArgs e)
        {
            Point thispoint = pictureBox3.PointToClient(Control.MousePosition);
            if (thispoint.X > pictureBox3.Width || thispoint.Y > pictureBox3.Height || thispoint.X < 0 || thispoint.Y < 0) return;
            screenX = Math.Max(0, Math.Min(10, thispoint.X / (pictureBox3.Width / 11)));
            screenY = Math.Max(0, Math.Min(10, thispoint.Y / (pictureBox3.Height / 11)));
            label10.Text = "(" + screenX.ToString() + "," + screenY.ToString() + ")";
            label10.Refresh();
            if (me.Button == MouseButtons.Right)
                button2_Click(sender, e);
            else if (me.Button == MouseButtons.Middle)
                button3_Click(sender, e);
            else if (me.Button == MouseButtons.Left)
                button4_Click(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, (string, int, int)> corres = new Dictionary<string, (string, int, int)>();
            if (File.Exists("DesignerReferrence.txt"))
            {
                string extradatatext = System.IO.File.ReadAllText("DesignerReferrence.txt");
                string[] extradata = extradatatext.Split(Environment.NewLine.ToCharArray());
                extradata = extradata.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                foreach (string str in extradata)
                {
                    string s1 = str.Split(':')[0], s2 = str.Split(':')[1];
                    string[] s3 = s2.Split(',');
                    corres.Add(s1, (s3[0], int.Parse(s3[1]), int.Parse(s3[2])));
                }
            }
            if (textBox2.Text.Split('/')[0] == "monster")
            {
                if (File.Exists("..\\data\\enemy\\enemy_" + textBox2.Text.Split('/')[1] + ".json"))
                {
                    string jsonstr = System.IO.File.ReadAllText("..\\data\\enemy\\enemy_" + textBox2.Text.Split('/')[1] + ".json");
                    Dictionary<string, object> enemydata = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonstr);
                    label6.Text = enemydata["file"].ToString();
                    label6.Refresh();
                    pos1 = 0;
                    pos2 = int.Parse(enemydata["pos"].ToString());
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                    redraw = true;
                }
                else
                {
                    MessageBox.Show("没有这号怪物");
                    return;
                }
            }
            else if (textBox2.Text.Split('/')[0] == "item")
            {
                if (File.Exists("..\\data\\item\\item_" + textBox2.Text.Split('/')[1] + ".json"))
                {
                    string jsonstr = System.IO.File.ReadAllText("..\\data\\item\\item_" + textBox2.Text.Split('/')[1] + ".json");
                    Dictionary<string, object> itemdata = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonstr);
                    label6.Text = itemdata["file"].ToString();
                    label6.Refresh();
                    int[] tmparr = JsonSerializer.Deserialize<int[]>(itemdata["pos"].ToString());
                    pos1 = tmparr[0];
                    pos2 = tmparr[1];
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    redraw = true;
                }
                else
                {
                    MessageBox.Show("没有这号道具");
                    return;
                }
            }
            else
            {
                if (corres.Count > 0)
                {
                    if (corres.ContainsKey(textBox2.Text))
                    {
                        (label6.Text, pos1, pos2) = corres[textBox2.Text];
                        label6.Refresh();
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        redraw = true;
                    }
                    else
                    {
                        MessageBox.Show("未找到支持的类型");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("未找到支持的类型");
                    return;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!haveAnEvent(screenX, screenY))
            {
                if (textBox2.Text == "" || label6.Text == "" || label6.Text == "无")
                    return;
                mapEvents[nowMapID].Add(new ProjectEvent(mapEvents[nowMapID].Count, textBox2.Text, label6.Text, new int[] { pos1, pos2 }, screenX, screenY, new int[] { int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text) }, checkBox1.Checked, checkBox2.Checked));
                copyEvID = mapEvents[nowMapID].Count - 1;
                label8.Text = copyEvID.ToString();
            }
            else
                button5_Click(sender, e);
            drawMapEvents();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (mapEvents[nowMapID].Count == 0) return;
            if (haveAnEvent(screenX, screenY))
            {
                ProjectEvent temp = getCertainEvent(screenX, screenY);
                textBox2.Text = temp.name;
                label6.Text = temp.file;
                label6.Refresh();
                textBox3.Text = temp.triggerCondition[0].ToString();
                textBox4.Text = temp.triggerCondition[1].ToString();
                textBox5.Text = temp.triggerCondition[2].ToString();
                checkBox1.Checked = Convert.ToBoolean(temp.move);
                checkBox2.Checked = Convert.ToBoolean(temp.through);
                pos1 = temp.pos[0];
                pos2 = temp.pos[1];
                copyEvID = mapEvents[nowMapID].IndexOf(temp);
                label8.Text = copyEvID.ToString();
                redraw = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (haveAnEvent(screenX, screenY))
            {
                mapEvents[nowMapID].Remove(getCertainEvent(screenX, screenY));
                if (copyEvID >= mapEvents[nowMapID].Count)
                {
                    copyEvID = mapEvents[nowMapID].Count - 1;
                    label8.Text = copyEvID.ToString();
                    label8.Refresh();
                }
                drawMapEvents();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (copyEvID == -1)
            {
                MessageBox.Show("未复制任何事件！");
                return;
            }
            if (haveAnEvent(screenX, screenY))
            {
                MessageBox.Show("此处已有事件！");
                return;
            }
            mapEvents[nowMapID][copyEvID].x = screenX;
            mapEvents[nowMapID][copyEvID].y = screenY;
            drawMapEvents();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (!haveAnEvent(screenX, screenY))
                MessageBox.Show("此处没有事件！");
            else
            {
                if (textBox2.Text == "" || label6.Text == "" || label6.Text == "无")
                    return;
                ProjectEvent temp = getCertainEvent(screenX, screenY);
                int tempid = mapEvents[nowMapID].IndexOf(temp);
                mapEvents[nowMapID][tempid].name = textBox2.Text;
                mapEvents[nowMapID][tempid].file = label6.Text;
                mapEvents[nowMapID][tempid].triggerCondition[0] = int.Parse(textBox3.Text);
                mapEvents[nowMapID][tempid].triggerCondition[1] = int.Parse(textBox4.Text);
                mapEvents[nowMapID][tempid].triggerCondition[2] = int.Parse(textBox5.Text);
                mapEvents[nowMapID][tempid].move = checkBox1.Checked;
                mapEvents[nowMapID][tempid].through = checkBox2.Checked;
                mapEvents[nowMapID][tempid].pos[0] = pos1;
                mapEvents[nowMapID][tempid].pos[1] = pos2;
                drawMapEvents();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            saveMapData();
            refreshList();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nowMapID != listBox1.SelectedIndex)
            {
                nowMapID = listBox1.SelectedIndex;
                if (nowMapID < 0)
                {
                    nowMapID = mapEvents.Count - 1;
                    listBox1.SelectedIndex = nowMapID;
                }
                file = "map_" + nowMapID.ToString() + ".json";
                label2.Text = file;
                label2.Refresh();
                textBox1.Text = mapName[nowMapID];
                textBox6.Text = bgmFile[nowMapID];
                copyEvID = -1;
                label8.Text = "无";
                label8.Refresh();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (File.Exists("..\\data\\map\\blankmap.json"))
            {
                string jsonstr = System.IO.File.ReadAllText("..\\data\\map\\blankmap.json");
                Dictionary<string, object> mapdata = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonstr);
                mapName.Add(mapdata["mapName"].ToString());
                bgmFile.Add(mapdata["bgmFile"].ToString());
                string evstr = mapdata["mapEvents"].ToString();
                List<ProjectEvent> temp = JsonSerializer.Deserialize<List<ProjectEvent>>(evstr);
                foreach (var ev in temp)
                {
                    if (!File.Exists("..\\graphics\\character\\" + ev.file))
                        ev.file = "无";
                }
                listBox1.Items.Add(mapEvents.Count.ToString().PadLeft(3, '0') + "：" + mapName[mapEvents.Count]);
                mapEvents.Add(temp);
            }
            else
                MessageBox.Show("未找到可用空白地图模板！");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult AF = MessageBox.Show("是否要将当前地图设为空白地图模板？", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (AF == DialogResult.OK)
            {
                saveMapData("blankmap.json");
                refreshList();
                MessageBox.Show("设置成功！");
            }
        }
        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            me = e;
            mouseDown = true;
        }
        private void PictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int picsiz = pictureBox1.Width / 4;
            e.Graphics.DrawRectangle(mypen, new Rectangle(pos1 * picsiz, pos2 * picsiz, picsiz, picsiz));
        }
    }
}

class ProjectEvent
{
    public int ID { get; set; }
    public string name { get; set; }
    public string file { get; set; }
    public int[] pos { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int[] triggerCondition { get; set; }
    public bool move { get; set; }
    public bool through { get; set; }
    public ProjectEvent()
    {
        ID = 0;
        name = "";
        file = "";
        pos = new int[] { 0, 0 };
        x = 0;
        y = 0;
        triggerCondition = new int[] { 0, 0, 0 };
        move = false;
        through = false;
    }
    public ProjectEvent(int ID, string name, string file, int[] pos, int x, int y, int[] triggerCondition, bool move, bool through)
    {
        this.ID = ID;
        this.name = name;
        this.file = file;
        this.pos = pos;
        this.x = x;
        this.y = y;
        this.triggerCondition = triggerCondition;
        this.move = move;
        this.through = through;
    }
}