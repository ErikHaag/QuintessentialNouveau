using Quintessential;
using MonoMod;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it
#pragma warning disable IDE1006 // Naming Styles

class patch_GameLogic
{
    public extern void orig_method_977();
    public void method_977()
    {
        QuintessentialLoader.PreInit();
        Logger.Log("Test A!");
        orig_method_977();
        Logger.Log("Test C!");
        QuintessentialLoader.PostLoad();
    }

    public extern void orig_method_991();
    public void method_991()
    {
        orig_method_991();
        QuintessentialLoader.LoadPuzzleContent();
    }

    public extern void orig_method_998(int exitCode);
    public void method_998(int exitCode)
    {
        QuintessentialLoader.Unload(exitCode);
        orig_method_998(exitCode);
    }
}
