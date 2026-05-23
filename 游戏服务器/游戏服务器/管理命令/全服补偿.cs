using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 全服补偿 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public int 物品编号;

		[字段描述(0, 排序 = 1)]
		public int 物品数量;

		[字段描述(0, 排序 = 3)]
		public int 限制等级;

		[字段描述(0, 排序 = 3)]
		public int 限制特权;

		[字段描述(0, 排序 = 2)]
		public string 补偿标题;

		[字段描述(0, 排序 = 3)]
		public string 补偿内容;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			foreach (游戏数据 value in 游戏数据网关.角色数据表.数据表.Values)
			{
				if (value is 角色数据 角色数据 && 角色数据.当前等级.V >= this.限制等级 && 角色数据.本期特权.V >= this.限制特权)
				{
					角色数据.发送邮件(null, this.补偿标题, this.补偿内容, this.物品编号, this.物品数量, 是否绑定: true);
				}
			}
		}
	}
}
