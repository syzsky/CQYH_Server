namespace 游戏服务器.模板类
{
	public sealed class CreateGameRole : LogData
	{
		public string areaName { get; set; }

		public string invitationCode { get; set; }

		public string gameAccount { get; set; }

		public string roleName { get; set; }
	}
}
