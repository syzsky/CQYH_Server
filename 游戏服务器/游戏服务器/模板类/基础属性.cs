namespace 游戏服务器.模板类
{
	public struct 基础属性
	{
		public 游戏对象属性 属性 { get; set; }

		public int 数值 { get; set; }

		public override string ToString()
		{
			return $"{this.属性} {this.数值}";
		}
	}
}
