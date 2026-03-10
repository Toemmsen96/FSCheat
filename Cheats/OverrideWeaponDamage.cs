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
        private static bool overrideOn = false;
        public override bool IsEnabled 
        { 
            get => overrideOn;
            set => overrideOn = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Weapon Damage Override: " + (overrideOn ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(DwellerWeaponItem), "GetName")]
        [HarmonyPostfix]
        public static void GetResultRarityPostfix(ref LunchBoxCard __instance, ref string __result, ref int ___m_DamageMin, ref int ___m_DamageMax)
        {
            if (overrideOn)
            {
                ___m_DamageMin = 1000;
                ___m_DamageMax = 1001;
                __result = "OP " + __result;
            }
        }


    }
}