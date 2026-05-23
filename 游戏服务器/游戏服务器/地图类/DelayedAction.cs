using System;

namespace 游戏服务器.地图类
{
	public class DelayedAction
	{
		public DelayedType Type;

		public DateTime Time;

		public DateTime StartTime;

		public object[] Params;

		public bool FlaggedToRemove;

		public DelayedAction(DelayedType type, DateTime time, params object[] p)
		{
			this.StartTime = 主程.当前时间;
			this.Type = type;
			this.Time = time;
			this.Params = p;
		}
	}
}
