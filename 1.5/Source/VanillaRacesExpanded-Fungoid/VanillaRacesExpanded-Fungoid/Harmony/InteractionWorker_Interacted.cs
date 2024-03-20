using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaRacesExpandedFungoid
{


    [HarmonyPatch(typeof(InteractionWorker))]
    [HarmonyPatch("Interacted")]
    public static class VanillaRacesExpandedFungoid_InteractionWorker_Interacted_Patch
    {
        [HarmonyPostfix]
        static void VomitIfFungoid(Pawn initiator, Pawn recipient)
        {
            if (initiator.genes?.HasGene(InternalDefOf.VRE_Repulsive) == true && recipient.genes?.HasGene(InternalDefOf.VRE_Repulsive) == false)
            {
                recipient.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
            }else if (initiator.genes?.HasGene(InternalDefOf.VRE_Repulsive) == false && recipient.genes?.HasGene(InternalDefOf.VRE_Repulsive) == true)
            {
                initiator.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
            }

        }
    }








}
