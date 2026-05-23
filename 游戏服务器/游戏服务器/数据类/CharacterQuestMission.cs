using System;

namespace 游戏服务器.数据类
{
	public class CharacterQuestMission : 游戏数据
	{
		public 数据监视器<CharacterQuest> CharacterQuest;

		public 数据监视器<GameQuestMission> Info;

		public 数据监视器<DateTime> CompletedDate;

		public 数据监视器<int> Count;

		public static CharacterQuestMission Create(CharacterQuest characterQuest, GameQuestMission mission)
		{
			CharacterQuestMission characterQuestMission;
			characterQuestMission = new CharacterQuestMission();
			characterQuestMission.CharacterQuest.V = characterQuest;
			characterQuestMission.Info.V = mission;
			characterQuestMission.Count.V = 0;
			游戏数据网关.CharacterQuestConstraintDataTable.添加数据(characterQuestMission, 分配索引: true);
			return characterQuestMission;
		}
	}
}
