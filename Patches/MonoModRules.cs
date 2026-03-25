using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.InlineRT;
using System;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace MonoMod;

[MonoModCustomMethodAttribute(nameof(MonoModRules.PatchSavePath))]
class PatchSavePath : Attribute { }

[MonoModCustomMethodAttribute(nameof(MonoModRules.PatchSettingsInit))]
class PatchSettingsInit : Attribute { }

public class MonoModRules
{
    public static void PatchSavePath(MethodDefinition method, CustomAttribute attrib)
    {
        MonoModRule.Modder.Log("Patching save path");
        if (!method.HasBody)
        {
            Console.WriteLine("Failed to mod save path; No body.");
            throw new Exception();
        }

        ILCursor gremlin = new(new ILContext(method));
        if (!gremlin.TryGotoNext(MoveType.After,
            instr => instr.MatchLdstr("NonSteamUser"),
            instr => instr.MatchStloc(0),
            instr => instr.MatchNop()))
        {
            Console.WriteLine("Failed to mod save path; No static string load.");
            throw new Exception();
        }
        gremlin.MoveAfterLabels();

        TypeDefinition holder = MonoModRule.Modder.FindType("class_107").Resolve();
        MethodDefinition ADNMethod = holder.Methods.First((m) => m.Name.Equals("AugmentDirectoryName"));

        gremlin.Emit(OpCodes.Ldloc_0);
        gremlin.Emit(OpCodes.Callvirt, ADNMethod);
        gremlin.Emit(OpCodes.Stloc_0);
    }

    public static void PatchSettingsInit(MethodDefinition method, CustomAttribute attrib)
    {
        MonoModRule.Modder.Log("Patching settings");
        if (!method.HasBody)
        {
            Console.WriteLine("Failed to patch settings; No body.");
            throw new Exception();
        }

        ILCursor gremlin = new(new ILContext(method));

        if (!gremlin.TryGotoNext(MoveType.Before,
            instr => instr.MatchLdcI4(1),
            instr => instr.MatchStsfld("class_233", "field_2099")))
        {
            Console.WriteLine("Failed to patch settings; No assignment.");
            throw new Exception();
        }

        gremlin.Remove();
        gremlin.Emit(OpCodes.Ldc_I4_0);

    }
}
