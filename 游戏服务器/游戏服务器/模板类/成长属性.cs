namespace 游戏服务器.模板类
{
	public struct 成长属性
	{
		public 游戏对象属性 属性 { get; set; }

		public int 零级 { get; set; }

		public int 一级 { get; set; }

		public int 二级 { get; set; }

		public int 三级 { get; set; }

		public int 四级 { get; set; }

		public int 五级 { get; set; }

		public int 六级 { get; set; }

		public int 七级 { get; set; }

		public override string ToString()
		{
			return $"{this.属性} {this.零级} {this.一级} {this.二级} {this.三级} {this.四级} {this.五级} {this.六级} {this.七级}";
		}
	}
}
