using System.Collections.Generic;
using System.IO;
using 游戏服务器.数据类;

namespace 游戏服务器.地图类
{
	public sealed class 玩家摊位
	{
		public byte 摊位状态;

		public string 摊位名字;

		public Dictionary<物品数据, int> 物品数量;

		public Dictionary<物品数据, int> 物品单价;

		public Dictionary<byte, 物品数据> 摊位物品;

		public 玩家摊位()
		{
			this.摊位状态 = 1;
			this.物品数量 = new Dictionary<物品数据, int>();
			this.物品单价 = new Dictionary<物品数据, int>();
			this.摊位物品 = new Dictionary<byte, 物品数据>();
		}

		public long 物品总价()
		{
			long num;
			num = 0L;
			foreach (物品数据 value in this.摊位物品.Values)
			{
				num += (long)this.物品数量[value] * (long)this.物品单价[value];
			}
			return num;
		}

		public byte[] 摊位描述()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write((byte)this.摊位物品.Count);
			foreach (KeyValuePair<byte, 物品数据> item in this.摊位物品)
			{
				binaryWriter.Write(item.Key);
				binaryWriter.Write(this.物品单价[item.Value]);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write((item.Value is 装备数据) ? item.Value.字节描述() : item.Value.字节描述(this.物品数量[item.Value]));
			}
			return memoryStream.ToArray();
		}
	}
}
