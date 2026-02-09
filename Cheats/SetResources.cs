using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetResources : CustomCommand
    {
        public override string Name => "Set Resources";

        public override string Description => "Set the amount of power, water, and food to a specific value";

        public override string Format => "/setres <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            if (message.Args.Count < 1)
            {
                Utils.DisplayMessage("Usage: /setres <amount>");
                return;
            }

            if (!float.TryParse(message.Args[0], out float amount))
            {
                Utils.DisplayMessage("Invalid amount. Please enter a valid number.");
                return;
            }

            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.Power = amount;
            resources.Water = amount;
            resources.Food = amount;
            Utils.DisplayMessage($"Resources set to {amount}");
        }
    }
}