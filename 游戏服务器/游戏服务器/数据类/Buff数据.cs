using System;
using System.Collections.Generic;
using 游戏服务器.地图类;
using 游戏服务器.模板类;

namespace 游戏服务器.数据类
{
	public sealed class Buff数据 : 游戏数据
	{
		public 地图对象 Buff来源;

		public 地图对象 Buff目标;

		public readonly 数据监视器<ushort> Buff编号;

		public readonly 数据监视器<TimeSpan> 持续时间;

		public readonly 数据监视器<TimeSpan> 剩余时间;

		public readonly 数据监视器<TimeSpan> 处理计时;

		public readonly 数据监视器<byte> 当前层数;

		public readonly 数据监视器<byte> Buff等级;

		public readonly 数据监视器<int> 伤害基数;

		public readonly 数据监视器<int> 护盾数值;

		public readonly 数据监视器<DateTime> 添加时间;

		public int 增减伤害基数;

		public float 增减伤害系数;

		public int 增减属性基数;

		public float 增减属性系数;

		public float 继承属性比例;

		public DateTime 触发时间;

		public bool 可以触发 => 主程.当前时间 >= this.触发时间;

		public Buff效果类型 Buff效果 => this.Buff模板.Buff效果;

		public 技能伤害类型 伤害类型 => this.Buff模板.Buff伤害类型;

		public ushort 触发技能伤害 => this.Buff模板.触发技能伤害;

		public bool 伤害不计神圣 => this.Buff模板.伤害不计神圣;

		public 游戏Buff Buff模板
		{
			get
			{
				if (!游戏Buff.数据表.TryGetValue(this.Buff编号.V, out var value))
				{
					return null;
				}
				return value;
			}
		}

		public bool 增益Buff => this.Buff模板.作用类型 == Buff作用类型.增益类型;

		public bool Buff同步 => this.Buff模板.同步至客户端;

		public bool 到期消失 => this.Buff模板?.到期主动消失 ?? false;

		public bool 离线计算 => this.Buff模板?.下线计算到期 ?? false;

		public bool 到期换图 => this.Buff模板.到期切换地图;

		public ushort 切换地图 => this.Buff模板.切换地图编号;

		public bool 下线消失 => this.Buff模板.角色下线消失;

		public bool 死亡消失 => this.Buff模板.角色死亡消失;

		public bool 换图消失 => this.Buff模板.切换地图消失;

		public bool 绑定武器 => this.Buff模板.切换武器消失;

		public bool 添加冷却 => this.Buff模板.移除添加冷却;

		public ushort 绑定技能 => this.Buff模板.绑定技能等级;

		public ushort 冷却时间 => this.Buff模板.技能冷却时间;

		public int 处理延迟 => this.Buff模板.Buff处理延迟;

		public int 处理间隔 => this.Buff模板.Buff处理间隔;

		public byte 最大层数 => this.Buff模板.Buff最大层数;

		public bool 攻击消失 => this.Buff模板.攻击动作消失;

		public ushort Buff分组
		{
			get
			{
				if (this.Buff模板.分组编号 == 0)
				{
					return this.Buff编号.V;
				}
				return this.Buff模板.分组编号;
			}
		}

		public ushort[] 后接列表 => this.Buff模板.后接Buff列表;

		public ushort[] 连带列表 => this.Buff模板.连带Buff列表;

		public ushort[] 依存列表 => this.Buff模板.依存Buff列表;

