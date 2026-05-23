namespace 游戏服务器.模板类
{
	public sealed class B_03_前摇结束通知 : 技能任务
	{
		public bool 发送结束通知 { get; set; }

		public bool 计算攻速缩减 { get; set; }

		public int 角色硬直时间 { get; set; }

		public int 禁止行走时间 { get; set; }

		public int 禁止奔跑时间 { get; set; }

		public bool 解除技能陷阱 { get; set; }

		public byte 发送特殊标记 { get; set; }
	}
}
