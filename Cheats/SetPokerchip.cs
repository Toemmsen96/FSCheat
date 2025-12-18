using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetPokerchip : CustomCommand
    {
        public override string Name => "Pokerchip Cheat";

        public override string Description => "Set Pokerchip to specified amount";
        public override string Format => "/pokerchip <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Pokerchips to set.");
                return;
            }
            float amount = float.Parse(message.Args[0]);
            if (amount <= 0)
            {
                Utils.DisplayError("Message: Amount cannot be negative.");
                return;
            }
            resources.PokerChip = amount;
            Utils.DisplayMessage("Pokerchip Cheat: Set to " + message.Args[0]);
        }
    }
}