using System;
using System.IO;

namespace Quintessential;

public class QuintessentialLoader
{
    public static string PathLightning;
    public static string PathMods;

    public static void PreInit()
    {
        try
        {
            PathLightning = Path.GetDirectoryName(typeof(GameLogic).Assembly.Location);
            PathMods = Path.Combine(PathLightning, "Mods");

        }
    }
}
