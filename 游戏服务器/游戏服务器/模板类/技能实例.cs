using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.模板类
{
	public class 技能实例
	{
		public 游戏技能 技能模板;

		public 技能数据 技能数据;

		public 地图对象 技能来源;

		public byte 动作编号;

		public byte 分段编号;

		public 地图实例 释放地图;

		public 地图对象 技能目标;

		public Point 技能锚点;

		public Point 释放位置;

		public DateTime 释放时间;

		public 技能实例 父类技能;

		public bool 目标借位;

		public Dictionary<int, 命中详情> 命中列表;

		public int 飞行耗时;

		public int 攻速缩减;

		public bool 经验增加;

		public DateTime 处理计时;

		public DateTime 预约时间;

		public SortedDictionary<int, 技能任务> 节点列表;

		public bool 是否中断;

		public int 来源编号 => this.技能来源.地图编号;

		public byte 分组编号 => this.技能模板.技能分组编号;

		public byte 铭文编号 => this.技能模板.自身铭文编号;

		public ushort 技能编号 => this.技能模板.自身技能编号;

		public bool 动作打断 => this.技能模板.动作打断;

		public byte 技能等级
		{
			get
			{
				if (this.技能模板.绑定等级编号 != 0)
				{
					if (this.技能来源 is 玩家实例 玩家实例 && 玩家实例.主体技能表.TryGetValue(this.技能模板.绑定等级编号, out var v))
					{
						return v.技能等级.V;
					}
					if (this.技能来源 is 陷阱实例 { 陷阱来源: 玩家实例 陷阱来源 } && 陷阱来源.主体技能表.TryGetValue(this.技能模板.绑定等级编号, out var v2))
					{
						return v2.技能等级.V;
					}
					return 0;
				}
				return 0;
			}
		}

		public bool 检查计数 => this.技能模板.检查技能计数;

		public 技能实例(地图对象 技能来源, 游戏技能 技能模板, 技能数据 技能数据, byte 动作编号, 地图实例 释放地图, Point 释放位置, 地图对象 技能目标, Point 技能锚点, 技能实例 父类技能, Dictionary<int, 命中详情> 命中列表 = null, bool 目标借位 = false)
		{
			this.技能来源 = 技能来源;
			this.技能模板 = 技能模板;
			this.技能数据 = 技能数据;
			this.动作编号 = 动作编号;
			this.释放地图 = 释放地图;
			this.释放位置 = 释放位置;
			this.技能目标 = 技能目标;
			this.技能锚点 = 技能锚点;
			this.父类技能 = 父类技能;
			this.释放时间 = 主程.当前时间;
			this.目标借位 = 目标借位;
			this.是否中断 = false;
			this.命中列表 = 命中列表 ?? new Dictionary<int, 命中详情>();
			this.节点列表 = new SortedDictionary<int, 技能任务>(技能模板.节点列表);
			if (this.节点列表.Count != 0)
			{
				this.技能来源.技能任务.Add(this);
				this.预约时间 = this.释放时间.AddMilliseconds(this.飞行耗时 + this.节点列表.First().Key);
			}
			if (技能来源 is 玩家实例 玩家实例 && 玩家实例.生效龙卫.Count > 0)
			{
				玩家实例.龙卫添加自身BUFF(技能模板.自身技能编号, 技能模板.自身铭文编号);
			}
		}

		public void 处理任务()
		{
			if (this.是否中断)
			{
				this.技能中断();
			}
			else if (this.节点列表.Count == 0)
			{
				this.技能来源.技能任务.Remove(this);
			}
			else
			{
				if ((this.预约时间 - this.处理计时).TotalMilliseconds > 5.0 && 主程.当前时间 < this.预约时间)
				{
					return;
				}
				KeyValuePair<int, 技能任务> keyValuePair;
				keyValuePair = this.节点列表.First();
				this.节点列表.Remove(keyValuePair.Key);
				技能任务 value;
				value = keyValuePair.Value;
				this.处理计时 = this.预约时间;
				if (value != null)
				{
					地图对象 真正来源;
					真正来源 = ((this.技能来源 is 陷阱实例 陷阱实例) ? 陷阱实例.陷阱来源 : this.技能来源);
					if (value is A_00_触发子类技能 a_00_触发子类技能)
					{
						if (a_00_触发子类技能.触发技能名字 != null && 游戏技能.数据表.TryGetValue(a_00_触发子类技能.触发技能名字, out var value2))
						{
							bool flag;
							flag = true;
							if (a_00_触发子类技能.计算触发概率)
							{
								float num;
								num = ((!a_00_触发子类技能.计算幸运概率) ? (a_00_触发子类技能.技能触发概率 + ((a_00_触发子类技能.增加概率Buff == 0 || !真正来源.Buff列表.ContainsKey(a_00_触发子类技能.增加概率Buff)) ? 0f : a_00_触发子类技能.Buff增加系数)) : 计算类.计算幸运(真正来源[游戏对象属性.幸运等级]));
								if (真正来源 is 玩家实例 玩家实例)
								{
									num += 玩家实例.龙卫技能触发概率(value2.自身技能编号);
								}
								if (真正来源 is 宠物实例 宠物实例)
								{
									num += 宠物实例.宠物主人.龙卫技能触发概率(value2.自身技能编号);
								}
								flag = 计算类.计算概率(num);
							}
							if (flag && a_00_触发子类技能.验证自身Buff)
							{
								if (真正来源.Buff列表.TryGetValue(a_00_触发子类技能.自身Buff编号, out var v))
								{
									if (a_00_触发子类技能.检测BUFF层数 > 0 && v.当前层数.V != a_00_触发子类技能.检测BUFF层数)
									{
										flag = false;
									}
									else if (a_00_触发子类技能.触发成功移除)
									{
										真正来源.移除Buff时处理(a_00_触发子类技能.自身Buff编号);
									}
								}
								else
								{
									flag = false;
								}
							}
							if (flag && a_00_触发子类技能.验证铭文技能 && 真正来源 is 玩家实例 玩家实例2)
							{
								int num2;
								num2 = a_00_触发子类技能.所需铭文编号 / 10;
								int num3;
								num3 = a_00_触发子类技能.所需铭文编号 % 10;
								flag = 玩家实例2.主体技能表.TryGetValue((ushort)num2, out var v2) && (a_00_触发子类技能.同组铭文无效 ? (num3 == v2.铭文编号) : (num3 == 0 || num3 == v2.铭文编号));
							}
							if (flag && a_00_触发子类技能.检测技能等级)
							{
								flag = this.技能等级 == a_00_触发子类技能.所需技能等级;
							}
							if (flag && a_00_触发子类技能.检测武器编号 && 真正来源 is 玩家实例 玩家实例3)
							{
								flag = 玩家实例3.角色数据.角色装备.TryGetValue(0, out var v3) && v3.物品编号 == a_00_触发子类技能.所需武器编号;
							}
							if (flag && a_00_触发子类技能.目标距离大于 > 0)
							{
								foreach (KeyValuePair<int, 命中详情> item in this.命中列表)
								{
									flag = 计算类.网格距离(this.技能来源.当前坐标, item.Value.技能目标.当前坐标) > a_00_触发子类技能.目标距离大于;
								}
							}
							if (flag && a_00_触发子类技能.目标距离小于 > 0)
							{
								foreach (KeyValuePair<int, 命中详情> item2 in this.命中列表)
								{
									flag = 计算类.网格距离(this.技能来源.当前坐标, item2.Value.技能目标.当前坐标) < a_00_触发子类技能.目标距离小于;
								}
							}
							if (flag && a_00_触发子类技能.检测目标数量)
							{
								flag = this.命中列表.Count == a_00_触发子类技能.限定目标数量;
							}
							if (flag)
							{
								switch (a_00_触发子类技能.技能触发方式)
								{
								case 技能触发方式.原点位置绝对触发:
									new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, this.技能目标, this.释放位置, this);
									break;
								case 技能触发方式.锚点位置绝对触发:
									new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, this.技能目标, this.技能锚点, this);
									break;
								case 技能触发方式.刺杀位置绝对触发:
									new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, this.技能目标, 计算类.前方坐标(this.释放位置, this.技能锚点, 2), this);
									break;
								case 技能触发方式.目标命中绝对触发:
									foreach (KeyValuePair<int, 命中详情> item3 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item3.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v9) || (a_00_触发子类技能.检测BUFF层数 > 0 && v9.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item3.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if ((item3.Value.技能反馈 & 技能命中反馈.闪避) == 0 && (item3.Value.技能反馈 & 技能命中反馈.丢失) == 0)
										{
											new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, (this.父类技能 == null) ? this.释放位置 : this.技能锚点, item3.Value.技能目标, item3.Value.技能目标.当前坐标, this);
										}
									}
									break;
								case 技能触发方式.怪物死亡绝对触发:
									foreach (KeyValuePair<int, 命中详情> item4 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item4.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v7) || (a_00_触发子类技能.检测BUFF层数 > 0 && v7.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item4.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if (item4.Value.技能目标 is 怪物实例 && (item4.Value.技能反馈 & 技能命中反馈.死亡) != 0)
										{
											new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, item4.Value.技能目标.当前坐标, this);
										}
									}
									break;
								case 技能触发方式.怪物死亡换位触发:
									foreach (KeyValuePair<int, 命中详情> item5 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item5.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v6) || (a_00_触发子类技能.检测BUFF层数 > 0 && v6.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item5.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if (item5.Value.技能目标 is 怪物实例 && (item5.Value.技能反馈 & 技能命中反馈.死亡) != 0)
										{
											new 技能实例(this.技能来源, value2, null, a_00_触发子类技能.增加动作编号 ? item5.Value.技能目标.动作编号++ : item5.Value.技能目标.动作编号, this.释放地图, item5.Value.技能目标.当前坐标, item5.Value.技能目标, item5.Value.技能目标.当前坐标, this, null, 目标借位: true);
										}
									}
									break;
								case 技能触发方式.怪物命中绝对触发:
									foreach (KeyValuePair<int, 命中详情> item6 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item6.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v8) || (a_00_触发子类技能.检测BUFF层数 > 0 && v8.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item6.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if (item6.Value.技能目标 is 怪物实例 && (item6.Value.技能反馈 & 技能命中反馈.丢失) == 0)
										{
											new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, (this.父类技能 == null) ? this.释放位置 : this.技能锚点, item6.Value.技能目标, item6.Value.技能目标.当前坐标, this);
										}
									}
									break;
								case 技能触发方式.无目标锚点位触发:
									if (this.命中列表.Count == 0 || this.命中列表.Values.FirstOrDefault((命中详情 O) => O.技能反馈 != 技能命中反馈.丢失) == null)
									{
										new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, this.技能锚点, this);
									}
									break;
								case 技能触发方式.目标位置绝对触发:
									foreach (KeyValuePair<int, 命中详情> item7 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item7.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v10) || (a_00_触发子类技能.检测BUFF层数 > 0 && v10.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item7.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, item7.Value.技能目标, item7.Value.技能目标.当前坐标, this);
									}
									break;
								case 技能触发方式.正手反手随机触发:
								{
									if (计算类.计算概率(0.5f) && 游戏技能.数据表.TryGetValue(a_00_触发子类技能.反手技能名字, out var value3))
									{
										new 技能实例(this.技能来源, value3, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, this.技能锚点, this);
									}
									else
									{
										new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, this.技能锚点, this);
									}
									break;
								}
								case 技能触发方式.目标死亡绝对触发:
									foreach (KeyValuePair<int, 命中详情> item8 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item8.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v5) || (a_00_触发子类技能.检测BUFF层数 > 0 && v5.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item8.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if ((item8.Value.技能反馈 & 技能命中反馈.死亡) != 0)
										{
											new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, item8.Value.技能目标.当前坐标, this);
										}
									}
									break;
								case 技能触发方式.目标闪避绝对触发:
									foreach (KeyValuePair<int, 命中详情> item9 in this.命中列表)
									{
										if (flag && a_00_触发子类技能.验证目标Buff)
										{
											if (!item9.Value.技能目标.Buff列表.TryGetValue(a_00_触发子类技能.目标Buff编号, out var v4) || (a_00_触发子类技能.检测BUFF层数 > 0 && v4.当前层数.V != a_00_触发子类技能.检测BUFF层数))
											{
												continue;
											}
											if (a_00_触发子类技能.触发成功移除)
											{
												item9.Value.技能目标.移除Buff时处理(a_00_触发子类技能.目标Buff编号);
											}
										}
										if ((item9.Value.技能反馈 & 技能命中反馈.闪避) != 0)
										{
											new 技能实例(this.技能来源, value2, this.技能数据, a_00_触发子类技能.增加动作编号 ? this.动作编号++ : this.动作编号, this.释放地图, this.释放位置, null, item9.Value.技能目标.当前坐标, this);
										}
									}
									break;
								}
								if (a_00_触发子类技能.触发成功结束)
								{
									if (!a_00_触发子类技能.不发结束通知)
									{
										this.技能来源.发送封包(new 技能释放完成
										{
											技能编号 = this.技能编号,
											动作编号 = this.动作编号
										});
									}
									this.技能来源.技能任务.Remove(this);
								}
							}
						}
					}
					else
					{
						A_01_触发对象Buff 触发Buff;
						触发Buff = value as A_01_触发对象Buff;
						if (触发Buff != null)
						{
							bool flag2;
							flag2 = false;
							if (触发Buff.角色自身添加)
							{
								bool flag3;
								flag3 = true;
								float num4;
								num4 = 触发Buff.Buff触发概率;
								if (真正来源 is 玩家实例 玩家实例4)
								{
									num4 += 玩家实例4.龙卫BUFF触发概率(触发Buff.触发Buff编号);
								}
								if (!计算类.计算概率(num4))
								{
									flag3 = false;
								}
								if (flag3 && 触发Buff.验证铭文技能 && 真正来源 is 玩家实例 玩家实例5)
								{
									int num5;
									num5 = 触发Buff.所需铭文编号 / 10;
									int num6;
									num6 = 触发Buff.所需铭文编号 % 10;
									flag3 = 玩家实例5.主体技能表.TryGetValue((ushort)num5, out var v11) && (触发Buff.同组铭文无效 ? (num6 == v11.铭文编号) : (num6 == 0 || num6 == v11.铭文编号));
								}
								if (flag3 && 触发Buff.验证自身Buff)
								{
									if (触发Buff.验证效果取反 ? 真正来源.Buff列表.ContainsKey(触发Buff.自身Buff编号) : (!真正来源.Buff列表.ContainsKey(触发Buff.自身Buff编号)))
									{
										flag3 = false;
									}
									else
									{
										if (触发Buff.触发成功移除)
										{
											真正来源.移除Buff时处理(触发Buff.自身Buff编号);
										}
										if (触发Buff.移除伴生Buff)
										{
											真正来源.移除Buff时处理(触发Buff.移除伴生编号);
										}
									}
								}
								if (flag3 && 触发Buff.验证分组Buff && 真正来源.Buff列表.Values.FirstOrDefault((Buff数据 O) => O.Buff分组 == 触发Buff.Buff分组编号) == null)
								{
									flag3 = false;
								}
								if (flag3 && 触发Buff.验证目标Buff && this.命中列表.Values.FirstOrDefault((命中详情 O) => (O.技能反馈 & 技能命中反馈.闪避) == 0 && (O.技能反馈 & 技能命中反馈.丢失) == 0 && O.技能目标.Buff列表.TryGetValue(触发Buff.目标Buff编号, out var v30) && v30.当前层数.V >= 触发Buff.所需Buff层数) == null)
								{
									flag3 = false;
								}
								if (flag3 && 触发Buff.验证目标类型 && this.命中列表.Values.FirstOrDefault((命中详情 O) => (O.技能反馈 & 技能命中反馈.闪避) == 0 && (O.技能反馈 & 技能命中反馈.丢失) == 0 && O.技能目标.特定类型(真正来源, 触发Buff.所需目标类型)) == null)
								{
									flag3 = false;
								}
								if (flag3)
								{
									真正来源.添加Buff时处理(触发Buff.触发Buff编号, 真正来源);
									if (触发Buff.伴生Buff编号 > 0)
									{
										真正来源.添加Buff时处理(触发Buff.伴生Buff编号, 真正来源);
									}
									flag2 = true;
								}
							}
							else
							{
								bool flag4;
								flag4 = true;
								if (触发Buff.验证自身Buff)
								{
									if (!真正来源.Buff列表.ContainsKey(触发Buff.自身Buff编号))
									{
										flag4 = false;
									}
									else
									{
										if (触发Buff.触发成功移除)
										{
											真正来源.移除Buff时处理(触发Buff.自身Buff编号);
										}
										if (触发Buff.移除伴生Buff)
										{
											真正来源.移除Buff时处理(触发Buff.移除伴生编号);
										}
									}
								}
								if (flag4 && 触发Buff.验证分组Buff && 真正来源.Buff列表.Values.FirstOrDefault((Buff数据 O) => O.Buff分组 == 触发Buff.Buff分组编号) == null)
								{
									flag4 = false;
								}
								if (flag4 && 触发Buff.验证铭文技能 && 真正来源 is 玩家实例 玩家实例6)
								{
									int num7;
									num7 = 触发Buff.所需铭文编号 / 10;
									int num8;
									num8 = 触发Buff.所需铭文编号 % 10;
									flag4 = 玩家实例6.主体技能表.TryGetValue((ushort)num7, out var v12) && (触发Buff.同组铭文无效 ? (num8 == v12.铭文编号) : (num8 == 0 || num8 == v12.铭文编号));
								}
								if (flag4)
								{
									foreach (KeyValuePair<int, 命中详情> item10 in this.命中列表)
									{
										bool flag5;
										flag5 = true;
										if ((item10.Value.技能反馈 & (技能命中反馈.闪避 | 技能命中反馈.丢失)) != 0)
										{
											flag5 = false;
										}
										if (flag5 && !计算类.计算概率(触发Buff.Buff触发概率))
										{
											flag5 = false;
										}
										if (flag5 && 触发Buff.验证目标类型 && !item10.Value.技能目标.特定类型(真正来源, 触发Buff.所需目标类型))
										{
											flag5 = false;
										}
										if (flag5 && 触发Buff.验证目标Buff)
										{
											flag5 = item10.Value.技能目标.Buff列表.TryGetValue(触发Buff.目标Buff编号, out var v13) && v13.当前层数.V >= 触发Buff.所需Buff层数;
										}
										if (flag5)
										{
											item10.Value.技能目标.增加Buff层数(触发Buff.增减目标BUFF, 触发Buff.增减BUFF层数);
											item10.Value.技能目标.添加Buff时处理(触发Buff.触发Buff编号, 真正来源);
											if (触发Buff.伴生Buff编号 > 0)
											{
												item10.Value.技能目标.添加Buff时处理(触发Buff.伴生Buff编号, 真正来源);
											}
											if (触发Buff.移除伴生Buff && 触发Buff.移除伴生编号 > 0)
											{
												item10.Value.技能目标.移除Buff时处理(触发Buff.移除伴生编号);
											}
											flag2 = true;
										}
									}
								}
							}
							if (flag2 && 触发Buff.增加技能经验 && 真正来源 is 玩家实例 玩家实例7)
							{
								玩家实例7.技能增加经验(触发Buff.经验技能编号);
							}
						}
						else if (value is A_02_触发陷阱技能 a_02_触发陷阱技能)
						{
							bool flag6;
							flag6 = true;
							if (a_02_触发陷阱技能.计算触发概率)
							{
								flag6 = 计算类.计算概率(a_02_触发陷阱技能.陷阱触发概率);
							}
							if (flag6 && 技能陷阱.数据表.TryGetValue(a_02_触发陷阱技能.触发陷阱技能, out var 陷阱模板))
							{
								int num9;
								num9 = 0;
								Point[] array;
								if (a_02_触发陷阱技能.启用个性范围)
								{
									游戏方向 key;
									key = 计算类.计算方向(this.释放位置, this.技能锚点);
									List<Point> list;
									list = a_02_触发陷阱技能.个性技能范围[游戏方向.左方];
									if (a_02_触发陷阱技能.计算对象方向)
									{
										list = a_02_触发陷阱技能.个性技能范围[key];
									}
									List<Point> list2;
									list2 = new List<Point>();
									foreach (Point item11 in list)
									{
										list2.Add(new Point(item11.X + this.技能锚点.X, item11.Y + this.技能锚点.Y));
									}
									if (a_02_触发陷阱技能.计算锚点自身)
									{
										list2.Add(this.技能锚点);
									}
									array = list2.ToArray();
								}
								else
								{
									array = 计算类.技能范围(this.技能锚点, 计算类.计算方向(this.释放位置, this.技能锚点), a_02_触发陷阱技能.触发陷阱数量);
								}
								地图对象 目标;
								目标 = null;
								if (a_02_触发陷阱技能.追踪命中目标)
								{
									foreach (KeyValuePair<int, 命中详情> item12 in this.命中列表)
									{
										目标 = item12.Value.技能目标;
									}
								}
								Point[] array2;
								array2 = array;
								foreach (Point 坐标 in array2)
								{
									if ((!this.释放地图.地形阻塞(坐标) || 陷阱模板.陷阱无视地形) && (陷阱模板.陷阱允许叠加 || this.释放地图[坐标].FirstOrDefault((地图对象 O) => O is 陷阱实例 { 陷阱分组编号: not 0 } 陷阱实例3 && 陷阱实例3.陷阱分组编号 == 陷阱模板.分组编号) == null))
									{
										this.技能来源.陷阱列表.Add(new 陷阱实例(this.技能来源, 陷阱模板, this.释放地图, 坐标, 目标, a_02_触发陷阱技能.出生限定方向 ? a_02_触发陷阱技能.陷阱出生方向 : this.技能来源.当前方向));
										num9++;
									}
								}
								if (num9 != 0 && a_02_触发陷阱技能.经验技能编号 != 0 && this.技能来源 is 玩家实例 玩家实例8)
								{
									玩家实例8.技能增加经验(a_02_触发陷阱技能.经验技能编号);
								}
							}
						}
						else if (value is B_00_技能切换通知 b_00_技能切换通知)
						{
							if (this.技能来源.Buff列表.ContainsKey(b_00_技能切换通知.技能标记编号))
							{
								if (b_00_技能切换通知.允许移除标记)
								{
									this.技能来源.移除Buff时处理(b_00_技能切换通知.技能标记编号);
								}
							}
							else if (游戏Buff.数据表.ContainsKey(b_00_技能切换通知.技能标记编号))
							{
								this.技能来源.添加Buff时处理(b_00_技能切换通知.技能标记编号, this.技能来源);
							}
						}
						else if (value is B_01_技能释放通知 b_01_技能释放通知)
						{
							if (b_01_技能释放通知.调整角色朝向)
							{
								游戏方向 游戏方向;
								游戏方向 = 计算类.计算方向(this.释放位置, this.技能锚点);
								if (游戏方向 == this.技能来源.当前方向)
								{
									this.技能来源.发送封包(new 对象转动方向
									{
										对象编号 = this.技能来源.地图编号,
										对象朝向 = (ushort)游戏方向,
										转向耗时 = ((!(this.技能来源 is 玩家实例)) ? ((ushort)1) : ((ushort)0))
									});
								}
								else
								{
									this.技能来源.当前方向 = 计算类.计算方向(this.释放位置, this.技能锚点);
								}
							}
							if (b_01_技能释放通知.移除技能标记 && this.技能模板.技能标记编号 != 0)
							{
								真正来源.移除Buff时处理(this.技能模板.技能标记编号);
							}
							if (b_01_技能释放通知.Buff增加层数 && b_01_技能释放通知.增加层数Buff != 0 && b_01_技能释放通知.增加Buff层数 != 0)
							{
								真正来源.增加Buff层数(b_01_技能释放通知.增加层数Buff, b_01_技能释放通知.增加Buff层数);
							}
							if (b_01_技能释放通知.自身冷却时间 != 0 || b_01_技能释放通知.Buff增加冷却)
							{
								if (this.检查计数 && 真正来源 is 玩家实例 玩家实例9)
								{
									if (--this.技能数据.剩余次数.V <= 0)
									{
										真正来源.冷却记录[this.技能编号 | 0x1000000] = this.释放时间.AddMilliseconds((this.技能数据.计数时间 - 主程.当前时间).TotalMilliseconds);
									}
									玩家实例9.网络连接?.发送封包(new 同步技能计数
									{
										技能编号 = this.技能数据.技能编号.V,
										技能计数 = this.技能数据.剩余次数.V,
										技能冷却 = (int)(this.技能数据.计数时间 - 主程.当前时间).TotalMilliseconds
									});
								}
								else if (b_01_技能释放通知.自身冷却时间 > 0 || b_01_技能释放通知.Buff增加冷却)
								{
									int num10;
									num10 = b_01_技能释放通知.自身冷却时间;
									if (b_01_技能释放通知.Buff增加冷却 && 真正来源.Buff列表.ContainsKey(b_01_技能释放通知.增加冷却Buff))
									{
										num10 += b_01_技能释放通知.冷却增加时间;
									}
									DateTime dateTime;
									dateTime = this.释放时间.AddMilliseconds(num10);
									DateTime dateTime2;
									dateTime2 = (真正来源.冷却记录.ContainsKey(this.技能编号 | 0x1000000) ? 真正来源.冷却记录[this.技能编号 | 0x1000000] : default(DateTime));
									if (num10 > 0 && dateTime > dateTime2)
									{
										真正来源.冷却记录[this.技能编号 | 0x1000000] = dateTime;
										真正来源.发送封包(new 添加技能冷却
										{
											冷却编号 = (this.技能编号 | 0x1000000),
											冷却时间 = num10
										});
									}
								}
							}
							if (真正来源 is 玩家实例 玩家实例10 && b_01_技能释放通知.分组冷却时间 != 0 && this.分组编号 != 0)
							{
								DateTime dateTime3;
								dateTime3 = this.释放时间.AddMilliseconds(b_01_技能释放通知.分组冷却时间);
								if (dateTime3 > (玩家实例10.冷却记录.ContainsKey(this.分组编号 | 0) ? 玩家实例10.冷却记录[this.分组编号 | 0] : default(DateTime)))
								{
									玩家实例10.冷却记录[this.分组编号 | 0] = dateTime3;
								}
								真正来源.发送封包(new 添加技能冷却
								{
									冷却编号 = (this.分组编号 | 0),
									冷却时间 = b_01_技能释放通知.分组冷却时间
								});
							}
							if (b_01_技能释放通知.角色忙绿时间 != 0)
							{
								真正来源.忙碌时间 = this.释放时间.AddMilliseconds(b_01_技能释放通知.角色忙绿时间);
							}
							if (b_01_技能释放通知.发送释放通知)
							{
								this.技能来源.发送封包(new 开始释放技能
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									锚点坐标 = this.技能锚点,
									动作编号 = this.动作编号,
									目标编号 = (this.技能目标?.地图编号 ?? 0),
									锚点高度 = this.释放地图.地形高度(this.技能锚点)
								});
							}
							if (b_01_技能释放通知.发送地图公告 != null && b_01_技能释放通知.发送地图公告 != string.Empty)
							{
								this.技能来源.当前地图.地图公告(b_01_技能释放通知.发送地图公告);
							}
							if (b_01_技能释放通知.发送全服公告 != null && b_01_技能释放通知.发送全服公告 != string.Empty)
							{
								网络服务网关.发送公告(b_01_技能释放通知.发送地图公告);
							}
						}
						else if (value is B_02_技能命中通知 b_02_技能命中通知)
						{
							if (b_02_技能命中通知.命中扩展通知)
							{
								this.技能来源.发送封包(new 触发技能扩展
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号,
									命中描述 = 命中详情.命中描述(this.命中列表, this.飞行耗时)
								});
							}
							else
							{
								this.技能来源.发送封包(new 触发技能正常
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号,
									命中描述 = 命中详情.命中描述(this.命中列表, this.飞行耗时)
								});
							}
							if (b_02_技能命中通知.计算飞行耗时)
							{
								this.飞行耗时 = 计算类.网格距离(this.释放位置, this.技能锚点) * b_02_技能命中通知.单格飞行耗时;
							}
						}
						else if (value is B_03_前摇结束通知 b_03_前摇结束通知)
						{
							if (b_03_前摇结束通知.计算攻速缩减)
							{
								this.攻速缩减 = 计算类.数值限制(计算类.计算攻速(-10), this.攻速缩减 + 计算类.计算攻速(this.技能来源[游戏对象属性.攻击速度]), 计算类.计算攻速(10));
								if (this.攻速缩减 != 0)
								{
									foreach (KeyValuePair<int, 技能任务> item13 in this.节点列表)
									{
										if (item13.Value is B_04_后摇结束通知)
										{
											int j;
											for (j = item13.Key - this.攻速缩减; this.节点列表.ContainsKey(j); j++)
											{
											}
											this.节点列表.Remove(item13.Key);
											this.节点列表.Add(j, item13.Value);
											break;
										}
									}
								}
							}
							if (b_03_前摇结束通知.禁止行走时间 != 0)
							{
								this.技能来源.行走时间 = this.释放时间.AddMilliseconds(b_03_前摇结束通知.禁止行走时间);
							}
							if (b_03_前摇结束通知.禁止奔跑时间 != 0)
							{
								this.技能来源.奔跑时间 = this.释放时间.AddMilliseconds(b_03_前摇结束通知.禁止奔跑时间);
							}
							if (b_03_前摇结束通知.角色硬直时间 != 0)
							{
								this.技能来源.硬直时间 = this.释放时间.AddMilliseconds(b_03_前摇结束通知.计算攻速缩减 ? (b_03_前摇结束通知.角色硬直时间 - this.攻速缩减) : b_03_前摇结束通知.角色硬直时间);
							}
							if (b_03_前摇结束通知.发送结束通知)
							{
								this.技能来源.发送封包(new 触发技能正常
								{
									发送特殊标记 = b_03_前摇结束通知.发送特殊标记,
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号
								});
							}
							if (b_03_前摇结束通知.解除技能陷阱 && this.技能来源 is 陷阱实例 陷阱实例2)
							{
								陷阱实例2.陷阱消失处理();
							}
						}
						else if (value is B_04_后摇结束通知 b_04_后摇结束通知)
						{
							this.技能来源.发送封包(new 技能释放完成
							{
								技能编号 = this.技能编号,
								动作编号 = this.动作编号
							});
							if (b_04_后摇结束通知.后摇结束死亡)
							{
								this.技能来源.自身死亡处理(null, 技能击杀: false);
							}
						}
						else if (value is C_00_计算技能锚点 c_00_计算技能锚点)
						{
							if (c_00_计算技能锚点.目标前方位置 && this.技能目标 != null)
							{
								this.技能锚点 = 计算类.前方坐标(this.技能目标.当前坐标, this.技能目标.当前方向, c_00_计算技能锚点.技能最近距离);
							}
							else if (c_00_计算技能锚点.计算当前位置)
							{
								this.技能目标 = null;
								if (c_00_计算技能锚点.计算当前方向)
								{
									this.技能锚点 = 计算类.前方坐标(this.技能来源.当前坐标, this.技能来源.当前方向, c_00_计算技能锚点.技能最近距离);
								}
								else
								{
									this.技能锚点 = 计算类.前方坐标(this.技能来源.当前坐标, this.技能锚点, c_00_计算技能锚点.技能最近距离);
								}
							}
							else if (c_00_计算技能锚点.计算BUFF目标 && c_00_计算技能锚点.目标BUFF编号 > 0)
							{
								bool flag7;
								flag7 = false;
								Point[] array2;
								array2 = 计算类.技能范围(this.技能来源.当前坐标, this.技能来源.当前方向, c_00_计算技能锚点.搜索目标范围);
								foreach (Point 坐标2 in array2)
								{
									foreach (地图对象 item14 in this.释放地图[坐标2])
									{
										if (item14.Buff列表.TryGetValue(c_00_计算技能锚点.目标BUFF编号, out var v14))
										{
											if (c_00_计算技能锚点.验证BUFF来源 && v14.Buff来源 == this.技能来源)
											{
												flag7 = true;
												this.技能锚点 = item14.当前坐标;
											}
											else
											{
												flag7 = true;
												this.技能锚点 = item14.当前坐标;
											}
											break;
										}
									}
								}
								if (!flag7)
								{
									this.技能来源.发送封包(new 技能释放完成
									{
										技能编号 = this.技能编号,
										动作编号 = this.动作编号
									});
									this.技能来源.技能任务.Remove(this);
									return;
								}
							}
							else if (计算类.网格距离(this.释放位置, this.技能锚点) > c_00_计算技能锚点.技能最远距离)
							{
								this.技能目标 = null;
								this.技能锚点 = 计算类.前方坐标(this.释放位置, this.技能锚点, c_00_计算技能锚点.技能最远距离);
							}
							else if (计算类.网格距离(this.释放位置, this.技能锚点) < c_00_计算技能锚点.技能最近距离)
							{
								this.技能目标 = null;
								if (this.释放位置 == this.技能锚点)
								{
									this.技能锚点 = 计算类.前方坐标(this.释放位置, this.技能来源.当前方向, c_00_计算技能锚点.技能最近距离);
								}
								else
								{
									this.技能锚点 = 计算类.前方坐标(this.释放位置, this.技能锚点, c_00_计算技能锚点.技能最近距离);
								}
							}
						}
						else if (value is C_01_计算命中目标 c_01_计算命中目标)
						{
							if (c_01_计算命中目标.清空命中列表)
							{
								this.命中列表 = new Dictionary<int, 命中详情>();
							}
							if (c_01_计算命中目标.技能能否穿墙 || !this.释放地图.地形遮挡(this.释放位置, this.技能锚点))
							{
								switch (c_01_计算命中目标.技能锁定方式)
								{
								case 技能锁定类型.锁定自身:
									this.技能来源.被技能命中处理(this, c_01_计算命中目标);
									break;
								case 技能锁定类型.锁定目标:
									this.技能目标?.被技能命中处理(this, c_01_计算命中目标);
									break;
								case 技能锁定类型.锁定自身坐标:
								{
									Point[] array4;
									if (c_01_计算命中目标.启用个性范围)
									{
										游戏方向 key3;
										key3 = 计算类.计算方向(this.释放位置, this.技能锚点);
										List<Point> list5;
										list5 = c_01_计算命中目标.个性技能范围[游戏方向.左方];
										if (c_01_计算命中目标.计算对象方向)
										{
											list5 = c_01_计算命中目标.个性技能范围[key3];
										}
										List<Point> list6;
										list6 = new List<Point>();
										foreach (Point item15 in list5)
										{
											list6.Add(new Point(item15.X + this.技能来源.当前坐标.X, item15.Y + this.技能来源.当前坐标.Y));
										}
										if (c_01_计算命中目标.计算锚点自身)
										{
											list6.Add(this.技能来源.当前坐标);
										}
										array4 = list6.ToArray();
									}
									else
									{
										array4 = 计算类.技能范围(this.技能来源.当前坐标, 计算类.计算方向(this.释放位置, this.技能锚点), c_01_计算命中目标.技能范围类型);
									}
									Point[] array2;
									array2 = array4;
									foreach (Point 坐标4 in array2)
									{
										foreach (地图对象 item16 in this.释放地图[坐标4])
										{
											item16.被技能命中处理(this, c_01_计算命中目标);
										}
									}
									break;
								}
								case 技能锁定类型.锁定目标坐标:
								{
									Point[] array3;
									if (c_01_计算命中目标.启用个性范围)
									{
										游戏方向 key2;
										key2 = 计算类.计算方向(this.释放位置, this.技能锚点);
										List<Point> list3;
										list3 = c_01_计算命中目标.个性技能范围[游戏方向.左方];
										if (c_01_计算命中目标.计算对象方向)
										{
											list3 = c_01_计算命中目标.个性技能范围[key2];
										}
										List<Point> list4;
										list4 = new List<Point>();
										foreach (Point item17 in list3)
										{
											list4.Add(new Point(item17.X + (this.技能目标?.当前坐标 ?? this.技能锚点).X, item17.Y + (this.技能目标?.当前坐标 ?? this.技能锚点).Y));
										}
										if (c_01_计算命中目标.计算锚点自身)
										{
											list4.Add(this.技能目标?.当前坐标 ?? this.技能锚点);
										}
										array3 = list4.ToArray();
									}
									else
									{
										array3 = 计算类.技能范围(this.技能目标?.当前坐标 ?? this.技能锚点, 计算类.计算方向(this.释放位置, this.技能锚点), c_01_计算命中目标.技能范围类型);
									}
									Point[] array2;
									array2 = array3;
									foreach (Point 坐标3 in array2)
									{
										foreach (地图对象 item18 in this.释放地图[坐标3])
										{
											item18.被技能命中处理(this, c_01_计算命中目标);
										}
									}
									break;
								}
								case 技能锁定类型.锁定锚点坐标:
								{
									Point[] array6;
									if (c_01_计算命中目标.启用个性范围)
									{
										游戏方向 key5;
										key5 = 计算类.计算方向(this.释放位置, this.技能锚点);
										List<Point> list9;
										list9 = c_01_计算命中目标.个性技能范围[游戏方向.左方];
										if (c_01_计算命中目标.计算对象方向)
										{
											list9 = c_01_计算命中目标.个性技能范围[key5];
										}
										List<Point> list10;
										list10 = new List<Point>();
										foreach (Point item19 in list9)
										{
											list10.Add(new Point(item19.X + this.技能锚点.X, item19.Y + this.技能锚点.Y));
										}
										if (c_01_计算命中目标.计算锚点自身)
										{
											list10.Add(this.技能锚点);
										}
										array6 = list10.ToArray();
									}
									else
									{
										array6 = 计算类.技能范围(this.技能锚点, 计算类.计算方向(this.释放位置, this.技能锚点), c_01_计算命中目标.技能范围类型);
									}
									Point[] array2;
									array2 = array6;
									foreach (Point 坐标6 in array2)
									{
										foreach (地图对象 item20 in this.释放地图[坐标6])
										{
											item20.被技能命中处理(this, c_01_计算命中目标);
										}
									}
									break;
								}
								case 技能锁定类型.放空锁定自身:
								{
									Point[] array5;
									if (c_01_计算命中目标.启用个性范围)
									{
										游戏方向 key4;
										key4 = 计算类.计算方向(this.释放位置, this.技能锚点);
										List<Point> list7;
										list7 = c_01_计算命中目标.个性技能范围[游戏方向.左方];
										if (c_01_计算命中目标.计算对象方向)
										{
											list7 = c_01_计算命中目标.个性技能范围[key4];
										}
										List<Point> list8;
										list8 = new List<Point>();
										foreach (Point item21 in list7)
										{
											list8.Add(new Point(item21.X + this.技能锚点.X, item21.Y + this.技能锚点.Y));
										}
										if (c_01_计算命中目标.计算锚点自身)
										{
											list8.Add(this.技能锚点);
										}
										array5 = list8.ToArray();
									}
									else
									{
										array5 = 计算类.技能范围(this.技能锚点, 计算类.计算方向(this.释放位置, this.技能锚点), c_01_计算命中目标.技能范围类型);
									}
									Point[] array2;
									array2 = array5;
									foreach (Point 坐标5 in array2)
									{
										foreach (地图对象 item22 in this.释放地图[坐标5])
										{
											item22.被技能命中处理(this, c_01_计算命中目标);
										}
									}
									if (this.命中列表.Count == 0)
									{
										this.技能来源.被技能命中处理(this, c_01_计算命中目标);
									}
									break;
								}
								case 技能锁定类型.宠物锁定主人:
									if (this.技能来源 is 宠物实例 宠物实例2)
									{
										宠物实例2.宠物主人.被技能命中处理(this, c_01_计算命中目标);
									}
									break;
								case 技能锁定类型.锁定所有宠物:
									if (!(this.技能来源 is 玩家实例 玩家实例11))
									{
										break;
									}
									foreach (宠物实例 item23 in 玩家实例11.宠物列表)
									{
										if (c_01_计算命中目标.宠物模板编号 <= 0 || item23.对象模板.怪物编号 == c_01_计算命中目标.宠物模板编号)
										{
											item23.被技能命中处理(this, c_01_计算命中目标);
										}
									}
									break;
								}
							}
							if (this.命中列表.Count == 0 && c_01_计算命中目标.放空结束技能)
							{
								if (c_01_计算命中目标.发送中断通知)
								{
									this.技能来源.发送封包(new 技能释放中断
									{
										对象编号 = this.技能来源.地图编号,
										技能编号 = this.技能编号,
										技能等级 = this.技能等级,
										技能铭文 = this.铭文编号,
										动作编号 = this.动作编号,
										技能分段 = this.分段编号
									});
								}
								this.技能来源.技能任务.Remove(this);
								return;
							}
							if (c_01_计算命中目标.补发释放通知)
							{
								this.技能来源.发送封包(new 开始释放技能
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									目标编号 = (this.技能目标?.地图编号 ?? 0),
									锚点坐标 = this.技能锚点,
									锚点高度 = this.释放地图.地形高度(this.技能锚点),
									动作编号 = this.动作编号
								});
							}
							if (this.命中列表.Count != 0 && c_01_计算命中目标.攻速提升类型 != 0)
							{
								this.攻速缩减 = 计算类.数值限制(计算类.计算攻速(-5), this.攻速缩减 + 计算类.计算攻速(c_01_计算命中目标.攻速提升幅度), 计算类.计算攻速(5));
							}
							if (c_01_计算命中目标.清除目标状态 && c_01_计算命中目标.清除状态列表.Count != 0 && (c_01_计算命中目标.清除状态几率 >= 1f || c_01_计算命中目标.清除状态几率 <= 0f || 计算类.计算概率(c_01_计算命中目标.清除状态几率)))
							{
								foreach (KeyValuePair<int, 命中详情> item24 in this.命中列表)
								{
									if ((item24.Value.技能反馈 & 技能命中反馈.闪避) != 0 || (item24.Value.技能反馈 & 技能命中反馈.丢失) != 0)
									{
										continue;
									}
									foreach (ushort item25 in c_01_计算命中目标.清除状态列表.ToList())
									{
										item24.Value.技能目标.移除Buff时处理(item25);
									}
								}
							}
							if (c_01_计算命中目标.触发被动技能 && this.命中列表.Count != 0 && 计算类.计算概率(c_01_计算命中目标.触发被动概率))
							{
								this.技能来源[游戏对象属性.技能标志] = 1;
							}
							if (c_01_计算命中目标.增加技能经验 && this.命中列表.Count != 0)
							{
								(this.技能来源 as 玩家实例).技能增加经验(c_01_计算命中目标.经验技能编号);
							}
							if (c_01_计算命中目标.计算飞行耗时 && c_01_计算命中目标.单格飞行耗时 != 0)
							{
								this.飞行耗时 = 计算类.网格距离(this.释放位置, this.技能锚点) * c_01_计算命中目标.单格飞行耗时;
							}
							if (c_01_计算命中目标.技能命中通知)
							{
								this.技能来源.发送封包(new 触发技能正常
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号,
									命中描述 = 命中详情.命中描述(this.命中列表, this.飞行耗时)
								});
							}
							if (c_01_计算命中目标.技能扩展通知)
							{
								this.技能来源.发送封包(new 触发技能扩展
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号,
									命中描述 = 命中详情.命中描述(this.命中列表, this.飞行耗时)
								});
							}
							if (this.技能来源 is 玩家实例 玩家实例12 && this.命中列表.Count != 0)
							{
								玩家实例12.龙卫命中减少冷却(this.技能编号, this.铭文编号, this.命中列表);
								玩家实例12.龙卫命中概率减少(this.技能编号, this.铭文编号, this.命中列表);
							}
						}
						else if (value is C_02_计算目标伤害 c_02_计算目标伤害)
						{
							float num11;
							num11 = 1f;
							int num12;
							num12 = 0;
							foreach (KeyValuePair<int, 命中详情> item26 in this.命中列表)
							{
								float num13;
								num13 = 0f;
								if (c_02_计算目标伤害.点爆命中目标 && c_02_计算目标伤害.点爆标记编号 != null && c_02_计算目标伤害.点爆标记编号.Length != 0)
								{
									for (int k = 0; k < c_02_计算目标伤害.点爆标记编号.Length; k++)
									{
										if (item26.Value.技能目标.Buff列表.ContainsKey(c_02_计算目标伤害.点爆标记编号[k]))
										{
											item26.Value.技能目标.移除Buff时处理(c_02_计算目标伤害.点爆标记编号[k]);
											num13 += c_02_计算目标伤害.点爆标记增伤;
										}
									}
								}
								else if (c_02_计算目标伤害.点爆命中目标 && c_02_计算目标伤害.失败添加层数 && c_02_计算目标伤害.点爆标记编号 != null && c_02_计算目标伤害.点爆标记编号.Length != 0)
								{
									for (int l = 0; l < c_02_计算目标伤害.点爆标记编号.Length; l++)
									{
										item26.Value.技能目标.添加Buff时处理(c_02_计算目标伤害.点爆标记编号[l], this.技能来源);
									}
									continue;
								}
								item26.Value.技能目标.被动受伤时处理(this, c_02_计算目标伤害, item26.Value, num11 + num13, this.命中列表.Count);
								num12 += item26.Value.技能伤害;
								if ((item26.Value.技能反馈 & 技能命中反馈.丢失) == 0)
								{
									if (c_02_计算目标伤害.数量衰减伤害)
									{
										num11 = Math.Max(c_02_计算目标伤害.伤害衰减下限, num11 - c_02_计算目标伤害.伤害衰减系数);
									}
									byte b;
									b = 0;
									if ((item26.Value.技能反馈 & 技能命中反馈.暴击) != 0)
									{
										b = Settings.暴击特效ID;
									}
									this.技能来源.发送封包(new 触发命中特效
									{
										对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
										技能编号 = this.技能编号,
										技能等级 = this.技能等级,
										技能铭文 = this.铭文编号,
										动作编号 = this.动作编号,
										目标编号 = item26.Value.技能目标.地图编号,
										技能反馈 = (ushort)item26.Value.技能反馈,
										技能伤害 = -item26.Value.技能伤害,
										招架伤害 = item26.Value.招架伤害,
										附加特效 = ((b != 0) ? b : c_02_计算目标伤害.附加特效编号)
									});
								}
							}
							if (num12 > 0 && this.技能来源 is 玩家实例 玩家实例13)
							{
								int num14;
								num14 = 0;
								switch (c_02_计算目标伤害.技能伤害类型)
								{
								case 技能伤害类型.攻击:
								case 技能伤害类型.刺术:
								{
									if (玩家实例13.角色装备.TryGetValue(8, out var v20) && v20.物品编号 == 99920031)
									{
										num14 += 7;
									}
									if (玩家实例13.角色装备.TryGetValue(9, out var v21) && v21.物品编号 == 99930037)
									{
										num14 += 7;
									}
									if (玩家实例13.角色装备.TryGetValue(10, out var v22) && v22.物品编号 == 99930037)
									{
										num14 += 7;
									}
									if (玩家实例13.角色装备.TryGetValue(11, out var v23) && v23.物品编号 == 99940029)
									{
										num14 += 7;
									}
									if (玩家实例13.角色装备.TryGetValue(12, out var v24) && v24.物品编号 == 99940029)
									{
										num14 += 7;
									}
									break;
								}
								case 技能伤害类型.魔法:
								case 技能伤害类型.道术:
								case 技能伤害类型.弓术:
								{
									if (玩家实例13.角色装备.TryGetValue(8, out var v15) && v15.物品编号 == 99920070)
									{
										num14 += 5;
									}
									if (玩家实例13.角色装备.TryGetValue(9, out var v16) && v16.物品编号 == 99930070)
									{
										num14 += 5;
									}
									if (玩家实例13.角色装备.TryGetValue(10, out var v17) && v17.物品编号 == 99930070)
									{
										num14 += 5;
									}
									if (玩家实例13.角色装备.TryGetValue(11, out var v18) && v18.物品编号 == 99940070)
									{
										num14 += 5;
									}
									if (玩家实例13.角色装备.TryGetValue(12, out var v19) && v19.物品编号 == 99940070)
									{
										num14 += 5;
									}
									break;
								}
								}
								if (num14 > 0)
								{
									this.技能来源.当前体力 += num14;
									this.技能来源.发送封包(new 体力变动飘字
									{
										血量变化 = num14,
										对象编号 = this.技能来源.地图编号
									});
								}
							}
							if (c_02_计算目标伤害.目标死亡回复)
							{
								foreach (KeyValuePair<int, 命中详情> item27 in this.命中列表)
								{
									if ((item27.Value.技能反馈 & 技能命中反馈.死亡) != 0 && item27.Value.技能目标.特定类型(this.技能来源, c_02_计算目标伤害.回复限定类型))
									{
										int num15;
										num15 = c_02_计算目标伤害.体力回复基数;
										if (c_02_计算目标伤害.等级差减回复)
										{
											int 数值;
											数值 = this.技能来源.当前等级 - item27.Value.技能目标.当前等级 - c_02_计算目标伤害.减回复等级差;
											int num16;
											num16 = c_02_计算目标伤害.零回复等级差 - c_02_计算目标伤害.减回复等级差;
											num15 = (int)((float)num15 - (float)num15 * ((float)计算类.数值限制(0, 数值, num16) / (float)num16));
										}
										if (num15 > 0)
										{
											this.技能来源.当前体力 += num15;
											this.技能来源.发送封包(new 体力变动飘字
											{
												血量变化 = num15,
												对象编号 = this.技能来源.地图编号
											});
										}
									}
								}
							}
							if (c_02_计算目标伤害.击杀减少冷却)
							{
								int num17;
								num17 = 0;
								foreach (KeyValuePair<int, 命中详情> item28 in this.命中列表)
								{
									if ((!c_02_计算目标伤害.击杀概率减少 || 计算类.计算概率(c_02_计算目标伤害.击杀减少概率)) && (item28.Value.技能反馈 & 技能命中反馈.死亡) != 0 && item28.Value.技能目标.特定类型(this.技能来源, c_02_计算目标伤害.冷却减少类型))
									{
										num17 += c_02_计算目标伤害.冷却减少时间;
									}
								}
								if (num17 > 0)
								{
									if (this.技能来源.冷却记录.TryGetValue(c_02_计算目标伤害.冷却减少技能 | 0x1000000, out var v25))
									{
										v25 -= TimeSpan.FromMilliseconds(num17);
										this.技能来源.冷却记录[c_02_计算目标伤害.冷却减少技能 | 0x1000000] = v25;
										this.技能来源.发送封包(new 添加技能冷却
										{
											冷却编号 = (c_02_计算目标伤害.冷却减少技能 | 0x1000000),
											冷却时间 = Math.Max(0, (int)(v25 - 主程.当前时间).TotalMilliseconds)
										});
									}
									if (c_02_计算目标伤害.冷却减少分组 != 0 && this.技能来源 is 玩家实例 玩家实例14 && 玩家实例14.冷却记录.TryGetValue(c_02_计算目标伤害.冷却减少分组 | 0, out var v26))
									{
										v26 -= TimeSpan.FromMilliseconds(num17);
										玩家实例14.冷却记录[c_02_计算目标伤害.冷却减少分组 | 0] = v26;
										this.技能来源.发送封包(new 添加技能冷却
										{
											冷却编号 = (c_02_计算目标伤害.冷却减少分组 | 0),
											冷却时间 = Math.Max(0, (int)(v26 - 主程.当前时间).TotalMilliseconds)
										});
									}
								}
							}
							if (c_02_计算目标伤害.命中减少冷却)
							{
								int num18;
								num18 = 0;
								foreach (KeyValuePair<int, 命中详情> item29 in this.命中列表)
								{
									if ((!c_02_计算目标伤害.命中概率减少 || 计算类.计算概率(c_02_计算目标伤害.命中减少概率)) && (item29.Value.技能反馈 & 技能命中反馈.闪避) == 0 && (item29.Value.技能反馈 & 技能命中反馈.丢失) == 0 && item29.Value.技能目标.特定类型(this.技能来源, c_02_计算目标伤害.冷却减少类型))
									{
										num18 += c_02_计算目标伤害.冷却减少时间;
									}
								}
								if (num18 > 0)
								{
									if (this.技能来源.冷却记录.TryGetValue(c_02_计算目标伤害.冷却减少技能 | 0x1000000, out var v27))
									{
										v27 -= TimeSpan.FromMilliseconds(num18);
										this.技能来源.冷却记录[c_02_计算目标伤害.冷却减少技能 | 0x1000000] = v27;
										this.技能来源.发送封包(new 添加技能冷却
										{
											冷却编号 = (c_02_计算目标伤害.冷却减少技能 | 0x1000000),
											冷却时间 = Math.Max(0, (int)(v27 - 主程.当前时间).TotalMilliseconds)
										});
									}
									if (c_02_计算目标伤害.冷却减少分组 != 0 && this.技能来源 is 玩家实例 玩家实例15 && 玩家实例15.冷却记录.TryGetValue(c_02_计算目标伤害.冷却减少分组 | 0, out var v28))
									{
										v28 -= TimeSpan.FromMilliseconds(num18);
										玩家实例15.冷却记录[c_02_计算目标伤害.冷却减少分组 | 0] = v28;
										this.技能来源.发送封包(new 添加技能冷却
										{
											冷却编号 = (c_02_计算目标伤害.冷却减少分组 | 0),
											冷却时间 = Math.Max(0, (int)(v28 - 主程.当前时间).TotalMilliseconds)
										});
									}
								}
							}
							if (c_02_计算目标伤害.目标硬直时间 > 0)
							{
								foreach (KeyValuePair<int, 命中详情> item30 in this.命中列表)
								{
									if ((item30.Value.技能反馈 & 技能命中反馈.闪避) == 0 && (item30.Value.技能反馈 & 技能命中反馈.丢失) == 0 && item30.Value.技能目标 is 怪物实例 { 怪物级别: not 怪物级别分类.头目首领 })
									{
										item30.Value.技能目标.硬直时间 = 主程.当前时间.AddMilliseconds(c_02_计算目标伤害.目标硬直时间);
									}
								}
							}
							if (c_02_计算目标伤害.清除目标状态 && c_02_计算目标伤害.清除状态列表.Count != 0 && (c_02_计算目标伤害.清除状态几率 >= 1f || c_02_计算目标伤害.清除状态几率 <= 0f || 计算类.计算概率(c_02_计算目标伤害.清除状态几率)))
							{
								foreach (KeyValuePair<int, 命中详情> item31 in this.命中列表)
								{
									if ((item31.Value.技能反馈 & 技能命中反馈.闪避) != 0 || (item31.Value.技能反馈 & 技能命中反馈.丢失) != 0)
									{
										continue;
									}
									foreach (ushort item32 in c_02_计算目标伤害.清除状态列表)
									{
										item31.Value.技能目标.移除Buff时处理(item32);
									}
								}
							}
							if (c_02_计算目标伤害.增加技能经验 && this.命中列表.Count != 0)
							{
								(this.技能来源 as 玩家实例).技能增加经验(c_02_计算目标伤害.经验技能编号);
							}
							if (c_02_计算目标伤害.扣除武器持久 && this.命中列表.Count != 0)
							{
								(this.技能来源 as 玩家实例).武器损失持久();
							}
						}
						else if (value is C_03_计算对象位移 { 自身位移次数: var 自身位移次数 } c_03_计算对象位移)
						{
							byte b2;
							b2 = (byte)((((自身位移次数 != null) ? 自身位移次数.Length : 0) > this.技能等级) ? c_03_计算对象位移.自身位移次数[this.技能等级] : 0);
							if (c_03_计算对象位移.角色自身位移 && (this.释放地图 != this.技能来源.当前地图 || this.分段编号 >= b2))
							{
								this.技能来源.发送封包(new 技能释放中断
								{
									对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
									技能编号 = this.技能编号,
									技能等级 = this.技能等级,
									技能铭文 = this.铭文编号,
									动作编号 = this.动作编号,
									技能分段 = this.分段编号
								});
								this.技能来源.发送封包(new 技能释放完成
								{
									技能编号 = this.技能编号,
									动作编号 = this.动作编号
								});
							}
							else if (c_03_计算对象位移.角色自身位移)
							{
								int 数量;
								数量 = (c_03_计算对象位移.推动目标位移 ? c_03_计算对象位移.连续推动数量 : 0);
								byte[] 自身位移距离;
								自身位移距离 = c_03_计算对象位移.自身位移距离;
								int num19;
								num19 = ((((自身位移距离 != null) ? 自身位移距离.Length : 0) > this.技能等级) ? c_03_计算对象位移.自身位移距离[this.技能等级] : 0);
								int num20;
								num20 = ((c_03_计算对象位移.允许超出锚点 || c_03_计算对象位移.锚点反向位移) ? num19 : Math.Min(num19, 计算类.网格距离(this.释放位置, this.技能锚点)));
								Point 锚点;
								锚点 = (c_03_计算对象位移.锚点反向位移 ? 计算类.前方坐标(this.技能来源.当前坐标, 计算类.计算方向(this.技能锚点, this.技能来源.当前坐标), num20) : this.技能锚点);
								if (this.技能来源.能否位移(this.技能来源, 锚点, num20, 数量, c_03_计算对象位移.能否穿越障碍, out var 终点, out var 目标2))
								{
									地图对象[] array7;
									array7 = 目标2;
									foreach (地图对象 地图对象 in array7)
									{
										if (c_03_计算对象位移.目标位移编号 != 0 && 计算类.计算概率(c_03_计算对象位移.位移Buff概率))
										{
											地图对象.添加Buff时处理(c_03_计算对象位移.目标位移编号, this.技能来源);
										}
										if (c_03_计算对象位移.目标附加编号 != 0 && 计算类.计算概率(c_03_计算对象位移.附加Buff概率) && 地图对象.特定类型(this.技能来源, c_03_计算对象位移.限定附加类型))
										{
											地图对象.添加Buff时处理(c_03_计算对象位移.目标附加编号, this.技能来源);
										}
										地图对象.当前方向 = 计算类.计算方向(地图对象.当前坐标, this.技能来源.当前坐标);
										Point point;
										point = 计算类.前方坐标(地图对象.当前坐标, 计算类.计算方向(this.技能来源.当前坐标, 地图对象.当前坐标), 1);
										地图对象.忙碌时间 = 主程.当前时间.AddMilliseconds(c_03_计算对象位移.目标位移耗时 * 60);
										地图对象.硬直时间 = 主程.当前时间.AddMilliseconds(c_03_计算对象位移.目标位移耗时 * 60 + c_03_计算对象位移.目标硬直时间);
										地图对象.发送封包(new 对象被动位移
										{
											位移坐标 = point,
											对象编号 = 地图对象.地图编号,
											位移朝向 = (ushort)地图对象.当前方向,
											位移速度 = c_03_计算对象位移.目标位移耗时,
											位移方式 = c_03_计算对象位移.角色位移方式
										});
										地图对象.自身移动时处理(point);
										if (c_03_计算对象位移.推动增加经验 && !this.经验增加)
										{
											(this.技能来源 as 玩家实例).技能增加经验(this.技能编号);
											this.经验增加 = true;
										}
									}
									if (c_03_计算对象位移.成功Buff编号 != 0 && 计算类.计算概率(c_03_计算对象位移.成功Buff概率))
									{
										this.技能来源.添加Buff时处理(c_03_计算对象位移.成功Buff编号, this.技能来源);
									}
									this.技能来源.当前方向 = 计算类.计算方向(this.技能来源.当前坐标, 终点);
									int num21;
									num21 = c_03_计算对象位移.自身位移耗时 * this.技能来源.网格距离(终点);
									this.技能来源.忙碌时间 = 主程.当前时间.AddMilliseconds(num21 * 60);
									this.技能来源.发送封包(new 对象被动位移
									{
										位移坐标 = 终点,
										对象编号 = this.技能来源.地图编号,
										位移朝向 = (ushort)this.技能来源.当前方向,
										位移速度 = (ushort)num21,
										位移方式 = c_03_计算对象位移.角色位移方式
									});
									if (c_03_计算对象位移.成功减少节点 > 0 && c_03_计算对象位移.每格减少时间 > 0 && this.节点列表.TryGetValue(c_03_计算对象位移.成功减少节点, out var value4))
									{
										this.节点列表.Remove(c_03_计算对象位移.成功减少节点);
										this.节点列表.Add(c_03_计算对象位移.成功减少节点 - c_03_计算对象位移.每格减少时间 * (num20 - 计算类.网格距离(this.技能来源.当前坐标, 终点)), value4);
									}
									this.技能来源.自身移动时处理(终点);
									if (this.技能来源 is 玩家实例 玩家实例16 && c_03_计算对象位移.位移增加经验 && !this.经验增加)
									{
										玩家实例16.技能增加经验(this.技能编号);
										this.经验增加 = true;
									}
									if (c_03_计算对象位移.多段位移通知)
									{
										this.技能来源.发送封包(new 触发技能正常
										{
											对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
											技能编号 = this.技能编号,
											技能等级 = this.技能等级,
											技能铭文 = this.铭文编号,
											动作编号 = this.动作编号,
											技能分段 = this.分段编号
										});
									}
									if (b2 > 1)
									{
										this.技能锚点 = 计算类.前方坐标(this.技能来源.当前坐标, this.技能来源.当前方向, num20);
									}
									this.分段编号++;
								}
								else
								{
									if (计算类.计算概率(c_03_计算对象位移.失败Buff概率))
									{
										this.技能来源.添加Buff时处理(c_03_计算对象位移.失败Buff编号, this.技能来源);
									}
									if (c_03_计算对象位移.失败触发节点 > 0 && this.节点列表.TryGetValue(c_03_计算对象位移.失败触发节点, out var value5))
									{
										this.节点列表.Remove(c_03_计算对象位移.失败触发节点);
										this.节点列表.Add(0, value5);
									}
									this.技能来源.硬直时间 = 主程.当前时间.AddMilliseconds((int)c_03_计算对象位移.自身硬直时间);
									this.分段编号 = b2;
								}
								if (b2 > 1)
								{
									int m;
									for (m = keyValuePair.Key + c_03_计算对象位移.自身位移耗时 * 60; this.节点列表.ContainsKey(m); m++)
									{
									}
									this.节点列表.Add(m, keyValuePair.Value);
								}
							}
							else if (c_03_计算对象位移.推动目标位移)
							{
								foreach (KeyValuePair<int, 命中详情> item33 in this.命中列表)
								{
									if ((item33.Value.技能反馈 & 技能命中反馈.闪避) != 0 || (item33.Value.技能反馈 & 技能命中反馈.丢失) != 0 || (item33.Value.技能反馈 & 技能命中反馈.死亡) != 0 || !计算类.计算概率(c_03_计算对象位移.推动目标概率) || !item33.Value.技能目标.特定类型(this.技能来源, c_03_计算对象位移.推动目标类型))
									{
										continue;
									}
									byte[] 目标位移距离;
									目标位移距离 = c_03_计算对象位移.目标位移距离;
									int num22;
									num22 = Math.Max(0, Math.Min(val2: (((目标位移距离 != null) ? 目标位移距离.Length : 0) > this.技能等级) ? c_03_计算对象位移.目标位移距离[this.技能等级] : 0, val1: c_03_计算对象位移.反向推动目标 ? 计算类.网格距离(this.技能来源.当前坐标, item33.Value.技能目标.当前坐标) : (8 - 计算类.网格距离(this.技能来源.当前坐标, item33.Value.技能目标.当前坐标))));
									if (num22 == 0)
									{
										continue;
									}
									Point 锚点2;
									锚点2 = 计算类.前方坐标(方向: c_03_计算对象位移.反向推动目标 ? 计算类.计算方向(item33.Value.技能目标.当前坐标, this.技能来源.当前坐标) : 计算类.计算方向(this.技能来源.当前坐标, item33.Value.技能目标.当前坐标), 原点: item33.Value.技能目标.当前坐标, 步数: num22);
									if (item33.Value.技能目标.能否位移(this.技能来源, 锚点2, num22, 0, 穿墙: false, out var 终点2, out var _))
									{
										if (计算类.计算概率(c_03_计算对象位移.位移Buff概率))
										{
											item33.Value.技能目标.添加Buff时处理(c_03_计算对象位移.目标位移编号, this.技能来源);
										}
										if (计算类.计算概率(c_03_计算对象位移.附加Buff概率) && item33.Value.技能目标.特定类型(this.技能来源, c_03_计算对象位移.限定附加类型))
										{
											item33.Value.技能目标.添加Buff时处理(c_03_计算对象位移.目标附加编号, this.技能来源);
										}
										item33.Value.技能目标.当前方向 = 计算类.计算方向(item33.Value.技能目标.当前坐标, this.技能来源.当前坐标);
										ushort num23;
										num23 = (ushort)(计算类.网格距离(item33.Value.技能目标.当前坐标, 终点2) * c_03_计算对象位移.目标位移耗时);
										item33.Value.技能目标.忙碌时间 = 主程.当前时间.AddMilliseconds(num23 * 60);
										item33.Value.技能目标.硬直时间 = 主程.当前时间.AddMilliseconds(num23 * 60 + c_03_计算对象位移.目标硬直时间);
										item33.Value.技能目标.发送封包(new 对象被动位移
										{
											位移坐标 = 终点2,
											位移速度 = num23,
											对象编号 = item33.Value.技能目标.地图编号,
											位移朝向 = (ushort)item33.Value.技能目标.当前方向,
											位移方式 = c_03_计算对象位移.角色位移方式
										});
										item33.Value.技能目标.自身移动时处理(终点2);
										if (c_03_计算对象位移.推动增加经验 && !this.经验增加 && this.技能来源 != null && this.技能来源 is 玩家实例 玩家实例17)
										{
											玩家实例17.技能增加经验(this.技能编号);
											this.经验增加 = true;
										}
									}
								}
							}
							else if (c_03_计算对象位移.互换目标坐标)
							{
								foreach (KeyValuePair<int, 命中详情> item34 in this.命中列表)
								{
									if ((item34.Value.技能反馈 & 技能命中反馈.闪避) == 0 && (item34.Value.技能反馈 & 技能命中反馈.丢失) == 0 && (item34.Value.技能反馈 & 技能命中反馈.死亡) == 0)
									{
										Point 当前坐标;
										当前坐标 = item34.Value.技能目标.当前坐标;
										Point 当前坐标2;
										当前坐标2 = this.技能来源.当前坐标;
										if (计算类.计算概率(c_03_计算对象位移.位移Buff概率))
										{
											item34.Value.技能目标.添加Buff时处理(c_03_计算对象位移.目标位移编号, this.技能来源);
										}
										if (计算类.计算概率(c_03_计算对象位移.附加Buff概率) && item34.Value.技能目标.特定类型(this.技能来源, c_03_计算对象位移.限定附加类型))
										{
											item34.Value.技能目标.添加Buff时处理(c_03_计算对象位移.目标附加编号, this.技能来源);
										}
										item34.Value.技能目标.当前方向 = 计算类.计算方向(item34.Value.技能目标.当前坐标, this.技能来源.当前坐标);
										int num24;
										num24 = 计算类.网格距离(当前坐标, 当前坐标2);
										if (c_03_计算对象位移.互换最大距离 > 0 && num24 > c_03_计算对象位移.互换最大距离)
										{
											this.技能来源.添加Buff时处理(c_03_计算对象位移.失败Buff编号, this.技能来源);
											this.技能来源.发送封包(new 技能释放完成
											{
												技能编号 = this.技能编号,
												动作编号 = this.动作编号
											});
											this.技能来源.技能任务.Remove(this);
											return;
										}
										ushort num25;
										num25 = (ushort)(num24 * c_03_计算对象位移.目标位移耗时);
										item34.Value.技能目标.忙碌时间 = 主程.当前时间.AddMilliseconds(num25 * 60);
										item34.Value.技能目标.发送封包(new 对象被动位移
										{
											位移坐标 = 当前坐标2,
											位移速度 = num25,
											对象编号 = item34.Value.技能目标.地图编号,
											位移朝向 = (ushort)item34.Value.技能目标.当前方向,
											位移方式 = c_03_计算对象位移.角色位移方式
										});
										item34.Value.技能目标.自身移动时处理(当前坐标2);
										this.技能来源.当前方向 = 计算类.计算方向(this.技能来源.当前坐标, item34.Value.技能目标.当前坐标);
										ushort num26;
										num26 = (ushort)(num24 * c_03_计算对象位移.自身位移耗时);
										this.技能来源.忙碌时间 = 主程.当前时间.AddMilliseconds(num26 * 60);
										this.技能来源.发送封包(new 对象被动位移
										{
											位移坐标 = 当前坐标,
											位移速度 = num26,
											对象编号 = this.技能来源.地图编号,
											位移朝向 = (ushort)this.技能来源.当前方向,
											位移方式 = c_03_计算对象位移.角色位移方式
										});
										this.技能来源.自身移动时处理(当前坐标);
									}
								}
							}
						}
						else if (value is C_04_计算目标诱惑 参数)
						{
							foreach (KeyValuePair<int, 命中详情> item35 in this.命中列表)
							{
								(this.技能来源 as 玩家实例).玩家诱惑目标(this, 参数, item35.Value.技能目标);
							}
						}
						else
						{
							C_06_计算宠物召唤 c_06_计算宠物召唤;
							c_06_计算宠物召唤 = value as C_06_计算宠物召唤;
							if (c_06_计算宠物召唤 != null)
							{
								if (c_06_计算宠物召唤.怪物召唤同伴)
								{
									if (c_06_计算宠物召唤.召唤宠物名字 == null || c_06_计算宠物召唤.召唤宠物名字.Length == 0)
									{
										return;
									}
									if (游戏怪物.数据表.TryGetValue(c_06_计算宠物召唤.召唤宠物名字, out var value6))
									{
										if (c_06_计算宠物召唤.启用范围召唤)
										{
											List<Point> list11;
											list11 = c_06_计算宠物召唤.范围召唤怪物[游戏方向.左方];
											if (c_06_计算宠物召唤.计算对象方向)
											{
												list11 = c_06_计算宠物召唤.范围召唤怪物[this.技能来源.当前方向];
											}
											new List<Point>();
											foreach (Point item36 in list11)
											{
												怪物实例 怪物实例2;
												怪物实例2 = new 怪物实例(value6, this.释放地图, int.MaxValue, new Point[1]
												{
													new Point(item36.X + this.释放位置.X, item36.Y + this.释放位置.Y)
												}, 禁止复活: true, 立即刷新: true, c_06_计算宠物召唤.死亡同伴消失 ? (this.技能来源 as 怪物实例) : null);
												怪物实例2.存活时间 = 主程.当前时间.AddMinutes(1.0);
												if (c_06_计算宠物召唤.同伴添加BUFF > 0)
												{
													怪物实例2.添加Buff时处理(c_06_计算宠物召唤.同伴添加BUFF, this.技能来源);
												}
											}
										}
										else
										{
											怪物实例 怪物实例3;
											怪物实例3 = new 怪物实例(value6, this.释放地图, int.MaxValue, new Point[1] { this.释放位置 }, 禁止复活: true, 立即刷新: true, c_06_计算宠物召唤.死亡同伴消失 ? (this.技能来源 as 怪物实例) : null);
											怪物实例3.存活时间 = 主程.当前时间.AddMinutes(1.0);
											if (c_06_计算宠物召唤.同伴添加BUFF > 0)
											{
												怪物实例3.添加Buff时处理(c_06_计算宠物召唤.同伴添加BUFF, this.技能来源);
											}
										}
									}
								}
								else if (this.技能来源 is 玩家实例 玩家实例18)
								{
									if ((c_06_计算宠物召唤.检查技能铭文 && (!玩家实例18.主体技能表.TryGetValue(this.技能编号, out var v29) || v29.铭文编号 != this.铭文编号)) || c_06_计算宠物召唤.召唤宠物名字 == null || c_06_计算宠物召唤.召唤宠物名字.Length == 0)
									{
										return;
									}
									int num27;
									num27 = ((c_06_计算宠物召唤.召唤宠物数量?.Length > this.技能等级) ? c_06_计算宠物召唤.召唤宠物数量[this.技能等级] : 0);
									if (玩家实例18.宠物列表.FindAll((宠物实例 宠物) => ((c_06_计算宠物召唤.忽略宠物列表 == null || Array.IndexOf(c_06_计算宠物召唤.忽略宠物列表, 宠物.宠物数据.宠物名字.V) == -1) && !宠物.物品召唤) ? true : false).Count < num27 && 游戏怪物.数据表.TryGetValue(c_06_计算宠物召唤.召唤宠物名字, out var value7))
									{
										宠物实例 宠物实例3;
										宠物实例3 = new 宠物实例(等级上限: (byte)((c_06_计算宠物召唤.宠物等级上限?.Length > this.技能等级) ? c_06_计算宠物召唤.宠物等级上限[this.技能等级] : 0), 宠物主人: 玩家实例18, 召唤宠物: value7, 初始等级: this.技能等级, 绑定武器: c_06_计算宠物召唤.宠物绑定武器, 绑定BUFF: c_06_计算宠物召唤.宠物绑定BUFF, 存活时长: (c_06_计算宠物召唤.宠物存活时间 > 0) ? c_06_计算宠物召唤.宠物存活时间 : int.MaxValue);
										玩家实例18.网络连接?.发送封包(new 同步宠物等级
										{
											宠物编号 = 宠物实例3.地图编号,
											宠物等级 = 宠物实例3.宠物等级
										});
										玩家实例18.网络连接?.发送封包(new 游戏错误提示
										{
											错误代码 = 9473,
											第一参数 = (int)玩家实例18.宠物模式
										});
										玩家实例18.宠物数据.Add(宠物实例3.宠物数据);
										玩家实例18.宠物列表.Add(宠物实例3);
										if (c_06_计算宠物召唤.增加技能经验)
										{
											玩家实例18.技能增加经验(c_06_计算宠物召唤.经验技能编号);
										}
										玩家实例18.龙卫召唤物加BUFF(this.技能编号, 宠物实例3);
									}
								}
							}
							else if (value is C_05_计算目标回复 c_05_计算目标回复)
							{
								foreach (KeyValuePair<int, 命中详情> item37 in this.命中列表)
								{
									item37.Value.技能目标.被动回复时处理(this, c_05_计算目标回复);
								}
								if (c_05_计算目标回复.增加技能经验 && this.命中列表.Count != 0)
								{
									(this.技能来源 as 玩家实例).技能增加经验(c_05_计算目标回复.经验技能编号);
								}
							}
							else if (value is C_07_计算目标瞬移 参数2)
							{
								(this.技能来源 as 玩家实例).玩家瞬间移动(this, 参数2);
							}
						}
					}
				}
				if (this.节点列表.Count == 0)
				{
					if (this.技能来源 is 玩家实例 玩家实例19)
					{
						玩家实例19.龙卫释放减少冷却(this.技能编号, this.铭文编号);
					}
					this.技能来源.技能任务.Remove(this);
				}
				else
				{
					this.预约时间 = this.释放时间.AddMilliseconds(this.飞行耗时 + this.节点列表.First().Key);
					this.处理任务();
				}
			}
		}

		public void 技能中断()
		{
			this.节点列表.Clear();
			this.技能来源.发送封包(new 技能释放中断
			{
				对象编号 = ((!this.目标借位 || this.技能目标 == null) ? this.技能来源.地图编号 : this.技能目标.地图编号),
				技能编号 = this.技能编号,
				技能等级 = this.技能等级,
				技能铭文 = this.铭文编号,
				动作编号 = this.动作编号,
				技能分段 = this.分段编号
			});
			this.技能来源.技能任务.Remove(this);
		}
	}
}
