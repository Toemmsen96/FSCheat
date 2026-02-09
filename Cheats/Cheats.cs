using System.Collections.Generic;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{internal class Cheats
    {
        public static List<CustomCommand> AllCheats { get; } = new List<CustomCommand> {
            new SetNuka(),
            new InstaBabyCheat(),
            new MaxLevelDwellers(),
            new FinishAllTrainings(),
            new MaxResources(),
            new SetBoxes(),
            new SetMrHandy(),
            new SetPetBoxes(),
            new SetQuantum(),
            new SetStimRad(),
            new SetMaxSpecial(),
            new OverrideWeaponDamage(),
            new OverPoweredPets(),
            new InstaAdultCheat(),
            new CreateRandomDweller(),
            new DecryptGameSaves(),
            new EncryptGameSaves(),
            new SetPokerchip(),
            //new UnlockAllRecipes(),
            new OverrideStorageLimit(),
            //new OverridePetLimit(),
            new ResetDwellerLevels(),
            new SetResources(),
            new SetStorageLimit(),
            };
    }
}