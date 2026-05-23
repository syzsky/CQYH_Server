using System;

namespace 游戏服务器.数据类
{
	public sealed class 定时器数据
	{
		public byte 定时器ID;

		public int 执行间隔秒数;

		public int 剩余执行次数;

		public DateTime 下次执行时间;
	}
}
