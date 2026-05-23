using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public sealed class 守卫实例 : 地图对象
	{
		public 地图守卫 对象模板;

		public 对象仇恨 对象仇恨;

		public Point 出生坐标;

		public 游戏方向 出生方向;

		public 地图实例 出生地图;

		public 游戏技能 普攻技能;

		public int 脚本计次;

		public DateTime 存活时间;

		public Dictionary<int, int> 脚本数字;

		public bool 尸体消失 { get; set; }

		public DateTime 复活时间 { get; set; }

		public DateTime 消失时间 { get; set; }

		public DateTime 转移计时 { get; set; }

		public DateTime 脚本计时 { get; set; }

		public override int 处理间隔 => 10;

		public override DateTime 忙碌时间
		{
			get
			{
				return base.忙碌时间;
			}
			set
			{
				if (base.忙碌时间 < value)
				{
					base.忙碌时间 = (this.硬直时间 = value);
				}
			}
		}

		public override DateTime 硬直时间
		{
			get
			{
				return base.硬直时间;
			}
			set
			{
				if (base.硬直时间 < value)
				{
					base.硬直时间 = value;
				}
			}
		}

		public override int 当前体力
		{
			get
			{
				return base.当前体力;
			}
			set
			{
				value = 计算类.数值限制(0, value, this[游戏对象属性.最大体力]);
				if (base.当前体力 != value)
				{
					base.当前体力 = value;
					base.发送封包(new 同步对象体力
					{
						对象编号 = this.地图编号,
						当前体力 = this.当前体力,
						体力上限 = this[游戏对象属性.最大体力]
					});
				}
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

		public override 游戏方向 当前方向
		{
			get
			{
				return base.当前方向;
			}
			set
			{
				if (this.当前方向 != value)
				{
					base.当前方向 = value;
					base.发送封包(new 对象转动方向
					{
						转向耗时 = 100,
						对象编号 = this.地图编号,
						对象朝向 = (ushort)value
					});
				}
			}
		}

		public override byte 当前等级 => this.对象模板.守卫等级;

		public override bool 能被命中
		{
			get
			{
				if (this.能否受伤)
				{
					return !this.对象死亡;
				}
				return false;
			}
		}

		public override string 对象名字 => Regex.Replace(this.对象模板.守卫名字, "[\\d-]", string.Empty);

		public override 游戏对象类型 对象类型 => 游戏对象类型.Npcc;

		public override 技能范围类型 对象体型 => 技能范围类型.单体1x1;

		public override int this[游戏对象属性 属性]
		{
			get
			{
				return base[属性];
			}
			set
			{
				base[属性] = value;
			}
		}

		public int 仇恨范围 => 10;

		public ushort 模板编号 => this.对象模板.守卫编号;

		public int 复活间隔 => this.对象模板.复活间隔;

		public int 商店编号 => this.对象模板.商店编号;

		public string 界面代码 => this.对象模板.界面代码;

		public bool 能否受伤 => this.对象模板.能否受伤;

		public bool 主动攻击目标 => this.对象模板.主动攻击;

		public bool 执行脚本 => this.对象模板.执行脚本;

		public bool 禁止复活 => this.对象模板.禁止复活;

		public string 脚本路径 => this.对象模板.脚本路径;

		public int ScriptID { get; set; }

		private void LoadScript()
		{
			this.ScriptID = NPCScript.GetOrAdd(this.地图编号, $"{this.模板编号}-{this.对象模板.守卫名字}", NPCScriptType.Normal).ScriptID;
		}

		public 守卫实例(地图守卫 对应模板, 地图实例 出生地图, 游戏方向 出生方向, Point 出生坐标)
		{
			this.脚本计时 = DateTime.Now;
			this.脚本计次 = 0;
			this.对象模板 = 对应模板;
			this.出生地图 = 出生地图;
			this.当前地图 = 出生地图;
			this.出生方向 = 出生方向;
			this.出生坐标 = 出生坐标;
			this.地图编号 = ++地图处理网关.对象编号;
			base.属性加成[this] = new Dictionary<游戏对象属性, int> { [游戏对象属性.最大体力] = 9999 };
			string text;
			text = this.对象模板.普攻技能;
			if (text != null && text.Length > 0)
			{
				游戏技能.数据表.TryGetValue(this.对象模板.普攻技能, out this.普攻技能);
			}
			地图处理网关.添加地图对象(this);
			this.守卫复活处理();
			this.LoadScript();
			this.存活时间 = DateTime.MaxValue;
			this.脚本数字 = new Dictionary<int, int>();
		}

		public override void 处理对象数据()
		{
			if (主程.当前时间 < base.预约时间)
			{
				return;
			}
			if (!this.对象死亡 && 主程.当前时间 >= this.存活时间)
			{
				base.删除对象();
				return;
			}
			if (this.对象死亡)
			{
				if (!this.尸体消失 && 主程.当前时间 >= this.消失时间)
				{
					if (this.禁止复活)
					{
						base.删除对象();
					}
					else
					{
						base.清空邻居时处理();
						base.解绑网格();
					}
					this.尸体消失 = true;
				}
				if (主程.当前时间 >= this.复活时间)
				{
					base.清空邻居时处理();
					base.解绑网格();
					this.守卫复活处理();
				}
			}
			else
			{
				foreach (KeyValuePair<ushort, Buff数据> item in this.Buff列表.ToList())
				{
					base.轮询Buff时处理(item.Value);
				}
				foreach (技能实例 item2 in base.技能任务.ToList())
				{
					item2.处理任务();
				}
				if (主程.当前时间 > base.恢复时间)
				{
					if (!this.检查状态(游戏对象状态.中毒状态))
					{
						this.当前体力 += 5;
					}
					base.恢复时间 = 主程.当前时间.AddSeconds(5.0);
				}
				if (this.主动攻击目标 && 主程.当前时间 > this.忙碌时间 && 主程.当前时间 > this.硬直时间)
				{
					if (this.更新对象仇恨())
					{
						this.守卫智能攻击();
					}
					else if (this.对象仇恨.仇恨列表.Count == 0 && this.能否转动())
					{
						this.当前方向 = this.出生方向;
					}
				}
				if (this.模板编号 == 6121 && this.当前地图.地图编号 == 183 && 主程.当前时间 > this.转移计时)
				{
					base.清空邻居时处理();
					base.解绑网格();
					this.当前坐标 = this.当前地图.传送区域.随机坐标;
					base.绑定网格();
					base.更新邻居时处理();
					this.转移计时 = 主程.当前时间.AddMinutes(2.5);
				}
			}
			base.处理对象数据();
		}

		public override void 自身死亡处理(地图对象 对象, bool 技能击杀, bool 脚本击杀 = false)
		{
			base.自身死亡处理(对象, 技能击杀);
			this.消失时间 = 主程.当前时间.AddMilliseconds(10000.0);
			this.复活时间 = 主程.当前时间.AddMilliseconds((this.当前地图.地图编号 == 80) ? int.MaxValue : 60000);
			this.Buff列表.Clear();
			base.次要对象 = true;
			地图处理网关.添加次要对象(this);
			if (base.激活对象)
			{
				base.激活对象 = false;
				地图处理网关.移除激活对象(this);
			}
		}

		public void 守卫沉睡处理()
		{
			if (base.激活对象)
			{
				base.激活对象 = false;
				base.技能任务.Clear();
				地图处理网关.移除激活对象(this);
			}
		}

		public void 守卫激活处理()
		{
			if (!base.激活对象)
			{
				base.激活对象 = true;
				地图处理网关.添加激活对象(this);
				int num;
				num = (int)Math.Max(0.0, (主程.当前时间 - base.恢复时间).TotalSeconds / 5.0);
				base.当前体力 = Math.Min(this[游戏对象属性.最大体力], this.当前体力 + num * this[游戏对象属性.体力恢复]);
				base.恢复时间 = base.恢复时间.AddSeconds(5.0);
			}
		}

		public void 守卫智能攻击()
		{
			if (!this.检查状态(游戏对象状态.麻痹状态 | 游戏对象状态.失神状态) && this.普攻技能 != null)
			{
				if (base.网格距离(this.对象仇恨.当前目标) > this.普攻技能.技能最远距离)
				{
					this.对象仇恨.移除仇恨(this.对象仇恨.当前目标);
				}
				else
				{
					new 技能实例(this, this.普攻技能, null, base.动作编号++, this.当前地图, this.当前坐标, this.对象仇恨.当前目标, this.对象仇恨.当前目标.当前坐标, null);
				}
			}
		}

		public void 守卫复活处理()
		{
			this.更新对象属性();
			base.次要对象 = false;
			this.对象死亡 = false;
			this.阻塞网格 = !this.对象模板.虚无状态;
			this.当前地图 = this.出生地图;
			this.当前方向 = this.出生方向;
			this.当前坐标 = this.出生坐标;
			this.当前体力 = this[游戏对象属性.最大体力];
			base.恢复时间 = 主程.当前时间.AddMilliseconds(主程.随机数.Next(5000));
			this.对象仇恨 = new 对象仇恨();
			base.绑定网格();
			base.更新邻居时处理();
		}

		public bool 更新对象仇恨()
		{
			if (this.对象仇恨.仇恨列表.Count == 0)
			{
				return false;
			}
			if (this.对象仇恨.当前目标 == null)
			{
				return this.对象仇恨.切换仇恨(this);
			}
			if (this.对象仇恨.当前目标.对象死亡)
			{
				this.对象仇恨.移除仇恨(this.对象仇恨.当前目标);
			}
			else if (!base.邻居列表.Contains(this.对象仇恨.当前目标))
			{
				this.对象仇恨.移除仇恨(this.对象仇恨.当前目标);
			}
			else if (!this.对象仇恨.仇恨列表.ContainsKey(this.对象仇恨.当前目标))
			{
				this.对象仇恨.移除仇恨(this.对象仇恨.当前目标);
			}
			else if (base.网格距离(this.对象仇恨.当前目标) > this.仇恨范围)
			{
				this.对象仇恨.移除仇恨(this.对象仇恨.当前目标);
			}
			if (this.对象仇恨.当前目标 == null)
			{
				return this.更新对象仇恨();
			}
			return true;
		}

		public void 清空守卫仇恨()
		{
			this.对象仇恨.当前目标 = null;
			this.对象仇恨.仇恨列表.Clear();
		}

		public override void Process(DelayedAction action)
		{
		}
	}
}
