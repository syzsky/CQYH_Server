using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace 游戏服务器.工具类
{
	public class HashSetEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService obj;
			obj = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			哈希编辑器 哈希编辑器2;
			哈希编辑器2 = new 哈希编辑器(value);
			obj.ShowDialog(哈希编辑器2);
			obj.CloseDropDown();
			return 哈希编辑器2.哈希表;
		}
	}
}
