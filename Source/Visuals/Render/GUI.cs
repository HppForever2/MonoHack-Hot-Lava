namespace HotLava_Cheat.Source.Visuals.Render
{
    public static class GUI
    {
        #region UI Elements

        public static void TabGlow(UnityEngine.Rect rectTab)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;
            UnityEngine.Color colGlow = theme.GlowPrimary;

            colGlow.a = 0.3f;

            for (int i = 1; i <= 3; i++)
            {
                UnityEngine.Rect rectGlow = new UnityEngine.Rect(rectTab.x - (float)i, rectTab.y - (float)i, rectTab.width + (float)(i * 2), rectTab.height + (float)(i * 2));

                UnityEngine.GUI.color = new UnityEngine.Color(colGlow.r, colGlow.g, colGlow.b, colGlow.a / (float)i);
                UnityEngine.GUI.DrawTexture(rectGlow, Textures.T2D.TabBorderTexture);
                UnityEngine.GUI.color = UnityEngine.Color.white;
            }
        }

        public static void TabText(UnityEngine.Rect rectTab, string strText, bool bIsActive)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.GUIStyle gsTabText = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);

            gsTabText.fontSize = 14;
            gsTabText.fontStyle = UnityEngine.FontStyle.Bold;
            gsTabText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            gsTabText.normal.textColor = new UnityEngine.Color(0f, 0f, 0f, 0.5f);

            UnityEngine.GUI.Label(new UnityEngine.Rect(rectTab.x + 1f, rectTab.y + 1f, rectTab.width, rectTab.height), strText, gsTabText);

            if (bIsActive)
                gsTabText.normal.textColor = theme.TabTextActive;

            else
                gsTabText.normal.textColor = (GUIManager.iHoveredTab == System.Array.IndexOf(NS_Core.Utils.Lang.InEN() ? GUIManager.strTabsEN : GUIManager.strTabsRU, strText))
                    ? theme.TabTextHover
                    : theme.TabText;

            UnityEngine.GUI.Label(rectTab, strText, gsTabText);
        }

        private static void GlowText(UnityEngine.Rect rect, string text, int fontSize)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.GUIStyle gsGlow = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);

            gsGlow.fontSize = fontSize;
            gsGlow.fontStyle = UnityEngine.FontStyle.Bold;
            gsGlow.alignment = UnityEngine.TextAnchor.MiddleCenter;

            UnityEngine.Rect textRect = new UnityEngine.Rect(rect.x, rect.y - 5f, rect.width, rect.height);
            UnityEngine.Color colGlow = theme.GlowPrimary;

            colGlow.a = 0.12f;
            gsGlow.normal.textColor = colGlow;

            for (int i = 4; i >= 1; i--)
            {
                UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x, textRect.y + i, textRect.width, textRect.height), text, gsGlow);
                UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x, textRect.y - i, textRect.width, textRect.height), text, gsGlow);
                UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x + i, textRect.y, textRect.width, textRect.height), text, gsGlow);
                UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x - i, textRect.y, textRect.width, textRect.height), text, gsGlow);
            }

            gsGlow.normal.textColor = new UnityEngine.Color(theme.WindowBg.r, theme.WindowBg.g, theme.WindowBg.b, 0.8f);
            UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x + 2f, textRect.y + 2f, textRect.width, textRect.height), text, gsGlow);

            gsGlow.normal.textColor = theme.LogoText;
            UnityEngine.GUI.Label(textRect, text, gsGlow);

            gsGlow.normal.textColor = new UnityEngine.Color(1f, 1f, 1f, 0.25f);
            UnityEngine.GUI.Label(new UnityEngine.Rect(textRect.x, textRect.y - 1f, textRect.width, textRect.height), text, gsGlow);
        }

        public static void Logo()
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.Rect rectLogo = UnityEngine.GUILayoutUtility.GetRect(GUIManager.WindowRect.width - 30f, 70f);

            UnityEngine.GUI.DrawTexture(rectLogo, Textures.T2D.LogoBgTexture);

            GlowText(rectLogo, "FURION", 32);

            UnityEngine.GUIStyle gsSubtitle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);

            gsSubtitle.fontSize = 11;
            gsSubtitle.fontStyle = UnityEngine.FontStyle.Normal;
            gsSubtitle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            gsSubtitle.normal.textColor = theme.LogoSubtitle;

            UnityEngine.GUI.Label(new UnityEngine.Rect(rectLogo.x, rectLogo.y + rectLogo.height - 22f, rectLogo.width, 18f), "Open Source Cheat for Hot Lava", gsSubtitle);
        }

        public static void Separator()
        {
            UnityEngine.GUILayout.Box("", Styles.GS.SeparatorStyle, UnityEngine.GUILayout.ExpandWidth(true));
        }

        public static void Space(float flSize = 8f)
        {
            UnityEngine.GUILayout.Space(flSize);
        }

        #endregion
    }
}