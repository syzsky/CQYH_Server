namespace 游戏服务器.日志类
{
	public sealed class MerchantRecyLog : LogData
	{
		public string areaName { get; set; }

		public string merchantName { get; set; }

		public string playName { get; set; }

		public string playActive { get; set; }

		public uint recyNum { get; set; }

		public uint recyResiduNum { get; set; }

		public string typeMoney { get; set; }
	}
}
