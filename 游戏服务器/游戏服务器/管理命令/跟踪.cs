using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public class 跟踪 : GM在线命令
	{
		[字段描述(0)]
		public string 角色名字;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
		}

		public override void 执行命令(玩家实例 管理员)
		{
			if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value))
			{
				管理员.发送系统消息("玩家[" + this.角色名字 + "]不存在.");
				return;
			}
			玩家实例 玩家实例;
			玩家实例 = (value as 角色数据)?.网络连接?.绑定角色;
			if (玩家实例 == null)
			{
				管理员.发送系统消息("玩家[" + this.角色名字 + "]没有在线.");
			}
			else
			{
				管理员.玩家切换地图(玩家实例.当前地图, 地图区域类型.未知区域, 玩家实例.当前坐标);
			}
		}
	}
}
