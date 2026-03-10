using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetStorageLimit : CustomCommand
    {
        public override string Name => "Set Storage Limit";

        public override string Description => "Set the storage limit for resources, needs to be used with /overridestoragelimit";
        public override string Format => "/setstoragelimit <amount>";
        public override string Category => "Resources";
        public override bool IsToggle => false;
        public override bool HasConfig => true;
        public override bool PersistConfig => true;

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
            SaveCustomConfig();
            if (OverrideStorageLimit.overrideOn)
            {
                OverrideStorageLimit.currentStorage?.SetMaxResources(new GameResources(amount, amount, amount, amount, amount, amount, amount, amount, amount, amount, amount, amount));
            }
            Utils.DisplayMessage($"Storage limit set to {amount}");
        }

        public override void LoadCustomConfig()
        {
            OverrideStorageLimit.maxResourcesCount = CTDynamicModMenu.CTDynamicModMenu.Instance.Config.Bind<int>(
            "Command Settings", 
            $"{Name}: MaxResourcesCount", 
            OverrideStorageLimit.maxResourcesCount, 
            "Max Number of resources"
        ).Value;
        }
        public override void SaveCustomConfig()
        {
            CTDynamicModMenu.CTDynamicModMenu.Instance.Config.Bind<int>(
            "Command Settings", 
            $"{Name}: MaxResourcesCount", 
            OverrideStorageLimit.maxResourcesCount, 
            "Max Number of resources"
        ).Value = OverrideStorageLimit.maxResourcesCount;
        }
    }
}
        
        
