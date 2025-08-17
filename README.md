# owoow

[![.NET Core Desktop](https://img.shields.io/github/actions/workflow/status/LegoFigure11/owoow/dotnet-desktop.yml?branch=master)](https://github.com/LegoFigure11/owoow/actions/workflows/dotnet-desktop.yml)
[![GitHub License](https://img.shields.io/github/license/legofigure11/owoow?color=ff69b4)](https://github.com/LegoFigure11/owoow/blob/master/LICENSE.txt)
[![Usage Guide](https://img.shields.io/badge/usage_guide-click_me!-purple)](https://billo-guides.github.io/)
<br />
[![Version](https://img.shields.io/github/v/release/LegoFigure11/owoow?label=latest%20release)](https://github.com/LegoFigure11/owoow/releases/latest)
![Download Count](https://img.shields.io/github/downloads/LegoFigure11/owoow/total?label=total%20downloads)

_by [@LegoFigure11](https://github.com/LegoFigure11/)_

![The ow stands for Overworld, the oow stands for RNG Tool](https://i.imgur.com/IGmlCkD.png)

RNG Tool and sys-botbase client for Pokémon Sword & Shield for the Nintendo Switch.

Successor to [LegoFigure11/swsh-overworld-rng-gui](https://github.com/LegoFigure11/swsh-overworld-rng-gui).

A comprehensive usage guide brought to you by [Billo-PS](https://github.com/Billo-PS) can be found **[here](https://billo-guides.github.io/)**.

![Tool Image](https://i.imgur.com/MaAmBky.png)

## Features & shortcuts

* Full RNG prediction for overworld encounter types:
  * Static (Strong Spawns) - including the IoA Wailord!
  * Symbol (Encounter Slots)
  * Hidden
  * Fishing
* Predicting the advances caused by the NPCs when closing the menu in all weathers.
* Rain/Thunderstorm calibration and prediction without the need for a debugger (retail/stock firmware viable!).
* Fly prediction (useful for Galarian Birds, see Billo's guide linked above for more information) in all weathers.
* Miscellaneous other RNG prediction:
  * Loto-ID (with expandable search list!)
  * Cram-o-matic (specifically for Pok&eacute;balls and Star/Ribbon Sweets)
  * Snowslide Slope Watt Trader's Highlight item
  * Digging Pa Watts
  * Isle of Armor Wailord Respawn
* Switch connectivity over both Wi-Fi and USB thanks to the bundled sys-botbase homebrew sysmodule for users with Custom Firmware (CFW).
* Automated seed reading and tracking (CFW only).
* Animations → Seed and Animations → Advances (re-identification) calculators for retail/stock firmware users.
* Encounter Lookup tool + prefilled encounter tables and personal details (Egg Move counts, etc) for every area.
* Spread Finder tool to check the availability of specific IV spread + Height combinations, given the number of guaranteed flawless IVs for an encounter (recommended for advanced users only).
* Mini tool for skipping a Xoroshiro128+ RNG an arbitrary amount forwards or backwards.
* Full support for encounter-modifying conditions such as Lead Abilities and Pok&eacute;dex Recommendations.
* Filtering results based on Species (rather than Encounter Slot), IVs, Shininess, Mark, Brilliant Aura, Height (for Jumbo/Teensy mark in Scarlet/Violet), and Rare EC (EC modulo 100 = 0, only useful for catching Dunsparce that evolve in to 3-Segment Dudunsparce in Scarlet/Violet).
* Parallelised search (can be adjusted by changing the value of ``MaxSearchTasksNthPowerOfTwo`` in ``config.json`` while the program is closed, recommended for advanced users only), resulting in search times many many times faster than SwSh OWRNG Generator GUI.
* Automatically read TID, SID, Shiny Charm, Mark Charm, Game Version, and Pok&eacute;dex Recommendations directly from RAM (CFW only).
* Profiles for saving TID/SID/Charms for retail users who RNG across multiple save files.
* Automatic searching and resetting for a seed that generates a specific target (CFW only), with Discord webhook integration for notifying when a result is found and the ability to log search results to a file while a search is underway.
* Automate advances through date skipping, summary screen attack animations, or your own custom input routines (CFW only).
* Read Wild Encounters (Click) or the KCoordinates Overworld Save Block (Shift + Click) with the "Read Encounter" button (CFW only).
* Reset any filter on the main window by clicking the associated label.
* Shift + Click on an IV button or label to apply the effect to every stat (Shift + Max = 31 all, Shift + Min = 0 all, Shift + any stat name = reset all).
* Click on the icon between IVs to switch between IV search modes for that stat: ``~`` for a range (e.g. ``0 ~ 31`` accepting all IVs, ``0 ~ 3`` accepting 0, 1, 2, or 3), or ``||`` for either or (e.g. ``0 || 31`` accepting 0 and 31 only, ``29 || 31`` accepting 29 and 31 only).

## Credits and thanks to

* [@Lusamine](https://github.com/Lusamine/) for research and development.
* [@Billo-PS](https://github.com/Billo-PS) for research and your endless patience and testing, as well as your incredible guides.
* [@Lincoln-LM](https://github.com/Lincoln-LM/) for research assistance.
* [@kwsch](https://github.com/kwsch/) and contributors for [PKHeX](https://github.com/kwsch/PKHeX/), [pkNX](https://github.com/kwsch/pkNX), and [SysBot.NET](https://github.com/kwsch/SysBot.NET) which are all leveraged by this project in some manner.
* [@Nicolic](https://github.com/NicoIic) for [this cat gif](https://tenor.com/view/cat-gif-25169380).
* The crew from #citrus for your invaluable feedback and testing, and putting up with my rambling.
* Everyone who participated in #owo, especially Anubis and Billo for facilitating, and Bowarcky, Cosplay Furret, mdash, ML_Lacius, Ohalright, santacrab, Tatertot74, TheBlah, TheMostPrimeape, tokeshimon, and wyrx for testing.
* All existing contributors to and credits from [LegoFigure11/swsh-overworld-rng-gui](https://github.com/LegoFigure11/swsh-overworld-rng-gui) also apply.

## Disclaimer

Every precaution has been taken to ensure that this program is safe to use, but by using this program you accept that as with any switch homebrew or CFW tools there is a potential risk for your switch to be banned or bricked, and that you alone are responsible for anything that may happen to your own console as a result of using owoow or the included sys-botbase.
