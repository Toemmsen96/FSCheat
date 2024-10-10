using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class SetMrHandy : CustomCheat
    {
        public override string Name => "Set MrHandy Cheat";

        public override string Description => "set how many MrHandy Boxes you want";

        public override string Format => "/setmrhandy <amount>";

        public override void Execute(CheatInput message)
        {
            try{
            if (!(message.Args[0].Length > 0))
            {
                MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.MrHandy);
                Utils.DisplayMessage("Set MrHandyBoxes Cheat added one box");
            }
            else{
                for(float i=0; i<float.Parse(message.Args[0]); i++){
                    MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.MrHandy);
                }
                Utils.DisplayMessage("Added: " + message.Args[0]+ " MrHandy boxes");
            }}
            catch (Exception e){
                Utils.DisplayMessage("Error: " + e.Message);
            }

        }
    }
}