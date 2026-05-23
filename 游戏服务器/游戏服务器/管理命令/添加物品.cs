using System;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
    public sealed class 添加物品 : GM命令
    {
        [字段描述(0, 排序 = 0)]
        public string 角色名字;

        [字段描述(0, 排序 = 1)]
        public string 物品名字;

        [字段描述(0, 排序 = 2, 可选 = true)]
        public int? 物品数量;

        [字段描述(0, 排序 = 3, 可选 = true)]
        public bool? 是否绑定;

        public override 执行方式 执行方式 => 执行方式.优先后台执行;

        public override void 执行命令()
        {
            if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
            {
                if (!游戏物品.检索表.TryGetValue(this.物品名字, out var value2))
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 物品不存在");
                    return;
                }
                if (value2.物品持久 == 0)
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 不能添加物品");
                    return;
                }
                int num;
                num = Math.Max(1, (value2.持久类型 == 物品持久分类.堆叠) ? 1 : (this.物品数量 ?? 1));
                if (角色数据.角色背包.Count + num > 角色数据.背包大小.V)
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色背包已满");
                    return;
                }
                if (value2.持久类型 == 物品持久分类.堆叠)
                {
                    byte b;
                    b = byte.MaxValue;
                    byte b2;
                    b2 = 0;
                    while (b2 < 角色数据.背包大小.V)
                    {
                        if (角色数据.角色背包.ContainsKey(b2))
                        {
                            b2++;
                            continue;
                        }
                        b = b2;
                        break;
                    }
                    角色数据.角色背包[b] = new 物品数据(value2, 角色数据, 1, b, this.物品数量 ?? 1, this.是否绑定.GetValueOrDefault(), 角色数据.角色名字.V + "-@添加物品");
                    角色数据.网络连接?.发送封包(new 玩家物品变动
                    {
                        物品描述 = 角色数据.角色背包[b].字节描述()
                    });
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        byte b3;
                        b3 = byte.MaxValue;
                        byte b4;
                        b4 = 0;
                        while (b4 < 角色数据.背包大小.V)
                        {
                            if (角色数据.角色背包.ContainsKey(b4))
                            {
                                b4++;
                                continue;
                            }
                            b3 = b4;
                            break;
                        }
                        if (value2 is 游戏装备 模板)
                        {
                            角色数据.角色背包[b3] = new 装备数据(模板, 角色数据, 1, b3, 随机生成: false, this.是否绑定.GetValueOrDefault(), 角色数据.角色名字.V + "-@添加物品");
                        }
                        else if (value2.持久类型 == 物品持久分类.容器)
                        {
                            角色数据.角色背包[b3] = new 物品数据(value2, 角色数据, 1, b3, 0, this.是否绑定.GetValueOrDefault(), 角色数据.角色名字.V + "-@添加物品");
                        }
                        else
                        {
                            角色数据.角色背包[b3] = new 物品数据(value2, 角色数据, 1, b3, value2.物品持久, this.是否绑定.GetValueOrDefault(), 角色数据.角色名字.V + "-@添加物品");
                        }
                        角色数据.角色背包[b3].当前持久.V = 角色数据.角色背包[b3].最大持久.V;
                        角色数据.网络连接?.发送封包(new 玩家物品变动
                        {
                            物品描述 = 角色数据.角色背包[b3].字节描述()
                        });
                    }
                }
                主程.添加命令日志("<= @" + base.GetType().Name + " 命令已经执行, 物品已经添加到角色背包");
                //主程.WebLog(LogDataType.MakederLog, Settings.统计UUID代码, Settings.游戏区服名称, this.角色名字, this.物品名字, (this.物品数量 ?? 1).ToString());
            }
            else
            {
                主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
            }
        }
    }
}
