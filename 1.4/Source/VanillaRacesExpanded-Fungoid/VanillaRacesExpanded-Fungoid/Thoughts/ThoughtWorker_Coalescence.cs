
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
            if (StaticCollectionsClass.xenotypesAndMood[StaticCollectionsClass.pawns_and_xenotypes[pawn]]<0)
            {
                return ThoughtState.ActiveAtStage(0);
            }else
            {
                return ThoughtState.ActiveAtStage(1);
            }
           
        }

        public override float MoodMultiplier(Pawn pawn)
        {
            return StaticCollectionsClass.xenotypesAndMood[StaticCollectionsClass.pawns_and_xenotypes[pawn]]/5;
        }
    }
}
