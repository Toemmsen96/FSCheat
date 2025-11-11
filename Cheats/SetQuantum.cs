using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetQuantum : CustomCommand
    {
        public override string Name => "Nuka Quantum Cheat";

        public override string Description => "Set Nuka Quantum to specified amount";

        public override string Format => "/quantum <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.NukeColaQuantum = float.Parse(message.Args[0]);
            Utils.DisplayMessage("Nuka Quantum Cheat: Set to " + message.Args[0]);
        }
    }
}