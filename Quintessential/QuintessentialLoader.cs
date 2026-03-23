using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quintessential;

public class QuintessentialLoader
{
    // If this feels familiar, that's because it is.
    public static readonly string VersionString = "0.0.1";

    public static string PathLightning;
    public static string PathMods;
    public static string PathUnpackedMods;
    public static string PathBlacklist;


    private static List<string> blacklisted = new();
    //private static List<ModMeta> loaded = new();
    //private static List<ModMeta> waiting = new();

    public static void PreInit()
    {
        try
        {
            PathLightning = Path.GetDirectoryName(typeof(GameLogic).Assembly.Location);
            PathMods = Path.Combine(PathLightning, "Mods");
            PathUnpackedMods = Path.Combine(PathLightning, "UnpackedMods");

            Logger.Init();
            Logger.Log($"Quintessential Nouveau v{VersionString}");
            Logger.Log("Starting pre-init loading");

            QApi.Init();

            if (!Directory.Exists(PathMods))
                Directory.CreateDirectory(PathMods);

            if (Directory.Exists(PathUnpackedMods))
                Directory.Delete(PathUnpackedMods, true);
            Directory.CreateDirectory(PathUnpackedMods);

            PathBlacklist = Path.Combine(PathMods, "blacklist.txt");
            if (File.Exists(PathBlacklist))
                blacklisted = File.ReadAllLines(PathBlacklist).Select(l => (l.StartsWith("#") ? "" : l).Trim()).ToList();
            else
            {
                File.WriteAllText(PathBlacklist, @"# This is the blacklist. Lines starting with # are ignored.
ExampleFolderThatIWantToBlacklist
SomeZipIDontLike.zip");
            }
        }
        catch (Exception e)
        {
            if (Logger.Setup)
            {
                Logger.Log("Failed to pre-initialize!");
                Logger.Log(e);
            }
            throw;
        }
    }

    public static void PostLoad()
    {
        Logger.Log("Hi!");
        Logger.Log(class_264.field_2186);
    }
}