using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class MaxLevelDwellers : CustomCommand
    {
        public override string Name => "Max Level Dwellers";

        public override string Description => "Toggle for making dwellers be max level";

        public override string Format => "/maxdwel";
        public override string Category => "Dwellers";

        public override async void Execute(CommandInput message)
        {
             SlowlyLevelAsync(100);
        }
        private async void SlowlyLevelAsync(int pauseMilliseconds)
        {
            try{
                bool areMaxed = false;
                while(!areMaxed){
                    int leveledCount = 0;
                    foreach(var DwellerExperience in Patches.dwellerExperiences){
                        if (DwellerExperience.CurrentLevel >= 50) {
                            Utils.DisplayMessage("Dweller " + DwellerExperience.ToString() + " is already max level.");
                            continue;
                        }
                        DwellerExperience.LevelUP();
                        DwellerExperience.AddExp(float.MaxValue);
                        leveledCount++;
                        await System.Threading.Tasks.Task.Delay(pauseMilliseconds);
                    }
                    if (leveledCount == 0)
                    {
                        areMaxed = true;
                    }
                }
                Utils.DisplayMessage("All Dwellers are now max level.");
            }
            catch (Exception e)
            {
                Utils.DisplayError("Message: " + e.Message);
            }
        } 
    }
}