using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using CTDynamicModMenu.Commands;


namespace FSCheat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("Toemmsen96.CTDynamicModMenu")]
    public partial class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "toemmsen.FSCheats";
        private const string modName = "FSCheats";
        private const string modVersion = "1.5.2";
        private readonly Harmony harmony = new Harmony(modGUID);
        internal static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
        private static Plugin instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

            }
            harmony.PatchAll(typeof(Patches));
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(Cheats.OverrideWeaponDamage));
            harmony.PatchAll(typeof(Cheats.OverPoweredPets));
            harmony.PatchAll(typeof(Cheats.InstaAdultCheat));
            harmony.PatchAll(typeof(Cheats.InstaBabyCheat));
            harmony.PatchAll(typeof(Cheats.OverrideStorageLimit));
            harmony.PatchAll(typeof(Cheats.OverrideMaxDwellers));
            harmony.PatchAll(typeof(Cheats.OverridePremiumPlusPass));
            harmony.PatchAll(typeof(Cheats.ResetDwellerLevels));
            harmony.PatchAll(typeof(Cheats.MaxHappyDwellers));
            harmony.PatchAll(typeof(Cheats.OverrideItemLimit));
            harmony.PatchAll(typeof(Cheats.FreeCraftingCheat));
            harmony.PatchAll(typeof(Cheats.InfiniteCraftingNuka));
            harmony.PatchAll(typeof(Cheats.BypassUpgradeDwellerRequirement));
            harmony.PatchAll(typeof(Cheats.BypassCraftingLevelRequirement));
            harmony.PatchAll(typeof(Cheats.BypassRoomBuildRequirement));

            logger.LogWarning((object)"\r\n" +
                "  ______                                                                         \r\n" +
                " /_  __/  ____   ___    ____ ___    ____ ___    _____  ___    ____    _____      \r\n" +
                "  / /    / __ \\ / _ \\  / __ `__ \\  / __ `__ \\  / ___/ / _ \\  / __ \\  / ___/\r\n" +
                " / /    / /_/ //  __/ / / / / / / / / / / / / (__  ) /  __/ / / / / (__  )  \r\n" +
                "/_/     \\____/ \\___/ /_/ /_/_/_/ /_/ /_/ /_/_/____/  \\___/ /_/ /_/ /____/\r\n");
            foreach (CustomCommand command in Cheats.Cheats.AllCheats)
            {
                CTDynamicModMenu.CTDynamicModMenu.Instance.RegisterCommand(command);
            }
            logger.LogInfo(modGUID+" loaded");
        }
    }
}