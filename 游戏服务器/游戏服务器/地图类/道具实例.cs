using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public class 道具实例 : 地图对象
	{
		private List<ChestPlayerOpener> 操作对象列表 = new List<ChestPlayerOpener>();

		public DateTime 存活时间;

		public DateTime 刷新时间;

		public 玩家实例 开包玩家;

		public Point[] 出生范围;

		public override 游戏对象类型 对象类型 => 游戏对象类型.Chest;

		public override 技能范围类型 对象体型 => 技能范围类型.单体1x1;

		public 地图道具 Template { get; set; }

		public 道具实例(地图道具 template, 地图实例 map, 游戏方向 direction, Point[] 范围)
		{
			this.Template = template;
			this.当前地图 = map;
			this.当前方向 = direction;
			this.出生范围 = 范围;
			this.获取坐标();
			this.地图编号 = ++地图处理网关.道具编号;
			地图处理网关.添加地图对象(this);
			this.更新对象属性();
			base.次要对象 = false;
			this.对象死亡 = false;
			this.阻塞网格 = false;
			base.绑定网格();
			base.更新邻居时处理();
			this.刷新时间 = DateTime.MinValue;
		}

		public 道具实例(int template, 地图实例 map, 游戏方向 direction, Point[] 范围, int 存活时间)
		{
			if (地图道具.数据表.TryGetValue(template, out var value))
			{
				this.Template = value;
			}
			else
			{
				this.Template = 地图道具.数据表[0];
			}
			this.当前地图 = map;
			this.当前方向 = direction;
			this.出生范围 = 范围;
			this.获取坐标();
			this.地图编号 = ++地图处理网关.道具编号;
			地图处理网关.添加地图对象(this);
			this.更新对象属性();
			base.次要对象 = false;
			this.对象死亡 = false;
			this.阻塞网格 = false;
			base.绑定网格();
			base.更新邻居时处理();
			this.刷新时间 = DateTime.MinValue;
			this.存活时间 = 主程.当前时间.AddMinutes(存活时间);
		}

		public void 获取坐标()
		{
			this.当前坐标 = this.出生范围[主程.随机数.Next(0, this.出生范围.Length)];
			Point point;
			point = this.当前坐标;
			int num;
			num = 0;
			while (true)
			{
				if (num < 120)
				{
					if (this.当前地图.能否通行(point = 计算类.螺旋坐标(this.当前坐标, num)))
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			this.当前坐标 = point;
		}

		public void ActivateObject()
		{
			if (!base.激活对象)
			{
				base.激活对象 = true;
				地图处理网关.添加激活对象(this);
			}
		}

		public void 复活处理()
		{
			this.获取坐标();
			this.ActivateObject();
			base.次要对象 = false;
			base.绑定网格();
			base.更新邻居时处理();
			this.刷新时间 = DateTime.MinValue;
		}

		public void 发送封包邻居(游戏封包 封包)
		{
			if (base.邻居列表.Count == 0)
			{
				base.更新邻居时处理();
			}
			foreach (地图对象 item in base.邻居列表.ToList())
			{
				if (item is 玩家实例 玩家实例2)
				{
					玩家实例2.网络连接?.发送封包(封包);
				}
			}
		}

		public void 消失处理(玩家实例 W)
		{
			base.清空邻居时处理();
			base.次要对象 = true;
			地图处理网关.添加次要对象(this);
			if (base.激活对象)
			{
				base.激活对象 = false;
				地图处理网关.移除激活对象(this);
			}
			base.解绑网格();
		}

		public override void 处理对象数据()
		{
			base.处理对象数据();
			if (this.存活时间 > 主程.当前时间)
			{
				base.删除对象();
				this.当前地图?.移除对象(this);
			}
			if (this.刷新时间 != DateTime.MinValue && 主程.当前时间 >= this.刷新时间)
			{
				this.复活处理();
			}
			ChestPlayerOpener[] array;
			array = this.操作对象列表.ToArray();
			int num;
			num = 0;
			ChestPlayerOpener chestPlayerOpener;
			while (true)
			{
				if (num >= array.Length)
				{
					return;
				}
				chestPlayerOpener = array[num];
				if (chestPlayerOpener.Player.操作道具 && !chestPlayerOpener.OpenCompleted && 主程.当前时间 >= chestPlayerOpener.EndOpensTime)
				{
					chestPlayerOpener.Player.操作道具 = false;
					if (this.Template.刷新时间 == 0 || (this.开包玩家 == chestPlayerOpener.Player && !this.开包玩家.对象死亡 && this.开包玩家.摆摊状态 <= 0 && this.开包玩家.交易状态 < 3))
					{
						this.发送封包邻居(new 结束操作道具
						{
							玩家编号 = chestPlayerOpener.Player.地图编号,
							对象编号 = this.地图编号
						});
						if (this.Template.刷新时间 != 0 && this.Template.掉落模板 != null)
						{
							List<物品数据> list;
							list = chestPlayerOpener.Player.查找背包物品(chestPlayerOpener.KeyId, chestPlayerOpener.KeyCost);
							if (list != null && list.Any())
							{
								base.掉落(this.Template.掉落模板.怪物掉落物品, chestPlayerOpener.Player, (decimal)chestPlayerOpener.Rate / 100000m, null, 0, this.Template.道具名字);
							}
						}
						游戏宝箱[] array2;
						array2 = chestPlayerOpener.Player.FilterItemTreasures(this.Template.道具列表);
						if (array2 != null)
						{
							游戏宝箱[] array3;
							array3 = array2;
							foreach (游戏宝箱 游戏宝箱 in array3)
							{
								if (!游戏物品.检索表.TryGetValue(游戏宝箱.物品名字, out var value))
								{
									continue;
								}
								int num2;
								num2 = ((游戏宝箱.物品数量 < 0) ? 计算类.范围随机(1, -游戏宝箱.物品数量) : 游戏宝箱.物品数量);
								if (游戏宝箱.物品叠加)
								{
									new 物品实例(value, null, this.当前地图, this.当前坐标, new HashSet<角色数据> { chestPlayerOpener.Player.角色数据 }, 1, 物品绑定: false, this);
								}
								else if (value.物品持久 == 0)
								{
									new 物品实例(value, null, this.当前地图, this.当前坐标, new HashSet<角色数据> { chestPlayerOpener.Player.角色数据 }, num2, 物品绑定: false, this);
								}
								else
								{
									for (int j = 0; j < num2; j++)
									{
										new 物品实例(value, null, this.当前地图, this.当前坐标, new HashSet<角色数据> { chestPlayerOpener.Player.角色数据 }, 1, 物品绑定: false, this);
									}
								}
								if (游戏宝箱.公告提示 != null && 游戏宝箱.公告提示 != string.Empty)
								{
									网络服务网关.发送公告(游戏宝箱.公告提示.Replace("%P%", chestPlayerOpener.Player.对象名字));
								}
							}
						}
						chestPlayerOpener.Player.CallDefaultNPC(DefaultNPCType.OpenDropBox, true, this.Template.道具编号);
						this.发送封包邻居(new 同步道具次数
						{
							玩家编号 = chestPlayerOpener.Player.地图编号,
							对象编号 = this.地图编号
						});
						this.发送封包邻居(new 对象离开视野
						{
							对象编号 = this.地图编号,
							消失方式 = 1
						});
						if (this.Template.刷新时间 != 0)
						{
							break;
						}
						chestPlayerOpener.OpenCompleted = true;
						chestPlayerOpener.NextAppearTime = 主程.当前时间.AddMinutes(1.0);
					}
				}
				else if (chestPlayerOpener.OpenCompleted && 主程.当前时间 >= chestPlayerOpener.NextAppearTime)
				{
					this.操作对象列表.Remove(chestPlayerOpener);
					if (this.开包玩家 == chestPlayerOpener.Player)
					{
						this.开包玩家 = null;
					}
				}
				num++;
			}
			this.消失处理(chestPlayerOpener.Player);
			this.刷新时间 = 主程.当前时间.AddSeconds(this.Template.刷新时间);
			chestPlayerOpener.Player.邻居列表.Remove(this);
			this.操作对象列表.Clear();
			this.开包玩家 = null;
		}

		public bool IsAlredyOpened(玩家实例 player)
		{
			return !this.操作对象列表.Any((ChestPlayerOpener x) => x.Player == player);
		}

		public void Open(玩家实例 player)
		{
			if (player.操作道具)
			{
				player.发送封包(new 游戏错误提示
				{
					错误代码 = 3588
				});
				return;
			}
			player.探索道具 = new ChestPlayerOpener
			{
				Player = player,
				道具 = this
			};
			player.探索道具.ScriptOp = false;
			player.CallDefaultNPC(DefaultNPCType.TryOpenDropBox, false, this.Template.道具编号);
			if (player.探索道具.ScriptOp)
			{
				return;
			}
			if (this.Template.刷新时间 != 0)
			{
				if (this.刷新时间 != DateTime.MinValue)
				{
					return;
				}
				if (this.开包玩家 != null && this.开包玩家.网络连接 != null)
				{
					player.发送封包(new 游戏错误提示
					{
						错误代码 = 3588
					});
					return;
				}
			}
			this.开包玩家 = player;
			player.探索道具.Opening();
			this.操作对象列表.Add(player.探索道具);
		}

		public void Stop(ChestPlayerOpener opener)
		{
			opener.Player.探索道具.ScriptOp = false;
			opener.Player.CallDefaultNPC(DefaultNPCType.StopOpenDropBox, false, this.Template.道具编号);
			if (!opener.Player.探索道具.ScriptOp)
			{
				if (this.Template.刷新时间 != 0 && this.开包玩家 == opener.Player)
				{
					this.开包玩家 = null;
				}
				this.操作对象列表.Remove(opener);
				opener.Cancel();
			}
		}

		public override void Process(DelayedAction action)
		{
		}
	}
}
