using UnityEngine;

namespace HotLava_Cheat.Source.Visuals
{
    public static class Textures
    {
        public struct T2D
        {
            public static UnityEngine.Texture2D WindowBgTexture;
            public static UnityEngine.Texture2D TabBgTexture;
            public static UnityEngine.Texture2D TabHoverBgTexture;
            public static UnityEngine.Texture2D ActiveTabBgTexture;
            public static UnityEngine.Texture2D ActiveTabHoverBgTexture;
            public static UnityEngine.Texture2D BoxBgTexture;
            public static UnityEngine.Texture2D ButtonBgTexture;
            public static UnityEngine.Texture2D ButtonHoverBgTexture;
            public static UnityEngine.Texture2D ButtonActiveBgTexture;
            public static UnityEngine.Texture2D CheckBoxBgTexture;
            public static UnityEngine.Texture2D CheckboxOnBgTexture;
            public static UnityEngine.Texture2D CheckboxHoverBgTexture;
            public static UnityEngine.Texture2D CheckboxOnHoverBgTexture;
            public static UnityEngine.Texture2D SliderBgTexture;
            public static UnityEngine.Texture2D ThumbBgTexture;
            public static UnityEngine.Texture2D ThumbHoverBgTexture;
            public static UnityEngine.Texture2D ThumbActiveBgTexture;
            public static UnityEngine.Texture2D SeparatorBgTexture;
            public static UnityEngine.Texture2D TooltipBgTexture;
            public static UnityEngine.Texture2D OverlayTexture;
            public static UnityEngine.Texture2D LogoBgTexture;
            public static UnityEngine.Texture2D TabNormalTexture;
            public static UnityEngine.Texture2D TabHoverTexture;
            public static UnityEngine.Texture2D TabActiveTexture;
            public static UnityEngine.Texture2D TabActiveHoverTexture;
            public static UnityEngine.Texture2D TabBorderTexture;
            public static UnityEngine.Texture2D SectionBgTexture;
            public static UnityEngine.Texture2D SectionHeaderTexture;
        }

        private static bool s_bInitialized = false;

