using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class InstaBabyCheat : CustomCheat
    {
        public override string Name => "Instant Baby Cheat";

        public override string Description => "Toggle for making babies be born instantly";

        public override string Format => "/instababy";

        public override void Execute(CheatInput message)
        {
            Patches.instaBabyCheat = !Patches.instaBabyCheat;
            Utils.DisplayMessage("Instababy Cheat: " + (Patches.instaBabyCheat ? "Enabled" : "Disabled"));
        }
    }
}