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
using DevExpress.XtraVerticalGrid;

namespace 游戏服务器.窗口视图
{
    public class 守卫视图窗口 : RibbonForm
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

        private BarButtonItem barButtonItem3;

        public 守卫视图窗口()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.treeList1.Nodes.Clear();
            foreach (守卫刷新 item in 守卫刷新.数据表)
            {
                this.treeList1.Nodes.Add(item.ToString()).Tag = item;
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

        private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统数据网关.加载数据(8);
            /*
             typeof(守卫刷新)
             */
            主程.添加系统日志("守卫刷新数据加载完成");
            this.Init();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            守卫刷新.保存数据();
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
            EditorButtonImageOptions editorButtonImageOptions9 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject33 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject34 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject35 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject36 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions10 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject37 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject38 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject39 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject40 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions11 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject41 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject42 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject43 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject44 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions12 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject45 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject46 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject47 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject48 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions13 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject49 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject50 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject51 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject52 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions14 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject53 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject54 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject55 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject56 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions15 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject57 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject58 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject59 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject60 = new SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions16 = new EditorButtonImageOptions();
            SerializableAppearanceObject serializableAppearanceObject61 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject62 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject63 = new SerializableAppearanceObject();
            SerializableAppearanceObject serializableAppearanceObject64 = new SerializableAppearanceObject();
            ribbon = new RibbonControl();
            SaveDataBaseButton = new BarButtonItem();
            ReLoadDataBaseButton = new BarButtonItem();
            BuffAddButton = new BarButtonItem();
            BuffNameEdit = new BarEditItem();
            repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            BuffDeleteButton = new BarButtonItem();
            BuffIndexEdit = new BarEditItem();
            repositoryItemTextEdit4 = new RepositoryItemTextEdit();
            barButtonItem3 = new BarButtonItem();
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
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, SaveDataBaseButton, ReLoadDataBaseButton, BuffAddButton, BuffNameEdit, BuffDeleteButton, BuffIndexEdit, barButtonItem3 });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 18;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemTextEdit1, repositoryItemTextEdit2, repositoryItemTextEdit3, repositoryItemSpinEdit1, repositoryItemSpinEdit2, repositoryItemTextEdit4 });
            ribbon.Size = new Size(290, 32);
            // 
            // SaveDataBaseButton
            // 
            SaveDataBaseButton.Id = 14;
            SaveDataBaseButton.Name = "SaveDataBaseButton";
            // 
            // ReLoadDataBaseButton
            // 
            ReLoadDataBaseButton.Caption = "重载 数据";
            ReLoadDataBaseButton.Id = 4;
            ReLoadDataBaseButton.LargeWidth = 50;
            ReLoadDataBaseButton.Name = "ReLoadDataBaseButton";
            ReLoadDataBaseButton.ItemClick += ReLoadDataBaseButton_ItemClick;
            // 
            // BuffAddButton
            // 
            BuffAddButton.Id = 17;
            BuffAddButton.Name = "BuffAddButton";
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
            BuffDeleteButton.Id = 15;
            BuffDeleteButton.Name = "BuffDeleteButton";
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
            // barButtonItem3
            // 
            barButtonItem3.Caption = "保存";
            barButtonItem3.Id = 16;
            barButtonItem3.Name = "barButtonItem3";
            barButtonItem3.ItemClick += barButtonItem3_ItemClick;
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
            ribbonPageGroup1.ItemLinks.Add(ReLoadDataBaseButton);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem3);
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
            treeList1.Location = new Point(0, 32);
            treeList1.MenuManager = ribbon;
            treeList1.Name = "treeList1";
            treeList1.Size = new Size(350, 215);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "守卫列表";
            treeListColumn1.FieldName = "守卫列表";
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
            groupControl1.Location = new Point(360, 32);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(0, 215);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "守卫配置";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(1, 153);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(0, 10);
            splitterControl2.TabIndex = 22;
            splitterControl2.TabStop = false;
            // 
            // propertyGridControl1
            // 
            propertyGridControl1.Dock = DockStyle.Fill;
            propertyGridControl1.Location = new Point(1, 23);
            propertyGridControl1.MenuManager = ribbon;
            propertyGridControl1.Name = "propertyGridControl1";
            propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DefaultBoolean.True;
            propertyGridControl1.Size = new Size(0, 140);
            propertyGridControl1.TabIndex = 20;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(1, 163);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(0, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 32);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 215);
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
            editorButtonImageOptions9.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions9, new KeyShortcut(Keys.None), serializableAppearanceObject33, serializableAppearanceObject34, serializableAppearanceObject35, serializableAppearanceObject36, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions10, new KeyShortcut(Keys.None), serializableAppearanceObject37, serializableAppearanceObject38, serializableAppearanceObject39, serializableAppearanceObject40, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit1.Name = "repositoryItemRibbonSearchEdit1";
            repositoryItemRibbonSearchEdit1.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit2
            // 
            repositoryItemRibbonSearchEdit2.AllowFocused = false;
            repositoryItemRibbonSearchEdit2.AutoHeight = false;
            repositoryItemRibbonSearchEdit2.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions11.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit2.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions11, new KeyShortcut(Keys.None), serializableAppearanceObject41, serializableAppearanceObject42, serializableAppearanceObject43, serializableAppearanceObject44, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions12, new KeyShortcut(Keys.None), serializableAppearanceObject45, serializableAppearanceObject46, serializableAppearanceObject47, serializableAppearanceObject48, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit2.Name = "repositoryItemRibbonSearchEdit2";
            repositoryItemRibbonSearchEdit2.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit3
            // 
            repositoryItemRibbonSearchEdit3.AllowFocused = false;
            repositoryItemRibbonSearchEdit3.AutoHeight = false;
            repositoryItemRibbonSearchEdit3.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions13.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit3.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions13, new KeyShortcut(Keys.None), serializableAppearanceObject49, serializableAppearanceObject50, serializableAppearanceObject51, serializableAppearanceObject52, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions14, new KeyShortcut(Keys.None), serializableAppearanceObject53, serializableAppearanceObject54, serializableAppearanceObject55, serializableAppearanceObject56, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit3.Name = "repositoryItemRibbonSearchEdit3";
            repositoryItemRibbonSearchEdit3.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit4
            // 
            repositoryItemRibbonSearchEdit4.AllowFocused = false;
            repositoryItemRibbonSearchEdit4.AutoHeight = false;
            repositoryItemRibbonSearchEdit4.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions15.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit4.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions15, new KeyShortcut(Keys.None), serializableAppearanceObject57, serializableAppearanceObject58, serializableAppearanceObject59, serializableAppearanceObject60, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions16, new KeyShortcut(Keys.None), serializableAppearanceObject61, serializableAppearanceObject62, serializableAppearanceObject63, serializableAppearanceObject64, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit4.Name = "repositoryItemRibbonSearchEdit4";
            repositoryItemRibbonSearchEdit4.NullText = "Search";
            // 
            // 守卫视图窗口
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 247);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "守卫视图窗口";
            Ribbon = ribbon;
            Text = "守卫配置";
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
