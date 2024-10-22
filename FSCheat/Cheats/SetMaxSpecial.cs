using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetMaxSpecial : CustomCommand
    {
        public override string Name => "Max Special Levels";

        public override string Description => "Max out all Dwellers special levels";

        public override string Format => "/maxspec";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            try{
                SpecialStatValues maxSpec = new SpecialStatValues();
                maxSpec.Strength = 10f;
                maxSpec.Perception = 10f;
                maxSpec.Endurance = 10f;
                maxSpec.Charisma = 10f;
                maxSpec.Intelligence = 10f;
                maxSpec.Agility = 10f;
                maxSpec.Luck = 10f;
            foreach(var DwellerStats in Patches.dwellerStats){
                DwellerStats.SetStatsBySpecialStatValues(maxSpec);
                Utils.DisplayMessage($"Successfully set max Special Values for: {DwellerStats}");
            }
            }
            catch (Exception e)
            {
                Utils.DisplayMessage("Error: " + e.Message);
            }
        }
    }
}