using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{internal class Cheats
    {
        public static List<CustomCheat> AllCheats { get; } = new List<CustomCheat> {
            new SetNuka(),
            new InstaBabyCheat(),
            new MaxLevelDwellers(),
            new FinishAllTrainings(),
            new MaxRessources(),
            new SetBoxes(),
            new SetMrHandy(),
            new SetPetBoxes(),
            new SetQuantum(),
            new SetStimRad(),
            new SetMaxSpecial(),
            //new ResetDwellerLevels(),
            };
    }
}