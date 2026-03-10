using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class MaxResources : CustomCommand
    {
        public override string Name => "Max Resources";

        public override string Description => "Toggle for always having power, water and food on max";

        public override string Format => "/maxres";
        public override string Category => "Resources";
        public override bool IsToggle => true;
        private static bool isOverriding = false;
        public override bool IsEnabled 
        { 
            get => isOverriding;
            set => isOverriding = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;
        private static GameResources originalResources;


        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            if (!IsEnabled)
            {
                resources.Power = originalResources.Power;
                resources.Water = originalResources.Water;
                resources.Food = originalResources.Food;
                Utils.DisplayMessage("Max Resources Cheat: Disabled");
                return;
            }
            else
            {
                originalResources = resources.Clone();
                resources.Power = float.MaxValue;
                resources.Water = float.MaxValue;
                resources.Food = float.MaxValue;
                Utils.DisplayMessage("Max Resources Cheat: Enabled");
            }
            
        }
    }
}