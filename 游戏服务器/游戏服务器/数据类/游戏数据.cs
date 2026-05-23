using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace 游戏服务器.数据类
{
	public abstract class 游戏数据
	{
		public readonly 数据监视器<int> 数据索引;

		public readonly Type 数据类型;

		public readonly MemoryStream 内存流;

		public readonly BinaryWriter 写入流;

		public byte[] 原始数据 { get; set; }

		public bool 已经修改 { get; set; }

		public 数据表基类 数据存表 { get; set; }

		protected void 创建字段()
		{
			FieldInfo[] fields;
			fields = base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType.IsGenericType)
				{
					Type genericTypeDefinition;
					genericTypeDefinition = fieldInfo.FieldType.GetGenericTypeDefinition();
					if (!(genericTypeDefinition != typeof(数据监视器<>)) || !(genericTypeDefinition != typeof(列表监视器<>)) || !(genericTypeDefinition != typeof(哈希监视器<>)) || !(genericTypeDefinition != typeof(字典监视器<, >)))
					{
						fieldInfo.SetValue(this, Activator.CreateInstance(fieldInfo.FieldType, this));
					}
				}
			}
		}

		public override string ToString()
		{
			return this.数据类型?.Name;
		}

		public 游戏数据()
		{
			this.数据类型 = base.GetType();
			this.内存流 = new MemoryStream();
			this.写入流 = new BinaryWriter(this.内存流);
			this.创建字段();
		}

		public void 保存数据()
		{
			this.内存流.SetLength(0L);
			foreach (数据字段 item in this.数据存表.当前映射.字段列表)
			{
				item.保存字段内容(this.写入流, item.字段详情.GetValue(this));
			}
			this.原始数据 = this.内存流.ToArray();
			if (this.已经修改)
			{
				this.已经修改 = false;
			}
		}

		public void 导出数据(BinaryWriter binaryWriter)
		{
			if (this.已经修改 || this.原始数据 == null)
			{
				try
				{
					if (this.原始数据 == null)
					{
						主程.添加系统日志(this.数据类型.Name + "导出数据>>null: " + this.数据索引.V);
					}
					this.保存数据();
				}
				catch (Exception ex)
				{
					主程.添加系统日志($"{this.数据类型.Name}.加载数据 , 索引:{this.数据索引.V.ToString()} 线程ID:{Thread.CurrentThread.ManagedThreadId} e:{ex.Message} {((ex.InnerException != null) ? (".IE:" + ex.InnerException.Message) : "...")}");
					binaryWriter.Write(0);
					return;
				}
			}
			binaryWriter.Write(this.原始数据.Length);
			binaryWriter.Write(this.原始数据);
		}

		public void 加载数据(数据映射 历史映射)
		{
			if (this.原始数据 == null)
			{
				return;
			}
			using MemoryStream input = new MemoryStream(this.原始数据);
			using BinaryReader 读取流 = new BinaryReader(input);
			foreach (数据字段 item in 历史映射.字段列表)
			{
				if (item.字段类型 == null)
				{
					主程.添加系统日志(item.字段类型Name + "字段类型>>null: " + item.ToString());
					continue;
				}
				object value;
				value = item.读取字段内容(读取流, this, item);
				if (!(item.字段详情 == null) && item.字段类型 == item.字段详情.FieldType)
				{
					item.字段详情.SetValue(this, value);
				}
			}
		}

		public virtual void 删除数据()
		{
			this.数据存表?.删除数据(this);
		}

		public virtual void 加载完成()
		{
		}
	}
}
