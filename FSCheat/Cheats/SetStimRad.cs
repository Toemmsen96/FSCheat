using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class SetStimRad : CustomCheat
    {
        public override string Name => "Set Stimpacks and Radaways Cheat";

        public override string Description => "Set Stimpacks and Radaways to specified amount";

        public override string Format => "/setstimrad <amount>";

        public override void Execute(CheatInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.StimPack = float.Parse(message.Args[0]);
            resources.RadAway = float.Parse(message.Args[0]);
            Utils.DisplayMessage("StimRad Cheat: Set to " + message.Args[0]);
        }
    }
}