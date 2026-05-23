using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _0005_0005_0004_0004_0001_0005_0003_0001_0002_0005_0005_0002_0001_0002;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.网络类;

namespace 游戏服务器.数据类
{
	public class 装备数据 : 物品数据
	{
		public readonly 数据监视器<byte> 升级次数;

		public readonly 数据监视器<byte> 升级攻击;

		public readonly 数据监视器<byte> 升级魔法;

		public readonly 数据监视器<byte> 升级道术;

		public readonly 数据监视器<byte> 升级刺术;

		public readonly 数据监视器<byte> 升级弓术;

		public readonly 数据监视器<bool> 灵魂绑定;

		public readonly 数据监视器<byte> 祈祷次数;

		public readonly 数据监视器<sbyte> 幸运等级;

		public readonly 数据监视器<bool> 装备神佑;

		public readonly 数据监视器<byte> 神圣伤害;

		public readonly 数据监视器<ushort> 圣石数量;

		public readonly 数据监视器<bool> 双铭文栏;

		public readonly 数据监视器<byte> 当前铭栏;

		public readonly 数据监视器<int> 洗练数一;

		public readonly 数据监视器<int> 洗练数二;

		public readonly 数据监视器<byte> 物品状态;

		public readonly 列表监视器<随机属性> 随机属性;

		public readonly 列表监视器<装备孔洞颜色> 孔洞颜色;

		public readonly 字典监视器<byte, 铭文技能> 铭文技能;

		public readonly 字典监视器<byte, 游戏物品> 镶嵌灵石;

		public readonly 数据监视器<byte> 失败次数;

		public readonly 数据监视器<byte> 升级属性;

		public readonly 数据监视器<byte> 铸魂次数;

		public readonly 数据监视器<int> 扣除持久;

		public readonly 数据监视器<byte> 开启精炼;

		public readonly 数据监视器<ushort> 精炼值一;

		public readonly 数据监视器<ushort> 精炼值二;

		public readonly 数据监视器<ushort> 精炼值三;

		public readonly 数据监视器<ushort> 精炼次数;

		public 游戏装备 装备模板 => base.物品模板 as 游戏装备;

