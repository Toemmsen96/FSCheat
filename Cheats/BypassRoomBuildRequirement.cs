using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class BypassRoomBuildRequirement : CustomCommand
    {
        public override string Name => "Bypass Room Build Requirement";
        public override string Description => "Bypass the dweller count requirement to unlock building a room";
        public override string Format => "/bypassroombuildrequirement";
        public override string Category => "Rooms";
        public override bool IsToggle => true;
        private static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Bypass Room Build Requirement: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        // Rooms locked by unmet objectives (e.g. not enough dwellers) report Locked — override to Unlocked
        [HarmonyPatch(typeof(UIRoomBuildList), "IsRoomAvailable")]
        [HarmonyPostfix]
        private static void IsRoomAvailablePostfix(ref ERoomBuildLockState __result)
        {
            if (!isEnabled) return;
            if (__result == ERoomBuildLockState.Locked)
                __result = ERoomBuildLockState.Unlocked;
        }
    }
}
