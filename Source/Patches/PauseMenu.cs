namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.PauseMenu))]
    internal class PauseMenu
    {
        [HarmonyLib.HarmonyPatch("OnEnable")]
        [HarmonyLib.HarmonyPostfix]
        private static void OnPauseMenuOpen_Postfix()
        {
            NS_Core.Vars.sGame.bPaused = true;
            NS_Core.Vars.sGame.bIn = false;
        }

        [HarmonyLib.HarmonyPatch("OnDisable")]
        [HarmonyLib.HarmonyPostfix]
        private static void OnPauseMenuClose_Postfix()
        {
            NS_Core.Vars.sGame.bPaused = false;
            NS_Core.Vars.sGame.bIn = true;
        }
    }
}