namespace HotLava_Cheat.Source.Visuals
{
    public static class Themes
    {
        public static string[] ThemeNamesEN = new string[]
        {
            "Purple night",
            "Cyber neon",
            "Ocean breeze",
            "Crimson fire",
            "Emerald forest",
            "Midnight gold",
            "Arctic frost",
            "Sunset blaze",
            "Royal velvet",
            "Toxic lime",
            "Neon sakura",
            "Obsidian steel",
            "Electric violet",
            "Copper bronze",
            "Matrix green",
            "Bubblegum pop",
            "Mocha coffee",
            "Aurora borealis",
            "Lavender dream",
            "Midnight cherry",
            "Plasma orange",
            "Deep space",
            "Mint fresh",
            "Blood moon",
            "Golden sand",
            "Ice crystal",
            "Neon yellow",
            "Dark rose",
            "Ocean depth",
            "Radioactive"
        };

        public static string[] ThemeNamesRU = new string[]
        {
            "Пурпурная ночь",
            "Кибер неон",
            "Морской бриз",
            "Багровый огонь",
            "Изумрудный лес",
            "Полуночное золото",
            "Арктический мороз",
            "Закатное пламя",
            "Королевский бархат",
            "Токсичный лайм",
            "Неоновая сакура",
            "Обсидиановая сталь",
            "Электрический фиолет",
            "Медная бронза",
            "Матрица",
            "Жвачка",
            "Мокко кофе",
            "Северное сияние",
            "Лавандовая мечта",
            "Полуночная вишня",
            "Плазменный апельсин",
            "Глубокий космос",
            "Свежая мята",
            "Кровавая луна",
            "Золотой песок",
            "Ледяной кристалл",
            "Неоновый жёлтый",
            "Тёмная роза",
            "Глубины океана",
            "Радиоактивный"
        };

        public struct ThemeColors
        {
            public UnityEngine.Color WindowBg;

            public UnityEngine.Color TabNormalTop;
            public UnityEngine.Color TabNormalBottom;
            public UnityEngine.Color TabHoverTop;
            public UnityEngine.Color TabHoverBottom;
            public UnityEngine.Color TabActiveTop;
            public UnityEngine.Color TabActiveBottom;
            public UnityEngine.Color TabActiveHoverTop;
            public UnityEngine.Color TabActiveHoverBottom;
            public UnityEngine.Color TabBorder;
            public UnityEngine.Color TabText;
            public UnityEngine.Color TabTextHover;
            public UnityEngine.Color TabTextActive;

            public UnityEngine.Color BoxBg;

            public UnityEngine.Color ButtonBg;
            public UnityEngine.Color ButtonHover;
            public UnityEngine.Color ButtonActive;
            public UnityEngine.Color ButtonText;

            public UnityEngine.Color CheckboxBg;
            public UnityEngine.Color CheckboxOn;
            public UnityEngine.Color CheckboxHover;
            public UnityEngine.Color CheckboxOnHover;

            public UnityEngine.Color SliderBg;
            public UnityEngine.Color SliderThumb;
            public UnityEngine.Color SliderThumbHover;
            public UnityEngine.Color SliderThumbActive;

            public UnityEngine.Color Separator;

            public UnityEngine.Color TooltipBg;
            public UnityEngine.Color TooltipText;

            public UnityEngine.Color SectionBg;
            public UnityEngine.Color SectionAccent;
            public UnityEngine.Color SectionHeaderLeft;
            public UnityEngine.Color SectionHeaderRight;
            public UnityEngine.Color SectionHeaderLine;

            public UnityEngine.Color LogoTopLeft;
            public UnityEngine.Color LogoTopRight;
            public UnityEngine.Color LogoBottomLeft;
            public UnityEngine.Color LogoBottomRight;
            public UnityEngine.Color LogoAccent;
            public UnityEngine.Color LogoAccentLight;
            public UnityEngine.Color LogoText;
            public UnityEngine.Color LogoSubtitle;

            public UnityEngine.Color TextPrimary;
            public UnityEngine.Color TextSecondary;

            public UnityEngine.Color DropdownBg;
            public UnityEngine.Color DropdownBgHover;
            public UnityEngine.Color DropdownBgOpen;
            public UnityEngine.Color DropdownBorder;
            public UnityEngine.Color DropdownAccent;
            public UnityEngine.Color DropdownListBg;
            public UnityEngine.Color DropdownItemHover;
            public UnityEngine.Color DropdownItemSelected;

            public UnityEngine.Color GlowPrimary;
            public UnityEngine.Color GlowSecondary;
        }

        private static ThemeColors s_currentTheme;

        private static int s_iCurrentThemeIndex = 0;
        private static bool s_bInitialized = false;

        public static ThemeColors CurrentTheme
        {
            get
            {
                if (!s_bInitialized)
                    InitializeDefaultTheme();

                return s_currentTheme;
            }
        }

        public static ThemeColors InitializeDefaultTheme()
        {
            if (!s_bInitialized)
            {
                s_iCurrentThemeIndex = NS_Core.Vars.sTab.sOther.iTheme;
                s_currentTheme = GetThemeByIndex(s_iCurrentThemeIndex);

                s_bInitialized = true;
            }

            return s_currentTheme;
        }

        public static void ApplyTheme(int iThemeIndex)
        {
            s_iCurrentThemeIndex = iThemeIndex;
            s_currentTheme = GetThemeByIndex(iThemeIndex);

            s_bInitialized = true;

            Textures.RecreateAllWithTheme(s_currentTheme);
            Styles.Reinitialize();
        }

        public static int GetCurrentThemeIndex()
        {
            return s_iCurrentThemeIndex;
        }

        private static ThemeColors GetThemeByIndex(int iIndex)
        {
            switch (iIndex)
            {
                case 0: return GetPurpleNightTheme();
                case 1: return GetCyberNeonTheme();
                case 2: return GetOceanBreezeTheme();
                case 3: return GetCrimsonFireTheme();
                case 4: return GetEmeraldForestTheme();
                case 5: return GetMidnightGoldTheme();
                case 6: return GetArcticFrostTheme();
                case 7: return GetSunsetBlazeTheme();
                case 8: return GetRoyalVelvetTheme();
                case 9: return GetToxicLimeTheme();
                case 10: return GetNeonSakuraTheme();
                case 11: return GetObsidianSteelTheme();
                case 12: return GetElectricVioletTheme();
                case 13: return GetCopperBronzeTheme();
                case 14: return GetMatrixGreenTheme();
                case 15: return GetBubblegumPopTheme();
                case 16: return GetMochaCoffeeTheme();
                case 17: return GetAuroraBorealisTheme();
                case 18: return GetLavenderDreamTheme();
                case 19: return GetMidnightCherryTheme();
                case 20: return GetPlasmaOrangeTheme();
                case 21: return GetDeepSpaceTheme();
                case 22: return GetMintFreshTheme();
                case 23: return GetBloodMoonTheme();
                case 24: return GetGoldenSandTheme();
                case 25: return GetIceCrystalTheme();
                case 26: return GetNeonYellowTheme();
                case 27: return GetDarkRoseTheme();
                case 28: return GetOceanDepthTheme();
                case 29: return GetRadioactiveTheme();
                default: return GetPurpleNightTheme();
            }
        }

        private static ThemeColors GetPurpleNightTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.05f, 0.05f, 0.15f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.10f, 0.08f, 0.18f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.14f, 0.11f, 0.24f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.18f, 0.15f, 0.30f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.22f, 0.18f, 0.35f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.40f, 0.32f, 0.75f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.50f, 0.40f, 0.85f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.50f, 0.42f, 0.85f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.60f, 0.50f, 0.95f, 0.98f),
                TabBorder = new UnityEngine.Color(0.5f, 0.4f, 0.9f, 0.6f),
                TabText = new UnityEngine.Color(0.8f, 0.8f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 1f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.08f, 0.08f, 0.2f, 0.9f),

                ButtonBg = new UnityEngine.Color(0.35f, 0.3f, 0.7f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.45f, 0.4f, 0.85f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.3f, 0.25f, 0.6f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.15f, 0.12f, 0.25f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.45f, 0.35f, 0.85f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.22f, 0.18f, 0.35f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.55f, 0.45f, 0.95f, 0.95f),

                SliderBg = new UnityEngine.Color(0.15f, 0.12f, 0.25f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.45f, 0.35f, 0.85f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.55f, 0.45f, 0.95f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.35f, 0.28f, 0.7f, 0.95f),

                Separator = new UnityEngine.Color(0.45f, 0.35f, 0.8f, 0.6f),

                TooltipBg = new UnityEngine.Color(0.05f, 0.05f, 0.1f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 1f, 0.8f, 1f),

                SectionBg = new UnityEngine.Color(0.08f, 0.07f, 0.14f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.45f, 0.35f, 0.8f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.16f, 0.12f, 0.28f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.10f, 0.08f, 0.18f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.4f, 0.35f, 0.7f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.12f, 0.10f, 0.22f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.18f, 0.12f, 0.28f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.05f, 0.12f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.10f, 0.06f, 0.18f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.5f, 0.4f, 0.8f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.8f, 0.8f, 0.95f, 1f),
                LogoText = new UnityEngine.Color(0.9f, 0.9f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.8f, 0.8f, 0.95f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.9f, 0.9f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.8f, 0.8f, 0.95f, 1f),

