using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class MaxLevelDwellers : CustomCheat
    {
        public override string Name => "Max Level Dwellers";

        public override string Description => "Toggle for making dwellers be max level";

        public override string Format => "/maxdwel";

        public override void Execute(CheatInput message)
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