        private static float CalculateCornerAlpha(int x, int y, int iWidth, int iHeight, int iRadius)
        {
            float flAlpha = 1f;

            if (x < iRadius && y < iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((iRadius - x) * (iRadius - x) + (iRadius - y) * (iRadius - y));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            else if (x >= iWidth - iRadius && y < iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((x - (iWidth - iRadius - 1)) * (x - (iWidth - iRadius - 1)) + (iRadius - y) * (iRadius - y));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            else if (x < iRadius && y >= iHeight - iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((iRadius - x) * (iRadius - x) + (y - (iHeight - iRadius - 1)) * (y - (iHeight - iRadius - 1)));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            else if (x >= iWidth - iRadius && y >= iHeight - iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((x - (iWidth - iRadius - 1)) * (x - (iWidth - iRadius - 1)) + (y - (iHeight - iRadius - 1)) * (y - (iHeight - iRadius - 1)));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            return flAlpha;
        }

        private static float CalculateTopCornerAlpha(int x, int y, int iWidth, int iHeight, int iRadius)
        {
            float flAlpha = 1f;

            if (x < iRadius && y < iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((iRadius - x) * (iRadius - x) + (iRadius - y) * (iRadius - y));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            else if (x >= iWidth - iRadius && y < iRadius)
            {
                float flDist = UnityEngine.Mathf.Sqrt((x - (iWidth - iRadius - 1)) * (x - (iWidth - iRadius - 1)) + (iRadius - y) * (iRadius - y));

                if (flDist > iRadius)
                    flAlpha = 0f;

                else if (flDist > iRadius - 1.5f)
                    flAlpha = UnityEngine.Mathf.Clamp01((iRadius - flDist) / 1.5f);
            }

            return flAlpha;
        }

        private static UnityEngine.Texture2D CreateSectionBackground(Themes.ThemeColors theme)
        {
            int iWidth = 64;
            int iHeight = 64;

            UnityEngine.Texture2D t2d = new UnityEngine.Texture2D(iWidth, iHeight);
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            int iRadius = 8;
            int iLineOffset = 2;
            int iLineWidth = 3;

            for (int y = 0; y < iHeight; y++)
            {
                for (int x = 0; x < iWidth; x++)
                {
                    float flGradientV = (float)y / (float)iHeight;

                    UnityEngine.Color colBase = theme.SectionBg;

                    colBase.r += (1f - flGradientV) * 0.015f;
                    colBase.g += (1f - flGradientV) * 0.012f;
                    colBase.b += (1f - flGradientV) * 0.025f;

                    if (x >= iLineOffset && x < iLineOffset + iLineWidth)
                    {
                        float flIntensity = 1f - (float)(x - iLineOffset) / (float)iLineWidth;
                        colBase = UnityEngine.Color.Lerp(colBase, theme.SectionAccent, flIntensity * 0.85f);
                    }

                    float flAlpha = CalculateCornerAlpha(x, y, iWidth, iHeight, iRadius);

                    colBase.a *= flAlpha;
                    colArray[y * iWidth + x] = colBase;
                }
            }

            t2d.SetPixels(colArray);
            t2d.Apply();

            t2d.filterMode = UnityEngine.FilterMode.Bilinear;
            t2d.wrapMode = UnityEngine.TextureWrapMode.Clamp;

            return t2d;
        }

        private static UnityEngine.Texture2D CreateSectionHeader(Themes.ThemeColors theme)
        {
            int iWidth = 64;
            int iHeight = 32;

            UnityEngine.Texture2D t2d = new UnityEngine.Texture2D(iWidth, iHeight);
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            int iRadius = 8;

            for (int y = 0; y < iHeight; y++)
            {
                for (int x = 0; x < iWidth; x++)
                {
                    float flGradientV = (float)y / (float)iHeight;
                    float flGradientH = (float)x / (float)iWidth;

                    UnityEngine.Color colBase = UnityEngine.Color.Lerp(theme.SectionHeaderLeft, theme.SectionHeaderRight, flGradientH);

                    colBase.r += (1f - flGradientV) * 0.03f;
                    colBase.g += (1f - flGradientV) * 0.025f;
                    colBase.b += (1f - flGradientV) * 0.05f;

                    if (x < 4)
                    {
                        float flIntensity = 1f - (float)x / 4f;

                        colBase = UnityEngine.Color.Lerp(colBase, theme.SectionAccent, flIntensity);
                    }

                    if (y >= iHeight - 1)
                        colBase = UnityEngine.Color.Lerp(colBase, theme.SectionHeaderLine, 0.5f);

                    float flAlpha = CalculateTopCornerAlpha(x, y, iWidth, iHeight, iRadius);

                    colBase.a *= flAlpha;
                    colArray[y * iWidth + x] = colBase;
                }
            }

            t2d.SetPixels(colArray);
            t2d.Apply();

            t2d.filterMode = UnityEngine.FilterMode.Bilinear;
            t2d.wrapMode = UnityEngine.TextureWrapMode.Clamp;

            return t2d;
        }

        private static void CreateTabs(Themes.ThemeColors theme)
        {
            T2D.TabNormalTexture = CreateGradient(120, 45, theme.TabNormalTop, theme.TabNormalBottom, true);
            T2D.TabHoverTexture = CreateGradient(120, 45, theme.TabHoverTop, theme.TabHoverBottom, true);
            T2D.TabActiveTexture = CreateGradient(120, 45, theme.TabActiveTop, theme.TabActiveBottom, true);
            T2D.TabActiveHoverTexture = CreateGradient(120, 45, theme.TabActiveHoverTop, theme.TabActiveHoverBottom, true);
            T2D.TabBorderTexture = CreateBorder(120, 45, theme.TabBorder, 2);
        }

        private static UnityEngine.Texture2D MakeTex(int iWidth, int iHeight, UnityEngine.Color colFill)
        {
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            for (int i = 0; i < colArray.Length; i++)
                colArray[i] = colFill;

            UnityEngine.Texture2D t2dResult = new UnityEngine.Texture2D(iWidth, iHeight);

            t2dResult.SetPixels(colArray);
            t2dResult.Apply();

            return t2dResult;
        }

        private static UnityEngine.Texture2D CreateLogo(Themes.ThemeColors theme)
        {
            int iWidth = 690;
            int iHeight = 70;

            UnityEngine.Texture2D t2dLogo = new UnityEngine.Texture2D(iWidth, iHeight);
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            for (int y = 0; y < iHeight; y++)
            {
                for (int x = 0; x < iWidth; x++)
                {
                    float flGradientV = (float)y / (float)iHeight;
                    float flGradientH = (float)x / (float)iWidth;

                    UnityEngine.Color colTop = UnityEngine.Color.Lerp(theme.LogoTopLeft, theme.LogoTopRight, flGradientH);
                    UnityEngine.Color colBottom = UnityEngine.Color.Lerp(theme.LogoBottomLeft, theme.LogoBottomRight, flGradientH);
                    UnityEngine.Color colResult = UnityEngine.Color.Lerp(colTop, colBottom, flGradientV);

                    if (y < 2)
                    {
                        float flLineAlpha = 1f - (float)y / 2f;

                        colResult = UnityEngine.Color.Lerp(colResult, theme.LogoAccent, flLineAlpha * 0.7f);
                    }

                    if (y > iHeight - 2)
                    {
                        float flLineAlpha = (float)(y - (iHeight - 2)) / 1f;

                        colResult = UnityEngine.Color.Lerp(colResult, theme.LogoAccent, flLineAlpha * 0.4f);
                    }

                    float flCenterDist = UnityEngine.Mathf.Abs(flGradientV - 0.35f);

                    if (flCenterDist < 0.08f)
                    {
                        float flHighlight = (0.08f - flCenterDist) / 0.08f;

                        flHighlight *= flHighlight;
                        colResult = UnityEngine.Color.Lerp(colResult, theme.LogoAccentLight, flHighlight * 0.08f);
                    }

                    float flNoise = UnityEngine.Mathf.PerlinNoise((float)x * 0.015f + 50f, (float)y * 0.04f) * 0.015f;

                    colResult.r += flNoise * 0.5f;
                    colResult.g += flNoise * 0.4f;
                    colResult.b += flNoise;

                    float flVignetteH = 1f - UnityEngine.Mathf.Pow(UnityEngine.Mathf.Abs((flGradientH - 0.5f) * 2f), 2f);

                    colResult.r *= 0.85f + flVignetteH * 0.15f;
                    colResult.g *= 0.85f + flVignetteH * 0.15f;
                    colResult.b *= 0.85f + flVignetteH * 0.15f;

                    colArray[y * iWidth + x] = colResult;
                }
            }

            t2dLogo.SetPixels(colArray);
            t2dLogo.Apply();

            t2dLogo.filterMode = UnityEngine.FilterMode.Bilinear;

            return t2dLogo;
        }

        private static UnityEngine.Texture2D CreateGradient(int iWidth, int iHeight, UnityEngine.Color colTop, UnityEngine.Color colBottom, bool bAddNoise = false)
        {
            UnityEngine.Texture2D t2dGradient = new UnityEngine.Texture2D(iWidth, iHeight);
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            for (int i = 0; i < iHeight; i++)
            {
                for (int j = 0; j < iWidth; j++)
                {
                    float flGradient = (float)i / (float)iHeight;

                    UnityEngine.Color colResult = UnityEngine.Color.Lerp(colTop, colBottom, flGradient);

                    if (bAddNoise)
                    {
                        float flNoise = (UnityEngine.Mathf.PerlinNoise((float)j * 0.05f, (float)i * 0.05f) - 0.5f) * 0.03f;

                        colResult.r += flNoise;
                        colResult.g += flNoise;
                        colResult.b += flNoise;
                    }

                    float flEdge = UnityEngine.Mathf.Clamp01((float)UnityEngine.Mathf.Min(UnityEngine.Mathf.Min(j, iWidth - j - 1), UnityEngine.Mathf.Min(i, iHeight - i - 1)) / 3f);

                    colResult = UnityEngine.Color.Lerp(colResult * 0.7f, colResult, flEdge);
                    colArray[i * iWidth + j] = colResult;
                }
            }

            t2dGradient.SetPixels(colArray);
            t2dGradient.Apply();

            return t2dGradient;
        }

        private static UnityEngine.Texture2D CreateBorder(int iWidth, int iHeight, UnityEngine.Color colBorder, int iBorderWidth)
        {
            UnityEngine.Texture2D t2dBorder = new UnityEngine.Texture2D(iWidth, iHeight);
            UnityEngine.Color[] colArray = new UnityEngine.Color[iWidth * iHeight];

            for (int i = 0; i < iHeight; i++)
            {
                for (int j = 0; j < iWidth; j++)
                {
                    bool bIsBorder = j < iBorderWidth || j >= iWidth - iBorderWidth || i < iBorderWidth || i >= iHeight - iBorderWidth;

                    colArray[i * iWidth + j] = (bIsBorder ? colBorder : UnityEngine.Color.clear);
                }
            }

            t2dBorder.SetPixels(colArray);
            t2dBorder.Apply();

            return t2dBorder;
        }

        public static void CreateAll()
        {
            if (s_bInitialized)
                return;

            Themes.ThemeColors theme = Themes.InitializeDefaultTheme();

            CreateAllWithTheme(theme);

            s_bInitialized = true;
        }

        public static void RecreateAllWithTheme(Themes.ThemeColors theme)
        {
            DestroyAllTextures();
            CreateAllWithTheme(theme);
        }

        private static void CreateAllWithTheme(Themes.ThemeColors theme)
        {
            T2D.WindowBgTexture = MakeTex(2, 2, theme.WindowBg);
            T2D.TabBgTexture = MakeTex(2, 2, theme.TabNormalTop);
            T2D.TabHoverBgTexture = MakeTex(2, 2, theme.TabHoverTop);
            T2D.ActiveTabBgTexture = MakeTex(2, 2, theme.TabActiveTop);
            T2D.ActiveTabHoverBgTexture = MakeTex(2, 2, theme.TabActiveHoverTop);
            T2D.BoxBgTexture = MakeTex(2, 2, theme.BoxBg);

            T2D.ButtonBgTexture = MakeTex(2, 2, theme.ButtonBg);
            T2D.ButtonHoverBgTexture = MakeTex(2, 2, theme.ButtonHover);
            T2D.ButtonActiveBgTexture = MakeTex(2, 2, theme.ButtonActive);

            T2D.CheckBoxBgTexture = MakeTex(20, 20, theme.CheckboxBg);
            T2D.CheckboxOnBgTexture = MakeTex(20, 20, theme.CheckboxOn);
            T2D.CheckboxHoverBgTexture = MakeTex(20, 20, theme.CheckboxHover);
            T2D.CheckboxOnHoverBgTexture = MakeTex(20, 20, theme.CheckboxOnHover);

            T2D.SliderBgTexture = MakeTex(2, 2, theme.SliderBg);
            T2D.ThumbBgTexture = MakeTex(2, 2, theme.SliderThumb);
            T2D.ThumbHoverBgTexture = MakeTex(2, 2, theme.SliderThumbHover);
            T2D.ThumbActiveBgTexture = MakeTex(2, 2, theme.SliderThumbActive);

            T2D.SeparatorBgTexture = MakeTex(2, 2, theme.Separator);

            T2D.TooltipBgTexture = MakeTex(2, 2, theme.TooltipBg);
            T2D.OverlayTexture = MakeTex(1, 1, new UnityEngine.Color(0f, 0f, 0f, 0.5f));

            T2D.SectionBgTexture = CreateSectionBackground(theme);
            T2D.SectionHeaderTexture = CreateSectionHeader(theme);

            T2D.LogoBgTexture = CreateLogo(theme);

            CreateTabs(theme);
        }

        private static void DestroyAllTextures()
        {
            Texture2D[] textures = new Texture2D[]
            {
                T2D.WindowBgTexture,
                T2D.TabBgTexture, T2D.TabHoverBgTexture, T2D.ActiveTabBgTexture, T2D.ActiveTabHoverBgTexture,
                T2D.BoxBgTexture,
                T2D.ButtonBgTexture, T2D.ButtonHoverBgTexture, T2D.ButtonActiveBgTexture, 
                T2D.CheckBoxBgTexture, T2D.CheckboxOnBgTexture, T2D.CheckboxHoverBgTexture, T2D.CheckboxOnHoverBgTexture,
                T2D.SliderBgTexture,
                T2D.ThumbBgTexture, T2D.ThumbHoverBgTexture, T2D.ThumbActiveBgTexture,
                T2D.SeparatorBgTexture,
                T2D.TooltipBgTexture,
                T2D.OverlayTexture,
                T2D.LogoBgTexture,
                T2D.TabNormalTexture, T2D.TabHoverTexture, T2D.TabActiveTexture, T2D.TabActiveHoverTexture, T2D.TabBorderTexture,
                T2D.SectionBgTexture, T2D.SectionHeaderTexture
            };

            foreach (var texture in textures)
            {
                if (texture != null)
                    UnityEngine.Object.Destroy(texture);
            }
        }
    }
}