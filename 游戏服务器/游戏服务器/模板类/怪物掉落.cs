namespace 游戏服务器.模板类
{
	public class 怪物掉落
	{
		public string 物品名字 { get; set; }

		public string 怪物名字 { get; set; }

		public int 掉落概率 { get; set; }

		public int 最小数量 { get; set; }

		public int 最大数量 { get; set; }

		public int 暴率分组 { get; set; }

		public int 公告ID { get; set; }

		public 掉落条件分组 条件分组 { get; set; }

		public override string ToString()
		{
			return $"{this.怪物名字} - {this.物品名字} - {this.掉落概率} - {this.最小数量}/{this.最大数量}";
		}
	}
}
