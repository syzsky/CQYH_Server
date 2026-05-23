using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
    public sealed class 添加金币 : GM命令
    {
        [字段描述(0, 排序 = 0)]
        public string 角色名字;

        [字段描述(0, 排序 = 1)]
        public uint 金币数量;

        public override 执行方式 执行方式 => 执行方式.优先后台执行;

        public override void 执行命令()
        {
            if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
            {
                if ((uint)(-1 - (int)角色数据.金币数量) < this.金币数量)
                {
                    主程.添加系统日志($"{base.GetType().Name} 金币相加后溢出,最多 {-1 - (int)角色数据.金币数量}");
                    return;
                }
                角色数据.金币数量 += this.金币数量;
                主程.添加货币日志(角色数据, "命令添加金币", 游戏货币.金币, this.金币数量);
                角色数据.网络连接?.发送封包(new 货币数量变动
                {
                    货币类型 = 1,
                    货币数量 = 角色数据.金币数量
                });
                主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 当前金币数量: {角色数据.金币数量}");
                //主程.WebLog(LogDataType.CommandSendingLog, Settings.统计UUID代码, Settings.游戏区服名称, "系统发送", this.角色名字, 角色数据.所属账号.V.账号名字.V, this.金币数量.ToString(), "金币");
            }
            else
            {
                主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
            }
        }
    }
}
