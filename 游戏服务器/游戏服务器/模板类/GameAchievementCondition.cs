using System.Collections.Generic;

namespace 游戏服务器.模板类
{
	public class GameAchievementCondition
	{
		public string Desc { get; set; }

		public string Type { get; set; }

		public Dictionary<string, object> Props { get; set; }
	}
}
