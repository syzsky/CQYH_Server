using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 全部攻城 : GM命令
	{
		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			主程.添加命令日志("<= @" + base.GetType().Name + " 命令已经执行, 参与的行会如下:");
			地图处理网关.沙巴克攻城战立即开始();
		}
	}
}
