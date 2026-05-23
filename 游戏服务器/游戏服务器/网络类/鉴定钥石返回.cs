namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 364, 长度 = 3, 注释 = "鉴定钥石返回")]
	public class 鉴定钥石返回 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 是否成功;

		public override ushort 封包编号 => 364;
	}
}
