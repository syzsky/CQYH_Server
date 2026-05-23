using System.Drawing;

namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 150, 长度 = 0, 注释 = "掉落物品")]
	public sealed class 对象掉落物品 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 8, 长度 = 4)]
		public int 地图编号;

		[封包字段描述(下标 = 12, 长度 = 4)]
		public Point 掉落坐标;

		[封包字段描述(下标 = 16, 长度 = 2)]
		public ushort 掉落高度;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int 物品编号;

		[封包字段描述(下标 = 22, 长度 = 4)]
		public int 物品数量;

		[封包字段描述(下标 = 30, 长度 = 4)]
		public int OwnerPlayerId;

		public override ushort 封包编号 => 150;
	}
}
