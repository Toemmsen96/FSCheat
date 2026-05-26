using System;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class OverridePremiumPlusPass : CustomCommand
    {
        public override string Name => "Override Premium Plus Pass";

        public override string Description => "Grants Premium Plus Pass status and records it in the season pass purchase history";

        public override string Format => "/overridepremiumpluspass";
        public override string Category => "Premium Pass";
        public override bool IsToggle => true;
        private static bool isOverriding = true;
        public override bool IsEnabled
        {
            get => isOverriding;
            set => isOverriding = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Override Premium Plus Pass: " + (isOverriding ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(BaseSeasonDataManager), "ImportSaveData")]
        [HarmonyPostfix]
        private static void SeasonPassPremiumPlus(BaseSeasonDataManager __instance)
        {
            if (!isOverriding) return;
            if (SeasonPassDataManager.Instance == null) return;
            if (Traverse.Create(SeasonPassDataManager.Instance).Field("m_seasonPassPurchaseHistory").GetValue() == null) return;
            try
            {
                __instance.EnablePremiumPlusPass(saveNonVaultFlags: false);
                Plugin.logger.LogInfo("Season Pass Premium Plus granted");
            }
            catch (Exception e)
            {
                Plugin.logger.LogError("Error granting Season Pass Premium Plus: " + e.Message);
            }
        }
    }
}