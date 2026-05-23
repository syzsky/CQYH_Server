using System.Collections.Generic;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 修复伐髓BUG : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public int 龙卫品质;

		[字段描述(0, 排序 = 1)]
		public int 大于属性值;

		[字段描述(0, 排序 = 1)]
		public int 新属性值;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			List<string> list;
			list = new List<string>();
			foreach (KeyValuePair<int, 游戏数据> item in 游戏数据网关.角色数据表.数据表)
			{
				角色数据 角色数据;
				角色数据 = item.Value as 角色数据;
				foreach (龙卫数据 item2 in 角色数据.龙卫属性)
				{
					if (item2.龙卫品质.V == this.龙卫品质 && item2.龙卫模板.龙卫编号 == 2030 && item2.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} {item2.装备位置} 第 {item2.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item2.当前属性}");
						item2.当前属性 = this.新属性值;
					}
				}
				foreach (龙卫数据 item3 in 角色数据.龙卫属性一)
				{
					if (item3.龙卫品质.V == this.龙卫品质 && item3.龙卫模板.龙卫编号 == 2030 && item3.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} 记录一 {item3.装备位置} 第 {item3.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item3.当前属性}");
						item3.当前属性 = this.新属性值;
					}
				}
				foreach (龙卫数据 item4 in 角色数据.龙卫属性二)
				{
					if (item4.龙卫品质.V == this.龙卫品质 && item4.龙卫模板.龙卫编号 == 2030 && item4.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} 记录二 {item4.装备位置} 第 {item4.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item4.当前属性}");
						item4.当前属性 = this.新属性值;
					}
				}
				foreach (龙卫数据 item5 in 角色数据.龙卫属性三)
				{
					if (item5.龙卫品质.V == this.龙卫品质 && item5.龙卫模板.龙卫编号 == 2030 && item5.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} 记录三 {item5.装备位置} 第 {item5.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item5.当前属性}");
						item5.当前属性 = this.新属性值;
					}
				}
				foreach (龙卫数据 item6 in 角色数据.龙卫属性四)
				{
					if (item6.龙卫品质.V == this.龙卫品质 && item6.龙卫模板.龙卫编号 == 2030 && item6.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} 记录四 {item6.装备位置} 第 {item6.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item6.当前属性}");
						item6.当前属性 = this.新属性值;
					}
				}
				foreach (龙卫数据 item7 in 角色数据.龙卫属性五)
				{
					if (item7.龙卫品质.V == this.龙卫品质 && item7.龙卫模板.龙卫编号 == 2030 && item7.当前属性 > this.大于属性值)
					{
						list.Add($"{角色数据.角色名字.V} 记录五 {item7.装备位置} 第 {item7.位置编号.V} 条伐髓 品质={this.龙卫品质} 属性值={item7.当前属性}");
						item7.当前属性 = this.新属性值;
					}
				}
			}
			主程.添加命令日志(string.Join("\r\n", list));
		}
	}
}
