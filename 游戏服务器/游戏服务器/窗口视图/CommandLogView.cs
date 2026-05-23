using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Clipboard = System.Windows.Forms.Clipboard;

namespace 游戏服务器.窗口视图
{
	public class CommandLogView : RibbonForm
	{
		public static BindingList<string> Logs = new BindingList<string>();

		private IContainer components;

		private RibbonControl ribbon;

		private RibbonPage ribbonPage1;

		private RibbonPageGroup ribbonPageGroup1;

		private BarButtonItem ClearLogsButton;

		private ListBoxControl LogListBoxControl;

		private Timer InterfaceTimer;

		public CommandLogView()
		{
			this.InitializeComponent();
			this.LogListBoxControl.DataSource = CommandLogView.Logs;
			this.LogListBoxControl.KeyDown += LogListBoxControl_KeyDown;
		}

		private void LogListBoxControl_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.C && this.LogListBoxControl.SelectedItem != null)
				{
					string text;
					text = this.LogListBoxControl.SelectedItem.ToString().Trim();
					int num;
					num = text.IndexOf('@');
					Clipboard.SetText((num == -1) ? text : text.Substring(num));
				}
			}
			catch
			{
			}
		}

		private void InterfaceTimer_Tick(object sender, EventArgs e)
		{
			while (!主程.DisplayCommandLogs.IsEmpty)
			{
				if (主程.DisplayCommandLogs.TryDequeue(out var result))
				{
					CommandLogView.Logs.Add(result);
				}
			}
			if (CommandLogView.Logs.Count > 0)
			{
				this.ClearLogsButton.Enabled = true;
			}
		}

		private void ClearLogsButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			CommandLogView.Logs.Clear();
			this.ClearLogsButton.Enabled = false;
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(CommandLogView));
            ribbon = new RibbonControl();
            ClearLogsButton = new BarButtonItem();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            LogListBoxControl = new ListBoxControl();
            InterfaceTimer = new Timer(components);
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)LogListBoxControl).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(35, 32, 35, 32);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, ClearLogsButton });
            ribbon.Location = new System.Drawing.Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 2;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.Size = new System.Drawing.Size(836, 160);
            // 
            // ClearLogsButton
            // 
            ClearLogsButton.Caption = "清空 日志";
            ClearLogsButton.Id = 1;
            ClearLogsButton.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ClearLogsButton.ImageOptions.SvgImage");
            ClearLogsButton.LargeWidth = 50;
            ClearLogsButton.Name = "ClearLogsButton";
            ClearLogsButton.ItemClick += ClearLogsButton_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "主页";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.AllowTextClipping = false;
            ribbonPageGroup1.CaptionButtonVisible = DefaultBoolean.False;
            ribbonPageGroup1.ItemLinks.Add(ClearLogsButton);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "动作";
            // 
            // LogListBoxControl
            // 
            LogListBoxControl.Dock = DockStyle.Fill;
            LogListBoxControl.ItemAutoHeight = true;
            LogListBoxControl.Location = new System.Drawing.Point(0, 160);
            LogListBoxControl.Margin = new Padding(4, 3, 4, 3);
            LogListBoxControl.Name = "LogListBoxControl";
            LogListBoxControl.Size = new System.Drawing.Size(836, 360);
            LogListBoxControl.TabIndex = 1;
            // 
            // InterfaceTimer
            // 
            InterfaceTimer.Enabled = true;
            InterfaceTimer.Interval = 1000;
            InterfaceTimer.Tick += InterfaceTimer_Tick;
            // 
            // CommandLogView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(836, 520);
            Controls.Add(LogListBoxControl);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "CommandLogView";
            Ribbon = ribbon;
            Text = "命令日志";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)LogListBoxControl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
