using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MapDesigner
{
    public partial class Form1 : Form
    {
        int screenX, screenY, copyEvID, nowMapID, gameTime, pos1, pos2;
        string file;
        List<string> mapName, bgm;
        bool redraw, mouseDown;
        List<List<ProjectEvent>> events;
        Pen mypen;
        Bitmap buffer1, buffer2;
        MouseEventArgs me;
        Graphics g1, g2, g3, g4;
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
            bgm = new List<string>();
            redraw = false;
            mouseDown = false;
            events = new List<List<ProjectEvent>>();
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
            while (File.Exists("..\\data\\map\\map_" + idx.ToString() + ".dat"))
            {
                file = "map_" + idx.ToString() + ".dat";
                string datatext = System.IO.File.ReadAllText("..\\data\\map\\map_" + idx.ToString() + ".dat");
                string[] datas = datatext.Split(',');
                mapName.Add(datas[0]);
                bgm.Add(datas[1]);
                int cnt = int.Parse(datas[2]);
                List<ProjectEvent> temp = new List<ProjectEvent>();
                for (int i = 0; i < cnt; ++i)
                    temp.Add(new ProjectEvent(datas[3 + 11 * i], datas[4 + 11 * i], int.Parse(datas[5 + 11 * i]), int.Parse(datas[6 + 11 * i]), int.Parse(datas[7 + 11 * i]), int.Parse(datas[8 + 11 * i]), int.Parse(datas[9 + 11 * i]), int.Parse(datas[10 + 11 * i]), int.Parse(datas[11 + 11 * i]), int.Parse(datas[12 + 11 * i]), int.Parse(datas[13 + 11 * i])));
                events.Add(temp);
                listBox1.Items.Add(idx.ToString().PadLeft(3, '0') + "：" + mapName[idx]);
                ++idx;
            }
            nowMapID = events.Count - 1;
            listBox1.SelectedIndex = nowMapID;
            if (nowMapID < 0)
            {
                MessageBox.Show("文件夹中没有可用地图数据");
                Application.Exit();
            }
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
            List<string> savestr = new List<string>();
            savestr.Add(textBox1.Text);
            savestr.Add(textBox6.Text);
            savestr.Add(events[nowMapID].Count.ToString());
            foreach (ProjectEvent ev in events[nowMapID])
            {
                savestr.Add(ev.name);
                savestr.Add(ev.file);
                savestr.Add(ev.pos1.ToString());
                savestr.Add(ev.pos2.ToString());
                savestr.Add(ev.x.ToString());
                savestr.Add(ev.y.ToString());
                savestr.Add(ev.type.ToString());
                savestr.Add(ev.id.ToString());
                savestr.Add(ev.value.ToString());
                savestr.Add(ev.move.ToString());
                savestr.Add(ev.through.ToString());
            }
            MessageBox.Show(string.Join(",", savestr));
            System.IO.File.WriteAllText("..\\data\\map\\" + savefile, string.Join(",", savestr));
        }
        private void drawMapEvents()
        {
            if (nowMapID < 0) return;

            int picsiz = pictureBox1.Width / 4;
            if (g1 != null)
            {
                g1.Dispose();
                g1 = null;
            }
            g1 = pictureBox1.CreateGraphics();
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
                else
                    g1.Clear(pictureBox1.BackColor);
                redraw = false;
            }
            g1.DrawRectangle(mypen, new Rectangle(pos1 * picsiz, pos2 * picsiz, picsiz, picsiz));
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
            foreach (ProjectEvent ev in events[nowMapID])
            {
                if (evimg != null)
                {
                    evimg.Dispose();
                    evimg = null;
                }
                evimg = Image.FromFile("..\\graphics\\character\\" + ev.file);
                g3.DrawImage(evimg, new Rectangle(ev.x * 32, ev.y * 32, 32, 32), new Rectangle(32 * ((ev.pos1 + gameTime * ev.move) % 4), 32 * ev.pos2, 32, 32), GraphicsUnit.Pixel);
                if (!checkBox3.Checked)
                    g3.DrawString(cnt.ToString(), new Font("Arial", 12), new SolidBrush(Color.White), new Point(ev.x * 32, ev.y * 32));
                cnt++;
            }
            if (!checkBox3.Checked)
                g3.DrawRectangle(mypen, screenX * 32 - 2, screenY * 32 - 2, 36, 36);
            if (g4 != null)
            {
                g4.Dispose();
                g4 = null;
            }
            g4 = this.CreateGraphics();
            g4.DrawImage(buffer2, textBox2.Left + textBox2.Width + 32, textBox2.Top + textBox2.Height);
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
            if (events[nowMapID].Count == 0) return false;
            foreach (ProjectEvent ev in events[nowMapID])
                if (ev.x == x && ev.y == y)
                    return true;
            return false;
        }
        private ProjectEvent getCertainEvent(int x, int y)
        {
            foreach (ProjectEvent ev in events[nowMapID])
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
            this.textBox6.Text = bgm[nowMapID];
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
            bgm[nowMapID] = textBox6.Text;
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
            Point thispoint = this.PointToClient(Control.MousePosition);
            (int x, int y) pos = (textBox2.Left + textBox2.Width + 32, textBox2.Top + textBox2.Height);
            if (thispoint.X > pos.x + 352 || thispoint.Y > pos.y + 352 || thispoint.X < pos.x || thispoint.Y < pos.y) return;
            screenX = (thispoint.X - pos.x) / 32;
            screenY = (thispoint.Y - pos.y) / 32;
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
                if (File.Exists("..\\data\\enemy\\enemy_" + textBox2.Text.Split('/')[1] + ".dat"))
                {
                    string datatext = System.IO.File.ReadAllText("..\\data\\enemy\\enemy_" + textBox2.Text.Split('/')[1] + ".dat");
                    string[] data = datatext.Split(Environment.NewLine.ToCharArray());
                    data = data.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    label6.Text = data[2].Split(':')[1];
                    label6.Refresh();
                    pos1 = 0;
                    pos2 = int.Parse(data[4].Split(':')[1]);
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
                if (File.Exists("..\\data\\item\\item_" + textBox2.Text.Split('/')[1] + ".dat"))
                {
                    string datatext = System.IO.File.ReadAllText("..\\data\\item\\item_" + textBox2.Text.Split('/')[1] + ".dat");
                    string[] data = datatext.Split(Environment.NewLine.ToCharArray());
                    data = data.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    label6.Text = data[3].Split(':')[1];
                    label6.Refresh();
                    pos1 = int.Parse(data[4].Split(':')[1].Split(',')[0]);
                    pos2 = int.Parse(data[4].Split(':')[1].Split(',')[1]);
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
                events[nowMapID].Add(new ProjectEvent(textBox2.Text, label6.Text, pos1, pos2, screenX, screenY, int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text), Convert.ToInt32(checkBox1.Checked), Convert.ToInt32(checkBox2.Checked)));
                copyEvID = events[nowMapID].Count - 1;
                label8.Text = copyEvID.ToString();
            }
            else
                button5_Click(sender, e);
            drawMapEvents();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (events[nowMapID].Count == 0) return;
            if (haveAnEvent(screenX, screenY))
            {
                ProjectEvent temp = getCertainEvent(screenX, screenY);
                textBox2.Text = temp.name;
                label6.Text = temp.file;
                label6.Refresh();
                textBox3.Text = temp.type.ToString();
                textBox4.Text = temp.id.ToString();
                textBox5.Text = temp.value.ToString();
                checkBox1.Checked = Convert.ToBoolean(temp.move);
                checkBox2.Checked = Convert.ToBoolean(temp.through);
                pos1 = temp.pos1;
                pos2 = temp.pos2;
                copyEvID = events[nowMapID].IndexOf(temp);
                label8.Text = copyEvID.ToString();
                redraw = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (haveAnEvent(screenX, screenY))
            {
                events[nowMapID].Remove(getCertainEvent(screenX, screenY));
                if (copyEvID >= events[nowMapID].Count)
                {
                    copyEvID = events[nowMapID].Count - 1;
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
            events[nowMapID][copyEvID].x = screenX;
            events[nowMapID][copyEvID].y = screenY;
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
                int tempid = events[nowMapID].IndexOf(temp);
                events[nowMapID][tempid].name = textBox2.Text;
                events[nowMapID][tempid].file = label6.Text;
                events[nowMapID][tempid].type = int.Parse(textBox3.Text);
                events[nowMapID][tempid].id = int.Parse(textBox4.Text);
                events[nowMapID][tempid].value = int.Parse(textBox5.Text);
                events[nowMapID][tempid].move = Convert.ToInt32(checkBox1.Checked);
                events[nowMapID][tempid].through = Convert.ToInt32(checkBox2.Checked);
                events[nowMapID][tempid].pos1 = pos1;
                events[nowMapID][tempid].pos2 = pos2;
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
                    nowMapID = events.Count - 1;
                    listBox1.SelectedIndex = nowMapID;
                }
                file = "map_" + nowMapID.ToString() + ".dat";
                label2.Text = file;
                label2.Refresh();
                textBox1.Text = mapName[nowMapID];
                textBox6.Text = bgm[nowMapID];
                copyEvID = -1;
                label8.Text = "无";
                label8.Refresh();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (File.Exists("..\\data\\map\\blankmap.dat"))
            {
                string datatext = System.IO.File.ReadAllText("..\\data\\map\\blankmap.dat");
                string[] datas = datatext.Split(',');
                string tempMapName = datas[0];
                string tempbgm = datas[1];
                int cnt = int.Parse(datas[2]);
                List<ProjectEvent> temp = new List<ProjectEvent>();
                for (int i = 0; i < cnt; ++i)
                    temp.Add(new ProjectEvent(datas[3 + 11 * i], datas[4 + 11 * i], int.Parse(datas[5 + 11 * i]), int.Parse(datas[6 + 11 * i]), int.Parse(datas[7 + 11 * i]), int.Parse(datas[8 + 11 * i]), int.Parse(datas[9 + 11 * i]), int.Parse(datas[10 + 11 * i]), int.Parse(datas[11 + 11 * i]), int.Parse(datas[12 + 11 * i]), int.Parse(datas[13 + 11 * i]))); mapName.Add(tempMapName);
                bgm.Add(tempbgm);
                listBox1.Items.Add(events.Count.ToString().PadLeft(3, '0') + "：" + mapName[events.Count]);
                events.Add(temp);
            }
            else
                MessageBox.Show("未找到可用空白地图模板！");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult AF = MessageBox.Show("是否要将当前地图设为空白地图模板？", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (AF == DialogResult.OK)
            {
                saveMapData("blankmap.dat");
                refreshList();
                MessageBox.Show("设置成功！");
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            me = e;
            mouseDown = true;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}

class ProjectEvent
{
    public string name, file;
    public int pos1, pos2, x, y, type, id, value, move, through;
    public ProjectEvent()
    {
        name = "";
        file = "";
        pos1 = 0;
        pos2 = 0;
        x = 0;
        y = 0;
        type = 0;
        id = 0;
        value = 0;
        move = 0;
        through = 0;
    }
    public ProjectEvent(string name, string file, int pos1, int pos2, int x, int y, int type, int id, int value, int move, int through)
    {
        this.name = name;
        this.file = file;
        this.pos1 = pos1;
        this.pos2 = pos2;
        this.x = x;
        this.y = y;
        this.type = type;
        this.id = id;
        this.value = value;
        this.move = move;
        this.through = through;
    }
}