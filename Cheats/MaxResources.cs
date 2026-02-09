using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class MaxResources : CustomCommand
    {
        public override string Name => "Max Resources";

        public override string Description => "Toggle for always having power, water and food on max";

        public override string Format => "/maxres";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.Power = 2.1474836E+09f;
            resources.Water = 2.1474836E+09f;
            resources.Food = 2.1474836E+09f;
            Utils.DisplayMessage("Max Resources Cheat: Enabled");
        }
    }
}