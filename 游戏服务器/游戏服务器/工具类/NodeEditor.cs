using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using 游戏服务器.模板类;

namespace 游戏服务器.工具类
{
	public class NodeEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService obj;
			obj = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			节点编辑器 节点编辑器2;
			节点编辑器2 = new 节点编辑器((SortedDictionary<int, 技能任务>)value);
			obj.ShowDialog(节点编辑器2);
			obj.CloseDropDown();
			return 节点编辑器2.节点列表;
		}
	}
}
