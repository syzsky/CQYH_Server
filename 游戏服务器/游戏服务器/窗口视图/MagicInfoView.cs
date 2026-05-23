using System;
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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid;
using Newtonsoft.Json;

namespace 游戏服务器.窗口视图
{
    public class MagicInfoView : RibbonForm
    {
        private TreeListNode _selectNode;

        public List<技能节点任务> 节点任务 = new List<技能节点任务>();

        public 技能节点任务 选中任务;

        private IContainer components;

        private RibbonControl ribbon;

        private RibbonPage ribbonPage1;

        private RibbonPageGroup ribbonPageGroup1;

        private BarButtonItem SaveDataBaseButton;

        private TreeList treeList1;

        private TreeListColumn treeListColumn1;

        private SplitterControl splitterControl1;

        private GroupControl groupControl1;

        private SplitterControl splitterControl2;

        private PropertyGridControl propertyGridControl1;

        private PropertyDescriptionControl propertyDescriptionControl1;

        private GroupControl groupControl2;

        private SplitterControl splitterControl3;

        private GridControl gridControl1;

        private GridView gridView1;

        private GridColumn gridColumn1;

        private GridColumn gridColumn2;

        private SplitterControl splitterControl5;

        private PropertyGridControl propertyGridControl2;

        private PropertyDescriptionControl propertyDescriptionControl2;

        private SplitterControl splitterControl4;

        private RepositoryItemImageComboBox repositoryItemImageComboBox1;

        private BarButtonItem barButtonItem1;

        private BarButtonItem ReLoadDataBaseButton;

        private BarButtonItem NodeDeleteButton;

        private BarButtonItem NodeAddButton;

        private BarEditItem SkillNameEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit1;

        private BarEditItem SkillIndexEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit2;

        private BarEditItem SkillRuneEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit3;

        private BarEditItem NodeTimeEdit;

        private RepositoryItemTextEdit repositoryItemTextEdit4;

        private RepositoryItemComboBox repositoryItemComboBox1;

        private PopupMenu SkillMenu;

        private PopupMenu NodeMenu;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit1;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit2;

        private BarEditItem NodeTypeEdit;

        private RepositoryItemImageComboBox repositoryItemImageComboBox2;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit3;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit4;

        private BarButtonItem SkillDeleteButton;

        private BarButtonItem SkillAddButton;

        private BarHeaderItem SkillHeader;

        private BarHeaderItem SkillAddHeader;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit5;

        private RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit6;

        private BarHeaderItem barHeaderItem1;

        private BarButtonItem SkillCopyButton;

        public MagicInfoView()
        {
            this.InitializeComponent();
            this.repositoryItemImageComboBox1.Items.AddEnum<技能任务类型>();
            this.repositoryItemImageComboBox2.Items.AddEnum<技能任务类型>();
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
                foreach (KeyValuePair<string, 游戏技能> item in 游戏技能.数据表)
                {
                    if (item.Value.技能职业.ToString() == text)
                    {
                        treeListNode.Nodes.Add(item.Value.ToString()).Tag = item.Value;
                    }
                }
            }
            this.propertyGridControl1.SelectedObject = null;
            this.gridControl1.DataSource = null;
            this.gridView1.RefreshData();
            this.propertyGridControl2.SelectedObject = null;
        }

        private void treeList1_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {
            this._selectNode = e.Node;
            if (e.Node == null || !(e.Node.Tag is 游戏技能))
            {
                return;
            }
            this.propertyGridControl1.SelectedObject = e.Node.Tag;
            this.节点任务.Clear();
            foreach (KeyValuePair<int, 技能任务> item in (e.Node.Tag as 游戏技能).节点列表)
            {
                this.节点任务.Add(new 技能节点任务
                {
                    触发时间 = item.Key,
                    技能任务 = item.Value,
                    任务类型 = (技能任务类型)Enum.Parse(typeof(技能任务类型), item.Value.GetType().Name),
                    节点列表 = (e.Node.Tag as 游戏技能).节点列表
                });
            }
            this.gridControl1.DataSource = this.节点任务;
            this.gridView1.RefreshData();
            this.propertyGridControl2.SelectedObject = null;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            this.选中任务 = (技能节点任务)this.gridView1.GetRow(e.RowHandle);
            this.propertyGridControl2.SelectedObject = ((技能节点任务)this.gridView1.GetRow(e.RowHandle))?.技能任务;
        }

