using System.Text.Json.Serialization;

namespace 游戏服务器
{
	public class GameQuestMission
	{
		[JsonIgnore]
		public int QuestId { get; set; }

		[JsonIgnore]
		public int MissionIndex { get; set; }

		public QuestMissionType Type { get; set; }

		public 游戏对象职业? Role { get; set; }

		public int Id { get; set; }

		public int Count { get; set; }
	}
}
