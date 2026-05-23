using System;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
    public class 刷 : GM在线命令
    {
        [字段描述(0)]
        public string 物品名字;

        [字段描述(0, 排序 = 1, 可选 = true)]
        public int? 物品数量;

        public override 执行方式 执行方式 => 执行方式.优先后台执行;

        public override void 执行命令()
        {
        }

        public override void 执行命令(玩家实例 管理员)
        {
            try
            {
                游戏物品 value;
                value = null;
                if (int.TryParse(this.物品名字, out var result))
                {
                    游戏物品.数据表.TryGetValue(result, out value);
                }
                else
                {
                    游戏物品.检索表.TryGetValue(this.物品名字, out value);
                }
                if (value == null)
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 物品不存在");
                    管理员.发送系统消息("命令执行失败, 物品[" + this.物品名字 + "]不存在");
                    return;
                }
                if (value.物品持久 == 0)
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 不能添加物品");
                    管理员.发送系统消息("命令执行失败, 不能添加物品");
                    return;
                }
                int num;
                num = Math.Max(1, (value.持久类型 == 物品持久分类.堆叠) ? 1 : (this.物品数量 ?? 1));
                if (管理员.角色数据.角色背包.Count + num > 管理员.角色数据.背包大小.V)
                {
                    主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色背包已满");
                    管理员.发送系统消息("命令执行失败, 角色背包已满");
                    return;
                }
                if (value.持久类型 == 物品持久分类.堆叠)
                {
                    byte b;
                    b = byte.MaxValue;
                    byte b2;
                    b2 = 0;
                    while (b2 < 管理员.角色数据.背包大小.V)
                    {
                        if (管理员.角色数据.角色背包.ContainsKey(b2))
                        {
                            b2++;
                            continue;
                        }
                        b = b2;
                        break;
                    }
                    管理员.角色数据.角色背包[b] = new 物品数据(value, 管理员.角色数据, 1, b, this.物品数量 ?? 1, 绑定: false, 管理员.对象名字 + "-@刷");
                    管理员.角色数据.网络连接?.发送封包(new 玩家物品变动
                    {
                        物品描述 = 管理员.角色数据.角色背包[b].字节描述()
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
                        while (b4 < 管理员.角色数据.背包大小.V)
                        {
                            if (管理员.角色数据.角色背包.ContainsKey(b4))
                            {
                                b4++;
                                continue;
                            }
                            b3 = b4;
                            break;
                        }
                        if (value is 游戏装备 模板)
                        {
                            管理员.角色数据.角色背包[b3] = new 装备数据(模板, 管理员.角色数据, 1, b3, 随机生成: true, 绑定: false, 管理员.对象名字 + "-@刷");
                        }
                        else if (value.持久类型 == 物品持久分类.容器)
                        {
                            管理员.角色数据.角色背包[b3] = new 物品数据(value, 管理员.角色数据, 1, b3, 0, 绑定: false, 管理员.对象名字 + "-@刷");
                        }
                        else
                        {
                            管理员.角色数据.角色背包[b3] = new 物品数据(value, 管理员.角色数据, 1, b3, value.物品持久, 绑定: false, 管理员.对象名字 + "-@刷");
                        }
                        管理员.角色数据.角色背包[b3].当前持久.V = 管理员.角色数据.角色背包[b3].最大持久.V;
                        管理员.角色数据.网络连接?.发送封包(new 玩家物品变动
                        {
                            物品描述 = 管理员.角色数据.角色背包[b3].字节描述()
                        });
                    }
                }
                //主程.WebLog(LogDataType.MakederLog, Settings.统计UUID代码, Settings.游戏区服名称, 管理员.对象名字, this.物品名字, (this.物品数量 ?? 1).ToString());
            }
            catch (Exception)
            {
                主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 出现异常");
                管理员.发送系统消息("<= @" + base.GetType().Name + " 命令执行失败, 出现异常");
            }
        }
    }
}
