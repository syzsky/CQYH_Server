using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using 游戏服务器.模板类;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid;

namespace 游戏服务器.窗口视图
{
    public class ItemInfoView : RibbonForm
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

        private PropertyGridControl propertyGridControl1;

        private PropertyDescriptionControl propertyDescriptionControl1;

        private SplitterControl splitterControl3;

        private BarButtonItem barButtonItem1;

        private BarButtonItem ReLoadDataBaseButton;

        public ItemInfoView()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.treeList1.Nodes.Clear();
            string[] names;
            names = Enum.GetNames(typeof(物品使用分类));
            foreach (string text in names)
            {
                TreeListNode treeListNode;
                treeListNode = this.treeList1.Nodes.Add(text);
                foreach (KeyValuePair<int, 游戏物品> item in 游戏物品.数据表)
                {
                    if (item.Value.物品分类.ToString() == text)
                    {
                        treeListNode.Nodes.Add(item.Value.ToString()).Tag = item.Value;
                    }
                }
            }
            this.propertyGridControl1.SelectedObject = null;
        }

        private void treeList1_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                this.propertyGridControl1.SelectedObject = e.Node.Tag;
            }
        }

        private void SaveDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            游戏物品.保存数据();
        }

        private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统数据网关.加载数据(5);
            /*
                   typeof(游戏物品),
                    typeof(随机属性),
                    typeof(装备属性),
                    typeof(游戏套装)
             */
            主程.添加系统日志("系统数据加载完成");
            this.Init();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ItemInfoView));
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
            propertyGridControl1 = new PropertyGridControl();
            propertyDescriptionControl1 = new PropertyDescriptionControl();
            splitterControl3 = new SplitterControl();
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)propertyGridControl1).BeginInit();
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
            ribbon.Size = new Size(822, 160);
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
            treeList1.Size = new Size(350, 353);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "物品名字";
            treeListColumn1.FieldName = "技能名字";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.OptionsColumn.AllowEdit = false;
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(splitterControl2);
            groupControl1.Controls.Add(propertyGridControl1);
            groupControl1.Controls.Add(propertyDescriptionControl1);
            groupControl1.Dock = DockStyle.Fill;
            groupControl1.Location = new Point(360, 160);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(462, 353);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "配置物品";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(2, 291);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(458, 10);
            splitterControl2.TabIndex = 22;
            splitterControl2.TabStop = false;
            // 
            // propertyGridControl1
            // 
            propertyGridControl1.Dock = DockStyle.Fill;
            propertyGridControl1.Location = new Point(2, 23);
            propertyGridControl1.MenuManager = ribbon;
            propertyGridControl1.Name = "propertyGridControl1";
            propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DefaultBoolean.True;
            propertyGridControl1.Size = new Size(458, 278);
            propertyGridControl1.TabIndex = 20;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(2, 301);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(458, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 160);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 353);
            splitterControl3.TabIndex = 22;
            splitterControl3.TabStop = false;
            // 
            // ItemInfoView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(822, 513);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ItemInfoView";
            Ribbon = ribbon;
            Text = "物品配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((ISupportInitialize)propertyGridControl1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
