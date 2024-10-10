using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class SetBoxes : CustomCheat
    {
        public override string Name => "Set Lunchboxes Cheat";

        public override string Description => "set how many boxes you want";

        public override string Format => "/setboxes <amount>";

        public override void Execute(CheatInput message)
        {
            try{
            if (!(message.Args[0].Length > 0))
            {
                MonoSingleton<Vault>.Instance.AddLunchBox();
                Utils.DisplayMessage("Set Boxes Cheat added one box");
            }
            else{
                for(float i=0; i<float.Parse(message.Args[0]); i++){
                    MonoSingleton<Vault>.Instance.AddLunchBox();
                }
                Utils.DisplayMessage("Added: " + message.Args[0]+ " boxes");
            }}
            catch (Exception e){
                Utils.DisplayMessage("Error: " + e.Message);
            }

        }
    }
}