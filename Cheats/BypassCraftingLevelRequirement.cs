using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class BypassCraftingLevelRequirement : CustomCommand
    {
        public override string Name => "Bypass Crafting Level Requirement";
        public override string Description => "Allow crafting Rare and Legendary items regardless of crafting room upgrade level";
        public override string Format => "/bypasscraftinglevelreq";
        public override string Category => "Rooms";
        public override bool IsToggle => true;
        private static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Bypass Crafting Level Requirement: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        // Override roomLevelRequired to false so Rare/Legendary recipes are available at any room level
        [HarmonyPatch(typeof(RecipeCraftingWindow), "ConstructRecipeList")]
        [HarmonyPrefix]
        private static void ConstructRecipeListPrefix(ref bool roomLevelRequired)
        {
            if (isEnabled)
                roomLevelRequired = false;
        }
    }
}
