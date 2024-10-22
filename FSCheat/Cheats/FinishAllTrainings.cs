using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;


namespace FSCheat.Cheats
{
    internal class FinishAllTrainings : CustomCommand
    {
        public override string Name => "Finish all Trainings";

        public override string Description => "Finishes all dweller trainings";

        public override string Format => "/fintrain";
        public override string Category => "Dwellers";

        public override void Execute(CommandInput message)
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