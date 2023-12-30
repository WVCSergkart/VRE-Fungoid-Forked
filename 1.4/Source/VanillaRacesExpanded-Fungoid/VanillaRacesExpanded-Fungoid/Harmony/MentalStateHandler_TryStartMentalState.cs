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



    [HarmonyPatch(typeof(MentalStateHandler))]
    [HarmonyPatch("TryStartMentalState")]
    public static class VanillaRacesExpandedFungoid_MentalStateHandler_TryStartMentalState_Patch
    {
        [HarmonyPostfix]
        public static void CauseCoalescentMentalBreaks(MentalStateDef stateDef, string reason,Pawn ___pawn, bool causedByMood, bool forceWake,bool __result)

        {

            if(__result)
            {
                if(___pawn.genes?.HasGene(InternalDefOf.VRE_MindCoalescence)==true && stateDef != MentalStateDefOf.SocialFighting && stateDef != InternalDefOf.Crying && stateDef != InternalDefOf.Giggling)
                {
                    List<Pawn> pawnsToBeAffected = (from x in StaticCollectionsClass.pawns_and_xenotypes where x.Value == ___pawn.genes.Xenotype select x.Key).ToList();


                    foreach (Pawn pawn in pawnsToBeAffected)
                    {
                        pawn.mindState.mentalStateHandler.TryStartMentalState(stateDef, reason, forceWake, causedByMood);
                    }


                }

            }




        }
    }













}

