using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace VanillaRacesExpandedFungoid
{
    public class DamageWorker_ExtraFungoidInfection : DamageWorker_Cut
    {
       

        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
			DamageWorker_ExtraFungoidInfection_Bite.Infector(pawn, dinfo);
			base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);


        }
    }
}