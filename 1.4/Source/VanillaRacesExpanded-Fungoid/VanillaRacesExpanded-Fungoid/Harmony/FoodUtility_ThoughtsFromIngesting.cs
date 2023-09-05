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

        public static List<PreceptDef> listOfPrecepts = new List<PreceptDef>(){InternalDefOf.Cannibalism_Acceptable, InternalDefOf.Cannibalism_Preferred,
                InternalDefOf.Cannibalism_RequiredStrong, InternalDefOf.Cannibalism_RequiredRavenous };


        [HarmonyPostfix]
        public static void AddMushroomThought(Pawn ingester, Thing foodSource, ThingDef foodDef)

        {
            

            if (ingester.Ideo!=null && ingester.genes?.HasGene(InternalDefOf.VRE_FungalFlesh)==true)
            {
                if (foodDef.IsFungus && IdeoNotCannibal(ingester))
                {
                    
                    Thought_Memory thought_Memory = ThoughtMaker.MakeThought(ThoughtDefOf.AteHumanlikeMeatDirectCannibal, null);
                    ingester.needs.mood.thoughts.memories.TryGainMemory(thought_Memory);

                    ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: false);
                    
                }
            }




        }

        public static bool IdeoNotCannibal(Pawn pawn)
        {
            
            foreach(PreceptDef precept in listOfPrecepts)
            {
                if (pawn.Ideo.HasPrecept(precept))
                {
                    return false;
                }

            }
            return true;

        }
    }













}

