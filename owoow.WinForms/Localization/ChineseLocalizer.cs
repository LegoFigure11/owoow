using owoow.Core;
using PKHeX.Core;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace owoow.WinForms;

/// <summary>
/// Simplified-Chinese presentation layer.
/// Internal encounter keys remain in English so localization cannot change RNG logic.
/// Pokémon terminology is sourced from PKHeX's official Simplified-Chinese game strings.
/// </summary>
internal static partial class ChineseLocalizer
{
    public const string LanguageCode = "zh-Hans";

    private static readonly GameStrings English = GameInfo.GetStrings("en");
    public static readonly GameStrings Chinese = GameInfo.GetStrings(LanguageCode);

    private static readonly ConditionalWeakTable<Form, object> LocalizedForms = new();
    private static readonly Dictionary<string, string> GameTranslations = new(StringComparer.OrdinalIgnoreCase);

    private static readonly Dictionary<string, string> UiTranslations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["(None)"] = "（无）",
        ["(Any)"] = "（任意）",
        ["None"] = "无",
        ["Any"] = "任意",
        ["All"] = "全部",
        ["Ignore"] = "忽略",
        ["Yes"] = "是",
        ["No"] = "否",
        ["Success"] = "成功",
        ["Fail"] = "失败",
        ["Sword"] = "剑",
        ["Shield"] = "盾",

        ["Read Encounter"] = "读取遭遇",
        ["Copy IVs to Filters"] = "将个体值复制到筛选",
        ["Pokédex Recommendation Searcher"] = "图鉴推荐搜索器",
        ["Location List"] = "地点列表",
        ["Menu Close Timeline"] = "关闭菜单时间线",
        ["Profile Manager"] = "档案管理",
        ["Seed Reset Settings"] = "种子重置设置",
        ["Turbo Settings"] = "自动操作设置",
        ["Overworld Scanner"] = "野外扫描器",
        ["VideoFeed"] = "视频监视",
        ["VideoFeedLog"] = "视频监视日志",
        ["Rare EC?"] = "稀有 EC？",
        ["Play Tone?"] = "播放提示音？",
        ["Focus Window?"] = "聚焦窗口？",
        ["Filters Enabled?"] = "启用筛选？",
        ["Height:"] = "身高：",
        ["Aura:"] = "超强气场：",
        ["Mark:"] = "证章：",
        ["Shiny:"] = "闪光：",
        ["Ability:"] = "特性：",
        ["Nature:"] = "性格：",
        ["Gender:"] = "性别：",
        ["Species:"] = "宝可梦：",
        ["IVs:"] = "个体值：",
        ["HP:"] = "ＨＰ：",
        ["Atk:"] = "攻击：",
        ["Def:"] = "防御：",
        ["SpA:"] = "特攻：",
        ["SpD:"] = "特防：",
        ["Spe:"] = "速度：",

        ["CFW Tools"] = "自制系统工具",
        ["Cancel"] = "取消",
        ["Turbo Controls"] = "自动操作设置",
        ["Turbo"] = "自动操作",
        ["Adv."] = "推进",
        ["Adv.:"] = "推进：",
        ["Advances:"] = "推进数：",
        ["Days-"] = "日期−",
        ["Days+"] = "日期＋",
        ["Skip:"] = "跳过：",
        ["Settings"] = "设置",
        ["Reset for Seed"] = "按目标种子重置",
        ["Retail Tools"] = "正版机工具",
        ["Generate"] = "生成",
        ["Generate Single"] = "生成单个结果",
        ["Update Seeds"] = "更新种子",
        ["Anim.:"] = "动画：",
        ["Animations:"] = "动画：",
        ["Initial:"] = "初始：",
        ["Initial Adv."] = "初始推进",
        ["Retail Seed Finder"] = "正版机种子搜索",
        ["Pokédex Recommendation"] = "图鉴推荐",
        ["Refresh"] = "刷新",
        ["Advanced Settings"] = "高级设置",
        ["Advanced Mode"] = "高级模式",
        ["Fly NPCs:"] = "飞行 NPC 数：",
        ["Calculate Rain Ticks"] = "计算降雨消耗",
        ["Rain Ticks:"] = "降雨消耗：",
        ["Area Load:"] = "区域加载：",
        ["Fly?"] = "计入飞行？",
        ["Raining/Thunderstorming?"] = "下雨／雷雨？",

        ["Static"] = "固定现身",
        ["Symbol"] = "明雷",
        ["Hidden"] = "暗雷",
        ["Fishing"] = "垂钓",
        ["Encounter Settings - Static"] = "遭遇设置－固定现身",
        ["Encounter Settings - Symbol"] = "遭遇设置－明雷",
        ["Encounter Settings - Hidden"] = "遭遇设置－暗雷",
        ["Encounter Settings - Fishing"] = "遭遇设置－垂钓",
        ["Target:"] = "目标：",
        ["Target Species:"] = "目标宝可梦：",
        ["Weather:"] = "天气：",
        ["Area:"] = "地点：",
        ["Lead Ability:"] = "首发特性：",
        ["Max Step:"] = "最大步数：",
        ["Calibrate NPCs"] = "校准 NPC",
        ["Holding Direction?"] = "按住方向键？",
        ["Consider Menu Close?"] = "计入关闭菜单？",
        ["NPCs:"] = "NPC 数：",
        ["KOs:"] = "击倒数：",

        ["Status:"] = "状态：",
        ["Connect"] = "连接",
        ["Disconnect"] = "断开连接",
        ["Switch IP:"] = "Switch IP：",
        ["USB Port:"] = "USB 端口：",
        ["Game:"] = "游戏：",
        ["Mark Charm?"] = "证章护符？",
        ["Shiny Charm?"] = "闪耀护符？",
        ["Seed:"] = "种子：",
        ["Seed[0]:"] = "种子[0]：",
        ["Seed[1]:"] = "种子[1]：",
        ["State[0]:"] = "状态[0]：",
        ["State[1]:"] = "状态[1]：",
        ["EC"] = "EC",
        ["EC:"] = "EC：",
        ["PID"] = "PID",
        ["PID:"] = "PID：",
        ["TID:"] = "TID：",
        ["SID:"] = "SID：",
        ["X:"] = "X：",
        ["Y:"] = "Y：",
        ["Z:"] = "Z：",

        ["Copy Seeds to Clipboard"] = "复制种子到剪贴板",
        ["Set as Initial Seed"] = "设为初始种子",
        ["Set as Initial Advances"] = "设为初始推进数",
        ["Mark Advance"] = "标记推进数",
        ["Profiles"] = "配置档案",
        ["Encounter Lookup"] = "遭遇查询",
        ["Spread Finder"] = "个体值组合搜索",
        ["Loto-ID"] = "ID 抽奖",
        ["Cram-o-matic"] = "机器鹕",
        ["Watt Trader"] = "瓦特商店",
        ["Digging Pa"] = "挖挖伯",
        ["Digging Bro (Skill)"] = "挖洞兄弟（技巧型）",
        ["Wailord Respawn"] = "吼鲸王刷新",
        ["Xoroshiro Tools"] = "Xoroshiro 工具",
        ["Capture Card Monitor"] = "采集卡监视器",

