using System;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class FreeCraftingCheat : CustomCommand
    {
        public override string Name => "Free Crafting";
        public override string Description => "Bypass all crafting requirements (no ingredients or Nuka cost needed)";
        public override string Format => "/freecrafting";
        public override string Category => "Recipes";
        public override bool IsToggle => true;
        internal static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Free Crafting: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        private static void SyncDebugFreeCrafting()
        {
            if (MonoSingleton<VaultGUIManager>.Instance?.m_recipeCraftingWindow != null)
                MonoSingleton<VaultGUIManager>.Instance.m_recipeCraftingWindow.DebugFreeCrafting = isEnabled;
        }

        // Force the UI to show all recipes as craftable regardless of resources
        [HarmonyPatch(typeof(RecipeEntry.RecipeData), MethodType.Constructor,
            new Type[] { typeof(RecipeList.Recipe), typeof(EItemType), typeof(CraftingRoom) })]
        [HarmonyPostfix]
        private static void RecipeDataCtorPostfix(RecipeEntry.RecipeData __instance)
        {
            if (!isEnabled) return;
            __instance.HasEnoughNuka = true;
            __instance.HasAllIngredients = true;
            __instance.CanBeCraftedNow = __instance.IsRecipeUnlocked;
        }

        // Keep DebugFreeCrafting in sync so StartCrafting skips ingredient collection natively
        [HarmonyPatch(typeof(CraftingRoom), "StartCrafting")]
        [HarmonyPrefix]
        private static void StartCraftingPrefix()
        {
            SyncDebugFreeCrafting();
        }

        // TryRecoverStatus resets crafting when m_ingredientItemIds is empty and DebugFreeCrafting is false —
        // sync the flag before that check runs
        [HarmonyPatch(typeof(CraftingRoom), "TryRecoverStatus")]
        [HarmonyPrefix]
        private static void TryRecoverStatusPrefix()
        {
            SyncDebugFreeCrafting();
        }

        // HasCraftingItem also gates on DebugFreeCrafting when ingredient list is empty
        [HarmonyPatch(typeof(CraftingRoom), "HasCraftingItem")]
        [HarmonyPrefix]
        private static void HasCraftingItemPrefix()
        {
            SyncDebugFreeCrafting();
        }

        // Zero the Nuka cost before it is deducted (DebugFreeCrafting handles the ingredient list)
        [HarmonyPatch(typeof(CraftingRoom), "RemoveIngredientsAndCost")]
        [HarmonyPrefix]
        private static void RemoveIngredientsAndCostPrefix(CraftingRoom __instance)
        {
            if (!isEnabled) return;
            Traverse.Create(__instance).Field("m_craftingCost").SetValue(new GameResources());
        }
    }
}
