using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetStorageLimit : CustomCommand
    {
        public override string Name => "Set Storage Limit Cheat";

        public override string Description => "Set the storage limit for resources, needs to be used with /overridestoragelimit";
        public override string Format => "/setstoragelimit <amount>";
        public override string Category => "Resources";
        public override bool IsToggle => false;

        public override void Execute(CommandInput message)
        {
            if (message.Args.Count < 1)
            {
                Utils.DisplayMessage("Usage: /setstoragelimit <amount>");
                return;
            }

            if (!int.TryParse(message.Args[0], out int amount))
            {
                Utils.DisplayMessage("Invalid amount. Please enter a valid number.");
                return;
            }
            OverrideStorageLimit.maxResourcesCount = amount;
            OverrideStorageLimit.maxItemCount = amount;
            Utils.DisplayMessage($"Storage limit set to {amount}");
        }
    }
    


}
        
        
