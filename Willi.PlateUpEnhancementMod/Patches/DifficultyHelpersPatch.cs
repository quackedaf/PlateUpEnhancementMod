﻿using HarmonyLib;
using Kitchen;
using static Willi.PlateUpEnhancementMod.Config.ConfigHelper;

namespace Willi.PlateUpEnhancementMod.Patches
{
    [HarmonyPatch(typeof(DifficultyHelpers))]
    public static class DifficultyHelpersPatch
    {
        [HarmonyPatch("TotalShopCount")]
        [HarmonyPrefix]
        public static bool TotalShopCountPrefix(ref int __result)
        {

            if (DefaultShopOverrideSettings.Value)
            {
                __result = DefaultShopNumberOfItems.Value;
                return false;
            }

            return true;
        }

        [HarmonyPatch("BaseUpgradedShopChance")]
        [HarmonyPostfix]
        public static bool UpgradedShopChance_Postfix(int day, ref float __result)
        {
            if (DefaultShopOverrideSettings.Value)
            {
                __result = __result * DefaultShopUpgradedChance.Value;
                return false;
            }

            return true;
        }

        [HarmonyPatch("MoneyRewardPlayerModifier")]
        [HarmonyPostfix]
        public static void MoneyRewardPlayerModifier_Postfix(ref float __result, int player_count)
        {
            __result *= MoneyRewardMultiplier.Value;
        }

        [HarmonyPatch("CustomerModifierRateModifier")]
        [HarmonyPostfix]
        public static void CustomerModifierRateModifierPrefix(ref float __result)
        {
            __result *= NumberOfCustomersMultiplier.Value;
        }
    }
}
