using 游戏服务器.地图类;

namespace 游戏服务器.模板类
{
	public class 掉落条件
	{
		public int VarId;

		public int Value;

		public string cmp;

		public bool Test(int Avalue)
		{
			if (this.VarId > 0)
			{
				return NPCSegment.Compare(this.cmp, Avalue, this.Value);
			}
			return true;
		}
	}
}
