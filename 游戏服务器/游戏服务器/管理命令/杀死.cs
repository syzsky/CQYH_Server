using System.Drawing;
using _001D_000F_0007_0013_0011_0015;
using 游戏服务器.地图类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public class 杀死 : GM在线命令
	{
		[_000A_0010_0014_0006_0006_000B(2)]
		[字段描述(0, 可选 = true)]
		public string 角色名字;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
		}

		public override void 执行命令(玩家实例 管理员)
		{
			if (this.角色名字 != null)
			{
				if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
				{
					(角色数据.网络连接?.绑定角色)?.自身死亡处理(null, 技能击杀: false);
				}
			}
			else
			{
				Point 坐标;
				坐标 = 计算类.前方坐标(管理员.当前坐标, 管理员.当前方向, 1);
				foreach (地图对象 item in 管理员.当前地图[坐标])
				{
					if (!(item is 物品实例))
					{
						item.自身死亡处理(null, 技能击杀: false);
					}
				}
			}
			管理员.发送系统消息("能杀的都杀掉了");
		}
	}
}
