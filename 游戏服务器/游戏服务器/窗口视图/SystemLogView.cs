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
	public class SystemLogView : RibbonForm
	{
		public static BindingList<string> Logs = new BindingList<string>();

		public int LogTopIndex;

		private IContainer components;

		private RibbonControl ribbon;

		private RibbonPage ribbonPage1;

		private RibbonPageGroup ribbonPageGroup1;

		private BarButtonItem ClearLogsButton;

		private ListBoxControl LogListBoxControl;

		private Timer InterfaceTimer;

		public SystemLogView()
		{
			this.InitializeComponent();
			this.LogListBoxControl.DataSource = SystemLogView.Logs;
		}

		private void InterfaceTimer_Tick(object sender, EventArgs e)
		{
			int num;
			num = 0;
			while (!主程.DisplayLogs.IsEmpty && num < 100)
			{
				if (主程.DisplayLogs.TryDequeue(out var result))
				{
					SystemLogView.Logs.Add(result);
					num++;
				}
			}
			if (SystemLogView.Logs.Count > 0)
			{
				this.ClearLogsButton.Enabled = true;
			}
			if (this.LogTopIndex != SystemLogView.Logs.Count)
			{
				this.LogListBoxControl.TopIndex = SystemLogView.Logs.Count - 1;
				this.LogTopIndex = SystemLogView.Logs.Count;
			}
		}

		private void ClearLogsButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			SystemLogView.Logs.Clear();
			this.ClearLogsButton.Enabled = false;
		}

		private void LogListBoxControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.LogListBoxControl.SelectedIndex >= 0)
			{
				Clipboard.SetText(this.LogListBoxControl.SelectedValue.ToString());
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SystemLogView));
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
            ribbon.Size = new System.Drawing.Size(822, 160);
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
            LogListBoxControl.Size = new System.Drawing.Size(822, 353);
            LogListBoxControl.TabIndex = 1;
            LogListBoxControl.MouseDoubleClick += LogListBoxControl_MouseDoubleClick;
            // 
            // InterfaceTimer
            // 
            InterfaceTimer.Enabled = true;
            InterfaceTimer.Interval = 1000;
            InterfaceTimer.Tick += InterfaceTimer_Tick;
            // 
            // SystemLogView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(822, 513);
            Controls.Add(LogListBoxControl);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SystemLogView";
            Ribbon = ribbon;
            Text = "系统消息";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)LogListBoxControl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
