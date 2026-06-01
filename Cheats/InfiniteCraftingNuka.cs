using System;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class InfiniteCraftingNuka : CustomCommand
    {
        public override string Name => "Infinite Crafting Nuka";
        public override string Description => "Nuka cost is never deducted when crafting (ingredients still required)";
        public override string Format => "/infinitecraftingnuka";
        public override string Category => "Recipes";
        public override bool IsToggle => true;
        internal static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Infinite Crafting Nuka: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        // Always report enough Nuka so the craft button stays enabled
        [HarmonyPatch(typeof(RecipeEntry.RecipeData), MethodType.Constructor,
            new Type[] { typeof(RecipeList.Recipe), typeof(EItemType), typeof(CraftingRoom) })]
        [HarmonyPostfix]
        private static void RecipeDataCtorPostfix(RecipeEntry.RecipeData __instance)
        {
            if (!isEnabled) return;
            __instance.HasEnoughNuka = true;
            if (__instance.HasAllIngredients)
                __instance.CanBeCraftedNow = __instance.IsRecipeUnlocked;
        }

        // Zero the Nuka cost before it is deducted (ingredients are still consumed normally)
        [HarmonyPatch(typeof(CraftingRoom), "RemoveIngredientsAndCost")]
        [HarmonyPrefix]
        private static void RemoveIngredientsAndCostPrefix(CraftingRoom __instance)
        {
            if (!isEnabled) return;
            Traverse.Create(__instance).Field("m_craftingCost").SetValue(new GameResources());
        }
    }
}
