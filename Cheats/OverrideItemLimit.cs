using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class OverrideItemLimit : CustomCommand
    {
        public override string Name => "Override Item Limit Cheat";

        public override string Description => "Override the item limit for inventory";

        public override string Format => "/overrideitemlimit";
        public override string Category => "Items";
        public override bool IsToggle => true;
        public static bool overrideOn { get; private set; } = false;
        public override bool IsEnabled 
        { 
            get => overrideOn;
            set => overrideOn = value;
        }
        public static int maxItemCount = 999999999;
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;
        internal static VaultInventory currentInventory;
        private static int originalMaxItems;
        public override void Execute(CommandInput message)
        {
            if(overrideOn && currentInventory != null)
            {
                originalMaxItems = currentInventory.ItemCountMax;
                currentInventory.SetMaxItems(maxItemCount);
            }
            else if(currentInventory != null)
            {
                currentInventory.SetMaxItems(originalMaxItems);
            }
            Utils.DisplayMessage("Override Item Limit Cheat: " + (overrideOn ? "Enabled" : "Disabled"));
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
            }
        }
    }
}















        