		public Dictionary<游戏对象属性, int> 属性加成
		{
			get
			{
				if ((this.Buff效果 & Buff效果类型.属性增减) != 0)
				{
					int num;
					num = 0;
					float num2;
					num2 = 0f;
					if (this.Buff来源 is 玩家实例 玩家实例)
					{
						if (this.Buff模板.继承铭文编号 != 0 && 玩家实例.主体技能表.TryGetValue((ushort)(this.Buff模板.继承铭文编号 / 10), out var v) && v.铭文编号 == this.Buff模板.继承铭文编号 % 10)
						{
							num += this.Buff模板.铭文继承基数;
							num2 += this.Buff模板.铭文继承系数;
						}
						if (this.Buff模板.按照层数计算)
						{
							num += this.当前层数.V;
						}
						if (this.Buff模板.层数计算系数)
						{
							num2 += (float)(this.当前层数.V - 1);
						}
						num += this.增减属性基数;
						num2 += this.增减属性系数;
						if (num <= 0 && num2 <= 0f)
						{
							return this.Buff模板.基础属性增减[this.Buff等级.V];
						}
						Dictionary<游戏对象属性, int> dictionary;
						dictionary = new Dictionary<游戏对象属性, int>();
						{
							foreach (KeyValuePair<游戏对象属性, int> item in this.Buff模板.基础属性增减[this.Buff等级.V])
							{
								dictionary[item.Key] = item.Value + num + (int)((float)item.Value * num2);
							}
							return dictionary;
						}
					}
					return this.Buff模板.基础属性增减[this.Buff等级.V];
				}
				return null;
			}
		}

		public 转换属性 属性转换
		{
			get
			{
				if (this.Buff模板.属性转换 != null && (this.Buff效果 & Buff效果类型.属性增减) != 0)
				{
					return this.Buff模板.属性转换[this.Buff等级.V];
				}
				return 转换属性.空;
			}
		}

		public Buff数据()
		{
		}

