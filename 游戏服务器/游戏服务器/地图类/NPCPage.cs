using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace 游戏服务器.地图类
{
	public class NPCPage
	{
		public string Key;

		public List<NPCSegment> SegmentList = new List<NPCSegment>();

		public List<string> Args = new List<string>();

		public List<string> Buttons = new List<string>();

		public List<int> ScriptCalls = new List<int>();

		public bool BreakFromSegments;

		public NPCPage(string key)
		{
			this.Key = key;
		}

		public override string ToString()
		{
			return this.Key;
		}

		public string ArgumentParse(string key)
		{
			if (key.StartsWith("[@_"))
			{
				return key;
			}
			Regex regex;
			regex = new Regex("\\((.*)\\)");
			Match match;
			match = regex.Match(key);
			if (!match.Success)
			{
				return key;
			}
			key = Regex.Replace(key, regex.ToString(), "()");
			string[] array;
			array = match.Groups[1].Value.Split(',');
			this.Args = new List<string>();
			string[] array2;
			array2 = array;
			foreach (string item in array2)
			{
				this.Args.Add(item);
			}
			return key;
		}
	}
}
