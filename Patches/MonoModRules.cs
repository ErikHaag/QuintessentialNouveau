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

        Console.WriteLine("A");
        ILCursor cursor = new(new ILContext(method));
        Console.WriteLine("B");
        if (!cursor.TryGotoNext(MoveType.After,
            instr => instr.MatchLdstr("NonSteamUser"),
            instr => instr.MatchStloc(0),
            instr => instr.MatchNop()))
        {
            Console.WriteLine("Failed to mod save path; No static string load.");
            throw new Exception();
        }
        Console.WriteLine("C");
        cursor.MoveAfterLabels();

        TypeDefinition holder = MonoModRule.Modder.FindType("class_250").Resolve();
        MethodDefinition m = holder.Methods.First((m) => m.Name.Equals("AugmentDirectoryName"));

        cursor.Emit(OpCodes.Ldloc_0);
        cursor.Emit(OpCodes.Callvirt, m);
        cursor.Emit(OpCodes.Stloc_0);
    }
}
