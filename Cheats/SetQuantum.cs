using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetQuantum : CustomCommand
    {
        public override string Name => "Set Nuka Quantum";

        public override string Description => "Set Nuka Quantum to specified amount";

        public override string Format => "/quantum <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Nuka Quantum to set.");
                return;
            }
            if (!float.TryParse(message.Args[0], out float amount) || amount < 0)
            {
                Utils.DisplayError("Message: Please enter a valid non-negative number.");
                return;
            }
            resources.NukeColaQuantum = amount;
            Utils.DisplayMessage("Nuka Quantum: Set to " + message.Args[0]);
        }
    }
}