using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class MaxResources : CustomCheat
    {
        public override string Name => "Max Resources";

        public override string Description => "Toggle for always having power, water and food on max";

        public override string Format => "/maxres";
        public override string Category => "Resources";

        public override void Execute(CheatInput message)
        {
            GameResources resources = MonoSingleton<Vault>.Instance.Storage.Resources;
            resources.Power = 9999999f;
            resources.Water = 9999999f;
            resources.Food = 9999999f;
            Utils.DisplayMessage("Max Ressources Cheat: Enabled");
        }
    }
}