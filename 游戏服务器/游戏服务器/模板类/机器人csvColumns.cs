using System;

namespace 游戏服务器.模板类
{
	public class 机器人csvColumns
	{
		public DateTime StartTime { get; set; }

		public string Interval { get; set; }

		public 机器人类型 Type { get; set; }

		public int Enable { get; set; }

		public int Count { get; set; }

		public string Key { get; set; }
	}
}
