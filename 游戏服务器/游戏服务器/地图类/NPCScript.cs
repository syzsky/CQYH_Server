using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using 游戏服务器.网络类;

namespace 游戏服务器.地图类
{
	public class NPCScript
	{
		public readonly int ScriptID;

		public readonly int LoadedObjectID;

		public readonly NPCScriptType Type;

		public readonly string FileName;

		public const string MainKey = "[@MAIN]";

		public const string BuyKey = "[@BUY]";

		public const string SellKey = "[@SELL]";

		public const string BuySellKey = "[@BUYSELL]";

		public const string RepairKey = "[@REPAIR]";

		public const string SRepairKey = "[@SREPAIR]";

		public const string RefineKey = "[@REFINE]";

		public const string RefineCheckKey = "[@REFINECHECK]";

		public const string RefineCollectKey = "[@REFINECOLLECT]";

		public const string ReplaceWedRingKey = "[@REPLACEWEDDINGRING]";

		public const string BuyBackKey = "[@BUYBACK]";

		public const string StorageKey = "[@STORAGE]";

		public const string ConsignKey = "[@CONSIGN]";

		public const string MarketKey = "[@MARKET]";

		public const string CraftKey = "[@CRAFT]";

		public const string GuildCreateKey = "[@CREATEGUILD]";

		public const string RequestWarKey = "[@REQUESTWAR]";

		public const string SendParcelKey = "[@SENDPARCEL]";

		public const string CollectParcelKey = "[@COLLECTPARCEL]";

		public const string AwakeningKey = "[@AWAKENING]";

		public const string DisassembleKey = "[@DISASSEMBLE]";

		public const string DowngradeKey = "[@DOWNGRADE]";

		public const string ResetKey = "[@RESET]";

		public const string PearlBuyKey = "[@PEARLBUY]";

		public const string BuyUsedKey = "[@BUYUSED]";

		public const string BuyNewKey = "[@BUYNEW]";

		public const string BuySellNewKey = "[@BUYSELLNEW]";

		public const string HeroCreateKey = "[@CREATEHERO]";

		public const string HeroManageKey = "[@MANAGEHERO]";

		public const string TradeKey = "[TRADE]";

		public const string RecipeKey = "[RECIPE]";

		public const string TypeKey = "[TYPES]";

		public const string UsedTypeKey = "[USEDTYPES]";

		public const string QuestKey = "[QUESTS]";

		public const string SpeechKey = "[SPEECH]";

		public List<NPCPage> NPCSections = new List<NPCPage>();

		public List<NPCPage> NPCPages = new List<NPCPage>();

		public static NPCScript Get(int index)
		{
			return 主程.Scripts[index];
		}

		public static NPCScript GetOrAdd(int loadedObjectID, string fileName, NPCScriptType type)
		{
			NPCScript value;
			value = 主程.Scripts.SingleOrDefault((KeyValuePair<int, NPCScript> x) => x.Value.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase) && x.Value.LoadedObjectID == loadedObjectID).Value;
			if (value != null)
			{
				return value;
			}
			return new NPCScript(loadedObjectID, fileName, type);
		}

		private NPCScript(int loadedObjectID, string fileName, NPCScriptType type)
		{
			this.ScriptID = ++主程.ScriptIndex;
			this.LoadedObjectID = loadedObjectID;
			this.FileName = fileName;
			this.Type = type;
			this.Load();
			主程.Scripts.Add(this.ScriptID, this);
		}

		public void Load()
		{
			this.LoadInfo();
		}

		public void LoadInfo()
		{
			this.ClearInfo();
			if (!Directory.Exists(Settings.NPCPath))
			{
				return;
			}
			string path;
			path = Path.Combine(Settings.NPCPath, this.FileName + ".txt");
			if (File.Exists(path))
			{
				List<string> lines;
				lines = this.ParseInclude(this.ParseInsert(File.ReadAllLines(path).ToList()));
				NPCScriptType type;
				type = this.Type;
				if (type != 0 && (uint)(type - 2) <= 2u)
				{
					this.ParseDefault(lines);
				}
				else
				{
					this.ParseScript(lines);
				}
			}
			else
			{
				主程.添加系统日志($"脚本未找到: {this.FileName}");
			}
		}

		public void ClearInfo()
		{
			this.NPCPages = new List<NPCPage>();
		}

