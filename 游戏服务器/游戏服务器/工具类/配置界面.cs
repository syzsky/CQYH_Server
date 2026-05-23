using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 游戏服务器.模板类;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace 游戏服务器.工具类
{
	public class 配置界面 : Form
	{
		public int 配置类型;

		private int lastMatch;

		private IContainer components;

		private Panel panel1;

		private Label label1;

		private ComboBox comboBox2;

		private Label label2;

		private NumericUpDown numericUpDown1;

		private NumericUpDown numericUpDown2;

		private Label label3;

		private TextBox textBox1;

		private Label label4;

		private NumericUpDown numericUpDown3;

		private Label label5;

		private NumericUpDown numericUpDown4;

		private Label label6;

		private Label label7;

		private CheckBox checkBox1;

		private NumericUpDown numericUpDown5;

		private NumericUpDown numericUpDown6;

		private Label label8;

		private CheckBox checkBox2;

		private Label label9;

		private Panel panel2;

		private Splitter splitter2;

		private ComboBox comboBox1;

		private ListBox listBox1;

		private Splitter splitter1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem 添加ToolStripMenuItem;

		private ToolStripMenuItem 删除ToolStripMenuItem;

		private ToolStripMenuItem 保存所有ToolStripMenuItem;

		private PropertyGrid propertyGrid1;

		private Splitter splitter3;

		private ToolStripMenuItem toolStripMenuItem1;

		private Panel panel3;

		private TextBox textBox2;

		private ToolStripMenuItem 剪切板导入爆率ToolStripMenuItem;

		private ToolStripMenuItem 剪切板导入怪物ToolStripMenuItem;

		private ToolStripMenuItem 添加到商城ToolStripMenuItem;

		private ToolStripMenuItem 复制怪物结构ToolStripMenuItem;

		private ToolStripMenuItem 粘贴怪物结构ToolStripMenuItem;

		public 配置界面(int 类型)
		{
			this.配置类型 = 类型;
			this.InitializeComponent();
		}

		private void 技能配置_Load(object sender, EventArgs e)
		{
			switch (this.配置类型)
			{
			case 0:
				this.Text = "铭文配置";
				this.comboBox1.DataSource = Enum.GetNames(typeof(游戏对象职业));
				break;
			case 1:
				this.Text = "技能配置";
				this.comboBox1.DataSource = Enum.GetNames(typeof(游戏对象职业));
				break;
			case 2:
				this.Text = "物品配置";
				this.comboBox1.DataSource = Enum.GetNames(typeof(物品使用分类));
				break;
			case 3:
				this.Text = "陷阱配置";
				break;
			case 4:
				this.Text = "怪物配置";
				this.comboBox1.DataSource = Enum.GetNames(typeof(怪物级别分类));
				break;
			case 5:
				this.Text = "BUFF配置";
				break;
			case 6:
				this.Text = "龙卫配置";
				this.comboBox1.DataSource = Enum.GetNames(typeof(游戏对象职业));
				break;
			case 7:
				this.Text = "守卫刷新";
				break;
			}
			this.刷新列表();
		}

		private void 刷新列表()
		{
			this.listBox1.Items.Clear();
			switch (this.配置类型)
			{
			case 0:
			{
				foreach (KeyValuePair<ushort, 铭文技能> item in 铭文技能.数据表)
				{
					if (item.Value.技能职业 == (游戏对象职业)Enum.Parse(typeof(游戏对象职业), this.comboBox1.SelectedItem.ToString(), ignoreCase: false) && item.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item.Value);
					}
				}
				break;
			}
			case 1:
			{
				foreach (KeyValuePair<string, 游戏技能> item2 in 游戏技能.数据表)
				{
					if (item2.Value.技能职业 == (游戏对象职业)Enum.Parse(typeof(游戏对象职业), this.comboBox1.SelectedItem.ToString(), ignoreCase: false) && item2.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item2.Value);
					}
				}
				break;
			}
			case 2:
			{
				foreach (KeyValuePair<int, 游戏物品> item3 in 游戏物品.数据表)
				{
					if (item3.Value.物品分类 == (物品使用分类)Enum.Parse(typeof(物品使用分类), this.comboBox1.SelectedItem.ToString(), ignoreCase: false) && item3.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item3.Value);
					}
				}
				break;
			}
			case 3:
			{
				foreach (KeyValuePair<string, 技能陷阱> item4 in 技能陷阱.数据表)
				{
					if (item4.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item4.Value);
					}
				}
				break;
			}
			case 4:
			{
				foreach (KeyValuePair<string, 游戏怪物> item5 in 游戏怪物.数据表)
				{
					if (item5.Value.怪物级别 == (怪物级别分类)Enum.Parse(typeof(怪物级别分类), this.comboBox1.SelectedItem.ToString(), ignoreCase: false) && (item5.Value.ToString().Contains(this.textBox2.Text) || item5.Value.备注信息.Contains(this.textBox2.Text)))
					{
						this.listBox1.Items.Add(item5.Value);
					}
				}
				break;
			}
			case 5:
			{
				foreach (KeyValuePair<ushort, 游戏Buff> item6 in 游戏Buff.数据表)
				{
					if (item6.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item6.Value);
					}
				}
				break;
			}
			case 6:
			{
				foreach (KeyValuePair<int, 龙卫模板> item7 in 龙卫模板.数据表)
				{
					if (item7.Value.需要职业 == (游戏对象职业)Enum.Parse(typeof(游戏对象职业), this.comboBox1.SelectedItem.ToString(), ignoreCase: false) && item7.Value.ToString().Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item7.Value);
					}
				}
				break;
			}
			case 7:
			{
				foreach (守卫刷新 item8 in 守卫刷新.数据表)
				{
					if (item8.区域名字.Contains(this.textBox2.Text))
					{
						this.listBox1.Items.Add(item8);
					}
				}
				break;
			}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.刷新列表();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.配置类型)
			{
			case 0:
			{
				铭文技能 selectedObject4;
				selectedObject4 = this.listBox1.SelectedItem as 铭文技能;
				this.propertyGrid1.SelectedObject = selectedObject4;
				break;
			}
			case 1:
			{
				游戏技能 selectedObject7;
				selectedObject7 = this.listBox1.SelectedItem as 游戏技能;
				this.propertyGrid1.SelectedObject = selectedObject7;
				break;
			}
			case 2:
			{
				游戏物品 selectedObject6;
				selectedObject6 = this.listBox1.SelectedItem as 游戏物品;
				this.propertyGrid1.SelectedObject = selectedObject6;
				break;
			}
			case 3:
			{
				技能陷阱 selectedObject5;
				selectedObject5 = this.listBox1.SelectedItem as 技能陷阱;
				this.propertyGrid1.SelectedObject = selectedObject5;
				break;
			}
			case 4:
			{
				List<游戏怪物> list;
				list = new List<游戏怪物>();
				foreach (object selectedItem in this.listBox1.SelectedItems)
				{
					list.Add(selectedItem as 游戏怪物);
				}
				this.propertyGrid1.SelectedObjects = list.ToArray();
				break;
			}
			case 5:
			{
				游戏Buff selectedObject3;
				selectedObject3 = this.listBox1.SelectedItem as 游戏Buff;
				this.propertyGrid1.SelectedObject = selectedObject3;
				break;
			}
			case 6:
			{
				龙卫模板 selectedObject2;
				selectedObject2 = this.listBox1.SelectedItem as 龙卫模板;
				this.propertyGrid1.SelectedObject = selectedObject2;
				break;
			}
			case 7:
			{
				守卫刷新 selectedObject;
				selectedObject = this.listBox1.SelectedItem as 守卫刷新;
				this.propertyGrid1.SelectedObject = selectedObject;
				break;
			}
			}
		}

		private void 添加铭文()
		{
			int.TryParse(Interaction.InputBox("请输入技能编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string 技能名字;
			技能名字 = Interaction.InputBox("请输入技能名称");
			int.TryParse(Interaction.InputBox("请输入铭文编号"), out var result2);
			if (铭文技能.数据表.TryGetValue((ushort)(result * 10 + result2), out var _))
			{
				MessageBox.Show("已存在的铭文技能！");
				return;
			}
			铭文技能.数据表.Add((ushort)(result * 10 + result2), new 铭文技能
			{
				技能编号 = (ushort)result,
				技能名字 = 技能名字,
				铭文编号 = (byte)result2,
				技能职业 = (游戏对象职业)Enum.Parse(typeof(游戏对象职业), this.comboBox1.SelectedItem.ToString(), ignoreCase: false)
			});
			this.刷新列表();
		}

		private void 添加技能()
		{
			int.TryParse(Interaction.InputBox("请输入技能编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入技能名称");
			int.TryParse(Interaction.InputBox("请输入铭文编号"), out var result2);
			if (游戏技能.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的技能！");
				return;
			}
			游戏技能.数据表.Add(text, new 游戏技能
			{
				自身技能编号 = (ushort)result,
				技能名字 = text,
				自身铭文编号 = (byte)result2,
				技能职业 = (游戏对象职业)Enum.Parse(typeof(游戏对象职业), this.comboBox1.SelectedItem.ToString(), ignoreCase: false)
			});
			this.刷新列表();
		}

		private void 添加Buff()
		{
			int.TryParse(Interaction.InputBox("请输入Buff编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string buff名字;
			buff名字 = Interaction.InputBox("请输入Buff名称");
			if (游戏Buff.数据表.TryGetValue((ushort)result, out var _))
			{
				MessageBox.Show("已存在的Buff！");
				return;
			}
			游戏Buff.数据表.Add((ushort)result, new 游戏Buff
			{
				Buff编号 = (ushort)result,
				Buff名字 = buff名字
			});
			this.刷新列表();
		}

		private void 添加物品()
		{
			int.TryParse(Interaction.InputBox("请输入物品编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string 物品名字;
			物品名字 = Interaction.InputBox("请输入物品名称");
			if (游戏Buff.数据表.TryGetValue((ushort)result, out var _))
			{
				MessageBox.Show("已存在的物品！");
				return;
			}
			if (MessageBox.Show("选择[是]代表此物品为普通物品，选择[否]代表此物品为装备物品", "选择物品类型", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				游戏物品.数据表.Add((ushort)result, new 游戏物品
				{
					物品编号 = (ushort)result,
					物品名字 = 物品名字
				});
			}
			else
			{
				游戏物品.数据表.Add((ushort)result, new 游戏装备
				{
					物品编号 = (ushort)result,
					物品名字 = 物品名字
				});
			}
			this.刷新列表();
		}

		private void 添加陷阱()
		{
			int.TryParse(Interaction.InputBox("请输入Buff编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入陷阱名称");
			if (技能陷阱.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的陷阱！");
				return;
			}
			技能陷阱.数据表.Add(text, new 技能陷阱
			{
				陷阱编号 = (ushort)result,
				陷阱名字 = text
			});
			this.刷新列表();
		}

		private void 添加怪物()
		{
			int.TryParse(Interaction.InputBox("请输入怪物编号"), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入怪物名称");
			if (游戏怪物.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的怪物！");
				return;
			}
			游戏怪物.数据表.Add(text, new 游戏怪物
			{
				怪物编号 = (ushort)result,
				怪物名字 = text
			});
			this.刷新列表();
		}

		private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (this.配置类型)
			{
			case 0:
				this.添加铭文();
				break;
			case 1:
				this.添加技能();
				break;
			case 2:
				this.添加物品();
				break;
			case 3:
				this.添加陷阱();
				break;
			case 4:
				this.添加怪物();
				break;
			case 5:
				this.添加Buff();
				break;
			}
		}

		private void 保存所有ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (this.配置类型)
			{
			case 0:
				铭文技能.保存数据();
				break;
			case 1:
				游戏技能.保存数据();
				break;
			case 2:
				游戏物品.保存数据();
				break;
			case 3:
				技能陷阱.保存数据();
				break;
			case 4:
				游戏怪物.保存数据();
				break;
			case 5:
				游戏Buff.保存数据();
				break;
			case 6:
				龙卫模板.保存数据();
				break;
			case 7:
				守卫刷新.保存数据();
				break;
			}
		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem != null)
			{
				switch (this.配置类型)
				{
				case 0:
				{
					铭文技能 铭文技能;
					铭文技能 = this.listBox1.SelectedItem as 铭文技能;
					铭文技能.数据表.Remove((ushort)(铭文技能.技能编号 * 10 + 铭文技能.铭文编号));
					this.listBox1.Items.Remove(铭文技能);
					this.刷新列表();
					break;
				}
				case 1:
				{
					游戏技能 游戏技能;
					游戏技能 = this.listBox1.SelectedItem as 游戏技能;
					游戏技能.数据表.Remove(游戏技能.技能名字);
					this.listBox1.Items.Remove(游戏技能);
					this.刷新列表();
					break;
				}
				case 2:
				{
					游戏物品 游戏物品;
					游戏物品 = this.listBox1.SelectedItem as 游戏物品;
					游戏物品.数据表.Remove(游戏物品.物品编号);
					游戏物品.检索表.Remove(游戏物品.物品名字);
					this.listBox1.Items.Remove(游戏物品);
					this.刷新列表();
					break;
				}
				case 3:
				{
					技能陷阱 技能陷阱;
					技能陷阱 = this.listBox1.SelectedItem as 技能陷阱;
					技能陷阱.数据表.Remove(技能陷阱.陷阱名字);
					this.listBox1.Items.Remove(技能陷阱);
					this.刷新列表();
					break;
				}
				case 4:
				{
					游戏怪物 游戏怪物;
					游戏怪物 = this.listBox1.SelectedItem as 游戏怪物;
					游戏怪物.数据表.Remove(游戏怪物.怪物名字);
					this.listBox1.Items.Remove(游戏怪物);
					this.刷新列表();
					break;
				}
				case 5:
				{
					游戏Buff 游戏Buff;
					游戏Buff = this.listBox1.SelectedItem as 游戏Buff;
					游戏Buff.数据表.Remove(游戏Buff.Buff编号);
					this.listBox1.Items.Remove(游戏Buff);
					this.刷新列表();
					break;
				}
				}
			}
		}

		public void 复制技能()
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个技能进行复制");
				return;
			}
			游戏技能 游戏技能;
			游戏技能 = this.listBox1.SelectedItem as 游戏技能;
			int.TryParse(Interaction.InputBox("请输入技能编号", "复制技能", 游戏技能.自身技能编号.ToString()), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入技能名称", "复制技能", 游戏技能.技能名字);
			int.TryParse(Interaction.InputBox("请输入铭文编号", "复制技能", 游戏技能.自身铭文编号.ToString()), out var result2);
			if (游戏技能.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的技能！");
				return;
			}
			游戏技能 游戏技能2;
			游戏技能2 = JsonConvert.DeserializeObject<游戏技能>(JsonConvert.SerializeObject(游戏技能, 序列化类.全局设置), 序列化类.全局设置);
			游戏技能2.技能名字 = text;
			游戏技能2.自身技能编号 = (ushort)result;
			游戏技能2.自身铭文编号 = (byte)result2;
			游戏技能.数据表.Add(text, 游戏技能2);
			MessageBox.Show("复制成功！");
			this.刷新列表();
		}

		public void 复制铭文()
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个铭文进行复制");
				return;
			}
			铭文技能 铭文技能;
			铭文技能 = this.listBox1.SelectedItem as 铭文技能;
			int.TryParse(Interaction.InputBox("请输入技能编号", "复制铭文", 铭文技能.技能编号.ToString()), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string 技能名字;
			技能名字 = Interaction.InputBox("请输入技能名称", "复制铭文", 铭文技能.技能名字);
			int.TryParse(Interaction.InputBox("请输入铭文编号", "复制铭文", 铭文技能.铭文编号.ToString()), out var result2);
			if (铭文技能.数据表.TryGetValue((ushort)(result * 10 + result2), out var _))
			{
				MessageBox.Show("已存在的技能！");
				return;
			}
			铭文技能 铭文技能2;
			铭文技能2 = JsonConvert.DeserializeObject<铭文技能>(JsonConvert.SerializeObject(铭文技能, 序列化类.全局设置), 序列化类.全局设置);
			铭文技能2.技能名字 = 技能名字;
			铭文技能2.技能编号 = (ushort)result;
			铭文技能2.铭文编号 = (byte)result2;
			铭文技能.数据表.Add((ushort)(result * 10 + result2), 铭文技能2);
			MessageBox.Show("复制成功！");
			this.刷新列表();
		}

		public void 复制Buff()
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个Buff进行复制");
				return;
			}
			游戏Buff 游戏Buff;
			游戏Buff = this.listBox1.SelectedItem as 游戏Buff;
			int.TryParse(Interaction.InputBox("请输入Buff编号", "复制Buff", 游戏Buff.Buff编号.ToString()), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string buff名字;
			buff名字 = Interaction.InputBox("请输入Buff名称", "复制Buff", 游戏Buff.Buff名字);
			if (游戏Buff.数据表.TryGetValue((ushort)result, out var _))
			{
				MessageBox.Show("已存在的Buff！");
				return;
			}
			游戏Buff 游戏Buff2;
			游戏Buff2 = JsonConvert.DeserializeObject<游戏Buff>(JsonConvert.SerializeObject(游戏Buff, 序列化类.全局设置), 序列化类.全局设置);
			游戏Buff2.Buff名字 = buff名字;
			游戏Buff2.Buff编号 = (ushort)result;
			游戏Buff.数据表.Add(游戏Buff2.Buff编号, 游戏Buff2);
			MessageBox.Show("复制成功！");
			this.刷新列表();
		}

		public void 复制陷阱()
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个陷阱进行复制");
				return;
			}
			技能陷阱 技能陷阱;
			技能陷阱 = this.listBox1.SelectedItem as 技能陷阱;
			int.TryParse(Interaction.InputBox("请输入陷阱编号", "复制陷阱", 技能陷阱.陷阱编号.ToString()), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入陷阱名称", "复制陷阱", 技能陷阱.陷阱名字);
			if (技能陷阱.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的陷阱！");
				return;
			}
			技能陷阱 技能陷阱2;
			技能陷阱2 = JsonConvert.DeserializeObject<技能陷阱>(JsonConvert.SerializeObject(技能陷阱, 序列化类.全局设置), 序列化类.全局设置);
			技能陷阱2.陷阱名字 = text;
			技能陷阱2.陷阱编号 = (ushort)result;
			技能陷阱.数据表.Add(技能陷阱2.陷阱名字, 技能陷阱2);
			this.刷新列表();
		}

		public void 复制怪物()
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个怪物进行复制");
				return;
			}
			游戏怪物 游戏怪物;
			游戏怪物 = this.listBox1.SelectedItem as 游戏怪物;
			int.TryParse(Interaction.InputBox("请输入怪物编号", "复制怪物", 游戏怪物.怪物编号.ToString()), out var result);
			if (result == 0)
			{
				MessageBox.Show("输入编号有误！");
				return;
			}
			string text;
			text = Interaction.InputBox("请输入怪物名称", "复制怪物", 游戏怪物.怪物名字);
			if (游戏怪物.数据表.TryGetValue(text, out var _))
			{
				MessageBox.Show("已存在的怪物！");
				return;
			}
			游戏怪物 游戏怪物2;
			游戏怪物2 = JsonConvert.DeserializeObject<游戏怪物>(JsonConvert.SerializeObject(游戏怪物, 序列化类.全局设置), 序列化类.全局设置);
			游戏怪物2.怪物名字 = text;
			游戏怪物2.怪物编号 = (ushort)result;
			游戏怪物.数据表.Add(游戏怪物2.怪物名字, 游戏怪物2);
			this.刷新列表();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			switch (this.配置类型)
			{
			case 0:
				this.复制铭文();
				break;
			case 1:
				this.复制技能();
				break;
			case 3:
				this.复制陷阱();
				break;
			case 4:
				this.复制怪物();
				break;
			case 5:
				this.复制Buff();
				break;
			case 2:
				break;
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			this.刷新列表();
		}

		private void 剪切板导入爆率ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个怪物进行导入");
			}
			else
			{
				if (!(this.listBox1.SelectedItem is 游戏怪物 游戏怪物))
				{
					return;
				}
				string[] array;
				array = Clipboard.GetText().Split("\r\n");
				游戏怪物.怪物掉落物品.Clear();
				string[] array2;
				array2 = array;
				foreach (string input in array2)
				{
					string[] array3;
					array3 = new Regex("[\\s]+").Replace(input, " ").Split(" ");
					if (!(array3[0] == string.Empty))
					{
						string value;
						value = array3[0].Split("/")[1];
						string 物品名字;
						物品名字 = array3[1];
						int 最小数量;
						最小数量 = 1;
						int 最大数量;
						最大数量 = 1;
						byte 暴率分组;
						暴率分组 = 0;
						if (array3.Length > 2)
						{
							最大数量 = Convert.ToInt32(array3[2]);
						}
						if (array3.Length > 3)
						{
							暴率分组 = Convert.ToByte(array3[3]);
						}
						游戏怪物.怪物掉落物品.Add(new 怪物掉落
						{
							物品名字 = 物品名字,
							怪物名字 = 游戏怪物.怪物名字,
							掉落概率 = Convert.ToInt32(value),
							最小数量 = 最小数量,
							最大数量 = 最大数量,
							暴率分组 = 暴率分组
						});
					}
				}
			}
		}

		private void 剪切板导入怪物ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string[] array;
			array = Clipboard.GetText().Split("\r\n");
			string text;
			text = "";
			string[] array2;
			array2 = array;
			foreach (string text2 in array2)
			{
				string[] array3;
				array3 = text2.Split("\t");
				if (array3.Length >= 14)
				{
					string key;
					key = array3[0];
					ushort num;
					num = Convert.ToUInt16(array3[1]);
					int 数值;
					数值 = Convert.ToInt32(array3[2]);
					int 数值2;
					数值2 = Convert.ToInt32(array3[3]);
					int 数值3;
					数值3 = Convert.ToInt32(array3[4]);
					int 数值4;
					数值4 = Convert.ToInt32(array3[5]);
					int 数值5;
					数值5 = Convert.ToInt32(array3[6]);
					int 数值6;
					数值6 = Convert.ToInt32(array3[7]);
					int 数值7;
					数值7 = Convert.ToInt32(array3[8]);
					int 数值8;
					数值8 = Convert.ToInt32(array3[9]);
					int 数值9;
					数值9 = Convert.ToInt32(array3[10]);
					int 数值10;
					数值10 = Convert.ToInt32(array3[11]);
					int 数值11;
					数值11 = Convert.ToInt32(array3[12]);
					int 数值12;
					数值12 = Convert.ToInt32(array3[13]);
					int num2;
					num2 = Convert.ToInt32(array3[14]);
					if (游戏怪物.数据表.TryGetValue(key, out var value) && value.怪物编号 == num)
					{
						value.怪物基础 = new 基础属性[12];
						value.怪物基础[0] = new 基础属性
						{
							属性 = 游戏对象属性.最大体力,
							数值 = 数值
						};
						value.怪物基础[1] = new 基础属性
						{
							属性 = 游戏对象属性.体力恢复,
							数值 = 数值2
						};
						value.怪物基础[2] = new 基础属性
						{
							属性 = 游戏对象属性.行走速度,
							数值 = 数值3
						};
						value.怪物基础[3] = new 基础属性
						{
							属性 = 游戏对象属性.攻击速度,
							数值 = 数值4
						};
						value.怪物基础[4] = new 基础属性
						{
							属性 = 游戏对象属性.物理准确,
							数值 = 数值5
						};
						value.怪物基础[5] = new 基础属性
						{
							属性 = 游戏对象属性.物理敏捷,
							数值 = 数值6
						};
						value.怪物基础[6] = new 基础属性
						{
							属性 = 游戏对象属性.最小攻击,
							数值 = 数值7
						};
						value.怪物基础[7] = new 基础属性
						{
							属性 = 游戏对象属性.最大攻击,
							数值 = 数值8
						};
						value.怪物基础[8] = new 基础属性
						{
							属性 = 游戏对象属性.最小防御,
							数值 = 数值9
						};
						value.怪物基础[9] = new 基础属性
						{
							属性 = 游戏对象属性.最大防御,
							数值 = 数值10
						};
						value.怪物基础[10] = new 基础属性
						{
							属性 = 游戏对象属性.最小魔防,
							数值 = 数值11
						};
						value.怪物基础[11] = new 基础属性
						{
							属性 = 游戏对象属性.最大魔防,
							数值 = 数值12
						};
						value.怪物提供经验 = (ushort)num2;
					}
					else
					{
						text = text + text2 + "\r\n";
					}
					if (text != string.Empty)
					{
						Clipboard.SetText(text);
					}
				}
			}
		}

		private void 添加到商城ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItem == null)
			{
				MessageBox.Show("请选择一个物品添加");
			}
			else
			{
				if (!(this.listBox1.SelectedItem is 游戏物品 游戏物品) || 珍宝商品.数据表.TryGetValue(游戏物品.物品编号, out var _))
				{
					return;
				}
				int.TryParse(Interaction.InputBox("请输入单位数量"), out var result);
				if (result == 0)
				{
					MessageBox.Show("输入有误！");
					return;
				}
				int.TryParse(Interaction.InputBox("请输入商品分类"), out var result2);
				int.TryParse(Interaction.InputBox("请输入商品标签"), out var result3);
				if (result3 == 0)
				{
					MessageBox.Show("输入有误！");
					return;
				}
				珍宝商品.数据表.Add(游戏物品.物品编号, new 珍宝商品
				{
					物品编号 = 游戏物品.物品编号,
					单位数量 = result,
					商品分类 = (byte)result2,
					商品标签 = 1,
					补充参数 = 63,
					商品原价 = result3,
					商品现价 = result3
				});
				珍宝商品.重新生成发送数据();
			}
		}

		private void 复制怪物结构ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(JsonConvert.SerializeObject(this.listBox1.SelectedItem, Formatting.Indented, 序列化类.全局设置));
		}

		private void 粘贴怪物结构ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!(JsonConvert.DeserializeObject(Clipboard.GetText(), typeof(游戏怪物), 序列化类.全局设置) is 游戏怪物 游戏怪物))
			{
				return;
			}
			foreach (object selectedItem in this.listBox1.SelectedItems)
			{
				游戏怪物 obj;
				obj = selectedItem as 游戏怪物;
				obj.怪物基础 = 游戏怪物.怪物基础;
				obj.怪物提供经验 = 游戏怪物.怪物提供经验;
				obj.怪物仇恨时间 = 游戏怪物.怪物仇恨时间;
				obj.怪物仇恨范围 = 游戏怪物.怪物仇恨范围;
				obj.主动攻击目标 = 游戏怪物.主动攻击目标;
				obj.尸体保留时长 = 游戏怪物.尸体保留时长;
				obj.怪物漫游间隔 = 游戏怪物.怪物漫游间隔;
				obj.怪物移动间隔 = 游戏怪物.怪物移动间隔;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.label9 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.剪切板导入爆率ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.剪切板导入怪物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加到商城ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.复制怪物结构ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴怪物结构ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel3 = new System.Windows.Forms.Panel();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
			this.panel2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.splitter3);
			this.panel1.Controls.Add(this.propertyGrid1);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.checkBox2);
			this.panel1.Controls.Add(this.numericUpDown6);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.numericUpDown5);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.numericUpDown4);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.numericUpDown3);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.numericUpDown2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.numericUpDown1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.comboBox2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(404, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(658, 1107);
			this.panel1.TabIndex = 3;
			this.splitter3.Location = new System.Drawing.Point(0, 0);
			this.splitter3.Margin = new System.Windows.Forms.Padding(5);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(12, 1107);
			this.splitter3.TabIndex = 24;
			this.splitter3.TabStop = false;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Margin = new System.Windows.Forms.Padding(5);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(658, 1107);
			this.propertyGrid1.TabIndex = 23;
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(1029, 36);
			this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(69, 20);
			this.label9.TabIndex = 22;
			this.label9.Text = "铭文描述";
			this.label9.Visible = false;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(890, 468);
			this.checkBox2.Margin = new System.Windows.Forms.Padding(5);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(91, 24);
			this.checkBox2.TabIndex = 21;
			this.checkBox2.Text = "广播通知";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.Visible = false;
			this.numericUpDown6.Location = new System.Drawing.Point(773, 627);
			this.numericUpDown6.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown6.TabIndex = 20;
			this.numericUpDown6.Visible = false;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(769, 601);
			this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(69, 20);
			this.label8.TabIndex = 19;
			this.label8.Text = "洗练概率";
			this.label8.Visible = false;
			this.numericUpDown5.Location = new System.Drawing.Point(773, 551);
			this.numericUpDown5.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown5.TabIndex = 18;
			this.numericUpDown5.Visible = false;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(773, 468);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(5);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(91, 24);
			this.checkBox1.TabIndex = 17;
			this.checkBox1.Text = "被动技能";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Visible = false;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(769, 525);
			this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(69, 20);
			this.label7.TabIndex = 16;
			this.label7.Text = "铭文品质";
			this.label7.Visible = false;
			this.numericUpDown4.Location = new System.Drawing.Point(773, 424);
			this.numericUpDown4.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown4.TabIndex = 15;
			this.numericUpDown4.Visible = false;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(769, 399);
			this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 20);
			this.label6.TabIndex = 14;
			this.label6.Text = "计数周期";
			this.label6.Visible = false;
			this.numericUpDown3.Location = new System.Drawing.Point(773, 353);
			this.numericUpDown3.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown3.TabIndex = 13;
			this.numericUpDown3.Visible = false;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(769, 328);
			this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 20);
			this.label5.TabIndex = 12;
			this.label5.Text = "技能计数";
			this.label5.Visible = false;
			this.textBox1.Location = new System.Drawing.Point(773, 61);
			this.textBox1.Margin = new System.Windows.Forms.Padding(5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(248, 27);
			this.textBox1.TabIndex = 11;
			this.textBox1.Visible = false;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(769, 36);
			this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 20);
			this.label4.TabIndex = 10;
			this.label4.Text = "技能名字";
			this.label4.Visible = false;
			this.numericUpDown2.Location = new System.Drawing.Point(773, 280);
			this.numericUpDown2.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown2.TabIndex = 9;
			this.numericUpDown2.Visible = false;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(769, 255);
			this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "铭文编号";
			this.label3.Visible = false;
			this.numericUpDown1.Location = new System.Drawing.Point(773, 209);
			this.numericUpDown1.Margin = new System.Windows.Forms.Padding(5);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(180, 27);
			this.numericUpDown1.TabIndex = 7;
			this.numericUpDown1.Visible = false;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(769, 185);
			this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "技能编号";
			this.label2.Visible = false;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(769, 105);
			this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 20);
			this.label1.TabIndex = 4;
			this.label1.Text = "技能职业";
			this.label1.Visible = false;
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(773, 129);
			this.comboBox2.Margin = new System.Windows.Forms.Padding(5);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(187, 28);
			this.comboBox2.TabIndex = 3;
			this.comboBox2.Visible = false;
			this.panel2.Controls.Add(this.listBox1);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.splitter2);
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(404, 1107);
			this.panel2.TabIndex = 4;
			this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 20;
			this.listBox1.Location = new System.Drawing.Point(0, 66);
			this.listBox1.Margin = new System.Windows.Forms.Padding(5);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(404, 1041);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 3;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.添加ToolStripMenuItem, this.toolStripMenuItem1, this.删除ToolStripMenuItem, this.保存所有ToolStripMenuItem, this.剪切板导入爆率ToolStripMenuItem, this.剪切板导入怪物ToolStripMenuItem, this.添加到商城ToolStripMenuItem, this.复制怪物结构ToolStripMenuItem, this.粘贴怪物结构ToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(184, 220);
			this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
			this.添加ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.添加ToolStripMenuItem.Text = "添加";
			this.添加ToolStripMenuItem.Click += new System.EventHandler(添加ToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 24);
			this.toolStripMenuItem1.Text = "复制";
			this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(删除ToolStripMenuItem_Click);
			this.保存所有ToolStripMenuItem.Name = "保存所有ToolStripMenuItem";
			this.保存所有ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.保存所有ToolStripMenuItem.Text = "保存所有";
			this.保存所有ToolStripMenuItem.Click += new System.EventHandler(保存所有ToolStripMenuItem_Click);
			this.剪切板导入爆率ToolStripMenuItem.Name = "剪切板导入爆率ToolStripMenuItem";
			this.剪切板导入爆率ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.剪切板导入爆率ToolStripMenuItem.Text = "剪切板导入爆率";
			this.剪切板导入爆率ToolStripMenuItem.Click += new System.EventHandler(剪切板导入爆率ToolStripMenuItem_Click);
			this.剪切板导入怪物ToolStripMenuItem.Name = "剪切板导入怪物ToolStripMenuItem";
			this.剪切板导入怪物ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.剪切板导入怪物ToolStripMenuItem.Text = "剪切板导入怪物";
			this.剪切板导入怪物ToolStripMenuItem.Click += new System.EventHandler(剪切板导入怪物ToolStripMenuItem_Click);
			this.添加到商城ToolStripMenuItem.Name = "添加到商城ToolStripMenuItem";
			this.添加到商城ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.添加到商城ToolStripMenuItem.Text = "添加到商城";
			this.添加到商城ToolStripMenuItem.Click += new System.EventHandler(添加到商城ToolStripMenuItem_Click);
			this.复制怪物结构ToolStripMenuItem.Name = "复制怪物结构ToolStripMenuItem";
			this.复制怪物结构ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.复制怪物结构ToolStripMenuItem.Text = "复制怪物结构";
			this.复制怪物结构ToolStripMenuItem.Click += new System.EventHandler(复制怪物结构ToolStripMenuItem_Click);
			this.粘贴怪物结构ToolStripMenuItem.Name = "粘贴怪物结构ToolStripMenuItem";
			this.粘贴怪物结构ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
			this.粘贴怪物结构ToolStripMenuItem.Text = "粘贴怪物结构";
			this.粘贴怪物结构ToolStripMenuItem.Click += new System.EventHandler(粘贴怪物结构ToolStripMenuItem_Click);
			this.panel3.Controls.Add(this.textBox2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 33);
			this.panel3.Margin = new System.Windows.Forms.Padding(5);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(404, 33);
			this.panel3.TabIndex = 7;
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(0, 0);
			this.textBox2.Margin = new System.Windows.Forms.Padding(5);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(404, 27);
			this.textBox2.TabIndex = 7;
			this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 28);
			this.splitter2.Margin = new System.Windows.Forms.Padding(5);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(404, 5);
			this.splitter2.TabIndex = 5;
			this.splitter2.TabStop = false;
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(0, 0);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(5);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(404, 28);
			this.comboBox1.TabIndex = 4;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			this.splitter1.Location = new System.Drawing.Point(200, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(10, 759);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 20f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1062, 1107);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.panel2);
			base.Margin = new System.Windows.Forms.Padding(5);
			base.Name = "配置界面";
			this.Text = "技能配置";
			base.Load += new System.EventHandler(技能配置_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
			this.panel2.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
