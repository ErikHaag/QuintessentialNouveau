using MonoMod;
using Quintessential;

#pragma warning disable IDE1006 // Naming Styles

[MonoModPatch("class_107")]
public class patch_OSAL
{
    [PatchSavePath]
    [MonoModIgnore]
    public static extern string method_127();

    public static string AugmentDirectoryName(string path)
    {
        return path + "-QN";
    }
}
