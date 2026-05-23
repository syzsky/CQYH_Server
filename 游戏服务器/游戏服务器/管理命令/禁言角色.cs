using System;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 禁言角色 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public int 禁言分钟;

		[字段描述(0, 排序 = 2)]
		public bool 发送邮件;

		[字段描述(0, 排序 = 3, 可选 = true)]
		public string 禁言原因;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			游戏数据 value;
			if (string.IsNullOrWhiteSpace(this.角色名字))
			{
				主窗口.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色名不能为空");
			}
			else if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out value))
			{
				if (!(value is 角色数据 角色数据))
				{
					return;
				}
				if (this.禁言分钟 > 0)
				{
					if (!string.IsNullOrWhiteSpace(this.禁言原因))
					{
						角色数据.网络连接?.绑定角色?.发送系统消息($"您已被禁言{this.禁言分钟}分钟,原因:{this.禁言原因}");
						if (this.发送邮件)
						{
							角色数据.发送邮件(null, "您已被系统禁言", this.禁言原因, null);
						}
					}
				}
				else if (角色数据.禁言日期.V > DateTime.Now)
				{
					角色数据.发送邮件(null, "解除禁言", "您的禁言已解除", null);
				}
				角色数据.禁言日期.V = ((this.禁言分钟 > 0) ? DateTime.Now.AddMinutes(this.禁言分钟) : default(DateTime));
			}
			else
			{
				主窗口.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
			}
		}
	}
}
