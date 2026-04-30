namespace HotLava_Cheat.Source.Visuals.Render
{
    public static class Core
    {
        #region Section State Dictionaries

        internal static System.Collections.Generic.Dictionary<string, bool> s_dictSectionStates = new System.Collections.Generic.Dictionary<string, bool>();
        internal static System.Collections.Generic.Dictionary<string, float> s_dictSectionHoverAnim = new System.Collections.Generic.Dictionary<string, float>();
        internal static System.Collections.Generic.Dictionary<string, float> s_dictSectionClickAnim = new System.Collections.Generic.Dictionary<string, float>();

        #endregion

        #region Section Methods

        public static void BeginSection(string sTitle, string sIcon = "▸")
        {
            UnityEngine.GUILayout.BeginVertical(Styles.GS.SectionStyle);
            UnityEngine.GUILayout.Label(sIcon + "  " + sTitle, Styles.GS.SectionHeaderStyle);
            UnityEngine.GUILayout.BeginVertical(Styles.GS.SectionContentStyle);
        }

        public static void EndSection()
        {
            UnityEngine.GUILayout.EndVertical();
            UnityEngine.GUILayout.EndVertical();
        }

        public static bool BeginCollapsibleSection(string sTitle, string sKey = null, bool bDefaultOpen = true)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            bool bInputBlocked = Tabs.IsBackgroundInteractionBlocked();

            string sActualKey = string.IsNullOrEmpty(sKey) ? sTitle : sKey;

            if (!s_dictSectionStates.ContainsKey(sActualKey))
                s_dictSectionStates[sActualKey] = bDefaultOpen;

            if (!s_dictSectionHoverAnim.ContainsKey(sActualKey))
                s_dictSectionHoverAnim[sActualKey] = 0f;

            if (!s_dictSectionClickAnim.ContainsKey(sActualKey))
                s_dictSectionClickAnim[sActualKey] = 0f;

            bool bIsOpen = s_dictSectionStates[sActualKey];
            string sArrow = bIsOpen ? "▼" : "▶";
            string sFullTitle = sArrow + "  " + sTitle;

            UnityEngine.GUILayout.BeginVertical(Styles.GS.SectionStyle);

            UnityEngine.Rect rectHeader = UnityEngine.GUILayoutUtility.GetRect(new UnityEngine.GUIContent(sFullTitle), Styles.GS.SectionHeaderStyle, UnityEngine.GUILayout.ExpandWidth(true), UnityEngine.GUILayout.Height(32f));

            bool bMouseInsideDropdown = IsPointInsideDropdown(UnityEngine.Event.current.mousePosition);
            bool bHover = !bInputBlocked && !bMouseInsideDropdown && rectHeader.Contains(UnityEngine.Event.current.mousePosition);

            float flTargetHover = bHover ? 1f : 0f;

            s_dictSectionHoverAnim[sActualKey] = UnityEngine.Mathf.Lerp(s_dictSectionHoverAnim[sActualKey], flTargetHover, UnityEngine.Time.deltaTime * 12f);
            float flHoverAnim = s_dictSectionHoverAnim[sActualKey];

            s_dictSectionClickAnim[sActualKey] = UnityEngine.Mathf.Lerp(s_dictSectionClickAnim[sActualKey], 0f, UnityEngine.Time.deltaTime * 8f);
            float flClickAnim = s_dictSectionClickAnim[sActualKey];

            UnityEngine.GUI.DrawTexture(rectHeader, Textures.T2D.SectionHeaderTexture, UnityEngine.ScaleMode.StretchToFill);

            if (flHoverAnim > 0.01f)
            {
                UnityEngine.Color colHoverOverlay = theme.GlowPrimary;
                colHoverOverlay.a = 0.15f * flHoverAnim;

                Helpers.RenderHoverGradient(rectHeader, colHoverOverlay);
            }

            if (flClickAnim > 0.01f)
            {
                UnityEngine.Color colClick = theme.GlowSecondary;

                colClick.a = 0.4f * flClickAnim;

                UnityEngine.GUI.color = colClick;
                UnityEngine.GUI.DrawTexture(rectHeader, Textures.T2D.SectionHeaderTexture, UnityEngine.ScaleMode.StretchToFill);
                UnityEngine.GUI.color = UnityEngine.Color.white;
            }

            float flAccentWidth = 4f + flHoverAnim * 2f;

            UnityEngine.Color colAccent = UnityEngine.Color.Lerp(theme.SectionAccent, theme.GlowSecondary, flHoverAnim);

            colAccent.a = 0.8f + flHoverAnim * 0.2f;

            UnityEngine.Rect rectAccent = new UnityEngine.Rect(rectHeader.x, rectHeader.y, flAccentWidth, rectHeader.height);

            Helpers.RenderAccentBar(rectAccent, colAccent, flHoverAnim);

            if (flHoverAnim > 0.01f)
                Helpers.RenderHeaderGlow(rectHeader, flHoverAnim);

            float flLineAlpha = 0.3f + flHoverAnim * 0.4f;

            UnityEngine.Color colLine = theme.SectionHeaderLine;
            colLine.a = flLineAlpha;

            UnityEngine.Rect rectLine = new UnityEngine.Rect(rectHeader.x, rectHeader.y + rectHeader.height - 1f, rectHeader.width, 1f);

            UnityEngine.GUI.color = colLine;
            UnityEngine.GUI.DrawTexture(rectLine, UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Helpers.RenderSectionTitle(rectHeader, sFullTitle, flHoverAnim, bIsOpen);
            Helpers.RenderAnimatedArrow(rectHeader, bIsOpen, flHoverAnim);

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectHeader.Contains(UnityEngine.Event.current.mousePosition) && !bMouseInsideDropdown)
            {
                s_dictSectionStates[sActualKey] = !s_dictSectionStates[sActualKey];
                s_dictSectionClickAnim[sActualKey] = 1f;

                UnityEngine.Event.current.Use();
            }

            if (s_dictSectionStates[sActualKey])
            {
                UnityEngine.GUILayout.BeginVertical(Styles.GS.SectionContentStyle);
                return true;
            }

            return false;
        }

        public static void EndCollapsibleSection(bool bWasOpen)
        {
            if (bWasOpen)
                UnityEngine.GUILayout.EndVertical();

            UnityEngine.GUILayout.EndVertical();
        }

        #endregion

        #region Dropdown Helper

        private static bool IsPointInsideDropdown(UnityEngine.Vector2 Point)
        {
            return Controls.IsPointInsideDropdown(Point);
        }

        #endregion
    }
}