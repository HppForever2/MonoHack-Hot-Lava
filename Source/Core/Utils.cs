namespace Hot_Lava_Cheat.Source.Core
{
    public class Utils
    {
        private static int iModBindReadDepth = 0;

        static public bool IsGameplayInputBlocked()
        {
            return NS_Visuals.GUIManager.bShowGUI || Vars.sGame.bPaused || !Vars.sGame.bIn || (Vars.sGame.bIn && UnityEngine.Input.GetKey(UnityEngine.KeyCode.Tab));
        }

        static public bool IsReadingModBind()
        {
            return iModBindReadDepth > 0;
        }

        static public bool GetKeyState(UnityEngine.KeyCode key)
        {
            iModBindReadDepth++;

            try
            {
                if (key >= UnityEngine.KeyCode.Mouse0 && key <= UnityEngine.KeyCode.Mouse6)
                    return UnityEngine.Input.GetMouseButton(key - UnityEngine.KeyCode.Mouse0);

                if (key == UnityEngine.KeyCode.None)
                    return false;

                return UnityEngine.Input.GetKey(key);
            }

            finally
            {
                iModBindReadDepth--;
            }
        }

        static public bool ShouldBlockGameKey(UnityEngine.KeyCode key)
        {
            if (IsReadingModBind() || key == UnityEngine.KeyCode.None)
                return false;

            return key == Vars.sTab.sMR.kcRecordKey
                || key == Vars.sTab.sMR.kcPlaybackKey
                || key == Vars.sTab.sMR.kcRewindKey
                || key == Vars.sTab.sMR.kcApplyFramesKey
                || key == Vars.sTab.sMR.kcInitNewRecordKey
                || key == Vars.sTab.sMR.kcRewindSpeedUpKey
                || key == Vars.sTab.sMR.kcRewindSpeedDownKey
                || key == Vars.sTab.sMR.kcRewindForwardKey
                || key == Vars.sTab.sMR.kcRewindBackwardKey;
        }

        static public bool ShouldBlockGameMouseButton(int iButton)
        {
            return ShouldBlockGameKey(UnityEngine.KeyCode.Mouse0 + iButton);
        }

        static public void UpdateCursorState()
        {
            if (IsGameplayInputBlocked())
            {
                UnityEngine.Cursor.visible = true;
                UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;

                return;
            }

            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        }

        static public bool ResetCurrentCourseRecord()
        {
            try
            {
                System.Type tInfo = typeof(Klei.HotLava.Game.Info);

                System.Reflection.PropertyInfo piCurrentLevelMetaData = HarmonyLib.AccessTools.Property(tInfo, "CurrentLevelMetaData");
                System.Reflection.PropertyInfo piCurrentGameMode = HarmonyLib.AccessTools.Property(tInfo, "CurrentGameMode");
                System.Reflection.MethodInfo miIsCurrentLevelSpecial = HarmonyLib.AccessTools.Method(tInfo, "IsCurrentLevelSpecial");
                Klei.HotLava.LevelMetaData level = piCurrentLevelMetaData?.GetValue(null, null) as Klei.HotLava.LevelMetaData;
                Klei.HotLava.Game.GameMode course = piCurrentGameMode?.GetValue(null, null) as Klei.HotLava.Game.GameMode;

                bool bIsCurrentLevelSpecial = miIsCurrentLevelSpecial != null && (bool)miIsCurrentLevelSpecial.Invoke(null, null);

                if (level == null || course == null || bIsCurrentLevelSpecial || !course.HasLeaderboard)
                {
                    NS_Core.Movement.Record.ShowNotification(Lang.GetStr("NO COURSE RECORD HERE", "ТУТ НЕТ РЕКОРДА КУРСА"));
                    return false;
                }

                System.Type tDataStore = HarmonyLib.AccessTools.TypeByName("Klei.HotLava.Leaderboard.DataStore");
                System.Reflection.PropertyInfo piImplementation = HarmonyLib.AccessTools.Property(tDataStore, "Implementation");

                object objImplementation = piImplementation?.GetValue(null, null);

                if (objImplementation == null)
                {
                    NS_Core.Movement.Record.ShowNotification(Lang.GetStr("DATASTORE ERROR", "ОШИБКА DATASTORE"));
                    return false;
                }

                System.Type tImplementation = objImplementation.GetType();

                System.Reflection.MethodInfo miGetLeaderboardName = HarmonyLib.AccessTools.Method(tImplementation, "GetLeaderboardName", new System.Type[]
                {
                    typeof(byte),
                    typeof(Klei.HotLava.LevelMetaData),
                    typeof(Klei.HotLava.Game.GameMode)
                });

                System.Reflection.MethodInfo miResetProfileCourseTime = HarmonyLib.AccessTools.Method(tImplementation, "ResetProfileCourseTime");
                System.Reflection.MethodInfo miResetLeaderboardCourseTime = HarmonyLib.AccessTools.Method(tImplementation, "ResetLeaderboardCourseTime");
                System.Reflection.MethodInfo miInvalidateLeaderboardData = HarmonyLib.AccessTools.Method(tDataStore, "InvalidateLeaderboardData");

                System.Type tProfileData = HarmonyLib.AccessTools.TypeByName("Klei.HotLava.Profiles.Data");

                System.Reflection.PropertyInfo piModifier = HarmonyLib.AccessTools.Property(tProfileData, "Modifier");
                System.Reflection.FieldInfo fiPlayerStatistics = HarmonyLib.AccessTools.Field(tProfileData, "s_PlayerStatistics");

                byte byModifier = piModifier != null ? (byte)piModifier.GetValue(null, null) : (byte)0;

                string strLeaderboardName = miGetLeaderboardName?.Invoke(objImplementation, new object[]
                {
                    byModifier,
                    level,
                    course
                }) as string;

                miResetProfileCourseTime?.Invoke(objImplementation, null);
                miResetLeaderboardCourseTime?.Invoke(objImplementation, null);

                if (!string.IsNullOrWhiteSpace(strLeaderboardName))
                    miInvalidateLeaderboardData?.Invoke(null, new object[] { strLeaderboardName });

                Klei.HotLava.Character.PlayerStatistics playerStatistics = fiPlayerStatistics?.GetValue(null) as Klei.HotLava.Character.PlayerStatistics;

                if (playerStatistics != null)
                {
                    Klei.HotLava.Character.GameModeStatistics gameModeStatistics = playerStatistics.GetGameModeStatistics(level, course);

                    if (gameModeStatistics != null && gameModeStatistics.m_Statistics != null)
                    {
                        gameModeStatistics.m_Statistics.Clear();
                        gameModeStatistics.m_Statistics.SetVersion(level, course);
                    }

                    playerStatistics.Save();
                }

                NS_Core.Movement.Record.ShowNotification(Lang.GetStr("COURSE RECORD DELETED", "РЕКОРД КУРСА УДАЛЕН"));
                return true;
            }

            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"[Utils] ResetCurrentCourseRecord error: {ex}");
                NS_Core.Movement.Record.ShowNotification(Lang.GetStr("DELETE ERROR", "ОШИБКА УДАЛЕНИЯ"));

                return false;
            }
        }

        static public void OpenUrl(string strUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strUrl))
                    return;

                System.Diagnostics.ProcessStartInfo objStartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = strUrl,
                    UseShellExecute = true
                };

                System.Diagnostics.Process.Start(objStartInfo);
            }

            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"[Utils] OpenUrl error: {ex}");
            }
        }

        public class Lang
        {
            static public bool InEN()
            {
                return Vars.sTab.sOther.iLanguage == 0 ? true : false;
            }

            static public bool InRU()
            {
                return InEN() ? false : true;
            }

            static public string GetStr(string strEN, string strRU)
            {
                return Vars.sTab.sOther.iLanguage == 0 ? strEN : strRU;
            }
        }
    }
}