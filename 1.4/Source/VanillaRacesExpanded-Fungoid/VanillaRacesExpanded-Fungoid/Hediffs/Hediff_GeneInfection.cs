using Verse;
using RimWorld;
using Verse.Sound;
using Verse.Noise;
using System.Collections.Generic;

namespace VanillaRacesExpandedFungoid
{
    class Hediff_GeneInfection : HediffWithComps
    {

        public string xenotypeName="";

        public override void ExposeData()
        {

            Scribe_Values.Look(ref this.xenotypeName, "xenotypeName");

            base.ExposeData();
        }



        public override string Description
        {
            get {
                return "VRE_InfectionDescription".Translate(xenotypeName.CapitalizeFirst());                 
            }
                
        }




        public override string Label
        {
            get
            {
                return "VRE_InfectionLabel".Translate(xenotypeName,this.Severity.ToStringPercent());
            }

        }

    }
}