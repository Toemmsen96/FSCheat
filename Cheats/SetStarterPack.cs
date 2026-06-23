using System;
using System.Collections.Generic;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetStarterPack : CustomCommand
    {
        public override string Name => "Set Starter Pack Boxes";

        public override string Description => "set how many Starter Pack boxes you want";

        public override string Format => "/setstarterpack <amount>";
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
                if (box.LunchBoxType == ELunchBoxType.StarterPack)
                    toRemove.Add(box);
            foreach (var box in toRemove)
                vault.RemoveLunchBox(box);
            for (int i = 0; i < amount; i++)
                vault.AddLunchBox(ELunchBoxType.StarterPack);
            Utils.DisplayMessage("Starter Pack boxes set to: " + amount);
            }
            catch (Exception e){
                Utils.DisplayError("Message: " + e.Message);
            }
        }
    }
}
