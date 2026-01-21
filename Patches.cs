using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Bhvr.UnityShared;
using HarmonyLib;
using UnityEngine;

namespace FSCheat
{
    internal class Patches
    {

        internal static List<Dweller> dwellers = new List<Dweller>();
        internal static List<DwellerExperience> dwellerExperiences = new List<DwellerExperience>();
        internal static List<DwellerStats> dwellerStats = new List<DwellerStats>();
        internal static List<TrainingSlot> trainingSlots = new List<TrainingSlot>();
        internal static List<DwellerChild> dwellerChildren = new List<DwellerChild>();
        internal static Dictionary<DwellerChild, Task> growUpTasks = new Dictionary<DwellerChild, Task>();
        internal static Dictionary<DwellerChild, LivingQuartersRoom> livingQuarters = new Dictionary<DwellerChild, LivingQuartersRoom>();
        internal static Dictionary<DwellerChild, Dweller> dwellersToGrowUp = new Dictionary<DwellerChild, Dweller>();
        internal static byte[] keyBytes;
        internal static byte[] ivBytes;
        internal static string decryptPassphrase;
        private static Color legendaryColor = Color.magenta;
        private static Color normalColor = Color.green;
        private static Color rareColor = Color.cyan;
        
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



        [HarmonyPatch(typeof(Dweller), "get_BabyReady")]
        [HarmonyPostfix]
        private static void BabyReady(ref bool __result, ref Dweller __instance)
        {
            if (Plugin.instaBabyCheatEnabled)
            {
            __result = true;
            }
        }

        [HarmonyPatch(typeof(PersistenceManager), "Decrypt")]
        [HarmonyPostfix]
        private static void DecryptPostfix(ref string data)
        {
            Plugin.logger.LogInfo("Persistence Manager Decrypt: " + data);
        }

        [HarmonyPatch(typeof(StringCipher), "Decrypt")]
        [HarmonyPostfix]
        private static void CyDecryptPostfix(ref string cipherText, ref string passPhrase, ref string __result)
        {
            Plugin.logger.LogInfo("StringCipher Decrypt:" + __result + " with passphrase: " + passPhrase);
            decryptPassphrase = passPhrase;
        }

        [HarmonyPatch(typeof(StringCipher), "GetVectorBytes")]
        [HarmonyPostfix]
        private static void CyGetVectorBytesPostfix(ref byte[] __result)
        {
            Plugin.logger.LogInfo("Vector Bytes:" + BitConverter.ToString(__result) + " Length: " + __result.Length);
            Plugin.logger.LogInfo("Vector Bytes (IV) (hexdump):" + BitConverter.ToString(__result).Replace("-", " "));
            ivBytes = __result;
        }
        
        [HarmonyPatch(typeof(StringCipher), "GetPassphraseBytes")]
        [HarmonyPostfix]
        private static void CyGetPassphraseBytesPostfix(ref byte[] __result)
        {
            Plugin.logger.LogInfo("Passphrase Bytes:" + BitConverter.ToString(__result) + " Length: " + __result.Length);
            Plugin.logger.LogInfo("Passphrase Bytes (Key) (hex):" + BitConverter.ToString(__result).Replace("-", " "));
            keyBytes = __result;
        }
        
        

        [HarmonyPatch(typeof(Dweller), "Awake")]
        [HarmonyPostfix]
        private static void AddDwellerToList(ref Dweller __instance, ref string ___m_Name){
            dwellers.Add(__instance);
            Plugin.logger.LogInfo("Dweller added to list: "+ ___m_Name);
        }

        [HarmonyPatch(typeof(DwellerExperience))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(Dweller) })]
        [HarmonyPostfix]
        private static void DwellerExperienceConstructorPatch(DwellerExperience __instance, Dweller dweller)
        {
            dwellerExperiences.Add(__instance);
            Plugin.logger.LogInfo("DwellerExperience added to list: "+ dweller);
        }

        [HarmonyPatch(typeof(DwellerStats))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(Dweller) })]
        [HarmonyPostfix]
        private static void DwellerStatsConstructorPatch(DwellerStats __instance, Dweller inDweller)
        {
            dwellerStats.Add(__instance);
            Plugin.logger.LogInfo("DwellerStats added to list: "+ inDweller);
        }

        [HarmonyPatch(typeof(TrainingSlot))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(TrainingRoom) })]
        [HarmonyPostfix]
        private static void TrainingSlotGetRef(TrainingSlot __instance, TrainingRoom room)
        {
            trainingSlots.Add(__instance);
            Plugin.logger.LogInfo("TrainingSlot added to list: "+ room);
        }

        [HarmonyPatch(typeof(BaseSeasonDataManager), "ImportSaveData")]
        [HarmonyPrefix]
        private static void SeasonPassPremiumPlus(ref Dictionary<string, object> data)
        {
            if (!Plugin.overridePremiumPlusPass) return;
            try{
            if (data.ContainsKey("isPremium")){
                data["isPremium"] = true;
            } else {
                data.Add("isPremium", true);
            };
            if (data.ContainsKey("isPremiumPlus")){
                data["isPremiumPlus"] = true;
            } else {
                data.Add("isPremiumPlus", true);
            };
            Plugin.logger.LogInfo("Season Pass Premium Plus field set to true");
            } catch (Exception e){
                Plugin.logger.LogError("Error setting Season Pass Premium Plus field: " + e.Message);
            }
        }

        [HarmonyPatch(typeof(DwellerManager), "get_MaximumDwellerCount")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch(ref int __result)
        {
            if (!Plugin.patchOverrideMaxDwellers) return;
            __result = 2147483647;
        }

        [HarmonyPatch(typeof(DwellerManager), "get_VaultIsWithMaxPopulation")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch2(ref bool __result)
        {
            if (!Plugin.patchOverrideMaxDwellers) return;
            __result = false;
        }

        [HarmonyPatch(typeof(Vault), "CanAddDwellers")]
        [HarmonyPostfix]
        private static void MaxDwellersPatch3(ref bool __result)
        {
            if (!Plugin.patchOverrideLivingQuarters) return;
            __result = true;
        }

        [HarmonyPatch(typeof(ItemParameters), "get_LegendaryCardGlow")]
        [HarmonyPostfix]
        private static void LegendaryGlowPatch(ref Color __result)
        {
            __result = legendaryColor;
        }
        [HarmonyPatch(typeof(ItemParameters), "get_NormalCardGlow")]
        [HarmonyPostfix]
        private static void NormalGlowPatch(ref Color __result)
        {
            __result = normalColor;
        }
        [HarmonyPatch(typeof(ItemParameters), "get_RareCardGlow")]
        [HarmonyPostfix]
        private static void RareGlowPatch(ref Color __result)
        {
            __result = rareColor;
        }

        [HarmonyPatch(typeof(ItemParameters), "get_LegendaryCardBorder")]
        [HarmonyPostfix]
        private static void LegendaryBorderPatch(ref Color __result)
        {
            __result = legendaryColor;
        }
        [HarmonyPatch(typeof(ItemParameters), "get_NormalCardBorder")]
        [HarmonyPostfix]
        private static void NormalBorderPatch(ref Color __result)
        {
            __result = normalColor;
        }
        [HarmonyPatch(typeof(ItemParameters), "get_RareCardBorder")]
        [HarmonyPostfix]
        private static void RareBorderPatch(ref Color __result)
        {
            __result = rareColor;
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
