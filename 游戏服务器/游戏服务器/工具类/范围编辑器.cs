using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 游戏服务器.工具类
{
	public class 范围编辑器 : Form
	{
		public Graphics 画板;

		public Graphics 底图;

		public int 缩放值;

		public int 偏移值;

		public int 块大小;

		public Dictionary<游戏方向, List<Point>> 范围坐标;

		public int X坐标;

		public int Y坐标;

		public int 行总数;

		public int 列总数;

		public int 偏移Y;

		public int 偏移X;

		public 游戏方向 方向;

		private IContainer components;

		private PictureBox pictureBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem 左方ToolStripMenuItem;

		private ToolStripMenuItem 左上ToolStripMenuItem;

		private ToolStripMenuItem 上方ToolStripMenuItem;

		private ToolStripMenuItem 右上ToolStripMenuItem;

		private ToolStripMenuItem 右方ToolStripMenuItem;

		private ToolStripMenuItem 右下ToolStripMenuItem;

		private ToolStripMenuItem 下方ToolStripMenuItem;

		private ToolStripMenuItem 左下ToolStripMenuItem;

		public 范围编辑器(object 范围)
		{
			this.范围坐标 = 范围 as Dictionary<游戏方向, List<Point>>;
			this.InitializeComponent();
		}

		private void 范围编辑器_Load(object sender, EventArgs e)
		{
			this.方向 = 游戏方向.左方;
			this.缩放值 = 30;
			this.偏移值 = 10;
			this.块大小 = 25;
			this.行总数 = this.pictureBox1.Width / this.缩放值;
			this.列总数 = this.pictureBox1.Height / this.缩放值;
			this.偏移Y = this.列总数 / 2;
			this.偏移X = this.行总数 / 2;
		}

		private void 范围编辑器_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			this.X坐标 = e.X - this.偏移值;
			this.Y坐标 = e.Y - this.偏移值;
			if (this.X坐标 >= 0 && this.X坐标 < (this.X坐标 / this.缩放值 + 1) * this.缩放值 && this.Y坐标 >= 0 && this.Y坐标 < (this.Y坐标 / this.缩放值 + 1) * this.缩放值)
			{
				this.Text = $"{this.X坐标 / this.缩放值} {this.Y坐标 / this.缩放值}  当前方向：{this.方向}";
			}
			else
			{
				this.Text = $"未获取到坐标  当前方向：{this.方向}";
				this.X坐标 = -1;
				this.Y坐标 = -1;
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			Point item;
			item = new Point(-(this.X坐标 / this.缩放值 - this.偏移Y), -(this.Y坐标 / this.缩放值 - this.偏移X));
			if (e.Button == MouseButtons.Left)
			{
				if (this.X坐标 > -1 && this.Y坐标 > -1 && !this.范围坐标[this.方向].Contains(item))
				{
					this.范围坐标[this.方向].Add(item);
				}
			}
			else if (e.Button == MouseButtons.Middle && this.X坐标 > -1 && this.Y坐标 > -1 && this.范围坐标[this.方向].Contains(item))
			{
				this.范围坐标[this.方向].Remove(item);
			}
			this.画板.DrawImage(this.pictureBox1.Image, 0, 0);
			for (int i = 0; i < this.范围坐标[this.方向].Count; i++)
			{
				this.画板.FillRectangle(new SolidBrush(Color.Red), this.偏移值 + (-this.范围坐标[this.方向][i].X + this.偏移X) * this.缩放值, this.偏移值 + (-this.范围坐标[this.方向][i].Y + this.偏移Y) * this.缩放值, this.块大小, this.块大小);
			}
		}

		private void 范围编辑器_Shown(object sender, EventArgs e)
		{
			Bitmap image;
			image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
			this.pictureBox1.Image = image;
			this.底图 = Graphics.FromImage(image);
			this.底图.Clear(Color.White);
			for (int i = 0; i < this.行总数; i++)
			{
				for (int j = 0; j < this.列总数; j++)
				{
					if (i == this.行总数 / 2 && j == this.列总数 / 2)
					{
						this.底图.FillRectangle(new SolidBrush(Color.Green), this.偏移值 + i * this.缩放值, this.偏移值 + j * this.缩放值, this.块大小, this.块大小);
					}
					else
					{
						this.底图.FillRectangle(new SolidBrush(Color.Black), this.偏移值 + i * this.缩放值, this.偏移值 + j * this.缩放值, this.块大小, this.块大小);
					}
				}
			}
			this.画板 = this.pictureBox1.CreateGraphics();
			for (int k = 0; k < this.范围坐标[this.方向].Count; k++)
			{
				this.底图.FillRectangle(new SolidBrush(Color.Red), this.偏移值 + (-this.范围坐标[this.方向][k].X + this.偏移X) * this.缩放值, this.偏移值 + (-this.范围坐标[this.方向][k].Y + this.偏移Y) * this.缩放值, this.块大小, this.块大小);
			}
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			this.左方ToolStripMenuItem.Checked = false;
			this.左上ToolStripMenuItem.Checked = false;
			this.上方ToolStripMenuItem.Checked = false;
			this.右上ToolStripMenuItem.Checked = false;
			this.右方ToolStripMenuItem.Checked = false;
			this.右下ToolStripMenuItem.Checked = false;
			this.下方ToolStripMenuItem.Checked = false;
			this.左下ToolStripMenuItem.Checked = false;
			switch (this.方向)
			{
			case 游戏方向.左上:
				this.左上ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.左方:
				this.左方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右上:
				this.右上ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.上方:
				this.上方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右下:
				this.右下ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右方:
				this.右方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.左下:
				this.左下ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.下方:
				this.下方ToolStripMenuItem.Checked = true;
				break;
			}
		}

		private void 左方ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (sender == this.左方ToolStripMenuItem)
			{
				this.方向 = 游戏方向.左方;
			}
			if (sender == this.左上ToolStripMenuItem)
			{
				this.方向 = 游戏方向.左上;
			}
			if (sender == this.上方ToolStripMenuItem)
			{
				this.方向 = 游戏方向.上方;
			}
			if (sender == this.右上ToolStripMenuItem)
			{
				this.方向 = 游戏方向.右上;
			}
			if (sender == this.右方ToolStripMenuItem)
			{
				this.方向 = 游戏方向.右方;
			}
			if (sender == this.右下ToolStripMenuItem)
			{
				this.方向 = 游戏方向.右下;
			}
			if (sender == this.下方ToolStripMenuItem)
			{
				this.方向 = 游戏方向.下方;
			}
			if (sender == this.左下ToolStripMenuItem)
			{
				this.方向 = 游戏方向.左下;
			}
			this.左方ToolStripMenuItem.Checked = false;
			this.左上ToolStripMenuItem.Checked = false;
			this.上方ToolStripMenuItem.Checked = false;
			this.右上ToolStripMenuItem.Checked = false;
			this.右方ToolStripMenuItem.Checked = false;
			this.右下ToolStripMenuItem.Checked = false;
			this.下方ToolStripMenuItem.Checked = false;
			this.左下ToolStripMenuItem.Checked = false;
			switch (this.方向)
			{
			case 游戏方向.左上:
				this.左上ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.左方:
				this.左方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右上:
				this.右上ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.上方:
				this.上方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右下:
				this.右下ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.右方:
				this.右方ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.左下:
				this.左下ToolStripMenuItem.Checked = true;
				break;
			case 游戏方向.下方:
				this.下方ToolStripMenuItem.Checked = true;
				break;
			}
			this.范围编辑器_Shown(null, null);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.左方ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.左上ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.上方ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.右上ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.右方ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.右下ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.下方ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.左下ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(950, 950);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseClick);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseMove);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.左方ToolStripMenuItem, this.左上ToolStripMenuItem, this.上方ToolStripMenuItem, this.右上ToolStripMenuItem, this.右方ToolStripMenuItem, this.右下ToolStripMenuItem, this.下方ToolStripMenuItem, this.左下ToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(101, 180);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStrip1_Opening);
			this.左方ToolStripMenuItem.Name = "左方ToolStripMenuItem";
			this.左方ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.左方ToolStripMenuItem.Text = "左方";
			this.左方ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.左上ToolStripMenuItem.Name = "左上ToolStripMenuItem";
			this.左上ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.左上ToolStripMenuItem.Text = "左上";
			this.左上ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.上方ToolStripMenuItem.Name = "上方ToolStripMenuItem";
			this.上方ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.上方ToolStripMenuItem.Text = "上方";
			this.上方ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.右上ToolStripMenuItem.Name = "右上ToolStripMenuItem";
			this.右上ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.右上ToolStripMenuItem.Text = "右上";
			this.右上ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.右方ToolStripMenuItem.Name = "右方ToolStripMenuItem";
			this.右方ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.右方ToolStripMenuItem.Text = "右方";
			this.右方ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.右下ToolStripMenuItem.Name = "右下ToolStripMenuItem";
			this.右下ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.右下ToolStripMenuItem.Text = "右下";
			this.右下ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.下方ToolStripMenuItem.Name = "下方ToolStripMenuItem";
			this.下方ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.下方ToolStripMenuItem.Text = "下方";
			this.下方ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			this.左下ToolStripMenuItem.Name = "左下ToolStripMenuItem";
			this.左下ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.左下ToolStripMenuItem.Text = "左下";
			this.左下ToolStripMenuItem.Click += new System.EventHandler(左方ToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(950, 950);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Margin = new System.Windows.Forms.Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "范围编辑器";
			this.Text = "范围编辑器 关闭后自动生效";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(范围编辑器_FormClosing);
			base.Load += new System.EventHandler(范围编辑器_Load);
			base.Shown += new System.EventHandler(范围编辑器_Shown);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
