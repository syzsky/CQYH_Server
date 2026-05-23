namespace 游戏服务器.日志类
{
	public sealed class ConsumptionLog : LogData
	{
		public string areaName { get; set; }

		public string playName { get; set; }

		public string playActive { get; set; }

		public int xhNum { get; set; }

		public string remake { get; set; }

		public string typeMoney { get; set; }
	}
}
