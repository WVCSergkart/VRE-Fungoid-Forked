

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using Verse.Sound;
using UnityEngine;

namespace VanillaRacesExpandedFungoid
{
    class HediffComp_StageByFungalDays : HediffComp
    {
        public int tickCounter = 1;

        public HediffCompProperties_StageByFungalDays Props
        {
            get
            {
                return (HediffCompProperties_StageByFungalDays)this.props;
            }
        }

     


        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (this.parent.pawn.IsHashIntervalTick(tickCounter))
            {
                tickCounter = 60000;

                Hediff_GeneInfected hediff = (Hediff_GeneInfected)this.parent.pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfected);


                if (hediff.infectionCounterInDays < Props.secondStageDays)
                {
                    this.parent.Severity = 1f;
                }else
                if (hediff.infectionCounterInDays < Props.thirdStageDays)
                {
                    this.parent.Severity = 0.5f;
                }
                else this.parent.Severity = 0.1f;
            }


        }



    }
}
