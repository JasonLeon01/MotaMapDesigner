namespace MapDesigner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listBox1 = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label7 = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            label10 = new Label();
            checkBox3 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            textBox6 = new TextBox();
            label11 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(10, 10);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 497);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(140, 10);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "文件名：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(200, 10);
            label2.Name = "label2";
            label2.Size = new Size(20, 17);
            label2.TabIndex = 2;
            label2.Text = "无";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(140, 35);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 3;
            label3.Text = "地图名：";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(200, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 23);
            textBox1.TabIndex = 4;
            textBox1.Text = "无";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(140, 92);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 5;
            label4.Text = "事件名：";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(200, 89);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 23);
            textBox2.TabIndex = 6;
            textBox2.Text = "无";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(140, 122);
            label5.Name = "label5";
            label5.Size = new Size(56, 17);
            label5.TabIndex = 7;
            label5.Text = "图片名：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(200, 122);
            label6.Name = "label6";
            label6.Size = new Size(20, 17);
            label6.TabIndex = 8;
            label6.Text = "无";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Location = new Point(140, 147);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ControlDark;
            pictureBox2.Location = new Point(274, 147);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(32, 32);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(140, 282);
            label7.Name = "label7";
            label7.Size = new Size(80, 17);
            label7.TabIndex = 11;
            label7.Text = "出现触发判定";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(140, 302);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(30, 23);
            textBox3.TabIndex = 12;
            textBox3.Text = "0";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(176, 302);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(30, 23);
            textBox4.TabIndex = 13;
            textBox4.Text = "0";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(212, 302);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(30, 23);
            textBox5.TabIndex = 14;
            textBox5.Text = "0";
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(200, 332);
            label8.Name = "label8";
            label8.Size = new Size(20, 17);
            label8.TabIndex = 16;
            label8.Text = "无";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(140, 332);
            label9.Name = "label9";
            label9.Size = new Size(56, 17);
            label9.TabIndex = 15;
            label9.Text = "事件号：";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(140, 352);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(51, 21);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "动效";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(205, 352);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(51, 21);
            checkBox2.TabIndex = 18;
            checkBox2.Text = "穿透";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(352, 480);
            label10.Name = "label10";
            label10.Size = new Size(33, 17);
            label10.TabIndex = 19;
            label10.Text = "(0,0)";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(384, 480);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(75, 21);
            checkBox3.TabIndex = 20;
            checkBox3.Text = "隐藏信息";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(140, 372);
            button1.Name = "button1";
            button1.Size = new Size(73, 30);
            button1.TabIndex = 21;
            button1.Text = "匹配图像";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Location = new Point(219, 372);
            button2.Name = "button2";
            button2.Size = new Size(73, 30);
            button2.TabIndex = 22;
            button2.Text = "添加(&A)";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Cursor = Cursors.Hand;
            button3.Location = new Point(219, 408);
            button3.Name = "button3";
            button3.Size = new Size(73, 30);
            button3.TabIndex = 24;
            button3.Text = "删除(&D)";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Cursor = Cursors.Hand;
            button4.Location = new Point(140, 408);
            button4.Name = "button4";
            button4.Size = new Size(73, 30);
            button4.TabIndex = 23;
            button4.Text = "复制(&C)";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Cursor = Cursors.Hand;
            button5.Location = new Point(219, 444);
            button5.Name = "button5";
            button5.Size = new Size(73, 30);
            button5.TabIndex = 26;
            button5.Text = "覆盖(&R)";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Cursor = Cursors.Hand;
            button6.Location = new Point(140, 444);
            button6.Name = "button6";
            button6.Size = new Size(73, 30);
            button6.TabIndex = 25;
            button6.Text = "移动(&M)";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Cursor = Cursors.Hand;
            button7.Location = new Point(140, 480);
            button7.Name = "button7";
            button7.Size = new Size(152, 30);
            button7.TabIndex = 27;
            button7.Text = "保存(&S)";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(200, 60);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 23);
            textBox6.TabIndex = 29;
            textBox6.Text = "无";
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(140, 63);
            label11.Name = "label11";
            label11.Size = new Size(49, 17);
            label11.TabIndex = 28;
            label11.Text = "BGM：";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(800, 541);
            Controls.Add(textBox6);
            Controls.Add(label11);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button6);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox3);
            Controls.Add(label10);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(label7);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "可视化地图编辑器";
            Load += Form1_Load;
            Click += Form1_Click;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Label label4;
        private TextBox textBox2;
        private Label label5;
        private Label label6;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label7;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label8;
        private Label label9;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label label10;
        private CheckBox checkBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private TextBox textBox6;
        private Label label11;
        private System.Windows.Forms.Timer timer1;
    }
}