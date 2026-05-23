using System;
using System.Linq;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 禁言账号 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 账号名字;

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
			if (string.IsNullOrWhiteSpace(this.账号名字))
			{
				主窗口.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 账号名字不能为空");
			}
			else if (游戏数据网关.账号数据表.检索表.TryGetValue(this.账号名字, out value))
			{
				账号数据 账号数据;
				账号数据 = value as 账号数据;
				if (账号数据.角色列表 != null && 账号数据.角色列表.Any())
				{
					foreach (角色数据 item in 账号数据.角色列表.ToList())
					{
						if (item != null)
						{
							if (this.禁言分钟 > 0)
							{
								if (!string.IsNullOrWhiteSpace(this.禁言原因))
								{
									item.网络连接?.绑定角色?.发送系统消息($"您已被禁言{this.禁言分钟}分钟,原因:{this.禁言原因}");
									if (this.发送邮件)
									{
										item.发送邮件(null, "您已被系统禁言", this.禁言原因, null);
									}
								}
							}
							else if (item.禁言日期.V > DateTime.Now)
							{
								item.发送邮件(null, "解除禁言", "您的禁言已解除", null);
							}
							item.禁言日期.V = ((this.禁言分钟 > 0) ? DateTime.Now.AddMinutes(this.禁言分钟) : default(DateTime));
						}
					}
					return;
				}
				主窗口.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 账号名下没有角色");
			}
			else
			{
				主窗口.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
			}
		}
	}
}
