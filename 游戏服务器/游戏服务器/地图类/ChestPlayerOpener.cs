using System;
using System.Collections.Generic;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public class ChestPlayerOpener
	{
		public 玩家实例 Player;

		public 道具实例 道具;

		public HashSet<物品数据> LockedKeys;

		public DateTime EndOpensTime;

		public DateTime NextAppearTime;

		public bool OpenCompleted;

		public bool ScriptOp;

		public int TimeSec = 1600;

		public int Rate;

		public int KeyId;

		public int KeyCost;

		public void Opening()
		{
			this.Player.操作道具 = true;
			this.EndOpensTime = 主程.当前时间.AddSeconds(this.TimeSec / 1000);
			this.道具.发送封包邻居(new 开始操作道具
			{
				玩家编号 = this.Player.地图编号,
				对象编号 = this.道具.地图编号,
				Duration = (ushort)(this.TimeSec / 60)
			});
		}

		public void Cancel()
		{
			this.道具.发送封包邻居(new 结束操作道具
			{
				玩家编号 = this.Player.地图编号,
				对象编号 = this.道具.地图编号,
				Unknown = 1
			});
			this.Player.操作道具 = false;
		}
	}
}
