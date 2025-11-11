using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetPetBoxes : CustomCommand
    {
        public override string Name => "Set PetBoxes Cheat";

        public override string Description => "set how many Pet Boxes you want";

        public override string Format => "/setmrhandy <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            try{
            if (!(message.Args[0].Length > 0))
            {
                MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.PetCarrier);
                Utils.DisplayMessage("Set PetBoxes Cheat added one box");
            }
            else{
                for(float i=0; i<float.Parse(message.Args[0]); i++){
                    MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.PetCarrier);
                }
                Utils.DisplayMessage("Added: " + message.Args[0]+ " Pet boxes");
            }}
            catch (Exception e){
                Utils.DisplayMessage("Error: " + e.Message);
            }

        }
    }
}