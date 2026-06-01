using System;
using System.Collections.Generic;
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
        private static List<BaseSeasonDataManager> seasonDataManagers = new List<BaseSeasonDataManager>();
        public override bool IsEnabled
        {
            get => isOverriding;
            set => isOverriding = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            if (isOverriding)
            {
                foreach (var manager in seasonDataManagers)
                {
                    if (manager == null) continue;
                    try
                    {
                        manager.EnablePremiumPlusPass(saveNonVaultFlags: false);
                        Plugin.logger.LogInfo("Season Pass Premium Plus granted for existing manager");
                    }
                    catch (Exception e)
                    {
                        Plugin.logger.LogError("Error granting Season Pass Premium Plus for existing manager: " + e.Message);
                    }
                }
            }
            Utils.DisplayMessage("Override Premium Plus Pass: " + (isOverriding ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(BaseSeasonDataManager), "ImportSaveData")]
        [HarmonyPostfix]
        private static void SeasonPassPremiumPlus(BaseSeasonDataManager __instance)
        {
            seasonDataManagers.Add(__instance);
            if (!isOverriding) return;
            if (SeasonPassDataManager.Instance == null)
            {
                Plugin.logger.LogError("SeasonPassDataManager instance is null. Cannot grant Premium Plus Pass.");
                return;
            };
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