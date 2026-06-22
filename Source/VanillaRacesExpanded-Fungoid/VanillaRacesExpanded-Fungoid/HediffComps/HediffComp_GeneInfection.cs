using Verse;
using RimWorld;
using Verse.Sound;
using Verse.Noise;
using System.Collections.Generic;
using UnityEngine.Analytics;
using System.Linq;
using WVC_XenotypesAndGenes;

namespace VanillaRacesExpandedFungoid
{
    class HediffComp_GeneInfection : HediffComp
    {


		//public List<GeneDef> xenogenes = new List<GeneDef>();
		//public string xenotypeName;
		//public XenotypeIconDef iconDef;

		public override bool CompShouldRemove => xenotypeHolder == null;

        public XenotypeHolder_Exposable xenotypeHolder;

        public HediffCompProperties_GeneInfection Props
        {
            get
            {
                return (HediffCompProperties_GeneInfection)this.props;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            //Scribe_Collections.Look(ref this.xenogenes, nameof(this.xenogenes), LookMode.Def);
            //Scribe_Values.Look(ref this.xenotypeName, nameof(this.xenotypeName));
            //Scribe_Defs.Look(ref this.iconDef, nameof(this.iconDef));
			Scribe_Deep.Look(ref xenotypeHolder, "xenotypeHolder");
		}

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            if (parent.Severity > 0.99f)
            {
				Pawn pawn = parent.pawn;
				if (pawn.Map != null)
                {
                    //for (int i = 0; i < 20; i++)
                    //{
                    //    IntVec3 c;
                    //    CellFinder.TryFindRandomReachableCellNearPosition(pawn.Position, pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);

                    //    FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_Slime);
                    //}
                    //InternalDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map, false));
					EffectsUtility.DoShapeshiftEffects_OnPawn(pawn);
				}
                //if (parent.pawn.genes != null)
                //{
                //    List<Gene> geneListBackup = (from x in parent.pawn.genes.GenesListForReading
                //        where x.def.exclusionTags?.Contains("HairStyle")==false
                //        select x).ToList();

                //    parent.pawn.genes.SetXenotype(XenotypeDefOf.Baseliner);

                //    foreach (Gene gene in geneListBackup)
                //    {
                //        parent.pawn.genes.AddGene(gene.def, false);

                //    }
      
                //    parent.pawn.genes.xenotypeName = xenotypeName;
                //    parent.pawn.genes.iconDef = iconDef;
             
                //    foreach (GeneDef geneDef in xenogenes)
                //    {
                //        parent.pawn.genes.AddGene(geneDef, true);
                //    }
                //}
                ReimplanterUtility.ConvertXenogenesToEndogenes(pawn);
                ReimplanterUtility.SetXenotype(pawn, xenotypeHolder);
                pawn.health.AddHediff(InternalDefOf.VRE_GeneInfected);
                pawn.health.RemoveHediff(parent);
                //parent.pawn.health.hediffSet.hediffs.Remove(parent);
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmos()
        {


            if (Prefs.DevMode)
            {
                Command_Action command_Action = new Command_Action();
                command_Action.defaultLabel = "DEBUG: Infect now";
                command_Action.icon = TexCommand.DesirePower;
                command_Action.action = delegate
                {
                    parent.Severity = 1f;

                };
                yield return command_Action;
            }
        }
    }
}