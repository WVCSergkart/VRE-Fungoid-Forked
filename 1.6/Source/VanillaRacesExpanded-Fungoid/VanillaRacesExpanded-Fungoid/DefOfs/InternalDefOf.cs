using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedFungoid
{
    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }

        public static ThoughtDef VRE_CoalescenceThought;
        public static GeneDef VRE_MindCoalescence;
        public static GeneDef VRE_GeneInfector;
        public static GeneDef VRE_FungalFlesh;
        public static GeneDef VRE_Repulsive;
        public static GeneDef VRE_Telepathy;
        public static GeneDef VRE_PartialAntirotLungs;

        public static ThingDef VRE_FungoidShipPart;

        public static XenotypeDef VRE_Fungoid;

        public static MentalStateDef VRE_SelectiveBerserk;
        public static MentalStateDef Crying;
        public static MentalStateDef Giggling;

        public static PawnKindDef VRE_AncientFungoid;


        public static HediffDef VRE_GeneInfection;
        public static HediffDef VRE_GeneInfected;

        [MayRequireIdeology]
        public static PreceptDef Cannibalism_Acceptable;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_Preferred;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_RequiredStrong;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_RequiredRavenous;

        public static ThoughtDef AteHumanlikeMeatDirectCannibal;

        public static SoundDef Hive_Spawn;

    }
}