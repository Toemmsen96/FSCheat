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
        public override bool IsToggle => true;
        public override string Category => "Dwellers";
        public override bool IsEnabled => Plugin.instaAdultCheatEnabled;


        public override void Execute(CommandInput message)
        {
            Plugin.instaAdultCheatEnabled = !Plugin.instaAdultCheatEnabled;
            IsEnabled = Plugin.instaAdultCheatEnabled;
            Utils.DisplayMessage("Instaadult Cheat: " + (Plugin.instaAdultCheatEnabled ? "Enabled" : "Disabled"));

        }

        [HarmonyPatch(typeof(DwellerParameters), "get_ChildhoodDuration")]
        [HarmonyPostfix]
        public static void GetChildhoodDurationPostfix(ref float __result)
        {
            if (Plugin.instaAdultCheatEnabled)
            {
                __result = 0f;
            }
        }

    }
}