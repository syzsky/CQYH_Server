using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace 游戏服务器.工具类
{
	public class FlagCheckedListBox : CheckedListBox
	{
		private Container components;

		private bool isUpdatingCheckStates;

		private Type enumType;

		private Enum enumValue;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Enum EnumValue
		{
			get
			{
				return (Enum)Enum.ToObject(this.enumType, this.GetCurrentValue());
			}
			set
			{
				base.Items.Clear();
				this.enumValue = value;
				this.enumType = value.GetType();
				this.FillEnumMembers();
				this.ApplyEnumValue();
			}
		}

		public FlagCheckedListBox()
		{
			this.InitializeComponent();
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
			base.CheckOnClick = true;
		}

		public FlagCheckedListBoxItem Add(int v, string c)
		{
			FlagCheckedListBoxItem flagCheckedListBoxItem;
			flagCheckedListBoxItem = new FlagCheckedListBoxItem(v, c);
			base.Items.Add(flagCheckedListBoxItem);
			return flagCheckedListBoxItem;
		}

		public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
		{
			base.Items.Add(item);
			return item;
		}

		protected override void OnItemCheck(ItemCheckEventArgs e)
		{
			base.OnItemCheck(e);
			if (!this.isUpdatingCheckStates)
			{
				this.UpdateCheckedItems(base.Items[e.Index] as FlagCheckedListBoxItem, e.NewValue);
			}
		}

		protected void UpdateCheckedItems(int value)
		{
			this.isUpdatingCheckStates = true;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem;
				flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (flagCheckedListBoxItem.value == 0)
				{
					base.SetItemChecked(i, value == 0);
				}
				else if ((flagCheckedListBoxItem.value & value) == flagCheckedListBoxItem.value && flagCheckedListBoxItem.value != 0)
				{
					base.SetItemChecked(i, value: true);
				}
				else
				{
					base.SetItemChecked(i, value: false);
				}
			}
			this.isUpdatingCheckStates = false;
		}

		protected void UpdateCheckedItems(FlagCheckedListBoxItem composite, CheckState cs)
		{
			if (composite.value == 0)
			{
				this.UpdateCheckedItems(0);
			}
			int num;
			num = 0;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem;
				flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (base.GetItemChecked(i))
				{
					num |= flagCheckedListBoxItem.value;
				}
			}
			this.UpdateCheckedItems((cs != 0) ? (num | composite.value) : (num & ~composite.value));
		}

		public int GetCurrentValue()
		{
			int num;
			num = 0;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem;
				flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (base.GetItemChecked(i))
				{
					num |= flagCheckedListBoxItem.value;
				}
			}
			return num;
		}

		private void FillEnumMembers()
		{
			string[] names;
			names = Enum.GetNames(this.enumType);
			foreach (string text in names)
			{
				this.Add((int)Convert.ChangeType(Enum.Parse(this.enumType, text), typeof(int)), text);
			}
		}

		private void ApplyEnumValue()
		{
			this.UpdateCheckedItems((int)Convert.ChangeType(this.enumValue, typeof(int)));
		}
	}
}
