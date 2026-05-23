using System;
using 游戏服务器.模板类;

namespace 游戏服务器.数据类
{
	public class AchievementData : 游戏数据
	{
		public readonly 数据监视器<ushort> AchievementId;

		public readonly 数据监视器<DateTime> CompletedAt;

		public readonly 数据监视器<DateTime> ReceivedAt;

		public readonly 数据监视器<角色数据> Character;

		public GameAchievements Info
		{
			get
			{
				if (!GameAchievements.数据表.TryGetValue(this.AchievementId.V, out var value))
				{
					return null;
				}
				return value;
			}
		}

		public AchievementData()
		{
		}

		public AchievementData(角色数据 角色)
		{
			this.Character.V = 角色;
			游戏数据网关.成就数据表.添加数据(this, 分配索引: true);
		}
	}
}
