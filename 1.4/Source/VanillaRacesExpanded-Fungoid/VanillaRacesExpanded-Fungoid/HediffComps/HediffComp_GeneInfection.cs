using Verse;
using RimWorld;
using Verse.Sound;
using Verse.Noise;
using System.Collections.Generic;

namespace VanillaRacesExpandedFungoid
{
    class HediffComp_GeneInfection : HediffComp
    {

        public XenotypeDef xenotype;
        public List<GeneDef> endogenes = new List<GeneDef>();
        public List<GeneDef> xenogenes = new List<GeneDef>();
        public string xenotypeName;
        public XenotypeIconDef iconDef;

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
           
            Scribe_Defs.Look(ref this.xenotype, nameof(this.xenotype));
            Scribe_Collections.Look(ref this.endogenes, nameof(this.endogenes), LookMode.Def);
            Scribe_Collections.Look(ref this.xenogenes, nameof(this.xenogenes), LookMode.Def);
            Scribe_Values.Look(ref this.xenotypeName, nameof(this.xenotypeName));
            Scribe_Defs.Look(ref this.iconDef, nameof(this.iconDef));


        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (parent.Severity>0.99f) {

                if (parent.pawn.Map != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        IntVec3 c;
                        CellFinder.TryFindRandomReachableCellNear(parent.pawn.Position, parent.pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);

                        FilthMaker.TryMakeFilth(c, parent.pawn.Map, ThingDefOf.Filth_Slime);
                    }

                    SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(parent.pawn.Position, parent.pawn.Map, false));
                }
                if(xenotype != null)
                {
                    if (xenotype != XenotypeDefOf.Baseliner)
                    {
                        parent.pawn.genes?.SetXenotype(xenotype);

                    }
                    else
                
                    {
                        parent.pawn.genes?.SetXenotype(XenotypeDefOf.Baseliner);
                        parent.pawn.genes.xenotypeName = xenotypeName;
                        parent.pawn.genes.iconDef = iconDef;
               
                        
                        foreach (GeneDef geneDef in endogenes)
                        {
                            parent.pawn.genes.AddGene(geneDef, false);
                        }
                        foreach (GeneDef geneDef in xenogenes)
                        {
                            parent.pawn.genes.AddGene(geneDef, true);
                        }

                    }

                }

                parent.pawn.health.AddHediff(InternalDefOf.VRE_GeneInfected);

                parent.pawn.health.hediffSet.hediffs.Remove(parent);


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
                    parent.Severity= 1f;

                };
                yield return command_Action;
            }
        }



    }
}