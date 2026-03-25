using MonoMod;

#pragma warning disable IDE1006 // Naming Styles

[MonoModPatch("class_233")]
class patch_Settings
{
    [PatchSettingsInit] /* Temporary thing (hopefully) */
    [MonoModConstructor]
    [MonoModIgnore]
    public static extern void cctor();

}