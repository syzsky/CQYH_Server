using System;
using System.ComponentModel;

namespace 游戏服务器.模板类
{
	public class 怪物指定掉落
	{
		public string 物品名字 { get; set; }

		public bool 已爆 => this.已爆出数量 > 0;

		public 指定掉落条件 指定条件 { get; set; }

		public string 指定条件值 { get; set; }

		[Browsable(false)]
		public int 已爆出数量 { get; set; }

		public int 最低数量 { get; set; }

		public int 最高数量 { get; set; }

		public string 爆出玩家 { get; set; }

		public DateTime 添加时间 { get; set; }

		public DateTime 爆出时间 { get; set; }

		public 怪物指定掉落()
		{
		}

		public 怪物指定掉落(string _物品名字, 指定掉落条件 _指定条件, string _指定条件值, int _最低数量 = 1, int _最高数量 = 1)
		{
			this.物品名字 = _物品名字;
			this.指定条件 = _指定条件;
			this.指定条件值 = _指定条件值;
			this.最低数量 = _最低数量;
			this.最高数量 = _最高数量;
			this.添加时间 = DateTime.Now;
		}
	}
}
