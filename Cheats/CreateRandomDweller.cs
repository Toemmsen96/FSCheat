using System;
using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class CreateRandomDweller : CustomCommand
    {
        public override string Name => "Create Random Dweller";

        public override string Description => "Generates a random dweller in front of the vault door";

        public override string Format => "/gendweller";
        public override string Category => "Dwellers";
        private static Random random = new Random();

        public override void Execute(CommandInput message)
        {
            EDwellerRarity rarity = (EDwellerRarity)random.Next((int)EDwellerRarity.Common, (int)EDwellerRarity.Legendary + 1);
            Dweller dweller = MonoSingleton<DwellerSpawner>.Instance.CreateWaitingDweller(EGender.Any, false, 0, rarity, true);
            Utils.DisplayMessage("Generated " + rarity + " Dweller: " + dweller.Name);
        }
    }
}