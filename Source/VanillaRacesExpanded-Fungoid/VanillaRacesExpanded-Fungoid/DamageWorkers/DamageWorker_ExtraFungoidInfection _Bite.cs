using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using WVC_XenotypesAndGenes;

namespace VanillaRacesExpandedFungoid
{
    public class DamageWorker_ExtraFungoidInfection_Bite : DamageWorker_Bite
    {
       

        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
		{
			Infector(pawn, dinfo);
			base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
		}

		public static void Infector(Pawn pawn, DamageInfo dinfo)
		{
			if (!pawn.IsHuman())
			{
				return;
			}
			Pawn attackingPawn = dinfo.Instigator as Pawn;
			XenotypeDef xenotype = attackingPawn?.genes?.Xenotype ?? null;
			if (xenotype == null)
			{
				return;
			}
			if ((pawn.genes?.HasActiveGene(InternalDefOf.VRE_GeneInfector)) != false)
			{
				return;
			}
			Hediff hediff = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);
			if (hediff != null || !Rand.Chance(VanillaRacesExpandedFungoid_Settings.infectionChance))
			{
				return;
			}
			try
			{
				pawn.health.AddHediff(InternalDefOf.VRE_GeneInfection, dinfo.HitPart);
				Hediff hediffApplied = pawn?.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfection);
				hediffApplied.TryGetComp<HediffComp_GeneInfection>()?.xenotypeHolder = new(pawn, false, pawn.genes.Xenogenes.ConvertToDefs());
				//HediffComp_GeneInfection comp = hediffApplied.TryGetComp<HediffComp_GeneInfection>();
				//List<GeneDef> xenogenesToPass = new List<GeneDef>();
				//foreach (Gene xenogene in attackingPawn.genes.Xenogenes)
				//{
				//	xenogenesToPass.Add(xenogene.def);
				//}
				//comp.xenogenes = xenogenesToPass;
				//Hediff_GeneInfection hediffWithClass = hediffApplied as Hediff_GeneInfection;
				//if (attackingPawn.genes.CustomXenotype != null)
				//{
				//	comp.xenotypeName = attackingPawn.genes.CustomXenotype.name;
				//	hediffWithClass.xenotypeName = attackingPawn.genes.CustomXenotype.name;
				//	comp.iconDef = attackingPawn.genes.CustomXenotype.IconDef;
				//}
				//else
				//{
				//	comp.xenotypeName = attackingPawn.genes.Xenotype.LabelCap;
				//	hediffWithClass.xenotypeName = attackingPawn.genes.Xenotype.LabelCap;
				//	comp.iconDef = attackingPawn.genes.iconDef;
				//}
			}
			catch (Exception)
			{
			}
		}

	}
}