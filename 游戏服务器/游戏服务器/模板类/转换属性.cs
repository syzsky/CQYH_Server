namespace 游戏服务器.模板类
{
	public struct 转换属性
	{
		public static 转换属性 空 = new 转换属性
		{
			属性来源 = 游戏对象属性.未知属性,
			属性转换 = 游戏对象属性.未知属性,
			转换比率 = 0f
		};

		public 游戏对象属性 属性来源 { get; set; }

		public 游戏对象属性 属性转换 { get; set; }

		public float 转换比率 { get; set; }

		public override string ToString()
		{
			return $"{this.属性来源} -> {this.属性转换} {this.转换比率}";
		}
	}
}
