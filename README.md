# Fallout Shelter BepInEx Cheat

A comprehensive cheat mod for Fallout Shelter that adds an in-game menu with various quality-of-life improvements and cheats. This mod uses BepInEx as its modding framework and provides a user-friendly GUI interface for activating cheats.

## 🎮 What is This?

FSCheat is a BepInEx plugin for Fallout Shelter that gives you complete control over your vault through an easy-to-use in-game menu. Whether you want to speed up gameplay, experiment with different strategies, or just have fun, this mod provides the tools you need.

## ✨ Features

### Resource Management
- **Max Resources** - Instantly max out all vault resources (food, water, power)
- **Set Nuka-Cola Quantum** - Set your Quantum cola count
- **Set Nuka-Cola** - Set your regular Nuka-Cola count
- **Set Stimpacks & RadAway** - Adjust your medical supplies
- **Set Lunchboxes** - Get as many lunchboxes as you want
- **Set Pet Carriers** - Control your pet carrier count
- **Set Mr. Handy** - Add Mr. Handy robots to your vault

### Dweller Management
- **Instant Baby** - Speed up pregnancy and childbirth
- **Instant Adult** - Instantly age up children to adults
- **Max Level Dwellers** - Level all dwellers to maximum level
- **Reset Dweller Levels** - Reset dweller levels back to 1
- **Set Max SPECIAL** - Max out all SPECIAL stats for dwellers
- **Finish All Training** - Complete all training room activities instantly
- **Create Random Dweller** - Generate new dwellers

### Items & Equipment
- **Overpowered Pets** - Make pets incredibly powerful
- **Override Item Rarity** - Change the rarity of items
- **Override Weapon Damage** - Modify weapon damage values

### Utilities
- **Decrypt Game Saves** - Decrypt your save files for backup/editing
- **Encrypt Game Saves** - Re-encrypt save files after modifications

## 📋 Requirements

- **Fallout Shelter** (Steam version)
- **BepInEx 5.x** - Modding framework for Unity games
- **CTDynamicModMenu** - Dependency for the in-game menu system (is included)
- **.NET Framework 4.8** - Required runtime (usually pre-installed on Windows)

## 🔧 Installation

1. **Install BepInEx**
   - Download BepInEx 5.x from [BepInEx releases](https://github.com/BepInEx/BepInEx/releases)
   - Extract BepInEx to your Fallout Shelter game directory
   - Run the game once to generate BepInEx folders

2. **Install CTDynamicModMenu**
   - Download CTDynamicModMenu from its repository or take it here from Dependencies
   - Place the DLL in `BepInEx/plugins/`

3. **Install FSCheat**
   - Download the latest release of FSCheat
   - Place `FSCheat.dll` in `BepInEx/plugins/`
   - Launch the game

## 🎯 How to Use

1. **Launch the game** - The mod loads automatically when the game starts
2. **Open the cheat menu** - Press the configured hotkey (default can be found/changed in BepInEx config files)
3. **Select cheats** - Use the GUI menu to activate the cheats you want
4. **Customize hotkey** - Go to `BepInEx/config/` and edit the configuration file to change the menu hotkey

## 🛠️ Building from Source

### Prerequisites
- Visual Studio 2019 or later / .NET SDK
- .NET Framework 4.8 Developer Pack
- Game dependencies (Unity, BepInEx, etc.) placed in `Dependencies/` folder

### Build Steps
```bash
# Clone the repository
git clone https://github.com/Toemmsen96/FSCheat.git
cd FSCheat

# Build the project
dotnet build -c Release

# Output will be in bin/Release/net48/
```

### Dependencies Required
Place the following DLLs in the `Dependencies/` folder:
- `0Harmony.dll`
- `Assembly-CSharp.dll` (from Fallout Shelter)
- `BepInEx.dll`
- `BepInEx.Harmony.dll`
- `BepInEx.Preloader.dll`
- `CTDynamicModMenu.dll`
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`


**Use at your own risk. Always backup your save files before using any mods.**

## 📝 License

This project is provided as-is for educational and personal use.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check the issues page or submit pull requests.

