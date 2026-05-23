namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 344, 长度 = 6, 注释 = "装备神佑通知")]
	public class 装备神佑通知 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 通知结果;

		public override ushort 封包编号 => 345;
	}
}
