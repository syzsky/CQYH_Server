using _001D_000F_0007_0013_0011_0015;
using 游戏服务器.地图类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public class 在线 : GM在线命令
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
			管理员.发送系统消息($"当前在线{网络服务网关.已登录连接数}人");
			if (this.角色名字 != null && !(this.角色名字 == string.Empty))
			{
				if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value))
				{
					管理员.发送系统消息("玩家[" + this.角色名字 + "]不存在.");
					return;
				}
				角色数据 角色数据;
				角色数据 = value as 角色数据;
				管理员.发送系统消息($"名字[{this.角色名字}]等级[{角色数据.当前等级.V}]金币[{角色数据.金币数量}]元宝[{角色数据.元宝数量}]");
			}
		}
	}
}
