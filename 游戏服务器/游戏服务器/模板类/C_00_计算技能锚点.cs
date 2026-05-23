namespace 游戏服务器.模板类
{
	public sealed class C_00_计算技能锚点 : 技能任务
	{
		public bool 计算BUFF目标 { get; set; }

		public bool 验证BUFF来源 { get; set; }

		public ushort 目标BUFF编号 { get; set; }

		public 技能范围类型 搜索目标范围 { get; set; }

		public bool 计算当前位置 { get; set; }

		public bool 计算当前方向 { get; set; }

		public int 技能最远距离 { get; set; }

		public int 技能最近距离 { get; set; }

		public bool 目标前方位置 { get; set; }
	}
}
