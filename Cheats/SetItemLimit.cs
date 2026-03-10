using System;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetItemLimit : CustomCommand
    {
        public override string Name => "Set Item Limit";

        public override string Description => "Set the item limit for inventory, needs to be used with /overrideitemlimit";
        public override string Format => "/setitemlimit <amount>";
        public override string Category => "Items";
        public override bool IsToggle => false;
        public override bool HasConfig => true;
        public override bool PersistConfig => true;

        public override void Execute(CommandInput message)
        {
            if (message.Args.Count < 1)
            {
                Utils.DisplayMessage("Usage: /setitemlimit <amount>");
                return;
            }

            if (!int.TryParse(message.Args[0], out int amount))
            {
                Utils.DisplayMessage("Invalid amount. Please enter a valid number.");
                return;
            }
            SaveCustomConfig();
            OverrideItemLimit.maxItemCount = amount;
            if (OverrideItemLimit.overrideOn)
            {
                OverrideItemLimit.currentInventory?.SetMaxItems(amount);
            }
            Utils.DisplayMessage($"Item limit set to {amount}");
        }
        public override void LoadCustomConfig()
        {
            OverrideItemLimit.maxItemCount = CTDynamicModMenu.CTDynamicModMenu.Instance.Config.Bind<int>(
            "Command Settings", 
            $"{Name}: MaxItemCount", 
            OverrideItemLimit.maxItemCount, 
            "Max Number of items"
        ).Value;
        }
        public override void SaveCustomConfig()
        {
            CTDynamicModMenu.CTDynamicModMenu.Instance.Config.Bind<int>(
            "Command Settings", 
            $"{Name}: MaxItemCount", 
            OverrideItemLimit.maxItemCount, 
            "Max Number of items"
        ).Value = OverrideItemLimit.maxItemCount;
        }
    }
}
        
        
