using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class CreateRandomLegendaryDweller : CustomCommand
    {
        public override string Name => "Create Random Legendary Dweller";

        public override string Description => "Generates a random legendary dweller in front of the vault door";

        public override string Format => "/genlegenddweller";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            Dweller dweller = MonoSingleton<DwellerSpawner>.Instance.CreateWaitingDweller(EGender.Any, false, 0, EDwellerRarity.Legendary, true);
            Utils.DisplayMessage("Generated Legendary Dweller: " + dweller.Name);
        }
    }
}