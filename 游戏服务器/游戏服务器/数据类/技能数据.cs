using System;
using System.Collections.Generic;
using 游戏服务器.模板类;

namespace 游戏服务器.数据类
{
	public sealed class 技能数据 : 游戏数据
	{
		public byte 铭文编号;

		public DateTime 计数时间;

		public readonly 数据监视器<ushort> 技能编号;

		public readonly 数据监视器<ushort> 技能经验;

		public readonly 数据监视器<byte> 技能等级;

		public readonly 数据监视器<byte> 快捷栏位;

		public readonly 数据监视器<byte> 剩余次数;

		public int 技能索引 => this.技能编号.V * 100 + this.铭文编号 * 10 + this.技能等级.V;

		public 铭文技能 铭文模板 => 铭文技能.数据表[this.铭文索引];

		public bool 自动装配 => this.铭文模板.被动技能;

		public byte 升级等级
		{
			get
			{
				if (this.铭文模板.需要角色等级 != null && this.铭文模板.需要角色等级.Length > this.技能等级.V + 1)
				{
					if (this.铭文模板.需要角色等级[this.技能等级.V] == 0)
					{
						return byte.MaxValue;
					}
					return this.铭文模板.需要角色等级[this.技能等级.V];
				}
				return byte.MaxValue;
			}
		}

		public byte 技能计数 => this.铭文模板.技能计数;

		public ushort 计数周期 => this.铭文模板.计数周期;

		public int 升级经验
		{
			get
			{
				if (this.铭文模板.需要技能经验 != null && this.铭文模板.需要技能经验.Length > this.技能等级.V)
				{
					return this.铭文模板.需要技能经验[this.技能等级.V];
				}
				return 0;
			}
		}

		public ushort 铭文索引 => (ushort)(this.技能编号.V * 10 + this.铭文编号);

		public int 战力加成
		{
			get
			{
				if (this.铭文模板.技能战力加成.Length <= this.技能等级.V)
				{
					return 0;
				}
				return this.铭文模板.技能战力加成[this.技能等级.V];
			}
		}

		public List<ushort> 技能Buff => this.铭文模板.铭文附带Buff;

		public List<ushort> 被动技能 => this.铭文模板.被动技能列表;

		public Dictionary<游戏对象属性, int> 属性加成
		{
			get
			{
				if (this.铭文模板.属性加成 != null && this.铭文模板.属性加成.Length > this.技能等级.V)
				{
					return this.铭文模板.属性加成[this.技能等级.V];
				}
				return null;
			}
		}

		public 技能数据()
		{
		}

		public 技能数据(ushort 编号, byte 等级 = 0)
		{
			this.快捷栏位.V = 100;
			this.技能编号.V = 编号;
			this.剩余次数.V = this.技能计数;
			this.技能等级.V = 等级;
			游戏数据网关.技能数据表.添加数据(this, 分配索引: true);
		}

		public override string ToString()
		{
			return this.铭文模板?.技能名字;
		}
	}
}
