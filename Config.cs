using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using System.Collections.Generic;
using CTDynamicModMenu.Commands;
using BepInEx.Logging;
using System.CodeDom;

namespace FSCheat
{
    public partial class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<KeyCode> toggleKey;
        public static bool overrideWeaponDamageEnabled = false;
        public static bool overPoweredPetsEnabled = false;
        public static bool instaAdultCheatEnabled = false;
        public static bool instaBabyCheatEnabled = false;
        public static bool patchOverrideMaxDwellers = true;
        public static bool patchOverrideLivingQuarters = true;
        public static bool overridePremiumPlusPass = true;

        public void InitConfig()
        {
            toggleKey = this.Config.Bind<KeyCode>("Command Settings", "Toggle Key", KeyCode.F4, "Key to toggle the menu");
            patchOverrideMaxDwellers = this.Config.Bind<bool>("Dweller Settings", "Override Max Dwellers", true, "If true, the max number of dwellers in the vault is overridden to int.MAX.").Value;
            patchOverrideLivingQuarters = this.Config.Bind<bool>("Dweller Settings", "Override Living Quarters", true, "If true, the living quarters capacity is overridden to always allow adding Dwellers.").Value;
            overrideWeaponDamageEnabled = this.Config.Bind<bool>("Cheat Settings", "Override Weapon Damage Cheat", false, "If true, the Override Weapon Damage cheat is enabled on startup.").Value;
            overPoweredPetsEnabled = this.Config.Bind<bool>("Cheat Settings", "Overpowered Pets Cheat", false, "If true, the Overpowered Pets cheat is enabled on startup.").Value;
            instaAdultCheatEnabled = this.Config.Bind<bool>("Cheat Settings", "Insta Adult Cheat", false, "If true, the Instant Adult cheat is enabled on startup.").Value;
            instaBabyCheatEnabled = this.Config.Bind<bool>("Cheat Settings", "Insta Baby Cheat", false, "If true, the Instant Baby cheat is enabled on startup.").Value;
            overridePremiumPlusPass = this.Config.Bind<bool>("Cheat Settings", "Override Premium Plus Pass", true, "If true, all vaults are treated as having Premium Plus Pass.").Value;
        }
    }
}