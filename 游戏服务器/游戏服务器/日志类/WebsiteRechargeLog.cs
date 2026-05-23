namespace 游戏服务器.日志类
{
	public sealed class WebsiteRechargeLog : LogData
	{
		public string areaName { get; set; }

		public string formUser { get; set; }

		public string playName { get; set; }

		public string playActive { get; set; }

		public uint playNum { get; set; }

		public uint formMoney { get; set; }

		public string typeMoney { get; set; }
	}
}
