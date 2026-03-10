using System;
using System.Collections.Generic;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class OverridePremiumPlusPass : CustomCommand
    {
        public override string Name => "Override Premium Plus Pass";

        public override string Description => "Overrides the Premium Plus Pass status to be always active";

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
        [HarmonyPrefix]
        private static void SeasonPassPremiumPlus(ref Dictionary<string, object> data)
        {
            try{
            if (data.ContainsKey("isPremium")){
                data["isPremium"] = isOverriding;
            } else {
                data.Add("isPremium", isOverriding);
            };
            if (data.ContainsKey("isPremiumPlus")){
                data["isPremiumPlus"] = isOverriding;
            } else {
                data.Add("isPremiumPlus", isOverriding);
            };
            Plugin.logger.LogInfo("Season Pass Premium Plus field set to " + isOverriding);
            } catch (Exception e){
                Plugin.logger.LogError("Error setting Season Pass Premium Plus field: " + e.Message);
            }
        }
        
        }
        
}