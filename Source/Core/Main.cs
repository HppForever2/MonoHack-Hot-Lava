global using NS_Core = Hot_Lava_Cheat.Source.Core;
global using NS_Visuals = HotLava_Cheat.Source.Visuals;
global using NS_Patches = Hot_Lava_Cheat.Source.Patches;

namespace Hot_Lava_Cheat.Source.Core
{
    public class Main
    {
        public const string strCheatVersion = "1.0";
        public static string strGameVersion = "";

        public static void InitGameVersion()
        {
            strGameVersion = $"{BuildInfo.kRevision}";
        }
    }
}