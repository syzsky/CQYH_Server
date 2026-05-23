using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using 游戏服务器.模板类;
using DevExpress.Data.Mask;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid;

namespace 游戏服务器.窗口视图
{
    public class RuneInfoView : RibbonForm
    {
        private TreeListNode _selectNode;

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

        private PopupMenu RuneMenu;

        private BarButtonItem RuneDeleteButton;

        private BarEditItem RuneNameEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit1;

        private BarEditItem RuneIndexEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit2;

        private BarEditItem RuneRuneEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit3;

        private BarButtonItem RuneAddButton;

        private BarHeaderItem RuneHeader;

        private BarHeaderItem RuneAddHeader;

        public RuneInfoView()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.treeList1.Nodes.Clear();
            string[] names;
            names = Enum.GetNames(typeof(游戏对象职业));
            foreach (string text in names)
            {
                TreeListNode treeListNode;
                treeListNode = this.treeList1.Nodes.Add(text);
                treeListNode.Tag = Enum.Parse(typeof(游戏对象职业), text);
                foreach (KeyValuePair<ushort, 铭文技能> item in 铭文技能.数据表)
                {
                    if (item.Value.技能职业.ToString() == text)
                    {
                        treeListNode.Nodes.Add(item.Value.ToString()).Tag = item.Value;
                    }
                }
            }
            this.propertyGridControl1.SelectedObject = null;
        }

        private void treeList1_RowClick(object sender, RowClickEventArgs e)
        {
            this._selectNode = e.Node;
            if (e.Node != null && e.Node.Tag != null)
            {
                this.propertyGridControl1.SelectedObject = e.Node.Tag;
            }
        }

