using CTDynamicModMenu.Commands;
using HarmonyLib;
using System.Reflection;

namespace FSCheat.Cheats
{
    internal class InstaAdultCheat : CustomCommand
    {
        public override string Name => "Instant Adult Cheat";

        public override string Description => "Toggle for making children become adults instantly";

        public override string Format => "/instaadult";
        public override bool IsToggle => true;
        public override string Category => "Dwellers";
        private static bool instaAdultOn = false;
        public override bool IsEnabled 
        { 
            get => instaAdultOn;
            set => instaAdultOn = value;
        }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Instaadult Cheat: " + (instaAdultOn ? "Enabled" : "Disabled"));
        }

        [HarmonyPatch(typeof(DwellerChild), MethodType.Constructor, new System.Type[] { typeof(Dweller) })]
        [HarmonyPostfix]
        public static void StartChildhoodTimerPostfix(DwellerChild __instance)
        {
            if (instaAdultOn)
            {
                // Schedule OnGrowUp to be called after 10 seconds
                MonoSingleton<TaskMgr>.Instance.NewTask(new TimeUnit(2f), (bool online) =>
                {
                    MethodInfo method = __instance.GetType().GetMethod("OnGrowUp", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        method.Invoke(__instance, new object[] { true });
                    }
                    else
                    {
                        Utils.DisplayMessage("Failed to find OnGrowUp method for DwellerChild");
                    }
                });
            }
        }

    }
}