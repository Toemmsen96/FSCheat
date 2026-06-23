using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetPokerchip : CustomCommand
    {
        public override string Name => "Set Spin Resources (Pokerchip etc.)";

        public override string Description => "Set Spin resources (depends on seasons) to specified amount";
        public override string Format => "/spinres <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Spin resources to set.");
                return;
            }
            if (!float.TryParse(message.Args[0], out float amount) || amount < 0)
            {
                Utils.DisplayError("Message: Please enter a valid non-negative number.");
                return;
            }
            resources.PokerChip = amount;
            Utils.DisplayMessage("Spin Resources Cheat: Set to " + message.Args[0]);
        }
    }
}