namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 693, 长度 = 6, 注释 = "m2c_ex_group_setting")]
	public class 附加组队设置 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 整数变量;

		public override ushort 封包编号 => 693;
	}
}
