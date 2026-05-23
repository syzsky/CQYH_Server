using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace LicenseTool
{
	public class LicenseInfo
	{
		public string machineCode = "";

		public int year;

		public int month;

		public int day;

		public bool isReadDone;

		public void LoadFromFile(string fileName)
		{
			if (File.Exists(fileName))
			{
				string text;
				text = this.Decrypt(File.ReadAllBytes(fileName));
				if (!string.IsNullOrWhiteSpace(text))
				{
					string[] array;
					array = text.Split("|");
					this.machineCode = array[0];
					string[] array2;
					array2 = array[1].Split(",");
					this.year = int.Parse(array2[0]);
					this.month = int.Parse(array2[1]);
					this.day = int.Parse(array2[2]);
					this.isReadDone = true;
				}
			}
		}

		public void SaveToFile(string fileName)
		{
			string s;
			s = $"{this.machineCode}|{this.year},{this.month},{this.day}";
			byte[] bytes;
			bytes = Encoding.UTF8.GetBytes(s);
			byte[] array;
			array = new byte[bytes.Length];
			for (int i = 0; i < bytes.Length; i++)
			{
				array[i] = (byte)(bytes[i] ^ 5u);
			}
			Random random;
			random = new Random();
			byte[] array2;
			array2 = new byte[65535];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = (byte)random.Next();
			}
			FileStream fileStream;
			fileStream = new FileStream(fileName, FileMode.Create);
			fileStream.Write(array);
			fileStream.Write(array2);
			fileStream.Close();
			fileStream.Dispose();
		}

		private string Decrypt(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				try
				{
					string xmlString;
					xmlString = "<RSAKeyValue><Modulus>7F47P0n68gnCdEp6Wu98oT5DC88w/EKF72WWHG4MFmPR2CS+IuYCAHdN6c1Gyy0EHEtWNpaYZvByRBoj4BBpGsyk05HOJynMUmm9IxsnsJMiirZwl1+RjhECLXuNqiK/KuHYJ6Ob5072BXeimah+A0nL8i9SSOUT5TjW+8KFgn0=</Modulus><Exponent>AQAB</Exponent><P>+V+YD/pQyzi7L0O+BMiP/jO7iehc5ONFnICH3Wv29cBrvyYsZ5efnbHyFMvDCHCSIphvqyoPvKNBQpFCqvYolw==</P><Q>8qYqnfz3sFGHu5gPuhPYflF6Jq99+pEXtiq0o7PWP8PaHsD8Lap1y8uFG9XbSkNita9hj8px7WZxvDQGa3DcCw==</Q><DP>CX5hLKKL9uCnB6qdjlMQYE4Z4qss9i3M1aNzCLP2h+6Wa4WJhTwZgIhove/v8d9PQO/quGp2hOj2MBGVpyqN6w==</DP><DQ>ujRxt9OMb63hL0A9sVnRZP45cr5xOntlatHS1V0IKN6u37LQ0mphAwcnGnk+UvrrIOl5QNLmInvfA8IYuxJciQ==</DQ><InverseQ>g6raXFuIc0xmkJebRaLe041AfSDdd1WNjQ37O8zxPlEZQrPjEn/Q/wAhz75wNTgH25pUEPuHXzLw+ThCeDtrlA==</InverseQ><D>WX+osWMiyOjXH09gGvSZXTiFDICsTKgnrKjreOJWY1fyigQHlpE+6sxBzSh0CNSHvOrtvwewtzGOIqk/MkUkdk3/DNcocfLLgrCJQ9gJjUc61Ym8MkvUQbOgmehJFyEgbrzaUMkvS2G3DzBld8VZF7T10cCVja2WQ8RnFR9FAGU=</D></RSAKeyValue>";
					using RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
					rSACryptoServiceProvider.FromXmlString(xmlString);
					int num;
					num = rSACryptoServiceProvider.KeySize / 8;
					if (data.Length > num)
					{
						using (MemoryStream memoryStream = new MemoryStream(data))
						{
							using MemoryStream memoryStream2 = new MemoryStream();
							byte[] array;
							array = new byte[num];
							for (int num2 = memoryStream.Read(array, 0, num); num2 > 0; num2 = memoryStream.Read(array, 0, num))
							{
								byte[] array2;
								array2 = new byte[num2];
								Array.Copy(array, 0, array2, 0, num2);
								byte[] array3;
								array3 = rSACryptoServiceProvider.Decrypt(array2, fOAEP: false);
								memoryStream2.Write(array3, 0, array3.Length);
							}
							return Encoding.UTF8.GetString(memoryStream2.ToArray());
						}
					}
					return Encoding.UTF8.GetString(rSACryptoServiceProvider.Decrypt(data, fOAEP: false));
				}
				catch (Exception)
				{
					return null;
				}
			}
			return null;
		}

		public static string GetThisMachineCode()
		{
			try
			{
				string result;
				result = "";
				foreach (ManagementObject instance in new ManagementClass("Win32_Processor").GetInstances())
				{
					result = instance.Properties["ProcessorId"].Value.ToString();
				}
				return result;
			}
			catch
			{
				return "unknow";
			}
		}
	}
}
