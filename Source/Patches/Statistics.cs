namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.Unlockables.Statistics))]
    internal class Statistics
    {
        [HarmonyLib.HarmonyPatch("CountUnlocked")]
        [HarmonyLib.HarmonyPostfix]
        private static void CountUnlocked_Postfix()
        {
            if (!NS_Core.Vars.sTab.sMain.bUnlock_WorldsAndToys)
                return;

            foreach (Klei.HotLava.Unlockables.Unlockable unlockable in Klei.HotLava.Unlockables.Statistics.AllUnlockables)
            {
                if (!Klei.HotLava.Unlockables.Statistics.HasUnlockedUnlockable(unlockable))
                    Klei.HotLava.Unlockables.Statistics.UnlockUnlockable(unlockable, true);
            }

            foreach (Klei.HotLava.Unlockables.Unlockable achievement in Klei.HotLava.Unlockables.Statistics.GetAllAchievementsForCurrentPlatform())
            {
                if (!Klei.HotLava.Unlockables.Statistics.HasUnlockedUnlockable(achievement))
                {
                    Klei.HotLava.Unlockables.Statistics.UnlockUnlockable(achievement, true);

                    if (Klei.HotLava.DistributionPlatform.DoesLocalUserExist)
                        Klei.HotLava.DistributionPlatform.Inst.ReportAchievement(achievement.name);
                }
            }
        }
    }
}