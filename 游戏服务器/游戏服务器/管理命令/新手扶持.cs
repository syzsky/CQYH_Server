using System.Windows.Forms;

namespace 游戏服务器.管理命令
{
	public sealed class 新手扶持 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public byte 扶持等级;

		public override 执行方式 执行方式 => 执行方式.只能后台执行;

		public override void 执行命令()
		{
			Settings.新手扶持等级 = (Settings.新手扶持等级 = this.扶持等级);
			Settings.Save();
			主窗口.主界面.BeginInvoke((MethodInvoker)delegate
			{
				主窗口.主界面.S_新手扶持等级.Value = this.扶持等级;
			});
			主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 当前扶持等级:{Settings.新手扶持等级}");
		}
	}
}
