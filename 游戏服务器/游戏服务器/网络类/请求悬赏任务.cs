namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 287, 长度 = 3, 注释 = "请求悬赏任务")]
	public sealed class 请求悬赏任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 任务类型;

		public override ushort 封包编号 => 287;
	}
}
