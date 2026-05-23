using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace 账号服务器
{
	internal sealed class 安全类型绑定器 : ISerializationBinder
	{
		private static readonly HashSet<string> 允许程序集 = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"账号服务器", "Assembly-CSharp"
		};

		public Type BindToType(string assemblyName, string typeName)
		{
			string asmShort = (assemblyName ?? string.Empty).Split(',')[0].Trim();
			if (!string.IsNullOrEmpty(asmShort) && !允许程序集.Contains(asmShort))
			{
				throw new System.Runtime.Serialization.SerializationException(
					"拒绝反序列化未授权类型: " + typeName + " from " + assemblyName);
			}
			string fullName = string.IsNullOrEmpty(assemblyName) ? typeName : (typeName + ", " + assemblyName);
			Type resolved = Type.GetType(fullName);
			if (resolved == null)
			{
				throw new System.Runtime.Serialization.SerializationException(
					"无法解析反序列化类型: " + fullName);
			}
			return resolved;
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = serializedType.Assembly.GetName().Name;
			typeName = serializedType.FullName;
		}
	}

	public static class 序列化类
	{
		private static readonly JsonSerializerSettings 全局设置;

		static 序列化类()
		{
			全局设置 = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameHandling = TypeNameHandling.Auto,
				SerializationBinder = new 安全类型绑定器(),
				Formatting = Formatting.Indented
			};
		}

		public static string 序列化(object O)
		{
			return JsonConvert.SerializeObject(O, 全局设置);
		}

		public static object[] 反序列化(string 文件夹, Type 类型)
		{
			List<object> list = new List<object>();
			// 仅扫描 .txt, 避免 desktop.ini / .DS_Store 等系统文件触发反序列化失败.
			FileInfo[] files = new DirectoryInfo(文件夹).GetFiles("*.txt");
			for (int i = 0; i < files.Length; i++)
			{
				object obj = JsonConvert.DeserializeObject(File.ReadAllText(files[i].FullName), 类型, 全局设置);
				if (obj != null)
				{
					list.Add(obj);
				}
			}
			return list.ToArray();
		}
	}
}
