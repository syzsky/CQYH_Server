namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 202, 长度 = 6, 注释 = "开始狩猎任务")]
	public sealed class 开始狩猎任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 8)]
		public byte[] 未知参数;

		public override ushort 封包编号 => 202;
	}
}
