using Verse;
using RimWorld;
using Verse.Sound;
using Verse.Noise;
using System.Collections.Generic;
using System;

namespace VanillaRacesExpandedFungoid
{
    class Filth_RotEmitting : Filth
    {

        

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            GasUtility.AddGas(this.PositionHeld, this.Map, GasType.RotStink, 2000);
        }

    }
}