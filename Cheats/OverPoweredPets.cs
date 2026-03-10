using CTDynamicModMenu.Commands;
using HarmonyLib;
using Game.Actors.Pets;


namespace FSCheat.Cheats
{
    internal class OverPoweredPets : CustomCommand
    {
        public override string Name => "Overpowered Pets";

        public override string Description => "Overrides all bonus effects to be 1000f";

        public override string Format => "/overpoweredpets";
        public override string Category => "Pets";
        public override bool IsToggle => true;
        private static bool isOverriding = false;
        public override bool IsEnabled 
        { 
            get => isOverriding;
            set => isOverriding = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Overpowered Pets Override: " + (isOverriding ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(Pet), "GetFollowerBonusForEffect")]
        [HarmonyPostfix]
        public static void OverPoweredPetsPatch(ref float __result, ref EBonusEffect effect)
        {
            if (isOverriding)
            {
                __result = 1000f;
            }
        }


    }
}