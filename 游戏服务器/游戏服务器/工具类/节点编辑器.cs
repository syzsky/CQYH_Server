using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using 游戏服务器.模板类;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace 游戏服务器.工具类
{
	public class 节点编辑器 : Form
	{
		public SortedDictionary<int, 技能任务> 节点列表;

		private IContainer components;

		private PropertyGrid propertyGrid1;

		private ListBox listBox1;

		private Button button1;

		private Button button2;

		private ComboBox comboBox1;

		private Label label1;

		private Label label2;

		private NumericUpDown numericUpDown1;

		private Button button3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem 复制节点ToolStripMenuItem;

		private ToolStripMenuItem 粘贴节点ToolStripMenuItem;

		public 节点编辑器(SortedDictionary<int, 技能任务> 列表)
		{
			this.节点列表 = 列表;
			this.InitializeComponent();
		}

		public void 刷新界面()
		{
			this.listBox1.Items.Clear();
			foreach (KeyValuePair<int, 技能任务> item in this.节点列表)
			{
				this.listBox1.Items.Add(item.Key);
			}
		}

		private void 节点编辑器_Load(object sender, EventArgs e)
		{
			this.刷新界面();
			this.comboBox1.Items.Add("A_00_触发子类技能");
			this.comboBox1.Items.Add("A_01_触发对象Buff");
			this.comboBox1.Items.Add("A_02_触发陷阱技能");
			this.comboBox1.Items.Add("B_00_技能切换通知");
			this.comboBox1.Items.Add("B_01_技能释放通知");
			this.comboBox1.Items.Add("B_02_技能命中通知");
			this.comboBox1.Items.Add("B_03_前摇结束通知");
			this.comboBox1.Items.Add("B_04_后摇结束通知");
			this.comboBox1.Items.Add("C_00_计算技能锚点");
			this.comboBox1.Items.Add("C_01_计算命中目标");
			this.comboBox1.Items.Add("C_02_计算目标伤害");
			this.comboBox1.Items.Add("C_03_计算对象位移");
			this.comboBox1.Items.Add("C_04_计算目标诱惑");
			this.comboBox1.Items.Add("C_05_计算目标回复");
			this.comboBox1.Items.Add("C_06_计算宠物召唤");
			this.comboBox1.Items.Add("C_07_计算目标瞬移");
			this.comboBox1.Items.Add("D_01_升级武器鉴定");
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem != null)
			{
				this.propertyGrid1.SelectedObject = this.节点列表[(int)this.listBox1.SelectedItem];
				this.numericUpDown1.Value = (int)this.listBox1.SelectedItem;
			}
		}

		private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (this.listBox1.Items.Count != 0)
			{
				e.DrawBackground();
				Brush brush;
				brush = Brushes.Black;
				switch (e.Index)
				{
				case 0:
					brush = Brushes.Red;
					break;
				case 1:
					brush = Brushes.Orange;
					break;
				case 2:
					brush = Brushes.Purple;
					break;
				}
				e.DrawFocusRectangle();
				e.Graphics.DrawString($"{(int)this.listBox1.Items[e.Index]} {this.节点列表[(int)this.listBox1.Items[e.Index]].GetType().Name}", e.Font, brush, e.Bounds, StringFormat.GenericDefault);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem != null)
			{
				this.节点列表.Remove((int)this.listBox1.SelectedItem);
				this.刷新界面();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int.TryParse(Interaction.InputBox("请输入节点触发时间"), out var result);
			if (this.节点列表.TryGetValue(result, out var _))
			{
				MessageBox.Show("触发时间不能重复！");
				return;
			}
			this.节点列表.Add(result, new A_00_触发子类技能());
			this.刷新界面();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem != null)
			{
				技能任务 value;
				value = this.comboBox1.SelectedIndex switch
				{
					0 => new A_00_触发子类技能(), 
					1 => new A_01_触发对象Buff(), 
					2 => new A_02_触发陷阱技能(), 
					3 => new B_00_技能切换通知(), 
					4 => new B_01_技能释放通知(), 
					5 => new B_02_技能命中通知(), 
					6 => new B_03_前摇结束通知(), 
					7 => new B_04_后摇结束通知(), 
					8 => new C_00_计算技能锚点(), 
					9 => new C_01_计算命中目标(), 
					10 => new C_02_计算目标伤害(), 
					11 => new C_03_计算对象位移(), 
					12 => new C_04_计算目标诱惑(), 
					13 => new C_05_计算目标回复(), 
					14 => new C_06_计算宠物召唤(), 
					15 => new C_07_计算目标瞬移(), 
					_ => new A_00_触发子类技能(), 
				};
				this.节点列表[(int)this.listBox1.SelectedItem] = value;
				this.刷新界面();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem == null)
			{
				return;
			}
			if (this.节点列表.TryGetValue((int)this.numericUpDown1.Value, out var _))
			{
				MessageBox.Show("触发时间不能重复！");
				return;
			}
			Dictionary<int, 技能任务> dictionary;
			dictionary = this.节点列表.ToDictionary((KeyValuePair<int, 技能任务> k) => (k.Key != (int)this.listBox1.SelectedItem) ? k.Key : ((int)this.numericUpDown1.Value), (KeyValuePair<int, 技能任务> k) => k.Value);
			this.节点列表.Clear();
			foreach (KeyValuePair<int, 技能任务> item in dictionary)
			{
				this.节点列表.Add(item.Key, item.Value);
			}
			this.刷新界面();
		}

		private void 复制节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(JsonConvert.SerializeObject(this.节点列表[(int)this.listBox1.SelectedItem], Formatting.Indented, new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameHandling = TypeNameHandling.Objects,
				Formatting = Formatting.Indented
			}));
			MessageBox.Show("节点已经复制到剪切板！");
		}

		private void 粘贴节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				object obj;
				obj = JsonConvert.DeserializeObject(Clipboard.GetText(), 序列化类.全局设置);
				if (!(obj is 技能任务))
				{
					MessageBox.Show("剪切板里数据类型不正确！", "失败");
					return;
				}
				int.TryParse(Interaction.InputBox("请输入节点触发时间"), out var result);
				if (result < 0)
				{
					MessageBox.Show("输入编号有误！");
					return;
				}
				if (this.节点列表.TryGetValue(result, out var _))
				{
					MessageBox.Show("已存在相同序号节点！");
					return;
				}
				this.节点列表[result] = (技能任务)obj;
				this.刷新界面();
			}
			catch (Exception)
			{
				MessageBox.Show("剪切板里数据类型不正确！", "失败");
			}
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.button3 = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.复制节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.propertyGrid1.Location = new System.Drawing.Point(260, 12);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(417, 524);
			this.propertyGrid1.TabIndex = 0;
			this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(12, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(241, 424);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 1;
			this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBox1_DrawItem);
			this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
			this.button1.Location = new System.Drawing.Point(12, 452);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "添加节点";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(button1_Click);
			this.button2.Location = new System.Drawing.Point(93, 452);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "删除节点";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(button2_Click);
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(93, 481);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 4;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 489);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "重置节点特性";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 510);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "触发时间调整";
			this.numericUpDown1.Location = new System.Drawing.Point(94, 504);
			this.numericUpDown1.Maximum = new decimal(new int[4] { 2100000000, 0, 0, 0 });
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(88, 21);
			this.numericUpDown1.TabIndex = 7;
			this.button3.Location = new System.Drawing.Point(188, 504);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(26, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "Ok";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(button3_Click);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.复制节点ToolStripMenuItem, this.粘贴节点ToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
			this.复制节点ToolStripMenuItem.Name = "复制节点ToolStripMenuItem";
			this.复制节点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.复制节点ToolStripMenuItem.Text = "复制节点";
			this.复制节点ToolStripMenuItem.Click += new System.EventHandler(复制节点ToolStripMenuItem_Click);
			this.粘贴节点ToolStripMenuItem.Name = "粘贴节点ToolStripMenuItem";
			this.粘贴节点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.粘贴节点ToolStripMenuItem.Text = "粘贴节点";
			this.粘贴节点ToolStripMenuItem.Click += new System.EventHandler(粘贴节点ToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(685, 536);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.numericUpDown1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.propertyGrid1);
			base.Name = "节点编辑器";
			this.Text = "节点编辑器";
			base.Load += new System.EventHandler(节点编辑器_Load);
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
