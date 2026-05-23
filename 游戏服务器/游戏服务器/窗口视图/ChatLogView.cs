using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace 游戏服务器.窗口视图
{
	public class ChatLogView : RibbonForm
	{
		public static BindingList<string> Logs = new BindingList<string>();

		private IContainer components;

		private RibbonControl ribbon;

		private RibbonPage ribbonPage1;

		private RibbonPageGroup ribbonPageGroup1;

		private BarButtonItem ClearLogsButton;

		private ListBoxControl LogListBoxControl;

		private Timer InterfaceTimer;

		public ChatLogView()
		{
			this.InitializeComponent();
			this.LogListBoxControl.DataSource = ChatLogView.Logs;
		}

		private void InterfaceTimer_Tick(object sender, EventArgs e)
		{
			while (!主程.DisplayChatLogs.IsEmpty)
			{
				if (主程.DisplayChatLogs.TryDequeue(out var result))
				{
					ChatLogView.Logs.Add(result);
				}
			}
			if (ChatLogView.Logs.Count > 0)
			{
				this.ClearLogsButton.Enabled = true;
			}
		}

		private void ClearLogsButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			ChatLogView.Logs.Clear();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ChatLogView));
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
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 2;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.Size = new Size(836, 160);
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
            LogListBoxControl.Location = new Point(0, 160);
            LogListBoxControl.Margin = new Padding(4, 3, 4, 3);
            LogListBoxControl.Name = "LogListBoxControl";
            LogListBoxControl.Size = new Size(836, 360);
            LogListBoxControl.TabIndex = 1;
            // 
            // InterfaceTimer
            // 
            InterfaceTimer.Enabled = true;
            InterfaceTimer.Interval = 1000;
            InterfaceTimer.Tick += InterfaceTimer_Tick;
            // 
            // ChatLogView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 520);
            Controls.Add(LogListBoxControl);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ChatLogView";
            Ribbon = ribbon;
            Text = "聊天日志";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)LogListBoxControl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
