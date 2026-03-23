using Quintessential;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it
#pragma warning disable IDE1006 // Naming Styles

class patch_GameLogic
{
    public extern void orig_method_978();
    public void method_978()
    {
        QuintessentialLoader.PreInit();
        orig_method_978();
        QuintessentialLoader.PostLoad();
    }
}
