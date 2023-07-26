using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace VanillaRacesExpandedFungoid
{
    public class MapComponent_CoalescenceTracker : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 30000;
        public static Dictionary<XenotypeDef, float> xenotypesAndMood_backup = new Dictionary<XenotypeDef, float>();
        List<XenotypeDef> list2;
        List<float> list3;

        public MapComponent_CoalescenceTracker(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {

            StaticCollectionsClass.xenotypesAndMood = xenotypesAndMood_backup;

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref xenotypesAndMood_backup, "xenotypesAndMood_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterFire", 0, true);

        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                StaticCollectionsClass.xenotypesAndMood.Clear();

                foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
                {
                    if (StaticCollectionsClass.pawns_and_xenotypes.ContainsKey(pawn))
                    {
                        StaticCollectionsClass.xenotypesAndMood[StaticCollectionsClass.pawns_and_xenotypes[pawn]] += pawn.needs.mood.CurInstantLevel;
                    }             
                
                }
               


                tickCounter = 0;
            }



        }




    }


}



