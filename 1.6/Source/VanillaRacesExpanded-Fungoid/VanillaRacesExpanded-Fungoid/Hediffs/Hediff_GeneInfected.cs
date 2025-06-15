using Verse;
using RimWorld;
using Verse.Sound;
using Verse.Noise;
using System.Collections.Generic;
using static HarmonyLib.Code;

namespace VanillaRacesExpandedFungoid
{
    class Hediff_GeneInfected : HediffWithComps
    {

        public int infectionCounterInDays = 0;

        public override void ExposeData()
        {

            Scribe_Values.Look(ref this.infectionCounterInDays, "infectionCounterInDays");

            base.ExposeData();
        }

        public override void Tick()
        {
            base.Tick();

            if (this.pawn.IsHashIntervalTick(60000))
            {
                infectionCounterInDays++;
            }
        }

        public override string Label
        {
            get
            {
                return "VRE_InfectedLabel".Translate(infectionCounterInDays);
            }

        }
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            if (Prefs.DevMode)
            {
                Command_Action command_Action = new Command_Action();
                command_Action.defaultLabel = "DEBUG: Advanced days by 10";
                command_Action.icon = TexCommand.DesirePower;
                command_Action.action = delegate
                {
                    infectionCounterInDays += 10;
                   
                };
                yield return command_Action;
            }
        }



    }
}