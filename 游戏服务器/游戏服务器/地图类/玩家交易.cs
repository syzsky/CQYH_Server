using System;
using System.Collections.Generic;
using System.Linq;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public sealed class 玩家交易
	{
		public 玩家实例 交易申请方;

		public 玩家实例 交易接收方;

		public byte 申请方状态;

		public byte 接收方状态;

		public int 申请方金币;

		public int 接收方金币;

		public Dictionary<byte, 物品数据> 申请方物品;

		public Dictionary<byte, 物品数据> 接收方物品;

		public 玩家交易(玩家实例 申请方, 玩家实例 接收方)
		{
			this.申请方物品 = new Dictionary<byte, 物品数据>();
			this.接收方物品 = new Dictionary<byte, 物品数据>();
			this.交易申请方 = 申请方;
			this.交易接收方 = 接收方;
			this.申请方状态 = 1;
			this.接收方状态 = 2;
			this.发送封包(new 交易状态改变
			{
				对象编号 = this.交易申请方.地图编号,
				交易状态 = this.申请方状态,
				对象等级 = this.交易申请方.当前等级
			});
			this.发送封包(new 交易状态改变
			{
				对象编号 = this.交易接收方.地图编号,
				交易状态 = this.接收方状态,
				对象等级 = this.交易接收方.当前等级
			});
		}

		public void 结束交易()
		{
			this.交易申请方.网络连接?.发送封包(new 交易状态改变
			{
				对象编号 = this.交易申请方.地图编号,
				交易状态 = 0,
				对象等级 = this.交易申请方.当前等级
			});
			this.交易接收方.网络连接?.发送封包(new 交易状态改变
			{
				对象编号 = this.交易接收方.地图编号,
				交易状态 = 0,
				对象等级 = this.交易接收方.当前等级
			});
			this.交易申请方.当前交易 = (this.交易接收方.当前交易 = null);
		}

		public void 交换物品()
		{
			if (this.接收方金币 > 0)
			{
				this.交易接收方.扣金币((uint)Math.Ceiling((float)this.接收方金币 * 1.04f));
				this.交易接收方.角色数据.转出金币.V += this.接收方金币;
				主程.添加货币日志(this.交易接收方, "玩家交易物品->" + this.交易申请方.角色数据.角色名字.V, 游戏货币.金币, -(int)Math.Ceiling((float)this.接收方金币 * 1.04f));
			}
			if (this.申请方金币 > 0)
			{
				this.交易申请方.扣金币((uint)Math.Ceiling((float)this.申请方金币 * 1.04f));
				this.交易申请方.角色数据.转出金币.V += this.申请方金币;
				主程.添加货币日志(this.交易申请方, "玩家交易物品->" + this.交易接收方.角色数据.角色名字.V, 游戏货币.金币, -(int)Math.Ceiling((float)this.申请方金币 * 1.04f));
			}
			if (this.接收方物品.Count > 0)
			{
				主程.添加系统日志($"[玩家交易]{this.交易接收方.角色数据.角色名字.V} 转给 {this.交易申请方.角色数据.角色名字.V} 物品:{string.Join(",", this.接收方物品.Values.Select((物品数据 i) => $"{i.物品名字}|{i.物品编号}"))}", hardLog: false);
			}
			if (this.申请方物品.Count > 0)
			{
				主程.添加系统日志($"[玩家交易]{this.交易申请方.角色数据.角色名字.V} 转给 {this.交易接收方.角色数据.角色名字.V} 物品:{string.Join(",", this.接收方物品.Values.Select((物品数据 i) => $"{i.物品名字}|{i.物品编号}"))}", hardLog: false);
			}
			foreach (物品数据 value in this.接收方物品.Values)
			{
				if (value.物品编号 == 80207)
				{
					this.交易接收方.角色数据.转出金币.V += 1000000L;
				}
				else if (value.物品编号 == 80209)
				{
					this.交易接收方.角色数据.转出金币.V += 5000000L;
				}
				this.交易接收方.角色背包.Remove(value.物品位置.V);
				this.交易接收方.网络连接?.发送封包(new 删除玩家物品
				{
					背包类型 = 1,
					物品位置 = value.物品位置.V
				});
			}
			foreach (物品数据 value2 in this.申请方物品.Values)
			{
				if (value2.物品编号 == 80207)
				{
					this.交易申请方.角色数据.转出金币.V += 1000000L;
				}
				else if (value2.物品编号 == 80209)
				{
					this.交易申请方.角色数据.转出金币.V += 5000000L;
				}
				this.交易申请方.角色背包.Remove(value2.物品位置.V);
				this.交易申请方.网络连接?.发送封包(new 删除玩家物品
				{
					背包类型 = 1,
					物品位置 = value2.物品位置.V
				});
			}
			foreach (物品数据 value3 in this.申请方物品.Values)
			{
				byte b;
				b = 0;
				while (b < this.交易接收方.背包大小)
				{
					if (this.交易接收方.角色背包.ContainsKey(b))
					{
						b++;
						continue;
					}
					this.交易接收方.角色背包.Add(b, value3);
					value3.物品容器.V = 1;
					value3.物品位置.V = b;
					this.交易接收方.网络连接?.发送封包(new 玩家物品变动
					{
						物品描述 = value3.字节描述()
					});
					主程.添加物品日志(this.交易接收方, "玩家交易物品", value3, 1, "来自->" + this.交易申请方.角色数据.角色名字.V);
					break;
				}
			}
			foreach (物品数据 value4 in this.接收方物品.Values)
			{
				byte b2;
				b2 = 0;
				while (b2 < this.交易申请方.背包大小)
				{
					if (this.交易申请方.角色背包.ContainsKey(b2))
					{
						b2++;
						continue;
					}
					this.交易申请方.角色背包.Add(b2, value4);
					value4.物品容器.V = 1;
					value4.物品位置.V = b2;
					this.交易申请方.网络连接?.发送封包(new 玩家物品变动
					{
						物品描述 = value4.字节描述()
					});
					主程.添加物品日志(this.交易申请方, "玩家交易物品", value4, 1, "来自->" + this.交易接收方.角色数据.角色名字.V);
					break;
				}
			}
			if (this.申请方金币 > 0)
			{
				this.交易接收方.修改货币("+", 游戏货币.金币, (uint)this.申请方金币);
				主程.添加货币日志(this.交易接收方, "玩家交易物品->" + this.交易申请方.角色数据.角色名字.V, 游戏货币.金币, this.申请方金币);
			}
			if (this.接收方金币 > 0)
			{
				this.交易申请方.修改货币("+", 游戏货币.金币, (uint)this.接收方金币);
				主程.添加货币日志(this.交易申请方, "玩家交易物品->" + this.交易接收方.角色数据.角色名字.V, 游戏货币.金币, this.接收方金币);
			}
			this.更改状态(6);
			this.结束交易();
		}

		public void 更改状态(byte 状态, 玩家实例 玩家 = null)
		{
			if (玩家 == null)
			{
				this.申请方状态 = (this.接收方状态 = 状态);
				this.发送封包(new 交易状态改变
				{
					对象编号 = this.交易申请方.地图编号,
					交易状态 = this.申请方状态,
					对象等级 = this.交易申请方.当前等级
				});
				this.发送封包(new 交易状态改变
				{
					对象编号 = this.交易接收方.地图编号,
					交易状态 = this.接收方状态,
					对象等级 = this.交易接收方.当前等级
				});
			}
			else if (玩家 == this.交易申请方)
			{
				this.申请方状态 = 状态;
				this.发送封包(new 交易状态改变
				{
					对象编号 = 玩家.地图编号,
					交易状态 = 玩家.交易状态,
					对象等级 = 玩家.当前等级
				});
			}
			else if (玩家 == this.交易接收方)
			{
				this.接收方状态 = 状态;
				this.发送封包(new 交易状态改变
				{
					对象编号 = 玩家.地图编号,
					交易状态 = 玩家.交易状态,
					对象等级 = 玩家.当前等级
				});
			}
			else
			{
				this.结束交易();
			}
		}

		public void 放入金币(玩家实例 玩家, int 数量)
		{
			if (玩家 == this.交易申请方)
			{
				this.申请方金币 = 数量;
				this.发送封包(new 放入交易金币
				{
					对象编号 = 玩家.地图编号,
					金币数量 = 数量
				});
			}
			else if (玩家 == this.交易接收方)
			{
				this.接收方金币 = 数量;
				this.发送封包(new 放入交易金币
				{
					对象编号 = 玩家.地图编号,
					金币数量 = 数量
				});
			}
			else
			{
				this.结束交易();
			}
		}

		public void 放入物品(玩家实例 玩家, 物品数据 物品, byte 位置)
		{
			if (!(物品 is 装备数据 装备数据) || !装备数据.灵魂绑定.V)
			{
				if (玩家 == this.交易申请方)
				{
					this.申请方物品.Add(位置, 物品);
					this.发送封包(new 放入交易物品
					{
						对象编号 = 玩家.地图编号,
						放入位置 = 位置,
						放入物品 = 1,
						物品描述 = 物品.字节描述()
					});
				}
				else if (玩家 == this.交易接收方)
				{
					this.接收方物品.Add(位置, 物品);
					this.发送封包(new 放入交易物品
					{
						对象编号 = 玩家.地图编号,
						放入位置 = 位置,
						放入物品 = 1,
						物品描述 = 物品.字节描述()
					});
				}
				else
				{
					this.结束交易();
				}
			}
		}

		public bool 背包已满(out 玩家实例 玩家)
		{
			玩家 = null;
			if (this.交易申请方.背包剩余 < this.接收方物品.Count)
			{
				玩家 = this.交易申请方;
				return true;
			}
			if (this.交易接收方.背包剩余 < this.申请方物品.Count)
			{
				玩家 = this.交易接收方;
				return true;
			}
			return false;
		}

		public bool 金币重复(玩家实例 玩家)
		{
			if (玩家 == this.交易申请方)
			{
				return this.申请方金币 != 0;
			}
			if (玩家 == this.交易接收方)
			{
				return this.接收方金币 != 0;
			}
			return true;
		}

		public bool 物品重复(玩家实例 玩家, 物品数据 物品)
		{
			if (玩家 == this.交易申请方)
			{
				return this.申请方物品.Values.FirstOrDefault((物品数据 O) => O == 物品) != null;
			}
			if (玩家 == this.交易接收方)
			{
				return this.接收方物品.Values.FirstOrDefault((物品数据 O) => O == 物品) != null;
			}
			return true;
		}

		public bool 物品重复(玩家实例 玩家, byte 位置)
		{
			if (玩家 == this.交易申请方)
			{
				return this.申请方物品.ContainsKey(位置);
			}
			if (玩家 == this.交易接收方)
			{
				return this.接收方物品.ContainsKey(位置);
			}
			return true;
		}

		public byte 对方状态(玩家实例 玩家)
		{
			if (玩家 == this.交易接收方)
			{
				return this.申请方状态;
			}
			if (玩家 == this.交易申请方)
			{
				return this.接收方状态;
			}
			return 0;
		}

		public void 发送封包(游戏封包 封包)
		{
			this.交易接收方.网络连接?.发送封包(封包);
			this.交易申请方.网络连接?.发送封包(封包);
		}

		public 玩家实例 对方玩家(玩家实例 玩家)
		{
			if (玩家 == this.交易接收方)
			{
				return this.交易申请方;
			}
			return this.交易接收方;
		}

		public Dictionary<byte, 物品数据> 对方物品(玩家实例 玩家)
		{
			if (玩家 == this.交易接收方)
			{
				return this.申请方物品;
			}
			if (玩家 == this.交易申请方)
			{
				return this.接收方物品;
			}
			return null;
		}
	}
}
