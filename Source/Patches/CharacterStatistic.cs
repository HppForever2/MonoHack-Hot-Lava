namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.Character.Progression.CharacterStatistics))]
    internal class CharacterStatistic
    {
        [HarmonyLib.HarmonyPatch("GainExperience")]
        [HarmonyLib.HarmonyPrefix]
        private static void GainExperience_Prefix(ref int amount)
        {
            amount = (NS_Core.Vars.sTab.sMain.bEnable_ExperienceTo60LVL ? 99999 : amount);
        }
    }
}