                DropdownBg = new UnityEngine.Color(0.12f, 0.10f, 0.22f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.18f, 0.15f, 0.32f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.20f, 0.17f, 0.35f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.5f, 0.4f, 0.9f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.6f, 0.5f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.10f, 0.08f, 0.18f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.5f, 0.4f, 0.9f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.5f, 0.4f, 0.9f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.5f, 0.4f, 0.9f, 1f),
                GlowSecondary = new UnityEngine.Color(0.7f, 0.6f, 1f, 1f)
            };
        }

        private static ThemeColors GetCyberNeonTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.02f, 0.02f, 0.06f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.05f, 0.05f, 0.12f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.08f, 0.08f, 0.18f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.1f, 0.15f, 0.25f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.12f, 0.2f, 0.32f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0f, 0.8f, 0.9f, 0.95f),
                TabActiveBottom = new UnityEngine.Color(0f, 0.6f, 0.8f, 0.95f),
                TabActiveHoverTop = new UnityEngine.Color(0.2f, 0.9f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.1f, 0.75f, 0.9f, 0.98f),
                TabBorder = new UnityEngine.Color(0f, 0.9f, 1f, 0.7f),
                TabText = new UnityEngine.Color(0.7f, 0.85f, 0.9f, 1f),
                TabTextHover = new UnityEngine.Color(0.8f, 1f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(0.02f, 0.02f, 0.06f, 1f),

                BoxBg = new UnityEngine.Color(0.04f, 0.06f, 0.12f, 0.92f),

                ButtonBg = new UnityEngine.Color(0f, 0.5f, 0.6f, 0.9f),
                ButtonHover = new UnityEngine.Color(0f, 0.7f, 0.85f, 0.95f),
                ButtonActive = new UnityEngine.Color(0f, 0.4f, 0.5f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.06f, 0.08f, 0.15f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0f, 0.85f, 0.95f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.08f, 0.12f, 0.22f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.2f, 0.95f, 1f, 0.95f),

                SliderBg = new UnityEngine.Color(0.06f, 0.08f, 0.15f, 0.9f),
                SliderThumb = new UnityEngine.Color(0f, 0.85f, 0.95f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.3f, 1f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0f, 0.6f, 0.7f, 0.95f),

                Separator = new UnityEngine.Color(0f, 0.8f, 0.9f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.02f, 0.04f, 0.08f, 0.98f),
                TooltipText = new UnityEngine.Color(0.7f, 1f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.03f, 0.05f, 0.1f, 0.92f),
                SectionAccent = new UnityEngine.Color(0f, 0.9f, 1f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.05f, 0.12f, 0.2f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.03f, 0.06f, 0.12f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0f, 0.7f, 0.8f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.04f, 0.08f, 0.15f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.02f, 0.12f, 0.2f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.02f, 0.03f, 0.08f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.03f, 0.06f, 0.12f, 0.98f),
                LogoAccent = new UnityEngine.Color(0f, 0.9f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.6f, 1f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.85f, 1f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.5f, 0.8f, 0.85f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.85f, 0.95f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.6f, 0.8f, 0.85f, 1f),

                DropdownBg = new UnityEngine.Color(0.04f, 0.06f, 0.14f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.06f, 0.12f, 0.22f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.08f, 0.15f, 0.25f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0f, 0.9f, 1f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0f, 1f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.03f, 0.05f, 0.12f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0f, 0.8f, 0.9f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0f, 0.8f, 0.9f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0f, 0.9f, 1f, 1f),
                GlowSecondary = new UnityEngine.Color(0.4f, 1f, 1f, 1f)
            };
        }

        private static ThemeColors GetOceanBreezeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.04f, 0.08f, 0.12f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.06f, 0.12f, 0.18f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.08f, 0.16f, 0.24f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.1f, 0.22f, 0.32f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.12f, 0.28f, 0.4f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.15f, 0.5f, 0.65f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.1f, 0.4f, 0.55f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.2f, 0.6f, 0.75f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.15f, 0.5f, 0.65f, 0.98f),
                TabBorder = new UnityEngine.Color(0.3f, 0.7f, 0.85f, 0.6f),
                TabText = new UnityEngine.Color(0.75f, 0.88f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(0.9f, 1f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.05f, 0.1f, 0.16f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.12f, 0.4f, 0.55f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.18f, 0.55f, 0.7f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.1f, 0.32f, 0.45f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.06f, 0.14f, 0.2f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.2f, 0.6f, 0.75f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.08f, 0.18f, 0.28f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.28f, 0.7f, 0.85f, 0.95f),

                SliderBg = new UnityEngine.Color(0.06f, 0.14f, 0.2f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.2f, 0.6f, 0.75f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.3f, 0.75f, 0.9f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.15f, 0.45f, 0.6f, 0.95f),

                Separator = new UnityEngine.Color(0.25f, 0.6f, 0.75f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.03f, 0.06f, 0.1f, 0.98f),
                TooltipText = new UnityEngine.Color(0.85f, 1f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.045f, 0.09f, 0.13f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.25f, 0.65f, 0.8f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.08f, 0.18f, 0.26f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.05f, 0.11f, 0.17f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.2f, 0.55f, 0.7f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.06f, 0.14f, 0.2f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.08f, 0.18f, 0.28f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.03f, 0.07f, 0.11f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.05f, 0.1f, 0.16f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.3f, 0.7f, 0.85f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.7f, 0.92f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.9f, 0.98f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.6f, 0.8f, 0.88f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.88f, 0.96f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.7f, 0.85f, 0.92f, 1f),

                DropdownBg = new UnityEngine.Color(0.06f, 0.12f, 0.18f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.08f, 0.18f, 0.28f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.1f, 0.22f, 0.32f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.3f, 0.7f, 0.85f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.35f, 0.8f, 0.95f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.05f, 0.1f, 0.16f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.2f, 0.55f, 0.7f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.2f, 0.55f, 0.7f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.3f, 0.7f, 0.85f, 1f),
                GlowSecondary = new UnityEngine.Color(0.5f, 0.85f, 0.95f, 1f)
            };
        }

        private static ThemeColors GetCrimsonFireTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.03f, 0.03f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.14f, 0.06f, 0.06f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.18f, 0.08f, 0.08f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.28f, 0.1f, 0.1f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.35f, 0.12f, 0.1f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.85f, 0.2f, 0.15f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.7f, 0.15f, 0.1f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.95f, 0.3f, 0.2f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.8f, 0.22f, 0.15f, 0.98f),
                TabBorder = new UnityEngine.Color(0.9f, 0.3f, 0.2f, 0.6f),
                TabText = new UnityEngine.Color(0.95f, 0.8f, 0.78f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.9f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.04f, 0.04f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.7f, 0.18f, 0.12f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.85f, 0.25f, 0.18f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.55f, 0.12f, 0.08f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.16f, 0.06f, 0.06f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.85f, 0.22f, 0.15f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.24f, 0.1f, 0.08f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.95f, 0.32f, 0.22f, 0.95f),

                SliderBg = new UnityEngine.Color(0.16f, 0.06f, 0.06f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.85f, 0.22f, 0.15f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.35f, 0.25f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.65f, 0.15f, 0.1f, 0.95f),

                Separator = new UnityEngine.Color(0.85f, 0.25f, 0.18f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.02f, 0.02f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.92f, 0.88f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.035f, 0.035f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.9f, 0.28f, 0.18f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.2f, 0.08f, 0.06f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.12f, 0.05f, 0.04f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.75f, 0.2f, 0.15f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.16f, 0.06f, 0.05f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.22f, 0.08f, 0.06f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.07f, 0.025f, 0.02f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.1f, 0.04f, 0.03f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.95f, 0.35f, 0.25f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.8f, 0.75f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.95f, 0.92f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.9f, 0.75f, 0.7f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.94f, 0.92f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.8f, 0.78f, 1f),

                DropdownBg = new UnityEngine.Color(0.14f, 0.055f, 0.05f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.22f, 0.09f, 0.07f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.26f, 0.1f, 0.08f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.9f, 0.3f, 0.2f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.4f, 0.28f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.1f, 0.04f, 0.035f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.8f, 0.2f, 0.15f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.8f, 0.2f, 0.15f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.95f, 0.3f, 0.2f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.5f, 0.4f, 1f)
            };
        }

        private static ThemeColors GetEmeraldForestTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.03f, 0.07f, 0.04f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.05f, 0.12f, 0.07f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.07f, 0.16f, 0.09f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.1f, 0.24f, 0.14f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.12f, 0.3f, 0.18f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.18f, 0.7f, 0.35f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.12f, 0.55f, 0.28f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.25f, 0.8f, 0.45f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.18f, 0.65f, 0.35f, 0.98f),
                TabBorder = new UnityEngine.Color(0.3f, 0.8f, 0.45f, 0.6f),
                TabText = new UnityEngine.Color(0.8f, 0.95f, 0.85f, 1f),
                TabTextHover = new UnityEngine.Color(0.92f, 1f, 0.95f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.04f, 0.09f, 0.05f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.15f, 0.55f, 0.3f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.22f, 0.7f, 0.4f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.1f, 0.42f, 0.22f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.06f, 0.13f, 0.08f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.2f, 0.72f, 0.38f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.09f, 0.2f, 0.12f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.28f, 0.82f, 0.48f, 0.95f),

                SliderBg = new UnityEngine.Color(0.06f, 0.13f, 0.08f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.2f, 0.72f, 0.38f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.32f, 0.85f, 0.5f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.14f, 0.55f, 0.28f, 0.95f),

                Separator = new UnityEngine.Color(0.25f, 0.7f, 0.4f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.025f, 0.055f, 0.03f, 0.98f),
                TooltipText = new UnityEngine.Color(0.88f, 1f, 0.92f, 1f),

                SectionBg = new UnityEngine.Color(0.035f, 0.08f, 0.045f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.28f, 0.78f, 0.45f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.08f, 0.18f, 0.1f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.05f, 0.11f, 0.06f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.22f, 0.62f, 0.35f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.06f, 0.14f, 0.08f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.08f, 0.18f, 0.1f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.025f, 0.06f, 0.035f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.04f, 0.09f, 0.05f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.35f, 0.85f, 0.5f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.75f, 1f, 0.85f, 1f),
                LogoText = new UnityEngine.Color(0.92f, 1f, 0.95f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.7f, 0.88f, 0.78f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.9f, 1f, 0.94f, 1f),
                TextSecondary = new UnityEngine.Color(0.75f, 0.9f, 0.82f, 1f),

                DropdownBg = new UnityEngine.Color(0.055f, 0.12f, 0.07f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.09f, 0.2f, 0.12f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.11f, 0.24f, 0.14f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.3f, 0.8f, 0.45f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.38f, 0.9f, 0.55f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.04f, 0.09f, 0.05f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.2f, 0.65f, 0.35f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.2f, 0.65f, 0.35f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.3f, 0.8f, 0.45f, 1f),
                GlowSecondary = new UnityEngine.Color(0.5f, 0.95f, 0.65f, 1f)
            };
        }

        private static ThemeColors GetMidnightGoldTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.06f, 0.05f, 0.04f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.1f, 0.09f, 0.07f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.14f, 0.12f, 0.09f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.2f, 0.17f, 0.12f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.26f, 0.22f, 0.15f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.85f, 0.68f, 0.25f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.72f, 0.55f, 0.18f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.95f, 0.78f, 0.35f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.82f, 0.65f, 0.25f, 0.98f),
                TabBorder = new UnityEngine.Color(0.9f, 0.72f, 0.3f, 0.6f),
                TabText = new UnityEngine.Color(0.92f, 0.88f, 0.78f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.98f, 0.92f, 1f),
                TabTextActive = new UnityEngine.Color(0.1f, 0.08f, 0.05f, 1f),

                BoxBg = new UnityEngine.Color(0.08f, 0.07f, 0.05f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.7f, 0.55f, 0.2f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.85f, 0.68f, 0.28f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.55f, 0.42f, 0.15f, 0.95f),
                ButtonText = new UnityEngine.Color(0.1f, 0.08f, 0.05f, 1f),

                CheckboxBg = new UnityEngine.Color(0.12f, 0.1f, 0.07f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.85f, 0.68f, 0.25f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.18f, 0.15f, 0.1f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.95f, 0.78f, 0.35f, 0.95f),

                SliderBg = new UnityEngine.Color(0.12f, 0.1f, 0.07f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.85f, 0.68f, 0.25f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.82f, 0.4f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.68f, 0.52f, 0.18f, 0.95f),

                Separator = new UnityEngine.Color(0.8f, 0.65f, 0.28f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.05f, 0.04f, 0.03f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.95f, 0.85f, 1f),

                SectionBg = new UnityEngine.Color(0.07f, 0.06f, 0.045f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.88f, 0.7f, 0.28f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.15f, 0.13f, 0.09f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.09f, 0.08f, 0.06f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.7f, 0.55f, 0.22f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.12f, 0.1f, 0.07f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.16f, 0.13f, 0.09f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.05f, 0.04f, 0.03f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.08f, 0.06f, 0.04f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.95f, 0.78f, 0.35f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.95f, 0.8f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.98f, 0.92f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.85f, 0.78f, 0.65f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.98f, 0.95f, 0.88f, 1f),
                TextSecondary = new UnityEngine.Color(0.88f, 0.82f, 0.72f, 1f),

                DropdownBg = new UnityEngine.Color(0.1f, 0.085f, 0.06f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.16f, 0.14f, 0.1f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.2f, 0.17f, 0.12f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.9f, 0.72f, 0.3f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.82f, 0.38f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.08f, 0.07f, 0.05f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.75f, 0.58f, 0.2f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.75f, 0.58f, 0.2f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.9f, 0.72f, 0.3f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.88f, 0.55f, 1f)
            };
        }

        private static ThemeColors GetArcticFrostTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.1f, 0.12f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.12f, 0.15f, 0.18f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.16f, 0.2f, 0.24f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.22f, 0.28f, 0.35f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.28f, 0.35f, 0.42f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.7f, 0.85f, 0.95f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.55f, 0.72f, 0.85f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.8f, 0.92f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.65f, 0.8f, 0.92f, 0.98f),
                TabBorder = new UnityEngine.Color(0.75f, 0.88f, 0.98f, 0.6f),
                TabText = new UnityEngine.Color(0.85f, 0.9f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(0.95f, 0.98f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(0.1f, 0.12f, 0.15f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.12f, 0.16f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.5f, 0.65f, 0.78f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.65f, 0.78f, 0.9f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.4f, 0.52f, 0.65f, 0.95f),
                ButtonText = new UnityEngine.Color(0.1f, 0.12f, 0.15f, 1f),

                CheckboxBg = new UnityEngine.Color(0.14f, 0.18f, 0.22f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.6f, 0.78f, 0.9f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.2f, 0.25f, 0.32f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.72f, 0.88f, 0.98f, 0.95f),

                SliderBg = new UnityEngine.Color(0.14f, 0.18f, 0.22f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.6f, 0.78f, 0.9f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.75f, 0.9f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.48f, 0.62f, 0.75f, 0.95f),

                Separator = new UnityEngine.Color(0.6f, 0.75f, 0.88f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.08f, 0.1f, 0.98f),
                TooltipText = new UnityEngine.Color(0.9f, 0.95f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.11f, 0.14f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.65f, 0.82f, 0.95f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.16f, 0.2f, 0.26f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.1f, 0.13f, 0.17f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.55f, 0.7f, 0.85f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.14f, 0.17f, 0.22f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.18f, 0.22f, 0.28f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.08f, 0.1f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.09f, 0.11f, 0.14f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.7f, 0.88f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.9f, 0.96f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.95f, 0.98f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.75f, 0.82f, 0.9f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.92f, 0.96f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.78f, 0.85f, 0.92f, 1f),

                DropdownBg = new UnityEngine.Color(0.12f, 0.15f, 0.19f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.18f, 0.23f, 0.3f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.22f, 0.28f, 0.35f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.75f, 0.88f, 0.98f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.8f, 0.92f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.1f, 0.12f, 0.16f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.55f, 0.7f, 0.85f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.55f, 0.7f, 0.85f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.7f, 0.88f, 1f, 1f),
                GlowSecondary = new UnityEngine.Color(0.85f, 0.95f, 1f, 1f)
            };
        }

        private static ThemeColors GetSunsetBlazeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.05f, 0.08f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.15f, 0.08f, 0.1f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.2f, 0.1f, 0.12f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.3f, 0.15f, 0.18f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.38f, 0.18f, 0.22f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.95f, 0.45f, 0.25f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.85f, 0.35f, 0.4f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 0.55f, 0.35f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.92f, 0.42f, 0.48f, 0.98f),
                TabBorder = new UnityEngine.Color(0.95f, 0.5f, 0.35f, 0.6f),
                TabText = new UnityEngine.Color(0.95f, 0.85f, 0.82f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.92f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.06f, 0.08f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.85f, 0.38f, 0.3f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.95f, 0.48f, 0.38f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.7f, 0.28f, 0.22f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.18f, 0.09f, 0.1f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.95f, 0.45f, 0.3f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.26f, 0.13f, 0.15f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.55f, 0.4f, 0.95f),

                SliderBg = new UnityEngine.Color(0.18f, 0.09f, 0.1f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.95f, 0.45f, 0.3f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.58f, 0.42f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.78f, 0.35f, 0.22f, 0.95f),

                Separator = new UnityEngine.Color(0.9f, 0.45f, 0.35f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.04f, 0.05f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.95f, 0.9f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.055f, 0.07f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.95f, 0.5f, 0.35f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.22f, 0.11f, 0.13f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.14f, 0.07f, 0.09f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.85f, 0.4f, 0.3f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.18f, 0.09f, 0.1f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.24f, 0.12f, 0.14f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.04f, 0.05f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.12f, 0.06f, 0.07f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.55f, 0.38f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.85f, 0.75f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 0.94f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.92f, 0.78f, 0.72f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.95f, 0.92f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.82f, 0.78f, 1f),

                DropdownBg = new UnityEngine.Color(0.16f, 0.08f, 0.09f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.24f, 0.12f, 0.14f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.3f, 0.15f, 0.17f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.95f, 0.5f, 0.35f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.6f, 0.42f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.12f, 0.06f, 0.07f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.88f, 0.4f, 0.28f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.88f, 0.4f, 0.28f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.95f, 0.5f, 0.35f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.7f, 0.55f, 1f)
            };
        }

        private static ThemeColors GetRoyalVelvetTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.04f, 0.1f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.12f, 0.06f, 0.15f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.16f, 0.08f, 0.2f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.24f, 0.12f, 0.3f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.3f, 0.15f, 0.38f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.65f, 0.25f, 0.8f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.5f, 0.18f, 0.65f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.75f, 0.35f, 0.9f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.6f, 0.25f, 0.75f, 0.98f),
                TabBorder = new UnityEngine.Color(0.7f, 0.35f, 0.9f, 0.6f),
                TabText = new UnityEngine.Color(0.9f, 0.82f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(0.98f, 0.92f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.05f, 0.12f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.55f, 0.22f, 0.7f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.68f, 0.32f, 0.85f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.42f, 0.15f, 0.55f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.14f, 0.07f, 0.18f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.65f, 0.28f, 0.82f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.2f, 0.1f, 0.26f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.78f, 0.38f, 0.95f, 0.95f),

                SliderBg = new UnityEngine.Color(0.14f, 0.07f, 0.18f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.65f, 0.28f, 0.82f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.8f, 0.42f, 0.98f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.5f, 0.2f, 0.65f, 0.95f),

                Separator = new UnityEngine.Color(0.6f, 0.3f, 0.78f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.03f, 0.08f, 0.98f),
                TooltipText = new UnityEngine.Color(0.95f, 0.88f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.045f, 0.11f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.7f, 0.35f, 0.88f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.18f, 0.09f, 0.24f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.11f, 0.055f, 0.14f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.55f, 0.25f, 0.72f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.14f, 0.07f, 0.18f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.18f, 0.09f, 0.24f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.03f, 0.08f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.09f, 0.045f, 0.12f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.78f, 0.4f, 0.95f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.92f, 0.8f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.96f, 0.92f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.82f, 0.72f, 0.9f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.95f, 0.9f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.85f, 0.78f, 0.92f, 1f),

                DropdownBg = new UnityEngine.Color(0.12f, 0.06f, 0.16f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.2f, 0.1f, 0.26f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.25f, 0.12f, 0.32f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.7f, 0.35f, 0.9f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.82f, 0.45f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.1f, 0.05f, 0.13f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.6f, 0.28f, 0.78f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.6f, 0.28f, 0.78f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.7f, 0.35f, 0.9f, 1f),
                GlowSecondary = new UnityEngine.Color(0.88f, 0.6f, 1f, 1f)
            };
        }

        private static ThemeColors GetToxicLimeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.04f, 0.06f, 0.03f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.07f, 0.1f, 0.05f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.1f, 0.14f, 0.07f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.15f, 0.22f, 0.1f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.2f, 0.3f, 0.14f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.6f, 0.9f, 0.15f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.45f, 0.75f, 0.1f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.72f, 1f, 0.22f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.55f, 0.85f, 0.15f, 0.98f),
                TabBorder = new UnityEngine.Color(0.7f, 0.95f, 0.25f, 0.6f),
                TabText = new UnityEngine.Color(0.85f, 0.92f, 0.78f, 1f),
                TabTextHover = new UnityEngine.Color(0.95f, 1f, 0.9f, 1f),
                TabTextActive = new UnityEngine.Color(0.08f, 0.1f, 0.05f, 1f),

                BoxBg = new UnityEngine.Color(0.05f, 0.08f, 0.04f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.5f, 0.75f, 0.12f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.62f, 0.88f, 0.18f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.38f, 0.58f, 0.08f, 0.95f),
                ButtonText = new UnityEngine.Color(0.08f, 0.1f, 0.05f, 1f),

                CheckboxBg = new UnityEngine.Color(0.08f, 0.12f, 0.06f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.58f, 0.88f, 0.15f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.12f, 0.18f, 0.08f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.7f, 0.98f, 0.22f, 0.95f),

                SliderBg = new UnityEngine.Color(0.08f, 0.12f, 0.06f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.58f, 0.88f, 0.15f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.72f, 1f, 0.25f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.45f, 0.68f, 0.1f, 0.95f),

                Separator = new UnityEngine.Color(0.55f, 0.82f, 0.18f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.03f, 0.05f, 0.025f, 0.98f),
                TooltipText = new UnityEngine.Color(0.92f, 1f, 0.85f, 1f),

                SectionBg = new UnityEngine.Color(0.045f, 0.07f, 0.035f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.65f, 0.92f, 0.2f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.1f, 0.16f, 0.07f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.06f, 0.1f, 0.045f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.5f, 0.75f, 0.15f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.08f, 0.12f, 0.055f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.1f, 0.16f, 0.07f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.035f, 0.055f, 0.025f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.05f, 0.08f, 0.04f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.72f, 0.98f, 0.25f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.88f, 1f, 0.75f, 1f),
                LogoText = new UnityEngine.Color(0.95f, 1f, 0.9f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.78f, 0.88f, 0.68f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.92f, 0.98f, 0.88f, 1f),
                TextSecondary = new UnityEngine.Color(0.8f, 0.9f, 0.72f, 1f),

                DropdownBg = new UnityEngine.Color(0.07f, 0.1f, 0.05f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.12f, 0.18f, 0.08f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.16f, 0.24f, 0.1f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.7f, 0.95f, 0.25f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.78f, 1f, 0.32f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.05f, 0.08f, 0.04f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.52f, 0.78f, 0.15f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.52f, 0.78f, 0.15f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.68f, 0.95f, 0.22f, 1f),
                GlowSecondary = new UnityEngine.Color(0.85f, 1f, 0.55f, 1f)
            };
        }

        private static ThemeColors GetNeonSakuraTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.09f, 0.04f, 0.07f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.14f, 0.06f, 0.1f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.18f, 0.08f, 0.13f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.28f, 0.12f, 0.2f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.35f, 0.15f, 0.25f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.95f, 0.4f, 0.65f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.85f, 0.3f, 0.55f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 0.5f, 0.75f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.92f, 0.38f, 0.62f, 0.98f),
                TabBorder = new UnityEngine.Color(0.95f, 0.45f, 0.7f, 0.6f),
                TabText = new UnityEngine.Color(0.95f, 0.85f, 0.9f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.98f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.11f, 0.05f, 0.08f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.85f, 0.35f, 0.55f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.95f, 0.45f, 0.65f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.7f, 0.25f, 0.45f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.16f, 0.07f, 0.11f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.95f, 0.4f, 0.6f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.24f, 0.1f, 0.16f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.5f, 0.7f, 0.95f),

                SliderBg = new UnityEngine.Color(0.16f, 0.07f, 0.11f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.95f, 0.4f, 0.6f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.55f, 0.75f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.78f, 0.3f, 0.48f, 0.95f),

                Separator = new UnityEngine.Color(0.9f, 0.4f, 0.6f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.07f, 0.03f, 0.05f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.92f, 0.96f, 1f),

                SectionBg = new UnityEngine.Color(0.1f, 0.045f, 0.07f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.95f, 0.45f, 0.68f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.2f, 0.09f, 0.14f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.12f, 0.055f, 0.085f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.85f, 0.35f, 0.55f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.16f, 0.07f, 0.11f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.22f, 0.09f, 0.15f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.07f, 0.03f, 0.05f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.11f, 0.05f, 0.08f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.5f, 0.72f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.85f, 0.92f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 0.98f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.92f, 0.78f, 0.85f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.95f, 0.97f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.82f, 0.87f, 1f),

                DropdownBg = new UnityEngine.Color(0.14f, 0.06f, 0.1f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.22f, 0.1f, 0.15f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.28f, 0.12f, 0.19f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.95f, 0.45f, 0.7f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.55f, 0.78f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.11f, 0.05f, 0.08f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.9f, 0.38f, 0.58f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.9f, 0.38f, 0.58f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.95f, 0.45f, 0.7f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.7f, 0.85f, 1f)
            };
        }

        private static ThemeColors GetObsidianSteelTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.08f, 0.09f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.12f, 0.12f, 0.14f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.16f, 0.16f, 0.18f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.22f, 0.22f, 0.26f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.28f, 0.28f, 0.32f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.55f, 0.58f, 0.65f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.42f, 0.45f, 0.52f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.65f, 0.68f, 0.75f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.52f, 0.55f, 0.62f, 0.98f),
                TabBorder = new UnityEngine.Color(0.6f, 0.62f, 0.7f, 0.6f),
                TabText = new UnityEngine.Color(0.85f, 0.85f, 0.88f, 1f),
                TabTextHover = new UnityEngine.Color(0.95f, 0.95f, 0.98f, 1f),
                TabTextActive = new UnityEngine.Color(0.1f, 0.1f, 0.12f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.1f, 0.11f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.4f, 0.42f, 0.48f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.52f, 0.55f, 0.62f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.32f, 0.34f, 0.4f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.14f, 0.14f, 0.16f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.55f, 0.58f, 0.65f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.2f, 0.2f, 0.23f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.65f, 0.68f, 0.75f, 0.95f),

                SliderBg = new UnityEngine.Color(0.14f, 0.14f, 0.16f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.55f, 0.58f, 0.65f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.68f, 0.7f, 0.78f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.42f, 0.45f, 0.52f, 0.95f),

                Separator = new UnityEngine.Color(0.5f, 0.52f, 0.58f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.06f, 0.07f, 0.98f),
                TooltipText = new UnityEngine.Color(0.92f, 0.92f, 0.95f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.09f, 0.1f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.58f, 0.6f, 0.68f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.16f, 0.16f, 0.19f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.1f, 0.1f, 0.12f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.48f, 0.5f, 0.56f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.14f, 0.14f, 0.16f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.18f, 0.18f, 0.21f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.06f, 0.07f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.09f, 0.09f, 0.1f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.65f, 0.68f, 0.78f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.88f, 0.89f, 0.94f, 1f),
                LogoText = new UnityEngine.Color(0.95f, 0.95f, 0.97f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.75f, 0.76f, 0.82f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.93f, 0.93f, 0.96f, 1f),
                TextSecondary = new UnityEngine.Color(0.78f, 0.78f, 0.83f, 1f),

                DropdownBg = new UnityEngine.Color(0.12f, 0.12f, 0.14f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.18f, 0.18f, 0.21f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.22f, 0.22f, 0.26f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.6f, 0.62f, 0.7f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.7f, 0.72f, 0.82f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.1f, 0.1f, 0.11f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.5f, 0.52f, 0.6f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.5f, 0.52f, 0.6f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.6f, 0.62f, 0.72f, 1f),
                GlowSecondary = new UnityEngine.Color(0.78f, 0.8f, 0.88f, 1f)
            };
        }

        private static ThemeColors GetElectricVioletTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.05f, 0.02f, 0.1f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.08f, 0.04f, 0.16f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.12f, 0.06f, 0.22f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.18f, 0.1f, 0.32f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.24f, 0.14f, 0.42f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.55f, 0.2f, 1f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.4f, 0.1f, 0.85f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.65f, 0.32f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.5f, 0.2f, 0.92f, 0.98f),
                TabBorder = new UnityEngine.Color(0.6f, 0.3f, 1f, 0.7f),
                TabText = new UnityEngine.Color(0.88f, 0.82f, 1f, 1f),
                TabTextHover = new UnityEngine.Color(0.95f, 0.92f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.06f, 0.03f, 0.12f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.45f, 0.15f, 0.85f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.58f, 0.25f, 0.95f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.35f, 0.1f, 0.7f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.1f, 0.05f, 0.2f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.55f, 0.22f, 1f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.15f, 0.08f, 0.3f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.68f, 0.35f, 1f, 0.95f),

                SliderBg = new UnityEngine.Color(0.1f, 0.05f, 0.2f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.55f, 0.22f, 1f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.7f, 0.4f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.42f, 0.15f, 0.8f, 0.95f),

                Separator = new UnityEngine.Color(0.5f, 0.25f, 0.9f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.04f, 0.02f, 0.08f, 0.98f),
                TooltipText = new UnityEngine.Color(0.92f, 0.88f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.055f, 0.025f, 0.11f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.58f, 0.28f, 1f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.14f, 0.07f, 0.28f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.08f, 0.04f, 0.16f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.48f, 0.2f, 0.85f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.1f, 0.05f, 0.2f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.14f, 0.07f, 0.28f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.04f, 0.02f, 0.08f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.06f, 0.03f, 0.12f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.65f, 0.35f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.88f, 0.78f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.95f, 0.92f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.8f, 0.72f, 0.95f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.94f, 0.9f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.82f, 0.75f, 0.95f, 1f),

                DropdownBg = new UnityEngine.Color(0.08f, 0.04f, 0.18f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.14f, 0.08f, 0.28f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.18f, 0.1f, 0.35f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.6f, 0.3f, 1f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.7f, 0.4f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.06f, 0.03f, 0.12f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.5f, 0.22f, 0.9f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.5f, 0.22f, 0.9f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.58f, 0.28f, 1f, 1f),
                GlowSecondary = new UnityEngine.Color(0.78f, 0.55f, 1f, 1f)
            };
        }

        private static ThemeColors GetCopperBronzeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.06f, 0.04f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.14f, 0.1f, 0.07f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.18f, 0.13f, 0.09f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.26f, 0.19f, 0.12f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.32f, 0.24f, 0.15f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.8f, 0.5f, 0.25f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.65f, 0.38f, 0.18f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.9f, 0.6f, 0.32f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.75f, 0.48f, 0.25f, 0.98f),
                TabBorder = new UnityEngine.Color(0.85f, 0.55f, 0.3f, 0.6f),
                TabText = new UnityEngine.Color(0.95f, 0.88f, 0.78f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.88f, 1f),
                TabTextActive = new UnityEngine.Color(0.1f, 0.07f, 0.04f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.07f, 0.05f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.7f, 0.42f, 0.2f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.82f, 0.52f, 0.28f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.55f, 0.32f, 0.15f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.16f, 0.11f, 0.07f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.8f, 0.48f, 0.22f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.24f, 0.16f, 0.1f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.9f, 0.58f, 0.3f, 0.95f),

                SliderBg = new UnityEngine.Color(0.16f, 0.11f, 0.07f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.8f, 0.48f, 0.22f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.92f, 0.62f, 0.35f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.62f, 0.38f, 0.18f, 0.95f),

                Separator = new UnityEngine.Color(0.75f, 0.48f, 0.25f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.04f, 0.03f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.94f, 0.85f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.065f, 0.045f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.82f, 0.52f, 0.28f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.2f, 0.14f, 0.09f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.12f, 0.085f, 0.055f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.68f, 0.42f, 0.22f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.16f, 0.11f, 0.07f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.22f, 0.15f, 0.09f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.04f, 0.03f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.1f, 0.07f, 0.045f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.9f, 0.58f, 0.32f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.88f, 0.72f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 0.9f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.88f, 0.78f, 0.65f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.98f, 0.94f, 0.88f, 1f),
                TextSecondary = new UnityEngine.Color(0.88f, 0.8f, 0.7f, 1f),

                DropdownBg = new UnityEngine.Color(0.14f, 0.1f, 0.065f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.22f, 0.15f, 0.1f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.28f, 0.19f, 0.12f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.85f, 0.55f, 0.3f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.95f, 0.62f, 0.35f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.1f, 0.07f, 0.045f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.75f, 0.45f, 0.22f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.75f, 0.45f, 0.22f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.85f, 0.52f, 0.28f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.72f, 0.48f, 1f)
            };
        }

        private static ThemeColors GetMatrixGreenTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.01f, 0.04f, 0.02f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.02f, 0.08f, 0.03f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.03f, 0.12f, 0.05f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.04f, 0.18f, 0.07f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.05f, 0.24f, 0.1f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0f, 0.9f, 0.25f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0f, 0.7f, 0.18f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.15f, 1f, 0.4f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.08f, 0.85f, 0.28f, 0.98f),
                TabBorder = new UnityEngine.Color(0f, 1f, 0.35f, 0.7f),
                TabText = new UnityEngine.Color(0.7f, 1f, 0.75f, 1f),
                TabTextHover = new UnityEngine.Color(0.85f, 1f, 0.88f, 1f),
                TabTextActive = new UnityEngine.Color(0.01f, 0.06f, 0.02f, 1f),

                BoxBg = new UnityEngine.Color(0.015f, 0.05f, 0.025f, 0.92f),

                ButtonBg = new UnityEngine.Color(0f, 0.65f, 0.18f, 0.9f),
                ButtonHover = new UnityEngine.Color(0f, 0.82f, 0.25f, 0.95f),
                ButtonActive = new UnityEngine.Color(0f, 0.5f, 0.12f, 0.95f),
                ButtonText = new UnityEngine.Color(0.01f, 0.06f, 0.02f, 1f),

                CheckboxBg = new UnityEngine.Color(0.02f, 0.1f, 0.04f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0f, 0.9f, 0.28f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.03f, 0.15f, 0.06f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.2f, 1f, 0.42f, 0.95f),

                SliderBg = new UnityEngine.Color(0.02f, 0.1f, 0.04f, 0.9f),
                SliderThumb = new UnityEngine.Color(0f, 0.9f, 0.28f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.25f, 1f, 0.48f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0f, 0.68f, 0.2f, 0.95f),

                Separator = new UnityEngine.Color(0f, 0.8f, 0.25f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.008f, 0.03f, 0.015f, 0.98f),
                TooltipText = new UnityEngine.Color(0.75f, 1f, 0.8f, 1f),

                SectionBg = new UnityEngine.Color(0.012f, 0.045f, 0.02f, 0.92f),
                SectionAccent = new UnityEngine.Color(0f, 0.95f, 0.32f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.03f, 0.12f, 0.05f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.015f, 0.06f, 0.025f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0f, 0.72f, 0.22f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.02f, 0.1f, 0.04f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.03f, 0.14f, 0.055f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.008f, 0.03f, 0.012f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.015f, 0.055f, 0.022f, 0.98f),
                LogoAccent = new UnityEngine.Color(0f, 1f, 0.38f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.65f, 1f, 0.75f, 1f),
                LogoText = new UnityEngine.Color(0.8f, 1f, 0.85f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.55f, 0.85f, 0.62f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.78f, 1f, 0.82f, 1f),
                TextSecondary = new UnityEngine.Color(0.55f, 0.88f, 0.62f, 1f),

                DropdownBg = new UnityEngine.Color(0.02f, 0.08f, 0.035f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.03f, 0.14f, 0.055f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.04f, 0.18f, 0.07f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0f, 1f, 0.35f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0f, 1f, 0.42f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.015f, 0.055f, 0.025f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0f, 0.82f, 0.28f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0f, 0.82f, 0.28f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0f, 1f, 0.35f, 1f),
                GlowSecondary = new UnityEngine.Color(0.4f, 1f, 0.58f, 1f)
            };
        }

        private static ThemeColors GetBubblegumPopTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.12f, 0.08f, 0.12f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.18f, 0.12f, 0.18f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.24f, 0.16f, 0.24f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.35f, 0.22f, 0.35f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.42f, 0.28f, 0.42f, 0.95f),
                TabActiveTop = new UnityEngine.Color(1f, 0.45f, 0.85f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.85f, 0.35f, 0.95f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 0.58f, 0.92f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.92f, 0.45f, 1f, 0.98f),
                TabBorder = new UnityEngine.Color(1f, 0.5f, 0.9f, 0.7f),
                TabText = new UnityEngine.Color(1f, 0.88f, 0.98f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(0.15f, 0.08f, 0.15f, 1f),

                BoxBg = new UnityEngine.Color(0.15f, 0.1f, 0.15f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.9f, 0.38f, 0.78f, 0.9f),
                ButtonHover = new UnityEngine.Color(1f, 0.5f, 0.88f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.75f, 0.28f, 0.65f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.22f, 0.14f, 0.22f, 0.9f),
                CheckboxOn = new UnityEngine.Color(1f, 0.45f, 0.88f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.32f, 0.2f, 0.32f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.6f, 0.95f, 0.95f),

                SliderBg = new UnityEngine.Color(0.22f, 0.14f, 0.22f, 0.9f),
                SliderThumb = new UnityEngine.Color(1f, 0.45f, 0.88f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.65f, 0.95f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.82f, 0.35f, 0.72f, 0.95f),

                Separator = new UnityEngine.Color(0.95f, 0.45f, 0.85f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.1f, 0.06f, 0.1f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.92f, 0.98f, 1f),

                SectionBg = new UnityEngine.Color(0.13f, 0.09f, 0.13f, 0.92f),
                SectionAccent = new UnityEngine.Color(1f, 0.5f, 0.9f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.28f, 0.18f, 0.28f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.17f, 0.11f, 0.17f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.88f, 0.4f, 0.78f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.22f, 0.14f, 0.22f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.28f, 0.18f, 0.28f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.1f, 0.06f, 0.1f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.14f, 0.09f, 0.14f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.58f, 0.92f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.88f, 0.98f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.92f, 0.78f, 0.9f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.95f, 0.99f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.82f, 0.9f, 1f),

                DropdownBg = new UnityEngine.Color(0.18f, 0.12f, 0.18f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.28f, 0.18f, 0.28f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.35f, 0.22f, 0.35f, 0.98f),
                DropdownBorder = new UnityEngine.Color(1f, 0.5f, 0.9f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.62f, 0.95f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.14f, 0.09f, 0.14f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.92f, 0.4f, 0.82f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.92f, 0.4f, 0.82f, 0.4f),

                GlowPrimary = new UnityEngine.Color(1f, 0.5f, 0.9f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.75f, 0.98f, 1f)
            };
        }

        private static ThemeColors GetMochaCoffeeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.07f, 0.05f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.16f, 0.11f, 0.08f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.21f, 0.15f, 0.1f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.3f, 0.22f, 0.15f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.38f, 0.28f, 0.19f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.72f, 0.52f, 0.35f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.58f, 0.4f, 0.25f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.82f, 0.62f, 0.42f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.68f, 0.5f, 0.32f, 0.98f),
                TabBorder = new UnityEngine.Color(0.75f, 0.55f, 0.38f, 0.6f),
                TabText = new UnityEngine.Color(0.95f, 0.9f, 0.82f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.96f, 0.9f, 1f),
                TabTextActive = new UnityEngine.Color(0.12f, 0.08f, 0.05f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.085f, 0.06f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.6f, 0.42f, 0.28f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.72f, 0.52f, 0.35f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.48f, 0.32f, 0.2f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.18f, 0.13f, 0.09f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.72f, 0.5f, 0.32f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.26f, 0.19f, 0.12f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.82f, 0.6f, 0.4f, 0.95f),

                SliderBg = new UnityEngine.Color(0.18f, 0.13f, 0.09f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.72f, 0.5f, 0.32f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.85f, 0.65f, 0.45f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.55f, 0.38f, 0.24f, 0.95f),

                Separator = new UnityEngine.Color(0.68f, 0.48f, 0.32f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.055f, 0.04f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.95f, 0.88f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.078f, 0.055f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.75f, 0.55f, 0.38f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.22f, 0.16f, 0.11f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.14f, 0.1f, 0.07f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.62f, 0.44f, 0.28f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.18f, 0.13f, 0.09f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.24f, 0.17f, 0.11f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.055f, 0.04f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.11f, 0.078f, 0.055f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.85f, 0.62f, 0.42f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.9f, 0.78f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.97f, 0.92f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.88f, 0.8f, 0.68f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.98f, 0.95f, 0.9f, 1f),
                TextSecondary = new UnityEngine.Color(0.88f, 0.82f, 0.72f, 1f),

                DropdownBg = new UnityEngine.Color(0.16f, 0.11f, 0.078f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.24f, 0.17f, 0.12f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.3f, 0.22f, 0.15f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.75f, 0.55f, 0.38f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.88f, 0.65f, 0.45f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.11f, 0.078f, 0.055f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.68f, 0.48f, 0.3f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.68f, 0.48f, 0.3f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.75f, 0.55f, 0.38f, 1f),
                GlowSecondary = new UnityEngine.Color(0.95f, 0.78f, 0.58f, 1f)
            };
        }

        private static ThemeColors GetAuroraBorealisTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.02f, 0.06f, 0.1f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.04f, 0.1f, 0.16f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.06f, 0.14f, 0.22f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.08f, 0.2f, 0.3f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.1f, 0.26f, 0.38f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.2f, 0.9f, 0.7f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.3f, 0.75f, 0.9f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.35f, 1f, 0.8f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.42f, 0.85f, 1f, 0.98f),
                TabBorder = new UnityEngine.Color(0.3f, 0.95f, 0.8f, 0.7f),
                TabText = new UnityEngine.Color(0.82f, 0.98f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(0.92f, 1f, 0.98f, 1f),
                TabTextActive = new UnityEngine.Color(0.02f, 0.08f, 0.1f, 1f),

                BoxBg = new UnityEngine.Color(0.03f, 0.08f, 0.12f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.15f, 0.7f, 0.58f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.22f, 0.85f, 0.7f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.1f, 0.55f, 0.45f, 0.95f),
                ButtonText = new UnityEngine.Color(0.02f, 0.08f, 0.1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.05f, 0.12f, 0.18f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.22f, 0.88f, 0.72f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.07f, 0.18f, 0.26f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.35f, 1f, 0.82f, 0.95f),

                SliderBg = new UnityEngine.Color(0.05f, 0.12f, 0.18f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.22f, 0.88f, 0.72f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.38f, 1f, 0.85f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.15f, 0.68f, 0.55f, 0.95f),

                Separator = new UnityEngine.Color(0.25f, 0.85f, 0.7f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.015f, 0.045f, 0.07f, 0.98f),
                TooltipText = new UnityEngine.Color(0.85f, 1f, 0.96f, 1f),

                SectionBg = new UnityEngine.Color(0.025f, 0.07f, 0.11f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.28f, 0.92f, 0.78f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.06f, 0.16f, 0.24f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.035f, 0.095f, 0.14f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.2f, 0.78f, 0.65f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.05f, 0.12f, 0.18f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.06f, 0.16f, 0.24f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.015f, 0.045f, 0.07f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.025f, 0.07f, 0.11f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.35f, 1f, 0.85f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.75f, 1f, 0.95f, 1f),
                LogoText = new UnityEngine.Color(0.9f, 1f, 0.98f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.7f, 0.92f, 0.88f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.88f, 1f, 0.97f, 1f),
                TextSecondary = new UnityEngine.Color(0.72f, 0.92f, 0.88f, 1f),

                DropdownBg = new UnityEngine.Color(0.04f, 0.1f, 0.15f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.06f, 0.16f, 0.24f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.08f, 0.2f, 0.3f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.3f, 0.95f, 0.8f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.38f, 1f, 0.88f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.03f, 0.08f, 0.12f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.2f, 0.82f, 0.68f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.2f, 0.82f, 0.68f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.28f, 0.95f, 0.8f, 1f),
                GlowSecondary = new UnityEngine.Color(0.5f, 1f, 0.9f, 1f)
            };
        }

        private static ThemeColors GetLavenderDreamTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.08f, 0.14f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.15f, 0.12f, 0.2f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.2f, 0.16f, 0.26f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.28f, 0.22f, 0.36f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.35f, 0.28f, 0.44f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.72f, 0.58f, 0.92f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.6f, 0.48f, 0.82f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.82f, 0.68f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.7f, 0.58f, 0.92f, 0.98f),
                TabBorder = new UnityEngine.Color(0.75f, 0.6f, 0.95f, 0.6f),
                TabText = new UnityEngine.Color(0.92f, 0.88f, 1f, 1f),
                TabTextHover = new UnityEngine.Color(0.98f, 0.95f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(0.12f, 0.1f, 0.16f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.1f, 0.17f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.6f, 0.48f, 0.8f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.72f, 0.58f, 0.92f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.48f, 0.38f, 0.68f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.17f, 0.14f, 0.24f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.7f, 0.55f, 0.9f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.24f, 0.2f, 0.32f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.8f, 0.65f, 1f, 0.95f),

                SliderBg = new UnityEngine.Color(0.17f, 0.14f, 0.24f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.7f, 0.55f, 0.9f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.82f, 0.68f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.55f, 0.44f, 0.75f, 0.95f),

                Separator = new UnityEngine.Color(0.68f, 0.55f, 0.88f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.06f, 0.11f, 0.98f),
                TooltipText = new UnityEngine.Color(0.95f, 0.92f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.09f, 0.15f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.75f, 0.6f, 0.95f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.22f, 0.18f, 0.3f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.14f, 0.11f, 0.19f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.62f, 0.5f, 0.82f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.17f, 0.14f, 0.24f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.22f, 0.18f, 0.3f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.06f, 0.11f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.11f, 0.09f, 0.15f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.8f, 0.65f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.94f, 0.9f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.97f, 0.95f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.85f, 0.78f, 0.95f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.96f, 0.93f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.85f, 0.8f, 0.95f, 1f),

                DropdownBg = new UnityEngine.Color(0.15f, 0.12f, 0.2f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.22f, 0.18f, 0.3f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.28f, 0.22f, 0.36f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.75f, 0.6f, 0.95f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.85f, 0.7f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.11f, 0.09f, 0.15f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.65f, 0.52f, 0.85f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.65f, 0.52f, 0.85f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.75f, 0.6f, 0.95f, 1f),
                GlowSecondary = new UnityEngine.Color(0.9f, 0.8f, 1f, 1f)
            };
        }

        private static ThemeColors GetMidnightCherryTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.03f, 0.05f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.16f, 0.05f, 0.08f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.22f, 0.07f, 0.1f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.32f, 0.1f, 0.15f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.4f, 0.12f, 0.18f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.9f, 0.15f, 0.3f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.75f, 0.1f, 0.22f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 0.25f, 0.4f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.85f, 0.18f, 0.32f, 0.98f),
                TabBorder = new UnityEngine.Color(0.95f, 0.2f, 0.38f, 0.6f),
                TabText = new UnityEngine.Color(1f, 0.88f, 0.9f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.96f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.04f, 0.06f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.78f, 0.12f, 0.25f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.9f, 0.2f, 0.35f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.62f, 0.08f, 0.18f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.2f, 0.06f, 0.1f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.9f, 0.18f, 0.32f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.28f, 0.1f, 0.14f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.28f, 0.42f, 0.95f),

                SliderBg = new UnityEngine.Color(0.2f, 0.06f, 0.1f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.9f, 0.18f, 0.32f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.32f, 0.48f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.72f, 0.12f, 0.24f, 0.95f),

                Separator = new UnityEngine.Color(0.85f, 0.18f, 0.32f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.025f, 0.04f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.92f, 0.94f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.035f, 0.055f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.92f, 0.22f, 0.38f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.24f, 0.08f, 0.12f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.15f, 0.05f, 0.075f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.78f, 0.15f, 0.28f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.2f, 0.06f, 0.1f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.26f, 0.08f, 0.13f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.025f, 0.04f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.12f, 0.04f, 0.06f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.3f, 0.45f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.8f, 0.85f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 0.97f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.95f, 0.78f, 0.82f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.95f, 0.96f, 1f),
                TextSecondary = new UnityEngine.Color(0.95f, 0.82f, 0.85f, 1f),

                DropdownBg = new UnityEngine.Color(0.16f, 0.05f, 0.08f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.26f, 0.08f, 0.13f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.32f, 0.1f, 0.16f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.95f, 0.2f, 0.38f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.32f, 0.5f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.12f, 0.04f, 0.06f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.85f, 0.15f, 0.3f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.85f, 0.15f, 0.3f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.95f, 0.2f, 0.38f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.5f, 0.62f, 1f)
            };
        }

        private static ThemeColors GetPlasmaOrangeTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.12f, 0.06f, 0.02f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.18f, 0.09f, 0.03f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.24f, 0.12f, 0.04f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.35f, 0.18f, 0.06f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.45f, 0.23f, 0.08f, 0.95f),
                TabActiveTop = new UnityEngine.Color(1f, 0.6f, 0.1f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(1f, 0.45f, 0.05f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 0.7f, 0.2f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(1f, 0.55f, 0.1f, 0.98f),
                TabBorder = new UnityEngine.Color(1f, 0.6f, 0.15f, 0.7f),
                TabText = new UnityEngine.Color(1f, 0.9f, 0.75f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.95f, 0.85f, 1f),
                TabTextActive = new UnityEngine.Color(0.15f, 0.08f, 0.02f, 1f),

                BoxBg = new UnityEngine.Color(0.14f, 0.07f, 0.025f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.9f, 0.5f, 0.1f, 0.9f),
                ButtonHover = new UnityEngine.Color(1f, 0.6f, 0.15f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.75f, 0.4f, 0.08f, 0.95f),
                ButtonText = new UnityEngine.Color(0.1f, 0.05f, 0.02f, 1f),

                CheckboxBg = new UnityEngine.Color(0.2f, 0.1f, 0.04f, 0.9f),
                CheckboxOn = new UnityEngine.Color(1f, 0.55f, 0.1f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.3f, 0.15f, 0.05f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.68f, 0.2f, 0.95f),

                SliderBg = new UnityEngine.Color(0.2f, 0.1f, 0.04f, 0.9f),
                SliderThumb = new UnityEngine.Color(1f, 0.55f, 0.1f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.7f, 0.25f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.85f, 0.45f, 0.08f, 0.95f),

                Separator = new UnityEngine.Color(1f, 0.55f, 0.15f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.1f, 0.05f, 0.02f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.92f, 0.8f, 1f),

                SectionBg = new UnityEngine.Color(0.13f, 0.065f, 0.022f, 0.92f),
                SectionAccent = new UnityEngine.Color(1f, 0.6f, 0.18f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.28f, 0.14f, 0.05f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.16f, 0.08f, 0.03f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.9f, 0.48f, 0.1f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.22f, 0.11f, 0.04f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.3f, 0.15f, 0.05f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.1f, 0.05f, 0.02f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.15f, 0.075f, 0.025f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.65f, 0.2f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.88f, 0.65f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.96f, 0.88f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.95f, 0.8f, 0.6f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.95f, 0.85f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.8f, 0.65f, 1f),

                DropdownBg = new UnityEngine.Color(0.18f, 0.09f, 0.03f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.28f, 0.14f, 0.05f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.35f, 0.18f, 0.06f, 0.98f),
                DropdownBorder = new UnityEngine.Color(1f, 0.6f, 0.15f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.7f, 0.25f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.14f, 0.07f, 0.025f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.95f, 0.5f, 0.1f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.95f, 0.5f, 0.1f, 0.4f),

                GlowPrimary = new UnityEngine.Color(1f, 0.6f, 0.15f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.8f, 0.4f, 1f)
            };
        }

        private static ThemeColors GetDeepSpaceTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.02f, 0.02f, 0.08f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.03f, 0.03f, 0.12f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.05f, 0.05f, 0.16f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.08f, 0.08f, 0.24f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.1f, 0.1f, 0.3f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.3f, 0.4f, 0.9f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.2f, 0.3f, 0.8f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.4f, 0.5f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.3f, 0.4f, 0.9f, 0.98f),
                TabBorder = new UnityEngine.Color(0.4f, 0.5f, 1f, 0.6f),
                TabText = new UnityEngine.Color(0.75f, 0.8f, 0.95f, 1f),
                TabTextHover = new UnityEngine.Color(0.88f, 0.9f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.025f, 0.025f, 0.1f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.25f, 0.32f, 0.75f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.35f, 0.42f, 0.88f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.18f, 0.24f, 0.6f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.04f, 0.04f, 0.14f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.35f, 0.45f, 0.95f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.06f, 0.06f, 0.2f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.45f, 0.55f, 1f, 0.95f),

                SliderBg = new UnityEngine.Color(0.04f, 0.04f, 0.14f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.35f, 0.45f, 0.95f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.5f, 0.6f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.25f, 0.35f, 0.8f, 0.95f),

                Separator = new UnityEngine.Color(0.35f, 0.45f, 0.9f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.015f, 0.015f, 0.06f, 0.98f),
                TooltipText = new UnityEngine.Color(0.85f, 0.88f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.022f, 0.022f, 0.09f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.4f, 0.5f, 1f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.06f, 0.06f, 0.2f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.03f, 0.03f, 0.12f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.3f, 0.4f, 0.85f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.04f, 0.04f, 0.15f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.06f, 0.06f, 0.2f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.015f, 0.015f, 0.06f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.025f, 0.025f, 0.1f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.5f, 0.6f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.8f, 0.85f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.9f, 0.92f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.7f, 0.75f, 0.92f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.88f, 0.9f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.7f, 0.75f, 0.9f, 1f),

                DropdownBg = new UnityEngine.Color(0.035f, 0.035f, 0.13f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.06f, 0.06f, 0.2f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.08f, 0.08f, 0.26f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.4f, 0.5f, 1f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.5f, 0.6f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.025f, 0.025f, 0.1f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.3f, 0.4f, 0.85f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.3f, 0.4f, 0.85f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.4f, 0.5f, 1f, 1f),
                GlowSecondary = new UnityEngine.Color(0.65f, 0.72f, 1f, 1f)
            };
        }

        private static ThemeColors GetMintFreshTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.04f, 0.1f, 0.09f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.06f, 0.15f, 0.13f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.08f, 0.2f, 0.17f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.1f, 0.28f, 0.24f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.12f, 0.35f, 0.3f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.3f, 0.9f, 0.75f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.2f, 0.8f, 0.65f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.4f, 0.95f, 0.82f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.3f, 0.88f, 0.72f, 0.98f),
                TabBorder = new UnityEngine.Color(0.4f, 0.95f, 0.8f, 0.6f),
                TabText = new UnityEngine.Color(0.8f, 0.98f, 0.92f, 1f),
                TabTextHover = new UnityEngine.Color(0.9f, 1f, 0.96f, 1f),
                TabTextActive = new UnityEngine.Color(0.05f, 0.12f, 0.1f, 1f),

                BoxBg = new UnityEngine.Color(0.05f, 0.12f, 0.1f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.25f, 0.75f, 0.62f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.32f, 0.85f, 0.72f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.18f, 0.62f, 0.5f, 0.95f),
                ButtonText = new UnityEngine.Color(0.05f, 0.12f, 0.1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.07f, 0.17f, 0.14f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.35f, 0.9f, 0.75f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.1f, 0.24f, 0.2f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.45f, 0.95f, 0.82f, 0.95f),

                SliderBg = new UnityEngine.Color(0.07f, 0.17f, 0.14f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.35f, 0.9f, 0.75f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.5f, 0.98f, 0.85f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.25f, 0.75f, 0.62f, 0.95f),

                Separator = new UnityEngine.Color(0.35f, 0.85f, 0.72f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.03f, 0.08f, 0.07f, 0.98f),
                TooltipText = new UnityEngine.Color(0.85f, 1f, 0.95f, 1f),

                SectionBg = new UnityEngine.Color(0.045f, 0.11f, 0.095f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.4f, 0.92f, 0.78f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.1f, 0.24f, 0.2f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.06f, 0.14f, 0.12f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.3f, 0.8f, 0.68f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.08f, 0.2f, 0.17f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.1f, 0.26f, 0.22f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.03f, 0.08f, 0.07f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.05f, 0.12f, 0.1f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.45f, 0.98f, 0.85f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.8f, 1f, 0.95f, 1f),
                LogoText = new UnityEngine.Color(0.92f, 1f, 0.97f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.7f, 0.92f, 0.85f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.9f, 1f, 0.96f, 1f),
                TextSecondary = new UnityEngine.Color(0.75f, 0.92f, 0.86f, 1f),

                DropdownBg = new UnityEngine.Color(0.06f, 0.15f, 0.13f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.1f, 0.24f, 0.2f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.12f, 0.3f, 0.25f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.4f, 0.95f, 0.8f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.5f, 1f, 0.88f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.05f, 0.12f, 0.1f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.3f, 0.82f, 0.68f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.3f, 0.82f, 0.68f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.4f, 0.95f, 0.8f, 1f),
                GlowSecondary = new UnityEngine.Color(0.65f, 1f, 0.9f, 1f)
            };
        }

        private static ThemeColors GetBloodMoonTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.06f, 0.02f, 0.02f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.1f, 0.03f, 0.03f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.14f, 0.04f, 0.04f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.22f, 0.06f, 0.06f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.28f, 0.08f, 0.08f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.7f, 0.1f, 0.1f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.55f, 0.05f, 0.05f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.82f, 0.15f, 0.15f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.65f, 0.1f, 0.1f, 0.98f),
                TabBorder = new UnityEngine.Color(0.75f, 0.12f, 0.12f, 0.6f),
                TabText = new UnityEngine.Color(0.92f, 0.75f, 0.75f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.88f, 0.88f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 0.95f, 0.95f, 1f),

                BoxBg = new UnityEngine.Color(0.08f, 0.025f, 0.025f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.6f, 0.08f, 0.08f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.72f, 0.12f, 0.12f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.48f, 0.05f, 0.05f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 0.92f, 0.92f, 1f),

                CheckboxBg = new UnityEngine.Color(0.12f, 0.04f, 0.04f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.72f, 0.12f, 0.12f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.18f, 0.06f, 0.06f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.85f, 0.18f, 0.18f, 0.95f),

                SliderBg = new UnityEngine.Color(0.12f, 0.04f, 0.04f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.72f, 0.12f, 0.12f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.88f, 0.2f, 0.2f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.55f, 0.08f, 0.08f, 0.95f),

                Separator = new UnityEngine.Color(0.68f, 0.12f, 0.12f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.04f, 0.015f, 0.015f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.88f, 0.88f, 1f),

                SectionBg = new UnityEngine.Color(0.07f, 0.022f, 0.022f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.78f, 0.15f, 0.15f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.16f, 0.05f, 0.05f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.09f, 0.03f, 0.03f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.62f, 0.1f, 0.1f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.14f, 0.045f, 0.045f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.18f, 0.06f, 0.06f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.05f, 0.015f, 0.015f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.08f, 0.025f, 0.025f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.85f, 0.18f, 0.18f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.7f, 0.7f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.92f, 0.92f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.88f, 0.68f, 0.68f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.9f, 0.9f, 1f),
                TextSecondary = new UnityEngine.Color(0.88f, 0.72f, 0.72f, 1f),

                DropdownBg = new UnityEngine.Color(0.1f, 0.032f, 0.032f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.16f, 0.05f, 0.05f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.22f, 0.07f, 0.07f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.75f, 0.12f, 0.12f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.88f, 0.2f, 0.2f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.07f, 0.022f, 0.022f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.65f, 0.1f, 0.1f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.65f, 0.1f, 0.1f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.75f, 0.12f, 0.12f, 1f),
                GlowSecondary = new UnityEngine.Color(0.92f, 0.35f, 0.35f, 1f)
            };
        }

        private static ThemeColors GetGoldenSandTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.085f, 0.055f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.15f, 0.13f, 0.08f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.2f, 0.17f, 0.1f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.28f, 0.24f, 0.14f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.35f, 0.3f, 0.18f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.92f, 0.78f, 0.42f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.82f, 0.65f, 0.32f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.98f, 0.85f, 0.5f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.9f, 0.75f, 0.4f, 0.98f),
                TabBorder = new UnityEngine.Color(0.95f, 0.8f, 0.45f, 0.6f),
                TabText = new UnityEngine.Color(0.98f, 0.92f, 0.78f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.96f, 0.88f, 1f),
                TabTextActive = new UnityEngine.Color(0.12f, 0.1f, 0.06f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.1f, 0.065f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.78f, 0.62f, 0.3f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.88f, 0.72f, 0.38f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.65f, 0.5f, 0.24f, 0.95f),
                ButtonText = new UnityEngine.Color(0.12f, 0.1f, 0.06f, 1f),

                CheckboxBg = new UnityEngine.Color(0.18f, 0.15f, 0.095f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.9f, 0.72f, 0.35f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.25f, 0.21f, 0.13f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.98f, 0.82f, 0.45f, 0.95f),

                SliderBg = new UnityEngine.Color(0.18f, 0.15f, 0.095f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.9f, 0.72f, 0.35f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 0.85f, 0.5f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.75f, 0.58f, 0.28f, 0.95f),

                Separator = new UnityEngine.Color(0.85f, 0.68f, 0.35f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.068f, 0.044f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.95f, 0.82f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.093f, 0.06f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.92f, 0.75f, 0.4f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.22f, 0.19f, 0.11f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.14f, 0.12f, 0.075f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.78f, 0.62f, 0.32f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.18f, 0.15f, 0.095f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.24f, 0.2f, 0.12f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.068f, 0.044f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.11f, 0.093f, 0.06f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.98f, 0.82f, 0.48f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.94f, 0.78f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.97f, 0.9f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.9f, 0.82f, 0.65f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.96f, 0.88f, 1f),
                TextSecondary = new UnityEngine.Color(0.9f, 0.82f, 0.68f, 1f),

                DropdownBg = new UnityEngine.Color(0.15f, 0.128f, 0.082f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.22f, 0.19f, 0.12f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.28f, 0.24f, 0.15f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.95f, 0.8f, 0.45f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 0.85f, 0.5f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.11f, 0.093f, 0.06f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.82f, 0.65f, 0.32f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.82f, 0.65f, 0.32f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.95f, 0.78f, 0.42f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.9f, 0.65f, 1f)
            };
        }

        private static ThemeColors GetIceCrystalTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.06f, 0.1f, 0.14f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.09f, 0.15f, 0.2f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.12f, 0.2f, 0.26f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.16f, 0.28f, 0.36f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.2f, 0.35f, 0.44f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.55f, 0.85f, 1f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.4f, 0.72f, 0.9f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.65f, 0.92f, 1f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.5f, 0.8f, 0.95f, 0.98f),
                TabBorder = new UnityEngine.Color(0.6f, 0.9f, 1f, 0.6f),
                TabText = new UnityEngine.Color(0.82f, 0.94f, 1f, 1f),
                TabTextHover = new UnityEngine.Color(0.92f, 0.98f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(0.08f, 0.12f, 0.16f, 1f),

                BoxBg = new UnityEngine.Color(0.07f, 0.12f, 0.16f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.38f, 0.68f, 0.85f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.48f, 0.78f, 0.95f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.28f, 0.55f, 0.72f, 0.95f),
                ButtonText = new UnityEngine.Color(0.08f, 0.12f, 0.16f, 1f),

                CheckboxBg = new UnityEngine.Color(0.1f, 0.17f, 0.22f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.5f, 0.82f, 0.98f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.14f, 0.24f, 0.3f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.62f, 0.9f, 1f, 0.95f),

                SliderBg = new UnityEngine.Color(0.1f, 0.17f, 0.22f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.5f, 0.82f, 0.98f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.65f, 0.92f, 1f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.38f, 0.68f, 0.85f, 0.95f),

                Separator = new UnityEngine.Color(0.48f, 0.78f, 0.95f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.05f, 0.08f, 0.11f, 0.98f),
                TooltipText = new UnityEngine.Color(0.88f, 0.96f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.065f, 0.11f, 0.15f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.55f, 0.88f, 1f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.12f, 0.21f, 0.28f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.08f, 0.14f, 0.18f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.42f, 0.72f, 0.9f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.1f, 0.18f, 0.24f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.13f, 0.23f, 0.3f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.05f, 0.08f, 0.11f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.07f, 0.12f, 0.16f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.62f, 0.92f, 1f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.88f, 0.98f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.94f, 0.99f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.75f, 0.9f, 0.98f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.92f, 0.98f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.78f, 0.9f, 0.97f, 1f),

                DropdownBg = new UnityEngine.Color(0.09f, 0.15f, 0.2f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.14f, 0.24f, 0.3f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.18f, 0.3f, 0.38f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.6f, 0.9f, 1f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.68f, 0.95f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.07f, 0.12f, 0.16f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.45f, 0.75f, 0.92f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.45f, 0.75f, 0.92f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.58f, 0.9f, 1f, 1f),
                GlowSecondary = new UnityEngine.Color(0.8f, 0.96f, 1f, 1f)
            };
        }

        private static ThemeColors GetNeonYellowTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.08f, 0.08f, 0.02f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.12f, 0.12f, 0.03f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.16f, 0.16f, 0.04f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.24f, 0.24f, 0.06f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.3f, 0.3f, 0.08f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.95f, 0.95f, 0.1f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.85f, 0.8f, 0.05f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(1f, 1f, 0.25f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.92f, 0.88f, 0.12f, 0.98f),
                TabBorder = new UnityEngine.Color(1f, 1f, 0.2f, 0.7f),
                TabText = new UnityEngine.Color(0.95f, 0.95f, 0.7f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 1f, 0.85f, 1f),
                TabTextActive = new UnityEngine.Color(0.1f, 0.1f, 0.02f, 1f),

                BoxBg = new UnityEngine.Color(0.1f, 0.1f, 0.025f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.8f, 0.78f, 0.08f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.92f, 0.9f, 0.15f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.65f, 0.62f, 0.05f, 0.95f),
                ButtonText = new UnityEngine.Color(0.1f, 0.1f, 0.02f, 1f),

                CheckboxBg = new UnityEngine.Color(0.14f, 0.14f, 0.035f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.92f, 0.9f, 0.12f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.2f, 0.2f, 0.05f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(1f, 0.98f, 0.25f, 0.95f),

                SliderBg = new UnityEngine.Color(0.14f, 0.14f, 0.035f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.92f, 0.9f, 0.12f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(1f, 1f, 0.3f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.75f, 0.72f, 0.08f, 0.95f),

                Separator = new UnityEngine.Color(0.88f, 0.85f, 0.15f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.06f, 0.06f, 0.015f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 1f, 0.8f, 1f),

                SectionBg = new UnityEngine.Color(0.09f, 0.09f, 0.022f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.95f, 0.92f, 0.18f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.18f, 0.18f, 0.045f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.11f, 0.11f, 0.028f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.82f, 0.78f, 0.1f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.15f, 0.15f, 0.038f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.2f, 0.2f, 0.05f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.06f, 0.06f, 0.015f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.09f, 0.09f, 0.022f, 0.98f),
                LogoAccent = new UnityEngine.Color(1f, 0.98f, 0.28f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 1f, 0.75f, 1f),
                LogoText = new UnityEngine.Color(1f, 1f, 0.9f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.9f, 0.88f, 0.6f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 1f, 0.88f, 1f),
                TextSecondary = new UnityEngine.Color(0.9f, 0.88f, 0.68f, 1f),

                DropdownBg = new UnityEngine.Color(0.12f, 0.12f, 0.03f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.2f, 0.2f, 0.05f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.26f, 0.26f, 0.065f, 0.98f),
                DropdownBorder = new UnityEngine.Color(1f, 1f, 0.2f, 0.6f),
                DropdownAccent = new UnityEngine.Color(1f, 1f, 0.35f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.09f, 0.09f, 0.022f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.85f, 0.82f, 0.12f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.85f, 0.82f, 0.12f, 0.4f),

                GlowPrimary = new UnityEngine.Color(1f, 0.98f, 0.2f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 1f, 0.55f, 1f)
            };
        }

        private static ThemeColors GetDarkRoseTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.1f, 0.04f, 0.06f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.15f, 0.06f, 0.09f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.2f, 0.08f, 0.12f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.3f, 0.12f, 0.18f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.38f, 0.15f, 0.22f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.85f, 0.35f, 0.5f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.72f, 0.25f, 0.4f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.95f, 0.45f, 0.6f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.82f, 0.35f, 0.5f, 0.98f),
                TabBorder = new UnityEngine.Color(0.9f, 0.4f, 0.55f, 0.6f),
                TabText = new UnityEngine.Color(1f, 0.85f, 0.9f, 1f),
                TabTextHover = new UnityEngine.Color(1f, 0.92f, 0.95f, 1f),
                TabTextActive = new UnityEngine.Color(0.12f, 0.05f, 0.07f, 1f),

                BoxBg = new UnityEngine.Color(0.12f, 0.05f, 0.07f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.72f, 0.28f, 0.42f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.85f, 0.38f, 0.52f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.58f, 0.2f, 0.32f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.18f, 0.07f, 0.1f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.88f, 0.38f, 0.52f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.26f, 0.1f, 0.15f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.95f, 0.48f, 0.62f, 0.95f),

                SliderBg = new UnityEngine.Color(0.18f, 0.07f, 0.1f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.88f, 0.38f, 0.52f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.98f, 0.52f, 0.65f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.72f, 0.28f, 0.4f, 0.95f),

                Separator = new UnityEngine.Color(0.82f, 0.35f, 0.48f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.08f, 0.032f, 0.048f, 0.98f),
                TooltipText = new UnityEngine.Color(1f, 0.9f, 0.94f, 1f),

                SectionBg = new UnityEngine.Color(0.11f, 0.045f, 0.065f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.9f, 0.42f, 0.58f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.24f, 0.1f, 0.14f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.14f, 0.058f, 0.082f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.78f, 0.32f, 0.45f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.2f, 0.08f, 0.11f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.26f, 0.1f, 0.15f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.08f, 0.032f, 0.048f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.11f, 0.045f, 0.065f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.95f, 0.5f, 0.65f, 1f),
                LogoAccentLight = new UnityEngine.Color(1f, 0.82f, 0.88f, 1f),
                LogoText = new UnityEngine.Color(1f, 0.94f, 0.96f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.92f, 0.75f, 0.8f, 0.7f),

                TextPrimary = new UnityEngine.Color(1f, 0.93f, 0.95f, 1f),
                TextSecondary = new UnityEngine.Color(0.92f, 0.78f, 0.82f, 1f),

                DropdownBg = new UnityEngine.Color(0.15f, 0.06f, 0.085f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.24f, 0.1f, 0.14f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.32f, 0.13f, 0.18f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.9f, 0.4f, 0.55f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.98f, 0.52f, 0.68f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.11f, 0.045f, 0.065f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.82f, 0.32f, 0.45f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.82f, 0.32f, 0.45f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.9f, 0.4f, 0.55f, 1f),
                GlowSecondary = new UnityEngine.Color(1f, 0.65f, 0.75f, 1f)
            };
        }

        private static ThemeColors GetOceanDepthTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.02f, 0.05f, 0.1f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.03f, 0.08f, 0.15f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.04f, 0.1f, 0.2f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.05f, 0.14f, 0.28f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.06f, 0.18f, 0.35f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.1f, 0.45f, 0.8f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.08f, 0.35f, 0.65f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.15f, 0.55f, 0.9f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.1f, 0.45f, 0.78f, 0.98f),
                TabBorder = new UnityEngine.Color(0.15f, 0.55f, 0.9f, 0.6f),
                TabText = new UnityEngine.Color(0.7f, 0.88f, 1f, 1f),
                TabTextHover = new UnityEngine.Color(0.85f, 0.94f, 1f, 1f),
                TabTextActive = new UnityEngine.Color(1f, 1f, 1f, 1f),

                BoxBg = new UnityEngine.Color(0.025f, 0.06f, 0.12f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.08f, 0.38f, 0.68f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.12f, 0.48f, 0.8f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.05f, 0.28f, 0.52f, 0.95f),
                ButtonText = new UnityEngine.Color(1f, 1f, 1f, 1f),

                CheckboxBg = new UnityEngine.Color(0.035f, 0.09f, 0.17f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.12f, 0.5f, 0.85f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.05f, 0.12f, 0.24f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.18f, 0.6f, 0.95f, 0.95f),

                SliderBg = new UnityEngine.Color(0.035f, 0.09f, 0.17f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.12f, 0.5f, 0.85f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.2f, 0.62f, 0.98f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.08f, 0.38f, 0.68f, 0.95f),

                Separator = new UnityEngine.Color(0.1f, 0.45f, 0.78f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.015f, 0.04f, 0.08f, 0.98f),
                TooltipText = new UnityEngine.Color(0.8f, 0.92f, 1f, 1f),

                SectionBg = new UnityEngine.Color(0.022f, 0.055f, 0.11f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.15f, 0.55f, 0.92f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.05f, 0.12f, 0.24f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.03f, 0.075f, 0.15f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.1f, 0.42f, 0.72f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.04f, 0.1f, 0.2f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.05f, 0.13f, 0.26f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.015f, 0.04f, 0.08f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.025f, 0.06f, 0.12f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.2f, 0.6f, 0.98f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.7f, 0.88f, 1f, 1f),
                LogoText = new UnityEngine.Color(0.88f, 0.95f, 1f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.6f, 0.8f, 0.95f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.85f, 0.94f, 1f, 1f),
                TextSecondary = new UnityEngine.Color(0.65f, 0.82f, 0.95f, 1f),

                DropdownBg = new UnityEngine.Color(0.03f, 0.08f, 0.16f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.05f, 0.12f, 0.24f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.06f, 0.16f, 0.3f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.15f, 0.55f, 0.9f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.22f, 0.65f, 1f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.025f, 0.06f, 0.12f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.1f, 0.42f, 0.75f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.1f, 0.42f, 0.75f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.15f, 0.55f, 0.92f, 1f),
                GlowSecondary = new UnityEngine.Color(0.4f, 0.75f, 1f, 1f)
            };
        }

        private static ThemeColors GetRadioactiveTheme()
        {
            return new ThemeColors
            {
                WindowBg = new UnityEngine.Color(0.04f, 0.06f, 0.02f, 0.98f),

                TabNormalTop = new UnityEngine.Color(0.06f, 0.1f, 0.03f, 0.95f),
                TabNormalBottom = new UnityEngine.Color(0.08f, 0.14f, 0.04f, 0.95f),
                TabHoverTop = new UnityEngine.Color(0.12f, 0.22f, 0.06f, 0.95f),
                TabHoverBottom = new UnityEngine.Color(0.16f, 0.28f, 0.08f, 0.95f),
                TabActiveTop = new UnityEngine.Color(0.7f, 1f, 0.15f, 0.98f),
                TabActiveBottom = new UnityEngine.Color(0.55f, 0.85f, 0.1f, 0.98f),
                TabActiveHoverTop = new UnityEngine.Color(0.8f, 1f, 0.3f, 0.98f),
                TabActiveHoverBottom = new UnityEngine.Color(0.65f, 0.92f, 0.18f, 0.98f),
                TabBorder = new UnityEngine.Color(0.75f, 1f, 0.2f, 0.7f),
                TabText = new UnityEngine.Color(0.85f, 1f, 0.7f, 1f),
                TabTextHover = new UnityEngine.Color(0.92f, 1f, 0.82f, 1f),
                TabTextActive = new UnityEngine.Color(0.08f, 0.1f, 0.03f, 1f),

                BoxBg = new UnityEngine.Color(0.05f, 0.08f, 0.025f, 0.92f),

                ButtonBg = new UnityEngine.Color(0.55f, 0.82f, 0.1f, 0.9f),
                ButtonHover = new UnityEngine.Color(0.68f, 0.95f, 0.18f, 0.95f),
                ButtonActive = new UnityEngine.Color(0.42f, 0.65f, 0.08f, 0.95f),
                ButtonText = new UnityEngine.Color(0.08f, 0.1f, 0.03f, 1f),

                CheckboxBg = new UnityEngine.Color(0.08f, 0.12f, 0.035f, 0.9f),
                CheckboxOn = new UnityEngine.Color(0.72f, 0.98f, 0.18f, 0.95f),
                CheckboxHover = new UnityEngine.Color(0.12f, 0.18f, 0.05f, 0.9f),
                CheckboxOnHover = new UnityEngine.Color(0.82f, 1f, 0.32f, 0.95f),

                SliderBg = new UnityEngine.Color(0.08f, 0.12f, 0.035f, 0.9f),
                SliderThumb = new UnityEngine.Color(0.72f, 0.98f, 0.18f, 0.95f),
                SliderThumbHover = new UnityEngine.Color(0.85f, 1f, 0.35f, 0.95f),
                SliderThumbActive = new UnityEngine.Color(0.55f, 0.78f, 0.12f, 0.95f),

                Separator = new UnityEngine.Color(0.65f, 0.92f, 0.18f, 0.5f),

                TooltipBg = new UnityEngine.Color(0.03f, 0.05f, 0.015f, 0.98f),
                TooltipText = new UnityEngine.Color(0.9f, 1f, 0.78f, 1f),

                SectionBg = new UnityEngine.Color(0.045f, 0.07f, 0.022f, 0.92f),
                SectionAccent = new UnityEngine.Color(0.75f, 1f, 0.22f, 1f),
                SectionHeaderLeft = new UnityEngine.Color(0.1f, 0.16f, 0.045f, 0.98f),
                SectionHeaderRight = new UnityEngine.Color(0.06f, 0.1f, 0.028f, 0.95f),
                SectionHeaderLine = new UnityEngine.Color(0.6f, 0.85f, 0.15f, 0.6f),

                LogoTopLeft = new UnityEngine.Color(0.08f, 0.13f, 0.038f, 0.95f),
                LogoTopRight = new UnityEngine.Color(0.11f, 0.17f, 0.05f, 0.95f),
                LogoBottomLeft = new UnityEngine.Color(0.03f, 0.05f, 0.015f, 0.98f),
                LogoBottomRight = new UnityEngine.Color(0.05f, 0.08f, 0.025f, 0.98f),
                LogoAccent = new UnityEngine.Color(0.82f, 1f, 0.3f, 1f),
                LogoAccentLight = new UnityEngine.Color(0.92f, 1f, 0.72f, 1f),
                LogoText = new UnityEngine.Color(0.94f, 1f, 0.85f, 1f),
                LogoSubtitle = new UnityEngine.Color(0.78f, 0.92f, 0.6f, 0.7f),

                TextPrimary = new UnityEngine.Color(0.92f, 1f, 0.82f, 1f),
                TextSecondary = new UnityEngine.Color(0.78f, 0.92f, 0.62f, 1f),

                DropdownBg = new UnityEngine.Color(0.065f, 0.1f, 0.032f, 0.95f),
                DropdownBgHover = new UnityEngine.Color(0.1f, 0.16f, 0.048f, 0.98f),
                DropdownBgOpen = new UnityEngine.Color(0.14f, 0.22f, 0.065f, 0.98f),
                DropdownBorder = new UnityEngine.Color(0.75f, 1f, 0.2f, 0.6f),
                DropdownAccent = new UnityEngine.Color(0.85f, 1f, 0.35f, 0.9f),
                DropdownListBg = new UnityEngine.Color(0.05f, 0.08f, 0.025f, 0.98f),
                DropdownItemHover = new UnityEngine.Color(0.62f, 0.88f, 0.15f, 0.25f),
                DropdownItemSelected = new UnityEngine.Color(0.62f, 0.88f, 0.15f, 0.4f),

                GlowPrimary = new UnityEngine.Color(0.75f, 1f, 0.2f, 1f),
                GlowSecondary = new UnityEngine.Color(0.88f, 1f, 0.5f, 1f)
            };
        }
    }
}