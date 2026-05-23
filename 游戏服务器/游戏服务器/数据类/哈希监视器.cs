using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace 游戏服务器.数据类
{
	public sealed class 哈希监视器<T> : IEnumerable<T>, IEnumerable
	{
		public delegate void 更改委托(List<T> 更改列表);

		private readonly HashSet<T> v;

		private readonly 游戏数据 对应数据;

		public int Count => this.v.Count;

		public ISet<T> ISet => this.v;

		public event 更改委托 更改事件;

		public 哈希监视器(游戏数据 数据)
		{
			this.v = new HashSet<T>();
			this.对应数据 = 数据;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.v.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.v).GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.v.GetEnumerator();
		}

		public override string ToString()
		{
			return this.v?.Count.ToString();
		}

		private void 设置状态()
		{
			if (this.对应数据 != null)
			{
				if (!this.对应数据.已经修改)
				{
					this.对应数据.已经修改 = true;
				}
				if (!游戏数据网关.已经修改)
				{
					游戏数据网关.已经修改 = true;
				}
			}
		}

		public void Clear()
		{
			if (this.v.Count > 0)
			{
				this.v.Clear();
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
			}
		}

		public bool Add(T Tv)
		{
			if (this.v.Add(Tv))
			{
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
				return true;
			}
			return false;
		}

		public bool Remove(T Tv)
		{
			if (this.v.Remove(Tv))
			{
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
				return true;
			}
			return false;
		}

		public void QuietlyAdd(T Tv)
		{
			this.v.Add(Tv);
		}

		public bool Contains(T Tv)
		{
			return this.v.Contains(Tv);
		}
	}
}
