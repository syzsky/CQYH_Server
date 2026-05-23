using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public class 隐身 : GM在线命令
	{
		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
		}

		public override void 执行命令(玩家实例 管理员)
		{
			管理员.隐身模式 = !管理员.隐身模式;
			管理员.阻塞网格 = !管理员.隐身模式;
			管理员.发送系统消息(管理员.隐身模式 ? "进入隐身模式" : "退出隐身模式");
		}
	}
}
