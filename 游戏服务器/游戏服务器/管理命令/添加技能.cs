using System;
using System.Collections.Generic;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 添加技能 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public ushort 序号;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public byte[] ToBytesFromHexString(string hexString)
		{
			string[] array;
			array = hexString.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			byte[] array2;
			array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Convert.ToByte(array[i], 16);
			}
			return array2;
		}

		public override void 执行命令()
		{
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (item.Value.角色数据.角色名字.V == this.角色名字)
				{
					item.Value.玩家学习技能(this.序号, 0);
				}
			}
		}
	}
}
