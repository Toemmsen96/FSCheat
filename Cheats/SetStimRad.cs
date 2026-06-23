using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetStimRad : CustomCommand
    {
        public override string Name => "Set Stimpacks and Radaways";

        public override string Description => "Set Stimpacks and Radaways to specified amount";

        public override string Format => "/setstimrad <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Stimpacks and Radaways to set.");
                return;
            }
            if (!float.TryParse(message.Args[0], out float amount) || amount < 0)
            {
                Utils.DisplayError("Message: Please enter a valid non-negative number.");
                return;
            }
            resources.StimPack = amount;
            resources.RadAway = amount;
            Utils.DisplayMessage("Stimpacks and Radaways: Set to " + message.Args[0]);
        }
    }
}