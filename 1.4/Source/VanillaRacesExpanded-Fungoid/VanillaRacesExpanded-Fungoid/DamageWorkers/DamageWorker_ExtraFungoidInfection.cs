using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedFungoid
{
    public class DamageWorker_ExtraFungoidInfection : DamageWorker_Cut
    {
        public float InfectionChance = 0.8f;

        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            Random random = new Random();

            

            if (pawn.RaceProps.Humanlike) {

                Pawn attackingPawn = dinfo.Instigator as Pawn;

                XenotypeDef xenotype = attackingPawn?.genes?.Xenotype ?? null;
                
                if (xenotype != null)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_GeneInfector)==false)
                    {

                        Hediff hediff = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);

                        if (hediff == null && pawn.GetStatValue(StatDefOf.ToxicResistance, true) < 1f && random.NextDouble() > InfectionChance)
                        {
                            pawn.health.AddHediff(InternalDefOf.VRE_GeneInfection, dinfo.HitPart, null, null);
                            Hediff hediffApplied = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);
                            HediffComp_GeneInfection comp = hediffApplied.TryGetComp<HediffComp_GeneInfection>();
                            comp.xenotype = xenotype;
                            List<GeneDef> endogenesToPass = new List<GeneDef>();
                            foreach (Gene endogene in attackingPawn.genes.Endogenes)
                            {
                                endogenesToPass.Add(endogene.def);
                            }
                            comp.endogenes = endogenesToPass;
                            List<GeneDef> xenogenesToPass = new List<GeneDef>();
                            foreach (Gene xenogene in attackingPawn.genes.Xenogenes)
                            {
                                xenogenesToPass.Add(xenogene.def);
                            }
                            comp.xenogenes = xenogenesToPass;
                            comp.iconDef = attackingPawn.genes.iconDef;
                            comp.xenotypeName = attackingPawn.genes.xenotypeName;
                        }

                    }

                }





    }
            


            
        }
    }
}