using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;
using HarmonyLib;


namespace FSCheat.Cheats
{
    internal class OverrideWeaponDamage : CustomCommand
    {
        public override string Name => "Override Weapon DMG";

        public override string Description => "Overrides the Damage of all weapons to 100";

        public override string Format => "/overridedamage";
        public override string Category => "Weapons";
        public override bool IsToggle => true;
        public override bool IsEnabled => Plugin.overrideWeaponDamageEnabled;

        public override void Execute(CommandInput message)
        {
            Plugin.overrideWeaponDamageEnabled = !Plugin.overrideWeaponDamageEnabled;
            IsEnabled = Plugin.overrideWeaponDamageEnabled;
            Utils.DisplayMessage("Weapon Damage Override: " + (Plugin.overrideWeaponDamageEnabled ? "Enabled" : "Disabled"));

        }

        [HarmonyPatch(typeof(DwellerWeaponItem), "GetName")]
        [HarmonyPostfix]
        public static void GetResultRarityPostfix(ref LunchBoxCard __instance, ref string __result, ref int ___m_DamageMin, ref int ___m_DamageMax)
        {
            if (Plugin.overrideWeaponDamageEnabled)
            {
                ___m_DamageMin = 1000;
                ___m_DamageMax = 1001;
                __result = "OP " + __result;
            }
        }


    }
}