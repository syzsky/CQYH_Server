using System;
using System.Drawing;
using System.Text;

namespace 游戏登录器
{
	public static class 图形验证
	{
		public static string 验证码;
		public static Bitmap 生成验证码()
		{
			Bitmap bitmap = new Bitmap(200, 60);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
			Font font = new Font(FontFamily.GenericSerif, 48f, FontStyle.Bold, GraphicsUnit.Pixel);
			Random random = new Random();
			string text = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 5; i++)
			{
				string text2 = text.Substring(random.Next(0, text.Length - 1), 1);
				stringBuilder.Append(text2);
				graphics.DrawString(text2, font, new SolidBrush(Color.Black), i * 38, random.Next(0, 15));
			}
			验证码 = stringBuilder.ToString();
			Pen pen = new Pen(new SolidBrush(Color.Black), 2f);
			for (int j = 0; j < 6; j++)
			{
				graphics.DrawLine(pen, new Point(random.Next(0, 199), random.Next(0, 59)), new Point(random.Next(0, 199), random.Next(0, 59)));
			}
			return bitmap;
		}
	}
}
