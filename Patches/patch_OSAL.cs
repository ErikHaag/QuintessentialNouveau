using MonoMod;
using Quintessential;

#pragma warning disable IDE1006 // Naming Styles

[MonoModPatch("class_250")]
public class patch_OSAL
{
    [PatchSavePath]
    [MonoModIgnore]
    public static extern string method_605();

    public static string AugmentDirectoryName(string path)
    {
        return path + "-QN";
    }
}
