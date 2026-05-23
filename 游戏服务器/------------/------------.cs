using System;
using System.Collections.Generic;
using System.Reflection;

namespace _0002_0002_0003_0002_0001_0006_0001_0001_0006_0002_0004_0002
{
	public class _0002_0002_0002_0003_0003_0003_0005_0005_0001_0005_0002_0003 : _0009_0003_0004_0003_0009_0004_0006_0007
	{
		public override string _njhgkl2dbgn()
		{
			return base.GetType().Name;
		}

		public override void _ejlzkxjv31i(_0005_0015_000E_000E_0012_0009 vmContext, _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004 instruction, out _0003_0003_000B_000D_000D_0006_0001_0009 state)
		{
			MethodBase methodBase;
			methodBase = base._0008_0006_0005_0001_0007_0008_0009_0002_0003_0002((int)instruction._0012_000A_0014_000A_0017_000A._0009_0018_0001_0008_0004_0009());
			_000D_0007_000E_0008_000B_0010 obj;
			obj = _0009_0003_0004_0003_0009_0004_0006_0007._0012_0001_0009_000D_0015_0001(vmContext, methodBase.GetParameters());
			object[] array;
			array = obj._0002_000A_0008_0003_0003_0007_0007_0006(methodBase.GetParameters());
			object obj2;
			obj2 = null;
			obj2 = ((!(methodBase is ConstructorInfo constructorInfo)) ? methodBase.Invoke(null, array) : (methodBase.IsStatic ? constructorInfo.Invoke(null, array) : ((array != null) ? constructorInfo.Invoke(array) : (((object)constructorInfo.DeclaringType.GetMethod(".cctor") == null) ? constructorInfo.Invoke(array) : Activator.CreateInstance(constructorInfo.DeclaringType, null)))));
			obj._0003_000B_0001_0017_0004_0011(vmContext, array);
			if (obj2 != null)
			{
				vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(obj2);
			}
			state = _0003_0003_000B_000D_000D_0006_0001_0009._000B_000D_0007_0015_0011_000F;
			vmContext._0006_0005_0001_0002_0006_0001_0006_0001_0006_0003_0005_0003++;
		}
	}
	public class _0004_0004_0001_0005_0005_0004_0001_0006_0002_0002_0004_0004 : _0009_0003_0004_0003_0009_0004_0006_0007
	{
		public override string _njhgkl2dbgn()
		{
			return base.GetType().Name;
		}

		public override void _ejlzkxjv31i(_0005_0015_000E_000E_0012_0009 vmContext, _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004 instruction, out _0003_0003_000B_000D_000D_0006_0001_0009 state)
		{
			int metadataToken;
			metadataToken = (int)instruction._0012_000A_0014_000A_0017_000A._0009_0018_0001_0008_0004_0009();
			vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(typeof(_0004_0004_0001_0005_0005_0004_0001_0006_0002_0002_0004_0004).Module.ResolveString(metadataToken));
			state = _0003_0003_000B_000D_000D_0006_0001_0009._000B_000D_0007_0015_0011_000F;
			vmContext._0006_0005_0001_0002_0006_0001_0006_0001_0006_0003_0005_0003++;
		}
	}
	public class _0006_0002_0002_0002_0005_0003_0005_0002_0001_0002_0005_0006
	{
		public Dictionary<int, _0012_0003_0003_0012_0006_000F_000A_0002> Args;

		public _0006_0002_0002_0002_0005_0003_0005_0002_0001_0002_0005_0006()
		{
			this.Args = new Dictionary<int, _0012_0003_0003_0012_0006_000F_000A_0002>();
		}

		public _0006_0002_0002_0002_0005_0003_0005_0002_0001_0002_0005_0006(object[] pm)
		{
			this.Args = new Dictionary<int, _0012_0003_0003_0012_0006_000F_000A_0002>();
			for (int i = 0; i < pm.Length; i++)
			{
				this.Args[i] = new _0012_0003_0003_0012_0006_000F_000A_0002(pm[i], i, 2);
			}
		}

		public void _0008_0007_000D_000C_000C_0007_0010_0008(int _0007_0014_0005_000C_000F_0016, _0012_0003_0003_0012_0006_000F_000A_0002 _0005_0001_0002_0003_0007_0009_0005_0006)
		{
			if (_0005_0001_0002_0003_0007_0009_0005_0006 != null)
			{
				this.Args[_0007_0014_0005_000C_000F_0016] = new _0012_0003_0003_0012_0006_000F_000A_0002(_0005_0001_0002_0003_0007_0009_0005_0006._0009_0018_0001_0008_0004_0009(), _0007_0014_0005_000C_000F_0016, 2);
			}
			else
			{
				this.Args[_0007_0014_0005_000C_000F_0016] = new _0012_0003_0003_0012_0006_000F_000A_0002(null, _0007_0014_0005_000C_000F_0016, 2);
			}
		}

		public _0012_0003_0003_0012_0006_000F_000A_0002 _0013_0004_000C_0019_001A_000B(int _0007_0014_0005_000C_000F_0016)
		{
			return this.Args[_0007_0014_0005_000C_000F_0016];
		}
	}
}
