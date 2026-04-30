using UnityEngine;

namespace HotLava_Cheat.Source.Visuals.Render
{
    public static class Tabs
    {
        private enum eMRDialogMode
        {
            None,
            Save,
            Load
        }

        private static eMRDialogMode eCurrentMRDialogMode = eMRDialogMode.None;
        private static string strMRSaveName = "record";
        private static UnityEngine.Vector2 vecMRLoadScroll = UnityEngine.Vector2.zero;
        private static bool bMRLoadScrollbarDragging = false;
        private static float flMRLoadScrollbarDragOffset = 0f;
        private static bool bFocusMRSaveTextField = false;
        private static bool bRenderingMRModal = false;
        private const string strMRSaveTextFieldControl = "mr_save_name_field";

        public static bool IsMRModalOpen()
        {
            return eCurrentMRDialogMode != eMRDialogMode.None;
        }

        public static bool IsBackgroundInteractionBlocked()
        {
            return eCurrentMRDialogMode != eMRDialogMode.None && !bRenderingMRModal;
        }

        private static void RenderBind(string sLabel, ref UnityEngine.KeyCode kcKey, ref bool bToggle, float flLabelWidth, float flKeybindWidth, float flModeWidth)
        {
            string[] arrModeOptions = { "Hold", "Toggle" };
            float flGapWidth = 4f;

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Label(sLabel, Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(flLabelWidth));
            UnityEngine.GUILayout.Space(flGapWidth);

            kcKey = Controls.KeybindButton("", kcKey, flKeybindWidth);

            UnityEngine.GUILayout.Space(flGapWidth);

            int iMode = bToggle ? 1 : 0;
            iMode = Controls.DropdownCompact(sLabel, iMode, arrModeOptions, flModeWidth);
            bToggle = iMode == 1;

            UnityEngine.GUILayout.EndHorizontal();
        }

        private static void RenderSimpleBind(string sLabel, ref UnityEngine.KeyCode kcKey, float flLabelWidth, float flKeybindWidth)
        {
            float flGapWidth = 4f;

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Label(sLabel, Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(flLabelWidth));
            UnityEngine.GUILayout.Space(flGapWidth);

            kcKey = Controls.KeybindButton("", kcKey, flKeybindWidth);

            UnityEngine.GUILayout.EndHorizontal();
        }

        #region Tabs Rendering

