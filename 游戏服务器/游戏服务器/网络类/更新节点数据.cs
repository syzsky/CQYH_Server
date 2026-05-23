namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 297, 长度 = 6, 注释 = "更新节点数据")]
	public sealed class 更新节点数据 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 节点数值;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 节点标志;

		public override ushort 封包编号 => 297;
	}
}
