using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class CreateRandomDweller : CustomCommand
    {
        public override string Name => "Create Random Legendary Dweller";

        public override string Description => "Generates a random legendary dweller in front of the vault door";

        public override string Format => "/genlegenddweller";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            MonoSingleton<DwellerSpawner>.Instance.CreateWaitingDweller(EGender.Any, false, 0, EDwellerRarity.Legendary, true);
            Utils.DisplayMessage("Generated Legendary Dweller");
        }
    }
}