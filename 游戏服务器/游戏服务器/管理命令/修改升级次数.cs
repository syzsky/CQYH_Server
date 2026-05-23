using System;
using System.Linq;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 修改升级次数 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public string 装备位置;

		[字段描述(0, 排序 = 2, 可选 = true)]
		public byte 升级次数;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (this.升级次数 <= 9 && this.升级次数 >= 0)
			{
				装备穿戴部位 装备穿戴部位;
				if (int.TryParse(this.装备位置, out var result))
				{
					if (!Enum.IsDefined(typeof(装备穿戴部位), result))
					{
						主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 装备位置 不是正确的数字\r\n" + string.Join("\r\n", from 装备穿戴部位 v in Enum.GetValues(typeof(装备穿戴部位))
							select $"{v}={v}"));
						return;
					}
					装备穿戴部位 = (装备穿戴部位)result;
				}
				else
				{
					if (!Enum.TryParse(typeof(装备穿戴部位), this.装备位置, out object result2))
					{
						主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 未知的装备位置");
						return;
					}
					装备穿戴部位 = (装备穿戴部位)result2;
				}
				if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) || !(value is 角色数据 角色数据))
				{
					return;
				}
				if (!角色数据.角色装备.TryGetValue((byte)装备穿戴部位, out var v2))
				{
					主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行失败, {装备穿戴部位} 未穿戴装备");
					return;
				}
				byte v3;
				v3 = v2.升级次数.V;
				v2.升级次数.V = this.升级次数;
				switch (角色数据.角色职业.V)
				{
				case 游戏对象职业.法师:
					v2.升级魔法.V = this.升级次数;
					break;
				case 游戏对象职业.刺客:
					v2.升级刺术.V = this.升级次数;
					break;
				case 游戏对象职业.弓手:
					v2.升级弓术.V = this.升级次数;
					break;
				case 游戏对象职业.道士:
					v2.升级道术.V = this.升级次数;
					break;
				case 游戏对象职业.战士:
				case 游戏对象职业.龙枪:
					v2.升级攻击.V = this.升级次数;
					break;
				}
				角色数据.网络连接?.发送封包(new 玩家物品变动
				{
					物品描述 = v2.字节描述()
				});
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行成功, {装备穿戴部位} 升级次数从 {v3} -> {this.升级次数}");
			}
			else
			{
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 升级次数只能0-9");
			}
		}
	}
}
