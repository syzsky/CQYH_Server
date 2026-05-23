namespace 游戏服务器.模板类
{
	public struct 铭文属性
	{
		public 游戏对象属性 属性 { get; set; }

		public int 零级 { get; set; }

		public int 一级 { get; set; }

		public int 二级 { get; set; }

		public int 三级 { get; set; }

		public override string ToString()
		{
			return $"{this.属性} {this.零级} {this.一级} {this.二级} {this.三级}";
		}
	}
}
