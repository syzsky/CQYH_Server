using System;

namespace 游戏服务器.模板类
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class 字段描述 : Attribute
	{
		public int 排序;

		public bool 可选 { get; set; }

		public 字段描述(int 排序 = 0)
		{
			this.排序 = 排序;
		}
	}
}
