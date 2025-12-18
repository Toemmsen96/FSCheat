using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetNuka : CustomCommand
    {
        public override string Name => "Nuka Cheat";

        public override string Description => "Set Nuka to specified amount";

        public override string Format => "/nuka <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Nuka to set.");
                return;
            }
            float amount = float.Parse(message.Args[0]);
            if (amount <= 0)
            {
                Utils.DisplayError("Message: Amount cannot be negative.");
                return;
            }
            resources.Nuka = amount;
            Utils.DisplayMessage("Nuka Cheat: Set to " + message.Args[0]);
        }
    }
}