using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;
using HarmonyLib;
using Game.Actors.Pets;


namespace FSCheat.Cheats
{
    internal class OverPoweredPets : CustomCommand
    {
        public override string Name => "Overpowered Pets";

        public override string Description => "Overrides all bonus effects to be 1000f";

        public override string Format => "/overpoweredpets";
        public override string Category => "Pets";
        private static bool isOverriding = false;

        public override void Execute(CommandInput message)
        {
            isOverriding = !isOverriding;
            Utils.DisplayMessage("Overpowered Pets Override: " + (isOverriding ? "Enabled" : "Disabled"));
            
        }

        [HarmonyPatch(typeof(Pet), "GetFollowerBonusForEffect")]
        [HarmonyPostfix]
        public static void OverPoweredPetsPatch(ref float __result, ref EBonusEffect effect)
        {
            if (isOverriding)
            {
                __result = 1000f;
            }
        }


    }
}