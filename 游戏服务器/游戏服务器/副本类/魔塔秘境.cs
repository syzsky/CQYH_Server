using System.Drawing;
using 游戏服务器.地图类;

namespace 游戏服务器.副本类
{
	public static class 魔塔秘境
	{
		public static void 执行(地图实例 地图)
		{
			if (!(主程.当前时间 > 地图.节点计时))
			{
				return;
			}
			if (地图.玩家列表.Count == 0)
			{
				地图.副本节点 = 255;
			}
			if (地图.副本节点 == 255)
			{
				地图.关闭副本();
			}
			else if (地图.副本节点 == 0)
			{
				地图.副本节点++;
				地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
			}
			else if (地图.副本节点 == 1)
			{
				地图.副本节点++;
				地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
			}
			else if (地图.副本节点 == 2)
			{
				地图.副本节点 = 10;
				地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
			}
			else
			{
				if (地图.副本节点 <= 9)
				{
					return;
				}
				int num;
				num = 地图.副本节点 % 10;
				int num2;
				num2 = 地图.副本节点 / 10;
				switch (num)
				{
				case 0:
					switch (num2)
					{
					case 1:
						地图.地图公告("<#T:990638>");
						break;
					case 5:
					case 6:
						地图.地图公告("<#T:990637>");
						break;
					case 2:
					case 3:
					case 4:
						地图.地图公告("<#T:990636>");
						break;
					}
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					break;
				case 1:
					switch (num2)
					{
					case 1:
						地图.地图公告("<#T:990638>");
						break;
					case 5:
					case 6:
						地图.地图公告("<#T:990637>");
						break;
					case 2:
					case 3:
					case 4:
						地图.地图公告("<#T:990636>");
						break;
					}
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					break;
				case 2:
					switch (num2)
					{
					case 1:
						地图.地图公告("<#T:990639>");
						foreach (玩家实例 item in 地图.获取玩家列表())
						{
							item.添加Buff时处理(2563, null);
							item.添加Buff时处理(2571, null);
						}
						break;
					case 2:
						地图.地图公告("<#T:990653>");
						break;
					case 5:
						地图.地图公告("<#T:990646>");
						break;
					case 3:
					case 4:
						地图.地图公告("<#T:990639>");
						break;
					}
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					break;
				case 3:
					副本.范围刷新怪物从地图实例("神武之囚03", 地图, 0, new Point(1043, 86), 禁止复活: true, 立即刷新: true);
					副本.范围刷新怪物从地图实例("秘法之囚03", 地图, 0, new Point(1029, 86), 禁止复活: true, 立即刷新: true);
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					break;
				case 4:
					if (地图.存活怪物总数 == 0)
					{
						地图.副本节点++;
						地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					}
					break;
				case 5:
					foreach (玩家实例 item2 in 地图.获取玩家列表())
					{
						item2.角色数据.脚本变量[副本.数字_魔塔个人秘境层数记录] = num2;
					}
					地图.地图公告($"<#P0:{num2}><#P1:0><#T:666728>");
					地图.地图公告($"<#P0:{num2}><#P1:10><#P2:20><#T:990314>");
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddSeconds(1.0);
					break;
				case 6:
					地图.地图公告("<#T:50030>");
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddHours(1.0);
					副本.刷新守卫从地图实例(7000, 地图, 游戏方向.下方, new Point(1036, 79));
					break;
				}
			}
		}
	}
}
