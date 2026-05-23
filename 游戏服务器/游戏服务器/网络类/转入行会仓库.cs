namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 615, 长度 = 0, 注释 = "行会仓库转入")]
	public sealed class 转入行会仓库 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 仓库页面;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 仓库位置;

		[封包字段描述(下标 = 8, 长度 = 0)]
		public byte[] 物品详情;

		public override ushort 封包编号 => 615;
	}
}
