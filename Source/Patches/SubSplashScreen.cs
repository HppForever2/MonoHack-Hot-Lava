namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.SubSplashScreen))]
    internal class SubSplashScreenPatch
    {
        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPostfix]
        private static void Update_Postfix(Klei.HotLava.UI.SubSplashScreen __instance)
        {
            if (!NS_Core.Movement.Record.ConsumePendingPlaybackCancel())
                return;

            __instance.DoSkip();
            NS_Core.Logger.Log($"[PLAYBACK CANCEL] SubSplashScreen skipped at frame {NS_Core.Movement.Record.CurrentFrame}");
        }
    }
}