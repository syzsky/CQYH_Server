using System.Windows.Forms;

namespace 游戏服务器.管理命令
{
	public sealed class 开放等级 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public byte 最高等级;

		public override 执行方式 执行方式 => 执行方式.只能后台执行;

		public override void 执行命令()
		{
			if (this.最高等级 <= Settings.游戏开放等级)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 等级低于当前已开放等级");
				return;
			}
			Settings.游戏开放等级 = (Settings.游戏开放等级 = this.最高等级);
			Settings.Save();
			主窗口.主界面.BeginInvoke((MethodInvoker)delegate
			{
				主窗口.主界面.S_游戏开放等级.Value = this.最高等级;
			});
			主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 当前开放等级:{Settings.游戏开放等级}");
		}
	}
}
