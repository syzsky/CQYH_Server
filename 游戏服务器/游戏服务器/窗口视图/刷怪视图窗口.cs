using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraVerticalGrid.Rows;

namespace 游戏服务器.窗口视图
{
	public class 刷怪视图窗口 : RibbonForm
	{
		private IContainer components;

		private RibbonControl ribbon;

		private RibbonPage ribbonPage1;

		private RibbonPageGroup ribbonPageGroup1;

		private BarButtonItem SaveDataBaseButton;

		private TreeList treeList1;

		private TreeListColumn treeListColumn1;

		private GroupControl groupControl1;

		private SplitterControl splitterControl2;

		private PropertyGridControl propertyGrid;

		private PropertyDescriptionControl propertyDescriptionControl1;

		private SplitterControl splitterControl3;

		private BarButtonItem barButtonItem1;

		private BarButtonItem ReLoadDataBaseButton;

		public 刷怪视图窗口()
		{
			this.InitializeComponent();
			this.Init();
		}

		private void Init()
		{
			this.treeList1.Nodes.Clear();
			foreach (怪物刷新 item in 怪物刷新.数据表)
			{
				if (item.刷新列表 != null && item.刷新列表.Length != 0)
				{
					TreeListNode treeListNode;
					treeListNode = this.treeList1.Nodes.Add(item.ToString());
					treeListNode.Tag = item;
					刷新信息[] 刷新列表;
					刷新列表 = item.刷新列表;
					foreach (刷新信息 刷新信息 in 刷新列表)
					{
						treeListNode.Nodes.Add(刷新信息.ToString()).Tag = 刷新信息;
					}
				}
			}
			this.propertyGrid.SelectedObject = null;
		}

		private void treeList1_RowClick(object sender, RowClickEventArgs e)
		{
			this.propertyGrid.SelectedObject = null;
			if (e.Node != null && e.Node.Tag != null)
			{
				this.propertyGrid.SelectedObject = e.Node.Tag;
				this.propertyGrid.RetrieveFields();
			}
		}

		private void SaveDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			主程.添加系统日志("系统数据加载完成");
			this.Init();
		}

		private void propertyGrid_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
		{
		}

		private void propertyGrid_ShowCustomizationForm(object sender, EventArgs e)
		{
		}

		private void propertyGrid_ShownEditor(object sender, EventArgs e)
		{
		}

		private void propertyGrid_ValidateRecord(object sender, ValidateRecordEventArgs e)
		{
		}

