using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace 游戏服务器.工具类
{
	public class FlagEnumUIEditor : UITypeEditor
	{
		private FlagCheckedListBox flagEnumCB;

		public FlagEnumUIEditor()
		{
			this.flagEnumCB = new FlagCheckedListBox();
			this.flagEnumCB.BorderStyle = BorderStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && context.Instance != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService;
				windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					Enum enumValue;
					enumValue = (Enum)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
					this.flagEnumCB.EnumValue = enumValue;
					windowsFormsEditorService.DropDownControl(this.flagEnumCB);
					return this.flagEnumCB.EnumValue;
				}
			}
			return null;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
	}
}
