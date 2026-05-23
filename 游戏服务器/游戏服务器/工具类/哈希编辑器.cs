using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 游戏服务器.工具类
{
	public class 哈希编辑器 : Form
	{
		public object 哈希表;

		private IContainer components;

		private TextBox textBox1;

		public 哈希编辑器(object 哈希)
		{
			this.哈希表 = 哈希;
			this.InitializeComponent();
		}

		private void 哈希编辑器_Load(object sender, EventArgs e)
		{
			this.textBox1.Clear();
			if (this.哈希表 is HashSet<int> hashSet)
			{
				{
					foreach (int item in hashSet)
					{
						this.textBox1.AppendText(item + "\r\n");
					}
					return;
				}
			}
			if (this.哈希表 is HashSet<string> hashSet2)
			{
				{
					foreach (string item2 in hashSet2)
					{
						this.textBox1.AppendText(item2 + "\r\n");
					}
					return;
				}
			}
			if (!(this.哈希表 is HashSet<ushort> hashSet3))
			{
				return;
			}
			foreach (ushort item3 in hashSet3)
			{
				this.textBox1.AppendText(item3 + "\r\n");
			}
		}

		private void 哈希编辑器_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.哈希表 is HashSet<int>)
			{
				HashSet<int> hashSet;
				try
				{
					hashSet = new HashSet<int>();
					string[] lines;
					lines = this.textBox1.Lines;
					foreach (string text in lines)
					{
						if (!(text == string.Empty))
						{
							int.TryParse(text, out var result);
							hashSet.Add(result);
						}
					}
				}
				catch (Exception)
				{
					return;
				}
				this.哈希表 = hashSet;
			}
			else if (this.哈希表 is HashSet<string>)
			{
				HashSet<string> hashSet2;
				try
				{
					hashSet2 = new HashSet<string>();
					string[] lines;
					lines = this.textBox1.Lines;
					foreach (string text2 in lines)
					{
						if (!(text2 == string.Empty))
						{
							hashSet2.Add(text2);
						}
					}
				}
				catch (Exception)
				{
					return;
				}
				this.哈希表 = hashSet2;
			}
			else
			{
				if (!(this.哈希表 is HashSet<ushort>))
				{
					return;
				}
				HashSet<ushort> hashSet3;
				try
				{
					hashSet3 = new HashSet<ushort>();
					string[] lines;
					lines = this.textBox1.Lines;
					foreach (string text3 in lines)
					{
						if (!(text3 == string.Empty))
						{
							int.TryParse(text3, out var result2);
							hashSet3.Add((ushort)result2);
						}
					}
				}
				catch (Exception)
				{
					return;
				}
				this.哈希表 = hashSet3;
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(420, 344);
			this.textBox1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(420, 344);
			base.Controls.Add(this.textBox1);
			base.Name = "哈希编辑器";
			this.Text = "哈希编辑器 关闭后自动生效";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(哈希编辑器_FormClosing);
			base.Load += new System.EventHandler(哈希编辑器_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
