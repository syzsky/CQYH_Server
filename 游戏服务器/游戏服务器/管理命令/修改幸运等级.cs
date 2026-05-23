using System;
using System.Linq;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 修改幸运等级 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public string 装备位置;

		[字段描述(0, 排序 = 2, 可选 = true)]
		public byte 幸运等级;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
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
			if (装备穿戴部位 == 装备穿戴部位.武器)
			{
				if (this.幸运等级 > 7 || this.幸运等级 < 0)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行失败, {装备穿戴部位} 幸运等级只能0-7");
					return;
				}
			}
			else if (this.幸运等级 > 2 || this.幸运等级 < 0)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行失败, {装备穿戴部位} 幸运等级只能0-2");
				return;
			}
			if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
			{
				if (!角色数据.角色装备.TryGetValue((byte)装备穿戴部位, out var v2))
				{
					主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行失败, {装备穿戴部位} 未穿戴装备");
					return;
				}
				sbyte v3;
				v3 = v2.幸运等级.V;
				v2.幸运等级.V = (sbyte)this.幸运等级;
				角色数据.网络连接?.发送封包(new 玩家物品变动
				{
					物品描述 = v2.字节描述()
				});
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行成功, {装备穿戴部位} 幸运等级从 {v3} -> {this.幸运等级}");
			}
		}
	}
}
