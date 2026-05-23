namespace 游戏服务器.模板类
{
	public sealed class C_05_计算目标回复 : 技能任务
	{
		public int[] 体力回复次数 { get; set; }

		public float[] 道术叠加次数 { get; set; }

		public byte[] 体力回复基数 { get; set; }

		public float[] 道术叠加基数 { get; set; }

		public int[] 立即回复基数 { get; set; }

		public float[] 立即回复系数 { get; set; }

		public bool 增加技能经验 { get; set; }

		public ushort 经验技能编号 { get; set; }

		public C_05_计算目标回复()
		{
			this.体力回复基数 = new byte[4];
		}
	}
}
