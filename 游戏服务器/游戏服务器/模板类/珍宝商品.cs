using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace 游戏服务器.模板类
{
	public sealed class 珍宝商品
	{
		public static byte[] 珍宝商店数据;

		public static int 珍宝商店效验;

		public static int 珍宝商店数量;

		public static Dictionary<int, 珍宝商品> 数据表;

		public int 物品编号;

		public int 单位数量;

		public byte 商品分类;

		public byte 商品标签;

		public byte 补充参数;

		public byte 热销排名;

		public int 商品原价;

		public int 商品现价;

		public byte 买入绑定;

		public int 未知参数15;

		public int 未知参数19;

		public int 未知参数23;

		public int 未知参数32;

		public int 未知参数36;

		public int 未知参数40;

		public int 未知参数44;

		public int 未知参数48;

		public int 未知参数52;

		public int 未知参数56;

		public int 未知参数60;

		public ushort 未知参数64;

		public byte 未知参数66;

		public static void 载入数据()
		{
			Dictionary<int, 珍宝商品> dictionary;
			dictionary = new Dictionary<int, 珍宝商品>();
			string text;
			text = Settings.游戏数据目录 + "\\System\\物品数据\\珍宝商品\\";
			if (Directory.Exists(text))
			{
				object[] array;
				array = 序列化类.反序列化(text, typeof(珍宝商品));
				foreach (object obj in array)
				{
					dictionary.Add(((珍宝商品)obj).物品编号, (珍宝商品)obj);
				}
			}
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			foreach (珍宝商品 item in (from X in dictionary.Values.ToList()
				orderby X.物品编号
				select X).ToList())
			{
				binaryWriter.Write(item.物品编号);
				binaryWriter.Write(item.单位数量);
				binaryWriter.Write(item.商品分类);
				binaryWriter.Write(item.商品标签);
				binaryWriter.Write(item.补充参数);
				binaryWriter.Write(item.商品原价);
				binaryWriter.Write(item.商品现价);
				binaryWriter.Write(item.未知参数15);
				binaryWriter.Write(item.未知参数19);
				binaryWriter.Write(item.未知参数23);
				binaryWriter.Write(item.热销排名);
				binaryWriter.Write(item.未知参数32);
				binaryWriter.Write(item.未知参数36);
				binaryWriter.Write(item.未知参数40);
				binaryWriter.Write(item.未知参数44);
				binaryWriter.Write(item.未知参数48);
				binaryWriter.Write(item.未知参数52);
				binaryWriter.Write(item.未知参数56);
				binaryWriter.Write(item.未知参数60);
				binaryWriter.Write(item.未知参数64);
				binaryWriter.Write(item.未知参数66);
			}
			int count;
			count = dictionary.Count;
			byte[] array2;
			array2 = memoryStream.ToArray();
			int num;
			num = 0;
			byte[] array3;
			array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				num += array3[i];
			}
			珍宝商品.珍宝商店数据 = array2;
			珍宝商品.珍宝商店效验 = num;
			珍宝商品.珍宝商店数量 = count;
			珍宝商品.数据表 = dictionary;
		}

		public static void 重新生成发送数据()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			foreach (珍宝商品 item in (from X in 珍宝商品.数据表.Values.ToList()
				orderby X.物品编号
				select X).ToList())
			{
				binaryWriter.Write(item.物品编号);
				binaryWriter.Write(item.单位数量);
				binaryWriter.Write(item.商品分类);
				binaryWriter.Write(item.商品标签);
				binaryWriter.Write(item.补充参数);
				binaryWriter.Write(item.商品原价);
				binaryWriter.Write(item.商品现价);
				binaryWriter.Write(new byte[12]);
				binaryWriter.Write(item.热销排名);
				binaryWriter.Write(new byte[35]);
			}
			珍宝商品.珍宝商店数量 = 珍宝商品.数据表.Count;
			珍宝商品.珍宝商店数据 = memoryStream.ToArray();
			珍宝商品.珍宝商店效验 = 0;
			byte[] array;
			array = 珍宝商品.珍宝商店数据;
			for (int i = 0; i < array.Length; i++)
			{
				珍宝商品.珍宝商店效验 += array[i];
			}
		}

		internal static void 保存数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\物品数据\\珍宝商品\\";
			if (!Directory.Exists(text))
			{
				return;
			}
			FileInfo[] files;
			files = new DirectoryInfo(text).GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
			foreach (KeyValuePair<int, 珍宝商品> item in 珍宝商品.数据表)
			{
				StreamWriter streamWriter;
				streamWriter = File.CreateText(text + 游戏物品.数据表[item.Value.物品编号].物品名字 + ".txt");
				streamWriter.Write(JsonConvert.SerializeObject(item.Value, Formatting.Indented, 序列化类.全局设置));
				streamWriter.Close();
			}
		}
	}
}
