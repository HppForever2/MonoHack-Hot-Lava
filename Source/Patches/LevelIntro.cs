namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.Game.LevelIntro))]
    internal class LevelIntroPatch
    {
        private static readonly System.Reflection.MethodInfo miStopIntro = HarmonyLib.AccessTools.Method(typeof(Klei.HotLava.Game.LevelIntro), "StopIntro");

        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPostfix]
        private static void Update_Postfix(object __instance)
        {
            if (!NS_Core.Movement.Record.ConsumePendingPlaybackCancel())
                return;

            miStopIntro?.Invoke(__instance, null);
            NS_Core.Logger.Log($"[PLAYBACK CANCEL] LevelIntro skipped at frame {NS_Core.Movement.Record.CurrentFrame}");
        }
    }
}