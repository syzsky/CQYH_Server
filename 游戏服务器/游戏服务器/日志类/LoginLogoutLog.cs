namespace 游戏服务器.日志类
{
	public sealed class LoginLogoutLog : LogData
	{
		public string userAcount { get; set; }

		public string si { get; set; }

		public int bt { get; set; }
	}
}
