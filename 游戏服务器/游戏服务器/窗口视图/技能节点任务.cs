using System.Collections.Generic;
using System.Windows.Forms;
using 游戏服务器.模板类;
using DevExpress.XtraEditors;

namespace 游戏服务器.窗口视图
{
	public class 技能节点任务
	{
		private int _触发时间;

		private 技能任务类型 _任务类型;

		public int 触发时间
		{
			get
			{
				return this._触发时间;
			}
			set
			{
				if (value == this._触发时间)
				{
					return;
				}
				if (this.节点列表 != null)
				{
					if (this.节点列表.ContainsKey(value))
					{
						XtraMessageBox.Show("触发时间不可重复", "重复的触发时间", MessageBoxButtons.OK);
						return;
					}
					this.节点列表.Add(value, this.节点列表[this._触发时间]);
					this.节点列表.Remove(this._触发时间);
				}
				this._触发时间 = value;
			}
		}

		public 技能任务类型 任务类型
		{
			get
			{
				return this._任务类型;
			}
			set
			{
				if (value == this._任务类型)
				{
					return;
				}
				if (this.节点列表 != null)
				{
					if (!this.节点列表.ContainsKey(this._触发时间))
					{
						XtraMessageBox.Show("没找到对应节点", "", MessageBoxButtons.OK);
						return;
					}
					this.技能任务 = (this.节点列表[this._触发时间] = 技能节点任务.获取技能任务(value));
				}
				this._任务类型 = value;
			}
		}

		public 技能任务 技能任务 { get; set; }

		public SortedDictionary<int, 技能任务> 节点列表 { get; set; }

		public static 技能任务 获取技能任务(技能任务类型 任务类型)
		{
			return 任务类型 switch
			{
				技能任务类型.A_00_触发子类技能 => new A_00_触发子类技能(), 
				技能任务类型.A_01_触发对象Buff => new A_01_触发对象Buff(), 
				技能任务类型.A_02_触发陷阱技能 => new A_02_触发陷阱技能(), 
				技能任务类型.B_00_技能切换通知 => new B_00_技能切换通知(), 
				技能任务类型.B_01_技能释放通知 => new B_01_技能释放通知(), 
				技能任务类型.B_02_技能命中通知 => new B_02_技能命中通知(), 
				技能任务类型.B_03_前摇结束通知 => new B_03_前摇结束通知(), 
				技能任务类型.B_04_后摇结束通知 => new B_04_后摇结束通知(), 
				技能任务类型.C_00_计算技能锚点 => new C_00_计算技能锚点(), 
				技能任务类型.C_01_计算命中目标 => new C_01_计算命中目标(), 
				技能任务类型.C_02_计算目标伤害 => new C_02_计算目标伤害(), 
				技能任务类型.C_03_计算对象位移 => new C_03_计算对象位移(), 
				技能任务类型.C_04_计算目标诱惑 => new C_04_计算目标诱惑(), 
				技能任务类型.C_05_计算目标回复 => new C_05_计算目标回复(), 
				技能任务类型.C_06_计算宠物召唤 => new C_06_计算宠物召唤(), 
				技能任务类型.C_07_计算目标瞬移 => new C_07_计算目标瞬移(), 
				_ => new A_00_触发子类技能(), 
			};
		}
	}
}
