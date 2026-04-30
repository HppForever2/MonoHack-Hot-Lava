namespace HotLava_Cheat.Source.Visuals
{
    public static class Styles
    {
        private static bool bInitialized = false;

        public struct GS
        {
            public static UnityEngine.GUIStyle WindowStyle;
            public static UnityEngine.GUIStyle TabStyle;
            public static UnityEngine.GUIStyle ActiveTabStyle;
            public static UnityEngine.GUIStyle BoxStyle;
            public static UnityEngine.GUIStyle LabelStyle;
            public static UnityEngine.GUIStyle ButtonStyle;
            public static UnityEngine.GUIStyle CheckboxStyle;
            public static UnityEngine.GUIStyle SliderStyle;
            public static UnityEngine.GUIStyle SliderThumbStyle;
            public static UnityEngine.GUIStyle SeparatorStyle;
            public static UnityEngine.GUIStyle TooltipStyle;
            public static UnityEngine.GUIStyle LogoStyle;

            public static UnityEngine.GUIStyle SectionStyle;
            public static UnityEngine.GUIStyle SectionHeaderStyle;
            public static UnityEngine.GUIStyle SectionContentStyle;
        }

        public static void Initialize()
        {
            if (bInitialized)
                return;

            CreateStyles();

            bInitialized = true;
        }

        public static void Reinitialize()
        {
            bInitialized = false;

            CreateStyles();

            bInitialized = true;
        }

        private static void CreateStyles()
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            GS.WindowStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.window);
            SetAllBackgrounds(GS.WindowStyle, Textures.T2D.WindowBgTexture);
            SetAllTextColors(GS.WindowStyle, theme.TextPrimary);
            GS.WindowStyle.border = new UnityEngine.RectOffset(15, 15, 15, 15);
            GS.WindowStyle.fontSize = 18;
            GS.WindowStyle.fontStyle = UnityEngine.FontStyle.Bold;
            GS.WindowStyle.padding = new UnityEngine.RectOffset(15, 15, 15, 15);

            GS.LogoStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);
            GS.LogoStyle.fontSize = 32;
            GS.LogoStyle.fontStyle = UnityEngine.FontStyle.Bold;
            GS.LogoStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            GS.LogoStyle.normal.textColor = theme.LogoText;

            GS.TabStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.button);
            GS.TabStyle.normal.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.hover.background = Textures.T2D.TabHoverTexture;
            GS.TabStyle.focused.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.active.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.onNormal.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.onFocused.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.onHover.background = Textures.T2D.TabHoverTexture;
            GS.TabStyle.onActive.background = Textures.T2D.TabNormalTexture;
            GS.TabStyle.normal.textColor = theme.TabText;
            GS.TabStyle.hover.textColor = theme.TabTextHover;
            GS.TabStyle.focused.textColor = theme.TabText;
            GS.TabStyle.active.textColor = theme.TabText;
            GS.TabStyle.onNormal.textColor = theme.TabText;
            GS.TabStyle.onFocused.textColor = theme.TabText;
            GS.TabStyle.onHover.textColor = theme.TabTextHover;
            GS.TabStyle.onActive.textColor = theme.TabText;
            GS.TabStyle.fontSize = 14;
            GS.TabStyle.fontStyle = UnityEngine.FontStyle.Bold;
            GS.TabStyle.border = new UnityEngine.RectOffset(15, 15, 15, 15);
            GS.TabStyle.margin = new UnityEngine.RectOffset(0, 0, 0, 0);
            GS.TabStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;

            GS.ActiveTabStyle = new UnityEngine.GUIStyle(GS.TabStyle);
            GS.ActiveTabStyle.normal.background = Textures.T2D.TabActiveTexture;
            GS.ActiveTabStyle.hover.background = Textures.T2D.TabActiveHoverTexture;
            GS.ActiveTabStyle.focused.background = Textures.T2D.TabActiveTexture;
            GS.ActiveTabStyle.active.background = Textures.T2D.TabActiveTexture;
            GS.ActiveTabStyle.onNormal.background = Textures.T2D.TabActiveTexture;
            GS.ActiveTabStyle.onFocused.background = Textures.T2D.TabActiveTexture;
            GS.ActiveTabStyle.onHover.background = Textures.T2D.TabActiveHoverTexture;
            GS.ActiveTabStyle.onActive.background = Textures.T2D.TabActiveTexture;
            SetAllTextColors(GS.ActiveTabStyle, theme.TabTextActive);

            GS.BoxStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.box);
            SetAllBackgrounds(GS.BoxStyle, Textures.T2D.BoxBgTexture);
            GS.BoxStyle.border = new UnityEngine.RectOffset(8, 8, 8, 8);
            GS.BoxStyle.padding = new UnityEngine.RectOffset(12, 12, 12, 12);
            GS.BoxStyle.margin = new UnityEngine.RectOffset(5, 5, 5, 5);

            GS.LabelStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);
            SetAllTextColors(GS.LabelStyle, theme.TextPrimary);
            GS.LabelStyle.fontSize = 13;
            GS.LabelStyle.fontStyle = UnityEngine.FontStyle.Normal;

            GS.ButtonStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.button);
            GS.ButtonStyle.normal.background = Textures.T2D.ButtonBgTexture;
            GS.ButtonStyle.hover.background = Textures.T2D.ButtonHoverBgTexture;
            GS.ButtonStyle.active.background = Textures.T2D.ButtonActiveBgTexture;
            GS.ButtonStyle.focused.background = Textures.T2D.ButtonBgTexture;
            GS.ButtonStyle.onNormal.background = Textures.T2D.ButtonBgTexture;
            GS.ButtonStyle.onFocused.background = Textures.T2D.ButtonBgTexture;
            GS.ButtonStyle.onHover.background = Textures.T2D.ButtonHoverBgTexture;
            GS.ButtonStyle.onActive.background = Textures.T2D.ButtonActiveBgTexture;
            SetAllTextColors(GS.ButtonStyle, theme.ButtonText);
            GS.ButtonStyle.fontSize = 13;
            GS.ButtonStyle.fontStyle = UnityEngine.FontStyle.Bold;
            GS.ButtonStyle.border = new UnityEngine.RectOffset(6, 6, 6, 6);

            GS.CheckboxStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.toggle);
            GS.CheckboxStyle.normal.background = Textures.T2D.CheckBoxBgTexture;
            GS.CheckboxStyle.onNormal.background = Textures.T2D.CheckboxOnBgTexture;
            GS.CheckboxStyle.hover.background = Textures.T2D.CheckboxHoverBgTexture;
            GS.CheckboxStyle.onHover.background = Textures.T2D.CheckboxOnHoverBgTexture;
            GS.CheckboxStyle.active.background = Textures.T2D.CheckBoxBgTexture;
            GS.CheckboxStyle.onActive.background = Textures.T2D.CheckboxOnBgTexture;
            GS.CheckboxStyle.focused.background = Textures.T2D.CheckBoxBgTexture;
            GS.CheckboxStyle.onFocused.background = Textures.T2D.CheckboxOnBgTexture;
            SetAllTextColors(GS.CheckboxStyle, theme.TextPrimary);
            GS.CheckboxStyle.fontSize = 13;
            GS.CheckboxStyle.margin = new UnityEngine.RectOffset(0, 5, 2, 2);
            GS.CheckboxStyle.border = new UnityEngine.RectOffset(2, 2, 2, 2);
            GS.CheckboxStyle.fixedWidth = 20f;
            GS.CheckboxStyle.fixedHeight = 20f;

            GS.SliderStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.horizontalSlider);
            SetAllBackgrounds(GS.SliderStyle, Textures.T2D.SliderBgTexture);
            GS.SliderStyle.fixedHeight = 20f;
            GS.SliderStyle.border = new UnityEngine.RectOffset(4, 4, 0, 0);
            GS.SliderStyle.margin = new UnityEngine.RectOffset(0, 0, 6, 6);

            GS.SliderThumbStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.horizontalSliderThumb);
            GS.SliderThumbStyle.normal.background = Textures.T2D.ThumbBgTexture;
            GS.SliderThumbStyle.hover.background = Textures.T2D.ThumbHoverBgTexture;
            GS.SliderThumbStyle.active.background = Textures.T2D.ThumbActiveBgTexture;
            GS.SliderThumbStyle.focused.background = Textures.T2D.ThumbBgTexture;
            GS.SliderThumbStyle.onNormal.background = Textures.T2D.ThumbBgTexture;
            GS.SliderThumbStyle.onFocused.background = Textures.T2D.ThumbBgTexture;
            GS.SliderThumbStyle.onHover.background = Textures.T2D.ThumbHoverBgTexture;
            GS.SliderThumbStyle.onActive.background = Textures.T2D.ThumbActiveBgTexture;
            GS.SliderThumbStyle.fixedWidth = 16f;
            GS.SliderThumbStyle.fixedHeight = 16f;
            GS.SliderThumbStyle.border = new UnityEngine.RectOffset(8, 8, 8, 8);

            GS.SeparatorStyle = new UnityEngine.GUIStyle();
            GS.SeparatorStyle.normal.background = Textures.T2D.SeparatorBgTexture;
            GS.SeparatorStyle.fixedHeight = 2f;
            GS.SeparatorStyle.margin = new UnityEngine.RectOffset(0, 0, 8, 8);

            GS.TooltipStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.box);
            SetAllBackgrounds(GS.TooltipStyle, Textures.T2D.TooltipBgTexture);
            SetAllTextColors(GS.TooltipStyle, theme.TooltipText);
            GS.TooltipStyle.fontSize = 12;
            GS.TooltipStyle.padding = new UnityEngine.RectOffset(8, 8, 6, 6);
            GS.TooltipStyle.border = new UnityEngine.RectOffset(6, 6, 6, 6);
            GS.TooltipStyle.wordWrap = true;
            GS.TooltipStyle.alignment = UnityEngine.TextAnchor.MiddleLeft;

            GS.SectionStyle = new UnityEngine.GUIStyle();
            SetAllBackgrounds(GS.SectionStyle, Textures.T2D.SectionBgTexture);
            GS.SectionStyle.border = new UnityEngine.RectOffset(10, 10, 10, 10);
            GS.SectionStyle.padding = new UnityEngine.RectOffset(0, 0, 0, 8);
            GS.SectionStyle.margin = new UnityEngine.RectOffset(0, 0, 0, 0);

            GS.SectionHeaderStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);
            SetAllTextColors(GS.SectionHeaderStyle, theme.TextPrimary);
            GS.SectionHeaderStyle.fontSize = 14;
            GS.SectionHeaderStyle.fontStyle = UnityEngine.FontStyle.Bold;
            GS.SectionHeaderStyle.alignment = UnityEngine.TextAnchor.MiddleLeft;
            GS.SectionHeaderStyle.padding = new UnityEngine.RectOffset(14, 10, 0, 0);
            GS.SectionHeaderStyle.fixedHeight = 30f;

            GS.SectionContentStyle = new UnityEngine.GUIStyle();
            GS.SectionContentStyle.padding = new UnityEngine.RectOffset(14, 14, 10, 6);
        }

        public static void SetAllBackgrounds(UnityEngine.GUIStyle gsStyle, UnityEngine.Texture2D t2dTexture)
        {
            gsStyle.normal.background = t2dTexture;
            gsStyle.focused.background = t2dTexture;
            gsStyle.hover.background = t2dTexture;
            gsStyle.active.background = t2dTexture;
            gsStyle.onNormal.background = t2dTexture;
            gsStyle.onFocused.background = t2dTexture;
            gsStyle.onHover.background = t2dTexture;
            gsStyle.onActive.background = t2dTexture;
        }

        public static void SetAllTextColors(UnityEngine.GUIStyle gsStyle, UnityEngine.Color colText)
        {
            gsStyle.normal.textColor = colText;
            gsStyle.focused.textColor = colText;
            gsStyle.hover.textColor = colText;
            gsStyle.active.textColor = colText;
            gsStyle.onNormal.textColor = colText;
            gsStyle.onFocused.textColor = colText;
            gsStyle.onHover.textColor = colText;
            gsStyle.onActive.textColor = colText;
        }
    }
}