using System.Drawing;
namespace 游戏服务器.网络类;
[封包信息描述(来源 = 封包来源.服务器, 编号 = 59, 长度 = 12, 注释 = "玩家挖矿成功")]
public sealed class 玩家挖矿成功 : 游戏封包
{
    [封包字段描述(下标 = 2, 长度 = 4)]
    public int 编号;
    [封包字段描述(下标 = 6, 长度 = 4)]
    public Point 坐标;
    [封包字段描述(下标 = 10, 长度 = 2)]
    public ushort 动作间隔;
    public override ushort 封包编号 => 59;
}