        private void SaveDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            铭文技能.保存数据();
        }

        private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统数据网关.加载数据(3);
            主程.添加系统日志("系统数据加载完成");
            this.Init();
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this._selectNode != null && this._selectNode.Tag is 铭文技能 铭文技能)
                {
                    this.RuneDeleteButton.Enabled = true;
                    this.RuneHeader.Caption = 铭文技能.技能名字;
                }
                else
                {
                    this.RuneDeleteButton.Enabled = false;
                    this.RuneHeader.Caption = "未选中";
                }
                TreeListNode treeListNode;
                treeListNode = ((this._selectNode == null) ? null : ((this._selectNode.Tag is 游戏对象职业) ? this._selectNode : this._selectNode.ParentNode));
                if (treeListNode == null)
                {
                    this.RuneAddButton.Enabled = false;
                    this.RuneAddHeader.Caption = "请选中职业分组后添加";
                }
                else
                {
                    this.RuneAddButton.Enabled = true;
                    this.RuneAddHeader.Caption = "新建职业技能: " + (游戏对象职业)treeListNode.Tag;
                }
                this.RuneMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void RuneAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode treeListNode;
            treeListNode = ((this._selectNode == null) ? null : ((this._selectNode.Tag is 游戏对象职业) ? this._selectNode : this._selectNode.ParentNode));
            if (treeListNode != null)
            {
                string 技能名字;
                技能名字 = (string)this.RuneNameEdit.EditValue;
                int num;
                num = (int)this.RuneIndexEdit.EditValue;
                int num2;
                num2 = (int)this.RuneRuneEdit.EditValue;
                if (游戏服务器.模板类.铭文技能.数据表.ContainsKey((ushort)(num * 10 + num2)))
                {
                    XtraMessageBox.Show("技能名称不可重复", "重复的名称", MessageBoxButtons.OK);
                    return;
                }
                铭文技能 铭文技能;
                铭文技能 = new 铭文技能
                {
                    技能名字 = 技能名字,
                    技能编号 = (ushort)num,
                    铭文编号 = (byte)num2,
                    技能职业 = (游戏对象职业)treeListNode.Tag
                };
                铭文技能.数据表.Add((ushort)(num * 10 + num2), 铭文技能);
                treeListNode.Nodes.Add(铭文技能.ToString()).Tag = 铭文技能;
                this.RuneNameEdit.EditValue = "";
                this.RuneIndexEdit.EditValue = 0;
                this.RuneRuneEdit.EditValue = 0;
                XtraMessageBox.Show("添加成功", "", MessageBoxButtons.OK);
            }
        }

        private void RuneDeleteButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this._selectNode != null && this._selectNode.Tag is 铭文技能 铭文技能)
            {
                铭文技能.数据表.Remove((ushort)(铭文技能.技能编号 * 10 + 铭文技能.铭文编号));
                this.treeList1.DeleteNode(this._selectNode);
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RuneInfoView));
            ribbon = new RibbonControl();
            SaveDataBaseButton = new BarButtonItem();
            barButtonItem1 = new BarButtonItem();
            ReLoadDataBaseButton = new BarButtonItem();
            RuneDeleteButton = new BarButtonItem();
            RuneNameEdit = new BarEditItem();
            repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            RuneIndexEdit = new BarEditItem();
            repositoryItemTextEdit2 = new RepositoryItemTextEdit();
            RuneRuneEdit = new BarEditItem();
            repositoryItemTextEdit3 = new RepositoryItemTextEdit();
            RuneAddButton = new BarButtonItem();
            RuneHeader = new BarHeaderItem();
            RuneAddHeader = new BarHeaderItem();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            treeList1 = new TreeList();
            treeListColumn1 = new TreeListColumn();
            groupControl1 = new GroupControl();
            splitterControl2 = new SplitterControl();
            propertyGridControl1 = new PropertyGridControl();
            propertyDescriptionControl1 = new PropertyDescriptionControl();
            splitterControl3 = new SplitterControl();
            RuneMenu = new PopupMenu(components);
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit3).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)propertyGridControl1).BeginInit();
            ((ISupportInitialize)RuneMenu).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(35, 32, 35, 32);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, SaveDataBaseButton, barButtonItem1, ReLoadDataBaseButton, RuneDeleteButton, RuneNameEdit, RuneIndexEdit, RuneRuneEdit, RuneAddButton, RuneHeader, RuneAddHeader });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 12;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemTextEdit1, repositoryItemTextEdit2, repositoryItemTextEdit3 });
            ribbon.Size = new Size(794, 160);
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
            // RuneDeleteButton
            // 
            RuneDeleteButton.Caption = "删除";
            RuneDeleteButton.Id = 5;
            RuneDeleteButton.Name = "RuneDeleteButton";
            RuneDeleteButton.ItemClick += RuneDeleteButton_ItemClick;
            // 
            // RuneNameEdit
            // 
            RuneNameEdit.Caption = "名字";
            RuneNameEdit.Edit = repositoryItemTextEdit1;
            RuneNameEdit.Id = 6;
            RuneNameEdit.Name = "RuneNameEdit";
            // 
            // repositoryItemTextEdit1
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // RuneIndexEdit
            // 
            RuneIndexEdit.Caption = "编号";
            RuneIndexEdit.Edit = repositoryItemTextEdit2;
            RuneIndexEdit.EditValue = 0;
            RuneIndexEdit.Id = 7;
            RuneIndexEdit.Name = "RuneIndexEdit";
            // 
            // repositoryItemTextEdit2
            // 
            repositoryItemTextEdit2.AutoHeight = false;
            repositoryItemTextEdit2.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit2.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            repositoryItemTextEdit2.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // RuneRuneEdit
            // 
            RuneRuneEdit.Caption = "铭文";
            RuneRuneEdit.Edit = repositoryItemTextEdit3;
            RuneRuneEdit.EditValue = 0;
            RuneRuneEdit.Id = 8;
            RuneRuneEdit.Name = "RuneRuneEdit";
            // 
            // repositoryItemTextEdit3
            // 
            repositoryItemTextEdit3.AutoHeight = false;
            repositoryItemTextEdit3.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit3.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            repositoryItemTextEdit3.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // RuneAddButton
            // 
            RuneAddButton.Caption = "添加铭文";
            RuneAddButton.Id = 9;
            RuneAddButton.Name = "RuneAddButton";
            RuneAddButton.ItemClick += RuneAddButton_ItemClick;
            // 
            // RuneHeader
            // 
            RuneHeader.Caption = "barHeaderItem1";
            RuneHeader.Id = 10;
            RuneHeader.Name = "RuneHeader";
            // 
            // RuneAddHeader
            // 
            RuneAddHeader.Caption = "barHeaderItem2";
            RuneAddHeader.Id = 11;
            RuneAddHeader.Name = "RuneAddHeader";
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
            treeList1.OptionsMenu.EnableNodeMenu = false;
            treeList1.Size = new Size(350, 339);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            treeList1.MouseUp += treeList1_MouseUp;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "铭文名字";
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
            groupControl1.Size = new Size(434, 339);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "配置铭文";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(2, 277);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(430, 10);
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
            propertyGridControl1.Size = new Size(430, 264);
            propertyGridControl1.TabIndex = 20;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(2, 287);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(430, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 160);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 339);
            splitterControl3.TabIndex = 22;
            splitterControl3.TabStop = false;
            // 
            // RuneMenu
            // 
            RuneMenu.ItemLinks.Add(RuneHeader);
            RuneMenu.ItemLinks.Add(RuneDeleteButton);
            RuneMenu.ItemLinks.Add(RuneAddHeader);
            RuneMenu.ItemLinks.Add(RuneAddButton);
            RuneMenu.ItemLinks.Add(RuneNameEdit);
            RuneMenu.ItemLinks.Add(RuneIndexEdit);
            RuneMenu.ItemLinks.Add(RuneRuneEdit);
            RuneMenu.Name = "RuneMenu";
            RuneMenu.Ribbon = ribbon;
            // 
            // RuneInfoView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 499);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "RuneInfoView";
            Ribbon = ribbon;
            Text = "铭文配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit2).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit3).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((ISupportInitialize)propertyGridControl1).EndInit();
            ((ISupportInitialize)RuneMenu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