		public int 精炼战力
		{
			get
			{
				int num;
				num = 0;
				if (this.精炼值一.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值一.V, out var value))
				{
					num += value.战力加成;
				}
				if (this.精炼值二.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值二.V, out var value2))
				{
					num += value2.战力加成;
				}
				if (this.精炼值三.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值三.V, out var value3))
				{
					num += value3.战力加成;
				}
				return num;
			}
		}

		public int 装备战力
		{
			get
			{
				if (this.装备模板.物品分类 == 物品使用分类.武器)
				{
					return (int)(this.装备模板.基础战力 * (this.幸运等级.V + 20) * 1717986919L >> 32 >> 3) + (this.神圣伤害.V * 3 + this.升级攻击.V * 5 + this.升级魔法.V * 5 + this.升级道术.V * 5 + this.升级刺术.V * 5 + this.升级弓术.V * 5) + this.随机属性.Sum((随机属性 x) => x.战力加成) + this.精炼战力;
				}
				int num;
				num = ((this.升级次数.V > 0 && 装备升级.数据表.TryGetValue(装备升级.数据表.Keys.First((升级装备 x) => x.装备编号 == base.物品编号 && x.升级等级 == this.升级次数.V - 1), out var value)) ? (value.升级属性一.战力 + value.升级属性二.战力) : 0);
				int num2;
				num2 = this.孔洞颜色.Count * 10;
				foreach (游戏物品 value2 in this.镶嵌灵石.Values)
				{
					string text;
					text = value2.物品名字;
					uint num3;
					num3 = method01(text);
					if (num3 <= 1965594569)
					{
						if (num3 <= 943749297)
						{
							if (num3 <= 573099060)
							{
								if (num3 <= 245049310)
								{
									if (num3 <= 208490910)
									{
										if (num3 <= 36171325)
										{
											if (num3 != 35240798)
											{
												if (num3 != 36171325 || !(text == "精绿灵石2级"))
												{
													continue;
												}
												goto IL_181a;
											}
											if (!(text == "精绿灵石5级"))
											{
												continue;
											}
											goto IL_17bb;
										}
										if (num3 == 36983370)
										{
											if (!(text == "精绿灵石9级"))
											{
												continue;
											}
											goto IL_1889;
										}
										if (num3 != 74678801)
										{
											if (num3 != 208490910 || !(text == "驭朱灵石4级"))
											{
												continue;
											}
											goto IL_18c5;
										}
										if (!(text == "透蓝灵石1级"))
										{
											continue;
										}
									}
									else
									{
										if (num3 > 209834109)
										{
											if (num3 != 210646154)
											{
												if (num3 != 234198814)
												{
													if (num3 != 245049310 || !(text == "蔚蓝灵石10级"))
													{
														continue;
													}
												}
												else if (!(text == "狂热幻彩灵石10级"))
												{
													continue;
												}
												goto IL_1927;
											}
											if (!(text == "驭朱灵石8级"))
											{
												continue;
											}
											goto IL_1703;
										}
										if (num3 != 209302955)
										{
											if (num3 != 209834109 || !(text == "驭朱灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (!(text == "驭朱灵石1级"))
										{
											continue;
										}
									}
									goto IL_1911;
								}
								if (num3 <= 406321612)
								{
									if (num3 <= 305587683)
									{
										if (num3 == 263991377)
										{
											if (!(text == "抵御幻彩灵石7级"))
											{
												continue;
											}
											goto IL_186d;
										}
										if (num3 != 305587683 || !(text == "精绿灵石8级"))
										{
											continue;
										}
									}
									else
									{
										if (num3 == 335211465)
										{
											if (!(text == "韧紫灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (num3 != 336023510)
										{
											if (num3 != 406321612 || !(text == "精绿灵石10级"))
											{
												continue;
											}
											goto IL_1927;
										}
										if (!(text == "韧紫灵石8级"))
										{
											continue;
										}
									}
								}
								else
								{
									if (num3 <= 479250467)
									{
										if (num3 != 470449305)
										{
											if (num3 != 479250467 || !(text == "驭朱灵石9级"))
											{
												continue;
											}
											goto IL_1889;
										}
										if (!(text == "命朱灵石7级"))
										{
											continue;
										}
										goto IL_186d;
									}
									if (num3 != 531090082)
									{
										if (num3 != 549347465)
										{
											if (num3 != 573099060 || !(text == "精绿灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (!(text == "透蓝灵石10级"))
										{
											continue;
										}
										goto IL_1927;
									}
									if (!(text == "抵御幻彩灵石8级"))
									{
										continue;
									}
								}
								goto IL_1703;
							}
							if (num3 <= 738772727)
							{
								if (num3 <= 680541790)
								{
									if (num3 > 607107224)
									{
										if (num3 != 607638378)
										{
											if (num3 != 611931354)
											{
												if (num3 != 680541790 || !(text == "橙黄灵石10级"))
												{
													continue;
												}
												goto IL_1927;
											}
											if (!(text == "透蓝灵石6级"))
											{
												continue;
											}
											goto IL_18de;
										}
										if (!(text == "新阳灵石3级"))
										{
											continue;
										}
										goto IL_193d;
									}
									if (num3 == 603534887)
									{
										if (!(text == "韧紫灵石1级"))
										{
											continue;
										}
										goto IL_1911;
									}
									if (num3 != 607107224 || !(text == "新阳灵石5级"))
									{
										continue;
									}
								}
								else
								{
									if (num3 <= 692924671)
									{
										if (num3 != 691994144)
										{
											if (num3 != 692924671 || !(text == "蔚蓝灵石2级"))
											{
												continue;
											}
											goto IL_181a;
										}
										if (!(text == "蔚蓝灵石9级"))
										{
											continue;
										}
										goto IL_1889;
									}
									if (num3 != 693736716)
									{
										if (num3 == 714727999)
										{
											if (!(text == "抵御幻彩灵石10级"))
											{
												continue;
											}
											goto IL_1927;
										}
										if (num3 != 738772727 || !(text == "命朱灵石5级"))
										{
											continue;
										}
									}
									else if (!(text == "蔚蓝灵石5级"))
									{
										continue;
									}
								}
								goto IL_17bb;
							}
							if (num3 <= 804167584)
							{
								if (num3 > 771022468)
								{
									if (num3 != 799900731)
									{
										if (num3 == 800712776)
										{
											if (!(text == "抵御幻彩灵石6级"))
											{
												continue;
											}
											goto IL_18de;
										}
										if (num3 != 804167584 || !(text == "橙黄灵石9级"))
										{
											continue;
										}
									}
									else if (!(text == "抵御幻彩灵石9级"))
									{
										continue;
									}
									goto IL_1889;
								}
								if (num3 != 746349172)
								{
									if (num3 != 771022468 || !(text == "守阳灵石1级"))
									{
										continue;
									}
									goto IL_1911;
								}
								if (!(text == "驭朱灵石2级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 > 805910156)
								{
									if (num3 != 840642163)
									{
										if (num3 != 896281263)
										{
											if (num3 != 943749297 || !(text == "深灰灵石4级"))
											{
												continue;
											}
											goto IL_18c5;
										}
										if (!(text == "进击幻彩灵石10级"))
										{
											continue;
										}
										goto IL_1927;
									}
									if (!(text == "赤褐灵石7级"))
									{
										continue;
									}
									goto IL_186d;
								}
								if (num3 != 805098111)
								{
									if (num3 != 805910156 || !(text == "橙黄灵石5级"))
									{
										continue;
									}
									goto IL_17bb;
								}
								if (!(text == "橙黄灵石2级"))
								{
									continue;
								}
							}
							goto IL_181a;
						}
						if (num3 <= 1307537531)
						{
							if (num3 > 1144359777)
							{
								if (num3 <= 1230989269)
								{
									if (num3 <= 1211660047)
									{
										if (num3 == 1200044703)
										{
											if (!(text == "纯紫灵石1级"))
											{
												continue;
											}
											goto IL_1911;
										}
										if (num3 != 1211660047 || !(text == "深灰灵石6级"))
										{
											continue;
										}
									}
									else
									{
										if (num3 == 1229646070)
										{
											if (!(text == "蔚蓝灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (num3 != 1230458115)
										{
											if (num3 != 1230989269 || !(text == "蔚蓝灵石4级"))
											{
												continue;
											}
											goto IL_18c5;
										}
										if (!(text == "蔚蓝灵石6级"))
										{
											continue;
										}
									}
								}
								else
								{
									if (num3 <= 1276630989)
									{
										if (num3 != 1275287790)
										{
											if (num3 != 1276630989 || !(text == "命朱灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (!(text == "命朱灵石4级"))
										{
											continue;
										}
										goto IL_18c5;
									}
									if (num3 != 1284105784)
									{
										if (num3 != 1300070770)
										{
											if (num3 != 1307537531 || !(text == "守阳灵石2级"))
											{
												continue;
											}
											goto IL_181a;
										}
										if (!(text == "守阳灵石10级"))
										{
											continue;
										}
										goto IL_1927;
									}
									if (!(text == "进击幻彩灵石6级"))
									{
										continue;
									}
								}
								goto IL_18de;
							}
							if (num3 <= 1038933218)
							{
								if (num3 <= 1014483090)
								{
									if (num3 != 1007377040)
									{
										if (num3 != 1014483090 || !(text == "进击幻彩灵石8级"))
										{
											continue;
										}
										goto IL_1703;
									}
									if (!(text == "命朱灵石6级"))
									{
										continue;
									}
									goto IL_18de;
								}
								if (num3 == 1015295135)
								{
									if (!(text == "进击幻彩灵石5级"))
									{
										continue;
									}
									goto IL_17bb;
								}
								if (num3 == 1025365623)
								{
									if (!(text == "命朱灵石10级"))
									{
										continue;
									}
									goto IL_1927;
								}
								if (num3 != 1038933218 || !(text == "守阳灵石3级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 > 1127514398)
								{
									if (num3 != 1128326443)
									{
										if (num3 != 1128444925)
										{
											if (num3 != 1144359777 || !(text == "新阳灵石4级"))
											{
												continue;
											}
										}
										else if (!(text == "盈绿灵石4级"))
										{
											continue;
										}
										goto IL_18c5;
									}
									if (!(text == "盈绿灵石6级"))
									{
										continue;
									}
									goto IL_18de;
								}
								if (num3 == 1108552913)
								{
									if (!(text == "赤褐灵石1级"))
									{
										continue;
									}
									goto IL_1911;
								}
								if (num3 != 1127514398 || !(text == "盈绿灵石3级"))
								{
									continue;
								}
							}
						}
						else
						{
							if (num3 > 1665978369)
							{
								if (num3 <= 1914880594)
								{
									if (num3 <= 1697659818)
									{
										if (num3 == 1697128664)
										{
											if (!(text == "狂热幻彩灵石1级"))
											{
												continue;
											}
											goto IL_1911;
										}
										if (num3 != 1697659818 || !(text == "狂热幻彩灵石7级"))
										{
											continue;
										}
									}
									else if (num3 != 1738109301)
									{
										if (num3 != 1914349440)
										{
											if (num3 != 1914880594 || !(text == "精绿灵石1级"))
											{
												continue;
											}
											goto IL_1911;
										}
										if (!(text == "精绿灵石7级"))
										{
											continue;
										}
									}
									else if (!(text == "纯紫灵石7级"))
									{
										continue;
									}
									goto IL_186d;
								}
								if (num3 <= 1953388070)
								{
									if (num3 != 1952576025)
									{
										if (num3 != 1953388070 || !(text == "透蓝灵石2级"))
										{
											continue;
										}
										goto IL_181a;
									}
									if (!(text == "透蓝灵石9级"))
									{
										continue;
									}
									goto IL_1889;
								}
								if (num3 != 1954318597)
								{
									if (num3 != 1964640041)
									{
										if (num3 != 1965594569 || !(text == "赤褐灵石10级"))
										{
											continue;
										}
										goto IL_1927;
									}
									if (!(text == "狂热幻彩灵石8级"))
									{
										continue;
									}
									goto IL_1703;
								}
								if (!(text == "透蓝灵石5级"))
								{
									continue;
								}
								goto IL_17bb;
							}
							if (num3 <= 1480470696)
							{
								if (num3 > 1342837891)
								{
									if (num3 != 1342956373)
									{
										if (num3 != 1468855352)
										{
											if (num3 != 1480470696 || !(text == "深灰灵石5级"))
											{
												continue;
											}
											goto IL_17bb;
										}
										if (!(text == "纯紫灵石2级"))
										{
											continue;
										}
										goto IL_181a;
									}
									if (!(text == "橙黄灵石4级"))
									{
										continue;
									}
									goto IL_18c5;
								}
								if (num3 != 1342025846)
								{
									if (num3 != 1342837891 || !(text == "橙黄灵石6级"))
									{
										continue;
									}
									goto IL_18de;
								}
								if (!(text == "橙黄灵石3级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 > 1553359733)
								{
									if (num3 != 1645599130)
									{
										if (num3 != 1665166324)
										{
											if (num3 != 1665978369 || !(text == "盈绿灵石8级"))
											{
												continue;
											}
											goto IL_1703;
										}
										if (!(text == "盈绿灵石5级"))
										{
											continue;
										}
										goto IL_17bb;
									}
									if (!(text == "赤褐灵石6级"))
									{
										continue;
									}
									goto IL_18de;
								}
								if (num3 == 1552016534)
								{
									if (!(text == "进击幻彩灵石4级"))
									{
										continue;
									}
									goto IL_18c5;
								}
								if (num3 != 1553359733 || !(text == "进击幻彩灵石3级"))
								{
									continue;
								}
							}
						}
						goto IL_193d;
					}
					if (num3 <= 3186246800u)
					{
						if (num3 <= 2719719079u)
						{
							if (num3 <= 2489297424u)
							{
								if (num3 <= 2214629611u)
								{
									if (num3 > 2087909056)
									{
										if (num3 != 2142391142)
										{
											if (num3 != 2143734341)
											{
												if (num3 != 2214629611u || !(text == "韧紫灵石5级"))
												{
													continue;
												}
												goto IL_17bb;
											}
											if (!(text == "抵御幻彩灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (!(text == "抵御幻彩灵石4级"))
										{
											continue;
										}
										goto IL_18c5;
									}
									if (num3 == 2004277479)
									{
										if (!(text == "纯紫灵石9级"))
										{
											continue;
										}
										goto IL_1889;
									}
									if (num3 != 2087909056 || !(text == "驭朱灵石6级"))
									{
										continue;
									}
								}
								else if (num3 <= 2451395657u)
								{
									if (num3 == 2215160765u)
									{
										if (!(text == "韧紫灵石7级"))
										{
											continue;
										}
										goto IL_186d;
									}
									if (num3 != 2451395657u || !(text == "精绿灵石6级"))
									{
										continue;
									}
								}
								else
								{
									if (num3 != 2486038143u)
									{
										if (num3 != 2486850188u)
										{
											if (num3 != 2489297424u || !(text == "透蓝灵石8级"))
											{
												continue;
											}
											goto IL_1703;
										}
										if (!(text == "新阳灵石1级"))
										{
											continue;
										}
										goto IL_1911;
									}
									if (!(text == "新阳灵石6级"))
									{
										continue;
									}
								}
								goto IL_18de;
							}
							if (num3 <= 2649319065u)
							{
								if (num3 <= 2491039996u)
								{
									if (num3 == 2490227951u)
									{
										if (!(text == "透蓝灵石3级"))
										{
											continue;
										}
										goto IL_193d;
									}
									if (num3 != 2491039996u || !(text == "透蓝灵石4级"))
									{
										continue;
									}
								}
								else
								{
									if (num3 == 2619608627u)
									{
										if (!(text == "命朱灵石9级"))
										{
											continue;
										}
										goto IL_1889;
									}
									if (num3 == 2625161609u)
									{
										if (!(text == "驭朱灵石7级"))
										{
											continue;
										}
										goto IL_186d;
									}
									if (num3 != 2649319065u || !(text == "守阳灵石4级"))
									{
										continue;
									}
								}
							}
							else
							{
								if (num3 <= 2679437359u)
								{
									if (num3 != 2651474309u)
									{
										if (num3 != 2679437359u || !(text == "抵御幻彩灵石5级"))
										{
											continue;
										}
										goto IL_17bb;
									}
									if (!(text == "守阳灵石8级"))
									{
										continue;
									}
									goto IL_1703;
								}
								if (num3 == 2680249404u)
								{
									if (!(text == "抵御幻彩灵石2级"))
									{
										continue;
									}
									goto IL_181a;
								}
								if (num3 == 2718739222u)
								{
									if (!(text == "盈绿灵石10级"))
									{
										continue;
									}
									goto IL_1927;
								}
								if (num3 != 2719719079u || !(text == "精绿灵石4级"))
								{
									continue;
								}
							}
							goto IL_18c5;
						}
						if (num3 <= 2988502213u)
						{
							if (num3 > 2887120004u)
							{
								if (num3 <= 2917642487u)
								{
									if (num3 != 2893072359u)
									{
										if (num3 != 2917642487u || !(text == "守阳灵石6级"))
										{
											continue;
										}
										goto IL_18de;
									}
									if (!(text == "驭朱灵石5级"))
									{
										continue;
									}
								}
								else
								{
									if (num3 == 2986346969u)
									{
										if (!(text == "赤褐灵石9级"))
										{
											continue;
										}
										goto IL_1889;
									}
									if (num3 == 2987159014u)
									{
										if (!(text == "赤褐灵石2级"))
										{
											continue;
										}
										goto IL_181a;
									}
									if (num3 != 2988502213u || !(text == "赤褐灵石5级"))
									{
										continue;
									}
								}
								goto IL_17bb;
							}
							if (num3 <= 2754361565u)
							{
								if (num3 != 2751882164u)
								{
									if (num3 != 2754361565u || !(text == "新阳灵石8级"))
									{
										continue;
									}
									goto IL_1703;
								}
								if (!(text == "韧紫灵石6级"))
								{
									continue;
								}
								goto IL_18de;
							}
							if (num3 != 2822149062u)
							{
								if (num3 != 2822961107u)
								{
									if (num3 != 2887120004u || !(text == "命朱灵石2级"))
									{
										continue;
									}
								}
								else if (!(text == "深灰灵石2级"))
								{
									continue;
								}
								goto IL_181a;
							}
							if (!(text == "深灰灵石7级"))
							{
								continue;
							}
						}
						else if (num3 <= 3090472484u)
						{
							if (num3 <= 3007257362u)
							{
								if (num3 == 3006726208u)
								{
									if (!(text == "盈绿灵石1级"))
									{
										continue;
									}
									goto IL_1911;
								}
								if (num3 != 3007257362u || !(text == "盈绿灵石7级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 != 3022965878u)
								{
									if (num3 != 3023777923u)
									{
										if (num3 != 3090472484u || !(text == "深灰灵石9级"))
										{
											continue;
										}
										goto IL_1889;
									}
									if (!(text == "新阳灵石2级"))
									{
										continue;
									}
									goto IL_181a;
								}
								if (!(text == "新阳灵石7级"))
								{
									continue;
								}
							}
						}
						else
						{
							if (num3 <= 3109167384u)
							{
								if (num3 != 3101887706u)
								{
									if (num3 != 3109167384u || !(text == "蔚蓝灵石1级"))
									{
										continue;
									}
									goto IL_1911;
								}
								if (!(text == "深灰灵石10级"))
								{
									continue;
								}
								goto IL_1927;
							}
							if (num3 != 3109285866u)
							{
								if (num3 != 3163745580u)
								{
									if (num3 != 3186246800u || !(text == "守阳灵石5级"))
									{
										continue;
									}
									goto IL_17bb;
								}
								if (!(text == "进击幻彩灵石2级"))
								{
									continue;
								}
								goto IL_181a;
							}
							if (!(text == "蔚蓝灵石7级"))
							{
								continue;
							}
						}
					}
					else
					{
						if (num3 <= 3581907291u)
						{
							if (num3 <= 3424978266u)
							{
								if (num3 > 3290876628u)
								{
									if (num3 <= 3360007324u)
									{
										if (num3 != 3348495148u)
										{
											if (num3 != 3360007324u || !(text == "深灰灵石1级"))
											{
												continue;
											}
											goto IL_1911;
										}
										if (!(text == "纯紫灵石6级"))
										{
											continue;
										}
										goto IL_18de;
									}
									if (num3 != 3376266089u)
									{
										if (num3 == 3423635067u)
										{
											if (!(text == "命朱灵石1级"))
											{
												continue;
											}
											goto IL_1911;
										}
										if (num3 != 3424978266u || !(text == "命朱灵石8级"))
										{
											continue;
										}
									}
									else if (!(text == "蔚蓝灵石8级"))
									{
										continue;
									}
									goto IL_1703;
								}
								if (num3 <= 3221134488u)
								{
									if (num3 != 3187989372u)
									{
										if (num3 != 3221134488u || !(text == "橙黄灵石1级"))
										{
											continue;
										}
										goto IL_1911;
									}
									if (!(text == "守阳灵石9级"))
									{
										continue;
									}
								}
								else
								{
									if (num3 == 3221665642u)
									{
										if (!(text == "橙黄灵石7级"))
										{
											continue;
										}
										goto IL_186d;
									}
									if (num3 != 3276673720u)
									{
										if (num3 != 3290876628u || !(text == "新阳灵石9级"))
										{
											continue;
										}
									}
									else if (!(text == "盈绿灵石9级"))
									{
										continue;
									}
								}
							}
							else if (num3 <= 3524411567u)
							{
								if (num3 > 3454157550u)
								{
									if (num3 != 3488645865u)
									{
										if (num3 != 3523068368u)
										{
											if (num3 != 3524411567u || !(text == "赤褐灵石3级"))
											{
												continue;
											}
											goto IL_193d;
										}
										if (!(text == "赤褐灵石8级"))
										{
											continue;
										}
									}
									else if (!(text == "橙黄灵石8级"))
									{
										continue;
									}
									goto IL_1703;
								}
								if (num3 != 3430725803u)
								{
									if (num3 != 3454157550u || !(text == "守阳灵石7级"))
									{
										continue;
									}
									goto IL_186d;
								}
								if (!(text == "进击幻彩灵石9级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 > 3575025888u)
								{
									if (num3 != 3575956415u)
									{
										if (num3 != 3576768460u)
										{
											if (num3 != 3581907291u || !(text == "韧紫灵石10级"))
											{
												continue;
											}
											goto IL_1927;
										}
										if (!(text == "狂热幻彩灵石5级"))
										{
											continue;
										}
										goto IL_17bb;
									}
									if (!(text == "狂热幻彩灵石2级"))
									{
										continue;
									}
									goto IL_181a;
								}
								if (num3 == 3525223612u)
								{
									if (!(text == "赤褐灵石4级"))
									{
										continue;
									}
									goto IL_18c5;
								}
								if (num3 != 3575025888u || !(text == "狂热幻彩灵石9级"))
								{
									continue;
								}
							}
							goto IL_1889;
						}
						if (num3 > 3968584065u)
						{
							if (num3 <= 4112884150u)
							{
								if (num3 <= 4093457362u)
								{
									if (num3 != 4093338880u)
									{
										if (num3 != 4093457362u || !(text == "韧紫灵石4级"))
										{
											continue;
										}
										goto IL_18c5;
									}
									if (!(text == "韧紫灵石2级"))
									{
										continue;
									}
									goto IL_181a;
								}
								if (num3 == 4094269407u)
								{
									if (!(text == "韧紫灵石9级"))
									{
										continue;
									}
									goto IL_1889;
								}
								if (num3 == 4101735347u)
								{
									if (!(text == "透蓝灵石7级"))
									{
										continue;
									}
									goto IL_186d;
								}
								if (num3 != 4112884150u || !(text == "狂热幻彩灵石3级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 <= 4113814677u)
								{
									if (num3 != 4113696195u)
									{
										if (num3 != 4113814677u || !(text == "狂热幻彩灵石4级"))
										{
											continue;
										}
										goto IL_18c5;
									}
									if (!(text == "狂热幻彩灵石6级"))
									{
										continue;
									}
									goto IL_18de;
								}
								if (num3 != 4153333633u)
								{
									if (num3 != 4189275687u)
									{
										if (num3 != 4290635251u || !(text == "抵御幻彩灵石1级"))
										{
											continue;
										}
										goto IL_1911;
									}
									if (!(text == "驭朱灵石10级"))
									{
										continue;
									}
									goto IL_1927;
								}
								if (!(text == "纯紫灵石3级"))
								{
									continue;
								}
							}
							goto IL_193d;
						}
						if (num3 <= 3686647075u)
						{
							if (num3 <= 3616405898u)
							{
								if (num3 != 3614663326u)
								{
									if (num3 != 3616405898u || !(text == "纯紫灵石4级"))
									{
										continue;
									}
									goto IL_18c5;
								}
								if (!(text == "纯紫灵石8级"))
								{
									continue;
								}
							}
							else
							{
								if (num3 != 3627518701u)
								{
									if (num3 != 3628330746u)
									{
										if (num3 != 3686647075u || !(text == "纯紫灵石10级"))
										{
											continue;
										}
										goto IL_1927;
									}
									if (!(text == "深灰灵石3级"))
									{
										continue;
									}
									goto IL_193d;
								}
								if (!(text == "深灰灵石8级"))
								{
									continue;
								}
							}
							goto IL_1703;
						}
						if (num3 <= 3812095847u)
						{
							if (num3 != 3700260643u)
							{
								if (num3 != 3812095847u || !(text == "盈绿灵石2级"))
								{
									continue;
								}
								goto IL_181a;
							}
							if (!(text == "进击幻彩灵石1级"))
							{
								continue;
							}
							goto IL_1911;
						}
						if (num3 == 3885010211u)
						{
							if (!(text == "纯紫灵石5级"))
							{
								continue;
							}
							goto IL_17bb;
						}
						if (num3 == 3922679306u)
						{
							if (!(text == "新阳灵石10级"))
							{
								continue;
							}
							goto IL_1927;
						}
						if (num3 != 3968584065u || !(text == "进击幻彩灵石7级"))
						{
							continue;
						}
					}
					goto IL_186d;
					IL_181a:
					num2 += 20;
					continue;
					IL_1927:
					num2 += 100;
					continue;
					IL_1703:
					num2 += 80;
					continue;
					IL_1889:
					num2 += 90;
					continue;
					IL_18c5:
					num2 += 40;
					continue;
					IL_18de:
					num2 += 60;
					continue;
					IL_193d:
					num2 += 30;
					continue;
					IL_17bb:
					num2 += 50;
					continue;
					IL_1911:
					num2 += 10;
					continue;
					IL_186d:
					num2 += 70;
				}
				int num4;
				num4 = this.随机属性.Sum((随机属性 x) => x.战力加成);
				return this.装备模板.基础战力 + num + num4 + num2 + this.精炼战力;
			}
		}

		public int 修理费用
		{
			get
			{
				int num;
				num = base.最大持久.V - base.当前持久.V;
				return (int)((decimal)((游戏装备)base.对应模板.V).修理花费 / ((decimal)((游戏装备)base.对应模板.V).物品持久 * 1000m) * (decimal)num);
			}
		}

		public int 特修费用
		{
			get
			{
				decimal num;
				num = (decimal)base.最大持久.V - (decimal)base.当前持久.V;
				return (int)((decimal)((游戏装备)base.对应模板.V).特修花费 / ((decimal)((游戏装备)base.对应模板.V).物品持久 * 1000m) * num * Settings.装备特修折扣 * 1.15m);
			}
		}

		public int 战神油数 => Math.Max(1, (base.最大持久.V - base.当前持久.V) / 1000) * ((base.对应模板.V.物品分类 != 物品使用分类.衣服 && base.对应模板.V.物品分类 != 物品使用分类.武器) ? 1 : 2);

		public int 需要攻击 => ((游戏装备)base.物品模板).需要攻击;

		public int 需要魔法 => ((游戏装备)base.物品模板).需要魔法;

		public int 需要道术 => ((游戏装备)base.物品模板).需要道术;

		public int 需要刺术 => ((游戏装备)base.物品模板).需要刺术;

		public int 需要弓术 => ((游戏装备)base.物品模板).需要弓术;

		public string 装备名字 => base.物品模板.物品名字;

		public bool 禁止卸下 => ((游戏装备)base.对应模板.V).禁止卸下;

		public bool 能否修理
		{
			get
			{
				if (base.持久类型 == 物品持久分类.装备)
				{
					return this.装备模板.能否修理;
				}
				return false;
			}
		}

		public int 传承材料 => base.物品编号 switch
		{
			99900022 => 21001, 
			99900023 => 21002, 
			99900024 => 21003, 
			99900025 => 21001, 
			99900026 => 21001, 
			99900027 => 21003, 
			99900028 => 21002, 
			99900029 => 21002, 
			99900030 => 21001, 
			99900031 => 21003, 
			99900032 => 21001, 
			99900033 => 21002, 
			99900037 => 21001, 
			99900038 => 21003, 
			99900039 => 21002, 
			99900044 => 21003, 
			99900045 => 21001, 
			99900046 => 21002, 
			99900047 => 21003, 
			99900048 => 21001, 
			99900049 => 21003, 
			99900050 => 21002, 
			99900055 => 21004, 
			99900056 => 21004, 
			99900057 => 21004, 
			99900058 => 21004, 
			99900059 => 21004, 
			99900060 => 21004, 
			99900061 => 21004, 
			99900062 => 21004, 
			99900063 => 21002, 
			99900064 => 21003, 
			99900074 => 21005, 
			99900076 => 21005, 
			99900077 => 21005, 
			99900078 => 21005, 
			99900079 => 21005, 
			99900080 => 21005, 
			99900081 => 21005, 
			99900082 => 21005, 
			99900104 => 21006, 
			99900105 => 21006, 
			99900106 => 21006, 
			99900107 => 21006, 
			99900108 => 21006, 
			99900109 => 21006, 
			99900110 => 21006, 
			99900111 => 21006, 
			_ => 0, 
		};

		public int 圣伤最小 => this.神圣伤害.V switch
		{
			1 => 0, 
			2 => 0, 
			3 => 1, 
			4 => 1, 
			5 => 1, 
			6 => 2, 
			7 => 2, 
			8 => 2, 
			9 => 3, 
			10 => 3, 
			11 => 3, 
			12 => 4, 
			13 => 4, 
			14 => 4, 
			15 => 5, 
			16 => 5, 
			17 => 5, 
			18 => 6, 
			19 => 6, 
			20 => 6, 
			21 => 7, 
			22 => 7, 
			23 => 7, 
			24 => 8, 
			25 => 8, 
			26 => 8, 
			27 => 9, 
			28 => 9, 
			29 => 9, 
			30 => 10, 
			31 => 10, 
			32 => 10, 
			33 => 11, 
			34 => 11, 
			35 => 11, 
			36 => 12, 
			37 => 12, 
			38 => 12, 
			39 => 13, 
			40 => 13, 
			41 => 13, 
			42 => 14, 
			43 => 14, 
			44 => 14, 
			45 => 15, 
			46 => 15, 
			47 => 15, 
			48 => 16, 
			49 => 16, 
			50 => 16, 
			51 => 17, 
			52 => 17, 
			53 => 17, 
			54 => 18, 
			55 => 18, 
			56 => 18, 
			57 => 19, 
			58 => 19, 
			59 => 19, 
			60 => 20, 
			_ => 0, 
		};

		public int 圣伤最大 => this.神圣伤害.V switch
		{
			1 => 1, 
			2 => 2, 
			3 => 2, 
			4 => 3, 
			5 => 4, 
			6 => 4, 
			7 => 5, 
			8 => 6, 
			9 => 6, 
			10 => 7, 
			11 => 8, 
			12 => 8, 
			13 => 9, 
			14 => 10, 
			15 => 10, 
			16 => 11, 
			17 => 12, 
			18 => 12, 
			19 => 13, 
			20 => 14, 
			21 => 14, 
			22 => 15, 
			23 => 16, 
			24 => 16, 
			25 => 17, 
			26 => 18, 
			27 => 18, 
			28 => 19, 
			29 => 20, 
			30 => 20, 
			31 => 21, 
			32 => 22, 
			33 => 22, 
			34 => 23, 
			35 => 24, 
			36 => 24, 
			37 => 25, 
			38 => 26, 
			39 => 26, 
			40 => 27, 
			41 => 28, 
			42 => 28, 
			43 => 29, 
			44 => 30, 
			45 => 30, 
			46 => 31, 
			47 => 32, 
			48 => 32, 
			49 => 33, 
			50 => 34, 
			51 => 34, 
			52 => 35, 
			53 => 36, 
			54 => 36, 
			55 => 37, 
			56 => 38, 
			57 => 38, 
			58 => 39, 
			59 => 40, 
			60 => 40, 
			_ => 0, 
		};

		public string 属性描述
		{
			get
			{
				string text;
				text = "";
				Dictionary<游戏对象属性, int> dictionary;
				dictionary = new Dictionary<游戏对象属性, int>();
				foreach (随机属性 item in this.随机属性)
				{
					dictionary[item.对应属性] = item.属性数值;
				}
				if (dictionary.ContainsKey(游戏对象属性.最小攻击) || dictionary.ContainsKey(游戏对象属性.最大攻击))
				{
					text += $"\n攻击{(dictionary.TryGetValue(游戏对象属性.最小攻击, out var value) ? value : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大攻击, out var value2) ? value2 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小魔法) || dictionary.ContainsKey(游戏对象属性.最大魔法))
				{
					text += $"\n魔法{(dictionary.TryGetValue(游戏对象属性.最小魔法, out var value3) ? value3 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大魔法, out var value4) ? value4 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小道术) || dictionary.ContainsKey(游戏对象属性.最大道术))
				{
					text += $"\n道术{(dictionary.TryGetValue(游戏对象属性.最小道术, out var value5) ? value5 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大道术, out var value6) ? value6 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小刺术) || dictionary.ContainsKey(游戏对象属性.最大刺术))
				{
					text += $"\n刺术{(dictionary.TryGetValue(游戏对象属性.最小刺术, out var value7) ? value7 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大刺术, out var value8) ? value8 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小弓术) || dictionary.ContainsKey(游戏对象属性.最大弓术))
				{
					text += $"\n弓术{(dictionary.TryGetValue(游戏对象属性.最小弓术, out var value9) ? value9 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大弓术, out var value10) ? value10 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小防御) || dictionary.ContainsKey(游戏对象属性.最大防御))
				{
					text += $"\n防御{(dictionary.TryGetValue(游戏对象属性.最小防御, out var value11) ? value11 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大防御, out var value12) ? value12 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最小魔防) || dictionary.ContainsKey(游戏对象属性.最大魔防))
				{
					text += $"\n魔防{(dictionary.TryGetValue(游戏对象属性.最小魔防, out var value13) ? value13 : 0)}-{(dictionary.TryGetValue(游戏对象属性.最大魔防, out var value14) ? value14 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.物理准确))
				{
					text += $"\n准确度{(dictionary.TryGetValue(游戏对象属性.物理准确, out var value15) ? value15 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.物理敏捷))
				{
					text += $"\n敏捷度{(dictionary.TryGetValue(游戏对象属性.物理敏捷, out var value16) ? value16 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最大体力))
				{
					text += $"\n体力值{(dictionary.TryGetValue(游戏对象属性.最大体力, out var value17) ? value17 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.最大魔力))
				{
					text += $"\n法力值{(dictionary.TryGetValue(游戏对象属性.最大魔力, out var value18) ? value18 : 0)}";
				}
				if (dictionary.ContainsKey(游戏对象属性.魔法闪避))
				{
					text += $"\n魔法闪避{(dictionary.TryGetValue(游戏对象属性.魔法闪避, out var value19) ? value19 : 0) / 100}%";
				}
				if (dictionary.ContainsKey(游戏对象属性.中毒躲避))
				{
					text += $"\n中毒躲避{(dictionary.TryGetValue(游戏对象属性.中毒躲避, out var value20) ? value20 : 0) / 100}%";
				}
				if (dictionary.ContainsKey(游戏对象属性.幸运等级))
				{
					text += $"\n幸运+{(dictionary.TryGetValue(游戏对象属性.幸运等级, out var value21) ? value21 : 0)}";
				}
				return text;
			}
		}

		public 铭文技能 第一铭文
		{
			get
			{
				if (this.当前铭栏.V == 0)
				{
					return this.铭文技能[0];
				}
				return this.铭文技能[2];
			}
			set
			{
				if (this.当前铭栏.V == 0)
				{
					this.铭文技能[0] = value;
				}
				else
				{
					this.铭文技能[2] = value;
				}
			}
		}

		public 铭文技能 第二铭文
		{
			get
			{
				if (this.当前铭栏.V == 0)
				{
					return this.铭文技能[1];
				}
				return this.铭文技能[3];
			}
			set
			{
				if (this.当前铭栏.V == 0)
				{
					this.铭文技能[1] = value;
				}
				else
				{
					this.铭文技能[3] = value;
				}
			}
		}

		public 铭文技能 最优铭文
		{
			get
			{
				if (this.当前铭栏.V == 0)
				{
					if (this.铭文技能[0].铭文品质 < this.铭文技能[1].铭文品质)
					{
						return this.铭文技能[1];
					}
					return this.铭文技能[0];
				}
				if (this.铭文技能[2].铭文品质 < this.铭文技能[3].铭文品质)
				{
					return this.铭文技能[3];
				}
				return this.铭文技能[2];
			}
			set
			{
				if (this.当前铭栏.V == 0)
				{
					if (this.铭文技能[0].铭文品质 >= this.铭文技能[1].铭文品质)
					{
						this.铭文技能[0] = value;
					}
					else
					{
						this.铭文技能[1] = value;
					}
				}
				else if (this.铭文技能[2].铭文品质 >= this.铭文技能[3].铭文品质)
				{
					this.铭文技能[2] = value;
				}
				else
				{
					this.铭文技能[3] = value;
				}
			}
		}

		public 铭文技能 最差铭文
		{
			get
			{
				if (this.当前铭栏.V == 0)
				{
					if (this.铭文技能[0].铭文品质 >= this.铭文技能[1].铭文品质)
					{
						return this.铭文技能[1];
					}
					return this.铭文技能[0];
				}
				if (this.铭文技能[2].铭文品质 >= this.铭文技能[3].铭文品质)
				{
					return this.铭文技能[3];
				}
				return this.铭文技能[2];
			}
			set
			{
				if (this.当前铭栏.V == 0)
				{
					if (this.铭文技能[0].铭文品质 < this.铭文技能[1].铭文品质)
					{
						this.铭文技能[0] = value;
					}
					else
					{
						this.铭文技能[1] = value;
					}
				}
				else if (this.铭文技能[2].铭文品质 < this.铭文技能[3].铭文品质)
				{
					this.铭文技能[2] = value;
				}
				else
				{
					this.铭文技能[3] = value;
				}
			}
		}

		public int 双铭文点
		{
			get
			{
				if (this.当前铭栏.V == 0)
				{
					return this.洗练数一.V;
				}
				return this.洗练数二.V;
			}
			set
			{
				if (this.当前铭栏.V == 0)
				{
					this.洗练数一.V = value;
				}
				else
				{
					this.洗练数二.V = value;
				}
			}
		}

		public Dictionary<游戏对象属性, int> 装备属性
		{
			get
			{
				Dictionary<游戏对象属性, int> dictionary;
				dictionary = new Dictionary<游戏对象属性, int>();
				if (this.装备模板.最小攻击 != 0)
				{
					dictionary[游戏对象属性.最小攻击] = this.装备模板.最小攻击;
				}
				if (this.装备模板.最大攻击 != 0)
				{
					dictionary[游戏对象属性.最大攻击] = this.装备模板.最大攻击;
				}
				if (this.装备模板.最小魔法 != 0)
				{
					dictionary[游戏对象属性.最小魔法] = this.装备模板.最小魔法;
				}
				if (this.装备模板.最大魔法 != 0)
				{
					dictionary[游戏对象属性.最大魔法] = this.装备模板.最大魔法;
				}
				if (this.装备模板.最小道术 != 0)
				{
					dictionary[游戏对象属性.最小道术] = this.装备模板.最小道术;
				}
				if (this.装备模板.最大道术 != 0)
				{
					dictionary[游戏对象属性.最大道术] = this.装备模板.最大道术;
				}
				if (this.装备模板.最小刺术 != 0)
				{
					dictionary[游戏对象属性.最小刺术] = this.装备模板.最小刺术;
				}
				if (this.装备模板.最大刺术 != 0)
				{
					dictionary[游戏对象属性.最大刺术] = this.装备模板.最大刺术;
				}
				if (this.装备模板.最小弓术 != 0)
				{
					dictionary[游戏对象属性.最小弓术] = this.装备模板.最小弓术;
				}
				if (this.装备模板.最大弓术 != 0)
				{
					dictionary[游戏对象属性.最大弓术] = this.装备模板.最大弓术;
				}
				if (this.装备模板.最小防御 != 0)
				{
					dictionary[游戏对象属性.最小防御] = this.装备模板.最小防御;
				}
				if (this.装备模板.最大防御 != 0)
				{
					dictionary[游戏对象属性.最大防御] = this.装备模板.最大防御;
				}
				if (this.装备模板.最小魔防 != 0)
				{
					dictionary[游戏对象属性.最小魔防] = this.装备模板.最小魔防;
				}
				if (this.装备模板.最大魔防 != 0)
				{
					dictionary[游戏对象属性.最大魔防] = this.装备模板.最大魔防;
				}
				if (this.装备模板.最大体力 != 0)
				{
					dictionary[游戏对象属性.最大体力] = this.装备模板.最大体力;
				}
				if (this.装备模板.最大魔力 != 0)
				{
					dictionary[游戏对象属性.最大魔力] = this.装备模板.最大魔力;
				}
				if (this.装备模板.攻击速度 != 0)
				{
					dictionary[游戏对象属性.攻击速度] = this.装备模板.攻击速度;
				}
				if (this.装备模板.魔法闪避 != 0)
				{
					dictionary[游戏对象属性.魔法闪避] = this.装备模板.魔法闪避;
				}
				if (this.装备模板.物理准确 != 0)
				{
					dictionary[游戏对象属性.物理准确] = this.装备模板.物理准确;
				}
				if (this.装备模板.物理敏捷 != 0)
				{
					dictionary[游戏对象属性.物理敏捷] = this.装备模板.物理敏捷;
				}
				if (this.幸运等级.V != 0)
				{
					dictionary[游戏对象属性.幸运等级] = (dictionary.ContainsKey(游戏对象属性.幸运等级) ? (dictionary[游戏对象属性.幸运等级] + this.幸运等级.V) : this.幸运等级.V);
				}
				if (this.升级次数.V != 0)
				{
					int num;
					num = 0;
					switch (this.装备模板.物品分类)
					{
					case 物品使用分类.衣服:
						switch (this.装备模板.装备套装)
						{
						case 游戏装备套装.祖玛装备:
							num = 4;
							break;
						case 游戏装备套装.赤月装备:
							num = 6;
							break;
						case 游戏装备套装.魔龙装备:
							num = 8;
							break;
						case 游戏装备套装.苍月装备:
							num = 11;
							break;
						case 游戏装备套装.星王装备:
							num = 13;
							break;
						case 游戏装备套装.神秘装备:
							num = 13;
							break;
						case 游戏装备套装.城主装备:
							num = 13;
							break;
						}
						dictionary[游戏对象属性.最大体力] = (dictionary.ContainsKey(游戏对象属性.最大体力) ? (dictionary[游戏对象属性.最大体力] + num * this.升级次数.V) : (num * this.升级次数.V));
						break;
					case 物品使用分类.披风:
						dictionary[游戏对象属性.最小防御] = (dictionary.ContainsKey(游戏对象属性.最小防御) ? (dictionary[游戏对象属性.最小防御] + Math.Max(0, this.升级次数.V - 6)) : Math.Max(0, this.升级次数.V - 7));
						dictionary[游戏对象属性.最大防御] = (dictionary.ContainsKey(游戏对象属性.最大防御) ? (dictionary[游戏对象属性.最大防御] + this.升级次数.V) : this.升级次数.V);
						break;
					case 物品使用分类.护肩:
					{
						int num3;
						num3 = 0;
						int[] array4;
						array4 = new int[10] { 0, 5, 10, 15, 20, 30, 40, 55, 70, 85 };
						int[] array5;
						array5 = new int[10] { 0, 10, 20, 30, 45, 60, 80, 100, 120, 140 };
						int[] array6;
						array6 = new int[10] { 0, 20, 40, 60, 80, 100, 120, 150, 190, 240 };
						switch (this.装备模板.装备套装)
						{
						case 游戏装备套装.祖玛装备:
							num3 = array4[this.升级次数.V];
							break;
						case 游戏装备套装.赤月装备:
							num3 = array5[this.升级次数.V];
							break;
						case 游戏装备套装.魔龙装备:
							num3 = array6[this.升级次数.V];
							break;
						}
						dictionary[游戏对象属性.物理抗性] = (dictionary.ContainsKey(游戏对象属性.物理抗性) ? (dictionary[游戏对象属性.物理抗性] + num3) : num3);
						break;
					}
					case 物品使用分类.护腕:
					{
						int num2;
						num2 = 0;
						int[] array;
						array = new int[10] { 0, 5, 10, 15, 20, 30, 40, 55, 70, 85 };
						int[] array2;
						array2 = new int[10] { 0, 10, 20, 30, 45, 60, 80, 100, 120, 140 };
						int[] array3;
						array3 = new int[10] { 0, 20, 40, 60, 80, 100, 120, 150, 190, 240 };
						switch (this.装备模板.装备套装)
						{
						case 游戏装备套装.祖玛装备:
							num2 = array[this.升级次数.V];
							break;
						case 游戏装备套装.赤月装备:
							num2 = array2[this.升级次数.V];
							break;
						case 游戏装备套装.魔龙装备:
							num2 = array3[this.升级次数.V];
							break;
						}
						dictionary[游戏对象属性.魔法抗性] = (dictionary.ContainsKey(游戏对象属性.魔法抗性) ? (dictionary[游戏对象属性.魔法抗性] + num2) : num2);
						break;
					}
					case 物品使用分类.腰带:
					case 物品使用分类.鞋子:
					case 物品使用分类.头盔:
						switch (this.装备模板.装备套装)
						{
						case 游戏装备套装.祖玛装备:
							num = 2;
							break;
						case 游戏装备套装.赤月装备:
							num = 4;
							break;
						case 游戏装备套装.魔龙装备:
							num = 5;
							break;
						case 游戏装备套装.苍月装备:
							num = 7;
							break;
						case 游戏装备套装.星王装备:
							num = 11;
							break;
						case 游戏装备套装.神秘装备:
							num = 9;
							break;
						case 游戏装备套装.城主装备:
							num = 9;
							break;
						}
						dictionary[游戏对象属性.最大体力] = (dictionary.ContainsKey(游戏对象属性.最大体力) ? (dictionary[游戏对象属性.最大体力] + num * this.升级次数.V) : (num * this.升级次数.V));
						break;
					case 物品使用分类.玉佩:
						dictionary[游戏对象属性.最小魔防] = (dictionary.ContainsKey(游戏对象属性.最小魔防) ? (dictionary[游戏对象属性.最小魔防] + Math.Max(0, this.升级次数.V - 6)) : Math.Max(0, this.升级次数.V - 7));
						dictionary[游戏对象属性.最大魔防] = (dictionary.ContainsKey(游戏对象属性.最大魔防) ? (dictionary[游戏对象属性.最大魔防] + this.升级次数.V) : this.升级次数.V);
						break;
					}
				}
				if (this.升级攻击.V != 0)
				{
					dictionary[游戏对象属性.最大攻击] = (dictionary.ContainsKey(游戏对象属性.最大攻击) ? (dictionary[游戏对象属性.最大攻击] + this.升级攻击.V) : this.升级攻击.V);
				}
				if (this.升级魔法.V != 0)
				{
					dictionary[游戏对象属性.最大魔法] = (dictionary.ContainsKey(游戏对象属性.最大魔法) ? (dictionary[游戏对象属性.最大魔法] + this.升级魔法.V) : this.升级魔法.V);
				}
				if (this.升级道术.V != 0)
				{
					dictionary[游戏对象属性.最大道术] = (dictionary.ContainsKey(游戏对象属性.最大道术) ? (dictionary[游戏对象属性.最大道术] + this.升级道术.V) : this.升级道术.V);
				}
				if (this.升级刺术.V != 0)
				{
					dictionary[游戏对象属性.最大刺术] = (dictionary.ContainsKey(游戏对象属性.最大刺术) ? (dictionary[游戏对象属性.最大刺术] + this.升级刺术.V) : this.升级刺术.V);
				}
				if (this.升级弓术.V != 0)
				{
					dictionary[游戏对象属性.最大弓术] = (dictionary.ContainsKey(游戏对象属性.最大弓术) ? (dictionary[游戏对象属性.最大弓术] + this.升级弓术.V) : this.升级弓术.V);
				}
				if (this.神圣伤害.V != 0)
				{
					dictionary[游戏对象属性.最小圣伤] = this.圣伤最小;
					dictionary[游戏对象属性.最大圣伤] = this.圣伤最大;
				}
				foreach (随机属性 item in this.随机属性.ToList())
				{
					dictionary[item.对应属性] = (dictionary.ContainsKey(item.对应属性) ? (dictionary[item.对应属性] + item.属性数值) : item.属性数值);
				}
				using IEnumerator<游戏物品> enumerator2 = this.镶嵌灵石.Values.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					switch (enumerator2.Current.物品编号)
					{
					case 10220:
					case 10221:
					case 10222:
					case 10223:
					case 10224:
					case 10225:
					case 10226:
					case 10227:
					case 10228:
					case 10229:
						dictionary[游戏对象属性.最大防御] = (dictionary.ContainsKey(游戏对象属性.最大防御) ? (dictionary[游戏对象属性.最大防御] + enumerator2.Current.物品编号 - 10220 + 1) : (enumerator2.Current.物品编号 - 10220 + 1));
						break;
					case 10210:
					case 10211:
					case 10212:
					case 10213:
					case 10214:
					case 10215:
					case 10216:
					case 10217:
					case 10218:
					case 10219:
						dictionary[游戏对象属性.暴击概率] = (dictionary.ContainsKey(游戏对象属性.暴击概率) ? (dictionary[游戏对象属性.暴击概率] + (enumerator2.Current.物品编号 - 10210 + 1) * 100) : ((enumerator2.Current.物品编号 - 10210 + 1) * 100));
						break;
					case 10110:
					case 10111:
					case 10112:
					case 10113:
					case 10114:
					case 10115:
					case 10116:
					case 10117:
					case 10118:
					case 10119:
						dictionary[游戏对象属性.最大道术] = (dictionary.ContainsKey(游戏对象属性.最大道术) ? (dictionary[游戏对象属性.最大道术] + enumerator2.Current.物品编号 - 10110 + 1) : (enumerator2.Current.物品编号 - 10110 + 1));
						break;
					case 10120:
					case 10121:
					case 10122:
					case 10123:
					case 10124:
					case 10125:
					case 10126:
					case 10127:
					case 10128:
					case 10129:
						dictionary[游戏对象属性.最大体力] = (dictionary.ContainsKey(游戏对象属性.最大体力) ? (dictionary[游戏对象属性.最大体力] + (enumerator2.Current.物品编号 - 10110 + 1) * 5) : ((enumerator2.Current.物品编号 - 10120 + 1) * 5));
						break;
					case 10130:
						dictionary[游戏对象属性.最大道术] = (dictionary.ContainsKey(游戏对象属性.最大道术) ? (dictionary[游戏对象属性.最大道术] + 3) : 3);
						break;
					case 10510:
					case 10511:
					case 10512:
					case 10513:
					case 10514:
					case 10515:
					case 10516:
					case 10517:
					case 10518:
					case 10519:
						dictionary[游戏对象属性.减暴击] = (dictionary.ContainsKey(游戏对象属性.减暴击) ? (dictionary[游戏对象属性.减暴击] + (enumerator2.Current.物品编号 - 10510 + 1) * 100) : ((enumerator2.Current.物品编号 - 10510 + 1) * 100));
						break;
					case 10410:
					case 10411:
					case 10412:
					case 10413:
					case 10414:
					case 10415:
					case 10416:
					case 10417:
					case 10418:
					case 10419:
						dictionary[游戏对象属性.破魔防] = (dictionary.ContainsKey(游戏对象属性.破魔防) ? (dictionary[游戏对象属性.破魔防] + enumerator2.Current.物品编号 - 10410 + 1) : (enumerator2.Current.物品编号 - 10410 + 1));
						break;
					case 10420:
					case 10421:
					case 10422:
					case 10423:
					case 10424:
					case 10425:
					case 10426:
					case 10427:
					case 10428:
					case 10429:
						dictionary[游戏对象属性.最大攻击] = (dictionary.ContainsKey(游戏对象属性.最大攻击) ? (dictionary[游戏对象属性.最大攻击] + enumerator2.Current.物品编号 - 10420 + 1) : (enumerator2.Current.物品编号 - 10420 + 1));
						break;
					case 10430:
						dictionary[游戏对象属性.最大攻击] = (dictionary.ContainsKey(游戏对象属性.最大攻击) ? (dictionary[游戏对象属性.最大攻击] + 3) : 3);
						break;
					case 10310:
					case 10311:
					case 10312:
					case 10313:
					case 10314:
					case 10315:
					case 10316:
					case 10317:
					case 10318:
					case 10319:
						dictionary[游戏对象属性.破物防] = (dictionary.ContainsKey(游戏对象属性.破物防) ? (dictionary[游戏对象属性.破物防] + enumerator2.Current.物品编号 - 10310 + 1) : (enumerator2.Current.物品编号 - 10310 + 1));
						break;
					case 10320:
					case 10321:
					case 10322:
					case 10323:
					case 10324:
					case 10325:
					case 10326:
					case 10327:
					case 10328:
					case 10329:
						dictionary[游戏对象属性.最大魔法] = (dictionary.ContainsKey(游戏对象属性.最大魔法) ? (dictionary[游戏对象属性.最大魔法] + enumerator2.Current.物品编号 - 10320 + 1) : (enumerator2.Current.物品编号 - 10320 + 1));
						break;
					case 10330:
						dictionary[游戏对象属性.最大魔法] = (dictionary.ContainsKey(游戏对象属性.最大魔法) ? (dictionary[游戏对象属性.最大魔法] + 3) : 3);
						break;
					case 10630:
						dictionary[游戏对象属性.最大刺术] = (dictionary.ContainsKey(游戏对象属性.最大刺术) ? (dictionary[游戏对象属性.最大刺术] + 3) : 3);
						break;
					case 10620:
					case 10621:
					case 10622:
					case 10623:
					case 10624:
					case 10625:
					case 10626:
					case 10627:
					case 10628:
					case 10629:
						dictionary[游戏对象属性.最大刺术] = (dictionary.ContainsKey(游戏对象属性.最大刺术) ? (dictionary[游戏对象属性.最大刺术] + enumerator2.Current.物品编号 - 10620 + 1) : (enumerator2.Current.物品编号 - 10620 + 1));
						break;
					case 10520:
					case 10521:
					case 10522:
					case 10523:
					case 10524:
					case 10525:
					case 10526:
					case 10527:
					case 10528:
					case 10529:
						dictionary[游戏对象属性.最大魔防] = (dictionary.ContainsKey(游戏对象属性.最大魔防) ? (dictionary[游戏对象属性.最大魔防] + enumerator2.Current.物品编号 - 10520 + 1) : (enumerator2.Current.物品编号 - 10520 + 1));
						break;
					case 10730:
						dictionary[游戏对象属性.最大弓术] = (dictionary.ContainsKey(游戏对象属性.最大弓术) ? (dictionary[游戏对象属性.最大弓术] + 3) : 3);
						break;
					case 10720:
					case 10721:
					case 10722:
					case 10723:
					case 10724:
					case 10725:
					case 10726:
					case 10727:
					case 10728:
					case 10729:
						dictionary[游戏对象属性.最大弓术] = (dictionary.ContainsKey(游戏对象属性.最大弓术) ? (dictionary[游戏对象属性.最大弓术] + enumerator2.Current.物品编号 - 10720 + 1) : (enumerator2.Current.物品编号 - 10720 + 1));
						break;
					case 10830:
						dictionary[游戏对象属性.最大攻击] = (dictionary.ContainsKey(游戏对象属性.最大攻击) ? (dictionary[游戏对象属性.最大攻击] + 3) : 3);
						break;
					case 10820:
					case 10821:
					case 10822:
					case 10823:
					case 10824:
					case 10825:
					case 10826:
					case 10827:
					case 10828:
					case 10829:
						dictionary[游戏对象属性.最大攻击] = (dictionary.ContainsKey(游戏对象属性.最大攻击) ? (dictionary[游戏对象属性.最大攻击] + enumerator2.Current.物品编号 - 10820 + 1) : (enumerator2.Current.物品编号 - 10820 + 1));
						break;
					}
				}
				if (this.精炼值一.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值一.V, out var value))
				{
					dictionary[value.属性类型] = (dictionary.ContainsKey(value.属性类型) ? (dictionary[value.属性类型] + value.属性数值) : value.属性数值);
				}
				if (this.精炼值二.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值二.V, out var value2))
				{
					dictionary[value2.属性类型] = (dictionary.ContainsKey(value2.属性类型) ? (dictionary[value2.属性类型] + value2.属性数值) : value2.属性数值);
				}
				if (this.精炼值三.V > 0 && 装备精炼.属性表.TryGetValue(this.精炼值三.V, out var value3))
				{
					dictionary[value3.属性类型] = (dictionary.ContainsKey(value3.属性类型) ? (dictionary[value3.属性类型] + value3.属性数值) : value3.属性数值);
				}
				return dictionary;
			}
		}

		public int 重铸所需灵气
		{
			get
			{
				switch (base.物品类型)
				{
				default:
					return 0;
				case 物品使用分类.武器:
					return 112001;
				case 物品使用分类.衣服:
				case 物品使用分类.披风:
				case 物品使用分类.腰带:
				case 物品使用分类.鞋子:
				case 物品使用分类.护肩:
				case 物品使用分类.护腕:
				case 物品使用分类.头盔:
					return 112003;
				case 物品使用分类.项链:
				case 物品使用分类.戒指:
				case 物品使用分类.手镯:
				case 物品使用分类.勋章:
				case 物品使用分类.玉佩:
					return 112002;
				}
			}
		}

		public 装备数据()
		{
		}

		public 装备数据(游戏装备 模板, 角色数据 来源, byte 容器, byte 位置, bool 随机生成 = false, bool 绑定 = false, string 掉落怪物 = "")
		{
			base.绑定物品.V = 绑定;
			base.对应模板.V = 模板;
			base.生成来源.V = 来源;
			base.物品容器.V = 容器;
			base.物品位置.V = 位置;
			base.生成时间.V = 主程.当前时间;
			数据监视器<string> obj;
			obj = base.掉落地图;
			object obj2;
			if (来源 == null)
			{
				obj2 = null;
			}
			else
			{
				客户网络 网络连接;
				网络连接 = 来源.网络连接;
				if (网络连接 == null)
				{
					obj2 = null;
				}
				else
				{
					玩家实例 绑定角色;
					绑定角色 = 网络连接.绑定角色;
					if (绑定角色 == null)
					{
						obj2 = null;
					}
					else
					{
						地图实例 当前地图;
						当前地图 = 绑定角色.当前地图;
						if (当前地图 == null)
						{
							obj2 = null;
						}
						else
						{
							游戏地图 地图模板;
							地图模板 = 当前地图.地图模板;
							if (地图模板 == null)
							{
								obj2 = null;
							}
							else
							{
								obj2 = 地图模板.地图名字;
								if (obj2 != null)
								{
									goto IL_00a0;
								}
							}
						}
					}
				}
			}
			obj2 = "";
			goto IL_00a0;
			IL_00a0:
			obj.V = (string)obj2;
			base.掉落怪物.V = 掉落怪物;
			this.物品状态.V = 1;
			base.最大持久.V = ((模板.持久类型 == 物品持久分类.装备) ? (模板.物品持久 * 1000) : 模板.物品持久);
			base.当前持久.V = ((!随机生成 || 模板.持久类型 != 物品持久分类.装备) ? base.最大持久.V : 主程.随机数.Next(0, base.最大持久.V));
			if (随机生成 && 模板.持久类型 == 物品持久分类.装备 && 模板.特殊属性一 == 0 && 模板.特殊属性二 == 0 && 模板.特殊属性三 == 0 && 模板.特殊属性四 == 0)
			{
				this.随机属性.SetValue(游戏服务器.模板类.装备属性.生成属性(base.物品类型));
			}
			if (游戏服务器.模板类.随机属性.数据表.TryGetValue(模板.特殊属性一, out var value))
			{
				this.随机属性.Add(value);
			}
			if (游戏服务器.模板类.随机属性.数据表.TryGetValue(模板.特殊属性二, out var value2))
			{
				this.随机属性.Add(value2);
			}
			if (游戏服务器.模板类.随机属性.数据表.TryGetValue(模板.特殊属性三, out var value3))
			{
				this.随机属性.Add(value3);
			}
			if (游戏服务器.模板类.随机属性.数据表.TryGetValue(模板.特殊属性四, out var value4))
			{
				this.随机属性.Add(value4);
			}
			if (Settings.开启任务系统)
			{
				CharacterQuest[] inProgressQuests;
				inProgressQuests = 来源.GetInProgressQuests();
				foreach (CharacterQuest characterQuest in inProgressQuests)
				{
					CharacterQuestMission[] missionsOfType;
					missionsOfType = characterQuest.GetMissionsOfType(QuestMissionType.AdquireItem);
					bool flag;
					flag = false;
					CharacterQuestMission[] array;
					array = missionsOfType;
					foreach (CharacterQuestMission characterQuestMission in array)
					{
						if (!(characterQuestMission.CompletedDate.V != DateTime.MinValue) && characterQuestMission.Info.V.Id == 模板.物品编号)
						{
							characterQuestMission.Count.V = (byte)(characterQuestMission.Count.V + 1);
							flag = true;
						}
					}
					if (flag)
					{
						来源.网络连接?.绑定角色.UpdateQuestProgress(characterQuest);
					}
				}
			}
			游戏数据网关.装备数据表.添加数据(this, 分配索引: true);
		}

		public override byte[] 字节描述()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(物品数据.数据版本);
			binaryWriter.Write(base.生成来源.V?.数据索引.V ?? 0);
			binaryWriter.Write(计算类.时间转换(base.生成时间.V));
			binaryWriter.Write(base.对应模板.V.物品编号);
			binaryWriter.Write(base.物品容器.V);
			binaryWriter.Write(base.物品位置.V);
			binaryWriter.Write(base.当前持久.V);
			binaryWriter.Write(base.最大持久.V);
			binaryWriter.Write((byte)(base.是否绑定 ? 10u : 0u));
			int num;
			num = 256;
			num = 0x100 | this.当前铭栏.V;
			if (this.双铭文栏.V)
			{
				num |= 0x200;
			}
			binaryWriter.Write((short)num);
			int num2;
			num2 = 524288;
			if (this.物品状态.V != 1)
			{
				num2 |= 1;
			}
			else if (this.随机属性.Count != 0)
			{
				num2 |= 1;
			}
			else if (this.神圣伤害.V != 0)
			{
				num2 |= 1;
			}
			else if (this.开启精炼.V != 0)
			{
				num2 |= 1;
			}
			else if (this.升级次数.V != 0)
			{
				num2 |= 1;
			}
			if (this.随机属性.Count >= 1)
			{
				num2 |= 2;
			}
			if (this.随机属性.Count >= 2)
			{
				num2 |= 4;
			}
			if (this.随机属性.Count >= 3)
			{
				num2 |= 8;
			}
			if (this.随机属性.Count >= 4)
			{
				num2 |= 0x10;
			}
			if (this.幸运等级.V != 0)
			{
				num2 |= 0x800;
			}
			if (this.升级次数.V != 0 || this.失败次数.V != 0 || this.铸魂次数.V != 0)
			{
				num2 |= 0x1000;
			}
			if (this.孔洞颜色.Count != 0)
			{
				num2 |= 0x2000;
			}
			if (this.镶嵌灵石[0] != null)
			{
				num2 |= 0x4000;
			}
			if (this.镶嵌灵石[1] != null)
			{
				num2 |= 0x8000;
			}
			if (this.镶嵌灵石[2] != null)
			{
				num2 |= 0x10000;
			}
			if (this.镶嵌灵石[3] != null)
			{
				num2 |= 0x20000;
			}
			if (this.神圣伤害.V != 0)
			{
				num2 |= 0x400000;
			}
			else if (this.圣石数量.V != 0)
			{
				num2 |= 0x400000;
			}
			if (this.祈祷次数.V != 0)
			{
				num2 |= 0x800000;
			}
			if (this.装备神佑.V)
			{
				num2 |= 0x2000000;
			}
			if (this.开启精炼.V != 0)
			{
				num2 |= 0x20000000;
			}
			binaryWriter.Write(num2);
			binaryWriter.Write(0);
			if (((uint)num2 & (true ? 1u : 0u)) != 0)
			{
				binaryWriter.Write(this.物品状态.V);
			}
			if (((uint)num2 & 2u) != 0)
			{
				binaryWriter.Write((ushort)this.随机属性[0].属性编号);
			}
			if (((uint)num2 & 4u) != 0)
			{
				binaryWriter.Write((ushort)this.随机属性[1].属性编号);
			}
			if (((uint)num2 & 8u) != 0)
			{
				binaryWriter.Write((ushort)this.随机属性[2].属性编号);
			}
			if (((uint)num2 & 0x10u) != 0)
			{
				binaryWriter.Write((ushort)this.随机属性[3].属性编号);
			}
			if (((uint)num & 0x100u) != 0)
			{
				int num3;
				num3 = 0;
				if (this.铭文技能[0] != null)
				{
					num3 |= 1;
				}
				if (this.铭文技能[1] != null)
				{
					num3 |= 2;
				}
				binaryWriter.Write((short)num3);
				binaryWriter.Write(this.洗练数一.V * 10000);
				if (((uint)num3 & (true ? 1u : 0u)) != 0)
				{
					binaryWriter.Write(this.铭文技能[0].铭文索引);
				}
				if (((uint)num3 & 2u) != 0)
				{
					binaryWriter.Write(this.铭文技能[1].铭文索引);
				}
			}
			if (((uint)num & 0x200u) != 0)
			{
				int num4;
				num4 = 0;
				if (this.铭文技能[2] != null)
				{
					num4 |= 1;
				}
				if (this.铭文技能[3] != null)
				{
					num4 |= 2;
				}
				binaryWriter.Write((short)num4);
				binaryWriter.Write(this.洗练数二.V * 10000);
				if (((uint)num4 & (true ? 1u : 0u)) != 0)
				{
					binaryWriter.Write(this.铭文技能[2].铭文索引);
				}
				if (((uint)num4 & 2u) != 0)
				{
					binaryWriter.Write(this.铭文技能[3].铭文索引);
				}
			}
			if (((uint)num2 & 0x800u) != 0)
			{
				binaryWriter.Write(this.幸运等级.V);
			}
			if (((uint)num2 & 0x1000u) != 0)
			{
				binaryWriter.Write(this.升级次数.V);
				binaryWriter.Write(this.失败次数.V);
				binaryWriter.Write(this.升级攻击.V);
				binaryWriter.Write(this.升级魔法.V);
				binaryWriter.Write(this.升级道术.V);
				binaryWriter.Write(this.升级刺术.V);
				binaryWriter.Write(this.升级弓术.V);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write(this.铸魂次数.V);
				binaryWriter.Write(this.灵魂绑定.V);
			}
			if (((uint)num2 & 0x2000u) != 0)
			{
				binaryWriter.Write(new byte[4]
				{
					(byte)this.孔洞颜色[0],
					(byte)this.孔洞颜色[1],
					(byte)this.孔洞颜色[2],
					(byte)this.孔洞颜色[3]
				});
			}
			if (((uint)num2 & 0x4000u) != 0)
			{
				binaryWriter.Write(this.镶嵌灵石[0].物品编号);
			}
			if (((uint)num2 & 0x8000u) != 0)
			{
				binaryWriter.Write(this.镶嵌灵石[1].物品编号);
			}
			if (((uint)num2 & 0x10000u) != 0)
			{
				binaryWriter.Write(this.镶嵌灵石[2].物品编号);
			}
			if (((uint)num2 & 0x20000u) != 0)
			{
				binaryWriter.Write(this.镶嵌灵石[3].物品编号);
			}
			if (((uint)num2 & 0x80000u) != 0)
			{
				binaryWriter.Write(base.上锁时间.V);
			}
			if (((uint)num2 & 0x100000u) != 0)
			{
				binaryWriter.Write(0);
			}
			if (((uint)num2 & 0x200000u) != 0)
			{
				binaryWriter.Write(0);
			}
			if (((uint)num2 & 0x400000u) != 0)
			{
				binaryWriter.Write(this.神圣伤害.V);
				binaryWriter.Write(this.圣石数量.V);
			}
			if (((uint)num2 & 0x800000u) != 0)
			{
				binaryWriter.Write((int)this.祈祷次数.V);
			}
			if (((uint)num2 & 0x20000000u) != 0)
			{
				binaryWriter.Write(this.开启精炼.V);
				binaryWriter.Write(this.精炼值一.V);
				binaryWriter.Write(this.精炼值二.V);
				binaryWriter.Write(this.精炼值三.V);
				binaryWriter.Write(this.精炼次数.V);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
				binaryWriter.Write((byte)0);
			}
			return memoryStream.ToArray();
		}
	}
}
