using CTDynamicModMenu.Commands;
using HarmonyLib;
using ZenFulcrum.EmbeddedBrowser;

namespace FSCheat.Cheats
{
    internal class OverrideStorageLimit : CustomCommand
    {
        public override string Name => "Override Storage Limit Cheat";

        public override string Description => "Override the storage limit for resources";

        public override string Format => "/overridestoragelimit";
        public override string Category => "Resources";
        public override bool IsToggle => true;
        private static bool overrideOn = true;
        public override bool IsEnabled => overrideOn;

        public override void Execute(CommandInput message)
        {
            overrideOn = !overrideOn;
            Utils.DisplayMessage("Override Storage Limit Cheat: " + (overrideOn ? "Enabled" : "Disabled"));
        }
        [HarmonyPatch(typeof(Inventory), "SetMaxItems")]
        [HarmonyPrefix]
        public static void SetMaxItemsPrefix(Inventory __instance, ref int count, ref int ___m_itemCountMax)
        {
            if (__instance is VaultInventory && overrideOn)
            {
                count = 1073741823;
                ___m_itemCountMax = 1073741823;
                Utils.DisplayMessage("Overriding item storage limit to max value");
                //count = 1073741823;
            }
        }
        [HarmonyPatch(typeof(VaultStorage), "AddModifier")]
        [HarmonyPostfix]
        public static void SetMaxResourcesPrefix(ref GameResources ___m_maxResources)
        {
            if (overrideOn)
            {
                Utils.DisplayMessage("Overriding storage limit to max value");
                ___m_maxResources += new GameResources(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
            }
        }
    }
    


}
// [Hook("Inventory::SetMaxItems(System.Int32)")]
// 	public void Hook_SetMaxItems(CallContext context, int count)
// 	{
// 		if (_config.UnlimitedItemStorage)
// 		{
// 			Inventory inventory = (Inventory)context.This;
// 			if (inventory is VaultInventory)
// 			{
// 				context.IsHandled = true;
// 				inventory.set_M_itemCountMax(1073741823);
// 			}
// 		}
// 	}

// 	[Hook("FSLOADER::VaultStorage.SetMaxResources(Storage,EResource,System.Single)")]
// 	public void Hook_SetMaxResources(CallContext context, Storage storage, EResource resource, float oldMax)
// 	{
// 		if (_config.Overwrites.Contains(resource))
// 		{
// 			context.IsHandled = true;
// 			context.ReturnValue = 1073741823;
// 		}
// 	}   
        
        
