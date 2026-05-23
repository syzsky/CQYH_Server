using System.Drawing;
namespace 游戏服务器.网络类;
[封包信息描述(来源 = 封包来源.服务器, 编号 = 367, 长度 = 15, 注释 = "玩家挖矿失败")]
public sealed class 玩家挖矿失败 : 游戏封包
{
    [封包字段描述(下标 = 2, 长度 = 7)]
    public byte[] UNK = new byte[7] { 48, 0, 11, 193, 54, 0, 0 };
    [封包字段描述(下标 = 9, 长度 = 4)]
    public Point 玩家坐标;
    [封包字段描述(下标 = 13, 长度 = 2)]
    public ushort 高度;
    public override ushort 封包编号 => 367;
}
