using HarmonyLib;
using Mono.Cecil.Cil;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using VanillaRacesExpandedFungoid;
using Verse;
using Verse.AI;
using static HarmonyLib.Code;

namespace VanillaRacesExpandedFungoid
{
    [HarmonyPatch(typeof(HediffComp_SeverityFromGas), "CompPostTick")]
    public static class VanillaRacesExpandedFungoid_HediffComp_SeverityFromGas_CompPostTick_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            var check = AccessTools.Method(typeof(VanillaRacesExpandedFungoid_HediffComp_SeverityFromGas_CompPostTick_Patch), "GeneNumberModifier");
           
            for (var i = 0; i < codes.Count; i++)
            {
                yield return codes[i];
                if (i > 0 && codes[i].opcode == OpCodes.Stloc_1 && codes[i - 1].opcode == OpCodes.Mul)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Call, check);
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Mul);
                    yield return new CodeInstruction(OpCodes.Stloc_1);
                }
            }
        }


        public static float GeneNumberModifier(Pawn pawn)
        {
            if (pawn.HasActiveGene(InternalDefOf.VRE_PartialAntirotLungs))
            {
                return 0.5f;
            }
            
            return 1f;
        }





    }
}
