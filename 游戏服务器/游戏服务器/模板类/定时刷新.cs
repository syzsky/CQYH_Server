namespace 游戏服务器.模板类
{
	public sealed class 定时刷新
	{
		public byte 小时 { get; set; }

		public byte 分钟 { get; set; }

		public override string ToString()
		{
			return $"{this.小时}.{this.分钟}分";
		}
	}
}
