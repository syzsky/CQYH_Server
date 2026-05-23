namespace 游戏服务器.模板类
{
	public sealed class B_01_技能释放通知 : 技能任务
	{
		public bool 发送释放通知 { get; set; }

		public bool 移除技能标记 { get; set; }

		public bool 调整角色朝向 { get; set; }

		public int 自身冷却时间 { get; set; }

		public bool Buff增加冷却 { get; set; }

		public ushort 增加冷却Buff { get; set; }

		public int 冷却增加时间 { get; set; }

		public int 分组冷却时间 { get; set; }

		public int 角色忙绿时间 { get; set; }

		public bool Buff增加层数 { get; set; }

		public ushort 增加层数Buff { get; set; }

		public int 增加Buff层数 { get; set; }

		public string 发送地图公告 { get; set; }

		public string 发送全服公告 { get; set; }
	}
}