		private void ParseDefault(List<string> lines)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				if (lines[i].ToUpper().StartsWith("[@_"))
				{
					this.NPCPages.AddRange(this.ParsePages(lines, lines[i]));
				}
			}
		}

		private void ParseScript(IList<string> lines)
		{
			this.NPCPages.AddRange(this.ParsePages(lines));
		}

		private List<string> ParseInsert(List<string> lines)
		{
			List<string> collection;
			collection = new List<string>();
			for (int i = 0; i < lines.Count; i++)
			{
				if (!lines[i].ToUpper().StartsWith("#INSERT"))
				{
					continue;
				}
				string[] array;
				array = lines[i].Split(' ');
				if (array.Length >= 2)
				{
					string text;
					text = Path.Combine(Settings.EnvirPath, array[1].Substring(1, array[1].Length - 2));
					if (!File.Exists(text))
					{
						主程.添加系统日志($"插入脚本未找到: {text}");
					}
					else
					{
						collection = File.ReadAllLines(text).ToList();
					}
					lines.AddRange(collection);
				}
			}
			lines.RemoveAll((string str) => str.ToUpper().StartsWith("#INSERT"));
			return lines;
		}

		private List<string> ParseInclude(List<string> lines)
		{
			int num;
			num = 0;
			string text;
			List<string> list;
			while (true)
			{
				if (num < lines.Count)
				{
					if (lines[num].ToUpper().StartsWith("#CALL"))
					{
						string[] array;
						array = lines[num].Split(' ');
						if (array.Length < 3)
						{
							主程.添加系统日志(string.Format("#CALL 参数不够:" + lines[num]));
						}
						else
						{
							text = Path.Combine(Settings.EnvirPath, array[1]);
							string value;
							value = ("[" + array[2] + "]").ToUpper();
							string text2;
							text2 = "@_" + text.Replace(":", "_").Replace("\\", "_").Replace("/", "_")
								.Replace(".", "_") + array[2];
							bool flag;
							flag = false;
							bool flag2;
							flag2 = false;
							list = new List<string>();
							if (!File.Exists(text))
							{
								break;
							}
							IList<string> list2;
							list2 = File.ReadAllLines(text);
							for (int i = 0; i < list2.Count; i++)
							{
								if (!list2[i].ToUpper().StartsWith(value))
								{
									continue;
								}
								for (int j = i + 1; j < list2.Count; j++)
								{
									if (list2[j].Trim() == "{")
									{
										flag = true;
										continue;
									}
									if (!(list2[j].Trim() == "}"))
									{
										list.Add(list2[j]);
										continue;
									}
									flag2 = true;
									break;
								}
							}
							if (flag && flag2)
							{
								if (this.LoadedObjectID == 主程.DefaultNPCID)
								{
									lines.Insert(num + 1, "sysgoto " + text2);
								}
								else
								{
									lines.Insert(num + 1, "goto " + text2);
								}
								list.Insert(0, ("[" + text2 + "]").ToUpper());
								lines.AddRange(list);
								list.Clear();
							}
							else
							{
								主程.添加系统日志("脚本CALL 失败:" + lines[num]);
							}
						}
					}
					num++;
					continue;
				}
				lines.RemoveAll((string str) => str.ToUpper().StartsWith("#CALL"));
				return lines;
			}
			主程.添加系统日志($"#CALL 脚本没有找到: {text}");
			return list;
		}

		private List<NPCPage> ParsePages(IList<string> lines, string key = "[@MAIN]")
		{
			List<NPCPage> list;
			list = new List<NPCPage>();
			List<string> list2;
			list2 = new List<string>();
			NPCPage nPCPage;
			nPCPage = this.ParsePage(lines, key);
			list.Add(nPCPage);
			list2.AddRange(nPCPage.Buttons);
			for (int i = 0; i < list2.Count; i++)
			{
				string section;
				section = list2[i];
				if (!list.Any((NPCPage t) => t.Key.ToUpper() == section.ToUpper()))
				{
					nPCPage = this.ParsePage(lines, section);
					list2.AddRange(nPCPage.Buttons);
					list.Add(nPCPage);
				}
			}
			return list;
		}

		private NPCPage ParsePage(IList<string> scriptLines, string sectionName)
		{
			bool flag;
			flag = false;
			bool flag2;
			flag2 = false;
			List<string> list;
			list = scriptLines.Where((string x) => !string.IsNullOrEmpty(x)).ToList();
			NPCPage nPCPage;
			nPCPage = new NPCPage(sectionName);
			string text;
			text = nPCPage.ArgumentParse(sectionName);
			int num;
			num = 0;
			while (true)
			{
				if (num < list.Count)
				{
					if (!list[num].StartsWith(";") && list[num].ToUpper().StartsWith(text.ToUpper()))
					{
						break;
					}
					num++;
					continue;
				}
				return nPCPage;
			}
			List<string> list2;
			list2 = new List<string>();
			flag = false;
			for (int i = num + 1; i < list.Count; i++)
			{
				string text2;
				text2 = list[i];
				text2 = ((i >= list.Count - 1) ? "" : list[i + 1]);
				if (text2.StartsWith("[") && text2.EndsWith("]"))
				{
					flag = true;
				}
				else if (text2.StartsWith("#IF") || text2.StartsWith("#OR"))
				{
					flag2 = true;
				}
				if (flag2 || flag)
				{
					list2.Add(list[i]);
					if (list2.Count > 0)
					{
						NPCSegment nPCSegment;
						nPCSegment = this.ParseSegment(nPCPage, list2);
						List<string> list3;
						list3 = new List<string>();
						list3.AddRange(nPCSegment.Buttons);
						list3.AddRange(nPCSegment.ElseButtons);
						list3.AddRange(nPCSegment.GotoButtons);
						if (text2.StartsWith("[@") && !text2.StartsWith("[@_"))
						{
							list3.Add(text2);
						}
						nPCPage.Buttons.AddRange(list3);
						nPCPage.SegmentList.Add(nPCSegment);
						list2.Clear();
						flag2 = false;
					}
					if (flag)
					{
						break;
					}
				}
				else
				{
					list2.Add(list[i]);
				}
			}
			if (list2.Count > 0)
			{
				NPCSegment nPCSegment2;
				nPCSegment2 = this.ParseSegment(nPCPage, list2);
				List<string> list4;
				list4 = new List<string>();
				list4.AddRange(nPCSegment2.Buttons);
				list4.AddRange(nPCSegment2.ElseButtons);
				list4.AddRange(nPCSegment2.GotoButtons);
				nPCPage.Buttons.AddRange(list4);
				nPCPage.SegmentList.Add(nPCSegment2);
				list2.Clear();
			}
			return nPCPage;
		}

		private NPCSegment ParseSegment(NPCPage page, IEnumerable<string> scriptLines)
		{
			List<string> list;
			list = new List<string>();
			List<string> list2;
			list2 = new List<string>();
			List<string> list3;
			list3 = new List<string>();
			List<string> list4;
			list4 = new List<string>();
			List<string> list5;
			list5 = new List<string>();
			List<string> list6;
			list6 = new List<string>();
			List<string> list7;
			list7 = new List<string>();
			List<string> list8;
			list8 = new List<string>();
			List<string> list9;
			list9 = scriptLines.ToList();
			List<string> list10;
			list10 = list3;
			List<string> list11;
			list11 = list4;
			bool orMode;
			orMode = false;
			for (int i = 0; i < list9.Count; i++)
			{
				if (string.IsNullOrEmpty(list9[i]) || list9[i].StartsWith(";"))
				{
					continue;
				}
				if (list9[i].StartsWith("#"))
				{
					switch (list9[i].Remove(0, 1).ToUpper().Trim()
						.Split(' ')[0])
					{
					case "ELSEACT":
						list10 = list6;
						list11 = list8;
						break;
					case "ELSESAY":
						list10 = list5;
						list11 = list7;
						break;
					case "ACT":
						list10 = list2;
						list11 = list8;
						break;
					case "SAY":
						list10 = list3;
						list11 = list4;
						break;
					case "OR":
						list10 = list;
						list11 = null;
						orMode = true;
						break;
					case "IF":
						list10 = list;
						list11 = null;
						break;
					default:
						throw new NotImplementedException();
					}
					continue;
				}
				if (list9[i].StartsWith("[") && list9[i].EndsWith("]"))
				{
					break;
				}
				if (list11 != null)
				{
					string[] array;
					array = list9[i].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (array.Count() > 1)
					{
						switch (array[0].ToUpper())
						{
						case "ROLLDIE":
						case "ROLLYUT":
							list4.Add($"[{array[1].ToUpper()}]");
							break;
						case "GOTO":
						case "GROUPGOTO":
							list8.Add($"[{array[1].ToUpper()}]");
							break;
						case "TIMERECALLGROUP":
						case "DELAYGOTO":
						case "TIMERECALL":
							if (array.Length > 2)
							{
								list8.Add($"[{array[2].ToUpper()}]");
							}
							break;
						}
					}
				}
				list10.Add(list9[i].TrimEnd());
			}
			NPCSegment nPCSegment;
			nPCSegment = new NPCSegment(page, list3, list4, list5, list7, list8);
			for (int j = 0; j < list.Count; j++)
			{
				nPCSegment.ParseCheck(list[j], orMode);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				nPCSegment.ParseAct(nPCSegment.ActList, list2[k]);
			}
			for (int l = 0; l < list6.Count; l++)
			{
				nPCSegment.ParseAct(nPCSegment.ElseActList, list6[l]);
			}
			list11 = new List<string>();
			list11.AddRange(list4);
			list11.AddRange(list7);
			list11.AddRange(list8);
			return nPCSegment;
		}

		public void Call(怪物实例 monster, string key)
		{
			key = key.ToUpper();
			for (int i = 0; i < this.NPCPages.Count; i++)
			{
				NPCPage nPCPage;
				nPCPage = this.NPCPages[i];
				if (!string.Equals(nPCPage.Key, key, StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}
				nPCPage.BreakFromSegments = false;
				foreach (NPCSegment segment in nPCPage.SegmentList)
				{
					if (!nPCPage.BreakFromSegments)
					{
						this.ProcessSegment(monster, nPCPage, segment);
						continue;
					}
					nPCPage.BreakFromSegments = false;
					break;
				}
			}
		}

		public void Call(string key)
		{
			key = key.ToUpper();
			for (int i = 0; i < this.NPCPages.Count; i++)
			{
				NPCPage nPCPage;
				nPCPage = this.NPCPages[i];
				if (!string.Equals(nPCPage.Key, key, StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}
				nPCPage.BreakFromSegments = false;
				foreach (NPCSegment segment in nPCPage.SegmentList)
				{
					if (!nPCPage.BreakFromSegments)
					{
						this.ProcessSegment(nPCPage, segment);
						continue;
					}
					nPCPage.BreakFromSegments = false;
					break;
				}
			}
		}

		public void Call(玩家实例 player, int objectID, string key)
		{
			key = key.ToUpper();
			if (!player.NPCDelayed)
			{
				if (key != "[@MAIN]" && player.NPCObjectID != objectID)
				{
					return;
				}
			}
			else
			{
				player.NPCDelayed = false;
			}
			for (int i = 0; i < this.NPCPages.Count; i++)
			{
				NPCPage nPCPage;
				nPCPage = this.NPCPages[i];
				if (!string.Equals(nPCPage.Key, key, StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}
				player.NPCSpeech = new List<string>();
				player.NPCSuccess.Clear();
				nPCPage.BreakFromSegments = false;
				foreach (NPCSegment segment in nPCPage.SegmentList)
				{
					if (!nPCPage.BreakFromSegments)
					{
						this.ProcessSegment(player, nPCPage, segment, objectID);
						continue;
					}
					nPCPage.BreakFromSegments = false;
					break;
				}
				this.Response(player, nPCPage);
			}
			player.NPCData.Remove("NPCInputStr");
		}

		private void Response(玩家实例 player, NPCPage page)
		{
			if (player.NPCSpeech.Count > 0)
			{
				player.网络连接?.发送封包(new 同步交互结果
				{
					对象编号 = ((player.对话守卫 != null) ? player.对话守卫.地图编号 : 0),
					交互文本 = Encoding.UTF8.GetBytes(player.NPCSpeech[0] + "\0")
				});
			}
		}

		private void ProcessSegment(玩家实例 player, NPCPage page, NPCSegment segment, int objectID)
		{
			if (objectID != 主程.DefaultNPC.LoadedObjectID)
			{
				player.NPCObjectID = objectID;
				player.NPCScriptID = this.ScriptID;
			}
			player.NPCSuccess.Add(segment, segment.Check(player));
			player.NPCPage = page;
		}

		private void ProcessSegment(怪物实例 monster, NPCPage page, NPCSegment segment)
		{
			segment.Check(monster);
		}

		private void ProcessSegment(NPCPage page, NPCSegment segment)
		{
			segment.Check();
		}
	}
}
