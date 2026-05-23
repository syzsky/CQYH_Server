using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace 游戏服务器.数据类
{
	public sealed class 列表监视器<T> : IEnumerable<T>, IEnumerable
	{
		public delegate void 更改委托(List<T> 更改列表);

		private List<T> v;

		private readonly 游戏数据 对应数据;

		public T Last
		{
			get
			{
				if (this.v.Count != 0)
				{
					return this.v.Last();
				}
				return default(T);
			}
		}

		public T this[int 索引]
		{
			get
			{
				if (索引 >= this.v.Count)
				{
					return default(T);
				}
				return this.v[索引];
			}
			set
			{
				if (索引 < this.v.Count)
				{
					this.v[索引] = value;
					this.更改事件?.Invoke(this.v.ToList());
					this.设置状态();
				}
			}
		}

		public IList IList => this.v;

		public int Count => this.v.Count;

		public event 更改委托 更改事件;

		public 列表监视器(游戏数据 数据)
		{
			this.v = new List<T>();
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

		public List<T> GetRange(int index, int count)
		{
			return this.v.GetRange(index, count);
		}

		public void Add(T Tv)
		{
			this.v.Add(Tv);
			this.更改事件?.Invoke(this.v.ToList());
			this.设置状态();
		}

		public void Insert(int index, T Tv)
		{
			this.v.Insert(index, Tv);
			this.更改事件?.Invoke(this.v.ToList());
			this.设置状态();
		}

		public void Remove(T Tv)
		{
			if (this.v.Remove(Tv))
			{
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
			}
		}

		public void RemoveAt(int i)
		{
			if (this.v.Count > i)
			{
				this.v.RemoveAt(i);
				this.更改事件?.Invoke(this.v.ToList());
				this.设置状态();
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

		public void SetValue(List<T> Lv)
		{
			this.v = Lv;
			this.更改事件?.Invoke(this.v.ToList());
			this.设置状态();
		}

		public void QuietlyAdd(T Tv)
		{
			this.v.Add(Tv);
		}
	}
}
