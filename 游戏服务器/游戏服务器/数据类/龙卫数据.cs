using System.Collections.Generic;
using 游戏服务器.模板类;

namespace 游戏服务器.数据类
{
	public class 龙卫数据 : 游戏数据
	{
		public readonly 数据监视器<龙卫模板> 对应模板;

		public readonly 数据监视器<byte> 位置编号;

		public readonly 数据监视器<byte> 龙卫品质;

		public readonly 数据监视器<byte> 阶段类型;

		public readonly 数据监视器<int> 属性值;

		public readonly 数据监视器<bool> 激活状态;

		private Dictionary<游戏对象属性, int> _基础属性;

		public 龙卫模板 龙卫模板 => this.对应模板.V;

		public 龙卫属性 龙卫属性 => this.龙卫模板.龙卫属性[this.龙卫品质.V - 1];

		public byte 装备位置
		{
			get
			{
				return this.位置编号.V;
			}
			set
			{
				this.位置编号.V = value;
			}
		}

		public byte 当前阶段
		{
			get
			{
				return this.阶段类型.V;
			}
			set
			{
				this.阶段类型.V = value;
			}
		}

		public int 属性编号 => this.对应模板.V.龙卫编号 + this.龙卫品质.V * 100000;

		public int 当前属性
		{
			get
			{
				return this.属性值.V;
			}
			set
			{
				this.属性值.V = value;
			}
		}

		public bool 是否激活
		{
			get
			{
				return this.激活状态.V;
			}
			set
			{
				this.激活状态.V = value;
			}
		}

		public Dictionary<游戏对象属性, int> 属性加成
		{
			get
			{
				if (this._基础属性 != null)
				{
					return this._基础属性;
				}
				this._基础属性 = new Dictionary<游戏对象属性, int>();
				if (this.对应模板.V.增加属性 != null)
				{
					基础属性[] 增加属性;
					增加属性 = this.对应模板.V.增加属性;
					for (int i = 0; i < 增加属性.Length; i++)
					{
						基础属性 基础属性;
						基础属性 = 增加属性[i];
						this._基础属性[基础属性.属性] = 基础属性.数值 + this.属性值.V;
					}
				}
				return this._基础属性;
			}
		}

		public 龙卫数据()
		{
		}

		public 龙卫数据(龙卫模板 模板, byte 位置, byte 阶段, 龙卫品质 品质)
		{
			this.对应模板.V = 模板;
			this.位置编号.V = 位置;
			this.阶段类型.V = 阶段;
			this.龙卫品质.V = (byte)品质;
			this.属性值.V = 计算类.范围随机(this.龙卫属性.最小数值, this.龙卫属性.最大数值);
			游戏数据网关.龙卫数据表.添加数据(this, 分配索引: true);
		}

		public 龙卫数据(龙卫数据 龙卫数据)
		{
			this.对应模板.V = 龙卫数据.对应模板.V;
			this.位置编号.V = 龙卫数据.位置编号.V;
			this.阶段类型.V = 龙卫数据.阶段类型.V;
			this.龙卫品质.V = 龙卫数据.龙卫品质.V;
			this.属性值.V = 龙卫数据.属性值.V;
			游戏数据网关.龙卫数据表.添加数据(this, 分配索引: true);
		}
	}
}
