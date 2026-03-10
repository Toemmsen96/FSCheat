using System;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{
    internal class SetMrHandy : CustomCommand
    {
        public override string Name => "Set MrHandy Boxes";

        public override string Description => "set how many MrHandy Boxes you want";

        public override string Format => "/setmrhandy <amount>";
        public override string Category => "Resources";

        public override void Execute(CommandInput message)
        {
            try{
            if (!(message.Args[0].Length > 0))
            {
                MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.MrHandy);
                Utils.DisplayMessage("Set MrHandyBoxes added one box");
            }
            else{
                for(float i=0; i<float.Parse(message.Args[0]); i++){
                    MonoSingleton<Vault>.Instance.AddLunchBox(ELunchBoxType.MrHandy);
                }
                Utils.DisplayMessage("Added: " + message.Args[0]+ " MrHandy boxes");
            }}
            catch (Exception e){
                Utils.DisplayError("Message: " + e.Message);
            }

        }
    }
}