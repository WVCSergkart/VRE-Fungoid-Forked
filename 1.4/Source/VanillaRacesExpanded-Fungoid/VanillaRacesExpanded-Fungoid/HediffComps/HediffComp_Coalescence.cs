
using Verse;
using RimWorld;

namespace VanillaRacesExpandedFungoid
{
    class HediffComp_Coalescence : HediffComp
    {

       

        public HediffCompProperties_Coalescence Props
        {
            get
            {
                return (HediffCompProperties_Coalescence)this.props;
            }
        }

     

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {

            StaticCollectionsClass.AddPawnAndXenotypeToList(this.parent.pawn, this.parent.pawn.genes.Xenotype);

        }

        public override void CompPostPostRemoved()
        {
            StaticCollectionsClass.RemovePawnAndXenotypeFromList(this.parent.pawn);

        }

        public override void Notify_PawnDied()
        {
            StaticCollectionsClass.RemovePawnAndXenotypeFromList(this.parent.pawn);

        }

        public override void Notify_PawnKilled()
        {
            StaticCollectionsClass.RemovePawnAndXenotypeFromList(this.parent.pawn);

        }
    }
}
