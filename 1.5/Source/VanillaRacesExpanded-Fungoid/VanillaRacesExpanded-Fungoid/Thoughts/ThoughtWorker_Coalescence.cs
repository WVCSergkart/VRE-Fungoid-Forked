
using RimWorld;
using Verse;

namespace VanillaRacesExpandedFungoid
{

    public class ThoughtWorker_Coalescence : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            if (!StaticCollectionsClass.pawns_and_xenotypes.ContainsKey(pawn))
            {
                return false;
            }          
            
            else
            {
                return ThoughtState.ActiveAtStage(0);
            }
           
        }

        public override float MoodMultiplier(Pawn pawn)
        {
            if (StaticCollectionsClass.xenotypesAndMood.ContainsKey(StaticCollectionsClass.pawns_and_xenotypes[pawn]))
            {

                return (StaticCollectionsClass.xenotypesAndMood[StaticCollectionsClass.pawns_and_xenotypes[pawn]]) / 5;
            }
            else return 1;

            
        }
    }
}
