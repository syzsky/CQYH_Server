namespace 游戏服务器.模板类
{
	public sealed class C_07_计算目标瞬移 : 技能任务
	{
		public float[] 每级成功概率 { get; set; }

		public ushort 瞬移失败提示 { get; set; }

		public ushort 失败添加Buff { get; set; }

		public bool 增加技能经验 { get; set; }

		public ushort 经验技能编号 { get; set; }
	}
}
