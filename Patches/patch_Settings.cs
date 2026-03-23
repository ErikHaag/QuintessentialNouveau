using MonoMod;

#pragma warning disable IDE1006 // Naming Styles

[MonoModPatch("class_93")]
public class patch_Settings
{
    [PatchSettingsStaticInit]
    public static extern void orig_cctor();

    [MonoModConstructor]
    public static void cctor()
    {
        orig_cctor();
    }
}