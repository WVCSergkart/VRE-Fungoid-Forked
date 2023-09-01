using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace VanillaRacesExpandedFungoid
{
    public class MapComponent_CoalescenceTracker : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 30000; //30000, half a day
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

            Scribe_Collections.Look(ref xenotypesAndMood_backup, "xenotypesAndMood_backup", LookMode.Def, LookMode.Value, ref list2, ref list3);

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterXenotypeMood", 0, true);

        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                StaticCollectionsClass.xenotypesAndMood.Clear();
                StaticCollectionsClass.pawns_and_xenotypes.Clear();


                foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_MindCoalescence)==true)
                    {
                        StaticCollectionsClass.AddPawnAndXenotypeToList(pawn, pawn.genes.Xenotype);

                        float totalMood=0;
                        List<Thought> GetAllThoughtsWithoutCoalescence = (from x in GetAllThoughts(pawn) where x.def != InternalDefOf.VRE_CoalescenceThought select x).ToList();

                        foreach(Thought thought in GetAllThoughtsWithoutCoalescence)
                        {
                            //Log.Message("For pawn " + pawn + " adding thought " + thought.def.defName + " with mood " + thought.MoodOffset());

                            totalMood += thought.MoodOffset();
                        }
                        //Log.Message("For pawn " + pawn + " total mood is " + totalMood);
                        StaticCollectionsClass.AddXenotypeAndMoodToListOrAddMood(StaticCollectionsClass.pawns_and_xenotypes[pawn], totalMood);
                    }             
                
                }

                Dictionary<XenotypeDef, float> xenotypeAndMoodTemp = new Dictionary<XenotypeDef, float>();


                foreach (KeyValuePair<XenotypeDef, float> xenotypeAndMood in StaticCollectionsClass.xenotypesAndMood)
                {
                    float colonistCount = (from x in StaticCollectionsClass.pawns_and_xenotypes where x.Value == xenotypeAndMood.Key select x).Count();
                    xenotypeAndMoodTemp[xenotypeAndMood.Key] = StaticCollectionsClass.xenotypesAndMood[xenotypeAndMood.Key];
                    xenotypeAndMoodTemp[xenotypeAndMood.Key] /= colonistCount;

                    //Log.Message("Average mood for xenotype " + xenotypeAndMood.Key.defName + " is " + StaticCollectionsClass.xenotypesAndMood[xenotypeAndMood.Key] / colonistCount + " with pawn count of " + colonistCount);
                }
                StaticCollectionsClass.xenotypesAndMood = xenotypeAndMoodTemp;

                xenotypesAndMood_backup = StaticCollectionsClass.xenotypesAndMood;

                tickCounter = 0;
            }



        }

        public List<Thought> GetAllThoughts(Pawn pawn)
        {
            List<Thought> outThoughts = new List<Thought>();
            pawn.needs.mood.thoughts.GetAllMoodThoughts(outThoughts);
            return outThoughts;
        }




    }


}