		public Buff数据(地图对象 来源, 地图对象 目标, ushort 编号)
		{
			this.Buff目标 = 目标;
			this.Buff来源 = 来源;
			this.Buff编号.V = 编号;
			this.当前层数.V = this.Buff模板.Buff初始层数;
			this.护盾数值.V = this.Buff模板.Buff初始护盾;
			this.持续时间.V = TimeSpan.FromMilliseconds(this.Buff模板.Buff持续时间);
			this.处理计时.V = TimeSpan.FromMilliseconds(this.Buff模板.Buff处理延迟);
			this.继承属性比例 = 0f;
			this.添加时间.V = 主程.当前时间;
			if (来源 is 玩家实例 玩家实例)
			{
				if (this.Buff模板.绑定技能等级 != 0 && 玩家实例.主体技能表.TryGetValue(this.Buff模板.绑定技能等级, out var v))
				{
					this.Buff等级.V = v.技能等级.V;
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.技能等级增加)
				{
					this.护盾数值.V += this.Buff等级.V * this.Buff模板.每级增加数值;
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.角色属性增加)
				{
					this.护盾数值.V += (int)((float)玩家实例[this.Buff模板.护盾角色属性] * this.Buff模板.属性增加系数);
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.特定铭文增加 && 玩家实例.主体技能表.TryGetValue((ushort)(this.Buff模板.护盾铭文技能 / 10), out var v2) && v2.铭文编号 == this.Buff模板.护盾铭文技能 % 10)
				{
					this.护盾数值.V += this.Buff模板.铭文增加数值;
				}
			}
			else if (来源 is 宠物实例 宠物实例)
			{
				if (this.Buff模板.绑定技能等级 != 0 && 宠物实例.宠物主人.主体技能表.TryGetValue(this.Buff模板.绑定技能等级, out var v3))
				{
					this.Buff等级.V = v3.技能等级.V;
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.技能等级增加)
				{
					this.护盾数值.V += this.Buff等级.V * this.Buff模板.每级增加数值;
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.角色属性增加)
				{
					this.护盾数值.V += (int)((float)宠物实例.宠物主人[this.Buff模板.护盾角色属性] * this.Buff模板.属性增加系数);
				}
				if (this.Buff模板.护盾数值增加 && this.Buff模板.特定铭文增加 && 宠物实例.宠物主人.主体技能表.TryGetValue((ushort)(this.Buff模板.护盾铭文技能 / 10), out var v4) && v4.铭文编号 == this.Buff模板.护盾铭文技能 % 10)
				{
					this.护盾数值.V += this.Buff模板.铭文增加数值;
				}
			}
			if (来源 is 玩家实例 玩家实例2)
			{
				if (this.Buff模板.绑定技能等级 != 0 && 玩家实例2.主体技能表.TryGetValue(this.Buff模板.绑定技能等级, out var v5))
				{
					this.Buff等级.V = v5.技能等级.V;
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.技能等级延时)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds(this.Buff等级.V * this.Buff模板.每级延长时间);
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.角色属性延时)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds((float)玩家实例2[this.Buff模板.绑定角色属性] * this.Buff模板.属性延时系数);
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.特定铭文延时 && 玩家实例2.主体技能表.TryGetValue((ushort)(this.Buff模板.特定铭文技能 / 10), out var v6) && v6.铭文编号 == this.Buff模板.特定铭文技能 % 10)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds(this.Buff模板.铭文延长时间);
				}
			}
			else if (来源 is 宠物实例 宠物实例2)
			{
				if (this.Buff模板.绑定技能等级 != 0 && 宠物实例2.宠物主人.主体技能表.TryGetValue(this.Buff模板.绑定技能等级, out var v7))
				{
					this.Buff等级.V = v7.技能等级.V;
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.技能等级延时)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds(this.Buff等级.V * this.Buff模板.每级延长时间);
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.角色属性延时)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds((float)宠物实例2.宠物主人[this.Buff模板.绑定角色属性] * this.Buff模板.属性延时系数);
				}
				if (this.Buff模板.持续时间延长 && this.Buff模板.特定铭文延时 && 宠物实例2.宠物主人.主体技能表.TryGetValue((ushort)(this.Buff模板.特定铭文技能 / 10), out var v8) && v8.铭文编号 == this.Buff模板.特定铭文技能 % 10)
				{
					this.持续时间.V += TimeSpan.FromMilliseconds(this.Buff模板.铭文延长时间);
				}
			}
			this.剩余时间.V = this.持续时间.V;
			if ((this.Buff效果 & Buff效果类型.造成伤害) != 0)
			{
				int num;
				num = ((this.Buff模板.Buff伤害基数?.Length > this.Buff等级.V) ? this.Buff模板.Buff伤害基数[this.Buff等级.V] : 0);
				float num2;
				num2 = ((this.Buff模板.Buff伤害系数?.Length > this.Buff等级.V) ? this.Buff模板.Buff伤害系数[this.Buff等级.V] : 0f);
				if (来源 is 玩家实例 玩家实例3 && this.Buff模板.强化铭文编号 != 0 && 玩家实例3.主体技能表.TryGetValue((ushort)(this.Buff模板.强化铭文编号 / 10), out var v9) && v9.铭文编号 == this.Buff模板.强化铭文编号 % 10)
				{
					num += this.Buff模板.铭文强化基数;
					num2 += this.Buff模板.铭文强化系数;
				}
				int num3;
				num3 = 0;
				switch (this.伤害类型)
				{
				case 技能伤害类型.攻击:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小攻击], 来源[游戏对象属性.最大攻击], 来源[游戏对象属性.幸运等级]);
					break;
				case 技能伤害类型.魔法:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小魔法], 来源[游戏对象属性.最大魔法], 来源[游戏对象属性.幸运等级]);
					break;
				case 技能伤害类型.道术:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小道术], 来源[游戏对象属性.最大道术], 来源[游戏对象属性.幸运等级]);
					break;
				case 技能伤害类型.刺术:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小刺术], 来源[游戏对象属性.最大刺术], 来源[游戏对象属性.幸运等级]);
					break;
				case 技能伤害类型.弓术:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小弓术], 来源[游戏对象属性.最大弓术], 来源[游戏对象属性.幸运等级]);
					break;
				case 技能伤害类型.毒性:
					num3 = 来源[游戏对象属性.最大道术];
					break;
				case 技能伤害类型.神圣:
					num3 = 计算类.计算攻击(来源[游戏对象属性.最小圣伤], 来源[游戏对象属性.最大圣伤], 0);
					break;
				}
				this.伤害基数.V = num + (int)((float)num3 * num2);
			}
			if (目标 is 玩家实例)
			{
				游戏数据网关.Buff数据表.添加数据(this, 分配索引: true);
			}
		}

		public override string ToString()
		{
			return this.Buff模板?.Buff名字;
		}
	}
}
