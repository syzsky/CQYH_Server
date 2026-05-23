using System.Windows.Forms;

namespace 游戏服务器.管理命令
{
	public sealed class 设置爆率 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public decimal 额外爆率;

		public override 执行方式 执行方式 => 执行方式.只能后台执行;

		public override void 执行命令()
		{
			if (this.额外爆率 < 0m)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 额外爆率太低");
				return;
			}
			if (this.额外爆率 >= 1m)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 额外爆率太高");
				return;
			}
			Settings.怪物额外爆率 = (Settings.怪物额外爆率 = this.额外爆率);
			Settings.Save();
			主窗口.主界面.BeginInvoke((MethodInvoker)delegate
			{
				主窗口.主界面.S_怪物额外爆率.Value = this.额外爆率;
			});
			主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 当前额外爆率:{Settings.怪物额外爆率}");
		}
	}
}
