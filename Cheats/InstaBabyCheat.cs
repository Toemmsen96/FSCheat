using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class InstaBabyCheat : CustomCommand
    {
        public override string Name => "Instant Baby Cheat";

        public override string Description => "Toggle for making babies be born instantly";

        public override string Format => "/instababy";
        public override string Category => "Dwellers";
        public override bool IsToggle => true;
        public override bool IsEnabled => Plugin.instaBabyCheatEnabled;

        public override void Execute(CommandInput message)
        {
            Plugin.instaBabyCheatEnabled = !Plugin.instaBabyCheatEnabled;
            IsEnabled = Plugin.instaBabyCheatEnabled;
            Utils.DisplayMessage("Instababy Cheat: " + (Plugin.instaBabyCheatEnabled ? "Enabled" : "Disabled"));
        }
    }
}