using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;
using HarmonyLib;

namespace FSCheat.Cheats
{
    internal class InstaAdultCheat : CustomCommand
    {
        public override string Name => "Instant Adult Cheat";

        public override string Description => "Toggle for making children become adults instantly";

        public override string Format => "/instaadult";
        public override string Category => "Dwellers";
        private static bool instaAdultCheat = false;

        public override void Execute(CommandInput message)
        {
            instaAdultCheat = !instaAdultCheat;
            Utils.DisplayMessage("Instaadult Cheat: " + (instaAdultCheat ? "Enabled" : "Disabled"));

        }

        [HarmonyPatch(typeof(DwellerParameters), "get_ChildhoodDuration")]
        [HarmonyPostfix]
        public static void GetChildhoodDurationPostfix(ref float __result)
        {
            if (instaAdultCheat)
            {
                __result = 0f;
            }
        }

    }
}