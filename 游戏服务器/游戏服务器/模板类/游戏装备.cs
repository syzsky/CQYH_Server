using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace 游戏服务器.模板类
{
	public class 游戏装备 : 游戏物品
	{
		[Category("装备特殊")]
		public bool 死亡销毁 { get; set; }

		[Category("装备特殊")]
		public bool 禁止卸下 { get; set; }

		[Category("装备特殊")]
		public bool 能否修理 { get; set; }

		[Category("装备属性")]
		public int 修理花费 { get; set; }

		[Category("装备属性")]
		public int 特修花费 { get; set; }

		[Category("装备条件")]
		public int 需要攻击 { get; set; }

		[Category("装备条件")]
		public int 需要魔法 { get; set; }

		[Category("装备条件")]
		public int 需要道术 { get; set; }

		[Category("装备条件")]
		public int 需要刺术 { get; set; }

		[Category("装备条件")]
		public int 需要弓术 { get; set; }

		[Category("装备属性")]
		public int 基础战力 { get; set; }

		[Category("装备属性-攻击")]
		public int 最小攻击 { get; set; }

		[Category("装备属性-攻击")]
		public int 最大攻击 { get; set; }

		[Category("装备属性-攻击")]
		public int 最小魔法 { get; set; }

		[Category("装备属性-攻击")]
		public int 最大魔法 { get; set; }

		[Category("装备属性-攻击")]
		public int 最小道术 { get; set; }

		[Category("装备属性-攻击")]
		public int 最大道术 { get; set; }

		[Category("装备属性-攻击")]
		public int 最小刺术 { get; set; }

		[Category("装备属性-攻击")]
		public int 最大刺术 { get; set; }

		[Category("装备属性-攻击")]
		public int 最小弓术 { get; set; }

		[Category("装备属性-攻击")]
		public int 最大弓术 { get; set; }

		[Category("装备属性-防御")]
		public int 最小防御 { get; set; }

		[Category("装备属性-防御")]
		public int 最大防御 { get; set; }

		[Category("装备属性-防御")]
		public int 最小魔防 { get; set; }

		[Category("装备属性-防御")]
		public int 最大魔防 { get; set; }

		[Category("装备属性-基础")]
		public int 最大体力 { get; set; }

		[Category("装备属性-基础")]
		public int 最大魔力 { get; set; }

		[Category("装备属性-基础")]
		public int 物理准确 { get; set; }

		[Category("装备属性-基础")]
		public int 物理敏捷 { get; set; }

		[Category("装备属性-基础")]
		public int 攻击速度 { get; set; }

		[Category("装备属性-基础")]
		public int 魔法闪避 { get; set; }

		[Category("装备打孔")]
		public bool 能否打孔 { get; set; }

		[Category("装备打孔")]
		public int 打孔上限 { get; set; }

		[Category("装备打孔")]
		public int 一孔花费 { get; set; }

		[Category("装备打孔")]
		public int 二孔花费 { get; set; }

		[Category("装备打孔")]
		public int 三孔花费 { get; set; }

		[Category("暂未启用")]
		public int 重铸灵石 { get; set; }

		[Category("暂未启用")]
		public int 灵石数量 { get; set; }

		[Category("暂未启用")]
		public int 精炼等级 { get; set; }

		[Category("天生极品")]
		public int 特殊属性一 { get; set; }

		[Category("天生极品")]
		public int 特殊属性二 { get; set; }

		[Category("天生极品")]
		public int 特殊属性三 { get; set; }

		[Category("天生极品")]
		public int 特殊属性四 { get; set; }

		[Category("暂未启用")]
		public bool 能否刻印 { get; set; }

		[Category("暂未启用")]
		public int 灵魂绑定 { get; set; }

		[Category("暂未启用")]
		public int 神圣次数 { get; set; }

		[Category("套装系统")]
		public 游戏装备套装 装备套装 { get; set; }

		[Category("暂未启用")]
		public 游戏对象职业 铭文职业 { get; set; }

		[Browsable(false)]
		[JsonIgnore]
		public List<游戏套装> 参与套装 { get; set; }

		public override string ToString()
		{
			return $"{base.物品编号}-{base.物品名字}";
		}
	}
}
