using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetStimRad : CustomCommand
    {
        public override string Name => "Set Stimpacks and Radaways Cheat";

        public override string Description => "Set Stimpacks and Radaways to specified amount";

        public override string Format => "/setstimrad <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Stimpacks and Radaways to set.");
                return;
            }
            float amount = float.Parse(message.Args[0]);
            if (amount <= 0)
            {
                Utils.DisplayError("Message: Amount cannot be negative.");
                return;
            }
            resources.StimPack = amount;
            resources.RadAway = amount;
            Utils.DisplayMessage("StimRad Cheat: Set to " + message.Args[0]);
        }
    }
}