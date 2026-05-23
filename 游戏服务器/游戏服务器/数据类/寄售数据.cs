using System;
using System.Collections.Generic;
using System.IO;
using 游戏服务器.网络类;

namespace 游戏服务器.数据类
{
	public sealed class 寄售数据 : 游戏数据
	{
		public readonly 数据监视器<角色数据> 卖方玩家;

		public readonly 数据监视器<物品数据> 物品数据;

		public readonly 数据监视器<DateTime> 上架时间;

		public readonly 数据监视器<DateTime> 下架时间;

		public readonly 数据监视器<int> 商品价格;

		public long 订单号码 => base.数据索引.V;

		public int 卖家编号 => this.卖方玩家.V.数据索引.V;

		public int 物品编号 => this.物品数据.V?.物品编号 ?? 0;

		public 寄售数据()
		{
		}

		public 寄售数据(角色数据 卖家数据, 物品数据 出售物品, int 出售价格, int 限制时间)
		{
			this.卖方玩家.V = 卖家数据;
			this.物品数据.V = 出售物品;
			this.商品价格.V = 出售价格;
			this.上架时间.V = 主程.当前时间;
			this.下架时间.V = 主程.当前时间.AddSeconds(限制时间);
			游戏数据网关.寄售数据表.添加数据(this, 分配索引: true);
		}

		public override string ToString()
		{
			if (this.物品数据.V == null)
			{
				this.删除数据();
				return "";
			}
			return $"{this.卖方玩家.V.角色名字} {this.物品数据.V.物品名字} {this.商品价格.V}";
		}

		public static 寄售数据 获取寄售(long 订单编号)
		{
			游戏数据网关.寄售数据表.数据表.TryGetValue((int)订单编号, out var value);
			return (寄售数据)value;
		}

		public static bool 发送商品(long 订单编号, 角色数据 角色 = null)
		{
			游戏数据网关.寄售数据表.数据表.TryGetValue((int)订单编号, out var value);
			寄售数据 寄售数据2;
			寄售数据2 = (寄售数据)value;
			if (寄售数据2 == null)
			{
				return false;
			}
			if (寄售数据2.物品数据.V == null)
			{
				寄售数据2.删除数据();
				return false;
			}
			if (角色 == null)
			{
				寄售数据2.卖方玩家.V.发送邮件(null, "寄售下架商品", "您寄售的[" + 寄售数据2.物品数据.V.物品模板.物品名字 + "]已下架", 寄售数据2.物品数据.V);
				寄售数据2.卖方玩家.V.网络连接?.SendRaw(514, 14, new byte[12]
				{
					2, 50, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0
				});
			}
			else
			{
				uint num;
				num = Math.Max(1u, (uint)((double)寄售数据2.商品价格.V * 0.95));
				寄售数据2.卖方玩家.V.元宝数量 += num;
				寄售数据2.卖方玩家.V.网络连接?.发送封包(new 同步元宝数量
				{
					元宝数量 = 寄售数据2.卖方玩家.V.元宝数量
				});
				主程.添加货币日志(寄售数据2.卖方玩家.V, "玩家寄售出售", 游戏货币.元宝, num);
				寄售数据2.卖方玩家.V.发送邮件(null, "寄售商品售出", $"您寄售的[{寄售数据2.物品数据.V.物品模板.物品名字}]已被[{角色.角色名字}]购买\r\n商品售价[{(float)寄售数据2.商品价格.V / 100f}]扣除5%手续费后您获得[{(float)num / 100f}]元宝，已直接进入您的背包\r\n售出时间[{主程.当前时间}]", null);
				角色.发送邮件(null, "寄售购买商品", $"您花费了[{(float)寄售数据2.商品价格.V / 100f}]元宝从[{寄售数据2.卖方玩家.V.角色名字}]处购买[{寄售数据2.物品数据.V.物品模板.物品名字}]", 寄售数据2.物品数据.V);
				主程.添加物品日志(角色, "寄售购买物品", 寄售数据2.物品数据.V, 1, $"卖方:{寄售数据2.卖方玩家.V.角色名字.V},价格:{(float)寄售数据2.商品价格.V / 100f}");
			}
			寄售数据2.删除数据();
			return true;
		}

