using System;

namespace _0002_0002_0003_0002_0001_0006_0001_0001_0006_0002_0004_0002
{
	public class _0002_0010_000A_0017 : _0009_0003_0004_0003_0009_0004_0006_0007
	{
		public override string _njhgkl2dbgn()
		{
			return base.GetType().Name;
		}

		public override void _ejlzkxjv31i(_0005_0015_000E_000E_0012_0009 vmContext, _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004 instruction, out _0003_0003_000B_000D_000D_0006_0001_0009 state)
		{
			state = _0003_0003_000B_000D_000D_0006_0001_0009._0002_0010_000A_0017;
			vmContext._0006_0005_0001_0002_0006_0001_0006_0001_0006_0003_0005_0003++;
		}
	}
	internal class _000D_000B_001E_000B_000A_0003 : Exception
	{
		private Exception _0006_0016_0012_0017_0011_0008;

		public _000D_000B_001E_000B_000A_0003(Exception e)
		{
			this._0006_0016_0012_0017_0011_0008 = e;
		}

		public Exception _0013_0004_000C_0019_001A_000B()
		{
			return this._0006_0016_0012_0017_0011_0008;
		}
	}
}
