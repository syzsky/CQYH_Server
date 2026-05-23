namespace 游戏服务器.数据类
{
	public sealed class 数据监视器<T>
	{
		public delegate void 更改委托(T 更改数据);

		private T v;

		private readonly 游戏数据 对应数据;

		public T V
		{
			get
			{
				return this.v;
			}
			set
			{
				this.v = value;
				this.更改事件?.Invoke(value);
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

		public event 更改委托 更改事件;

		public void QuietlySetValue(T value)
		{
			this.v = value;
		}

		public 数据监视器(游戏数据 数据)
		{
			this.对应数据 = 数据;
		}

		public override string ToString()
		{
			return this.v?.ToString();
		}
	}
}
