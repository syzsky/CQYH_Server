namespace 游戏服务器.管理命令
{
	public sealed class 重载模板 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public byte 模板序号;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			主窗口.加载系统数据(this.模板序号);
		}
	}
}
