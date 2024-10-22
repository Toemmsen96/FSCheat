using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class ResetDwellerLevels : CustomCommand
    {
        public override string Name => "Reset Dweller Levels";

        public override string Description => "Sets Dwellers to level 1";

        public override string Format => "/resdwlvl";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
        {
            try{
            /*
            foreach (var dweller in Patches.dwellers)
            {
                Utils.DisplayMessage("Maxing out dweller: " + dweller.Name);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Agility);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Charisma);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Endurance);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Intelligence);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Luck);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Perception);
                dweller.DebugLevelUpFromTrainingRoom(ESpecialStat.Strength);
            }
            */
            foreach(var DwellerExperience in Patches.dwellerExperiences){
                DwellerExperience.LevelUP();
                DwellerExperience.AddExp(1000000f);
            }
            }
            catch (Exception e)
            {
                Utils.DisplayMessage("Error: " + e.Message);
            }
        }
    }
}