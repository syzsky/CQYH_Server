using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using 游戏服务器.模板类;
using DevExpress.Data.Mask;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid;

namespace 游戏服务器.窗口视图
{
    public class BuffInfoView : RibbonForm
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

        private BarButtonItem ReLoadDataBaseButton;

        private BarButtonItem barButtonItem1;

        private BarEditItem barEditItem1;

        private RepositoryItemTextEdit repositoryItemTextEdit1;

        private BarEditItem barEditItem2;

        private RepositoryItemTextEdit repositoryItemTextEdit2;

        private BarButtonItem barButtonItem2;

        private RepositoryItemButtonEdit repositoryItemButtonEdit1;

        private PopupMenu popupMenu1;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit1;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit2;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit3;

        private BarButtonItem BuffAddButton;

        private BarEditItem BuffNameEdit;

        private BarButtonItem BuffDeleteButton;

        private RepositoryItemSpinEdit repositoryItemSpinEdit2;

        private RepositoryItemTextEdit repositoryItemTextEdit3;

        private RepositoryItemSpinEdit repositoryItemSpinEdit1;

        private BarEditItem BuffIndexEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit4;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit4;

        public BuffInfoView()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.treeList1.Nodes.Clear();
            foreach (KeyValuePair<ushort, 游戏Buff> item in 游戏Buff.数据表)
            {
                this.treeList1.Nodes.Add(item.Value.ToString()).Tag = item.Value;
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
            游戏Buff.保存数据();
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
                this.treeList1.ContextMenuStrip = null;
                TreeListNode node;
                node = this.treeList1.CalcHitInfo(new Point(e.X, e.Y)).Node;
                this.treeList1.FocusedNode = node;
                if (node != null)
                {
                    this.popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void BuffAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            ushort num;
            num = (ushort)(int)this.BuffIndexEdit.EditValue;
            string buff名字;
            buff名字 = (string)this.BuffNameEdit.EditValue;
            游戏Buff.数据表.Add(num, new 游戏Buff
            {
                Buff编号 = num,
                Buff名字 = buff名字
            });
            this.Init();
        }

        private void BuffDeleteButton_ItemClick(object sender, ItemClickEventArgs e)
        {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(BuffInfoView));
            EditorButtonImageOptions editorButtonImageOptions1 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject1 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject2 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject3 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject4 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions2 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject5 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject6 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject7 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject8 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions3 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject9 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject10 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject11 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject12 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions4 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject13 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject14 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject15 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject16 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions5 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject17 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject18 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject19 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject20 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions6 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject21 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject22 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject23 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject24 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions7 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject25 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject26 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject27 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject28 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions8 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject29 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject30 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject31 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject32 = new SerializableAppearanceObject();
            ribbon = new RibbonControl();
            SaveDataBaseButton = new BarButtonItem();
            ReLoadDataBaseButton = new BarButtonItem();
            BuffAddButton = new BarButtonItem();
            BuffNameEdit = new BarEditItem();
            repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            BuffDeleteButton = new BarButtonItem();
            BuffIndexEdit = new BarEditItem();
            repositoryItemTextEdit4 = new RepositoryItemTextEdit();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
            repositoryItemTextEdit2 = new RepositoryItemTextEdit();
            repositoryItemTextEdit3 = new RepositoryItemTextEdit();
            repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit2 = new RepositoryItemSpinEdit();
            treeList1 = new TreeList();
            treeListColumn1 = new TreeListColumn();
            groupControl1 = new GroupControl();
            splitterControl2 = new SplitterControl();
            propertyGridControl1 = new PropertyGridControl();
            propertyDescriptionControl1 = new PropertyDescriptionControl();
            splitterControl3 = new SplitterControl();
            popupMenu1 = new PopupMenu(components);
            repositoryItemRibbonSearchEdit1 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit2 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit3 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit4 = new RepositoryItemRibbonSearchEdit();
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit4).BeginInit();
            ((ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemSpinEdit2).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)propertyGridControl1).BeginInit();
            ((ISupportInitialize)popupMenu1).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(35, 32, 35, 32);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, SaveDataBaseButton, ReLoadDataBaseButton, BuffAddButton, BuffNameEdit, BuffDeleteButton, BuffIndexEdit });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 14;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemTextEdit1, repositoryItemTextEdit2, repositoryItemTextEdit3, repositoryItemSpinEdit1, repositoryItemSpinEdit2, repositoryItemTextEdit4 });
            ribbon.Size = new Size(724, 160);
            // 
            // SaveDataBaseButton
            // 
            SaveDataBaseButton.Caption = "保存 数据";
            SaveDataBaseButton.Id = 1;
            SaveDataBaseButton.ImageOptions.SvgImage = (SvgImage)resources.GetObject("SaveDataBaseButton.ImageOptions.SvgImage");
            SaveDataBaseButton.LargeWidth = 50;
            SaveDataBaseButton.Name = "SaveDataBaseButton";
            SaveDataBaseButton.ItemClick += SaveDataBaseButton_ItemClick;
            // 
            // ReLoadDataBaseButton
            // 
            ReLoadDataBaseButton.Caption = "重载 数据";
            ReLoadDataBaseButton.Id = 4;
            ReLoadDataBaseButton.ImageOptions.SvgImage = (SvgImage)resources.GetObject("ReLoadDataBaseButton.ImageOptions.SvgImage");
            ReLoadDataBaseButton.LargeWidth = 50;
            ReLoadDataBaseButton.Name = "ReLoadDataBaseButton";
            ReLoadDataBaseButton.ItemClick += ReLoadDataBaseButton_ItemClick;
            // 
            // BuffAddButton
            // 
            BuffAddButton.Caption = "添加Buff";
            BuffAddButton.Id = 5;
            BuffAddButton.Name = "BuffAddButton";
            BuffAddButton.ItemClick += BuffAddButton_ItemClick;
            // 
            // BuffNameEdit
            // 
            BuffNameEdit.Caption = "名字";
            BuffNameEdit.Edit = repositoryItemTextEdit1;
            BuffNameEdit.Id = 7;
            BuffNameEdit.Name = "BuffNameEdit";
            // 
            // repositoryItemTextEdit1
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // BuffDeleteButton
            // 
            BuffDeleteButton.Caption = "删除";
            BuffDeleteButton.Id = 9;
            BuffDeleteButton.Name = "BuffDeleteButton";
            BuffDeleteButton.ItemClick += BuffDeleteButton_ItemClick;
            // 
            // BuffIndexEdit
            // 
            BuffIndexEdit.Caption = "编号";
            BuffIndexEdit.Edit = repositoryItemTextEdit4;
            BuffIndexEdit.Id = 13;
            BuffIndexEdit.Name = "BuffIndexEdit";
            // 
            // repositoryItemTextEdit4
            // 
            repositoryItemTextEdit4.AutoHeight = false;
            repositoryItemTextEdit4.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit4.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            repositoryItemTextEdit4.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
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
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.AutoHeight = false;
            repositoryItemButtonEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            repositoryItemTextEdit2.AutoHeight = false;
            repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTextEdit3
            // 
            repositoryItemTextEdit3.AutoHeight = false;
            repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // repositoryItemSpinEdit1
            // 
            repositoryItemSpinEdit1.AutoHeight = false;
            repositoryItemSpinEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // repositoryItemSpinEdit2
            // 
            repositoryItemSpinEdit2.AutoHeight = false;
            repositoryItemSpinEdit2.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // treeList1
            // 
            treeList1.Columns.AddRange(new TreeListColumn[] { treeListColumn1 });
            treeList1.Dock = DockStyle.Left;
            treeList1.Location = new Point(0, 160);
            treeList1.MenuManager = ribbon;
            treeList1.Name = "treeList1";
            treeList1.Size = new Size(350, 304);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            treeList1.MouseUp += treeList1_MouseUp;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "Buff名字";
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
            groupControl1.Size = new Size(364, 304);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "配置Buff";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(2, 242);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(360, 10);
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
            propertyGridControl1.Size = new Size(360, 229);
            propertyGridControl1.TabIndex = 20;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(2, 252);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(360, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 160);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 304);
            splitterControl3.TabIndex = 22;
            splitterControl3.TabStop = false;
            // 
            // popupMenu1
            // 
            popupMenu1.ItemLinks.Add(BuffDeleteButton);
            popupMenu1.ItemLinks.Add(BuffAddButton);
            popupMenu1.ItemLinks.Add(BuffNameEdit);
            popupMenu1.ItemLinks.Add(BuffIndexEdit);
            popupMenu1.Name = "popupMenu1";
            popupMenu1.Ribbon = ribbon;
            // 
            // repositoryItemRibbonSearchEdit1
            // 
            repositoryItemRibbonSearchEdit1.AllowFocused = false;
            repositoryItemRibbonSearchEdit1.AutoHeight = false;
            repositoryItemRibbonSearchEdit1.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions1.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions1, new KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions2, new KeyShortcut(Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit1.Name = "repositoryItemRibbonSearchEdit1";
            repositoryItemRibbonSearchEdit1.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit2
            // 
            repositoryItemRibbonSearchEdit2.AllowFocused = false;
            repositoryItemRibbonSearchEdit2.AutoHeight = false;
            repositoryItemRibbonSearchEdit2.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions3.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit2.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions3, new KeyShortcut(Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions4, new KeyShortcut(Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit2.Name = "repositoryItemRibbonSearchEdit2";
            repositoryItemRibbonSearchEdit2.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit3
            // 
            repositoryItemRibbonSearchEdit3.AllowFocused = false;
            repositoryItemRibbonSearchEdit3.AutoHeight = false;
            repositoryItemRibbonSearchEdit3.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions5.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit3.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions5, new KeyShortcut(Keys.None), serializableAppearanceObject17, serializableAppearanceObject18, serializableAppearanceObject19, serializableAppearanceObject20, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions6, new KeyShortcut(Keys.None), serializableAppearanceObject21, serializableAppearanceObject22, serializableAppearanceObject23, serializableAppearanceObject24, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit3.Name = "repositoryItemRibbonSearchEdit3";
            repositoryItemRibbonSearchEdit3.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit4
            // 
            repositoryItemRibbonSearchEdit4.AllowFocused = false;
            repositoryItemRibbonSearchEdit4.AutoHeight = false;
            repositoryItemRibbonSearchEdit4.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions7.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit4.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions7, new KeyShortcut(Keys.None), serializableAppearanceObject25, serializableAppearanceObject26, serializableAppearanceObject27, serializableAppearanceObject28, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions8, new KeyShortcut(Keys.None), serializableAppearanceObject29, serializableAppearanceObject30, serializableAppearanceObject31, serializableAppearanceObject32, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit4.Name = "repositoryItemRibbonSearchEdit4";
            repositoryItemRibbonSearchEdit4.NullText = "Search";
            // 
            // BuffInfoView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 464);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "BuffInfoView";
            Ribbon = ribbon;
            Text = "Buff配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit4).EndInit();
            ((ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit2).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit3).EndInit();
            ((ISupportInitialize)repositoryItemSpinEdit1).EndInit();
            ((ISupportInitialize)repositoryItemSpinEdit2).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((ISupportInitialize)propertyGridControl1).EndInit();
            ((ISupportInitialize)popupMenu1).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
