using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.InlineRT;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoMod;

[MonoModCustomMethodAttribute(nameof(MonoModRules.PatchSettingsStaticInit))]
class PatchSettingsStaticInit : Attribute { }

[MonoModCustomMethodAttribute(nameof(MonoModRules.PatchScoreManagerLoad))]
class PatchScoreManagerLoad : Attribute { }

public class MonoModRules
{
    public static void PatchSettingsStaticInit(MethodDefinition method, CustomAttribute attrib)
    {
        MonoModRule.Modder.Log("Patching settings static init");

        if (!method.HasBody)
        {
            Console.WriteLine("Failed to disable Steam setting in class_93; no body.");
            throw new Exception();
        }

        ILCursor gremlin = new(new ILContext(method));

        if (!gremlin.TryGotoNext(MoveType.Before,
            instr => instr.MatchLdcI4(1),
            instr => instr.MatchStsfld("class_93", "field_551")))
        {
            Console.WriteLine("Failed to disable Steam setting in class_93; no assignment.");
            throw new Exception();
        }

        gremlin.Remove();
        gremlin.Emit(OpCodes.Ldc_I4_0);

    }

    public static void PatchScoreManagerLoad(MethodDefinition method, CustomAttribute attrib)
    {
        MonoModRule.Modder.Log("Patching score manager");
        if (!method.HasBody)
        {
            Console.WriteLine("Failed to disable Steam submission; no body");
            throw new Exception();
        }

        ILCursor gremlin = new(new ILContext(method));

        if (!gremlin.TryGotoNext(MoveType.After, instr => instr.Match(OpCodes.Brfalse_S))
            || !gremlin.TryGotoNext(MoveType.After, instr => instr.Match(OpCodes.Brfalse_S))
            || !gremlin.TryGotoNext(MoveType.After, instr => instr.Match(OpCodes.Brfalse_S)))
        {
            Console.WriteLine("Failed to disable Steam submission; no 3rd branch");
            throw new Exception();
        }
        // premature return;
        gremlin.Emit(OpCodes.Ret);
    }
}