        public static void RenderTabs()
        {
            string[] strTabs = NS_Core.Utils.Lang.InEN() ? GUIManager.strTabsEN : GUIManager.strTabsRU;

            float flTabWidth = 225f;
            float flTabSpacing = 5f;
            float flTabsWidth = flTabWidth * (float)strTabs.Length + flTabSpacing * (float)(strTabs.Length - 1);
            float flOffset = (GUIManager.WindowRect.width - flTabsWidth) / 2f - 15f;

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Space(flOffset);

            UnityEngine.Vector2 vec2MousePos = UnityEngine.Event.current.mousePosition;

            GUIManager.iHoveredTab = -1;

            for (int i = 0; i < strTabs.Length; i++)
            {
                UnityEngine.Rect rectTab = UnityEngine.GUILayoutUtility.GetRect(flTabWidth, 40f);

                if (rectTab.Contains(vec2MousePos))
                    GUIManager.iHoveredTab = i;

                UnityEngine.GUIStyle gsTab = (GUIManager.iSelectedTab == i) ? Styles.GS.ActiveTabStyle : Styles.GS.TabStyle;

                if (!IsBackgroundInteractionBlocked() && UnityEngine.GUI.Button(rectTab, "", gsTab))
                    GUIManager.iSelectedTab = i;

                if (GUIManager.iSelectedTab == i)
                    GUI.TabGlow(rectTab);

                GUI.TabText(rectTab, strTabs[i], GUIManager.iSelectedTab == i);

                if (i < strTabs.Length - 1)
                    UnityEngine.GUILayout.Space(flTabSpacing);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndHorizontal();
        }

        #endregion

        #region Tab Content

        public static void CharacterTab()
        {
            UnityEngine.GUILayout.BeginHorizontal();

            UnityEngine.GUILayout.BeginVertical(UnityEngine.GUILayout.Width(340f));
            {
                bool bParamsOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Parameters", "Параметры"), "params");

                if (bParamsOpen)
                {
                    NS_Core.Vars.sTab.sMain.flGravityMultiplier = Controls.Slider(NS_Core.Utils.Lang.GetStr("Gravity", "Гравитация"), NS_Core.Vars.sTab.sMain.flGravityMultiplier, 0f, 1f);

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMain.flVelocityMultiplier = Controls.Slider(NS_Core.Utils.Lang.GetStr("Velocity", "Скорость"), NS_Core.Vars.sTab.sMain.flVelocityMultiplier, 0f, 1f);
                }

                Core.EndCollapsibleSection(bParamsOpen);
                GUI.Space(10f);

                bool bOtherOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Other", "Прочее"), "other");

                if (bOtherOpen)
                {
                    NS_Core.Vars.sTab.sMain.bEnableGodMode = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Immortality", "Бессмертие"), NS_Core.Vars.sTab.sMain.bEnableGodMode);

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMain.bEnableBhop = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Bhop", "Бхоп"), NS_Core.Vars.sTab.sMain.bEnableBhop);

                    GUI.Space(8f);

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Delete course time", "Удалить время курса"), 172f, 28f))
                        NS_Core.Utils.ResetCurrentCourseRecord();
                }

                Core.EndCollapsibleSection(bOtherOpen);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndVertical();

            GUI.Space(10f);

            UnityEngine.GUILayout.BeginVertical();
            {
                bool bResourcesOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Resources", "Ресурсы"), "resources");

                if (bResourcesOpen)
                {
                    NS_Core.Vars.sTab.sMain.bMultiplied100X_Currency = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Get currency from product cost (x100)", "Получать валюту от стоимос. товаров (x100)"),
                        NS_Core.Vars.sTab.sMain.bMultiplied100X_Currency);

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMain.bEnable_ExperienceTo60LVL = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Get level 60 from any control. p.", "Получить 60-й уровень от любой контрол. т."),
                        NS_Core.Vars.sTab.sMain.bEnable_ExperienceTo60LVL);

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMain.bGetAllCards = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Get all the cards", "Получить все карты"), NS_Core.Vars.sTab.sMain.bGetAllCards);
                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMain.bUnlock_WorldsAndToys = Controls.Checkbox(NS_Core.Utils.Lang.GetStr("Unlock worlds, achievements, toys (402+ ★)", "Разбл. ачивки, миры, игрушки (402+ звезд)"),
                        NS_Core.Vars.sTab.sMain.bUnlock_WorldsAndToys);
                }

                Core.EndCollapsibleSection(bResourcesOpen);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndVertical();

            UnityEngine.GUILayout.EndHorizontal();
        }

        public static void MRTab()
        {
            UnityEngine.GUILayout.BeginVertical();
            {
                bool bMainOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Main", "Главное"), "mr_main");

                if (bMainOpen)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.BeginHorizontal();
                            RenderBind(NS_Core.Utils.Lang.GetStr("Record", "Запись"), ref NS_Core.Vars.sTab.sMR.kcRecordKey, ref NS_Core.Vars.sTab.sMR.bRecordKeyToggle, 140f, 92f, 92f);
                            GUILayout.EndHorizontal();

                            GUI.Space(8f);

                            GUILayout.BeginHorizontal();
                            RenderSimpleBind(NS_Core.Utils.Lang.GetStr("Rewind/Apply", "Перемотка/Применить"), ref NS_Core.Vars.sTab.sMR.kcRewindKey, 140f, 92f);
                            GUI.Space(-116f);
                            RenderSimpleBind("", ref NS_Core.Vars.sTab.sMR.kcApplyFramesKey, 74f, 92f);
                            GUILayout.EndHorizontal();

                            GUI.Space(8f);

                            GUILayout.BeginHorizontal();
                            RenderSimpleBind(NS_Core.Utils.Lang.GetStr("Speed +/-", "Скорость +/-"), ref NS_Core.Vars.sTab.sMR.kcRewindSpeedUpKey, 140f, 92f);
                            GUI.Space(-119.5f);
                            RenderSimpleBind("", ref NS_Core.Vars.sTab.sMR.kcRewindSpeedDownKey, 78f, 92f);
                            GUILayout.EndHorizontal();

                            GUI.Space(8f);

                            GUILayout.BeginHorizontal();
                            RenderSimpleBind(NS_Core.Utils.Lang.GetStr("Back/Forward", "Назад/Вперед"), ref NS_Core.Vars.sTab.sMR.kcRewindBackwardKey, 140f, 92f);
                            GUI.Space(-108f);
                            RenderSimpleBind("", ref NS_Core.Vars.sTab.sMR.kcRewindForwardKey, 66f, 92f);
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();

                        GUI.Space(3.5f);

                        GUILayout.BeginVertical();
                        {
                            GUILayout.BeginHorizontal();
                            RenderBind("Playback", ref NS_Core.Vars.sTab.sMR.kcPlaybackKey, ref NS_Core.Vars.sTab.sMR.bPlaybackKeyToggle, 72f, 92f, 92f);
                            GUILayout.EndHorizontal();

                            GUI.Space(8f);

                            GUILayout.BeginHorizontal();
                            RenderSimpleBind("New record", ref NS_Core.Vars.sTab.sMR.kcInitNewRecordKey, 72f, 92f);
                            GUILayout.EndHorizontal();

                            GUI.Space(6f);

                            GUILayout.BeginHorizontal();
                            if (Controls.Button(NS_Core.Utils.Lang.GetStr("Save", "Сохранить"), 80f, 28f))
                                OpenMRSaveDialog();

                            GUI.Space(6f);

                            if (Controls.Button(NS_Core.Utils.Lang.GetStr("Load", "Загрузить"), 80f, 28f))
                                OpenMRLoadDialog();
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndHorizontal();

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMR.iRecordDelayFrames = Mathf.RoundToInt(Controls.Slider(NS_Core.Utils.Lang.GetStr("Record delay", "Задержка записи"), NS_Core.Vars.sTab.sMR.iRecordDelayFrames, 0f, 120f, 145f, 150f));

                    GUI.Space(8f);

                    float flDisplaySlowmotion = Mathf.Clamp(NS_Core.Vars.sTab.sMR.flRecordSlowmotion, 0f, 0.70f);
                    flDisplaySlowmotion = Controls.Slider(NS_Core.Utils.Lang.GetStr("Record slowmo", "Замедление записи"), flDisplaySlowmotion, 0f, 0.70f, 145f, 150f);

                    NS_Core.Vars.sTab.sMR.flRecordSlowmotion = flDisplaySlowmotion;

                    GUI.Space(8f);

                    NS_Core.Vars.sTab.sMR.bTeleportPlaybackToStart = Controls.Checkbox(
                        NS_Core.Utils.Lang.GetStr("Teleport playback to start", "Телепортировать playback в старт"),
                        NS_Core.Vars.sTab.sMR.bTeleportPlaybackToStart
                    );
                }

                Core.EndCollapsibleSection(bMainOpen);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndVertical();
        }

        public static void OtherTab()
        {
            UnityEngine.GUILayout.BeginHorizontal();

            UnityEngine.GUILayout.BeginVertical(UnityEngine.GUILayout.Width(340f));
            {
                bool bSettingsOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Settings", "Настройки"), "settings");

                if (bSettingsOpen)
                {
                    string[] arrLanguages = { "English", "Русский" };

                    NS_Core.Vars.sTab.sOther.iLanguage = Controls.Dropdown(NS_Core.Utils.Lang.GetStr("Language", "Язык"), NS_Core.Vars.sTab.sOther.iLanguage, arrLanguages);

                    GUI.Space(8f);

                    int iNewTheme = Controls.Dropdown(NS_Core.Utils.Lang.InEN() ? "Theme" : "Тема", NS_Core.Vars.sTab.sOther.iTheme, NS_Core.Utils.Lang.InEN() ? NS_Visuals.Themes.ThemeNamesEN : NS_Visuals.Themes.ThemeNamesRU);

                    if (iNewTheme != NS_Core.Vars.sTab.sOther.iTheme)
                    {
                        NS_Core.Vars.sTab.sOther.iTheme = iNewTheme;
                        NS_Visuals.Themes.ApplyTheme(iNewTheme);
                    }
                }

                Core.EndCollapsibleSection(bSettingsOpen);
                GUI.Space(10f);

                bool bInfoOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Information", "Информация"), "info");

                if (bInfoOpen)
                {
                    UnityEngine.GUILayout.Label($"{NS_Core.Utils.Lang.GetStr("Version game", "Версия игры")}: {NS_Core.Main.strGameVersion} ({NS_Core.Utils.Lang.GetStr("recommended", "рекомендуется")} 612636 или 477245)", Styles.GS.LabelStyle);
                    GUI.Space(4f);

                    UnityEngine.GUILayout.Label($"{NS_Core.Utils.Lang.GetStr("Version cheat", "Версия чита")}: {NS_Core.Main.strCheatVersion}", Styles.GS.LabelStyle);

                    GUILayout.BeginHorizontal();

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Social links", "Социальные ссылки")))
                    {
                        NS_Core.Utils.OpenUrl("https://guns.lol/hpp_forever");
                    }
                    GUI.Space(6f);

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Repository", "Репозиторий")))
                    {
                        NS_Core.Utils.OpenUrl("https://github.com/HppForever2/MonoHack-Hot-Lava");
                    }

                    GUILayout.EndHorizontal();
                }

                Core.EndCollapsibleSection(bInfoOpen);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndVertical();

            GUI.Space(10f);

            UnityEngine.GUILayout.BeginVertical();
            {
                bool bControlOpen = Core.BeginCollapsibleSection(NS_Core.Utils.Lang.GetStr("Config", "Конфиг"), "config");

                if (bControlOpen)
                {
                    GUILayout.BeginHorizontal();

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Save", "Сохранить")))
                    {
                        NS_Core.Config.Save();
                    }
                    GUI.Space(6f);

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Load", "Загрузить")))
                    {
                        NS_Core.Config.Load();
                    }
                    GUI.Space(6f);

                    GUILayout.EndHorizontal();

                    GUI.Space(6f);

                    GUILayout.BeginHorizontal();

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Open folder", "Открыть папку")))
                    {
                        NS_Core.Config.OpenFolder();
                    }
                    GUI.Space(6f);

                    if (Controls.Button(NS_Core.Utils.Lang.GetStr("Open file", "Открыть файл")))
                    {
                        NS_Core.Config.OpenFile();
                    }

                    GUILayout.EndHorizontal();
                }

                Core.EndCollapsibleSection(bControlOpen);
            }

            UnityEngine.GUILayout.FlexibleSpace();
            UnityEngine.GUILayout.EndVertical();

            UnityEngine.GUILayout.EndHorizontal();
        }

        public static void RenderModal()
        {
            if (eCurrentMRDialogMode == eMRDialogMode.None)
                return;

            bRenderingMRModal = true;

            if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown)
            {
                if (UnityEngine.Event.current.keyCode == UnityEngine.KeyCode.Escape)
                {
                    CloseMRDialog();
                    UnityEngine.Event.current.Use();

                    bRenderingMRModal = false;

                    return;
                }

                if (eCurrentMRDialogMode == eMRDialogMode.Save && (UnityEngine.Event.current.keyCode == UnityEngine.KeyCode.Return || UnityEngine.Event.current.keyCode == UnityEngine.KeyCode.KeypadEnter))
                {
                    CommitMRSave();
                    UnityEngine.Event.current.Use();

                    bRenderingMRModal = false;

                    return;
                }
            }

            Themes.ThemeColors theme = Themes.CurrentTheme;
            UnityEngine.Rect rectOverlay = new UnityEngine.Rect(0f, 0f, GUIManager.WindowRect.width, GUIManager.WindowRect.height);

            UnityEngine.GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.52f);
            UnityEngine.GUI.DrawTexture(rectOverlay, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            float flModalWidth = 360f;
            float flModalHeight = eCurrentMRDialogMode == eMRDialogMode.Save ? 182f : 286f;

            UnityEngine.Rect rectModal = new UnityEngine.Rect(
                (GUIManager.WindowRect.width - flModalWidth) * 0.5f,
                (GUIManager.WindowRect.height - flModalHeight) * 0.5f,
                flModalWidth,
                flModalHeight
            );

            UnityEngine.GUI.color = theme.BoxBg;
            UnityEngine.GUI.DrawTexture(rectModal, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Helpers.RenderRectBorder(rectModal, theme.DropdownBorder, 1f);

            UnityEngine.GUIStyle gsTitle = new UnityEngine.GUIStyle(Styles.GS.LabelStyle);

            gsTitle.fontSize = 15;
            gsTitle.fontStyle = UnityEngine.FontStyle.Bold;
            gsTitle.alignment = UnityEngine.TextAnchor.UpperLeft;
            gsTitle.normal.textColor = theme.TextPrimary;

            UnityEngine.GUIStyle gsText = new UnityEngine.GUIStyle(Styles.GS.LabelStyle);

            gsText.wordWrap = true;
            gsText.normal.textColor = theme.TextSecondary;

            UnityEngine.Rect rectContent = new UnityEngine.Rect(rectModal.x + 16f, rectModal.y + 14f, rectModal.width - 32f, rectModal.height - 28f);

            if (eCurrentMRDialogMode == eMRDialogMode.Save)
                RenderMRSaveDialog(rectContent, gsTitle, gsText, theme);

            else if (eCurrentMRDialogMode == eMRDialogMode.Load)
                RenderMRLoadDialog(rectContent, gsTitle, gsText, theme);

            bRenderingMRModal = false;
        }

        #endregion

        private static void OpenMRSaveDialog()
        {
            strMRSaveName = NS_Core.Movement.Record.GetSuggestedSaveName();
            eCurrentMRDialogMode = eMRDialogMode.Save;
            vecMRLoadScroll = UnityEngine.Vector2.zero;
            bMRLoadScrollbarDragging = false;
            flMRLoadScrollbarDragOffset = 0f;
            bFocusMRSaveTextField = true;

            Controls.CloseAllDropdowns();
        }

        private static void OpenMRLoadDialog()
        {
            eCurrentMRDialogMode = eMRDialogMode.Load;
            vecMRLoadScroll = UnityEngine.Vector2.zero;
            bMRLoadScrollbarDragging = false;
            flMRLoadScrollbarDragOffset = 0f;

            Controls.CloseAllDropdowns();
        }

        private static void CloseMRDialog()
        {
            eCurrentMRDialogMode = eMRDialogMode.None;
            strMRSaveName = "record";
            vecMRLoadScroll = UnityEngine.Vector2.zero;
            bMRLoadScrollbarDragging = false;
            flMRLoadScrollbarDragOffset = 0f;
            bFocusMRSaveTextField = false;
        }

        private static void CommitMRSave()
        {
            NS_Core.Movement.Record.SaveRecord(strMRSaveName);
            CloseMRDialog();
        }

        private static void RenderMRSaveDialog(UnityEngine.Rect rectContent, UnityEngine.GUIStyle gsTitle, UnityEngine.GUIStyle gsText, Themes.ThemeColors theme)
        {
            UnityEngine.GUI.Label(new UnityEngine.Rect(rectContent.x, rectContent.y, rectContent.width, 22f), NS_Core.Utils.Lang.GetStr("Save movement record", "Сохранить movement record"), gsTitle);
            UnityEngine.GUI.Label(new UnityEngine.Rect(rectContent.x, rectContent.y + 28f, rectContent.width, 34f), NS_Core.Utils.Lang.GetStr("Enter save name. File will be created for the current world and course.", "Введи имя сохранения. Файл создастся для текущего мира и курса."), gsText);

            UnityEngine.Rect rectField = new UnityEngine.Rect(rectContent.x, rectContent.y + 72f, rectContent.width, 32f);

            UnityEngine.GUI.color = theme.DropdownBg;
            UnityEngine.GUI.DrawTexture(rectField, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Helpers.RenderRectBorder(rectField, theme.DropdownBorder, 1f);

            UnityEngine.GUIStyle gsField = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.textField);

            gsField.normal.background = null;
            gsField.hover.background = null;
            gsField.focused.background = null;
            gsField.active.background = null;
            gsField.fontSize = 13;
            gsField.alignment = UnityEngine.TextAnchor.MiddleLeft;
            gsField.padding = new UnityEngine.RectOffset(8, 8, 7, 7);
            gsField.normal.textColor = theme.TextPrimary;
            gsField.focused.textColor = theme.TextPrimary;
            gsField.hover.textColor = theme.TextPrimary;
            gsField.active.textColor = theme.TextPrimary;

            UnityEngine.GUI.SetNextControlName(strMRSaveTextFieldControl);
            strMRSaveName = UnityEngine.GUI.TextField(rectField, strMRSaveName ?? string.Empty, 64, gsField);

            if (bFocusMRSaveTextField && UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
            {
                UnityEngine.GUI.FocusControl(strMRSaveTextFieldControl);
                bFocusMRSaveTextField = false;
            }

            UnityEngine.GUILayout.BeginArea(new UnityEngine.Rect(rectContent.x, rectContent.y + 122f, rectContent.width, 32f));
            UnityEngine.GUILayout.BeginHorizontal();

            if (Controls.Button(NS_Core.Utils.Lang.GetStr("Save", "Сохранить"), 150f, 28f))
                CommitMRSave();

            UnityEngine.GUILayout.Space(8f);

            if (Controls.Button(NS_Core.Utils.Lang.GetStr("Cancel", "Отмена"), 150f, 28f))
                CloseMRDialog();

            UnityEngine.GUILayout.EndHorizontal();
            UnityEngine.GUILayout.EndArea();
        }

        private static void RenderMRLoadDialog(UnityEngine.Rect rectContent, UnityEngine.GUIStyle gsTitle, UnityEngine.GUIStyle gsText, Themes.ThemeColors theme)
        {
            UnityEngine.GUI.Label(new UnityEngine.Rect(rectContent.x, rectContent.y, rectContent.width, 22f), NS_Core.Utils.Lang.GetStr("Load movement record", "Загрузить movement record"), gsTitle);
            UnityEngine.GUI.Label(new UnityEngine.Rect(rectContent.x, rectContent.y + 28f, rectContent.width, 34f), NS_Core.Utils.Lang.GetStr("Only saves for the current world and course are shown here.", "Тут показываются только сохранения для текущего мира и курса."), gsText);

            string[] arrFiles = NS_Core.Movement.Record.GetAvailableRecordFiles();

            UnityEngine.Rect rectList = new UnityEngine.Rect(rectContent.x, rectContent.y + 72f, rectContent.width, 150f);
            UnityEngine.GUI.color = theme.DropdownBg;
            UnityEngine.GUI.DrawTexture(rectList, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Helpers.RenderRectBorder(rectList, theme.DropdownBorder, 1f);
            RenderMRLoadList(rectList, arrFiles, gsText, theme);

            UnityEngine.GUILayout.BeginArea(new UnityEngine.Rect(rectContent.x, rectContent.y + 232f, rectContent.width, 32f));
            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.FlexibleSpace();

            if (Controls.Button(NS_Core.Utils.Lang.GetStr("Cancel", "Отмена"), 150f, 28f))
                CloseMRDialog();

            UnityEngine.GUILayout.EndHorizontal();
            UnityEngine.GUILayout.EndArea();
        }

        private static void RenderMRLoadList(UnityEngine.Rect rectList, string[] arrFiles, UnityEngine.GUIStyle gsText, Themes.ThemeColors theme)
        {
            UnityEngine.Event evt = UnityEngine.Event.current;
            UnityEngine.Rect rectViewport = new UnityEngine.Rect(rectList.x + 6f, rectList.y + 6f, rectList.width - 12f, rectList.height - 12f);

            if (arrFiles.Length <= 0)
            {
                UnityEngine.GUI.Label(rectViewport, NS_Core.Utils.Lang.GetStr("No saves for this course", "Для этого курса нет сохранений"), gsText);
                bMRLoadScrollbarDragging = false;
                vecMRLoadScroll = UnityEngine.Vector2.zero;

                return;
            }

            const float flRowHeight = 26f;
            const float flRowSpacing = 4f;
            const float flScrollbarWidth = 12f;
            float flContentHeight = arrFiles.Length * flRowHeight + UnityEngine.Mathf.Max(0f, (arrFiles.Length - 1) * flRowSpacing);
            bool bNeedsScrollbar = flContentHeight > rectViewport.height + 0.5f;
            float flItemsWidth = bNeedsScrollbar ? rectViewport.width - flScrollbarWidth - 4f : rectViewport.width;
            float flMaxScroll = UnityEngine.Mathf.Max(0f, flContentHeight - rectViewport.height);

            vecMRLoadScroll.y = UnityEngine.Mathf.Clamp(vecMRLoadScroll.y, 0f, flMaxScroll);

            if (!bNeedsScrollbar)
                bMRLoadScrollbarDragging = false;

            if (bMRLoadScrollbarDragging && (evt.rawType == UnityEngine.EventType.MouseUp || evt.type == UnityEngine.EventType.MouseUp))
                bMRLoadScrollbarDragging = false;

            if (rectList.Contains(evt.mousePosition) && evt.type == UnityEngine.EventType.ScrollWheel && bNeedsScrollbar)
            {
                vecMRLoadScroll.y = UnityEngine.Mathf.Clamp(vecMRLoadScroll.y + evt.delta.y * 22f, 0f, flMaxScroll);
                evt.Use();
            }

            UnityEngine.Rect rectItemsArea = new UnityEngine.Rect(rectViewport.x, rectViewport.y, flItemsWidth, rectViewport.height);
            UnityEngine.Rect rectScrollbarArea = new UnityEngine.Rect(rectItemsArea.xMax + 4f, rectViewport.y, flScrollbarWidth, rectViewport.height);

            float flVisibleRatio = rectViewport.height / flContentHeight;
            float flThumbHeight = bNeedsScrollbar ? UnityEngine.Mathf.Max(rectScrollbarArea.height * flVisibleRatio, 20f) : rectScrollbarArea.height;
            float flScrollRatio = flMaxScroll > 0f ? vecMRLoadScroll.y / flMaxScroll : 0f;
            float flThumbY = rectScrollbarArea.y + (rectScrollbarArea.height - flThumbHeight) * flScrollRatio;

            UnityEngine.Rect rectThumb = new UnityEngine.Rect(rectScrollbarArea.x + 2f, flThumbY, rectScrollbarArea.width - 4f, flThumbHeight);

            if (bNeedsScrollbar && bMRLoadScrollbarDragging && (evt.type == UnityEngine.EventType.MouseDrag || evt.rawType == UnityEngine.EventType.MouseDrag || evt.type == UnityEngine.EventType.MouseMove))
            {
                float flThumbTop = UnityEngine.Mathf.Clamp(evt.mousePosition.y - flMRLoadScrollbarDragOffset, rectScrollbarArea.y, rectScrollbarArea.y + rectScrollbarArea.height - flThumbHeight);
                float flThumbRatio = rectScrollbarArea.height - flThumbHeight > 0f ? (flThumbTop - rectScrollbarArea.y) / (rectScrollbarArea.height - flThumbHeight) : 0f;

                vecMRLoadScroll.y = flThumbRatio * flMaxScroll;

                evt.Use();
            }

            else if (bNeedsScrollbar && evt.type == UnityEngine.EventType.MouseDown && evt.button == 0 && rectScrollbarArea.Contains(evt.mousePosition))
            {
                if (rectThumb.Contains(evt.mousePosition))
                {
                    bMRLoadScrollbarDragging = true;
                    flMRLoadScrollbarDragOffset = evt.mousePosition.y - rectThumb.y;
                }

                else
                {
                    float flTargetThumbTop = UnityEngine.Mathf.Clamp(evt.mousePosition.y - rectScrollbarArea.y - flThumbHeight * 0.5f, 0f, rectScrollbarArea.height - flThumbHeight);
                    float flThumbRatio = rectScrollbarArea.height - flThumbHeight > 0f ? flTargetThumbTop / (rectScrollbarArea.height - flThumbHeight) : 0f;
                    vecMRLoadScroll.y = flThumbRatio * flMaxScroll;
                    bMRLoadScrollbarDragging = true;
                    flMRLoadScrollbarDragOffset = flThumbHeight * 0.5f;
                }

                evt.Use();
            }

            string strSelectedFile = null;
            UnityEngine.Vector2 vecMousePos = evt.mousePosition;

            UnityEngine.GUI.BeginGroup(rectItemsArea);

            for (int i = 0; i < arrFiles.Length; i++)
            {
                float flItemY = i * (flRowHeight + flRowSpacing) - vecMRLoadScroll.y;

                if (flItemY + flRowHeight < 0f || flItemY > rectItemsArea.height)
                    continue;

                UnityEngine.Rect rectItemLocal = new UnityEngine.Rect(0f, flItemY, rectItemsArea.width, flRowHeight);
                UnityEngine.Rect rectItemGlobal = new UnityEngine.Rect(rectItemsArea.x, rectItemsArea.y + flItemY, rectItemsArea.width, flRowHeight);

                bool bHovered = rectItemGlobal.Contains(vecMousePos) && rectItemsArea.Contains(vecMousePos);
                bool bPressed = bHovered && UnityEngine.Input.GetMouseButton(0);

                RenderMRLoadRow(rectItemLocal, System.IO.Path.GetFileName(arrFiles[i]), theme, bHovered, bPressed);

                if (evt.type == UnityEngine.EventType.MouseDown && evt.button == 0 && bHovered)
                    strSelectedFile = arrFiles[i];
            }

            UnityEngine.GUI.EndGroup();

            if (!string.IsNullOrWhiteSpace(strSelectedFile))
            {
                evt.Use();

                if (NS_Core.Movement.Record.LoadRecord(strSelectedFile))
                    CloseMRDialog();

                return;
            }

            if (!bNeedsScrollbar)
                return;

            UnityEngine.GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.28f);
            UnityEngine.GUI.DrawTexture(rectScrollbarArea, UnityEngine.Texture2D.whiteTexture);

            bool bThumbHover = rectThumb.Contains(vecMousePos);

            UnityEngine.Color colThumb = bMRLoadScrollbarDragging || bThumbHover
                ? theme.DropdownAccent
                : new UnityEngine.Color(theme.DropdownAccent.r, theme.DropdownAccent.g, theme.DropdownAccent.b, theme.DropdownAccent.a * 0.65f);

            UnityEngine.GUI.color = colThumb;
            UnityEngine.GUI.DrawTexture(rectThumb, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        private static void RenderMRLoadRow(UnityEngine.Rect rect, string strText, Themes.ThemeColors theme, bool bHovered, bool bPressed)
        {
            UnityEngine.Color colBg = bPressed
                ? theme.ButtonActive
                : UnityEngine.Color.Lerp(theme.ButtonBg, theme.ButtonHover, bHovered ? 1f : 0f);

            UnityEngine.Color colBorder = theme.DropdownBorder;
            colBorder.a = bHovered ? 0.72f : 0.46f;

            UnityEngine.GUI.color = colBg;
            UnityEngine.GUI.DrawTexture(rect, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Helpers.RenderRectBorder(rect, colBorder, 1f);

            UnityEngine.GUIStyle gsRow = new UnityEngine.GUIStyle(Styles.GS.LabelStyle);

            gsRow.alignment = UnityEngine.TextAnchor.MiddleLeft;
            gsRow.fontSize = 12;
            gsRow.fontStyle = UnityEngine.FontStyle.Normal;
            gsRow.clipping = UnityEngine.TextClipping.Clip;
            gsRow.padding = new UnityEngine.RectOffset(10, 8, 0, 0);
            gsRow.normal.textColor = UnityEngine.Color.Lerp(theme.TextSecondary, theme.ButtonText, bHovered ? 0.9f : 0.35f);

            UnityEngine.GUI.Label(rect, strText, gsRow);
        }
    }
}