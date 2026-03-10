using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetUltracite : CustomCommand
    {
        public override string Name => "Set Ultracite";

        public override string Description => "Set Ultracite to specified amount";

        public override string Format => "/ultracite <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!(message.Args[0].Length > 0))
            {
                Utils.DisplayError("Message: Please specify an amount of Ultracite to set.");
                return;
            }
            float amount = float.Parse(message.Args[0]);
            if (amount <= 0)
            {
                Utils.DisplayError("Message: Amount cannot be negative.");
                return;
            }
            for(int i = 0; i < amount; i++)
            {
                DwellerItem item = new DwellerItem(EItemType.Junk, "Ultracite");
                MonoSingleton<Vault>.Instance.Inventory.AddItem(item);
            }
            Utils.DisplayMessage("Ultracite: Set to " + message.Args[0]);
        }
    }
}