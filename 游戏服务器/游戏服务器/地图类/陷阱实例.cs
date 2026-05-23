using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using 游戏服务器.模板类;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public sealed class 陷阱实例 : 地图对象
	{
		public byte 陷阱等级;

		public ushort 陷阱编号;

		public DateTime 放置时间;

		public DateTime 消失时间;

		public DateTime 触发时间;

		public 地图对象 陷阱来源;

		public 地图对象 命中目标;

		public 技能陷阱 陷阱模板;

		public HashSet<地图对象> 被动触发列表;

		public byte 陷阱移动次数;

		public 游戏技能 被动触发技能;

		public 游戏技能 主动触发技能;

		public ushort 陷阱分组编号 => this.陷阱模板.分组编号;

		public ushort 主动触发间隔 => this.陷阱模板.主动触发间隔;

		public ushort 主动触发延迟 => this.陷阱模板.主动触发延迟;

		public ushort 陷阱剩余时间 => (ushort)Math.Ceiling((this.消失时间 - 主程.当前时间).TotalMilliseconds / 62.5);

		public Point 目的坐标
		{
			get
			{
				if (this.命中目标 != null)
				{
					return this.命中目标.当前坐标;
				}
				return Point.Empty;
			}
		}

		public ushort 目的高度
		{
			get
			{
				if (this.命中目标 != null)
				{
					return this.命中目标.当前高度;
				}
				return this.当前高度;
			}
		}

		public override 地图实例 当前地图
		{
			get
			{
				return base.当前地图;
			}
			set
			{
				if (this.当前地图 != value)
				{
					base.当前地图?.移除对象(this);
					base.当前地图 = value;
					base.当前地图.添加对象(this);
				}
			}
		}

		public override int 处理间隔 => 10;

		public override byte 当前等级
		{
			get
			{
				return this.陷阱来源.当前等级;
			}
			set
			{
				this.陷阱来源.当前等级 = value;
			}
		}

		public override bool 阻塞网格
		{
			get
			{
				return false;
			}
			set
			{
				base.阻塞网格 = value;
			}
		}

		public override bool 能被命中 => false;

		public override string 对象名字 => this.陷阱模板.陷阱名字;

		public override 游戏对象类型 对象类型 => 游戏对象类型.陷阱;

		public override 技能范围类型 对象体型 => this.陷阱模板.陷阱体型;

		public override Dictionary<游戏对象属性, int> 当前属性 => base.当前属性;

		public 陷阱实例(地图对象 来源, 技能陷阱 模板, 地图实例 地图, Point 坐标, 地图对象 目标 = null, 游戏方向 方向 = 游戏方向.左方)
		{
			this.陷阱来源 = 来源;
			this.命中目标 = 目标;
			this.陷阱模板 = 模板;
			this.当前地图 = 地图;
			this.当前坐标 = 坐标;
			this.行走时间 = 主程.当前时间;
			this.放置时间 = 主程.当前时间;
			this.陷阱编号 = 模板.陷阱编号;
			this.当前方向 = 方向;
			this.被动触发列表 = new HashSet<地图对象>();
			this.消失时间 = this.放置时间 + TimeSpan.FromMilliseconds(this.陷阱模板.陷阱持续时间);
			this.触发时间 = this.放置时间 + TimeSpan.FromMilliseconds((int)this.陷阱模板.主动触发延迟);
			if (来源 is 玩家实例 玩家实例2)
			{
				if (this.陷阱模板.绑定等级 != 0 && 玩家实例2.主体技能表.TryGetValue(this.陷阱模板.绑定等级, out var v))
				{
					this.陷阱等级 = v.技能等级.V;
				}
				if (this.陷阱模板.持续时间延长 && this.陷阱模板.技能等级延时)
				{
					this.消失时间 += TimeSpan.FromMilliseconds(this.陷阱等级 * this.陷阱模板.每级延长时间);
				}
				if (this.陷阱模板.持续时间延长 && this.陷阱模板.角色属性延时)
				{
					this.消失时间 += TimeSpan.FromMilliseconds((float)玩家实例2[this.陷阱模板.绑定角色属性] * this.陷阱模板.属性延时系数);
				}
				if (this.陷阱模板.持续时间延长 && this.陷阱模板.特定铭文延时 && 玩家实例2.主体技能表.TryGetValue((ushort)(this.陷阱模板.特定铭文技能 / 10), out var v2) && v2.铭文编号 == this.陷阱模板.特定铭文技能 % 10)
				{
					this.消失时间 += TimeSpan.FromMilliseconds(this.陷阱模板.铭文延长时间);
				}
			}
			this.主动触发技能 = ((this.陷阱模板.主动触发技能 == null || !游戏技能.数据表.ContainsKey(this.陷阱模板.主动触发技能)) ? null : 游戏技能.数据表[this.陷阱模板.主动触发技能]);
			this.被动触发技能 = ((this.陷阱模板.被动触发技能 == null || !游戏技能.数据表.ContainsKey(this.陷阱模板.被动触发技能)) ? null : 游戏技能.数据表[this.陷阱模板.被动触发技能]);
			this.地图编号 = ++地图处理网关.陷阱编号;
			base.绑定网格();
			base.更新邻居时处理();
			地图处理网关.添加地图对象(this);
			base.激活对象 = true;
			地图处理网关.添加激活对象(this);
		}

		public override void 处理对象数据()
		{
			if (主程.当前时间 < base.预约时间)
			{
				return;
			}
			if (主程.当前时间 > this.消失时间)
			{
				this.陷阱消失处理();
			}
			else
			{
				foreach (技能实例 item in base.技能任务.ToList())
				{
					item.处理任务();
				}
				if (this.主动触发技能 != null && 主程.当前时间 > this.触发时间)
				{
					this.主动触发陷阱();
				}
				if (this.陷阱模板.陷阱能否移动 && this.陷阱移动次数 < this.陷阱模板.限制移动次数 && 主程.当前时间 > this.行走时间)
				{
					if (this.陷阱模板.当前方向移动)
					{
						base.自身移动时处理(计算类.前方坐标(this.当前坐标, this.当前方向, 1));
						base.发送封包(new 陷阱移动位置
						{
							陷阱编号 = this.地图编号,
							移动坐标 = this.当前坐标,
							移动高度 = this.当前高度,
							移动速度 = (ushort)(this.陷阱模板.陷阱移动速度 / 60),
							当前方向 = (ushort)this.当前方向
						});
					}
					else if (this.陷阱模板.主动追击敌人 && this.命中目标 != null)
					{
						base.自身移动时处理(计算类.前方坐标(this.当前坐标, 计算类.计算方向(this.当前坐标, this.命中目标.当前坐标), 1));
						base.发送封包(new 陷阱移动位置
						{
							陷阱编号 = this.地图编号,
							移动坐标 = this.当前坐标,
							移动高度 = this.当前高度,
							移动速度 = (ushort)(this.陷阱模板.陷阱移动速度 / 60),
							当前方向 = (ushort)this.当前方向
						});
					}
					if (this.被动触发技能 != null)
					{
						Point[] array;
						array = 计算类.技能范围(this.当前坐标, this.当前方向, this.对象体型);
						foreach (Point 坐标 in array)
						{
							foreach (地图对象 item2 in this.当前地图[坐标].ToList())
							{
								this.被动触发陷阱(item2);
							}
						}
					}
					this.陷阱移动次数++;
					this.行走时间 = this.行走时间.AddMilliseconds((int)this.陷阱模板.陷阱移动速度);
				}
			}
			base.处理对象数据();
		}

		public void 被动触发陷阱(地图对象 对象)
		{
			if (!(对象 is 玩家实例 { 隐身模式: not false }) && !(主程.当前时间 > this.消失时间) && this.被动触发技能 != null && !对象.对象死亡 && (对象.对象类型 & this.陷阱模板.被动限定类型) != 0 && 对象.特定类型(this.陷阱来源, this.陷阱模板.被动指定类型) && (this.陷阱来源.对象关系(对象) & this.陷阱模板.被动限定关系) != 0 && (!this.陷阱模板.禁止重复触发 || this.被动触发列表.Add(对象)))
			{
				new 技能实例(this, this.被动触发技能, null, 0, this.当前地图, this.当前坐标, 对象, 对象.当前坐标, null);
			}
		}

		public void 主动触发陷阱()
		{
			if (!(主程.当前时间 > this.消失时间))
			{
				new 技能实例(this, this.主动触发技能, null, 0, this.当前地图, this.当前坐标, null, this.当前坐标, null);
				this.触发时间 += TimeSpan.FromMilliseconds((int)this.主动触发间隔);
			}
		}

		public void 陷阱消失处理()
		{
			base.删除对象();
			this.当前地图?.移除对象(this);
		}

		public override void Process(DelayedAction action)
		{
		}
	}
}
