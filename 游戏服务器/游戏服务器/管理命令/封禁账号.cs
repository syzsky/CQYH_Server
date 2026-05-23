using System;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 封禁账号 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 账号名字;

		[字段描述(0, 排序 = 1)]
		public float 封禁天数;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (游戏数据网关.账号数据表.检索表.TryGetValue(this.账号名字, out var value) && value is 账号数据 账号数据)
			{
				账号数据.封禁日期.V = DateTime.Now.AddDays(this.封禁天数);
				账号数据.网络连接?.尝试断开连接(new Exception("账号被封禁, 强制下线"));
				主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 封禁到期时间: {账号数据.封禁日期}");
			}
			else
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 账号不存在");
			}
		}
	}
}
