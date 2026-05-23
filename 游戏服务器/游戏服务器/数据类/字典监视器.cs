using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace 游戏服务器.数据类
{
	public sealed class 字典监视器<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>, IEnumerable
	{
		public delegate void 更改委托(List<KeyValuePair<TK, TV>> 更改字典);

		public delegate void 更改委托KV(TK K, TV V);

		private readonly Dictionary<TK, TV> v;

		private readonly 游戏数据 对应数据;

		public TV this[TK key]
		{
			get
			{
				if (!this.v.TryGetValue(key, out var value))
				{
					return default(TV);
				}
				return value;
			}
			set
			{
				this.v[key] = value;
				this.更改事件?.Invoke(this.v.ToList());
				this.更改KV事件?.Invoke(key, value);
				this.设置状态();
			}
		}

		public ICollection<TK> Keys => this.v.Keys;

		public ICollection<TV> Values => this.v.Values;

		public IDictionary IDictionary_0 => this.v;

		public int Count => this.v.Count;

		public event 更改委托 更改事件;

		public event 更改委托KV 更改KV事件;

		public 字典监视器(游戏数据 数据)
		{
			this.v = new Dictionary<TK, TV>();
			this.对应数据 = 数据;
		}

		public bool ContainsKey(TK k)
		{
			return this.v.ContainsKey(k);
		}

		public bool TryGetValue(TK k, out TV v)
		{
			return this.v.TryGetValue(k, out v);
		}

		public void Add(TK key, TV value)
		{
			this.v.Add(key, value);
			this.更改事件?.Invoke(this.v.ToList());
			this.设置状态();
		}

		public bool Remove(TK key)
		{
			if (this.v.Remove(key))
			{
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
				return true;
			}
			return false;
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

		public void QuietlyAdd(TK key, TV value)
		{
			this.v.Add(key, value);
		}

		public void QuietlySet(TK key, TV value)
		{
			if (this.v.ContainsKey(key))
			{
				this.v[key] = value;
			}
		}

		public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
		{
			return this.v.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.v).GetEnumerator();
		}

		IEnumerator<KeyValuePair<TK, TV>> IEnumerable<KeyValuePair<TK, TV>>.GetEnumerator()
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
	}
}
