using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 发送公告 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 公告内容;

		public override 执行方式 执行方式 => 执行方式.前台立即执行;

		public override void 执行命令()
		{
			网络服务网关.发送公告(this.公告内容, 滚动播报: true);
		}
	}
}