        ["Add"] = "添加",
        ["Delete"] = "删除",
        ["Remove"] = "移除",
        ["Clear"] = "清空",
        ["Copy"] = "复制",
        ["Select"] = "选择",
        ["Update"] = "更新",
        ["Update Main Form"] = "更新主窗口",
        ["Search"] = "搜索",
        ["Search!"] = "搜索！",
        ["Calculate"] = "计算",
        ["Calculate Seed"] = "计算种子",
        ["Operation:"] = "操作：",
        ["Value:"] = "数值：",
        ["Distance:"] = "距离：",
        ["Input:"] = "输入：",
        ["Slot 1"] = "槽位 1",
        ["Slot 2"] = "槽位 2",
        ["Slot 3"] = "槽位 3",
        ["Slot 4"] = "槽位 4",
        ["Loop?"] = "循环？",
        ["Reset time after Date Skipping?"] = "跳日期后重置时间？",
        ["Time between inputs (ms):"] = "输入间隔（毫秒）：",
        ["Turn Screen Off?"] = "关闭屏幕？",
        ["Wait (100ms)"] = "等待（100 毫秒）",
        ["Wait (500ms)"] = "等待（500 毫秒）",
        ["Wait (1000ms)"] = "等待（1000 毫秒）",

        ["Name:"] = "名称：",
        ["ID:"] = "ID：",
        ["ID List"] = "ID 列表",
        ["Loaded IDs: 0"] = "已加载 ID：0",
        ["Excluded Maps: 0"] = "已排除地图：0",
        ["Manage  Excluded Maps"] = "管理排除地图",
        ["Map to ignore:"] = "要排除的地图：",
        ["Map:"] = "地图：",
        ["View Details:"] = "查看详情：",
        ["Friendship Steps:"] = "友好度步数：",
        ["Hatch Cycle Steps:"] = "孵化周期步数：",
        ["Player X:"] = "玩家 X：",
        ["Player Y:"] = "玩家 Y：",
        ["Player Z:"] = "玩家 Z：",

        ["Bonus Only?"] = "仅奖励结果？",
        ["Min. Watts:"] = "最少瓦特：",
        ["Slot Range:"] = "槽位范围：",
        ["Specific Slot:"] = "指定槽位：",
        ["Min Adv.:"] = "最小推进：",
        ["Max Adv.:"] = "最大推进：",
        ["Min Total Rewards:"] = "最少奖励总数：",
        ["Guaranteed IVs:"] = "保底满项个体值数：",
        ["Search Tasks:"] = "搜索任务数：",
        ["Item 1:"] = "道具 1：",
        ["Item 2:"] = "道具 2：",
        ["Item 3:"] = "道具 3：",
        ["Item 4:"] = "道具 4：",
        ["Species 1:"] = "宝可梦 1：",
        ["Species 2:"] = "宝可梦 2：",
        ["Species 3:"] = "宝可梦 3：",
        ["Species 4:"] = "宝可梦 4：",

        ["Enable Discord Webhooks?"] = "启用 Discord Webhook？",
        ["Result Message URLs:"] = "结果通知网址：",
        ["Error Message URLs:"] = "错误通知网址：",
        ["Result Found Webhook Message:"] = "找到结果时的 Webhook 消息：",
        ["Error Webhook Message:"] = "错误 Webhook 消息：",
        ["Test Webhooks"] = "测试 Webhook",
        ["Log results while Seed Resetting?"] = "重置种子时记录结果？",
        ["Avoid System Update?"] = "避开系统更新提示？",
        ["Keep same date"] = "保持当前日期",
        ["Extra time to open HOME Menu: "] = "打开 HOME 菜单的额外等待：",
        ["Extra time to close the game:"] = "关闭游戏的额外等待：",
        ["Extra time to load player profile:"] = "加载用户的额外等待：",
        ["Extra time to load the game:"] = "加载游戏的额外等待：",

        ["Select Video Source:"] = "选择视频源：",
        ["Start Feed"] = "启动画面",
        ["Stop Feed"] = "停止画面",
        ["Screenshot (Physical)"] = "截图（物理）",
        ["Screenshot (Special)"] = "截图（特殊）",
        ["Screenshot (Idle)"] = "截图（待机）",
        ["Load Image (Physical)"] = "载入图像（物理）",
        ["Load Image (Special)"] = "载入图像（特殊）",
        ["Load Image (Idle)"] = "载入图像（待机）",
        ["Monitor Animations"] = "监视动画",
        ["Stop Monitoring"] = "停止监视",
        ["Pin to top"] = "置顶",
        ["Acceptable Difference Threshold (px):"] = "允许的像素差阈值：",
        ["Match Cooldown (ms):"] = "匹配冷却（毫秒）：",
        ["Show CV Output"] = "显示 CV 输出",
        ["Show Logs"] = "显示日志",
        ["Log Accepts"] = "记录匹配",
        ["Log Rejections"] = "记录未匹配",

        ["Open download page"] = "打开下载页面",
        ["New update available!"] = "发现新版本！",
        ["It is advised to update as soon as possible!"] = "建议尽快更新到新版本。",
        ["Update available!"] = "有可用更新！",

        ["All Weather"] = "全部天气",
        ["Normal Weather"] = "晴朗",
        ["Overcast"] = "阴天",
        ["Raining"] = "下雨",
        ["Thunderstorm"] = "雷雨",
        ["Intense Sun"] = "烈日",
        ["Snowing"] = "下雪",
        ["Snowstorm"] = "暴风雪",
        ["Sandstorm"] = "沙暴",
        ["Heavy Fog"] = "雾",

        ["Brilliant"] = "超强气场",
        ["Any Mark"] = "任意证章",
        ["Any Personality"] = "任意个性之证",
        ["Personality/Rare"] = "个性／未知之证",
        ["Any (No Uncommon)"] = "任意（不含一般）",
        ["Time"] = "时间类证章",
        ["Weather"] = "天气类证章",
        ["Star/Square"] = "星形／方形",
        ["Square Only"] = "仅方形闪光",
        ["Star Only"] = "仅星形闪光",
        ["Not Shiny"] = "非闪光",
        ["Square"] = "方形闪光",
        ["Star"] = "星形闪光",
        ["Shiny"] = "闪光",
        ["XXXS or XXXL"] = "XXXS 或 XXXL",

        ["Advance 𝑛"] = "前进 𝑛 次",
        ["Backwards 𝑛"] = "后退 𝑛 次",
        ["Find Initial"] = "查找初始状态",
        ["NextInt(𝑛)"] = "生成小于 𝑛 的整数",
        ["(0) Physical"] = "（0）物理",
        ["(1) Special"] = "（1）特殊",
        ["Physical"] = "物理",
        ["Special"] = "特殊",
        ["Screen On"] = "开启屏幕",
        ["Screen Off"] = "关闭屏幕",
        ["Screenshot"] = "截图",
        ["Release Stick"] = "松开摇杆",
        ["Up (Hold)"] = "上（按住）",
        ["Down (Hold)"] = "下（按住）",
        ["Left (Hold)"] = "左（按住）",
        ["Right (Hold)"] = "右（按住）",
        ["D-Pad Up"] = "十字键上",
        ["D-Pad Down"] = "十字键下",
        ["D-Pad Left"] = "十字键左",
        ["D-Pad Right"] = "十字键右",
        ["Left Stick (L3)"] = "左摇杆（L3）",
        ["Right Stick (R3)"] = "右摇杆（R3）",

