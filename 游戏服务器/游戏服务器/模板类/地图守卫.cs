using System.Collections.Generic;
using System.IO;

namespace 游戏服务器.模板类
{
    public sealed class 地图守卫
    {
        public static Dictionary<ushort, 地图守卫> 数据表;

        public string 守卫名字;

        public ushort 守卫编号;

        public byte 守卫等级;

        public bool 虚无状态;

        public bool 能否受伤;

        public int 尸体保留;

        public int 复活间隔;

        public bool 主动攻击;

        public byte 仇恨范围;

        public string 普攻技能;

        public int 商店编号;

        public string 界面代码;

        public bool 执行脚本;

        public bool 禁止复活;

        public string 脚本路径;

        //public bool 触发lua;
        public bool 触发lua { get; set; }

        public static void 载入数据()
        {
            地图守卫.数据表 = new Dictionary<ushort, 地图守卫>();
            string text;
            text = Settings.游戏数据目录 + "\\System\\Npc数据\\守卫数据\\";
            if (!Directory.Exists(text))
            {
                return;
            }
            object[] array;
            array = 序列化类.反序列化(text, typeof(地图守卫));
            foreach (object obj in array)
            {
                if (!地图守卫.数据表.ContainsKey(((地图守卫)obj).守卫编号))
                {
                    地图守卫.数据表.Add(((地图守卫)obj).守卫编号, (地图守卫)obj);
                }
            }
        }
    }
}
