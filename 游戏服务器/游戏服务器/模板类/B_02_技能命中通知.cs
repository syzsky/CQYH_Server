namespace 游戏服务器.模板类
{
	public sealed class B_02_技能命中通知 : 技能任务
	{
		public bool 命中扩展通知 { get; set; }

		public bool 计算飞行耗时 { get; set; }

		public int 单格飞行耗时 { get; set; }
	}
}
