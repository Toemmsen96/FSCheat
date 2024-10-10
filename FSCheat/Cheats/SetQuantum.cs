using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class SetQuantum : CustomCheat
    {
        public override string Name => "Nuka Quantum Cheat";

        public override string Description => "Set Nuka Quantum to specified amount";

        public override string Format => "/quantum <amount>";

        public override void Execute(CheatInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.NukeColaQuantum = float.Parse(message.Args[0]);
            Utils.DisplayMessage("Nuka Quantum Cheat: Set to " + message.Args[0]);
        }
    }
}