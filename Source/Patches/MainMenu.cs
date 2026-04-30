namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.MainMenu))]
    internal class MainMenu
    {
        [HarmonyLib.HarmonyPostfix]
        [HarmonyLib.HarmonyPatch("OnEnable")]
        private static void OnEnable_Postfix()
        {
            NS_Core.Vars.sGame.bPaused = false;
            NS_Core.Vars.sGame.bIn = false;
        }
    }
}