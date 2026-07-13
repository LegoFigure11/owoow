# owoow

[English](README.md)

[![.NET Core Desktop](https://img.shields.io/github/actions/workflow/status/LegoFigure11/owoow/dotnet-desktop.yml?branch=master)](https://github.com/LegoFigure11/owoow/actions/workflows/dotnet-desktop.yml)
[![GitHub License](https://img.shields.io/github/license/legofigure11/owoow?color=ff69b4)](https://github.com/LegoFigure11/owoow/blob/master/LICENSE.txt)
[![使用指南](https://img.shields.io/badge/%E4%BD%BF%E7%94%A8%E6%8C%87%E5%8D%97-%E7%82%B9%E5%87%BB%E6%9F%A5%E7%9C%8B-purple)](https://billo-guides.github.io/)
<br />
[![版本](https://img.shields.io/github/v/release/LegoFigure11/owoow?label=%E6%9C%80%E6%96%B0%E7%89%88%E6%9C%AC)](https://github.com/LegoFigure11/owoow/releases/latest)
![下载次数](https://img.shields.io/github/downloads/LegoFigure11/owoow/total?label=%E6%80%BB%E4%B8%8B%E8%BD%BD%E6%95%B0)

作者：[@LegoFigure11](https://github.com/LegoFigure11/)

![ow 代表 Overworld，oow 代表 RNG Tool](https://legofigure11.github.io/tools/desktop/res/owoow/oowstandsforrngtool.png)

适用于 Nintendo Switch《宝可梦 剑／盾》的 RNG 工具及 sys-botbase 客户端。

本项目是 [LegoFigure11/swsh-overworld-rng-gui](https://github.com/LegoFigure11/swsh-overworld-rng-gui) 的后继版本。

由 [Billo-PS](https://github.com/Billo-PS) 编写的完整使用指南可在**[此处](https://billo-guides.github.io/)**查看。

![工具截图](https://legofigure11.github.io/tools/desktop/res/owoow/README.png)

## 功能与快捷操作

* 完整预测下列地图遭遇类型的 RNG：
  * 固定现身（强力宝可梦），包括铠之孤岛的吼鲸王；
  * 明雷（遭遇槽位）；
  * 暗雷；
  * 垂钓。
* 预测在各种天气下关闭菜单时，NPC 所造成的推进数。
* 无需调试器即可校准并预测下雨／雷雨天气下的推进（可用于未改机的正版主机）。
* 预测各种天气下的飞行推进（可用于伽勒尔地区形态的三只传说的鸟宝可梦；详情请参阅上方 Billo 的指南）。
* 其他 RNG 预测功能：
  * ID 抽奖（可展开搜索结果列表）；
  * 机器鹕（主要用于精灵球及星星糖饰／蝴蝶结糖饰）；
  * 雪中溪谷瓦特商人的精选商品；
  * 挖挖伯获得的瓦特数；
  * 挖洞兄弟的道具奖励（仅限技巧型）；
  * 铠之孤岛吼鲸王重新出现。
* 借助随附的 sys-botbase 自制系统模块，使用自制固件（CFW）的用户既可通过 Wi-Fi，也可通过 USB 连接 Nintendo Switch。连接时按住 Shift 可跳过游戏检查并强制连接，以便在其他游戏中使用连发操作。
* 自动读取并追踪种子（仅限 CFW）。
* 为正版／未改机主机用户提供“动画 → 种子”和“动画 → 推进数（重新识别）”计算器。
* 内置采集卡监控工具，可为正版主机用户自动记录动画，并自动查找初始种子和重新识别当前状态。
* 遭遇查询工具，以及覆盖所有区域的预置遭遇表和宝可梦个体数据（蛋招式数量等）。
* 个体值组合查找工具：根据一次遭遇必定拥有的满个体值数量，检查指定个体值组合与身高组合是否可能出现（仅建议高级用户使用）。
* 用于将 Xoroshiro128+ RNG 向前或向后推进任意次数的小工具。
* 完整支持首发宝可梦特性、图鉴推荐等会改变遭遇的条件。
* 自动重置并寻找指定图鉴推荐的工具（仅限 CFW）。按住 Shift 并单击主窗口“图鉴推荐”下方的“刷新”按钮即可开始。
* 可按宝可梦（而非遭遇槽位）、个体值、是否闪光、证章、超强气场、身高（用于《朱／紫》的大个子之证／小不点之证）以及稀有加密常数（EC 对 100 取模为 0；仅适合在《朱／紫》中捕捉可进化为三节形态土龙节节的土龙弟弟）筛选结果。
* 并行搜索。程序关闭时可修改 `config.json` 中的 `MaxSearchTasksNthPowerOfTwo` 调整并行度（仅建议高级用户使用）；搜索速度比 SwSh OWRNG Generator GUI 快许多倍。
* 直接从内存自动读取训练家 ID（TID）、秘密 ID（SID）、闪耀护符、证章护符、游戏版本及图鉴推荐（仅限 CFW）。
* 为使用多个存档进行 RNG 的正版主机用户提供档案功能，用于保存 TID、SID 和护符状态。
* 自动搜索并重置游戏，直至找到能生成指定目标的种子（仅限 CFW）；支持通过 Discord Webhook 通知找到的结果，也可在搜索期间将结果记录到文件。
* 通过更改日期、状态概览画面中的攻击动作或自定义输入操作自动推进 RNG（仅限 CFW）。
* 使用“读取遭遇”按钮读取野生宝可梦遭遇（单击），或读取 KCoordinates 地图存档块及友好度步数计数（按住 Shift 并单击）（仅限 CFW）。
* 单击主窗口中与筛选条件对应的标签，可重置该项筛选。
* 按住 Shift 并单击任一个体值按钮或标签，可将操作应用到全部六项能力（Shift + 最大值：全部设为 31；Shift + 最小值：全部设为 0；Shift + 任一能力名称：全部重置）。
* 单击个体值之间的图标，可切换该项能力的个体值搜索模式：`~` 表示范围（例如 `0 ~ 31` 接受全部个体值，`0 ~ 3` 接受 0、1、2 或 3），`||` 表示二选一（例如 `0 || 31` 仅接受 0 和 31，`29 || 31` 仅接受 29 和 31）。

## 致谢

* 感谢 [@Lusamine](https://github.com/Lusamine/) 的研究与开发工作。
* 感谢 [@Billo-PS](https://github.com/Billo-PS) 所做的研究、始终如一的耐心测试以及出色的指南。
* 感谢 [@Lincoln-LM](https://github.com/Lincoln-LM/) 协助研究。
* 感谢 [@kwsch](https://github.com/kwsch/) 及各项目贡献者开发的 [PKHeX](https://github.com/kwsch/PKHeX/)、[pkNX](https://github.com/kwsch/pkNX) 和 [SysBot.NET](https://github.com/kwsch/SysBot.NET)；本项目以不同方式使用了这些项目。
* 感谢 [@Nicolic](https://github.com/NicoIic) 提供的[猫咪动图](https://tenor.com/view/cat-gif-25169380)。
* 感谢 #citrus 的所有成员提供宝贵的反馈与测试，并耐心听取作者的各种想法。
* 感谢 #owo 的所有参与者，尤其感谢 Anubis 和 Billo 对活动的组织与协调，也感谢 Bowarcky、Cosplay Furret、mdash、ML_Lacius、Ohalright、santacrab、Tatertot74、TheBlah、TheMostPrimeape、tokeshimon 和 wyrx 参与测试。
* [LegoFigure11/swsh-overworld-rng-gui](https://github.com/LegoFigure11/swsh-overworld-rng-gui) 的所有既有贡献者与致谢对象同样适用于本项目。

## 免责声明

作者已采取一切合理的预防措施，以确保本程序能够安全使用。不过，使用本程序即表示你理解并接受：与任何 Nintendo Switch 自制程序或 CFW 工具一样，使用过程中始终存在主机被封禁或变砖的潜在风险。因使用 owoow 或随附的 sys-botbase 而对你的主机造成的任何后果，均由你本人承担责任。
