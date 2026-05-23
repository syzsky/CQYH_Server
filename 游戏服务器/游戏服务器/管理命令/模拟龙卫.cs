using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public sealed class 模拟龙卫 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public ushort 龙卫编号;

		[字段描述(0, 排序 = 1)]
		public ushort 龙卫品质;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (龙卫模板.数据表.TryGetValue(this.龙卫编号, out var value))
			{
				bool flag;
				flag = true;
				int num;
				num = 0;
				while (flag)
				{
					num++;
					龙卫模板 龙卫模板;
					龙卫模板 = 龙卫模板.获取龙卫模板(value.词缀类型, value.需要职业, 不许多格词条: false, out var 龙卫品质);
					if ((byte)龙卫品质 == this.龙卫品质 && 龙卫模板.龙卫编号 == this.龙卫编号)
					{
						break;
					}
				}
				主程.添加命令日志($"洗炼了{num}次得到了");
			}
			else
			{
				主程.添加命令日志("未知的龙卫编号");
			}
		}
	}
}
