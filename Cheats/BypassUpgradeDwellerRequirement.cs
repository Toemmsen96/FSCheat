using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class BypassUpgradeDwellerRequirement : CustomCommand
    {
        public override string Name => "Bypass Upgrade Dweller Requirement";
        public override string Description => "Remove the dweller count requirement to upgrade rooms (crafting rooms, overseer, etc.)";
        public override string Format => "/bypassupgraderequirement";
        public override string Category => "Rooms";
        public override bool IsToggle => true;
        private static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Bypass Upgrade Dweller Requirement: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        // FillData runs every time the upgrade window is shown — sync the requirement flag before the dweller check
        [HarmonyPatch(typeof(RoomUpgradeWindow), "FillData")]
        [HarmonyPrefix]
        private static void FillDataPrefix(RoomUpgradeWindow __instance)
        {
            __instance.EnableUpgradeRequirement(!isEnabled);
        }
    }
}
