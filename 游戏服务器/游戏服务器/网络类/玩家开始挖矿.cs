using System.Drawing;

namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 30, 长度 = 6, 注释 = "玩家开始挖矿")]
	public sealed class 玩家开始挖矿 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public Point 挖掘坐标;

		public override ushort 封包编号 => 30;
	}
}
