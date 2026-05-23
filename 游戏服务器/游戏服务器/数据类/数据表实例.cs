using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace 游戏服务器.数据类
{
	public sealed class 数据表实例<T> : 数据表基类 where T : 游戏数据, new()
	{
		public 数据表实例()
		{
			base.当前映射 = new 数据映射(base.数据类型 = typeof(T));
			base.数据表 = new Dictionary<int, 游戏数据>();
			base.检索表 = new Dictionary<string, 游戏数据>();
			数据快速检索 customAttribute;
			customAttribute = CustomAttributeExtensions.GetCustomAttribute<数据快速检索>(base.数据类型);
			if (customAttribute != null)
			{
				base.检索字段 = base.数据类型.GetField(customAttribute.检索字段, BindingFlags.Instance | BindingFlags.Public);
			}
			if (base.数据类型 == typeof(行会数据))
			{
				base.当前索引 = 1610612736;
			}
			if (base.数据类型 == typeof(队伍数据))
			{
				base.当前索引 = 1879048192;
			}
		}

		public override void 添加数据(游戏数据 数据, bool 分配索引 = false)
		{
			if (分配索引)
			{
				数据.数据索引.V = ++base.当前索引;
			}
			if (数据.数据索引.V == 0)
			{
				MessageBox.Show("数据表添加数据异常, 索引为零.");
			}
			数据.数据存表 = this;
			base.数据表.Add(数据.数据索引.V, 数据);
			if (base.检索字段 != null)
			{
				base.检索表.Add((base.检索字段.GetValue(数据) as 数据监视器<string>).V, 数据);
			}
			if (!游戏数据网关.已经修改)
			{
				游戏数据网关.已经修改 = true;
			}
		}

		public override void 删除数据(游戏数据 数据)
		{
			base.数据表.Remove(数据.数据索引.V);
			if (base.检索字段 != null)
			{
				base.检索表.Remove((base.检索字段.GetValue(数据) as 数据监视器<string>).V);
			}
			if (!游戏数据网关.已经修改)
			{
				游戏数据网关.已经修改 = true;
			}
		}

		public override void 保存数据()
		{
			foreach (KeyValuePair<int, 游戏数据> item in base.数据表.ToList())
			{
				if (!base.版本一致 || item.Value.已经修改)
				{
					item.Value.保存数据();
				}
			}
			base.版本一致 = true;
		}

		public override void 强制保存()
		{
			foreach (KeyValuePair<int, 游戏数据> item in base.数据表.ToList())
			{
				item.Value.保存数据();
			}
			base.版本一致 = true;
		}

		public override void 加载数据(byte[] 存表数据, 数据映射 历史映射)
		{
			base.版本一致 = 历史映射.检查映射版本(base.当前映射);
			using MemoryStream input = new MemoryStream(存表数据);
			using BinaryReader binaryReader = new BinaryReader(input);
			base.当前索引 = binaryReader.ReadInt32();
			int num;
			num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				T val;
				val = new T
				{
					数据存表 = this
				};
				int num2;
				num2 = binaryReader.ReadInt32();
				if (num2 <= 0)
				{
					主程.添加系统日志($"{base.数据类型.Name}.无效,  len: {num2}");
				}
				else
				{
					try
					{
						val.原始数据 = binaryReader.ReadBytes(num2);
					}
					catch (Exception ex)
					{
						val.原始数据 = null;
						主程.添加系统日志($"{base.数据类型.Name}.加载数据 , Len:{num2} 线程ID:{Thread.CurrentThread.ManagedThreadId} e:{ex.Message} {((ex.InnerException != null) ? (".IE:" + ex.InnerException.Message) : "...")}");
					}
				}
				val.加载数据(历史映射);
				if (val.数据索引 != null)
				{
					base.数据表[val.数据索引.V] = val;
					if (base.检索字段 != null && base.检索字段.GetValue(val) is 数据监视器<string> { V: not null } 数据监视器2)
					{
						base.检索表[数据监视器2.V] = val;
					}
				}
			}
			主程.添加系统日志($"{base.数据类型.Name}已经加载,  数量: {num}");
		}

		public override byte[] 存表数据()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(base.当前索引);
			binaryWriter.Write(base.数据表.Count);
			foreach (KeyValuePair<int, 游戏数据> item in base.数据表)
			{
				item.Value.导出数据(binaryWriter);
			}
			memoryStream.Seek(4L, SeekOrigin.Begin);
			return memoryStream.ToArray();
		}
	}
}
