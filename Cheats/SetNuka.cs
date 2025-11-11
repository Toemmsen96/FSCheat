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
            resources.Nuka = float.Parse(message.Args[0]);
            Utils.DisplayMessage("Nuka Cheat: Set to " + message.Args[0]);
        }
    }
}