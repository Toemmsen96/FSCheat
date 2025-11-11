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

        public override void Execute(CommandInput message)
        {
            Patches.instaBabyCheat = !Patches.instaBabyCheat;
            Utils.DisplayMessage("Instababy Cheat: " + (Patches.instaBabyCheat ? "Enabled" : "Disabled"));
        }
    }
}