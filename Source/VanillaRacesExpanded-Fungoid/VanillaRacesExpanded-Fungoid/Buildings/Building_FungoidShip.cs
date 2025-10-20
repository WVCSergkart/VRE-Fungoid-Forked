﻿using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using UnityEngine;
using UnityEngine.Networking;
using Verse;
using Verse.AI.Group;
using static HarmonyLib.Code;

namespace VanillaRacesExpandedFungoid
{

    public class Building_FungoidShip : Building
    {


        public bool doOnce = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref this.doOnce, nameof(this.doOnce));
            base.ExposeData();
        }

        protected override void TickInterval(int delta)
        {


            if (this.IsHashIntervalTick(60, delta) && this.Map != null)
            {

                GasUtility.AddGas(this.Position - new IntVec3(0, 0, -2), this.Map, GasType.RotStink, 2000);
            }
            base.TickInterval(delta);
        }

        public override void PostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            if (!doOnce) { PopUpFungoids(); }
            base.PostApplyDamage(dinfo, totalDamageDealt);
        }
        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            if (!doOnce) { PopUpFungoids(); }
            base.Destroy(mode);
        }

        public void PopUpFungoids()
        {
            doOnce = true;
            int numberOfFungoids = new IntRange(1, 4).RandomInRange;
            PawnGenerationRequest request = new PawnGenerationRequest(mustBeCapableOfViolence: true, colonistRelationChanceFactor: 0f,
                forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: false,
                inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false,
                worldPawnFactionDoesntMatter: false, biocodeWeaponChance: 0f, extraPawnForExtraRelationChance: null,
                relationWithExtraPawnChanceFactor: 0f, fixedGender: null, kind: InternalDefOf.VRE_AncientFungoid, faction: Faction.OfAncientsHostile,
                context: PawnGenerationContext.NonPlayer, tile: -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: false,
                canGeneratePawnRelations: false, biocodeApparelChance: 1f, validatorPreGear: null, validatorPostGear: null,
                forcedTraits: null, prohibitedTraits: null, minChanceToRedressWorldPawn: null, fixedBiologicalAge: 40,
                fixedChronologicalAge: 1000, fixedLastName: null, fixedBirthName: null, fixedTitle: null, fixedIdeo: null,
                forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, forcedXenogenes: null,
                forcedEndogenes: null, forcedXenotype: InternalDefOf.VRE_Fungoid, forcedCustomXenotype: null, allowedXenotypes: null, forceBaselinerChance: 0f
                , developmentalStages: DevelopmentalStage.Adult);
            for (int i = 0; i < numberOfFungoids; i++)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                Pawn pawnSpawned = (Pawn)GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 4), this.Map, Rot4.South);
                Hediff_GeneInfected hediff = (Hediff_GeneInfected)pawnSpawned.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfected);
                hediff.infectionCounterInDays = 10000;
                // pawnSpawned.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.VRE_SelectiveBerserk, null, forceWake: true, causedByMood: false, null, transitionSilently: false, causedByDamage: false);
                IEnumerable<Pawn> enumerable = from p in this.Map.mapPawns.AllPawns
                                               where p.RaceProps.Humanlike && p.GetLord() == null && p.Faction == Faction.OfAncientsHostile
                                               select p;
                if (enumerable.Any())
                {
                    LordMaker.MakeNewLord(Faction.OfAncientsHostile, new LordJob_AssaultColony(Faction.OfAncientsHostile, canKidnap: false, canTimeoutOrFlee: true, sappers: false, useAvoidGridSmart: false, canSteal: false), base.Map, enumerable);
                }
            }
        }





    }
}