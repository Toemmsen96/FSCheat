using System;
using System.Collections.Generic;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetQuantumBox : CustomCommand
    {
        public override string Name => "Set Nuka Quantum Boxes";

        public override string Description => "set how many Nuka Cola Quantum boxes you want";

        public override string Format => "/setquantumbox <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            try{
            if (!int.TryParse(message.Args[0], out int amount) || amount < 0)
            {
                Utils.DisplayError("Message: Please enter a valid non-negative number.");
                return;
            }
            var vault = MonoSingleton<Vault>.Instance;
            var toRemove = new List<LunchBox>();
            foreach (var box in vault.LocalLunchBoxes)
                if (box.LunchBoxType == ELunchBoxType.NukaColaQuantum)
                    toRemove.Add(box);
            foreach (var box in toRemove)
                vault.RemoveLunchBox(box);
            for (int i = 0; i < amount; i++)
                vault.AddLunchBox(ELunchBoxType.NukaColaQuantum);
            Utils.DisplayMessage("Nuka Cola Quantum boxes set to: " + amount);
            }
            catch (Exception e){
                Utils.DisplayError("Message: " + e.Message);
            }
        }
    }
}
