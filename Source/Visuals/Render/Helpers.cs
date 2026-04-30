namespace HotLava_Cheat.Source.Visuals.Render
{
    public static class Helpers
    {
        #region Drawing Helpers

        public static void RenderHoverGradient(UnityEngine.Rect rect, UnityEngine.Color col)
        {
            int iSteps = 8;
            float flStepWidth = rect.width / iSteps;

            for (int i = 0; i < iSteps; i++)
            {
                float flAlpha = col.a * (1f - (float)i / iSteps) * 0.7f;

                UnityEngine.Color colStep = new UnityEngine.Color(col.r, col.g, col.b, flAlpha);
                UnityEngine.Rect rectStep = new UnityEngine.Rect(rect.x + i * flStepWidth, rect.y, flStepWidth + 1f, rect.height);

                UnityEngine.GUI.color = colStep;
                UnityEngine.GUI.DrawTexture(rectStep, UnityEngine.Texture2D.whiteTexture);
            }

            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        public static void RenderAccentBar(UnityEngine.Rect rect, UnityEngine.Color col, float flHoverAnim)
        {
            UnityEngine.GUI.color = col;

            UnityEngine.GUI.DrawTexture(rect, UnityEngine.Texture2D.whiteTexture);

            if (flHoverAnim > 0.01f)
            {
                for (int i = 1; i <= 3; i++)
                {
                    float flGlowAlpha = col.a * 0.3f * flHoverAnim / i;

                    UnityEngine.Color colGlow = new UnityEngine.Color(col.r, col.g, col.b, flGlowAlpha);
                    UnityEngine.Rect rectGlow = new UnityEngine.Rect(rect.x + rect.width, rect.y, i * 4f, rect.height);

                    UnityEngine.GUI.color = colGlow;

                    UnityEngine.GUI.DrawTexture(rectGlow, UnityEngine.Texture2D.whiteTexture);
                }
            }

            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        public static void RenderHeaderGlow(UnityEngine.Rect rect, float flIntensity)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;
            UnityEngine.Color colGlow = theme.GlowPrimary;

            colGlow.a = 0.08f * flIntensity;

            for (int i = 1; i <= 2; i++)
            {
                UnityEngine.Rect rectGlow = new UnityEngine.Rect(rect.x - i, rect.y - i, rect.width + i * 2, rect.height + i * 2);
                UnityEngine.GUI.color = new UnityEngine.Color(colGlow.r, colGlow.g, colGlow.b, colGlow.a / i);

                UnityEngine.GUI.DrawTexture(rectGlow, UnityEngine.Texture2D.whiteTexture);
            }

            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        public static void RenderSectionTitle(UnityEngine.Rect rect, string sTitle, float flHoverAnim, bool bIsOpen)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            string sTextOnly = sTitle.Length > 3 ? sTitle.Substring(3) : sTitle;

            UnityEngine.GUIStyle gsText = new UnityEngine.GUIStyle(Styles.GS.SectionHeaderStyle);

            gsText.padding = new UnityEngine.RectOffset(28, 10, 0, 0);

            UnityEngine.Color colShadow = new UnityEngine.Color(0f, 0f, 0f, 0.5f);

            gsText.normal.textColor = colShadow;

            UnityEngine.GUI.Label(new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height), sTextOnly, gsText);

            UnityEngine.Color colNormal = theme.TextSecondary;
            UnityEngine.Color colHover = theme.TextPrimary;
            UnityEngine.Color colActive = UnityEngine.Color.Lerp(theme.TextPrimary, theme.GlowSecondary, 0.3f);
            UnityEngine.Color colFinal = UnityEngine.Color.Lerp(bIsOpen ? colActive : colNormal, colHover, flHoverAnim);

            gsText.normal.textColor = colFinal;

            UnityEngine.GUI.Label(rect, sTextOnly, gsText);

            if (flHoverAnim > 0.3f)
            {
                UnityEngine.Color colHighlight = new UnityEngine.Color(1f, 1f, 1f, 0.15f * (flHoverAnim - 0.3f) / 0.7f);

                gsText.normal.textColor = colHighlight;

                UnityEngine.GUI.Label(new UnityEngine.Rect(rect.x, rect.y - 0.5f, rect.width, rect.height), sTextOnly, gsText);
            }
        }

        public static void RenderAnimatedArrow(UnityEngine.Rect rect, bool bIsOpen, float flHoverAnim)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.GUIStyle gsArrow = new UnityEngine.GUIStyle();

            gsArrow.fontSize = 12;
            gsArrow.fontStyle = UnityEngine.FontStyle.Bold;
            gsArrow.alignment = UnityEngine.TextAnchor.MiddleLeft;
            gsArrow.padding = new UnityEngine.RectOffset(12, 0, 0, 0);

            string sArrow = bIsOpen ? "▼" : "▶";
            
            UnityEngine.Color colArrowNormal = theme.SectionAccent;
            UnityEngine.Color colArrowHover = theme.GlowSecondary;
            UnityEngine.Color colArrow = UnityEngine.Color.Lerp(colArrowNormal, colArrowHover, flHoverAnim);

            gsArrow.normal.textColor = new UnityEngine.Color(0f, 0f, 0f, 0.4f);
            UnityEngine.GUI.Label(new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height), sArrow, gsArrow);

            gsArrow.normal.textColor = colArrow;
            UnityEngine.GUI.Label(rect, sArrow, gsArrow);
        }

        public static void RenderRectBorder(UnityEngine.Rect rect, UnityEngine.Color colBorder, float flWidth)
        {
            UnityEngine.GUI.color = colBorder;

            UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(rect.x, rect.y, rect.width, flWidth), UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(rect.x, rect.y + rect.height - flWidth, rect.width, flWidth), UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(rect.x, rect.y, flWidth, rect.height), UnityEngine.Texture2D.whiteTexture);
            UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(rect.x + rect.width - flWidth, rect.y, flWidth, rect.height), UnityEngine.Texture2D.whiteTexture);

            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        #endregion
    }
}