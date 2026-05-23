using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace 游戏服务器.模板类
{
	public class GameAchievements
	{
		public static Dictionary<ushort, GameAchievements> 数据表;

		public ushort Id { get; set; }

		public string Name { get; set; }

		public string Desc { get; set; }

		public int BaseClass { get; set; }

		public int SubClass { get; set; }

		public QuestResetType ResetType { get; set; }

		public int AchievementPoints { get; set; }

		public List<int> PreAchivements { get; set; } = new List<int>();


		public List<GameAchievementCondition> Conditions { get; set; } = new List<GameAchievementCondition>();


		public List<GameAchievementReward> Rewards { get; set; } = new List<GameAchievementReward>();


		public static void 载入数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\Achievements\\";
			if (Directory.Exists(text))
			{
				GameAchievements.数据表 = 序列化类.反序列化<GameAchievements>(text).ToDictionary((GameAchievements x) => x.Id);
			}
			else
			{
				GameAchievements.数据表 = new Dictionary<ushort, GameAchievements>();
			}
		}
	}
}
