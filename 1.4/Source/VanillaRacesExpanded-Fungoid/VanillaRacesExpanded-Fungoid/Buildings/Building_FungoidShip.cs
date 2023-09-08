using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using UnityEngine;
using UnityEngine.Networking;
using Verse;
using Verse.AI.Group;
using static HarmonyLib.Code;
using static UnityEngine.GraphicsBuffer;

namespace VanillaRacesExpandedFungoid
{

    public class Building_FungoidShip : Building
    {
      
       

      

        public override void Tick()
        {
           

            if (this.IsHashIntervalTick(60) &&this.Map!=null)
            {

                GasUtility.AddGas(this.Position-new IntVec3(0,0,-2), this.Map, GasType.RotStink, 2000);
            }
            base.Tick();
        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            

            int numberOfFungoids = UnityEngine.Random.Range(1, 4);
            PawnGenerationRequest request = new PawnGenerationRequest(mustBeCapableOfViolence: true, colonistRelationChanceFactor: 0f, 
                forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: false, 
                inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, 
                worldPawnFactionDoesntMatter: false, biocodeWeaponChance: 0f, extraPawnForExtraRelationChance: null,
                relationWithExtraPawnChanceFactor: 0f, fixedGender: null, kind: PawnKindDefOf.WildMan, faction: Faction.OfAncientsHostile, 
                context: PawnGenerationContext.NonPlayer, tile: -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: false, 
                canGeneratePawnRelations: false, biocodeApparelChance: 1f, validatorPreGear: null, validatorPostGear: null, 
                forcedTraits: null, prohibitedTraits: null, minChanceToRedressWorldPawn: null, fixedBiologicalAge: 40, 
                fixedChronologicalAge: 1000, fixedLastName: null, fixedBirthName: null, fixedTitle: null, fixedIdeo: null, 
                forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, forcedXenogenes: null, 
                forcedEndogenes: null, forcedXenotype: InternalDefOf.VRE_Fungoid, forcedCustomXenotype: null, allowedXenotypes: null, forceBaselinerChance: 0f
                , developmentalStages: DevelopmentalStage.Adult);
            for (int i=0; i < numberOfFungoids; i++)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                Pawn pawnSpawned = (Pawn)GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 4), this.Map, Rot4.South);
                Hediff_GeneInfected hediff = (Hediff_GeneInfected)pawnSpawned.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VRE_GeneInfected);
                hediff.infectionCounterInDays = 10000;
                pawnSpawned.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.VRE_SelectiveBerserk, null, forceWake: true, causedByMood: false, null, transitionSilently: false, causedByDamage: false);
            }
            base.Destroy(mode);
        }



    }
}