		public static byte[] 查询商品数据(int 筛选条件, out int 数量)
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			数量 = 0;
			foreach (KeyValuePair<int, 游戏数据> item in 游戏数据网关.寄售数据表.数据表)
			{
				if (数量 <= 254)
				{
					寄售数据 寄售数据2;
					寄售数据2 = (寄售数据)item.Value;
					if (寄售数据2.物品数据.V == null)
					{
						寄售数据2.删除数据();
					}
					else if ((筛选条件 != 1 || 寄售数据2.物品数据.V.物品类型 == 物品使用分类.武器) && (筛选条件 != 2 || 寄售数据2.物品数据.V.防具物品) && (筛选条件 != 3 || 寄售数据2.物品数据.V.饰品物品) && (筛选条件 != 4 || 寄售数据2.物品数据.V.物品类型 == 物品使用分类.可用杂物) && (筛选条件 != 5 || (寄售数据2.物品数据.V.物品类型 != 物品使用分类.武器 && 寄售数据2.物品数据.V.物品类型 != 物品使用分类.可用杂物 && !寄售数据2.物品数据.V.防具物品 && !寄售数据2.物品数据.V.饰品物品)))
					{
						binaryWriter.Write(寄售数据2.订单号码);
						binaryWriter.Write(寄售数据2.卖家编号);
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.上架时间.V));
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.下架时间.V));
						binaryWriter.Write(寄售数据2.商品价格.V);
						binaryWriter.Write(250);
						binaryWriter.Write(寄售数据2.物品数据.V.字节描述());
						数量++;
					}
					continue;
				}
				break;
			}
			return memoryStream.ToArray();
		}

		public static byte[] 我的商品数据(角色数据 玩家, out int 数量)
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			数量 = 0;
			foreach (KeyValuePair<int, 游戏数据> item in 游戏数据网关.寄售数据表.数据表)
			{
				if (数量 <= 254)
				{
					寄售数据 寄售数据2;
					寄售数据2 = (寄售数据)item.Value;
					if (寄售数据2.物品数据.V == null)
					{
						寄售数据2.删除数据();
					}
					else if (寄售数据2.卖家编号 == 玩家.数据索引.V)
					{
						binaryWriter.Write(寄售数据2.订单号码);
						binaryWriter.Write(寄售数据2.卖家编号);
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.上架时间.V));
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.下架时间.V));
						binaryWriter.Write(寄售数据2.商品价格.V);
						binaryWriter.Write(250);
						binaryWriter.Write(寄售数据2.物品数据.V.字节描述());
						数量++;
					}
					continue;
				}
				break;
			}
			return memoryStream.ToArray();
		}

		public static byte[] 指定商品数据(int 物品编号, out int 数量)
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			数量 = 0;
			foreach (KeyValuePair<int, 游戏数据> item in 游戏数据网关.寄售数据表.数据表)
			{
				if (数量 <= 254)
				{
					寄售数据 寄售数据2;
					寄售数据2 = (寄售数据)item.Value;
					if (寄售数据2.物品数据.V == null)
					{
						寄售数据2.删除数据();
					}
					else if (寄售数据2.物品编号 == 物品编号)
					{
						binaryWriter.Write(寄售数据2.订单号码);
						binaryWriter.Write(寄售数据2.卖家编号);
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.上架时间.V));
						binaryWriter.Write(0);
						binaryWriter.Write(计算类.时间转换(寄售数据2.下架时间.V));
						binaryWriter.Write(寄售数据2.商品价格.V);
						binaryWriter.Write(250);
						binaryWriter.Write(寄售数据2.物品数据.V.字节描述());
						数量++;
					}
					continue;
				}
				break;
			}
			return memoryStream.ToArray();
		}

		public void 处理数据()
		{
			if (主程.当前时间 > this.下架时间.V)
			{
				寄售数据.发送商品(this.订单号码);
			}
		}
	}
}
