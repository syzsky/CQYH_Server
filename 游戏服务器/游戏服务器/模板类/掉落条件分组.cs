using 游戏服务器.地图类;
using 游戏服务器.数据类;

namespace 游戏服务器.模板类
{
	public class 掉落条件分组
	{
		public 掉落条件 个人条件 { get; set; }

		public 掉落条件 全局条件 { get; set; }

		public bool 检测(玩家实例 P)
		{
			if (this.个人条件 != null && !this.个人条件.Test(P.角色数据.脚本数字[this.个人条件.VarId]))
			{
				return false;
			}
			if (this.全局条件 != null && !this.全局条件.Test(系统数据.数据.脚本数字[this.全局条件.VarId]))
			{
				return false;
			}
			return true;
		}

		public 掉落条件分组(string cfg)
		{
			string[] array;
			array = cfg.Split(' ', '\t');
			if (array.Length > 2 && array[1] != "*")
			{
				array[1] = array[1].Remove(0, 1);
				this.全局条件 = new 掉落条件
				{
					VarId = int.Parse(array[1]),
					Value = int.Parse(array[3]),
					cmp = array[2]
				};
			}
			if (array.Length > 6)
			{
				array[4] = array[4].Remove(0, 1);
				this.个人条件 = new 掉落条件
				{
					VarId = int.Parse(array[4]),
					Value = int.Parse(array[6]),
					cmp = array[5]
				};
			}
		}
	}
}
