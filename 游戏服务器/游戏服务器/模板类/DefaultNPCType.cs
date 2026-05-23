namespace 游戏服务器.模板类
{
	public enum DefaultNPCType : byte
	{
		Login = 0,
		LevelUp = 1,
		UseItem = 2,
		MapCoord = 3,
		MapEnter = 4,
		Die = 5,
		MonDie = 6,
		RedeemCode = 7,
		PlayerDie = 8,
		TryOpenDropBox = 9,
		OpenDropBox = 10,
		StopOpenDropBox = 11,
		MotaSuccess = 12,
		buff_add = 13,
		buff_remove = 14,
		buff_delete = 15,
		buff_run = 16,
		DeCompose = 17,
		Trigger = 18,
		CustomCommand = 19,
		OnAcceptQuest = 20,
		OnFinishQuest = 21,
		DayChange = 22,
		PlayKill = 23,
		KillPlay = 24,
		Client = 25,
		Timer = 26,
		ItemRestore = 27
	}
}