        private void SaveDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            游戏技能.保存数据();
        }

        private void ReLoadDataBaseButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            系统数据网关.加载数据(3);
            /*
                    typeof(铭文技能),
                    typeof(游戏技能),
                    typeof(技能陷阱),
                    typeof(游戏Buff)
             */
            主程.添加系统日志("系统数据加载完成");
            this.Init();
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this._selectNode != null && this._selectNode.Tag is 游戏技能 游戏技能)
                {
                    this.SkillDeleteButton.Enabled = true;
                    this.SkillCopyButton.Enabled = true;
                    this.SkillHeader.Caption = 游戏技能.技能名字;
                }
                else
                {
                    this.SkillDeleteButton.Enabled = false;
                    this.SkillCopyButton.Enabled = false;
                    this.SkillHeader.Caption = "未选中";
                }
                TreeListNode treeListNode;
                treeListNode = ((this._selectNode == null) ? null : ((this._selectNode.Tag is 游戏对象职业) ? this._selectNode : this._selectNode.ParentNode));
                if (treeListNode == null)
                {
                    this.SkillAddButton.Enabled = false;
                    this.SkillAddHeader.Caption = "请选中职业分组后添加";
                }
                else
                {
                    this.SkillAddButton.Enabled = true;
                    this.SkillAddHeader.Caption = "新建职业技能: " + (游戏对象职业)treeListNode.Tag;
                }
                this.SkillMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void SkillAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode treeListNode;
            treeListNode = ((this._selectNode == null) ? null : ((this._selectNode.Tag is 游戏对象职业) ? this._selectNode : this._selectNode.ParentNode));
            if (treeListNode != null)
            {
                string text;
                text = (string)this.SkillNameEdit.EditValue;
                ushort 自身技能编号;
                自身技能编号 = (ushort)(int)this.SkillIndexEdit.EditValue;
                byte 自身铭文编号;
                自身铭文编号 = (byte)(int)this.SkillRuneEdit.EditValue;
                if (游戏服务器.模板类.游戏技能.数据表.ContainsKey(text))
                {
                    XtraMessageBox.Show("技能名称不可重复", "重复的名称", MessageBoxButtons.OK);
                    return;
                }
                游戏技能 游戏技能;
                游戏技能 = new 游戏技能
                {
                    技能名字 = text,
                    自身技能编号 = 自身技能编号,
                    自身铭文编号 = 自身铭文编号,
                    技能职业 = (游戏对象职业)treeListNode.Tag
                };
                游戏技能.数据表.Add(text, 游戏技能);
                treeListNode.Nodes.Add(游戏技能.ToString()).Tag = 游戏技能;
                this.SkillNameEdit.EditValue = "";
                this.SkillIndexEdit.EditValue = 0;
                this.SkillRuneEdit.EditValue = 0;
                XtraMessageBox.Show("添加成功", "", MessageBoxButtons.OK);
            }
        }

        private void SkillCopyButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(this._selectNode.Tag is 游戏技能))
            {
                XtraMessageBox.Show("请选中一个技能", "", MessageBoxButtons.OK);
                return;
            }
            游戏技能 value;
            value = this._selectNode.Tag as 游戏技能;
            string text;
            text = (string)this.SkillNameEdit.EditValue;
            ushort 自身技能编号;
            自身技能编号 = (ushort)(int)this.SkillIndexEdit.EditValue;
            byte 自身铭文编号;
            自身铭文编号 = (byte)(int)this.SkillRuneEdit.EditValue;
            if (游戏服务器.模板类.游戏技能.数据表.ContainsKey(text))
            {
                XtraMessageBox.Show("技能名称不可重复", "重复的名称", MessageBoxButtons.OK);
                return;
            }
            游戏技能 游戏技能;
            游戏技能 = JsonConvert.DeserializeObject<游戏技能>(JsonConvert.SerializeObject(value, 序列化类.全局设置), 序列化类.全局设置);
            游戏技能.技能名字 = text;
            游戏技能.自身技能编号 = 自身技能编号;
            游戏技能.自身铭文编号 = 自身铭文编号;
            游戏技能.数据表.Add(text, 游戏技能);
            this._selectNode.ParentNode.Nodes.Add(游戏技能.ToString()).Tag = 游戏技能;
            this.SkillNameEdit.EditValue = "";
            this.SkillIndexEdit.EditValue = 0;
            this.SkillRuneEdit.EditValue = 0;
            XtraMessageBox.Show("复制成功", "", MessageBoxButtons.OK);
        }

        private void SkillDeleteButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this._selectNode != null && this._selectNode.Tag is 游戏技能 游戏技能)
            {
                游戏技能.数据表.Remove(游戏技能.技能名字);
                this.treeList1.DeleteNode(this._selectNode);
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.NodeDeleteButton.Enabled = this.选中任务 != null;
                this.NodeAddButton.Enabled = this.propertyGridControl1.SelectedObject != null;
                this.NodeMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void NodeAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.propertyGridControl1.SelectedObject == null)
            {
                XtraMessageBox.Show("请先选择一个技能", "", MessageBoxButtons.OK);
                return;
            }
            int num;
            num = (int)this.NodeTimeEdit.EditValue;
            技能任务类型 任务类型;
            任务类型 = (技能任务类型)this.NodeTypeEdit.EditValue;
            游戏技能 游戏技能;
            游戏技能 = (游戏技能)this.propertyGridControl1.SelectedObject;
            if (游戏技能.节点列表.ContainsKey(num))
            {
                XtraMessageBox.Show("触发时间不可重复", "重复的触发时间", MessageBoxButtons.OK);
                return;
            }
            技能任务 技能任务;
            技能任务 = 技能节点任务.获取技能任务(任务类型);
            游戏技能.节点列表.Add(num, 技能任务);
            this.节点任务.Add(new 技能节点任务
            {
                触发时间 = num,
                技能任务 = 技能任务,
                任务类型 = 任务类型,
                节点列表 = 游戏技能.节点列表
            });
            this.gridView1.RefreshData();
        }

        private void NodeDeleteButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.选中任务 == null)
            {
                XtraMessageBox.Show("请先选择一个节点", "", MessageBoxButtons.OK);
                return;
            }
            this.选中任务.节点列表.Remove(this.选中任务.触发时间);
            this.节点任务.Remove(this.选中任务);
            this.gridView1.RefreshData();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MagicInfoView));
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
            ribbon = new RibbonControl();
            SaveDataBaseButton = new BarButtonItem();
            barButtonItem1 = new BarButtonItem();
            ReLoadDataBaseButton = new BarButtonItem();
            NodeDeleteButton = new BarButtonItem();
            NodeAddButton = new BarButtonItem();
            SkillNameEdit = new BarEditItem();
            repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            SkillIndexEdit = new BarEditItem();
            repositoryItemTextEdit2 = new RepositoryItemTextEdit();
            SkillRuneEdit = new BarEditItem();
            repositoryItemTextEdit3 = new RepositoryItemTextEdit();
            NodeTimeEdit = new BarEditItem();
            repositoryItemTextEdit4 = new RepositoryItemTextEdit();
            NodeTypeEdit = new BarEditItem();
            repositoryItemImageComboBox2 = new RepositoryItemImageComboBox();
            SkillDeleteButton = new BarButtonItem();
            SkillAddButton = new BarButtonItem();
            SkillHeader = new BarHeaderItem();
            SkillAddHeader = new BarHeaderItem();
            barHeaderItem1 = new BarHeaderItem();
            SkillCopyButton = new BarButtonItem();
            ribbonPage1 = new RibbonPage();
            ribbonPageGroup1 = new RibbonPageGroup();
            repositoryItemComboBox1 = new RepositoryItemComboBox();
            treeList1 = new TreeList();
            treeListColumn1 = new TreeListColumn();
            splitterControl1 = new SplitterControl();
            groupControl1 = new GroupControl();
            splitterControl2 = new SplitterControl();
            propertyGridControl1 = new PropertyGridControl();
            propertyDescriptionControl1 = new PropertyDescriptionControl();
            groupControl2 = new GroupControl();
            splitterControl5 = new SplitterControl();
            propertyGridControl2 = new PropertyGridControl();
            propertyDescriptionControl2 = new PropertyDescriptionControl();
            splitterControl4 = new SplitterControl();
            gridControl1 = new GridControl();
            gridView1 = new GridView();
            gridColumn1 = new GridColumn();
            gridColumn2 = new GridColumn();
            repositoryItemImageComboBox1 = new RepositoryItemImageComboBox();
            splitterControl3 = new SplitterControl();
            SkillMenu = new PopupMenu(components);
            NodeMenu = new PopupMenu(components);
            repositoryItemRibbonSearchEdit1 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit2 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit3 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit4 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit5 = new RepositoryItemRibbonSearchEdit();
            repositoryItemRibbonSearchEdit6 = new RepositoryItemRibbonSearchEdit();
            ((ISupportInitialize)ribbon).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit4).BeginInit();
            ((ISupportInitialize)repositoryItemImageComboBox2).BeginInit();
            ((ISupportInitialize)repositoryItemComboBox1).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((ISupportInitialize)propertyGridControl1).BeginInit();
            ((ISupportInitialize)groupControl2).BeginInit();
            groupControl2.SuspendLayout();
            ((ISupportInitialize)propertyGridControl2).BeginInit();
            ((ISupportInitialize)gridControl1).BeginInit();
            ((ISupportInitialize)gridView1).BeginInit();
            ((ISupportInitialize)repositoryItemImageComboBox1).BeginInit();
            ((ISupportInitialize)SkillMenu).BeginInit();
            ((ISupportInitialize)NodeMenu).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit5).BeginInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit6).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(35, 32, 35, 32);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new BarItem[] { ribbon.ExpandCollapseItem, SaveDataBaseButton, barButtonItem1, ReLoadDataBaseButton, NodeDeleteButton, NodeAddButton, SkillNameEdit, SkillIndexEdit, SkillRuneEdit, NodeTimeEdit, NodeTypeEdit, SkillDeleteButton, SkillAddButton, SkillHeader, SkillAddHeader, barHeaderItem1, SkillCopyButton });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(4, 3, 4, 3);
            ribbon.MaxItemId = 20;
            ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.Pages.AddRange(new RibbonPage[] { ribbonPage1 });
            ribbon.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemTextEdit1, repositoryItemTextEdit2, repositoryItemTextEdit3, repositoryItemTextEdit4, repositoryItemComboBox1, repositoryItemImageComboBox2 });
            ribbon.Size = new Size(1048, 160);
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
            ReLoadDataBaseButton.ImageOptions.SvgImage = (SvgImage)resources.GetObject("ReLoadDataBaseButton.ImageOptions.SvgImage");
            ReLoadDataBaseButton.LargeWidth = 50;
            ReLoadDataBaseButton.Name = "ReLoadDataBaseButton";
            ReLoadDataBaseButton.ItemClick += ReLoadDataBaseButton_ItemClick;
            // 
            // NodeDeleteButton
            // 
            NodeDeleteButton.Caption = "删除";
            NodeDeleteButton.Id = 5;
            NodeDeleteButton.Name = "NodeDeleteButton";
            NodeDeleteButton.ItemClick += NodeDeleteButton_ItemClick;
            // 
            // NodeAddButton
            // 
            NodeAddButton.Caption = "添加节点";
            NodeAddButton.Id = 6;
            NodeAddButton.Name = "NodeAddButton";
            NodeAddButton.ItemClick += NodeAddButton_ItemClick;
            // 
            // SkillNameEdit
            // 
            SkillNameEdit.Caption = "名字";
            SkillNameEdit.Edit = repositoryItemTextEdit1;
            SkillNameEdit.Id = 7;
            SkillNameEdit.Name = "SkillNameEdit";
            // 
            // repositoryItemTextEdit1
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // SkillIndexEdit
            // 
            SkillIndexEdit.Caption = "编号";
            SkillIndexEdit.Edit = repositoryItemTextEdit2;
            SkillIndexEdit.EditValue = 0;
            SkillIndexEdit.Id = 8;
            SkillIndexEdit.Name = "SkillIndexEdit";
            // 
            // repositoryItemTextEdit2
            // 
            repositoryItemTextEdit2.AutoHeight = false;
            repositoryItemTextEdit2.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit2.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            repositoryItemTextEdit2.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // SkillRuneEdit
            // 
            SkillRuneEdit.Caption = "铭文";
            SkillRuneEdit.Edit = repositoryItemTextEdit3;
            SkillRuneEdit.EditValue = 0;
            SkillRuneEdit.Id = 9;
            SkillRuneEdit.Name = "SkillRuneEdit";
            // 
            // repositoryItemTextEdit3
            // 
            repositoryItemTextEdit3.AutoHeight = false;
            repositoryItemTextEdit3.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit3.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            repositoryItemTextEdit3.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // NodeTimeEdit
            // 
            NodeTimeEdit.Caption = "时间";
            NodeTimeEdit.Edit = repositoryItemTextEdit4;
            NodeTimeEdit.EditValue = 0;
            NodeTimeEdit.Id = 10;
            NodeTimeEdit.Name = "NodeTimeEdit";
            // 
            // repositoryItemTextEdit4
            // 
            repositoryItemTextEdit4.AutoHeight = false;
            repositoryItemTextEdit4.MaskSettings.Set("MaskManagerType", typeof(NumericMaskManager));
            repositoryItemTextEdit4.MaskSettings.Set("mask", "n0");
            repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // NodeTypeEdit
            // 
            NodeTypeEdit.Caption = "类型";
            NodeTypeEdit.Edit = repositoryItemImageComboBox2;
            NodeTypeEdit.Id = 12;
            NodeTypeEdit.Name = "NodeTypeEdit";
            // 
            // repositoryItemImageComboBox2
            // 
            repositoryItemImageComboBox2.AutoHeight = false;
            repositoryItemImageComboBox2.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            // 
            // SkillDeleteButton
            // 
            SkillDeleteButton.Caption = "删除";
            SkillDeleteButton.Id = 13;
            SkillDeleteButton.Name = "SkillDeleteButton";
            SkillDeleteButton.ItemClick += SkillDeleteButton_ItemClick;
            // 
            // SkillAddButton
            // 
            SkillAddButton.Caption = "添加技能";
            SkillAddButton.Id = 14;
            SkillAddButton.Name = "SkillAddButton";
            SkillAddButton.ItemClick += SkillAddButton_ItemClick;
            // 
            // SkillHeader
            // 
            SkillHeader.Caption = "未选中";
            SkillHeader.Id = 15;
            SkillHeader.Name = "SkillHeader";
            // 
            // SkillAddHeader
            // 
            SkillAddHeader.Caption = "战士";
            SkillAddHeader.Id = 17;
            SkillAddHeader.Name = "SkillAddHeader";
            // 
            // barHeaderItem1
            // 
            barHeaderItem1.Caption = "添加或删除技能节点";
            barHeaderItem1.Id = 18;
            barHeaderItem1.Name = "barHeaderItem1";
            // 
            // SkillCopyButton
            // 
            SkillCopyButton.Caption = "复制技能";
            SkillCopyButton.Id = 19;
            SkillCopyButton.Name = "SkillCopyButton";
            SkillCopyButton.ItemClick += SkillCopyButton_ItemClick;
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
            // repositoryItemComboBox1
            // 
            repositoryItemComboBox1.AutoHeight = false;
            repositoryItemComboBox1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // treeList1
            // 
            treeList1.Columns.AddRange(new TreeListColumn[] { treeListColumn1 });
            treeList1.Dock = DockStyle.Left;
            treeList1.Location = new Point(0, 160);
            treeList1.MenuManager = ribbon;
            treeList1.Name = "treeList1";
            treeList1.OptionsMenu.EnableColumnMenu = false;
            treeList1.OptionsMenu.EnableNodeMenu = false;
            treeList1.OptionsMenu.ShowAddNodeItems = DefaultBoolean.False;
            treeList1.Size = new Size(350, 379);
            treeList1.TabIndex = 16;
            treeList1.RowClick += treeList1_RowClick;
            treeList1.MouseUp += treeList1_MouseUp;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "技能名字";
            treeListColumn1.FieldName = "技能名字";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.OptionsColumn.AllowEdit = false;
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            // 
            // splitterControl1
            // 
            splitterControl1.Location = new Point(660, 160);
            splitterControl1.Name = "splitterControl1";
            splitterControl1.Size = new Size(10, 379);
            splitterControl1.TabIndex = 18;
            splitterControl1.TabStop = false;
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(splitterControl2);
            groupControl1.Controls.Add(propertyGridControl1);
            groupControl1.Controls.Add(propertyDescriptionControl1);
            groupControl1.Dock = DockStyle.Left;
            groupControl1.Location = new Point(360, 160);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(300, 379);
            groupControl1.TabIndex = 20;
            groupControl1.Text = "配置技能";
            // 
            // splitterControl2
            // 
            splitterControl2.Dock = DockStyle.Bottom;
            splitterControl2.Location = new Point(2, 317);
            splitterControl2.Name = "splitterControl2";
            splitterControl2.Size = new Size(296, 10);
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
            propertyGridControl1.Size = new Size(296, 304);
            propertyGridControl1.TabIndex = 20;
            // 
            // propertyDescriptionControl1
            // 
            propertyDescriptionControl1.Dock = DockStyle.Bottom;
            propertyDescriptionControl1.Location = new Point(2, 327);
            propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            propertyDescriptionControl1.Size = new Size(296, 50);
            propertyDescriptionControl1.TabIndex = 21;
            // 
            // groupControl2
            // 
            groupControl2.Controls.Add(splitterControl5);
            groupControl2.Controls.Add(propertyGridControl2);
            groupControl2.Controls.Add(propertyDescriptionControl2);
            groupControl2.Controls.Add(splitterControl4);
            groupControl2.Controls.Add(gridControl1);
            groupControl2.Dock = DockStyle.Fill;
            groupControl2.Location = new Point(670, 160);
            groupControl2.Name = "groupControl2";
            groupControl2.Size = new Size(378, 379);
            groupControl2.TabIndex = 21;
            groupControl2.Text = "节点列表";
            // 
            // splitterControl5
            // 
            splitterControl5.Dock = DockStyle.Bottom;
            splitterControl5.Location = new Point(256, 317);
            splitterControl5.Name = "splitterControl5";
            splitterControl5.Size = new Size(120, 10);
            splitterControl5.TabIndex = 26;
            splitterControl5.TabStop = false;
            // 
            // propertyGridControl2
            // 
            propertyGridControl2.Dock = DockStyle.Fill;
            propertyGridControl2.Location = new Point(256, 23);
            propertyGridControl2.MenuManager = ribbon;
            propertyGridControl2.Name = "propertyGridControl2";
            propertyGridControl2.OptionsView.AllowReadOnlyRowAppearance = DefaultBoolean.True;
            propertyGridControl2.Size = new Size(120, 304);
            propertyGridControl2.TabIndex = 24;
            // 
            // propertyDescriptionControl2
            // 
            propertyDescriptionControl2.Dock = DockStyle.Bottom;
            propertyDescriptionControl2.Location = new Point(256, 327);
            propertyDescriptionControl2.Name = "propertyDescriptionControl2";
            propertyDescriptionControl2.Size = new Size(120, 50);
            propertyDescriptionControl2.TabIndex = 25;
            // 
            // splitterControl4
            // 
            splitterControl4.Location = new Point(246, 23);
            splitterControl4.Name = "splitterControl4";
            splitterControl4.Size = new Size(10, 354);
            splitterControl4.TabIndex = 23;
            splitterControl4.TabStop = false;
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Left;
            gridControl1.Location = new Point(2, 23);
            gridControl1.MainView = gridView1;
            gridControl1.MenuManager = ribbon;
            gridControl1.Name = "gridControl1";
            gridControl1.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemImageComboBox1 });
            gridControl1.Size = new Size(244, 354);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new BaseView[] { gridView1 });
            gridControl1.MouseUp += gridControl1_MouseUp;
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new GridColumn[] { gridColumn1, gridColumn2 });
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.RowClick += gridView1_RowClick;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "时间";
            gridColumn1.FieldName = "触发时间";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 50;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "节点类型";
            gridColumn2.ColumnEdit = repositoryItemImageComboBox1;
            gridColumn2.FieldName = "任务类型";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 169;
            // 
            // repositoryItemImageComboBox1
            // 
            repositoryItemImageComboBox1.AutoHeight = false;
            repositoryItemImageComboBox1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // splitterControl3
            // 
            splitterControl3.Location = new Point(350, 160);
            splitterControl3.Name = "splitterControl3";
            splitterControl3.Size = new Size(10, 379);
            splitterControl3.TabIndex = 22;
            splitterControl3.TabStop = false;
            // 
            // SkillMenu
            // 
            SkillMenu.ItemLinks.Add(SkillHeader);
            SkillMenu.ItemLinks.Add(SkillDeleteButton);
            SkillMenu.ItemLinks.Add(SkillAddHeader);
            SkillMenu.ItemLinks.Add(SkillAddButton);
            SkillMenu.ItemLinks.Add(SkillNameEdit);
            SkillMenu.ItemLinks.Add(SkillIndexEdit);
            SkillMenu.ItemLinks.Add(SkillRuneEdit);
            SkillMenu.ItemLinks.Add(SkillCopyButton);
            SkillMenu.Name = "SkillMenu";
            SkillMenu.Ribbon = ribbon;
            // 
            // NodeMenu
            // 
            NodeMenu.ItemLinks.Add(barHeaderItem1);
            NodeMenu.ItemLinks.Add(NodeDeleteButton);
            NodeMenu.ItemLinks.Add(NodeAddButton);
            NodeMenu.ItemLinks.Add(NodeTimeEdit);
            NodeMenu.ItemLinks.Add(NodeTypeEdit);
            NodeMenu.Name = "NodeMenu";
            NodeMenu.Ribbon = ribbon;
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
            // repositoryItemRibbonSearchEdit5
            // 
            repositoryItemRibbonSearchEdit5.AllowFocused = false;
            repositoryItemRibbonSearchEdit5.AutoHeight = false;
            repositoryItemRibbonSearchEdit5.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions9.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit5.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions9, new KeyShortcut(Keys.None), serializableAppearanceObject33, serializableAppearanceObject34, serializableAppearanceObject35, serializableAppearanceObject36, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions10, new KeyShortcut(Keys.None), serializableAppearanceObject37, serializableAppearanceObject38, serializableAppearanceObject39, serializableAppearanceObject40, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit5.Name = "repositoryItemRibbonSearchEdit5";
            repositoryItemRibbonSearchEdit5.NullText = "Search";
            // 
            // repositoryItemRibbonSearchEdit6
            // 
            repositoryItemRibbonSearchEdit6.AllowFocused = false;
            repositoryItemRibbonSearchEdit6.AutoHeight = false;
            repositoryItemRibbonSearchEdit6.BorderStyle = BorderStyles.NoBorder;
            editorButtonImageOptions11.AllowGlyphSkinning = DefaultBoolean.True;
            repositoryItemRibbonSearchEdit6.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions11, new KeyShortcut(Keys.None), serializableAppearanceObject41, serializableAppearanceObject42, serializableAppearanceObject43, serializableAppearanceObject44, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new EditorButton(ButtonPredefines.Clear, "", -1, true, false, false, editorButtonImageOptions12, new KeyShortcut(Keys.None), serializableAppearanceObject45, serializableAppearanceObject46, serializableAppearanceObject47, serializableAppearanceObject48, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemRibbonSearchEdit6.Name = "repositoryItemRibbonSearchEdit6";
            repositoryItemRibbonSearchEdit6.NullText = "Search";
            // 
            // MagicInfoView
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 539);
            Controls.Add(groupControl2);
            Controls.Add(splitterControl1);
            Controls.Add(groupControl1);
            Controls.Add(splitterControl3);
            Controls.Add(treeList1);
            Controls.Add(ribbon);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MagicInfoView";
            Ribbon = ribbon;
            Text = "技能配置";
            ((ISupportInitialize)ribbon).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit2).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit3).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit4).EndInit();
            ((ISupportInitialize)repositoryItemImageComboBox2).EndInit();
            ((ISupportInitialize)repositoryItemComboBox1).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((ISupportInitialize)propertyGridControl1).EndInit();
            ((ISupportInitialize)groupControl2).EndInit();
            groupControl2.ResumeLayout(false);
            ((ISupportInitialize)propertyGridControl2).EndInit();
            ((ISupportInitialize)gridControl1).EndInit();
            ((ISupportInitialize)gridView1).EndInit();
            ((ISupportInitialize)repositoryItemImageComboBox1).EndInit();
            ((ISupportInitialize)SkillMenu).EndInit();
            ((ISupportInitialize)NodeMenu).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit1).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit2).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit3).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit4).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit5).EndInit();
            ((ISupportInitialize)repositoryItemRibbonSearchEdit6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
