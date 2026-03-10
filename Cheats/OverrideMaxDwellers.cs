using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class OverrideMaxDwellers : CustomCommand
    {
        public override string Name => "Override Max Dwellers";

        public override string Description => "Overrides the maximum dweller count";

        public override string Format => "/overridemaxdwellers";
        public override string Category => "Dwellers";
        public override bool IsToggle => true;
        private static bool isOverriding = true;
        public override bool IsEnabled 
        { 
            get => isOverriding;
            set => isOverriding = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Override Max Dwellers: " + (isOverriding ? "Enabled" : "Disabled"));
        }


[HarmonyPatch(typeof(DwellerManager), "get_MaximumDwellerCount")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch(ref int __result)
        {
            if (!isOverriding) return;
            __result = 2147483647;
        }

        [HarmonyPatch(typeof(DwellerManager), "get_VaultIsWithMaxPopulation")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch2(ref bool __result)
        {
            if (!isOverriding) return;
            __result = false;
        }

        [HarmonyPatch(typeof(Vault), "CanAddDwellers")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch3(ref bool __result)
        {
            if (!isOverriding) return;
            __result = true;
        }
}
}