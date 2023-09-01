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

        public static HediffDef VRE_GeneInfection;



    }
}