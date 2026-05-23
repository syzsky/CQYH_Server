using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;

namespace 游戏服务器.模板类
{
	public class 刷出的怪Editor : CollectionEditor
	{
		public 刷出的怪Editor(Type type)
			: base(type)
		{
		}

		protected override CollectionForm CreateCollectionForm()
		{
			CollectionForm collectionForm;
			collectionForm = base.CreateCollectionForm();
			ButtonBase buttonBase;
			buttonBase = (ButtonBase)collectionForm.Controls.Find("addButton", searchAllChildren: true).First();
			ButtonBase buttonBase2;
			buttonBase2 = (ButtonBase)collectionForm.Controls.Find("removeButton", searchAllChildren: true).First();
			buttonBase2.Visible = false;
			buttonBase.Visible = false;
			return collectionForm;
		}
	}
}
