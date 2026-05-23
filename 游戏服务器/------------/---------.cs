using System;
using System.Reflection;

namespace _0002_0002_0003_0002_0001_0006_0001_0001_0006_0002_0004_0002
{
	public class _0009_0006_0005_0007_0007_0005_0009_0003_0005_0003 : _0009_0003_0004_0003_0009_0004_0006_0007
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
			Type type;
			type = null;
			type = ((methodBase.IsConstructor || methodBase.Name == ".cctor") ? typeof(void) : ((MethodInfo)methodBase).ReturnType);
			_0012_0003_0003_0012_0006_000F_000A_0002 obj2;
			obj2 = null;
			object obj3;
			obj3 = null;
			if (!methodBase.IsStatic)
			{
				obj2 = vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0006_000C_0007_0002_000C_000D();
				obj3 = obj2._0009_0018_0001_0008_0004_0009();
			}
			object obj4;
			obj4 = methodBase.Invoke(obj3, array);
			if ((object)type != typeof(void))
			{
				if (obj4 != null)
				{
					vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(obj4);
				}
				else
				{
					vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(new _0012_0003_0003_0012_0006_000F_000A_0002(null));
				}
			}
			if (!methodBase.IsStatic && obj2._0001_0003_0004_0003_0002_0002_0004_0001_0002_0001_0003_0001_0002_0003() == 1)
			{
				vmContext._0001_000E_000C_000E_0007_000D_0001_0002._0008_0007_000D_000C_000C_0007_0010_0008(obj2._0014_0015_000A_0007_0012_000B(), new _0012_0003_0003_0012_0006_000F_000A_0002(obj3));
			}
			obj._0003_000B_0001_0017_0004_0011(vmContext, array);
			state = _0003_0003_000B_000D_000D_0006_0001_0009._000B_000D_0007_0015_0011_000F;
			vmContext._0006_0005_0001_0002_0006_0001_0006_0001_0006_0003_0005_0003++;
		}
	}
	public class _0009_0006_0008_0002_0008_0005_0003_0004_0001_0002 : _0009_0003_0004_0003_0009_0004_0006_0007
	{
		public override string _njhgkl2dbgn()
		{
			return base.GetType().Name;
		}

		public override void _ejlzkxjv31i(_0005_0015_000E_000E_0012_0009 vmContext, _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004 instruction, out _0003_0003_000B_000D_000D_0006_0001_0009 state)
		{
			int metadataToken;
			metadataToken = (int)instruction._0012_000A_0014_000A_0017_000A._0009_0018_0001_0008_0004_0009();
			Type type;
			type = typeof(_0009_0006_0008_0002_0008_0005_0003_0004_0001_0002).Module.ResolveType(metadataToken);
			if (!type.IsValueType)
			{
				vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(null);
			}
			else
			{
				object obj;
				obj = Activator.CreateInstance(type);
				vmContext._0002_0002_0005_0002_0006_0002_0003_0002_0005_0003._0002_0003_0004_000F_0003_0001_0009_0002(obj);
			}
			state = _0003_0003_000B_000D_000D_0006_0001_0009._000B_000D_0007_0015_0011_000F;
			vmContext._0006_0005_0001_0002_0006_0001_0006_0001_0006_0003_0005_0003++;
		}
	}
	public class _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004
	{
		public _0018_0001_0006_000D_0012_0017 _0007_0014_0006_0011_0009_0015;

		public _0012_0003_0003_0012_0006_000F_000A_0002 _0012_000A_0014_000A_0017_000A;

		public int _0007_0017_000C_0013_0011_0019;

		public uint _0010_000D_0015_000F_0004_000C;

		public _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004(_0018_0001_0006_000D_0012_0017 opcode, _0012_0003_0003_0012_0006_000F_000A_0002 value, int offset, uint flags)
		{
			this._0007_0017_000C_0013_0011_0019 = offset;
			this._0010_000D_0015_000F_0004_000C = flags;
			this._0007_0014_0006_0011_0009_0015 = opcode;
			if (value == null)
			{
				this._0012_000A_0014_000A_0017_000A = null;
			}
			else
			{
				this._0012_000A_0014_000A_0017_000A = value;
			}
		}

		public _0009_0003_0007_0007_0007_0005_0007_0004_0007_0004(_0018_0001_0006_000D_0012_0017 opcode, int offset, uint flags)
		{
			this._0007_0017_000C_0013_0011_0019 = offset;
			this._0010_000D_0015_000F_0004_000C = flags;
			this._0007_0014_0006_0011_0009_0015 = opcode;
			this._0012_000A_0014_000A_0017_000A = null;
		}
	}
}
