namespace 游戏服务器.工具类
{
	public class FlagCheckedListBoxItem
	{
		public int value;

		public string caption;

		public bool IsFlag => (this.value & (this.value - 1)) == 0;

		public FlagCheckedListBoxItem(int v, string c)
		{
			this.value = v;
			this.caption = c;
		}

		public override string ToString()
		{
			return this.caption;
		}

		public bool IsMemberFlag(FlagCheckedListBoxItem composite)
		{
			if (this.IsFlag)
			{
				return (this.value & composite.value) == this.value;
			}
			return false;
		}
	}
}
