using System.Collections.Generic;

namespace 游戏服务器.模板类
{
	public sealed class 刷新信息
	{
		public string 怪物名字 { get; set; }

		public int 刷新数量 { get; set; }

		public int 复活间隔 { get; set; }

		public bool 按秒复活 { get; set; }

		public List<定时刷新> 定时刷新 { get; set; }

		public override string ToString()
		{
			return $"{this.怪物名字}-{this.刷新数量}{((this.定时刷新 == null || this.定时刷新.Count == 0) ? "" : ("-(" + this.定时刷新.Count + ")" + this.定时刷新[0].ToString()))}";
		}
	}
}
