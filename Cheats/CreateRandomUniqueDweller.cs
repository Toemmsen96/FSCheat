using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class CreateRandomUniqueDweller : CustomCommand
    {
        public override string Name => "Create Random Unique Dweller";

        public override string Description => "Generates a random unique dweller in front of the vault door";

        public override string Format => "/genuniquedweller <rarity>";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            if (message.Args.Count < 1)
            {
                Utils.DisplayMessage("No Rarity Specified using default: legendary. Usage: /genuniquedweller <rarity>");
                message.Args.Add("legendary");
            }

            string rarity = message.Args[0].ToLower();
            UniqueDwellerData uniqueDwellerData;

            switch (rarity)
            {
                case "legendary":
                    uniqueDwellerData = Patches.legendaryDwellerShuffle.Next();
                    break;
                case "rare":
                    uniqueDwellerData = Patches.rareDwellerShuffle.Next();
                    break;
                default:
                    Utils.DisplayMessage("Invalid rarity. Please specify 'legendary' or 'rare'.");
                    return;
            }

            Dweller dweller = MonoSingleton<DwellerSpawner>.Instance.CreateUniqueWaitingDweller(uniqueDwellerData);
            if (dweller != null)
            {
                Utils.DisplayMessage("Generated Unique Dweller: " + dweller.Name);
            }
            else
            {
                Utils.DisplayWarning("Failed to generate unique dweller, this may be because there are too many in front of the vault door.");
            }
        }
    }
}