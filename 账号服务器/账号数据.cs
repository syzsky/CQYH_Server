using System;
using System.Security.Cryptography;
using System.Text;

namespace 账号服务器
{
	public sealed class 账号数据
	{
		private static readonly RandomNumberGenerator 安全随机 = RandomNumberGenerator.Create();

		// 常量时间字符串比较, 防止密码/密保答案被时序攻击逐字节还原.
		// 长度不同直接返回 false (长度本身不构成可放大的口令信息泄漏).
		public static bool 安全比较(string a, string b)
		{
			if (a == null || b == null) return false;
			byte[] ba = Encoding.UTF8.GetBytes(a);
			byte[] bb = Encoding.UTF8.GetBytes(b);
			if (ba.Length != bb.Length) return false;
			return CryptographicOperations.FixedTimeEquals(ba, bb);
		}

		// 强密码: 必须至少包含 2 种字符类别 (小写/大写/数字/特殊).
		// 拦掉 "123456" "abcdef" "ABCDEF" 这类极弱口令 (MISC-02).
		public static bool 是强密码(string pwd)
		{
			if (string.IsNullOrEmpty(pwd)) return false;
			int 类别 = 0;
			bool 有小写 = false, 有大写 = false, 有数字 = false, 有特殊 = false;
			foreach (char c in pwd)
			{
				if (!有小写 && c >= 'a' && c <= 'z') { 有小写 = true; 类别++; }
				else if (!有大写 && c >= 'A' && c <= 'Z') { 有大写 = true; 类别++; }
				else if (!有数字 && c >= '0' && c <= '9') { 有数字 = true; 类别++; }
				else if (!有特殊 && !char.IsLetterOrDigit(c)) { 有特殊 = true; 类别++; }
				if (类别 >= 2) return true;
			}
			return false;
		}

		private static char[] RandomChars = new char[62]
		{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
		'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
		'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
		'y', 'z'
		};

		public string 账号名字;

		public string 账号密码;

		public string 密保问题;

		public string 密保答案;

		public DateTime 创建日期;

		public static string 生成门票()
		{
			// 用 GetInt32 做拒绝-重抽, 消除 256 % 62 = 8 带来的模偏置 (LOW-M).
			char[] chars = new char[32];
			for (int i = 0; i < 32; i++)
			{
				chars[i] = RandomChars[RandomNumberGenerator.GetInt32(0, RandomChars.Length)];
			}
			return "ULS21-" + new string(chars);
		}

		public 账号数据(string 账号, string 密码, string 问题, string 答案)
		{
			账号名字 = 账号;
			账号密码 = 密码;
			密保问题 = 问题;
			密保答案 = 答案;
			创建日期 = DateTime.Now;
		}
	}
}
