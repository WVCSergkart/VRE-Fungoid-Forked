using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace VanillaRacesExpandedFungoid
{



    public class VanillaRacesExpandedFungoid_Mod : Mod
    {


        public VanillaRacesExpandedFungoid_Mod(ModContentPack content) : base(content)
        {
            GetSettings<VanillaRacesExpandedFungoid_Settings>();
        }
        public override string SettingsCategory() => "VRE - Fungoid";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            VanillaRacesExpandedFungoid_Settings.DoWindowContents(inRect);
        }
    }


}