        ["Ability"] = "特性",
        ["Ability Locked?"] = "特性锁定？",
        ["Advances"] = "推进数",
        ["Animation"] = "动画",
        ["Area"] = "地点",
        ["Atk"] = "攻击",
        ["Bonus"] = "奖励",
        ["Counted Watts"] = "显示瓦特",
        ["Def"] = "防御",
        ["Egg Move"] = "蛋招式",
        ["Encounter Rate (%)"] = "遇见率（%）",
        ["Encounter Type"] = "遭遇类型",
        ["Gender"] = "性别",
        ["Gender Locked?"] = "性别锁定？",
        ["Guaranteed IVs"] = "保底满项个体值",
        ["Height"] = "身高",
        ["Highlight"] = "精选商品",
        ["HP"] = "ＨＰ",
        ["Item"] = "道具",
        ["Jump"] = "跳跃",
        ["Level"] = "等级",
        ["Locked Ability"] = "锁定特性",
        ["Mark"] = "证章",
        ["Max Level"] = "最高等级",
        ["Min Level"] = "最低等级",
        ["Nature"] = "性格",
        ["Prize"] = "奖品",
        ["Regular"] = "普通商品",
        ["Respawn"] = "刷新",
        ["Seed"] = "种子",
        ["Seed0"] = "种子0",
        ["Seed1"] = "种子1",
        ["Shiny Locked?"] = "闪光锁定？",
        ["SlotMax"] = "槽位上限",
        ["SlotMin"] = "槽位下限",
        ["SpA"] = "特攻",
        ["SpD"] = "特防",
        ["Spe"] = "速度",
        ["Species"] = "宝可梦",
        ["Step"] = "步数",
        ["Total"] = "总计",
        ["Total Watts"] = "实际瓦特",

