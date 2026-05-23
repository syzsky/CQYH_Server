using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using 游戏服务器.模板类;
using DevExpress.Data.Mask;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace 游戏服务器.窗口视图
{
    public class 公告视图窗口 : RibbonForm
    {
        private IContainer components;

        private RibbonControl ribbon;

        private RibbonPage ribbonPage1;

        private RibbonPageGroup ribbonPageGroup1;

        private BarButtonItem SaveDataBaseButton;

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

        private AlertControl alertControl1;

        private GroupControl groupControl1;

        private GridControl gridControl1;

        private GridView gridView1;

        private SimpleButton simpleButton1;

        private SimpleButton 发送;

        private LabelControl labelControl23;

        private DateEdit StartDateEdit;

        private TextEdit AutoSafeTimeEdit;

        private LabelControl labelControl8;

        private SimpleButton simpleButton3;

        private SimpleButton simpleButton2;

        private SpinEdit spinEdit1;

        private LabelControl labelControl1;

        private CheckEdit checkEdit11;

        private CheckEdit checkEdit1;

        private TextEdit textEdit1;

        private SpinEdit spinEdit2;

        private LabelControl labelControl2;

        public 公告视图窗口()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.gridControl1.DataSource = null;
            this.gridControl1.DataSource = 系统公告.数据表;
            this.gridView1.PopulateColumns();
        }

        private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统数据网关.加载数据(9);
            /*
             typeof(系统公告) 
             */
            主程.添加系统日志("系统公告加载完成");
            this.Init();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统公告.保存数据();
        }

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            object focusedRow;
            focusedRow = this.gridView1.GetFocusedRow();
            if (focusedRow != null)
            {
                系统公告 系统公告;
                系统公告 = (系统公告)focusedRow;
                if (系统公告 != null)
                {
                    this.textEdit1.Text = 系统公告.公告内容;
                    this.StartDateEdit.DateTime = 系统公告.开始时间;
                    this.spinEdit1.Value = 系统公告.间隔时间_秒;
                    this.spinEdit2.Value = 系统公告.发送次数;
                    this.checkEdit1.Checked = 系统公告.启用 == 1;
                    this.checkEdit11.Checked = 系统公告.滚动播报 == 1;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            object focusedRow;
            focusedRow = this.gridView1.GetFocusedRow();
            if (focusedRow == null)
            {
                return;
            }
            系统公告 系统公告;
            系统公告 = (系统公告)focusedRow;
            if (系统公告 != null)
            {
                系统公告.公告内容 = this.textEdit1.Text;
                系统公告.开始时间 = this.StartDateEdit.DateTime;
                系统公告.间隔时间_秒 = (int)this.spinEdit1.Value;
                系统公告.发送次数 = (int)this.spinEdit2.Value;
                if (this.checkEdit11.Checked)
                {
                    系统公告.滚动播报 = 1;
                }
                else
                {
                    系统公告.滚动播报 = 0;
                }
                if (this.checkEdit1.Checked)
                {
                    系统公告.启用 = 1;
                }
                else
                {
                    系统公告.启用 = 0;
                }
                this.Init();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            系统公告 系统公告;
            系统公告 = new 系统公告();
            if (系统公告 != null)
            {
                系统公告.公告内容 = this.textEdit1.Text;
                系统公告.开始时间 = this.StartDateEdit.DateTime;
                系统公告.间隔时间_秒 = (int)this.spinEdit1.Value;
                系统公告.发送次数 = (int)this.spinEdit2.Value;
                if (this.checkEdit11.Checked)
                {
                    系统公告.滚动播报 = 1;
                }
                else
                {
                    系统公告.滚动播报 = 0;
                }
                if (this.checkEdit1.Checked)
                {
                    系统公告.启用 = 1;
                }
                else
                {
                    系统公告.启用 = 0;
                }
                系统公告.数据表.Add(系统公告);
                this.Init();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            object focusedRow;
            focusedRow = this.gridView1.GetFocusedRow();
            if (focusedRow != null)
            {
                系统公告 系统公告;
                系统公告 = (系统公告)focusedRow;
                if (系统公告 != null)
                {
                    系统公告.数据表.Remove(系统公告);
                    this.Init();
                }
            }
        }

        private void 发送_Click(object sender, EventArgs e)
        {
            object focusedRow;
            focusedRow = this.gridView1.GetFocusedRow();
            if (focusedRow != null)
            {
                ((系统公告)focusedRow)?.发送();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(公告视图窗口));
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
            barButtonItem3 = new BarButtonItem();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
            repositoryItemTextEdit2 = new RepositoryItemTextEdit();
            repositoryItemTextEdit3 = new RepositoryItemTextEdit();
            repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit2 = new RepositoryItemSpinEdit();
            popupMenu1 = new PopupMenu(components);
            repositoryItemRibbonSearchEdit1 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit2 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit3 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit4 = new RepositoryItemRibbonSearchEdit();
            alertControl1 = new AlertControl(components);
            groupControl1 = new GroupControl();
            spinEdit2 = new SpinEdit();
            labelControl2 = new LabelControl();
            checkEdit1 = new CheckEdit();
            textEdit1 = new TextEdit();
            spinEdit1 = new SpinEdit();
            labelControl1 = new LabelControl();
            checkEdit11 = new CheckEdit();
            simpleButton3 = new SimpleButton();
            simpleButton2 = new SimpleButton();
            simpleButton1 = new SimpleButton();
            发送 = new SimpleButton();
            labelControl23 = new LabelControl();
            StartDateEdit = new DateEdit();
            labelControl8 = new LabelControl();
            gridControl1 = new GridControl();
            gridView1 = new GridView();
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit4).BeginInit();
            ((ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemSpinEdit2).BeginInit();
            ((ISupportInitialize)popupMenu1).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)spinEdit2.Properties).BeginInit();
            ((ISupportInitialize)checkEdit1.Properties).BeginInit();
            ((ISupportInitialize)textEdit1.Properties).BeginInit();
            ((ISupportInitialize)spinEdit1.Properties).BeginInit();
            ((ISupportInitialize)checkEdit11.Properties).BeginInit();
            ((ISupportInitialize)StartDateEdit.Properties).BeginInit();
            ((ISupportInitialize)StartDateEdit.Properties.CalendarTimeProperties).BeginInit();
            ((ISupportInitialize)gridControl1).BeginInit();
            ((ISupportInitialize)gridView1).BeginInit();
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
            ribbon.Size = new Size(848, 160);
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
            ReLoadDataBaseButton.ImageOptions.SvgImage = (SvgImage)resources.GetObject("ReLoadDataBaseButton.ImageOptions.SvgImage");
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
            barButtonItem3.ImageOptions.SvgImage = (SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
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
            // groupControl1
            // 
            groupControl1.Controls.Add(spinEdit2);
            groupControl1.Controls.Add(labelControl2);
            groupControl1.Controls.Add(checkEdit1);
            groupControl1.Controls.Add(textEdit1);
            groupControl1.Controls.Add(spinEdit1);
            groupControl1.Controls.Add(labelControl1);
            groupControl1.Controls.Add(checkEdit11);
            groupControl1.Controls.Add(simpleButton3);
            groupControl1.Controls.Add(simpleButton2);
            groupControl1.Controls.Add(simpleButton1);
            groupControl1.Controls.Add(发送);
            groupControl1.Controls.Add(labelControl23);
            groupControl1.Controls.Add(StartDateEdit);
            groupControl1.Controls.Add(labelControl8);
            groupControl1.Dock = DockStyle.Bottom;
            groupControl1.Location = new Point(0, 430);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(848, 100);
            groupControl1.TabIndex = 1;
            groupControl1.Text = "操作";
            // 
            // spinEdit2
            // 
            spinEdit2.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            spinEdit2.Location = new Point(353, 56);
            spinEdit2.MenuManager = ribbon;
            spinEdit2.Name = "spinEdit2";
            spinEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            spinEdit2.Size = new Size(60, 20);
            spinEdit2.TabIndex = 71;
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(323, 59);
            labelControl2.Margin = new Padding(3, 4, 3, 4);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(24, 14);
            labelControl2.TabIndex = 70;
            labelControl2.Text = "次数";
            // 
            // checkEdit1
            // 
            checkEdit1.Location = new Point(480, 57);
            checkEdit1.MenuManager = ribbon;
            checkEdit1.Name = "checkEdit1";
            checkEdit1.Properties.Caption = "启用";
            checkEdit1.Size = new Size(45, 20);
            checkEdit1.TabIndex = 69;
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(77, 24);
            textEdit1.MenuManager = ribbon;
            textEdit1.Name = "textEdit1";
            textEdit1.Size = new Size(839, 20);
            textEdit1.TabIndex = 68;
            // 
            // spinEdit1
            // 
            spinEdit1.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            spinEdit1.Location = new Point(246, 56);
            spinEdit1.MenuManager = ribbon;
            spinEdit1.Name = "spinEdit1";
            spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            spinEdit1.Size = new Size(71, 20);
            spinEdit1.TabIndex = 67;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(192, 59);
            labelControl1.Margin = new Padding(3, 4, 3, 4);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(48, 14);
            labelControl1.TabIndex = 66;
            labelControl1.Text = "间隔时间";
            // 
            // checkEdit11
            // 
            checkEdit11.Location = new Point(429, 57);
            checkEdit11.MenuManager = ribbon;
            checkEdit11.Name = "checkEdit11";
            checkEdit11.Properties.Caption = "滚动";
            checkEdit11.Size = new Size(45, 20);
            checkEdit11.TabIndex = 65;
            // 
            // simpleButton3
            // 
            simpleButton3.Location = new Point(841, 55);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new Size(75, 23);
            simpleButton3.TabIndex = 50;
            simpleButton3.Text = "删除";
            simpleButton3.Click += simpleButton3_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.Location = new Point(679, 55);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(75, 23);
            simpleButton2.TabIndex = 49;
            simpleButton2.Text = "修改";
            simpleButton2.Click += simpleButton2_Click;
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new Point(760, 55);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(75, 23);
            simpleButton1.TabIndex = 48;
            simpleButton1.Text = "增加";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // 发送
            // 
            发送.Location = new Point(575, 55);
            发送.Name = "发送";
            发送.Size = new Size(75, 23);
            发送.TabIndex = 47;
            发送.Text = "立即发送";
            发送.Click += 发送_Click;
            // 
            // labelControl23
            // 
            labelControl23.Location = new Point(12, 55);
            labelControl23.Margin = new Padding(3, 4, 3, 4);
            labelControl23.Name = "labelControl23";
            labelControl23.Size = new Size(48, 14);
            labelControl23.TabIndex = 46;
            labelControl23.Text = "开始时间";
            // 
            // StartDateEdit
            // 
            StartDateEdit.EditValue = null;
            StartDateEdit.Location = new Point(77, 56);
            StartDateEdit.MenuManager = ribbon;
            StartDateEdit.Name = "StartDateEdit";
            StartDateEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            StartDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            StartDateEdit.Size = new Size(100, 20);
            StartDateEdit.TabIndex = 45;
            // 
            // labelControl8
            // 
            labelControl8.Location = new Point(22, 28);
            labelControl8.Margin = new Padding(3, 4, 3, 4);
            labelControl8.Name = "labelControl8";
            labelControl8.Size = new Size(48, 14);
            labelControl8.TabIndex = 43;
            labelControl8.Text = "公告内容";
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(0, 160);
            gridControl1.MainView = gridView1;
            gridControl1.MenuManager = ribbon;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(848, 270);
            gridControl1.TabIndex = 2;
            gridControl1.ViewCollection.AddRange(new BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            // 
            // 公告视图窗口
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(848, 530);
            Controls.Add(gridControl1);
            Controls.Add(groupControl1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "公告视图窗口";
            Ribbon = ribbon;
            Text = "公告配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit4).EndInit();
            ((ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit2).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit3).EndInit();
            ((ISupportInitialize)repositoryItemSpinEdit1).EndInit();
            ((ISupportInitialize)repositoryItemSpinEdit2).EndInit();
            ((ISupportInitialize)popupMenu1).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((ISupportInitialize)spinEdit2.Properties).EndInit();
            ((ISupportInitialize)checkEdit1.Properties).EndInit();
            ((ISupportInitialize)textEdit1.Properties).EndInit();
            ((ISupportInitialize)spinEdit1.Properties).EndInit();
            ((ISupportInitialize)checkEdit11.Properties).EndInit();
            ((ISupportInitialize)StartDateEdit.Properties.CalendarTimeProperties).EndInit();
            ((ISupportInitialize)StartDateEdit.Properties).EndInit();
            ((ISupportInitialize)gridControl1).EndInit();
            ((ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
