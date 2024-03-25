using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;
using System.Security.Cryptography;


namespace VanillaRacesExpandedFungoid
{

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    public static class VanillaRacesExpandedFungoid_FoodUtility_ThoughtsFromIngesting_Patch
    {

        [HarmonyPostfix]
        public static void AddPorkThought(Pawn ingester, Thing foodSource, ThingDef foodDef)
        {
            if (foodDef.IsFungus && ingester.genes?.HasGene(InternalDefOf.VRE_FungalFlesh) == true)
            {
                ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);
            }
        }
    }

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("AddThoughtsFromIdeo")]
    public static class VanillaRacesExpandedFungoid_FoodUtility_AddThoughtsFromIdeo_Patch
    {

        [HarmonyPrefix]
        public static bool DisableNonCannibalFoodThought(HistoryEventDef eventDef, Pawn ingester, ThingDef foodDef)
        {
            if (foodDef.IsFungus && ingester.genes?.HasGene(InternalDefOf.VRE_FungalFlesh) == true && eventDef == HistoryEventDefOf.AteNonCannibalFood)
            {
                return false;
            }
            return true;
        }
    }











}





