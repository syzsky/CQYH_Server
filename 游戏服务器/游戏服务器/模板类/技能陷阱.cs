using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using 游戏服务器.工具类;
using Newtonsoft.Json;

namespace 游戏服务器.模板类
{
	public sealed class 技能陷阱
	{
		public static Dictionary<string, 技能陷阱> 数据表;

		[Category("基本属性")]
		public string 陷阱名字 { get; set; }

		[Category("基本属性")]
		public ushort 陷阱编号 { get; set; }

		[Category("基本属性")]
		public ushort 分组编号 { get; set; }

		[Category("高级属性")]
		public 技能范围类型 陷阱体型 { get; set; }

		[Category("基本属性")]
		public ushort 绑定等级 { get; set; }

		[Category("高级属性")]
		public bool 陷阱允许叠加 { get; set; }

		[Category("高级属性")]
		public bool 陷阱无视地形 { get; set; }

		[Category("时间设置")]
		public int 陷阱持续时间 { get; set; }

		[Category("时间设置")]
		public bool 持续时间延长 { get; set; }

		[Category("时间设置")]
		public bool 技能等级延时 { get; set; }

		[Category("时间设置")]
		public int 每级延长时间 { get; set; }

		[Category("时间设置")]
		public bool 角色属性延时 { get; set; }

		[Category("时间设置")]
		public 游戏对象属性 绑定角色属性 { get; set; }

		[Category("时间设置")]
		public float 属性延时系数 { get; set; }

		[Category("时间设置")]
		public bool 特定铭文延时 { get; set; }

		[Category("时间设置")]
		public 铭文技能 绑定铭文技能 { get; set; }

		[Category("时间设置")]
		public int 特定铭文技能 { get; set; }

		[Category("时间设置")]
		public int 铭文延长时间 { get; set; }

		[Category("高级属性")]
		public bool 陷阱能否移动 { get; set; }

		[Category("高级属性")]
		public ushort 陷阱移动速度 { get; set; }

		[Category("高级属性")]
		public byte 限制移动次数 { get; set; }

		[Category("高级属性")]
		public bool 当前方向移动 { get; set; }

		[Category("高级属性")]
		public bool 主动追击敌人 { get; set; }

		[Category("高级属性")]
		public byte 陷阱追击范围 { get; set; }

		[Category("被动触发")]
		public string 被动触发技能 { get; set; }

		[Category("被动触发")]
		public bool 禁止重复触发 { get; set; }

		[Category("被动触发")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 被动指定类型 { get; set; }

		[Category("被动触发")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 游戏对象类型 被动限定类型 { get; set; }

		[Category("被动触发")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 游戏对象关系 被动限定关系 { get; set; }

		[Category("主动触发")]
		public string 主动触发技能 { get; set; }

		[Category("主动触发")]
		public ushort 主动触发间隔 { get; set; }

		[Category("主动触发")]
		public ushort 主动触发延迟 { get; set; }

		public static void 载入数据()
		{
			技能陷阱.数据表 = new Dictionary<string, 技能陷阱>();
			string text;
			text = Settings.游戏数据目录 + "\\System\\技能数据\\陷阱数据\\";
			if (Directory.Exists(text))
			{
				object[] array;
				array = 序列化类.反序列化(text, typeof(技能陷阱));
				foreach (object obj in array)
				{
					技能陷阱.数据表.Add(((技能陷阱)obj).陷阱名字, (技能陷阱)obj);
				}
			}
		}

		public override string ToString()
		{
			return $"{this.陷阱编号}-{this.陷阱名字}";
		}

		internal static void 保存数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\技能数据\\陷阱数据\\";
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
			foreach (KeyValuePair<string, 技能陷阱> item in 技能陷阱.数据表)
			{
				StreamWriter streamWriter;
				streamWriter = File.CreateText($"{text}{item.Value.陷阱编号}-{item.Value.陷阱名字}.txt");
				streamWriter.Write(JsonConvert.SerializeObject(item.Value, Formatting.Indented, 序列化类.全局设置));
				streamWriter.Close();
			}
		}
	}
}