		private void propertyGrid_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			BaseRow focusedRow;
			focusedRow = this.propertyGrid.FocusedRow;
			if (focusedRow == null)
			{
				e.Cancel = false;
			}
			if (focusedRow is EditorRow { Enabled: false })
			{
				e.Cancel = false;
			}
			RowProperties rowProperties;
			rowProperties = focusedRow.GetRowProperties(this.propertyGrid.FocusedRecordCellIndex);
			if (rowProperties == null || !rowProperties.Bindable || !rowProperties.AllowEdit)
			{
				e.Cancel = false;
			}
			if (rowProperties.RowType == typeof(List<怪物实例>))
			{
				rowProperties.ReadOnly = true;
			}
			RepositoryItem rowEdit;
			rowEdit = rowProperties.RowEdit;
			if (rowEdit != null && !rowEdit.Editable)
			{
				rowEdit.ReadOnly = true;
			}
		}

		private void propertyGrid_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
		{
		}

		private void propertyGrid_DoubleClick(object sender, EventArgs e)
		{
			e.GetType();
		}

		private void propertyGrid_CustomRowCreated(object sender, CustomRowCreatedEventArgs e)
		{
		}

		private void propertyGrid_HyperlinkClick(object sender, VGridHyperlinkClickEventArgs e)
		{
			e.GetType();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(刷怪视图窗口));
            ribbon = new RibbonControl();
            SaveDataBaseButton = new BarButtonItem();
            barButtonItem1 = new BarButtonItem();
            ReLoadDataBaseButton = new BarButtonItem();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            treeList1 = new TreeList();
            treeListColumn1 = new TreeListColumn();
            groupControl1 = new GroupControl();
            splitterControl2 = new SplitterControl();
            propertyGrid = new PropertyGridControl();
            propertyDescriptionControl1 = new PropertyDescriptionControl();
            splitterControl3 = new SplitterControl();
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)propertyGrid).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(35, 32, 35, 32);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, SaveDataBaseButton, barButtonItem1, ReLoadDataBaseButton });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 5;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.Size = new Size(780, 160);
            // 
            // SaveDataBaseButton
            // 
            SaveDataBaseButton.Caption = "保存 数据";
            SaveDataBaseButton.Id = 1;
            SaveDataBaseButton.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("SaveDataBaseButton.ImageOptions.SvgImage");
            SaveDataBaseButton.LargeWidth = 50;
            SaveDataBaseButton.Name = "SaveDataBaseButton";
            SaveDataBaseButton.ItemClick += SaveDataBaseButton_ItemClick;
            // 
            // barButtonItem1
            // 
            barButtonItem1.Caption = "重新加载";
            barButtonItem1.Id = 2;
            barButtonItem1.Name = "barButtonItem1";
            // 
            // ReLoadDataBaseButton
            // 
            ReLoadDataBaseButton.Caption = "重载 数据";
            ReLoadDataBaseButton.Id = 4;
            ReLoadDataBaseButton.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ReLoadDataBaseButton.ImageOptions.SvgImage");
            ReLoadDataBaseButton.LargeWidth = 50;
            ReLoadDataBaseButton.Name = "ReLoadDataBaseButton";
            ReLoadDataBaseButton.ItemClick += ReLoadDataBaseButton_ItemClick;
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
            ribbonPageGroup1.ItemLinks.Add(SaveDataBaseButton);
            ribbonPageGroup1.ItemLinks.Add(ReLoadDataBaseButton);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "动作";
            // 
            // treeList1
            // 
            treeList1.Columns.AddRange(new TreeListColumn[] { treeListColumn1 });
            treeList1.Dock = DockStyle.Left;
            treeList1.Location = new Point(0, 160);
            treeList1.MenuManager = ribbon;
            treeList1.Name = "treeList1";
            treeList1.Size = new Size(350, 332);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "怪物名字";
            treeListColumn1.FieldName = "技能名字";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.OptionsColumn.AllowEdit = false;
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(splitterControl2);
            groupControl1.Controls.Add(propertyGrid);
            groupControl1.Controls.Add(propertyDescriptionControl1);
            groupControl1.Dock = DockStyle.Fill;
            groupControl1.Location = new Point(360, 160);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(420, 332);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "配置怪物";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(2, 270);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(416, 10);
            splitterControl2.TabIndex = 22;
            splitterControl2.TabStop = false;
            // 
            // propertyGrid
            // 
            propertyGrid.Dock = DockStyle.Fill;
            propertyGrid.LayoutStyle = LayoutViewStyle.MultiRecordView;
            propertyGrid.Location = new Point(2, 23);
            propertyGrid.MenuManager = ribbon;
            propertyGrid.Name = "propertyGrid";
            propertyGrid.OptionsView.AllowReadOnlyRowAppearance = DefaultBoolean.True;
            propertyGrid.Size = new Size(416, 257);
            propertyGrid.TabIndex = 20;
            propertyGrid.CustomRowCreated += propertyGrid_CustomRowCreated;
            propertyGrid.HyperlinkClick += propertyGrid_HyperlinkClick;
            propertyGrid.ShowingEditor += propertyGrid_ShowingEditor;
            propertyGrid.ShownEditor += propertyGrid_ShownEditor;
            propertyGrid.CellValueChanging += propertyGrid_CellValueChanging;
            propertyGrid.ValidatingEditor += propertyGrid_ValidatingEditor;
            propertyGrid.ShowCustomizationForm += propertyGrid_ShowCustomizationForm;
            propertyGrid.ValidateRecord += propertyGrid_ValidateRecord;
            propertyGrid.DoubleClick += propertyGrid_DoubleClick;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(2, 280);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(416, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 160);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 332);
            splitterControl3.TabIndex = 22;
            splitterControl3.TabStop = false;
            // 
            // 刷怪视图窗口
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 492);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "刷怪视图窗口";
            Ribbon = ribbon;
            Text = "刷怪配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((ISupportInitialize)propertyGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
