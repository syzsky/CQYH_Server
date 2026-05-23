using System.Collections.Generic;
using System.Net;

namespace 游戏服务器
{
	public struct WebData
	{
		public Dictionary<string, string> Data;

		public HttpListenerResponse Respons;

		public WebDataType Type;
	}
}
