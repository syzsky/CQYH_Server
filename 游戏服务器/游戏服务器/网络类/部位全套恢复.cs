namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 342, 长度 = 0, 注释 = "全套部位恢复")]
	public class 部位全套恢复 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 0)]
		public byte[] 数据;

		public override ushort 封包编号 => 342;
	}
}
