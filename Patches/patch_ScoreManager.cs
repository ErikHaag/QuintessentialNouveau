using MonoMod;
using static class_126;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it
#pragma warning disable IDE1006 // Naming Styles

class patch_ScoreManager
{

    // removes a steam-related call to upload scores

    [PatchScoreManagerLoad]
    [MonoModIgnore]
    public extern void method_1414(Puzzle param_5238, enum_148 param_5239, int param_5240);

    public extern void orig_method_1415(string str);
    public void method_1415(string str)
    {
        // no-op
    }
}   