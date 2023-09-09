
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;




namespace VanillaRacesExpandedFungoid
{

    public class Gene_RandomGene : Gene
    {
        public List<GeneDef> genes = new List<GeneDef>();

        public override void PostAdd()
        {
            base.PostAdd();


            genes = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.defName.Contains("VRE_Fungoid_Hair_")).ToList();
            if (genes.Count > 0)
            {
                pawn.genes.AddGene(genes.RandomElement(), true);

            }
            pawn.genes.RemoveGene(this);






        }



    }
}