        ["Bottle Cap"] = "银色王冠",
        ["Gold Bottle Cap"] = "金色王冠",
        ["Normal Gem"] = "一般宝石",
        ["Sticky Barb"] = "附着针",
        ["Light Clay"] = "光之黏土",
        ["Lagging Tail"] = "后攻之尾",
        ["Iron Ball"] = "黑色铁球",
        ["Metal Coat"] = "金属膜",
        ["Ice Stone"] = "冰之石",
        ["Dawn Stone"] = "觉醒之石",
        ["Dusk Stone"] = "暗之石",
        ["Shiny Stone"] = "光之石",
        ["Moon Stone"] = "月之石",
        ["Sun Stone"] = "日之石",
        ["Fossilized Fish"] = "化石鱼",
        ["Fossilized Drake"] = "化石龙",
        ["Fossilized Dino"] = "化石海兽",
        ["Fossilized Bird"] = "化石鸟",
        ["Wishing Piece"] = "许愿星块",
        ["Comet Shard"] = "彗星碎片",
        ["Rare Bone"] = "贵重骨头",
        ["Sweet Ingredient"] = "糖饰",
        ["Apricorn Ball"] = "球果球",
        ["Shop Ball"] = "商店球",
        ["Beast or Dream Ball"] = "究极球或梦境球",
    };

    private static readonly Dictionary<string, string> AreaTranslations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Town of Postwick"] = "化朗镇",
        ["Your House"] = "主角的家",
        ["Hop's House (Downstairs)"] = "丹帝和赫普的家（楼下）",
        ["Hop's House (Upstairs)"] = "丹帝和赫普的家（楼上）",
        ["Town of Wedgehurst"] = "木杆镇",
        ["Town of Wedgehurst (House)"] = "木杆镇（房屋）",
        ["Wedgehurst Station"] = "木杆镇车站",
        ["Pokémon Research Lab"] = "宝可梦研究所",
        ["Wedgehurst Boutique"] = "木杆镇时装店",
        ["Wedgehurst Pokémon Center"] = "木杆镇宝可梦中心",
        ["Wild Area Station"] = "旷野地带车站",
        ["Motostoke Pokémon Center"] = "机擎市宝可梦中心",
        ["Motostoke Hair Salon"] = "机擎市美发沙龙",
        ["Motostoke Boutique"] = "机擎市服装店",
        ["Motostoke Battle Café"] = "机擎市对战咖啡馆",
        ["Motostoke Stadium"] = "机擎竞技场",
        ["Motostoke Station"] = "机擎市车站",
        ["Motostoke Pokémon Center (West)"] = "机擎市宝可梦中心（西）",
        ["Magnolia's House (Downstairs)"] = "木兰博士的家（楼下）",
        ["Magnolia's House (Upstairs)"] = "木兰博士的家（楼上）",
        ["Budew Drop Inn (Downstairs)"] = "含羞苞旅店（楼下）",
        ["Budew Drop Inn (Upstairs)"] = "含羞苞旅店（楼上）",
        ["Budew Drop Inn (Guest Room)"] = "含羞苞旅店（客房）",
        ["Budew Drop Inn (Marnie's Room)"] = "含羞苞旅店（玛俐的房间）",
        ["Town of Turffield"] = "草路镇",
        ["Turffield Pokémon Center"] = "草路镇宝可梦中心",
        ["Turffield Stadium"] = "草路竞技场",
        ["Hulbury Pokémon Center"] = "水舟镇宝可梦中心",
        ["Town of Hulbury (House)"] = "水舟镇（房屋）",
        ["Hulbury Station"] = "水舟镇车站",
        ["Hulbury Stadium"] = "水舟竞技场",
        ["Hulbury Seafood Restaurant"] = "水舟镇海鲜餐厅",
        ["City of Hammerlocke"] = "拳关市",
        ["City of Hammerlocke (House)"] = "拳关市（房屋）",
        ["Hammerlocke Pokémon Center"] = "拳关市宝可梦中心",
        ["Hammerlocke Pokémon Center (East)"] = "拳关市宝可梦中心（东）",
        ["Hammerlocke Pokémon Center (West)"] = "拳关市宝可梦中心（西）",
        ["Hammerlocke Station"] = "拳关市车站",
        ["Hammerlocke Vault (Lobby)"] = "拳关市宝物库（大厅）",
        ["Hammerlocke Vault (Tapestry Room)"] = "拳关市宝物库（挂毯室）",
        ["Hammerlocke Stadium"] = "拳关竞技场",
        ["Hammerlocke Salon"] = "拳关市美发沙龙",
        ["Hammerlocke Boutique"] = "拳关市服装店",
        ["Hammerlocke Battle Café"] = "拳关市对战咖啡馆",
        ["Energy Plant"] = "能源工厂",
        ["Tower Summit"] = "塔顶",
        ["Town of Stow-on-Side"] = "溯传镇",
        ["Town of Stow-on-Side (House)"] = "溯传镇（房屋）",
        ["Stow-on-Side Pokémon Center"] = "溯传镇宝可梦中心",
        ["Stow-on-Side Stadium (Shield)"] = "溯传竞技场（盾）",
        ["Town of Ballonlea"] = "舞姿镇",
        ["Town of Ballonlea (House)"] = "舞姿镇（房屋）",
        ["Ballonlea Pokémon Center"] = "舞姿镇宝可梦中心",
        ["Ballonlea Stadium"] = "舞姿竞技场",
        ["Route 9 Tunnel"] = "九路隧道",
        ["City of Circhester"] = "战竞镇",
        ["City of Circhester (House)"] = "战竞镇（房屋）",
        ["Circhester Pokémon Center"] = "战竞镇宝可梦中心",
        ["Circhester Stadium (Shield)"] = "战竞竞技场（盾）",
        ["Circhester Boutique"] = "战竞镇服装店",
        ["Circhester Hair Salon"] = "战竞镇美发沙龙",
        ["Hotel Ionia (Director's Room)"] = "爱奥尼亚酒店（馆长室）",
        ["Hotel Ionia (East Lobby)"] = "爱奥尼亚酒店（东大厅）",
        ["Hotel Ionia (East, Guest Room)"] = "爱奥尼亚酒店（东客房）",
        ["Hotel Ionia (East, Upstairs)"] = "爱奥尼亚酒店（东楼上）",
        ["Hotel Ionia (Morimoto's Room)"] = "爱奥尼亚酒店（森本的房间）",
        ["Hotel Ionia (West Lobby)"] = "爱奥尼亚酒店（西大厅）",
        ["Hotel Ionia (West, Guest Room)"] = "爱奥尼亚酒店（西客房）",
        ["Hotel Ionia (West, Upstairs)"] = "爱奥尼亚酒店（西楼上）",
        ["Town of Spikemuth"] = "尖钉镇",
        ["Spikemuth Pokémon Center"] = "尖钉镇宝可梦中心",
        ["White Hill Station"] = "白丘车站",
        ["City of Wyndon"] = "宫门市",
        ["City of Wyndon (House)"] = "宫门市（房屋）",
        ["Wyndon Pokémon Center"] = "宫门市宝可梦中心",
        ["Wyndon Station"] = "宫门市车站",
        ["Wyndon Battle Café"] = "宫门市对战咖啡馆",
        ["Wyndon Boutique"] = "宫门市服装店",
        ["Wyndon Hair Salon"] = "宫门市美发沙龙",
        ["Wyndon Stadium"] = "宫门竞技场",
        ["Wyndon Stadium (Locker Room)"] = "宫门竞技场（更衣室）",
        ["Wyndon Stadium (Pitch)"] = "宫门竞技场（场地）",
        ["Wyndon Pokémon Center (Stadium)"] = "宫门竞技场宝可梦中心",
        ["Battle Tower (Lobby)"] = "对战塔（大厅）",
        ["City of Wyndon (Battle Tower)"] = "对战塔",
        ["Armor Station"] = "铠岛驿站",
        ["Master Dojo"] = "马师傅武馆",
        ["Master Dojo (Stadium)"] = "马师傅武馆（竞技场）",
        ["Tower of Darkness (1F)"] = "恶之塔（1楼）",
        ["Tower of Darkness (2F)"] = "恶之塔（2楼）",
        ["Tower of Darkness (3F)"] = "恶之塔（3楼）",
        ["Tower of Darkness (4F)"] = "恶之塔（4楼）",
        ["Tower of Darkness (5F)"] = "恶之塔（5楼）",
        ["Tower of Waters (1F)"] = "水之塔（1楼）",
        ["Tower of Waters (2F)"] = "水之塔（2楼）",
        ["Tower of Waters (3F)"] = "水之塔（3楼）",
        ["Tower of Waters (4F)"] = "水之塔（4楼）",
        ["Tower of Waters (5F)"] = "水之塔（5楼）",
        ["Crown Tundra Station"] = "王冠雪原车站",
        ["Max Lair"] = "极巨巢穴",
        ["Freezington"] = "冻凝村",
        ["Freezington (Cosmog House)"] = "冻凝村（科斯莫古的家）",
        ["Freezington (Mayor's House)"] = "冻凝村（村长的家）",
        ["Freezington (Peony's House)"] = "冻凝村（皮欧尼的家）",
        ["Freezington (Sonia's House)"] = "冻凝村（索妮亚的家）",
        ["Crown Shrine"] = "王冠神殿",
        ["Crown Shrine (Inside)"] = "王冠神殿（内部）",
        ["Iron Ruins"] = "黑金遗迹",
        ["Rock Peak Ruins"] = "岩山遗迹",
        ["Iceburg Ruins"] = "冰山遗迹",
        ["Split-Decision Ruins"] = "抉择遗迹",
        ["Rose of the Rondelands (Lobby)"] = "伦度罗瑟（大厅）",
        ["Bob's Your Uncle"] = "宝可帮帮忙",

        ["Axew's Eye"] = "牙牙湖之眼",
        ["Ballimere Lake"] = "球湖湖畔",
        ["Ballimere Lake (Surfing)"] = "球湖湖畔（水上）",
        ["Brawlers' Cave"] = "战斗洞窟",
        ["Brawlers' Cave (Surfing)"] = "战斗洞窟（水上）",
        ["Bridge Field"] = "桥间空地",
        ["Bridge Field (Flying)"] = "桥间空地（空中）",
        ["Bridge Field (Surfing)"] = "桥间空地（水上）",
        ["Challenge Beach"] = "挑战海滩",
        ["Challenge Beach (Beach)"] = "挑战海滩（海滩）",
        ["Challenge Beach (Surfing - Ocean)"] = "挑战海滩（水上－海）",
        ["Challenge Beach (Surfing - River)"] = "挑战海滩（水上－河）",
        ["Challenge Beach (Surfing)"] = "挑战海滩（水上）",
        ["Challenge Road"] = "挑战之路",
        ["City of Motostoke"] = "机擎市",
        ["Courageous Cavern"] = "斗志洞窟",
        ["Courageous Cavern (Surfing)"] = "斗志洞窟（水上）",
        ["Dappled Grove"] = "沐光森林",
        ["Dusty Bowl"] = "沙尘洼地",
        ["Dusty Bowl (Flying)"] = "沙尘洼地（空中）",
        ["Dusty Bowl and Giant's Mirror (Surfing)"] = "沙尘洼地与巨人镜池（水上）",
        ["Dyna Tree Hill"] = "巨树丘陵",
        ["East Lake Axewell"] = "牙牙湖东岸",
        ["East Lake Axewell (Flying)"] = "牙牙湖东岸（空中）",
        ["East Lake Axewell (Surfing)"] = "牙牙湖东岸（水上）",
        ["Fields of Honor"] = "揖礼原野",
        ["Fields of Honor (Beach)"] = "揖礼原野（海滩）",
        ["Fields of Honor (Surfing)"] = "揖礼原野（水上）",
        ["Forest of Focus"] = "专注森林",
        ["Forest of Focus (Surfing)"] = "专注森林（水上）",
        ["Frigid Sea"] = "冻海",
        ["Frigid Sea (Surfing)"] = "冻海（水上）",
        ["Frostpoint Field"] = "冰点雪原",
        ["Galar Mine"] = "伽勒尔矿山",
        ["Galar Mine No. 2"] = "第二矿山",
        ["Giant's Bed"] = "巨人睡榻",
        ["Giant's Bed / Giant's Foot (Surfing)"] = "巨人睡榻／巨人鞋底（水上）",
        ["Giant's Cap"] = "巨人帽岩",
        ["Giant's Cap (2)"] = "巨人帽岩（区域 2）",
        ["Giant's Cap (3)"] = "巨人帽岩（区域 3）",
        ["Giant's Cap (Ground)"] = "巨人帽岩（地面）",
        ["Giant's Cap (Lunatone/Solrock)"] = "巨人帽岩（月石／太阳岩）",
        ["Giant's Foot"] = "巨人鞋底",
        ["Giant's Mirror"] = "巨人镜池",
        ["Giant's Mirror (Flying)"] = "巨人镜池（空中）",
        ["Giant's Mirror (Ground)"] = "巨人镜池（地面）",
        ["Giant's Seat"] = "巨人凳岩",
        ["Glimwood Tangle"] = "迷光森林",
        ["Hammerlocke Hills"] = "拳关丘陵",
        ["Hammerlocke Hills (Flying)"] = "拳关丘陵（空中）",
        ["Honeycalm Island"] = "蜂巢岛",
        ["Honeycalm Island (Surfing)"] = "蜂巢岛（水上）",
        ["Honeycalm Sea"] = "蜂巢海",
        ["Honeycalm Sea (Sharpedo)"] = "蜂巢海（巨牙鲨）",
        ["Honeycalm Sea (Surfing)"] = "蜂巢海（水上）",
        ["Insular Sea"] = "离岛海域",
        ["Insular Sea (Sharpedo)"] = "离岛海域（巨牙鲨）",
        ["Insular Sea (Surfing)"] = "离岛海域（水上）",
        ["Lake of Outrage"] = "逆鳞湖",
        ["Lake of Outrage (Surfing)"] = "逆鳞湖（水上）",
        ["Lakeside Cave"] = "湖畔洞窟",
        ["Loop Lagoon"] = "圆环海湾",
        ["Loop Lagoon (Beach)"] = "圆环海湾（海滩）",
        ["Loop Lagoon (Surfing)"] = "圆环海湾（水上）",
        ["Motostoke Outskirts"] = "机擎市郊外",
        ["Motostoke Riverbank"] = "机擎河岸",
        ["Motostoke Riverbank (Surfing)"] = "机擎河岸（水上）",
        ["North Lake Miloch"] = "美纳斯湖北岸",
        ["North Lake Miloch (Surfing)"] = "美纳斯湖北岸（水上）",
        ["Old Cemetery"] = "远古墓地",
        ["Path to the Peak"] = "通顶雪道",
        ["Potbottom Desert"] = "锅底沙漠",
        ["Roaring-Sea Caves"] = "海鸣洞窟",
        ["Roaring-Sea Caves (Surfing)"] = "海鸣洞窟（水上）",
        ["Rolling Fields"] = "煦丽草原",
        ["Rolling Fields (2)"] = "煦丽草原（区域 2）",
        ["Rolling Fields (Flying)"] = "煦丽草原（空中）",
        ["Rolling Fields (Ground)"] = "煦丽草原（地面）",
        ["Route 1"] = "１号道路",
        ["Route 2"] = "２号道路",
        ["Route 2 (High Level)"] = "２号道路（高等级）",
        ["Route 2 (Surfing)"] = "２号道路（水上）",
        ["Route 3"] = "３号道路",
        ["Route 3 (Garbage)"] = "３号道路（垃圾堆）",
        ["Route 4"] = "４号道路",
        ["Route 5"] = "５号道路",
        ["Route 6"] = "６号道路",
        ["Route 7"] = "７号道路",
        ["Route 8"] = "８号道路",
        ["Route 8 (on Steamdrift Way)"] = "８号道路（泉烟小路）",
        ["Route 9"] = "９号道路",
        ["Route 9 (in Circhester Bay)"] = "９号道路（战竞湾）",
        ["Route 9 (in Circhester Bay) (Surfing)"] = "９号道路（战竞湾）（水上）",
        ["Route 9 (in Outer Spikemuth)"] = "９号道路（尖钉镇郊外）",
        ["Route 10"] = "１０号道路",
        ["Route 10 (Near Station)"] = "１０号道路（车站附近）",
        ["Route 10 (Wyndon Outskirts)"] = "１０号道路（宫门市郊外）",
        ["Route 5 (Nursery)"] = "５号道路（培育屋）",
        ["Slippery Slope"] = "起橇雪原",
        ["Slumbering Weald"] = "微寐森林",
        ["Slumbering Weald (High Level)"] = "微寐森林（高等级）",
        ["Slumbering Weald (Low Level)"] = "微寐森林（低等级）",
        ["Snowslide Slope"] = "雪中溪谷",
        ["Soothing Wetlands"] = "清凉湿原",
        ["Soothing Wetlands (Puddles)"] = "清凉湿原（水洼）",
        ["South Lake Miloch"] = "美纳斯湖南岸",
        ["South Lake Miloch (2)"] = "美纳斯湖南岸（区域 2）",
        ["South Lake Miloch (Flying)"] = "美纳斯湖南岸（空中）",
        ["South Lake Miloch (Surfing)"] = "美纳斯湖南岸（水上）",
        ["Stepping-Stone Sea"] = "列岛海域",
        ["Stepping-Stone Sea (Sharpedo)"] = "列岛海域（巨牙鲨）",
        ["Stepping-Stone Sea (Surfing)"] = "列岛海域（水上）",
        ["Stony Wilderness"] = "巨石原野",
        ["Stony Wilderness (2)"] = "巨石原野（区域 2）",
        ["Stony Wilderness (3)"] = "巨石原野（区域 3）",
        ["Stony Wilderness (Flying)"] = "巨石原野（空中）",
        ["Three-Point Pass"] = "三岔平原",
        ["Town of Hulbury"] = "水舟镇",
        ["Training Lowlands"] = "锻炼平原",
        ["Training Lowlands (Beach)"] = "锻炼平原（海滩）",
        ["Training Lowlands (Surfing)"] = "锻炼平原（水上）",
        ["Tunnel to the Top"] = "登顶隧道",
        ["Turffield"] = "草路镇",
        ["Warm-up Tunnel"] = "热身洞穴",
        ["Watchtower Ruins"] = "瞭望塔旧址",
        ["Watchtower Ruins (Flying)"] = "瞭望塔旧址（空中）",
        ["West Lake Axewell"] = "牙牙湖西岸",
        ["West Lake Axewell (Surfing)"] = "牙牙湖西岸（水上）",
        ["Workout Sea"] = "健身之海",
        ["Workout Sea (Sharpedo)"] = "健身之海（巨牙鲨）",
        ["Workout Sea (Surfing)"] = "健身之海（水上）",
        ["Route 9  (Surfing)"] = "９号道路（水上）",
    };

    private static readonly Dictionary<string, string> FormSpeciesTranslations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Articuno-1"] = "急冻鸟（伽勒尔的样子）",
        ["Basculin-1"] = "野蛮鲈鱼（蓝条纹的样子）",
        ["Corsola-1"] = "太阳珊瑚（伽勒尔的样子）",
        ["Darmanitan-2"] = "达摩狒狒（伽勒尔的样子）",
        ["Darumaka-1"] = "火红不倒翁（伽勒尔的样子）",
        ["Farfetch’d-1"] = "大葱鸭（伽勒尔的样子）",
        ["Gastrodon-1"] = "海兔兽（东海）",
        ["Indeedee-1"] = "爱管侍（雌性）",
        ["Keldeo-1"] = "凯路迪欧（觉悟的样子）",
        ["Linoone-1"] = "直冲熊（伽勒尔的样子）",
        ["Lycanroc-1"] = "鬃岩狼人（黑夜的样子）",
        ["Meowstic-1"] = "超能妙喵（雌性）",
        ["Meowth-2"] = "喵喵（伽勒尔的样子）",
        ["Moltres-1"] = "火焰鸟（伽勒尔的样子）",
        ["Mr. Mime-1"] = "魔墙人偶（伽勒尔的样子）",
        ["Ponyta-1"] = "小火马（伽勒尔的样子）",
        ["Pumpkaboo-1"] = "南瓜精（小尺寸）",
        ["Pumpkaboo-2"] = "南瓜精（大尺寸）",
        ["Pumpkaboo-3"] = "南瓜精（特大尺寸）",
        ["Rapidash-1"] = "烈焰马（伽勒尔的样子）",
        ["Rotom-1"] = "加热洛托姆",
        ["Rotom-2"] = "清洗洛托姆",
        ["Rotom-3"] = "结冰洛托姆",
        ["Rotom-4"] = "旋转洛托姆",
        ["Rotom-5"] = "切割洛托姆",
        ["Shellos-1"] = "无壳海兔（东海）",
        ["Sinistea-1"] = "来悲茶（真品形态）",
        ["Slowpoke-1"] = "呆呆兽（伽勒尔的样子）",
        ["Stunfisk-1"] = "泥巴鱼（伽勒尔的样子）",
        ["Weezing-1"] = "双弹瓦斯（伽勒尔的样子）",
        ["Yamask-1"] = "哭哭面具（伽勒尔的样子）",
        ["Zapdos-1"] = "闪电鸟（伽勒尔的样子）",
        ["Zigzagoon-1"] = "蛇纹熊（伽勒尔的样子）",
    };

    private static readonly Dictionary<string, string> MessageFragments = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Not Connected."] = "未连接。",
        ["Connecting..."] = "正在连接……",
        ["Configuring sysmodule..."] = "正在配置系统模块……",
        ["Reading SAV..."] = "正在读取存档……",
        ["Connected!"] = "连接成功！",
        ["Connected! (forced)"] = "连接成功！（强制）",
        ["Disconnecting controller"] = "正在断开控制器",
        ["Disconnecting..."] = "正在断开连接……",
        ["Disconnected!"] = "已断开连接！",
        ["Detecting game version..."] = "正在检测游戏版本……",
        ["Reading Pokédex Recommendations..."] = "正在读取图鉴推荐……",
        ["Reading RNG State..."] = "正在读取 RNG 状态……",
        ["Monitoring RNG State..."] = "正在监视 RNG 状态……",
        ["Returning HOME..."] = "正在返回 HOME 菜单……",
        ["Closing game..."] = "正在关闭游戏……",
        ["Loading profile..."] = "正在加载用户……",
        ["Avoiding System Update..."] = "正在避开系统更新提示……",
        ["Opening the game..."] = "正在启动游戏……",
        ["Loading game..."] = "正在加载游戏……",
        ["Waiting on HOME Menu..."] = "正在等待 HOME 菜单……",
        ["Seed Reset Error"] = "种子重置错误",
        ["No row selected!"] = "尚未选择任何行！",
        ["No results found."] = "未找到结果。",
        ["Name is a required field!"] = "名称为必填项！",
        ["Please enter a valid numerical USB port."] = "请输入有效的数字 USB 端口。",
        ["Unable to detect Pokémon Sword or Pokémon Shield running on your Switch!"] = "无法检测到 Switch 正在运行《宝可梦 剑》或《宝可梦 盾》！",
        ["Unable to detect Pokémon Sword or Pokémon Shield running on your Switch, but forcing connection anyway as Shift was held."] = "无法检测到 Switch 正在运行《宝可梦 剑》或《宝可梦 盾》，但因按住了 Shift，将强制继续连接。",
        ["Error occurred while reading Pokédex Recommendations:"] = "读取图鉴推荐时发生错误：",
        ["Error occurred while reading initial RNG state:"] = "读取初始 RNG 状态时发生错误：",
        ["Error occurred while attempting to read KCoordinates block:"] = "读取 KCoordinates 区块时发生错误：",
        ["Error occurred during Seed Reset routine:"] = "执行种子重置流程时发生错误：",
        ["Something went wrong when writing the RNG state:"] = "写入 RNG 状态时发生错误：",
        ["Error during ResetTimeNTP:"] = "通过 NTP 重置时间时发生错误：",
        ["Failed to get start tick!"] = "无法取得起始时间戳！",
        ["Something went wrong retrieving the system time! Please NTP and try again."] = "读取系统时间时发生错误！请先执行 NTP 校时后重试。",
        ["Cannot push the date any further back! Please NTP and try again."] = "日期已无法继续往前调整！请先执行 NTP 校时后重试。",
        ["Seed reset failed to get a new seed twice in a row, cancelling routine to preserve your CPU."] = "连续两次重置都未取得新种子。为避免持续占用 CPU，流程已取消。",
        ["Could not open the selected video device."] = "无法打开所选视频设备。",
        ["Please report this error."] = "请报告此错误。",
        ["Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances."] = "找到的结果过多，最多显示 1000 条。请使用更严格的筛选条件或缩小推进范围后重新搜索。",
        ["Whoops! Max number of results found, please set the IV filters to be stricter and try again."] = "结果数量已达上限，请收紧个体值筛选条件后重试。",
        ["Searches made with this tool may take several minutes up to multiple hours depending on your device and cause high CPU load and temperatures. Proceed at your own risk."] = "此工具的搜索可能持续数分钟至数小时，并造成较高的 CPU 负载和温度。请自行评估风险后继续。",
        ["Unable to build encounter table!"] = "无法建立遭遇表！",
        ["Please report this as a bug."] = "请将此问题报告为错误。",
        ["Game:"] = "游戏：",
        ["Encounter Type:"] = "遭遇类型：",
        ["Count:"] = "数量：",
        ["Expected"] = "预期",
    };

    static ChineseLocalizer()
    {
        AddGameStrings(English.Species, Chinese.Species);
        AddGameStrings(English.Move, Chinese.Move);
        AddGameStrings(English.Item, Chinese.Item);
        AddGameStrings(English.Ability, Chinese.Ability);
        AddGameStrings(English.Types, Chinese.Types);
        AddGameStrings(English.Natures, Chinese.Natures);
        AddGameStrings(English.ribbons, Chinese.ribbons, addMarkAliases: true);
    }

    public static void Apply(Form form)
    {
        if (LocalizedForms.TryGetValue(form, out _))
            return;

        LocalizedForms.Add(form, new object());
        LocalizeControl(form);

        if (form is MainWindow && !form.Text.Contains("简体中文版", StringComparison.Ordinal))
            form.Text += " - 简体中文版";
    }

    public static string TranslateMessage(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (MessageFragments.TryGetValue(value, out var exact))
            return exact;

        var result = value;
        foreach (var pair in MessageFragments.OrderByDescending(z => z.Key.Length))
            result = result.Replace(pair.Key, pair.Value, StringComparison.OrdinalIgnoreCase);

        result = SearchCountRegex().Replace(result, "正在搜索……（$1）");
        return result;
    }

    public static string TranslateValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        var species = TranslateSpecies(value);
        if (!string.Equals(species, value, StringComparison.Ordinal))
            return species;

        var area = TranslateArea(value);
        if (!string.Equals(area, value, StringComparison.Ordinal))
            return area;

        if (UiTranslations.TryGetValue(value, out var ui))
            return ui;
        if (GameTranslations.TryGetValue(value, out var game))
            return game;

        return TranslateGameValueWithSuffix(value);
    }

    public static string TranslateSpecies(int species, int form = 0)
    {
        if ((uint)species >= (uint)English.Species.Count)
            return species.ToString();

        var english = English.Species[species];
        if (form == 0)
            return TranslateSpecies(english);

        var formKey = $"{english}-{form}";
        var translated = TranslateSpecies(formKey);
        return translated.Equals(formKey, StringComparison.Ordinal) ? $"{TranslateSpecies(english)}-{form}" : translated;
    }

    public static int FindItemIndex(ComboBox comboBox, string text)
    {
        for (var i = 0; i < comboBox.Items.Count; i++)
        {
            var invariant = comboBox.Items[i]?.ToString() ?? string.Empty;
            if (invariant.Equals(text, StringComparison.CurrentCultureIgnoreCase) ||
                TranslateComboItem(comboBox, invariant).Equals(text, StringComparison.CurrentCultureIgnoreCase))
            {
                return i;
            }
        }
        return -1;
    }

    public static string GetInvariantText(this ComboBox comboBox)
    {
        if (comboBox.InvokeRequired)
            return comboBox.Invoke(() => comboBox.GetInvariantText());

        if (comboBox.SelectedIndex >= 0)
            return comboBox.Items[comboBox.SelectedIndex]?.ToString() ?? string.Empty;

        var index = FindItemIndex(comboBox, comboBox.Text);
        return index >= 0 ? comboBox.Items[index]?.ToString() ?? string.Empty : comboBox.Text;
    }

    private static void LocalizeControl(Control control)
    {
        if (control is TextBoxBase textBox)
        {
            // Designer text in read-only fields is a placeholder; live result fields are updated elsewhere.
            if (textBox.ReadOnly)
                textBox.Text = TranslateReadOnlyText(textBox.Text);
        }
        else if (control is not ComboBox and not NumericUpDown)
        {
            control.Text = TranslateUi(control.Text);
        }

        if (control is ComboBox comboBox)
        {
            comboBox.FormattingEnabled = true;
            comboBox.Format -= ComboBox_Format;
            comboBox.Format += ComboBox_Format;
        }
        else if (control is ListBox listBox)
        {
            listBox.FormattingEnabled = true;
            listBox.Format -= ListBox_Format;
            listBox.Format += ListBox_Format;
        }

        if (control is DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
                column.HeaderText = TranslateUi(column.HeaderText);

            grid.CellFormatting -= Grid_CellFormatting;
            grid.CellFormatting += Grid_CellFormatting;
        }

        if (control is ToolStrip toolStrip)
            LocalizeToolStripItems(toolStrip.Items);

        if (control.ContextMenuStrip is not null)
            LocalizeToolStripItems(control.ContextMenuStrip.Items);

        foreach (Control child in control.Controls)
            LocalizeControl(child);
    }

    private static void LocalizeToolStripItems(ToolStripItemCollection items)
    {
        foreach (ToolStripItem item in items)
        {
            item.Text = TranslateUi(item.Text ?? string.Empty);
            if (item is ToolStripDropDownItem dropDown)
                LocalizeToolStripItems(dropDown.DropDownItems);
        }
    }

    private static void ComboBox_Format(object? sender, ListControlConvertEventArgs e)
    {
        if (sender is ComboBox comboBox && e.ListItem is not null)
            e.Value = TranslateComboItem(comboBox, e.ListItem.ToString() ?? string.Empty);
    }

    private static void ListBox_Format(object? sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is string value)
            e.Value = TranslateValue(value);
    }

    private static void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid || e.ColumnIndex < 0 || e.ColumnIndex >= grid.Columns.Count)
            return;

        var column = grid.Columns[e.ColumnIndex];
        var key = string.IsNullOrEmpty(column.DataPropertyName) ? column.Name : column.DataPropertyName;

        if (e.Value is char flag && flag is 'Y' or 'N')
        {
            e.Value = flag == 'Y' ? "是" : "否";
            e.FormattingApplied = true;
            return;
        }

        if (e.Value is char gender && gender is 'M' or 'F')
        {
            e.Value = gender == 'M' ? "雄性" : "雌性";
            e.FormattingApplied = true;
            return;
        }

        if (e.Value is not string value || string.IsNullOrEmpty(value))
            return;

        e.Value = key switch
        {
            "Species" => TranslateSpecies(value),
            "Area" => TranslateArea(value),
            "Weather" or "EncounterType" => TranslateUi(value),
            "Ability" or "LockedAbility" or "Nature" or "Item" or "Prize" or "Highlight" or "Regular" or "Mark" or "EggMove" => TranslateGameFirst(value),
            _ => TranslateValue(value),
        };
        e.FormattingApplied = true;
    }

    private static string TranslateComboItem(ComboBox comboBox, string value)
    {
        var name = comboBox.Name;
        if (name.Contains("Species", StringComparison.OrdinalIgnoreCase) ||
            name.Contains("DexRec", StringComparison.OrdinalIgnoreCase) ||
            name == "CB_Target" && Encounters.Personal?.ContainsKey(value) == true)
        {
            var translated = TranslateSpecies(value);
            return translated.Equals(value, StringComparison.Ordinal) ? TranslateUi(value) : translated;
        }

        if (name.Contains("Area", StringComparison.OrdinalIgnoreCase) ||
            name.Contains("Map", StringComparison.OrdinalIgnoreCase))
        {
            var translated = TranslateArea(value);
            return translated.Equals(value, StringComparison.Ordinal) ? TranslateUi(value) : translated;
        }

        if (name.Contains("LeadAbility", StringComparison.OrdinalIgnoreCase) ||
            name.Contains("Mark", StringComparison.OrdinalIgnoreCase) ||
            name.StartsWith("CB_Item", StringComparison.OrdinalIgnoreCase))
        {
            var translated = TranslateGameFirst(value);
            return translated.Equals(value, StringComparison.Ordinal) ? TranslateUi(value) : translated;
        }

        return TranslateValue(value);
    }

    private static string TranslateUi(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        if (UiTranslations.TryGetValue(value, out var translated))
            return translated;

        if (GameTranslations.TryGetValue(value, out translated))
            return translated;

        if (value.EndsWith(':') && GameTranslations.TryGetValue(value[..^1], out translated))
            return $"{translated}：";

        var match = CurrentVersionRegex().Match(value);
        if (match.Success)
            return $"当前：v{match.Groups[1].Value}｜最新：v{match.Groups[2].Value}";

        match = CountLabelRegex().Match(value);
        if (match.Success)
        {
            var prefix = match.Groups[1].Value switch
            {
                "Loaded IDs" => "已加载 ID",
                "Excluded Maps" => "已排除地图",
                "Observations" => "观测数",
                "Pokémon Present" => "当前宝可梦数",
                _ => match.Groups[1].Value,
            };
            return $"{prefix}：{match.Groups[2].Value}";
        }

        if (value.StartsWith("Completed Animations: ", StringComparison.OrdinalIgnoreCase))
            return value.Replace("Completed Animations: ", "已完成动画：", StringComparison.OrdinalIgnoreCase);

        return TranslateMessage(value);
    }

    private static string TranslateGameFirst(string value)
    {
        if (GameTranslations.TryGetValue(value, out var translated))
            return translated;
        return TranslateGameValueWithSuffix(value);
    }

    private static string TranslateReadOnlyText(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (value.Contains("\r\n", StringComparison.Ordinal) || value.Contains('\n'))
        {
            var lines = value.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
            for (var i = 0; i < lines.Length; i++)
                lines[i] = TranslateReadOnlyText(lines[i]);
            return string.Join(Environment.NewLine, lines);
        }

        if (value.Equals("Adamant", StringComparison.OrdinalIgnoreCase))
            return GameTranslations.TryGetValue(value, out var nature) ? nature : value;
        if (value.Equals("Compoundeyes", StringComparison.OrdinalIgnoreCase))
            return GameTranslations.TryGetValue("Compound Eyes", out var ability) ? ability : value;
        if (value.Equals("F", StringComparison.Ordinal))
            return "雌性";
        if (value.Equals("255 (XXXL)", StringComparison.OrdinalIgnoreCase))
            return "255（XXXL）";

        if (value.StartsWith("Shiny - Species (Gender) @ Item", StringComparison.OrdinalIgnoreCase))
            return "闪光 - 宝可梦（性别） @ 道具";
        if (value.Equals("WWWWWWW Nature", StringComparison.OrdinalIgnoreCase))
            return "WWWWWWW 性格";
        if (value.StartsWith("Ability:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("Ability:", "特性：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("IVs:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("IVs:", "个体值：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("Height:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("Height:", "身高：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("Mark:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("Mark:", "证章：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("EC:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("EC:", "EC：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("PID:", StringComparison.OrdinalIgnoreCase))
            return value.Replace("PID:", "PID：", StringComparison.OrdinalIgnoreCase);
        if (value.StartsWith("- Move ", StringComparison.OrdinalIgnoreCase))
            return value.Replace("- Move ", "- 招式 ", StringComparison.OrdinalIgnoreCase);

        return value;
    }

    private static string TranslateGameValueWithSuffix(string value)
    {
        var count = ItemCountRegex().Match(value);
        if (count.Success)
        {
            var item = TranslateGameFirst(count.Groups[1].Value);
            var id = count.Groups[3].Success ? $"（{count.Groups[3].Value}）" : string.Empty;
            return $"{item} ×{count.Groups[2].Value}{id}";
        }

        var qualifier = QualifierRegex().Match(value);
        if (qualifier.Success && GameTranslations.TryGetValue(qualifier.Groups[1].Value, out var baseValue))
            return $"{baseValue}（{qualifier.Groups[2].Value}）";

        return value;
    }

    private static string TranslateSpecies(string value)
    {
        if (FormSpeciesTranslations.TryGetValue(value, out var form))
            return form;

        var separator = value.LastIndexOf('-');
        if (separator > 0 && byte.TryParse(value[(separator + 1)..], out var formNumber))
        {
            var baseName = value[..separator];
            if (GameTranslations.TryGetValue(baseName, out var translatedBase))
                return $"{translatedBase}-{formNumber}";
        }

        if (Encounters.Personal?.TryGetValue(value, out var personal) == true &&
            personal.DevId >= 0 && personal.DevId < Chinese.Species.Count)
        {
            return Chinese.Species[personal.DevId];
        }

        return GameTranslations.TryGetValue(value, out var species) ? species : value;
    }

    private static string TranslateArea(string value)
    {
        if (AreaTranslations.TryGetValue(value, out var exact))
            return exact;

        var result = value;
        foreach (var pair in AreaTranslations.OrderByDescending(z => z.Key.Length))
        {
            if (result.Contains(pair.Key, StringComparison.OrdinalIgnoreCase))
                result = result.Replace(pair.Key, pair.Value, StringComparison.OrdinalIgnoreCase);
        }

        return result
            .Replace("(Surfing)", "（水上）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Surfing - Ocean)", "（水上－海）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Surfing - River)", "（水上－河）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Flying)", "（空中）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Ground)", "（地面）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Beach)", "（海滩）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Puddles)", "（水洼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Sharpedo)", "（巨牙鲨）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Garbage)", "（垃圾堆）", StringComparison.OrdinalIgnoreCase)
            .Replace("(High Level)", "（高等级）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Low Level)", "（低等级）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Near Station)", "（车站附近）", StringComparison.OrdinalIgnoreCase)
            .Replace("(East)", "（东）", StringComparison.OrdinalIgnoreCase)
            .Replace("(West)", "（西）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Shield)", "（盾）", StringComparison.OrdinalIgnoreCase)
            .Replace("(House)", "（房屋）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Lobby)", "（大厅）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Tapestry Room)", "（挂毯室）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Downstairs)", "（楼下）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Upstairs)", "（楼上）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Guest Room)", "（客房）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Marnie's Room)", "（玛俐的房间）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Director's Room)", "（馆长室）", StringComparison.OrdinalIgnoreCase)
            .Replace("(East Lobby)", "（东大厅）", StringComparison.OrdinalIgnoreCase)
            .Replace("(West Lobby)", "（西大厅）", StringComparison.OrdinalIgnoreCase)
            .Replace("(East, Guest Room)", "（东客房）", StringComparison.OrdinalIgnoreCase)
            .Replace("(West, Guest Room)", "（西客房）", StringComparison.OrdinalIgnoreCase)
            .Replace("(East, Upstairs)", "（东楼上）", StringComparison.OrdinalIgnoreCase)
            .Replace("(West, Upstairs)", "（西楼上）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Morimoto's Room)", "（森本的房间）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Nursery)", "（培育屋）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Wyndon Outskirts)", "（宫门市郊外）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Battle Tower)", "（对战塔）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Stadium)", "（竞技场）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Locker Room)", "（更衣室）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Pitch)", "（场地）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Inside)", "（内部）", StringComparison.OrdinalIgnoreCase)
            .Replace("(1F)", "（1楼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(2F)", "（2楼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(3F)", "（3楼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(4F)", "（4楼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(5F)", "（5楼）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Cosmog House)", "（科斯莫古的家）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Mayor's House)", "（村长的家）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Peony's House)", "（皮欧尼的家）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Sonia's House)", "（索妮亚的家）", StringComparison.OrdinalIgnoreCase)
            .Replace("(on Steamdrift Way)", "（泉烟小路）", StringComparison.OrdinalIgnoreCase)
            .Replace("(in Circhester Bay)", "（战竞湾）", StringComparison.OrdinalIgnoreCase)
            .Replace("(in Outer Spikemuth)", "（尖钉镇郊外）", StringComparison.OrdinalIgnoreCase)
            .Replace("(Lunatone/Solrock)", "（月石／太阳岩）", StringComparison.OrdinalIgnoreCase)
            .Replace("(2)", "（区域 2）", StringComparison.OrdinalIgnoreCase)
            .Replace("(3)", "（区域 3）", StringComparison.OrdinalIgnoreCase);
    }

    private static void AddGameStrings(IReadOnlyList<string> english, IReadOnlyList<string> chinese, bool addMarkAliases = false)
    {
        var count = Math.Min(english.Count, chinese.Count);
        for (var i = 0; i < count; i++)
        {
            var source = english[i];
            var target = chinese[i];
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target) || source == "---")
                continue;

            GameTranslations.TryAdd(source, target);
            if (addMarkAliases && source.EndsWith(" Mark", StringComparison.OrdinalIgnoreCase))
                GameTranslations.TryAdd(source[..^5], target);
        }
    }

    [GeneratedRegex(@"^Current: v(.+?) \| New:? v(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex CurrentVersionRegex();

    [GeneratedRegex(@"^(Loaded IDs|Excluded Maps|Observations|Pokémon Present):\s*(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex CountLabelRegex();

    [GeneratedRegex(@"^Searching\.\.\. \((.+)\)$", RegexOptions.IgnoreCase)]
    private static partial Regex SearchCountRegex();

    [GeneratedRegex(@"^(.+?) x(\d+)(?: \((\d+)\))?$", RegexOptions.IgnoreCase)]
    private static partial Regex ItemCountRegex();

    [GeneratedRegex(@"^(.+?) \(([12H])\)$", RegexOptions.IgnoreCase)]
    private static partial Regex QualifierRegex();
}
