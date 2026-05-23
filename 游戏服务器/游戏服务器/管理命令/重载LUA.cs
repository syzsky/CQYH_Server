using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public sealed class 重载LUA : GM命令
	{
		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			游戏脚本.初始化脚本系统();
		}
	}
}
