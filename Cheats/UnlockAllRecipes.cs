using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class UnlockAllRecipes : CustomCommand
    {
        public override string Name => "Unlock All Recipes Cheat";

        public override string Description => "Unlock all crafting recipes";
        public override string Format => "/unlockallrecipes";
        public override string Category => "Recipes";
        public override void Execute(CommandInput message)
        {
            MonoSingleton<GameParameters>.Instance.Items.UnlockAllRecipes();
            Utils.DisplayMessage("Unlock All Recipes Cheat: All recipes unlocked.");

        }
    }}