using CTDynamicModMenu.Commands;
using HarmonyLib;
using UnityEngine;

namespace FSCheat.Cheats
{
    internal class BypassRoomBuildRequirement : CustomCommand
    {
        public override string Name => "Bypass Room Build Requirement";
        public override string Description => "Bypass the dweller count requirement to unlock building a room";
        public override string Format => "/bypassroombuildrequirement";
        public override string Category => "Rooms";
        public override bool IsToggle => true;
        private static bool isEnabled = false;
        public override bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public override bool HasConfig { get; } = true;
        public override bool PersistConfig { get; } = true;

        public override void Execute(CommandInput message)
        {
            Utils.DisplayMessage("Bypass Room Build Requirement: " + (isEnabled ? "Enabled" : "Disabled"));
        }

        // Rooms locked by unmet objectives (e.g. not enough dwellers) report Locked — override to Unlocked
        [HarmonyPatch(typeof(UIRoomBuildList), "IsRoomAvailable")]
        [HarmonyPostfix]
        private static void IsRoomAvailablePostfix(ref ERoomBuildLockState __result)
        {
            if (!isEnabled) return;
            if (__result == ERoomBuildLockState.Locked)
                __result = ERoomBuildLockState.Unlocked;
        }

        // BiggerVault patches OnRoomEntryIndexChanged to set lock state and call SetAvailable() directly,
        // bypassing IsRoomAvailable entirely for Ultracite rooms. Running at Priority.Low ensures this
        // postfix executes after BiggerVault's and gets the final say.
        [HarmonyPatch(typeof(UIRoomBuildList), "OnRoomEntryIndexChanged")]
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Low)]
        private static void OnRoomEntryIndexChangedPostfix(UIRoomBuildList __instance, GameObject entry)
        {
            if (!isEnabled || entry == null) return;
            var component = entry.GetComponent<UIRoomBuildListItem>();
            if (component == null || component.m_RoomInfo == null) return;

            var fieldInfo = AccessTools.Field(typeof(UIRoomBuildList), "m_roomAvailableConstruction");
            if (fieldInfo == null) return;

            if (fieldInfo.GetValue(__instance) is not ERoomBuildLockState[] states) return;
            int roomIndex = (int)component.m_RoomInfo.m_eRoomType;
            if (roomIndex < 0 || roomIndex >= states.Length) return;

            if (states[roomIndex] == ERoomBuildLockState.Locked)
            {
                states[roomIndex] = ERoomBuildLockState.Unlocked;
                component.SetAvailable();
            }
        }
    }
}
