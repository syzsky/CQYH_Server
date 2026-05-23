using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace 游戏服务器.数据类
{
	public sealed class 数据映射
	{
		public Type 数据类型;

		public List<数据字段> 字段列表 { get; }

		public override string ToString()
		{
			return this.数据类型?.Name;
		}

		public 数据映射(BinaryReader 读取流)
		{
			this.字段列表 = new List<数据字段>();
			string name;
			name = 读取流.ReadString();
			this.数据类型 = Assembly.GetEntryAssembly().GetType(name) ?? Assembly.GetCallingAssembly().GetType(name);
			int num;
			num = 读取流.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				this.字段列表.Add(new 数据字段(读取流, this.数据类型));
			}
		}

		public 数据映射(Type 数据类型)
		{
			this.字段列表 = new List<数据字段>();
			this.数据类型 = 数据类型;
			FieldInfo[] fields;
			fields = this.数据类型.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType.IsGenericType)
				{
					Type genericTypeDefinition;
					genericTypeDefinition = fieldInfo.FieldType.GetGenericTypeDefinition();
					if (!(genericTypeDefinition != typeof(数据监视器<>)) || !(genericTypeDefinition != typeof(列表监视器<>)) || !(genericTypeDefinition != typeof(哈希监视器<>)) || !(genericTypeDefinition != typeof(字典监视器<, >)))
					{
						this.字段列表.Add(new 数据字段(fieldInfo));
					}
				}
			}
		}

		public void 保存映射描述(BinaryWriter 写入流)
		{
			写入流.Write(this.数据类型.FullName);
			写入流.Write(this.字段列表.Count);
			foreach (数据字段 item in this.字段列表)
			{
				item.保存字段描述(写入流);
			}
		}

		public bool 检查映射版本(数据映射 对比映射)
		{
			if (this.字段列表.Count != 对比映射.字段列表.Count)
			{
				return false;
			}
			for (int i = 0; i < this.字段列表.Count; i++)
			{
				if (!this.字段列表[i].检查字段版本(对比映射.字段列表[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
