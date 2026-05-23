using System.Windows.Forms;

namespace 游戏服务器.管理命令
{
	public sealed class 设置经验 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public decimal 经验倍率;

		public override 执行方式 执行方式 => 执行方式.只能后台执行;

		public override void 执行命令()
		{
			if (this.经验倍率 <= 0m)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 经验倍率太低");
				return;
			}
			if (this.经验倍率 > 1000000m)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 经验倍率太高");
				return;
			}
			Settings.怪物经验倍率 = (Settings.怪物经验倍率 = this.经验倍率);
			Settings.Save();
			主窗口.主界面.BeginInvoke((MethodInvoker)delegate
			{
				主窗口.主界面.S_怪物经验倍率.Value = this.经验倍率;
			});
			主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 当前经验倍率:{Settings.怪物经验倍率}");
		}
	}
}
