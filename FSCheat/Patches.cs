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
        
        [HarmonyPatch(typeof(Application), "get_isEditor")]
        [HarmonyPostfix]
        private static void EnableEditor(ref bool __result)
        {
            __result = true;
            Plugin.logger.LogInfo("Editor enabled");
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
            //__result = 999999f;
            //Plugin.logger.LogInfo("Infinite Nuka enabled");
            gRInstance = __instance;
            gRInstance.Nuka = 999999f;
            //gRInstance.Food = 300f;
            //gRInstance.Water = 999999f;
            gRInstance.StimPack = 99f;
            gRInstance.RadAway = 99f;
            gRInstance.Lunchbox = 20f;
            gRInstance.NukeColaQuantum = 999f;
            gRInstance.MrHandy = 0f;
            gRInstance.PetCarrier = 5f;
            //gRInstance.Power = 400f;
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
