using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedFungoid
{
    public class DamageWorker_ExtraFungoidInfection : DamageWorker_Cut
    {
       

        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
           
            Random random = new Random();

            

            if (pawn.RaceProps.Humanlike) {

                Pawn attackingPawn = dinfo.Instigator as Pawn;

                XenotypeDef xenotype = attackingPawn?.genes?.Xenotype ?? null;

                if (xenotype != null)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_GeneInfector) == false)
                    {

                        Hediff hediff = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);

                        if (hediff == null && random.NextDouble() > (1 - VanillaRacesExpandedFungoid_Settings.infectionChance))
                        {
                            try
                            {
                                pawn.health.AddHediff(InternalDefOf.VRE_GeneInfection, dinfo.HitPart);
                                Hediff hediffApplied = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);
                                HediffComp_GeneInfection comp = hediffApplied.TryGetComp<HediffComp_GeneInfection>();


                                List<GeneDef> xenogenesToPass = new List<GeneDef>();
                                foreach (Gene xenogene in attackingPawn.genes.Xenogenes)
                                {
                                    xenogenesToPass.Add(xenogene.def);
                                }
                                comp.xenogenes = xenogenesToPass;
                                Hediff_GeneInfection hediffWithClass = hediffApplied as Hediff_GeneInfection;
                                if (attackingPawn.genes.CustomXenotype != null)
                                {
                                    comp.xenotypeName = attackingPawn.genes.CustomXenotype.name.CapitalizeFirst();
                                    hediffWithClass.xenotypeName = attackingPawn.genes.CustomXenotype.name.CapitalizeFirst();
                                    comp.iconDef = attackingPawn.genes.CustomXenotype.IconDef;

                                }
                                else
                                {
                                    comp.xenotypeName = attackingPawn.genes.Xenotype.LabelCap;
                                    hediffWithClass.xenotypeName = attackingPawn.genes.Xenotype.LabelCap;
                                    comp.iconDef = attackingPawn.genes.iconDef;

                                }





                            }
                            catch (Exception) { }

                        }

                    }

                }




            }

            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);


        }
    }
}