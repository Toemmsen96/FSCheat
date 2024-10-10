using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class FinishAllTrainings : CustomCheat
    {
        public override string Name => "Finish all Trainings";

        public override string Description => "Finishes all dweller trainings";

        public override string Format => "/fintrain";

        public override void Execute(CheatInput message)
        {
            try{
            foreach(var trainingslot in Patches.trainingSlots){
                trainingslot.FinishTraining();
                Utils.DisplayMessage("Finished Training");
            }
            }
            catch (Exception e)
            {
                Utils.DisplayMessage("Error: " + e.Message);
            }
        }
    }
}