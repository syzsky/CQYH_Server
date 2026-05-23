using System.Collections.Generic;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
    public sealed class 元宝奖励 : GM命令
    {
        [字段描述(0, 排序 = 0)]
        public uint 数量;

        public override 执行方式 执行方式 => 执行方式.前台立即执行;

        public override void 执行命令()
        {
            foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
            {
                item.Value.修改货币("+", 游戏货币.元宝, this.数量 * 100);
                主程.添加货币日志(item.Value, "命令添加元宝", 游戏货币.元宝, this.数量 * 100);
                //主程.WebLog(LogDataType.CommandSendingLog, Settings.统计UUID代码, Settings.游戏区服名称, "系统发送", item.Value.对象名字, item.Value.所属账号.账号名字.V, (this.数量 * 100).ToString(), "元宝");
            }
        }
    }
}
