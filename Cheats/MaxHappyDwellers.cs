using System;
using System.Collections.Generic;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class MaxHappyDwellers : CustomCommand
    {
        public override string Name => "Max Happy Dwellers";

        public override string Description => "Max out all Dwellers happy levels";

        public override string Format => "/maxhappy";
        public override string Category => "Dwellers";
        private static List<DwellerHappiness> dwellerHappinessList = new List<DwellerHappiness>();

        public override void Execute(CommandInput message)
        {
            try{
            foreach(DwellerHappiness happiness in dwellerHappinessList){
                happiness.AddHappiness(happiness.HappinessMax);
                Utils.DisplayMessage($"Successfully set max Happiness for: {happiness.Dweller.Name}");
            }
            }
            catch (Exception e)
            {
                Utils.DisplayError("Message: " + e.Message);
            }
        }

        [HarmonyPatch(typeof(DwellerHappiness), MethodType.Constructor, new[] { typeof(Dweller) })]
        [HarmonyPostfix]
        public static void DwellerHappinessConstructorPostfix(DwellerHappiness __instance)
        {
            dwellerHappinessList.Add(__instance);
        }
    }
}