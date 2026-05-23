using System;

namespace 游戏服务器.数据类
{
	public sealed class 宠物数据 : 游戏数据
	{
		public readonly 数据监视器<string> 宠物名字;

		public readonly 数据监视器<int> 当前体力;

		public readonly 数据监视器<int> 当前经验;

		public readonly 数据监视器<byte> 当前等级;

		public readonly 数据监视器<byte> 等级上限;

		public readonly 数据监视器<bool> 绑定武器;

		public readonly 数据监视器<ushort> 绑定BUFF;

		public readonly 数据监视器<DateTime> 叛变时间;

		public readonly 数据监视器<DateTime> 存活时间;

		public readonly 数据监视器<bool> 物品召唤;

		public readonly 字典监视器<ushort, Buff数据> Buff数据;

		public 宠物数据()
		{
		}

		public 宠物数据(string 宠物名字, byte 当前等级, byte 等级上限, bool 绑定武器, ushort 绑定BUFF, DateTime 叛变时间, DateTime 存活时间)
		{
			this.宠物名字.V = 宠物名字;
			this.当前等级.V = 当前等级;
			this.等级上限.V = 等级上限;
			this.绑定武器.V = 绑定武器;
			this.叛变时间.V = 叛变时间;
			this.存活时间.V = 存活时间;
			this.绑定BUFF.V = 绑定BUFF;
			游戏数据网关.宠物数据表.添加数据(this, 分配索引: true);
		}
	}
}
