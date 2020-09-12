using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Garam_RaceAddon
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("CombinedDisabledWorkTags", MethodType.Getter)]
    public static class HarmonyPatches_CombinedDisabledWorkTags
    {
        [HarmonyPostfix]
        private static void Postfix(Pawn __instance, ref WorkTags __result)
        {
            if (__instance.def is RaceAddonThingDef thingDef)
            {
                __result |= thingDef.DisabledWorkTags;
            }
        }
    }
    [HarmonyPatch(typeof(Pawn_StoryTracker))]
    [HarmonyPatch("DisabledWorkTagsBackstoryAndTraits", MethodType.Getter)]
    public static class HarmonyPatches_DisabledWorkTagsBackstoryAndTraits
    {
        [HarmonyPostfix]
        private static void Postfix(Pawn ___pawn, ref WorkTags __result)
        {
            if (___pawn.def is RaceAddonThingDef thingDef)
            {
                __result |= thingDef.DisabledWorkTags;
            }
        }
    }
    [HarmonyPatch(typeof(Pawn))]
    public static class HarmonyPatches_GetDisabledWorkTypes
    {

        [HarmonyPatch(nameof(Pawn.GetDisabledWorkTypes)), HarmonyPostfix]
        private static void Postfix(Pawn __instance, bool permanentOnly, ref List<WorkTypeDef> __result)
        {
            if (__instance.def is RaceAddonThingDef thingDef)
            {
                foreach (WorkTypeDef def in thingDef.DisabledWorkTypes)
                {
                    if (!__result.Contains(def))
                    {
                        __result.Add(def);
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(WorkGiver))]
    [HarmonyPatch("ShouldSkip")]
    public static class HarmonyPatches_ShouldSkip
    {
        [HarmonyPostfix]
        private static void Postfix(Pawn pawn, WorkGiver __instance, ref bool __result)
        {
            if (!__result && !RaceAddonTools.CheckWorkGiver(pawn.def, __instance.def))
            {
                __result = true;
            }
        }
    }
}
