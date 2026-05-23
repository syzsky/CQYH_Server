namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 105, 长度 = 8, 注释 = "切换战斗姿态")]
	public sealed class 切换战斗姿态 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 6, 长度 = 1)]
		public byte 姿态编号;

		[封包字段描述(下标 = 7, 长度 = 1)]
		public byte 触发动作;

		public override ushort 封包编号 => 105;

        static 切换战斗姿态()
        {


        }
    }
}
