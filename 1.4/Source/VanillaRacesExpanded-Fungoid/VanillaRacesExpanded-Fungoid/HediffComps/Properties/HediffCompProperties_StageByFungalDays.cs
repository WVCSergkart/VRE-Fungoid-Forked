using System;
using Verse;
using System.Collections.Generic;


namespace VanillaRacesExpandedFungoid
{
    public class HediffCompProperties_StageByFungalDays : HediffCompProperties
    {

       
        public int secondStageDays = 120;
        public int thirdStageDays = 360;

        public HediffCompProperties_StageByFungalDays()
        {
            this.compClass = typeof(HediffComp_StageByFungalDays);
        }
    }
}
