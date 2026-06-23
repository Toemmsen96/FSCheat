using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetNuka : CustomCommand
    {
        public override string Name => "Set Nuka Caps";

        public override string Description => "Set Nuka Caps to specified amount";

        public override string Format => "/nuka <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Nuka to set.");
                return;
            }
            if (!float.TryParse(message.Args[0], out float amount) || amount < 0)
            {
                Utils.DisplayError("Message: Please enter a valid non-negative number.");
                return;
            }
            resources.Nuka = amount;
            Utils.DisplayMessage("Nuka Caps: Set to " + message.Args[0]);
        }
    }
}