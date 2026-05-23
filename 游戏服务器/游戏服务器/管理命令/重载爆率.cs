using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public sealed class 重载爆率 : GM命令
	{
		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			游戏怪物.重载所有怪物爆率();
			主程.添加系统日志("所有怪物重载成功");
		}
	}
}
