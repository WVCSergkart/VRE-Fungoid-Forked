
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaRacesExpandedFungoid
{

	public static class StaticCollectionsClass
	{


		
		// A list of pawns and xenotypes with the mind coalescence gene
		public static Dictionary<Pawn, XenotypeDef> pawns_and_xenotypes = new Dictionary<Pawn, XenotypeDef>();

		// A list of xenotypes and average mood
		public static Dictionary<XenotypeDef, float> xenotypesAndMood = new Dictionary<XenotypeDef, float>();



		public static void AddPawnAndXenotypeToList(Pawn pawn, XenotypeDef xenotype)
		{

			if (!pawns_and_xenotypes.ContainsKey(pawn))
			{
				if (xenotype != null) { pawns_and_xenotypes.Add(pawn, xenotype); }			

			}
		}

		public static void RemovePawnAndXenotypeFromList(Pawn pawn)
		{
			if (pawns_and_xenotypes.ContainsKey(pawn))
			{
				pawns_and_xenotypes.Remove(pawn);
			}

		}

		public static void AddXenotypeAndMoodToListOrAddMood(XenotypeDef xenotype, float mood)
		{
			if (xenotype != null)
			{
				if (!xenotypesAndMood.ContainsKey(xenotype))
				{
					xenotypesAndMood.Add(xenotype, 0);
				}
				xenotypesAndMood[xenotype] += mood;
			}
        }


    }
}
