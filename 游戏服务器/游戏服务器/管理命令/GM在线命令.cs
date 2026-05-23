using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public abstract class GM在线命令 : GM命令
	{
		public abstract void 执行命令(玩家实例 管理员);
	}
}
