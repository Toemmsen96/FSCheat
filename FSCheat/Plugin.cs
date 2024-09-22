using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace FSCheat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {

        private const string modGUID = "toemmsen.FSCheats";
        private const string modName = "FSCheats";
        private const string modVersion = "1.0.0";
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

            logger.LogWarning((object)"\r\n" +
                "  ______                                                                                                       \r\n" +
                " /_  __/  ____   ___    ____ ___    ____ ___    _____  ___    ____    _____                                    \r\n" +
                "  / /    / __ \\ / _ \\  / __ `__ \\  / __ `__ \\  / ___/ / _ \\  / __ \\  / ___/                                    \r\n" +
                " / /    / /_/ //  __/ / / / / / / / / / / / / (__  ) /  __/ / / / / (__  )                                     \r\n" +
                "/_/_____\\____/_\\___/ /_/ /_/_/_/ /_/ /_/ /_/_/____/  \\___/ /_/ /_/ /____/                            __        \r\n");
            logger.LogInfo(modGUID+" loaded");
        }
    }
}
