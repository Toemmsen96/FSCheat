using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class OverrideStorageLimit : CustomCommand
    {
        public override string Name => "Override Storage Limit Cheat";

        public override string Description => "Override the storage limit for resources";

        public override string Format => "/overridestoragelimit";
        public override string Category => "Resources";
        public override bool IsToggle => true;
        private static bool overrideOn = false;
        public override bool IsEnabled 
        { 
            get => overrideOn;
            set => overrideOn = value;
        }
        public static int maxResourcesCount = 999999999;
        public static int maxItemCount = 999999999;
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;
        private static VaultStorage currentStorage;
        private static GameResources originalResources;
        public override void Execute(CommandInput message)
        {
            if(overrideOn && currentStorage != null)
            {
                originalResources = currentStorage.MaxResources.Clone();
                currentStorage.SetMaxResources(new GameResources(maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount));
            }
            else if(currentStorage != null)
            {
                currentStorage.SetMaxResources(originalResources);
            }
            Utils.DisplayMessage("Override Storage Limit Cheat: " + (overrideOn ? "Enabled" : "Disabled"));
        }
        [HarmonyPatch(typeof(Inventory), "SetMaxItems")]
        [HarmonyPrefix]
        public static void SetMaxItemsPrefix(Inventory __instance, ref int count, ref int ___m_itemCountMax)
        {
            if (__instance is VaultInventory && overrideOn)
            {
                count = maxItemCount;
                ___m_itemCountMax = maxItemCount;
                Utils.DisplayMessage("Overriding item storage limit to max value");
                //count = 1073741823;
            }
        }
        // [HarmonyPatch(typeof(VaultStorage), "AddModifier")]
        // [HarmonyPostfix]
        // public static void SetMaxResourcesPrefix(ref GameResources ___m_maxResources)
        // {
        //     if (overrideOn)
        //     {
        //         Utils.DisplayMessage("Overriding storage limit to max value");
        //         ___m_maxResources += new GameResources(maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount, maxResourcesCount);
        //     }
        // }
        [HarmonyPatch(typeof(VaultStorage), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void VaultStorageConstructorPostfix(VaultStorage __instance)
        {
            currentStorage = __instance;
            Utils.DisplayMessage("VaultStorage instance stored for reference");
        }
    
    }

}
        
        
