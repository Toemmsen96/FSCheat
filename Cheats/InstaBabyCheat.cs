using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class InstaBabyCheat : CustomCommand
    {
        public override string Name => "Instant Baby Cheat";

        public override string Description => "Toggle for making babies be born instantly";

        public override string Format => "/instababy";
        public override string Category => "Dwellers";
        public override bool IsToggle => true;
        private static bool instaBabyOn = false;
        public override bool IsEnabled 
        { 
            get => instaBabyOn;
            set => instaBabyOn = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Instababy Cheat: " + (instaBabyOn ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(Dweller), "get_BabyReady")]
        [HarmonyPostfix]
        private static void BabyReady(ref bool __result, ref Dweller __instance)
        {
            if (instaBabyOn)
            {
            __result = true;
            }
        }
        [HarmonyPatch(typeof(DwellerPartnership), "MakeBabyFinish")]
        [HarmonyPostfix]
        private static void MakeBabyFinishPostfix(ref DwellerPartnership __instance)
        {
            if (instaBabyOn)
            {
                __instance.BabyBirth(true);
            }
        }
    }
}