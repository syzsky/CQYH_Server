using System;
using System.Linq;
using 游戏服务器.模板类;

namespace 游戏服务器.数据类
{
	public class CharacterQuest : 游戏数据
	{
		public 数据监视器<角色数据> Character;

		public readonly 数据监视器<GameQuests> Info;

		public readonly 数据监视器<DateTime> StartDate;

		public readonly 数据监视器<DateTime> CompleteDate;

		public readonly 数据监视器<byte> CompleteCount;

		public readonly 哈希监视器<CharacterQuestMission> Missions;

		public bool IsCompleted => this.Missions.All((CharacterQuestMission x) => x.CompletedDate.V != DateTime.MinValue);

		public static CharacterQuest Create(角色数据 character, GameQuests gameQuest)
		{
			CharacterQuest characterQuest;
			characterQuest = new CharacterQuest();
			characterQuest.Character.V = character;
			characterQuest.Info.V = gameQuest;
			characterQuest.StartDate.V = 主程.当前时间;
			foreach (GameQuestMission mission in gameQuest.Missions)
			{
				if (!mission.Role.HasValue || mission.Role.Value == character.角色职业.V)
				{
					characterQuest.Missions.Add(CharacterQuestMission.Create(characterQuest, mission));
				}
			}
			游戏数据网关.CharacterQuestDataTable.添加数据(characterQuest, 分配索引: true);
			return characterQuest;
		}

		public override void 删除数据()
		{
			foreach (CharacterQuestMission mission in this.Missions)
			{
				mission.删除数据();
			}
			base.删除数据();
		}

		public CharacterQuestMission[] GetMissionsOfType(QuestMissionType type)
		{
			return this.Missions.Where(delegate(CharacterQuestMission x)
			{
				数据监视器<GameQuestMission> info;
				info = x.Info;
				return info != null && info.V?.Type == type;
			}).ToArray();
		}
	}
}
