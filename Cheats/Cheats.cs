using System.Collections.Generic;
using CTDynamicModMenu.Commands;

namespace FSCheat.Cheats
{internal class Cheats
    {
        public static List<CustomCommand> AllCheats { get; } = new List<CustomCommand> {

            // Resources
            new SetNuka(),
            new SetQuantum(),
            new SetStimRad(),
            new SetPokerchip(),
            new SetUltracite(),
            new SetPetBoxes(),
            new SetMrHandy(),
            new SetBoxes(),
            new OverrideStorageLimit(),
            new SetStorageLimit(),
            new MaxResources(),
            new SetResources(),

            // Items
            new SetItemLimit(),
            new OverrideItemLimit(),

            // Dwellers
            new CreateRandomDweller(),
            new CreateRandomLegendaryDweller(),
            new InstaBabyCheat(),
            new InstaAdultCheat(),
            new MaxLevelDwellers(),
            new ResetDwellerLevels(),
            new OverrideMaxDwellers(),
            new MaxHappyDwellers(),
            new FinishAllTrainings(),
            new SetMaxSpecial(),
            new OverrideWeaponDamage(),
            new OverPoweredPets(),

            // PremiumPass
            new OverridePremiumPlusPass(),

            //new UnlockAllRecipes(),
            //new OverridePetLimit(),

            // Decrypts and Encrypts
            new DecryptGameSaves(),
            new EncryptGameSaves(),
            };
    }
}