using System.Collections.Generic;
using Verse;
using System.Linq;
using UnityEngine;
using System;



namespace VanillaRacesExpandedFungoid
{
    public class VanillaRacesExpandedFungoid_Settings : ModSettings

    {

        public static float infectionChance = baseInfectionChance;
        public const float baseInfectionChance = 0.2f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref infectionChance, "infectionChance", baseInfectionChance);
        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.Gap(10f);

            var infectionLabel = ls.LabelPlusButton("VRE_InfectionChance".Translate() + ": " + infectionChance.ToStringPercent(), "VRE_InfectionChanceDesc".Translate());
            infectionChance = (float)Math.Round(ls.Slider(infectionChance, 0.01f, 1), 2);

            if (ls.Settings_Button("VRE_Reset".Translate(), new Rect(0f, infectionLabel.position.y + 35, 250f, 29f)))
            {
                infectionChance = baseInfectionChance;
            }


            ls.End();
        }



    }










}
