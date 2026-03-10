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
        public static bool overrideOn { get; private set; } = false;
        public override bool IsEnabled 
        { 
            get => overrideOn;
            set => overrideOn = value;
        }
        public static int maxResourcesCount = 999999999;
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;
        internal static VaultStorage currentStorage;
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
        [HarmonyPatch(typeof(VaultStorage), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void VaultStorageConstructorPostfix(VaultStorage __instance)
        {
            currentStorage = __instance;
            Utils.DisplayMessage("VaultStorage instance stored for reference");
        }
    
    }

}
        
        
