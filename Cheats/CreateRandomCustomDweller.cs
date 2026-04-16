using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class CreateRandomCustomDweller : CustomCommand
    {
        public override string Name => "Create Random Custom Dweller";

        public override string Description => "Generates a random custom dweller in front of the vault door";

        public override string Format => "/gencustomdweller <rarity>";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            if (Patches.customDwellers == null || Patches.customDwellers.Length == 0)
            {
                Utils.DisplayWarning("No custom dwellers available to generate.");
                return;
            }
            UniqueDwellerData uniqueDwellerData = Patches.customDwellers[UnityEngine.Random.Range(0, Patches.customDwellers.Length)];
            Dweller dweller = MonoSingleton<DwellerSpawner>.Instance.CreateUniqueWaitingDweller(uniqueDwellerData);
            if (dweller != null)
            {
                Utils.DisplayMessage("Generated Custom Dweller: " + dweller.Name);
            }
            else
            {
                Utils.DisplayWarning("Failed to generate custom dweller, this may be because there are too many in front of the vault door.");
            }
        }
    }
}