using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace 游戏服务器.工具类
{
	public class InIReader
	{
		private readonly List<string> _contents;

		private readonly string _fileName;

		public InIReader(string fileName)
		{
			this._fileName = fileName;
			this._contents = new List<string>();
			try
			{
				if (File.Exists(this._fileName))
				{
					this._contents.AddRange(File.ReadAllLines(this._fileName));
				}
			}
			catch
			{
			}
		}

		private string FindValue(string section, string key)
		{
			for (int i = 0; i < this._contents.Count; i++)
			{
				if (string.CompareOrdinal(this._contents[i], "[" + section + "]") != 0)
				{
					continue;
				}
				for (int j = i + 1; j < this._contents.Count; j++)
				{
					if (string.CompareOrdinal(this._contents[j].Split('=')[0], key) != 0)
					{
						if (this._contents[j].StartsWith("[") && this._contents[j].EndsWith("]"))
						{
							return null;
						}
						continue;
					}
					return this._contents[j].Split('=')[1];
				}
			}
			return null;
		}

		private int FindIndex(string section, string key)
		{
			for (int i = 0; i < this._contents.Count; i++)
			{
				if (string.CompareOrdinal(this._contents[i], "[" + section + "]") != 0)
				{
					continue;
				}
				for (int j = i + 1; j < this._contents.Count; j++)
				{
					if (string.CompareOrdinal(this._contents[j].Split('=')[0], key) != 0)
					{
						if (!this._contents[j].StartsWith("[") || !this._contents[j].EndsWith("]"))
						{
							if (this._contents.Count - 1 == j)
							{
								this._contents.Add(key + "=");
								return this._contents.Count - 1;
							}
							continue;
						}
						this._contents.Insert(j - 1, key + "=");
						return j - 1;
					}
					return j;
				}
			}
			if (this._contents.Count > 0)
			{
				this._contents.Add("");
			}
			this._contents.Add("[" + section + "]");
			this._contents.Add(key + "=");
			return this._contents.Count - 1;
		}

		public void Save()
		{
			try
			{
				File.WriteAllLines(this._fileName, this._contents);
			}
			catch
			{
			}
		}

		public bool ReadBoolean(string section, string key, bool Default)
		{
			if (!bool.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public byte ReadByte(string section, string key, byte Default)
		{
			if (!byte.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public sbyte ReadSByte(string section, string key, sbyte Default)
		{
			if (!sbyte.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public ushort ReadUInt16(string section, string key, ushort Default)
		{
			if (!ushort.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public short ReadInt16(string section, string key, short Default)
		{
			if (!short.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public uint ReadUInt32(string section, string key, uint Default)
		{
			if (!uint.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public int ReadInt32(string section, string key, int Default, bool writeWhenNull = true)
		{
			if (!int.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				if (writeWhenNull)
				{
					this.Write(section, key, Default);
				}
			}
			return result;
		}

		public ulong ReadUInt64(string section, string key, ulong Default)
		{
			if (!ulong.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public long ReadInt64(string section, string key, long Default)
		{
			if (!long.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public float ReadSingle(string section, string key, float Default)
		{
			if (!float.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public double ReadDouble(string section, string key, double Default)
		{
			if (!double.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public decimal ReadDecimal(string section, string key, decimal Default)
		{
			if (!decimal.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public string ReadString(string section, string key, string Default)
		{
			string text;
			text = this.FindValue(section, key);
			if (string.IsNullOrEmpty(text))
			{
				text = Default;
				this.Write(section, key, Default);
			}
			return text;
		}

		public char ReadChar(string section, string key, char Default)
		{
			if (!char.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public Point ReadPoint(string section, string key, Point Default)
		{
			string text;
			text = this.FindValue(section, key);
			if (text != null && int.TryParse(text.Split(',')[0], out var result))
			{
				if (!int.TryParse(text.Split(',')[1], out var result2))
				{
					this.Write(section, key, Default);
					return Default;
				}
				return new Point(result, result2);
			}
			this.Write(section, key, Default);
			return Default;
		}

		public Size ReadSize(string section, string key, Size Default)
		{
			string text;
			text = this.FindValue(section, key);
			if (!int.TryParse(text.Split(',')[0], out var result))
			{
				this.Write(section, key, Default);
				return Default;
			}
			if (!int.TryParse(text.Split(',')[1], out var result2))
			{
				this.Write(section, key, Default);
				return Default;
			}
			return new Size(result, result2);
		}

		public TimeSpan ReadTimeSpan(string section, string key, TimeSpan Default)
		{
			if (!TimeSpan.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public float ReadFloat(string section, string key, float Default)
		{
			if (!float.TryParse(this.FindValue(section, key), out var result))
			{
				result = Default;
				this.Write(section, key, Default);
			}
			return result;
		}

		public void Write(string section, string key, bool value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, byte value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, sbyte value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, ushort value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, short value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, uint value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, int value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, ulong value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, long value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, float value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, double value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, decimal value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, string value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, char value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}

		public void Write(string section, string key, Point value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value.X + "," + value.Y;
			this.Save();
		}

		public void Write(string section, string key, Size value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value.Width + "," + value.Height;
			this.Save();
		}

		public void Write(string section, string key, TimeSpan value)
		{
			this._contents[this.FindIndex(section, key)] = key + "=" + value;
			this.Save();
		}
	}
}
