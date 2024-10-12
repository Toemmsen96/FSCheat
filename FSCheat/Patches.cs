using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bhvr.UnityShared;
using HarmonyLib;
using UnityEngine;

namespace FSCheat
{
    internal class Patches
    {

        private static GameResources gRInstance;
        internal static bool nukaCheat = false;
        internal static bool instaBabyCheat = false;
        internal static bool maxLevelDwellers = false;
        internal static List<Dweller> dwellers = new List<Dweller>();
        internal static List<DwellerExperience> dwellerExperiences = new List<DwellerExperience>();
        internal static List<DwellerStats> dwellerStats = new List<DwellerStats>();
        internal static List<TrainingSlot> trainingSlots = new List<TrainingSlot>();
        
        [HarmonyPatch(typeof(Application), "get_isEditor")]
        [HarmonyPostfix]
        private static void EnableEditor(ref bool __result)
        {
            __result = true;
            //Plugin.logger.LogInfo("Editor enabled");
        }

        /*
        [HarmonyPatch(typeof(Storage), "HasResources")]
        [HarmonyPostfix]
        private static void InfiniteResources(ref bool __result, Storage __instance)
        {
            // Check if the instance or resources are null before applying the patch
            if (__instance == null)
            {
                Plugin.logger.LogError("Storage or resources are null, cannot apply infinite resources.");
                return;
            }

            // Apply your infinite resources logic if the check passes
            __result = true;
            Plugin.logger.LogInfo("Infinite resources enabled");  
        }
        */


        [HarmonyPatch(typeof(GameResources), "get_Nuka")]
        [HarmonyPostfix]
        private static void InfiniteNuka(ref float __result, ref GameResources __instance)
        {
            if (nukaCheat){
            //__result = 999999f;
            //Plugin.logger.LogInfo("Infinite Nuka enabled");
            gRInstance = __instance;
            gRInstance.Nuka = 999999f;
            //gRInstance.Food = 9999999f;
            //gRInstance.Water = 9999999f;
            //gRInstance.Power = 9999999f;
            gRInstance.StimPack = 99f;
            gRInstance.RadAway = 99f;
            gRInstance.Lunchbox = 10f;
            gRInstance.NukeColaQuantum = 999f;
            gRInstance.MrHandy = 0f;
            gRInstance.PetCarrier = 0f;
            }

        }

        [HarmonyPatch(typeof(Dweller), "get_BabyReady")]
        [HarmonyPostfix]
        private static void BabyReady(ref bool __result, ref Dweller __instance)
        {
            if (instaBabyCheat){
            __result = true;
            }
        }

        [HarmonyPatch(typeof(Dweller), "Awake")]
        [HarmonyPostfix]
        private static void AddDwellerToList(ref Dweller __instance, ref string ___m_Name){
            dwellers.Add(__instance);
            Utils.DisplayMessage("Dweller added to list: "+ ___m_Name);
        }

        [HarmonyPatch(typeof(DwellerExperience))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(Dweller) })]
        [HarmonyPostfix]
        private static void DwellerExperienceConstructorPatch(DwellerExperience __instance, Dweller dweller)
        {
            dwellerExperiences.Add(__instance);
            Utils.DisplayMessage("DwellerExperience added to list: "+ dweller);
        }

        [HarmonyPatch(typeof(DwellerStats))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(Dweller) })]
        [HarmonyPostfix]
        private static void DwellerStatsConstructorPatch(DwellerStats __instance, Dweller inDweller)
        {
            dwellerStats.Add(__instance);
            Utils.DisplayMessage("DwellerStats added to list: "+ inDweller);
        }

        [HarmonyPatch(typeof(TrainingSlot))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(TrainingRoom) })]
        [HarmonyPostfix]
        private static void TrainingSlotGetRef(TrainingSlot __instance, TrainingRoom room)
        {
            trainingSlots.Add(__instance);
            Utils.DisplayMessage("TrainingSlot added to list: "+ room);
        }


        /*
        [HarmonyPatch(typeof(GameResources), "get_Food")]
        [HarmonyPostfix]
        private static void InfiniteFood(ref float __result, ref GameResources __instance)
        {
            __result = 999999f;
            Plugin.logger.LogInfo("Infinite Food enabled");
            gRInstance = __instance;
        }
        */

    }
}
