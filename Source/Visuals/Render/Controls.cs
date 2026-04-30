namespace HotLava_Cheat.Source.Visuals.Render
{
    public static class Controls
    {
        private static System.Collections.Generic.Dictionary<string, float> s_dictButtonHoverAnim = new System.Collections.Generic.Dictionary<string, float>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictCheckboxHoverAnim = new System.Collections.Generic.Dictionary<string, float>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictSliderHoverAnim = new System.Collections.Generic.Dictionary<string, float>();
        private static string s_sActiveSliderKey = null;
        private static float s_flActiveSliderGrabOffset = 0f;
        private const float flCheckboxLabelYOffset = -5f;

        #region Basic

        private static float UpdateHoverAnimation(System.Collections.Generic.Dictionary<string, float> dictAnim, string sKey, bool bHovered, float flHoverInSpeed = 9f, float flHoverOutSpeed = 4f)
        {
            if (!dictAnim.ContainsKey(sKey))
                dictAnim[sKey] = 0f;

            float flTarget = bHovered ? 1f : 0f;
            float flSpeed = bHovered ? flHoverInSpeed : flHoverOutSpeed;

            dictAnim[sKey] = UnityEngine.Mathf.MoveTowards(dictAnim[sKey], flTarget, UnityEngine.Time.deltaTime * flSpeed);

            return dictAnim[sKey];
        }

        private static void RenderText(UnityEngine.Rect rect, string sText, UnityEngine.Color colText, UnityEngine.TextAnchor textAnchor, int iFontSize, UnityEngine.FontStyle fontStyle, UnityEngine.RectOffset rectOffset = null)
        {
            UnityEngine.GUIStyle gsText = new UnityEngine.GUIStyle();

            gsText.fontSize = iFontSize;
            gsText.fontStyle = fontStyle;
            gsText.alignment = textAnchor;
            gsText.wordWrap = false;
            gsText.clipping = UnityEngine.TextClipping.Clip;
            gsText.padding = rectOffset != null ? new UnityEngine.RectOffset(rectOffset.left, rectOffset.right, rectOffset.top, rectOffset.bottom) : new UnityEngine.RectOffset();

            UnityEngine.Color colShadow = new UnityEngine.Color(0f, 0f, 0f, 0.5f * colText.a);

            gsText.normal.textColor = colShadow;
            UnityEngine.GUI.Label(new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height), sText, gsText);

            gsText.normal.textColor = colText;
            UnityEngine.GUI.Label(rect, sText, gsText);
        }

        private static void RenderSurface(UnityEngine.Rect rect, UnityEngine.Color colBg, UnityEngine.Color colBorder, UnityEngine.Color colAccent, float flAccentAlpha)
        {
            UnityEngine.GUI.color = colBg;
            UnityEngine.GUI.DrawTexture(rect, UnityEngine.Texture2D.whiteTexture);

            Helpers.RenderRectBorder(rect, colBorder, 1f);

            if (flAccentAlpha > 0.01f)
            {
                colAccent.a *= flAccentAlpha;

                UnityEngine.Rect rectAccent = new UnityEngine.Rect(rect.x, rect.y, 3f, rect.height);

                UnityEngine.GUI.color = colAccent;
                UnityEngine.GUI.DrawTexture(rectAccent, UnityEngine.Texture2D.whiteTexture);
            }

            UnityEngine.GUI.color = UnityEngine.Color.white;
        }

        private static void ResetActiveSlider()
        {
            s_sActiveSliderKey = null;
            s_flActiveSliderGrabOffset = 0f;
        }

        private static bool IsInputBlockedByModal()
        {
            return Tabs.IsBackgroundInteractionBlocked();
        }

        public static bool Checkbox(string sLabel, bool bValue)
        {
            string sKey = sLabel + "_checkbox";

            Themes.ThemeColors theme = Themes.CurrentTheme;

            bool bInputBlocked = IsInputBlockedByModal();

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.Rect rectBox = UnityEngine.GUILayoutUtility.GetRect(22f, 22f, UnityEngine.GUILayout.Width(24f), UnityEngine.GUILayout.Height(24f));
            UnityEngine.Rect rectLabel = UnityEngine.GUILayoutUtility.GetRect(new UnityEngine.GUIContent(sLabel), Styles.GS.LabelStyle, UnityEngine.GUILayout.Height(24f), UnityEngine.GUILayout.ExpandWidth(true));
            UnityEngine.GUILayout.EndHorizontal();

            UnityEngine.Rect rectRow = new UnityEngine.Rect(rectBox.x, rectBox.y, rectLabel.xMax - rectBox.x, UnityEngine.Mathf.Max(rectBox.height, rectLabel.height));

            bool bHovered = rectRow.Contains(UnityEngine.Event.current.mousePosition);
            float flHoverAnim = UpdateHoverAnimation(s_dictCheckboxHoverAnim, sKey, bHovered);
            bool bPressed = bHovered && UnityEngine.Input.GetMouseButton(0);
            bool bResult = bValue;

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectRow.Contains(UnityEngine.Event.current.mousePosition))
            {
                bResult = !bValue;
                UnityEngine.Event.current.Use();
            }

            UnityEngine.Color colBg = bValue ? UnityEngine.Color.Lerp(theme.CheckboxOn, theme.CheckboxOnHover, flHoverAnim) : UnityEngine.Color.Lerp(theme.CheckboxBg, theme.CheckboxHover, flHoverAnim);

            if (bPressed)
                colBg = bValue ? theme.CheckboxOnHover : theme.CheckboxHover;

            UnityEngine.Color colBorder = theme.DropdownBorder;
            colBorder.a = 0.45f + flHoverAnim * 0.25f;

            RenderSurface(rectBox, colBg, colBorder, theme.DropdownAccent, 0f);

            if (bValue)
                RenderText(rectBox, "✓", theme.TextPrimary, UnityEngine.TextAnchor.MiddleCenter, 13, UnityEngine.FontStyle.Bold);

            UnityEngine.Color colLabel = UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, 0.35f + flHoverAnim * 0.65f);
            RenderText(new UnityEngine.Rect(rectLabel.x, rectLabel.y + flCheckboxLabelYOffset, rectLabel.width, rectLabel.height), sLabel, colLabel, UnityEngine.TextAnchor.MiddleLeft, 13, UnityEngine.FontStyle.Normal);

            return bResult;
        }

        public static float Slider(string sLabel, float flValue, float flMin, float flMax, float flLabelWidth = 90f, float flSliderWidth = 160f)
        {
            string sKey = sLabel + "_slider";

            Themes.ThemeColors theme = Themes.CurrentTheme;

            bool bInputBlocked = IsInputBlockedByModal();

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.Rect rectLabel = UnityEngine.GUILayoutUtility.GetRect(new UnityEngine.GUIContent(sLabel), Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(flLabelWidth), UnityEngine.GUILayout.Height(28f));
            UnityEngine.Rect rectSlider = UnityEngine.GUILayoutUtility.GetRect(flSliderWidth, 28f, UnityEngine.GUILayout.Width(flSliderWidth), UnityEngine.GUILayout.Height(28f));
            UnityEngine.Rect rectValue = UnityEngine.GUILayoutUtility.GetRect(45f, 28f, UnityEngine.GUILayout.Width(45f), UnityEngine.GUILayout.Height(28f));
            UnityEngine.GUILayout.EndHorizontal();

            UnityEngine.Rect rectTrack = new UnityEngine.Rect(rectSlider.x, rectSlider.y + 11f, rectSlider.width, 6f);

            float flNormalized = UnityEngine.Mathf.InverseLerp(flMin, flMax, flValue);
            float flThumbX = rectTrack.x + rectTrack.width * flNormalized;

            UnityEngine.Rect rectThumb = new UnityEngine.Rect(flThumbX - 8f, rectSlider.y + 6f, 16f, 16f);
            UnityEngine.Rect rectHit = new UnityEngine.Rect(rectSlider.x, rectSlider.y + 2f, rectSlider.width, 24f);

            bool bThumbHover = rectThumb.Contains(UnityEngine.Event.current.mousePosition);

            bool bDragging = s_sActiveSliderKey == sKey;
            bool bHovered = rectHit.Contains(UnityEngine.Event.current.mousePosition) || bThumbHover || bDragging;
            float flHoverAnim = UpdateHoverAnimation(s_dictSliderHoverAnim, sKey, bHovered);

            if (bInputBlocked && s_sActiveSliderKey == sKey)
                ResetActiveSlider();

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && bThumbHover)
            {
                s_sActiveSliderKey = sKey;
                s_flActiveSliderGrabOffset = UnityEngine.Event.current.mousePosition.x - flThumbX;

                bDragging = true;

                UnityEngine.Event.current.Use();
            }

            else if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectHit.Contains(UnityEngine.Event.current.mousePosition))
            {
                s_sActiveSliderKey = sKey;
                s_flActiveSliderGrabOffset = 0f;

                bDragging = true;

                float flMouseT = UnityEngine.Mathf.Clamp01((UnityEngine.Event.current.mousePosition.x - rectTrack.x) / rectTrack.width);
                flValue = UnityEngine.Mathf.Lerp(flMin, flMax, flMouseT);

                UnityEngine.Event.current.Use();
            }

            if (!bInputBlocked && bDragging && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag)
            {
                float flDragX = UnityEngine.Event.current.mousePosition.x - s_flActiveSliderGrabOffset;
                float flMouseT = UnityEngine.Mathf.Clamp01((flDragX - rectTrack.x) / rectTrack.width);

                flValue = UnityEngine.Mathf.Lerp(flMin, flMax, flMouseT);

                UnityEngine.Event.current.Use();
            }

            if (!bInputBlocked && s_sActiveSliderKey == sKey && UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp && UnityEngine.Event.current.button == 0)
            {
                ResetActiveSlider();
                UnityEngine.Event.current.Use();
            }

            flNormalized = UnityEngine.Mathf.InverseLerp(flMin, flMax, flValue);
            flThumbX = rectTrack.x + rectTrack.width * flNormalized;
            rectThumb = new UnityEngine.Rect(flThumbX - 8f, rectSlider.y + 6f, 16f, 16f);

            UnityEngine.Color colTrackBg = UnityEngine.Color.Lerp(theme.DropdownBg, theme.DropdownBgHover, flHoverAnim * 0.6f);
            UnityEngine.Color colTrackBorder = theme.DropdownBorder;

            colTrackBorder.a = 0.4f + flHoverAnim * 0.25f;

            UnityEngine.GUI.color = colTrackBg;
            UnityEngine.GUI.DrawTexture(rectTrack, UnityEngine.Texture2D.whiteTexture);

            UnityEngine.Rect rectFill = new UnityEngine.Rect(rectTrack.x + 1f, rectTrack.y + 1f, UnityEngine.Mathf.Max(2f, flThumbX - rectTrack.x - 1f), UnityEngine.Mathf.Max(2f, rectTrack.height - 2f));
            UnityEngine.Color colFill = UnityEngine.Color.Lerp(theme.SliderThumb, theme.SliderThumbHover, 0.35f + flHoverAnim * 0.65f);

            UnityEngine.GUI.color = colFill;
            UnityEngine.GUI.DrawTexture(rectFill, UnityEngine.Texture2D.whiteTexture);

            Helpers.RenderRectBorder(rectTrack, colTrackBorder, 1f);

            UnityEngine.Color colThumb = bDragging ? theme.SliderThumbActive : UnityEngine.Color.Lerp(theme.SliderThumb, theme.SliderThumbHover, flHoverAnim);
            UnityEngine.Color colThumbBorder = theme.DropdownBorder;

            colThumbBorder.a = 0.55f + flHoverAnim * 0.25f;

            RenderSurface(rectThumb, colThumb, colThumbBorder, theme.DropdownAccent, 0f);

            UnityEngine.GUI.color = UnityEngine.Color.white;

            RenderText(rectLabel, sLabel, UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, 0.2f + flHoverAnim * 0.8f), UnityEngine.TextAnchor.MiddleLeft, 13, UnityEngine.FontStyle.Normal);
            RenderText(rectValue, flValue.ToString("F2"), UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, 0.35f + flHoverAnim * 0.65f), UnityEngine.TextAnchor.MiddleRight, 12, UnityEngine.FontStyle.Normal);

            return flValue;
        }

        public static bool Button(string sLabel, float flWidth = 150, float flHeight = 35f)
        {
            string sKey = sLabel + "_button_" + flWidth.ToString("F0") + "_" + flHeight.ToString("F0");

            Themes.ThemeColors theme = Themes.CurrentTheme;

            bool bInputBlocked = IsInputBlockedByModal();

            UnityEngine.Rect rectButton = UnityEngine.GUILayoutUtility.GetRect(0f, 0f, UnityEngine.GUILayout.Width(flWidth), UnityEngine.GUILayout.Height(flHeight));

            bool bHovered = rectButton.Contains(UnityEngine.Event.current.mousePosition);
            float flHoverAnim = UpdateHoverAnimation(s_dictButtonHoverAnim, sKey, bHovered);
            bool bPressed = bHovered && UnityEngine.Input.GetMouseButton(0);
            bool bClicked = false;

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectButton.Contains(UnityEngine.Event.current.mousePosition))
            {
                bClicked = true;
                UnityEngine.Event.current.Use();
            }

            UnityEngine.Color colBg = bPressed ? theme.ButtonActive : UnityEngine.Color.Lerp(theme.ButtonBg, theme.ButtonHover, flHoverAnim);

            UnityEngine.Color colBorder = theme.DropdownBorder;
            colBorder.a = bPressed ? 0.85f : 0.45f + flHoverAnim * 0.25f;

            RenderSurface(rectButton, colBg, colBorder, theme.DropdownAccent, 0f);

            UnityEngine.Color colText = UnityEngine.Color.Lerp(theme.TextSecondary, theme.ButtonText, 0.35f + flHoverAnim * 0.65f);

            RenderText(rectButton, sLabel, colText, UnityEngine.TextAnchor.MiddleCenter, 13, UnityEngine.FontStyle.Bold);

            return bClicked;
        }

        #endregion

        #region KeyBind

        private static System.Collections.Generic.Dictionary<string, bool> s_dictKeyBindListening = new System.Collections.Generic.Dictionary<string, bool>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictKeyBindHoverAnim = new System.Collections.Generic.Dictionary<string, float>();

        public static void KeyBind(string sLabel, ref UnityEngine.KeyCode kcKey, ref int iMode)
        {
            string sKey = sLabel + "_keybind";
            bool bInputBlocked = IsInputBlockedByModal();

            if (!s_dictKeyBindListening.ContainsKey(sKey))
                s_dictKeyBindListening[sKey] = false;

            if (!s_dictKeyBindHoverAnim.ContainsKey(sKey))
                s_dictKeyBindHoverAnim[sKey] = 0f;

            bool bListening = s_dictKeyBindListening[sKey];

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Label(sLabel, Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(50f));

            float flButtonWidth = 100f;

            UnityEngine.Rect rectButton = UnityEngine.GUILayoutUtility.GetRect(flButtonWidth, 28f);

            bool bButtonHover = rectButton.Contains(UnityEngine.Event.current.mousePosition);
            float flTargetHover = bButtonHover ? 1f : 0f;

            s_dictKeyBindHoverAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictKeyBindHoverAnim[sKey], flTargetHover, UnityEngine.Time.deltaTime * 14f);

            float flHoverAnim = s_dictKeyBindHoverAnim[sKey];

            RenderKeyBindButton(rectButton, kcKey, bListening, flHoverAnim);

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectButton.Contains(UnityEngine.Event.current.mousePosition))
            {
                s_dictKeyBindListening[sKey] = !s_dictKeyBindListening[sKey];
                UnityEngine.Event.current.Use();
            }

            if (!bInputBlocked && bListening && UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown)
            {
                UnityEngine.KeyCode newKey = UnityEngine.Event.current.keyCode;

                if (newKey == UnityEngine.KeyCode.Insert)
                {
                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey == UnityEngine.KeyCode.Backspace)
                {
                    kcKey = UnityEngine.KeyCode.None;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey != UnityEngine.KeyCode.None && newKey != UnityEngine.KeyCode.Escape)
                {
                    kcKey = newKey;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey == UnityEngine.KeyCode.Escape)
                {
                    kcKey = UnityEngine.KeyCode.None;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }
            }

            if (!bInputBlocked && bListening && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown)
            {
                if (UnityEngine.Event.current.button >= 0 && UnityEngine.Event.current.button <= 6)
                {
                    kcKey = UnityEngine.KeyCode.Mouse0 + UnityEngine.Event.current.button;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }
            }

            UnityEngine.GUILayout.Space(5f);

            string[] arrModes = new string[] { "Hold", "Toggle" };
            iMode = DropdownCompact("", iMode, arrModes, 110f);

            UnityEngine.GUILayout.EndHorizontal();
        }

        public static UnityEngine.KeyCode KeybindButton(string sLabel, UnityEngine.KeyCode kcKey, float flButtonWidth = 120f)
        {
            string sKey = sLabel + "_keybind_button_" + kcKey.ToString();
            bool bInputBlocked = IsInputBlockedByModal();

            if (!s_dictKeyBindListening.ContainsKey(sKey))
                s_dictKeyBindListening[sKey] = false;

            if (!s_dictKeyBindHoverAnim.ContainsKey(sKey))
                s_dictKeyBindHoverAnim[sKey] = 0f;

            bool bListening = s_dictKeyBindListening[sKey];

            if (!string.IsNullOrEmpty(sLabel))
            {
                UnityEngine.GUILayout.BeginHorizontal();
                UnityEngine.GUILayout.Label(sLabel, Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(150f));
            }

            UnityEngine.Rect rectButton = UnityEngine.GUILayoutUtility.GetRect(0f, 0f, UnityEngine.GUILayout.Width(flButtonWidth), UnityEngine.GUILayout.Height(28f));

            bool bButtonHover = rectButton.Contains(UnityEngine.Event.current.mousePosition);

            float flTargetHover = bButtonHover ? 1f : 0f;
            s_dictKeyBindHoverAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictKeyBindHoverAnim[sKey], flTargetHover, UnityEngine.Time.deltaTime * 14f);

            float flHoverAnim = s_dictKeyBindHoverAnim[sKey];

            RenderKeyBindButton(rectButton, kcKey, bListening, flHoverAnim);

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectButton.Contains(UnityEngine.Event.current.mousePosition))
            {
                s_dictKeyBindListening[sKey] = !s_dictKeyBindListening[sKey];
                UnityEngine.Event.current.Use();
            }

            if (!bInputBlocked && bListening && UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown)
            {
                UnityEngine.KeyCode newKey = UnityEngine.Event.current.keyCode;

                if (newKey == UnityEngine.KeyCode.Insert)
                {
                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey == UnityEngine.KeyCode.Backspace)
                {
                    kcKey = UnityEngine.KeyCode.None;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey != UnityEngine.KeyCode.None && newKey != UnityEngine.KeyCode.Escape)
                {
                    kcKey = newKey;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }

                else if (newKey == UnityEngine.KeyCode.Escape)
                {
                    kcKey = UnityEngine.KeyCode.None;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }
            }

            if (!bInputBlocked && bListening && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown)
            {
                if (UnityEngine.Event.current.button >= 0 && UnityEngine.Event.current.button <= 6)
                {
                    kcKey = UnityEngine.KeyCode.Mouse0 + UnityEngine.Event.current.button;

                    s_dictKeyBindListening[sKey] = false;
                    UnityEngine.Event.current.Use();
                }
            }

            if (!string.IsNullOrEmpty(sLabel))
                UnityEngine.GUILayout.EndHorizontal();

            return kcKey;
        }

        public static int DropdownCompact(string sLabel, int iSelected, string[] arrItems, float flWidth)
        {
            string sKey = sLabel + "_dropdown_compact";
            bool bInputBlocked = IsInputBlockedByModal();

            if (!s_dictDropdownOpen.ContainsKey(sKey))
                s_dictDropdownOpen[sKey] = false;

            if (!s_dictDropdownAnim.ContainsKey(sKey))
                s_dictDropdownAnim[sKey] = 0f;

            if (!s_dictDropdownHoverAnim.ContainsKey(sKey))
                s_dictDropdownHoverAnim[sKey] = 0f;

            if (!s_dictDropdownScrollOffset.ContainsKey(sKey))
                s_dictDropdownScrollOffset[sKey] = 0f;

            if (s_iDropdownNewSelection >= 0 && s_sDropdownSelectionKey == sKey)
            {
                iSelected = s_iDropdownNewSelection;

                s_iDropdownNewSelection = -1;
                s_sDropdownSelectionKey = null;
                s_dictDropdownOpen[sKey] = false;
                s_dictDropdownScrollOffset[sKey] = 0f;
                s_sActiveDropdownKey = null;
                s_bNeedCloseDropdown = false;
                s_bDraggingScrollbar = false;
            }

            if (s_bNeedCloseDropdown && s_sActiveDropdownKey == sKey)
            {
                s_dictDropdownOpen[sKey] = false;
                s_dictDropdownScrollOffset[sKey] = 0f;
                s_sActiveDropdownKey = null;
                s_bNeedCloseDropdown = false;
                s_bDraggingScrollbar = false;
            }

            bool bIsOpen = s_dictDropdownOpen[sKey];

            float flTargetAnim = bIsOpen ? 1f : 0f;

            s_dictDropdownAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictDropdownAnim[sKey], flTargetAnim, UnityEngine.Time.deltaTime * 12f);

            float flHoverAnim = s_dictDropdownHoverAnim[sKey];

            string sCurrentItem = (iSelected >= 0 && iSelected < arrItems.Length) ? arrItems[iSelected] : "---";

            UnityEngine.Rect rectButton = UnityEngine.GUILayoutUtility.GetRect(0f, 0f, UnityEngine.GUILayout.Width(flWidth), UnityEngine.GUILayout.Height(28f));

            if (UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
                s_dictDropdownButtonRects[sKey] = rectButton;

            if (s_dictDropdownButtonRects.ContainsKey(sKey) && s_dictDropdownButtonRects[sKey].width > 0)
                rectButton = s_dictDropdownButtonRects[sKey];

            bool bButtonHover = rectButton.Contains(UnityEngine.Event.current.mousePosition);

            if (!string.IsNullOrEmpty(s_sActiveDropdownKey) && s_sActiveDropdownKey != sKey && s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey])
            {
                UnityEngine.Rect rectActiveDropdownList = GetActiveDropdownRect();

                if (rectActiveDropdownList.Contains(UnityEngine.Event.current.mousePosition))
                    bButtonHover = false;
            }

            float flTargetHover = bButtonHover ? 1f : 0f;

            s_dictDropdownHoverAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictDropdownHoverAnim[sKey], flTargetHover, UnityEngine.Time.deltaTime * 14f);

            flHoverAnim = s_dictDropdownHoverAnim[sKey];

            RenderDropdownButton(rectButton, sCurrentItem, flHoverAnim, bIsOpen);

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectButton.Contains(UnityEngine.Event.current.mousePosition))
            {
                bool bClickBlocked = false;

                if (!string.IsNullOrEmpty(s_sActiveDropdownKey) && s_sActiveDropdownKey != sKey && s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey])
                {
                    UnityEngine.Rect rectActiveDropdownList = GetActiveDropdownRect();

                    if (rectActiveDropdownList.Contains(UnityEngine.Event.current.mousePosition))
                        bClickBlocked = true;
                }

                if (!bClickBlocked)
                {
                    foreach (var key in new System.Collections.Generic.List<string>(s_dictDropdownOpen.Keys))
                    {
                        if (key != sKey)
                        {
                            s_dictDropdownOpen[key] = false;
                            s_dictDropdownScrollOffset[key] = 0f;
                        }
                    }

                    s_dictDropdownOpen[sKey] = !s_dictDropdownOpen[sKey];

                    if (s_dictDropdownOpen[sKey])
                    {
                        s_sActiveDropdownKey = sKey;
                        s_iActiveDropdownSelected = iSelected;
                        s_arrActiveDropdownItems = arrItems;
                        s_flActiveDropdownWidth = flWidth;

                        s_dictDropdownScrollOffset[sKey] = CalculateScrollOffsetForSelectedItem(iSelected, arrItems.Length);
                    }

                    else
                    {
                        s_sActiveDropdownKey = null;
                        s_dictDropdownScrollOffset[sKey] = 0f;
                        s_bDraggingScrollbar = false;
                    }

                    s_bClickWasHandled = true;

                    UnityEngine.Event.current.Use();
                }
            }

            if (s_dictDropdownOpen[sKey])
            {
                s_sActiveDropdownKey = sKey;
                s_iActiveDropdownSelected = iSelected;
                s_arrActiveDropdownItems = arrItems;
                s_flActiveDropdownWidth = flWidth;
            }

            return iSelected;
        }

        private static void RenderKeyBindButton(UnityEngine.Rect rect, UnityEngine.KeyCode kcKey, bool bListening, float flHoverAnim)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.Color colBg = bListening ? theme.DropdownBgOpen : UnityEngine.Color.Lerp(theme.DropdownBg, theme.DropdownBgHover, flHoverAnim);

            UnityEngine.GUI.color = colBg;
            UnityEngine.GUI.DrawTexture(rect, UnityEngine.Texture2D.whiteTexture);

            float flBorderAlpha = bListening ? 0.8f : 0.4f + flHoverAnim * 0.3f;

            UnityEngine.Color colBorder = theme.DropdownBorder;
            colBorder.a = flBorderAlpha;

            Helpers.RenderRectBorder(rect, colBorder, 1f);

            UnityEngine.GUI.color = UnityEngine.Color.white;
            UnityEngine.GUIStyle gsText = new UnityEngine.GUIStyle();

            gsText.fontSize = 12;
            gsText.fontStyle = UnityEngine.FontStyle.Normal;
            gsText.alignment = UnityEngine.TextAnchor.MiddleCenter;
            gsText.wordWrap = false;
            gsText.clipping = UnityEngine.TextClipping.Clip;

            string sDisplayText = bListening ? "Press key..." : GetKeyName(kcKey);

            UnityEngine.Color colText = UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, flHoverAnim);
            gsText.normal.textColor = colText;

            UnityEngine.GUI.Label(rect, sDisplayText, gsText);
        }

        private static string GetKeyName(UnityEngine.KeyCode key)
        {
            switch (key)
            {
                case UnityEngine.KeyCode.Mouse0: return "Mouse1";
                case UnityEngine.KeyCode.Mouse1: return "Mouse2";
                case UnityEngine.KeyCode.Mouse2: return "Mouse3";
                case UnityEngine.KeyCode.Mouse3: return "Mouse4";
                case UnityEngine.KeyCode.Mouse4: return "Mouse5";
                case UnityEngine.KeyCode.Mouse5: return "Mouse6";
                case UnityEngine.KeyCode.Mouse6: return "Mouse7";
                case UnityEngine.KeyCode.Alpha0: return "0";
                case UnityEngine.KeyCode.Alpha1: return "1";
                case UnityEngine.KeyCode.Alpha2: return "2";
                case UnityEngine.KeyCode.Alpha3: return "3";
                case UnityEngine.KeyCode.Alpha4: return "4";
                case UnityEngine.KeyCode.Alpha5: return "5";
                case UnityEngine.KeyCode.Alpha6: return "6";
                case UnityEngine.KeyCode.Alpha7: return "7";
                case UnityEngine.KeyCode.Alpha8: return "8";
                case UnityEngine.KeyCode.Alpha9: return "9";
                case UnityEngine.KeyCode.LeftShift: return "LShift";
                case UnityEngine.KeyCode.RightShift: return "RShift";
                case UnityEngine.KeyCode.LeftControl: return "LCtrl";
                case UnityEngine.KeyCode.RightControl: return "RCtrl";
                case UnityEngine.KeyCode.LeftAlt: return "LAlt";
                case UnityEngine.KeyCode.RightAlt: return "RAlt";
                case UnityEngine.KeyCode.Space: return "Space";
                case UnityEngine.KeyCode.Return: return "Enter";
                case UnityEngine.KeyCode.Backspace: return "Backspace";
                case UnityEngine.KeyCode.BackQuote: return "`";
                case UnityEngine.KeyCode.Minus: return "-";
                case UnityEngine.KeyCode.Equals: return "=";
                case UnityEngine.KeyCode.LeftBracket: return "[";
                case UnityEngine.KeyCode.RightBracket: return "]";
                case UnityEngine.KeyCode.Backslash: return "\\";
                case UnityEngine.KeyCode.Semicolon: return ";";
                case UnityEngine.KeyCode.Quote: return "'";
                case UnityEngine.KeyCode.Comma: return ",";
                case UnityEngine.KeyCode.Period: return ".";
                case UnityEngine.KeyCode.Slash: return "/";
                case UnityEngine.KeyCode.Tab: return "Tab";
                case UnityEngine.KeyCode.CapsLock: return "CapsLock";
                default: return key.ToString();
            }
        }

        #endregion

        #region Dropdown

        private static System.Collections.Generic.Dictionary<string, bool> s_dictDropdownOpen = new System.Collections.Generic.Dictionary<string, bool>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictDropdownAnim = new System.Collections.Generic.Dictionary<string, float>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictDropdownHoverAnim = new System.Collections.Generic.Dictionary<string, float>();
        private static System.Collections.Generic.Dictionary<string, UnityEngine.Rect> s_dictDropdownButtonRects = new System.Collections.Generic.Dictionary<string, UnityEngine.Rect>();
        private static System.Collections.Generic.Dictionary<string, float> s_dictDropdownScrollOffset = new System.Collections.Generic.Dictionary<string, float>();

        private static string s_sActiveDropdownKey = null;
        private static int s_iActiveDropdownSelected;
        private static string[] s_arrActiveDropdownItems;
        private static float s_flActiveDropdownWidth;
        private static int s_iDropdownNewSelection = -1;
        private static string s_sDropdownSelectionKey = null;
        private static bool s_bNeedCloseDropdown = false;
        private static bool s_bMouseDownThisFrame = false;
        private static bool s_bClickWasHandled = false;

        private static bool s_bDraggingScrollbar = false;
        private static float s_flDragStartY = 0f;
        private static float s_flDragStartScrollOffset = 0f;

        private const int MAX_VISIBLE_ITEMS = 5;
        private const float SCROLLBAR_WIDTH = 12f;
        private const float ITEM_HEIGHT = 26f;

        public static bool IsPointInsideDropdown(UnityEngine.Vector2 Point)
        {
            if (string.IsNullOrEmpty(s_sActiveDropdownKey))
                return false;

            if (!s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) || !s_dictDropdownOpen[s_sActiveDropdownKey])
                return false;

            if (!s_dictDropdownButtonRects.ContainsKey(s_sActiveDropdownKey))
                return false;

            UnityEngine.Rect rectButton = s_dictDropdownButtonRects[s_sActiveDropdownKey];

            if (rectButton.width <= 0 || rectButton.height <= 0)
                return false;

            int iVisibleItems = System.Math.Min(s_arrActiveDropdownItems.Length, MAX_VISIBLE_ITEMS);
            float flListHeight = iVisibleItems * ITEM_HEIGHT + 4f;

            UnityEngine.Rect rectList = new UnityEngine.Rect(rectButton.x, rectButton.y + rectButton.height + 2f, s_flActiveDropdownWidth, flListHeight);

            return rectButton.Contains(Point) || rectList.Contains(Point);
        }

        public static bool IsAnyDropdownOpen()
        {
            if (string.IsNullOrEmpty(s_sActiveDropdownKey))
                return false;

            return s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey];
        }

        public static UnityEngine.Rect GetActiveDropdownRect()
        {
            if (string.IsNullOrEmpty(s_sActiveDropdownKey) || !s_dictDropdownButtonRects.ContainsKey(s_sActiveDropdownKey))
                return new UnityEngine.Rect();

            UnityEngine.Rect rectButton = s_dictDropdownButtonRects[s_sActiveDropdownKey];

            int iVisibleItems = System.Math.Min(s_arrActiveDropdownItems.Length, MAX_VISIBLE_ITEMS);
            float flListHeight = iVisibleItems * ITEM_HEIGHT + 4f;

            return new UnityEngine.Rect(rectButton.x, rectButton.y + rectButton.height + 2f, s_flActiveDropdownWidth, flListHeight);
        }

        public static void CloseAllDropdowns()
        {
            foreach (var Key in new System.Collections.Generic.List<string>(s_dictDropdownOpen.Keys))
                s_dictDropdownOpen[Key] = false;

            s_sActiveDropdownKey = null;
            s_bNeedCloseDropdown = false;
            s_bDraggingScrollbar = false;
        }

        public static void BeginFrame()
        {
            if (!string.IsNullOrEmpty(s_sActiveSliderKey))
            {
                UnityEngine.Rect rectLocalWindow = new UnityEngine.Rect(0f, 0f, NS_Visuals.GUIManager.WindowRect.width, NS_Visuals.GUIManager.WindowRect.height);

                if (!NS_Visuals.GUIManager.bShowGUI || !rectLocalWindow.Contains(UnityEngine.Event.current.mousePosition))
                    ResetActiveSlider();
            }

            if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0)
            {
                s_bMouseDownThisFrame = true;
                s_bClickWasHandled = false;
            }

            else if (UnityEngine.Event.current.type == UnityEngine.EventType.Layout)
            {
                s_bMouseDownThisFrame = false;
                s_bClickWasHandled = false;
            }

            if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp && UnityEngine.Event.current.button == 0)
            {
                s_bDraggingScrollbar = false;
            }
        }

        public static void MarkClickHandled()
        {
            s_bClickWasHandled = true;
        }

        private static float CalculateScrollOffsetForSelectedItem(int iSelectedIndex, int iTotalItems)
        {
            if (iTotalItems <= MAX_VISIBLE_ITEMS)
                return 0f;

            if (iSelectedIndex < MAX_VISIBLE_ITEMS / 2)
                return 0f;

            float flFullContentHeight = iTotalItems * ITEM_HEIGHT;
            float flVisibleHeight = MAX_VISIBLE_ITEMS * ITEM_HEIGHT;
            float flMaxScroll = flFullContentHeight - flVisibleHeight;

            if (iSelectedIndex >= iTotalItems - MAX_VISIBLE_ITEMS / 2)
                return flMaxScroll;

            float flSelectedItemTop = iSelectedIndex * ITEM_HEIGHT;
            float flCenterOffset = flSelectedItemTop - (flVisibleHeight / 2f) + (ITEM_HEIGHT / 2f);

            return UnityEngine.Mathf.Clamp(flCenterOffset, 0f, flMaxScroll);
        }

        public static int Dropdown(string sLabel, int iSelected, string[] arrItems)
        {
            string sKey = sLabel + "_dropdown";
            bool bInputBlocked = IsInputBlockedByModal();

            if (!s_dictDropdownOpen.ContainsKey(sKey))
                s_dictDropdownOpen[sKey] = false;

            if (!s_dictDropdownAnim.ContainsKey(sKey))
                s_dictDropdownAnim[sKey] = 0f;

            if (!s_dictDropdownHoverAnim.ContainsKey(sKey))
                s_dictDropdownHoverAnim[sKey] = 0f;

            if (!s_dictDropdownScrollOffset.ContainsKey(sKey))
                s_dictDropdownScrollOffset[sKey] = 0f;

            if (s_iDropdownNewSelection >= 0 && s_sDropdownSelectionKey == sKey)
            {
                iSelected = s_iDropdownNewSelection;

                s_iDropdownNewSelection = -1;
                s_sDropdownSelectionKey = null;
                s_dictDropdownOpen[sKey] = false;
                s_dictDropdownScrollOffset[sKey] = 0f;
                s_sActiveDropdownKey = null;
                s_bNeedCloseDropdown = false;
                s_bDraggingScrollbar = false;
            }

            if (s_bNeedCloseDropdown && s_sActiveDropdownKey == sKey)
            {
                s_dictDropdownOpen[sKey] = false;
                s_dictDropdownScrollOffset[sKey] = 0f;
                s_sActiveDropdownKey = null;
                s_bNeedCloseDropdown = false;
                s_bDraggingScrollbar = false;
            }

            bool bIsOpen = s_dictDropdownOpen[sKey];

            float flTargetAnim = bIsOpen ? 1f : 0f;

            s_dictDropdownAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictDropdownAnim[sKey], flTargetAnim, UnityEngine.Time.deltaTime * 12f);

            float flHoverAnim = s_dictDropdownHoverAnim[sKey];

            UnityEngine.GUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(sLabel))
                UnityEngine.GUILayout.Label(sLabel, Styles.GS.LabelStyle, UnityEngine.GUILayout.Width(90f));

            string sCurrentItem = (iSelected >= 0 && iSelected < arrItems.Length) ? arrItems[iSelected] : "---";

            float flWidthButton = 220f;

            UnityEngine.Rect rectButton = UnityEngine.GUILayoutUtility.GetRect(flWidthButton, 28f);

            if (UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
                s_dictDropdownButtonRects[sKey] = rectButton;

            if (s_dictDropdownButtonRects.ContainsKey(sKey) && s_dictDropdownButtonRects[sKey].width > 0)
                rectButton = s_dictDropdownButtonRects[sKey];

            bool bButtonHover = rectButton.Contains(UnityEngine.Event.current.mousePosition);

            if (!string.IsNullOrEmpty(s_sActiveDropdownKey) && s_sActiveDropdownKey != sKey && s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey])
            {
                UnityEngine.Rect rectActiveDropdownList = GetActiveDropdownRect();

                if (rectActiveDropdownList.Contains(UnityEngine.Event.current.mousePosition))
                    bButtonHover = false;
            }

            float flTargetHover = bButtonHover ? 1f : 0f;

            s_dictDropdownHoverAnim[sKey] = UnityEngine.Mathf.Lerp(s_dictDropdownHoverAnim[sKey], flTargetHover, UnityEngine.Time.deltaTime * 14f);

            flHoverAnim = s_dictDropdownHoverAnim[sKey];

            RenderDropdownButton(rectButton, sCurrentItem, flHoverAnim, bIsOpen);

            if (!bInputBlocked && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectButton.Contains(UnityEngine.Event.current.mousePosition))
            {
                bool bClickBlocked = false;

                if (!string.IsNullOrEmpty(s_sActiveDropdownKey) && s_sActiveDropdownKey != sKey && s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey])
                {
                    UnityEngine.Rect rectActiveDropdownList = GetActiveDropdownRect();

                    if (rectActiveDropdownList.Contains(UnityEngine.Event.current.mousePosition))
                        bClickBlocked = true;
                }

                if (!bClickBlocked)
                {
                    foreach (var key in new System.Collections.Generic.List<string>(s_dictDropdownOpen.Keys))
                    {
                        if (key != sKey)
                        {
                            s_dictDropdownOpen[key] = false;
                            s_dictDropdownScrollOffset[key] = 0f;
                        }
                    }

                    s_dictDropdownOpen[sKey] = !s_dictDropdownOpen[sKey];

                    if (s_dictDropdownOpen[sKey])
                    {
                        s_sActiveDropdownKey = sKey;
                        s_iActiveDropdownSelected = iSelected;
                        s_arrActiveDropdownItems = arrItems;
                        s_flActiveDropdownWidth = flWidthButton;

                        s_dictDropdownScrollOffset[sKey] = CalculateScrollOffsetForSelectedItem(iSelected, arrItems.Length);
                    }

                    else
                    {
                        s_sActiveDropdownKey = null;
                        s_dictDropdownScrollOffset[sKey] = 0f;
                        s_bDraggingScrollbar = false;
                    }

                    s_bClickWasHandled = true;

                    UnityEngine.Event.current.Use();
                }
            }

            UnityEngine.GUILayout.EndHorizontal();

            if (s_dictDropdownOpen[sKey])
            {
                s_sActiveDropdownKey = sKey;
                s_iActiveDropdownSelected = iSelected;
                s_arrActiveDropdownItems = arrItems;
                s_flActiveDropdownWidth = flWidthButton;
            }

            return iSelected;
        }

        public static void RenderDropdownOverlay()
        {
            if (string.IsNullOrEmpty(s_sActiveDropdownKey))
                return;

            if (!s_dictDropdownAnim.ContainsKey(s_sActiveDropdownKey))
                return;

            if (!s_dictDropdownButtonRects.ContainsKey(s_sActiveDropdownKey))
                return;

            bool bIsOpen = s_dictDropdownOpen.ContainsKey(s_sActiveDropdownKey) && s_dictDropdownOpen[s_sActiveDropdownKey];
            float flOpenAnim = s_dictDropdownAnim[s_sActiveDropdownKey];

            if (!bIsOpen && flOpenAnim <= 0.01f)
            {
                s_sActiveDropdownKey = null;
                s_bDraggingScrollbar = false;

                return;
            }

            UnityEngine.Rect rectButton = s_dictDropdownButtonRects[s_sActiveDropdownKey];

            if (rectButton.width <= 0 || rectButton.height <= 0)
                return;

            bool bNeedsScrollbar = s_arrActiveDropdownItems.Length > MAX_VISIBLE_ITEMS;
            int iVisibleItems = bNeedsScrollbar ? MAX_VISIBLE_ITEMS : s_arrActiveDropdownItems.Length;
            float flFullContentHeight = s_arrActiveDropdownItems.Length * ITEM_HEIGHT;
            float flVisibleHeight = iVisibleItems * ITEM_HEIGHT;
            float flListHeight = flVisibleHeight + 4f;
            float flCurrentHeight = flListHeight * flOpenAnim;

            UnityEngine.Rect rectList = new UnityEngine.Rect(rectButton.x, rectButton.y + rectButton.height + 2f, s_flActiveDropdownWidth, flCurrentHeight);

            float flScrollOffset = 0f;

            if (s_dictDropdownScrollOffset.ContainsKey(s_sActiveDropdownKey))
                flScrollOffset = s_dictDropdownScrollOffset[s_sActiveDropdownKey];

            float flMaxScroll = flFullContentHeight - flVisibleHeight;

            if (flMaxScroll < 0f)
                flMaxScroll = 0f;

            float flItemsWidth = bNeedsScrollbar ? s_flActiveDropdownWidth - SCROLLBAR_WIDTH - 2f : s_flActiveDropdownWidth;

            UnityEngine.Rect rectScrollbarArea = new UnityEngine.Rect(rectList.x + flItemsWidth + 1f, rectList.y + 2f, SCROLLBAR_WIDTH, flVisibleHeight);

            if (s_bDraggingScrollbar && bNeedsScrollbar)
            {
                if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag || UnityEngine.Event.current.type == UnityEngine.EventType.Repaint)
                {
                    float flDeltaY = UnityEngine.Event.current.mousePosition.y - s_flDragStartY;

                    float flVisibleRatio = flVisibleHeight / flFullContentHeight;
                    float flThumbHeight = UnityEngine.Mathf.Max(rectScrollbarArea.height * flVisibleRatio, 20f);
                    float flScrollableTrackHeight = rectScrollbarArea.height - flThumbHeight;

                    if (flScrollableTrackHeight > 0f)
                    {
                        float flScrollDelta = (flDeltaY / flScrollableTrackHeight) * flMaxScroll;
                        flScrollOffset = s_flDragStartScrollOffset + flScrollDelta;
                        flScrollOffset = UnityEngine.Mathf.Clamp(flScrollOffset, 0f, flMaxScroll);

                        s_dictDropdownScrollOffset[s_sActiveDropdownKey] = flScrollOffset;
                    }

                    if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag)
                        UnityEngine.Event.current.Use();
                }
            }

            if (bNeedsScrollbar && bIsOpen && flOpenAnim > 0.9f && rectList.Contains(UnityEngine.Event.current.mousePosition))
            {
                if (UnityEngine.Event.current.type == UnityEngine.EventType.ScrollWheel)
                {
                    flScrollOffset += UnityEngine.Event.current.delta.y * 20f;
                    flScrollOffset = UnityEngine.Mathf.Clamp(flScrollOffset, 0f, flMaxScroll);

                    s_dictDropdownScrollOffset[s_sActiveDropdownKey] = flScrollOffset;

                    UnityEngine.Event.current.Use();
                }
            }

            if (bIsOpen && flOpenAnim > 0.9f && UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 0 && rectList.Contains(UnityEngine.Event.current.mousePosition))
            {
                UnityEngine.Vector2 v2MousePos = UnityEngine.Event.current.mousePosition;

                UnityEngine.Rect rectItemsArea = new UnityEngine.Rect(rectList.x, rectList.y + 2f, flItemsWidth, flVisibleHeight);

                if (bNeedsScrollbar && rectScrollbarArea.Contains(v2MousePos))
                {
                    float flVisibleRatio = flVisibleHeight / flFullContentHeight;
                    float flThumbHeight = UnityEngine.Mathf.Max(rectScrollbarArea.height * flVisibleRatio, 20f);
                    float flScrollRatio = flMaxScroll > 0f ? flScrollOffset / flMaxScroll : 0f;
                    float flThumbY = rectScrollbarArea.y + (rectScrollbarArea.height - flThumbHeight) * flScrollRatio;

                    UnityEngine.Rect rectThumb = new UnityEngine.Rect(rectScrollbarArea.x, flThumbY, SCROLLBAR_WIDTH, flThumbHeight);

                    if (rectThumb.Contains(v2MousePos))
                    {
                        s_bDraggingScrollbar = true;
                        s_flDragStartY = v2MousePos.y;
                        s_flDragStartScrollOffset = flScrollOffset;
                    }

                    else
                    {
                        float flClickY = v2MousePos.y - rectScrollbarArea.y;
                        float flTargetScrollRatio = flClickY / rectScrollbarArea.height;

                        flScrollOffset = flTargetScrollRatio * flMaxScroll;
                        flScrollOffset = UnityEngine.Mathf.Clamp(flScrollOffset, 0f, flMaxScroll);

                        s_dictDropdownScrollOffset[s_sActiveDropdownKey] = flScrollOffset;
                    }

                    s_bClickWasHandled = true;

                    UnityEngine.Event.current.Use();
                    return;
                }

                if (rectItemsArea.Contains(v2MousePos))
                {
                    float flRelativeY = v2MousePos.y - rectItemsArea.y + flScrollOffset;
                    int iClickedIndex = (int)(flRelativeY / ITEM_HEIGHT);

                    if (iClickedIndex >= 0 && iClickedIndex < s_arrActiveDropdownItems.Length)
                    {
                        s_iDropdownNewSelection = iClickedIndex;
                        s_sDropdownSelectionKey = s_sActiveDropdownKey;
                        s_dictDropdownOpen[s_sActiveDropdownKey] = false;
                        s_dictDropdownScrollOffset[s_sActiveDropdownKey] = 0f;
                        s_bNeedCloseDropdown = true;
                        s_bClickWasHandled = true;
                        s_bDraggingScrollbar = false;

                        UnityEngine.Event.current.Use();
                        return;
                    }
                }

                s_bClickWasHandled = true;

                UnityEngine.Event.current.Use();
                return;
            }

            RenderDropdownList(rectButton, s_iActiveDropdownSelected, s_arrActiveDropdownItems, flOpenAnim, s_flActiveDropdownWidth, flScrollOffset);
        }

        public static void EndFrame()
        {
            if (s_bMouseDownThisFrame && !s_bClickWasHandled && IsAnyDropdownOpen())
            {
                if (!IsPointInsideDropdown(UnityEngine.Event.current.mousePosition))
                {
                    s_dictDropdownOpen[s_sActiveDropdownKey] = false;
                    s_dictDropdownScrollOffset[s_sActiveDropdownKey] = 0f;
                    s_sActiveDropdownKey = null;
                    s_bDraggingScrollbar = false;
                }
            }

            s_bMouseDownThisFrame = false;
        }

        private static void RenderDropdownButton(UnityEngine.Rect rect, string sText, float flHoverAnim, bool bIsOpen)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            UnityEngine.Color colBg = UnityEngine.Color.Lerp(theme.DropdownBg, theme.DropdownBgHover, flHoverAnim);

            if (bIsOpen)
                colBg = theme.DropdownBgOpen;

            UnityEngine.GUI.color = colBg;
            UnityEngine.GUI.DrawTexture(rect, UnityEngine.Texture2D.whiteTexture);

            float flBorderAlpha = 0.4f + flHoverAnim * 0.3f;

            if (bIsOpen)
                flBorderAlpha = 0.8f;

            UnityEngine.Color colBorder = theme.DropdownBorder;
            colBorder.a = flBorderAlpha;

            Helpers.RenderRectBorder(rect, colBorder, 1f);

            if (flHoverAnim > 0.01f || bIsOpen)
            {
                float flAccentAlpha = bIsOpen ? 0.9f : 0.7f * flHoverAnim;

                UnityEngine.Color colAccent = theme.DropdownAccent;
                colAccent.a = flAccentAlpha;

                UnityEngine.Rect rectAccent = new UnityEngine.Rect(rect.x, rect.y, 3f, rect.height);

                UnityEngine.GUI.color = colAccent;

                UnityEngine.GUI.DrawTexture(rectAccent, UnityEngine.Texture2D.whiteTexture);
            }

            UnityEngine.GUI.color = UnityEngine.Color.white;

            UnityEngine.GUIStyle gsText = new UnityEngine.GUIStyle();

            gsText.fontSize = 12;
            gsText.fontStyle = UnityEngine.FontStyle.Normal;
            gsText.alignment = UnityEngine.TextAnchor.MiddleLeft;
            gsText.padding = new UnityEngine.RectOffset(10, 10, 0, 0);
            gsText.wordWrap = false;
            gsText.clipping = UnityEngine.TextClipping.Clip;

            gsText.normal.textColor = new UnityEngine.Color(0f, 0f, 0f, 0.5f);

            UnityEngine.Rect rectText = new UnityEngine.Rect(rect.x, rect.y, UnityEngine.Mathf.Max(0f, rect.width - 18f), rect.height);

            UnityEngine.GUI.Label(new UnityEngine.Rect(rectText.x + 1f, rectText.y + 1f, rectText.width, rectText.height), sText, gsText);

            UnityEngine.Color colText = UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, flHoverAnim);

            gsText.normal.textColor = colText;

            UnityEngine.GUI.Label(rectText, sText, gsText);

            UnityEngine.GUIStyle gsArrow = new UnityEngine.GUIStyle();

            gsArrow.fontSize = 10;
            gsArrow.alignment = UnityEngine.TextAnchor.MiddleRight;
            gsArrow.padding = new UnityEngine.RectOffset(0, 10, 0, 0);
            gsArrow.normal.textColor = colText;

            UnityEngine.GUI.Label(rect, bIsOpen ? "▲" : "▼", gsArrow);
        }

        private static void RenderDropdownList(UnityEngine.Rect rectButton, int iSelected, string[] arrItems, float flOpenAnim, float flWidth, float flScrollOffset)
        {
            Themes.ThemeColors theme = Themes.CurrentTheme;

            bool bNeedsScrollbar = arrItems.Length > MAX_VISIBLE_ITEMS;
            int iVisibleItems = bNeedsScrollbar ? MAX_VISIBLE_ITEMS : arrItems.Length;
            float flFullContentHeight = arrItems.Length * ITEM_HEIGHT;
            float flVisibleHeight = iVisibleItems * ITEM_HEIGHT;
            float flListHeight = flVisibleHeight + 4f;
            float flCurrentHeight = flListHeight * flOpenAnim;

            UnityEngine.Rect rectList = new UnityEngine.Rect(rectButton.x, rectButton.y + rectButton.height + 2f, flWidth, flCurrentHeight);

            UnityEngine.Color colShadow = new UnityEngine.Color(0f, 0f, 0f, 0.4f * flOpenAnim);
            UnityEngine.Rect rectShadow = new UnityEngine.Rect(rectList.x + 3f, rectList.y + 3f, rectList.width, rectList.height);

            UnityEngine.GUI.color = colShadow;

            UnityEngine.GUI.DrawTexture(rectShadow, UnityEngine.Texture2D.whiteTexture);

            UnityEngine.Color colListBg = theme.DropdownListBg;
            colListBg.a *= flOpenAnim;

            UnityEngine.GUI.color = colListBg;

            UnityEngine.GUI.DrawTexture(rectList, UnityEngine.Texture2D.whiteTexture);

            UnityEngine.Color colListBorder = theme.DropdownBorder;
            colListBorder.a *= flOpenAnim;

            Helpers.RenderRectBorder(rectList, colListBorder, 1f);

            UnityEngine.GUI.color = UnityEngine.Color.white;

            if (flOpenAnim > 0.5f)
            {
                float flItemAlpha = (flOpenAnim - 0.5f) * 2f;
                float flItemsWidth = bNeedsScrollbar ? flWidth - SCROLLBAR_WIDTH - 2f : flWidth;

                UnityEngine.Rect rectItemsArea = new UnityEngine.Rect(rectList.x, rectList.y + 2f, flItemsWidth, flVisibleHeight);

                UnityEngine.Vector2 v2MousePosGlobal = UnityEngine.Event.current.mousePosition;

                UnityEngine.GUI.BeginClip(rectItemsArea);

                for (int i = 0; i < arrItems.Length; i++)
                {
                    float flItemY = i * ITEM_HEIGHT - flScrollOffset;

                    if (flItemY + ITEM_HEIGHT < 0f || flItemY > flVisibleHeight)
                        continue;

                    UnityEngine.Rect rectItem = new UnityEngine.Rect(0f, flItemY, flItemsWidth, ITEM_HEIGHT);
                    UnityEngine.Rect rectItemGlobal = new UnityEngine.Rect(rectItemsArea.x, rectItemsArea.y + flItemY, flItemsWidth, ITEM_HEIGHT);

                    bool bItemVisible = (flItemY + ITEM_HEIGHT > 0f) && (flItemY < flVisibleHeight);
                    bool bItemHover = bItemVisible && rectItemGlobal.Contains(v2MousePosGlobal);

                    if (bItemHover)
                    {
                        float flVisibleTop = UnityEngine.Mathf.Max(flItemY, 0f);
                        float flVisibleBottom = UnityEngine.Mathf.Min(flItemY + ITEM_HEIGHT, flVisibleHeight);

                        UnityEngine.Rect rectVisiblePart = new UnityEngine.Rect(rectItemsArea.x, rectItemsArea.y + flVisibleTop, flItemsWidth, flVisibleBottom - flVisibleTop);

                        bItemHover = rectVisiblePart.Contains(v2MousePosGlobal);
                    }

                    bool bItemSelected = (i == iSelected);

                    if (bItemSelected)
                    {
                        UnityEngine.Color colSelected = theme.DropdownItemSelected;
                        colSelected.a *= flItemAlpha;

                        UnityEngine.GUI.color = colSelected;

                        UnityEngine.GUI.DrawTexture(rectItem, UnityEngine.Texture2D.whiteTexture);
                    }

                    else if (bItemHover)
                    {
                        UnityEngine.Color colHover = theme.DropdownItemHover;
                        colHover.a *= flItemAlpha;

                        UnityEngine.GUI.color = colHover;

                        UnityEngine.GUI.DrawTexture(rectItem, UnityEngine.Texture2D.whiteTexture);
                    }

                    UnityEngine.GUI.color = UnityEngine.Color.white;

                    UnityEngine.GUIStyle gsItem = new UnityEngine.GUIStyle();

                    gsItem.fontSize = 12;
                    gsItem.alignment = UnityEngine.TextAnchor.MiddleLeft;
                    gsItem.padding = new UnityEngine.RectOffset(12, 8, 0, 0);

                    UnityEngine.Color colItemText;

                    if (bItemSelected)
                    {
                        colItemText = theme.TextPrimary;
                        colItemText.a = flItemAlpha;
                    }

                    else if (bItemHover)
                    {
                        colItemText = UnityEngine.Color.Lerp(theme.TextSecondary, theme.TextPrimary, 0.7f);
                        colItemText.a = flItemAlpha;
                    }

                    else
                    {
                        colItemText = theme.TextSecondary;
                        colItemText.a = flItemAlpha;
                    }

                    gsItem.normal.textColor = colItemText;

                    UnityEngine.GUI.Label(rectItem, arrItems[i], gsItem);

                    if (bItemSelected)
                    {
                        UnityEngine.Rect rectIndicator = new UnityEngine.Rect(0f, flItemY + 4f, 2f, ITEM_HEIGHT - 8f);

                        UnityEngine.Color colIndicator = theme.DropdownAccent;
                        colIndicator.a *= flItemAlpha;

                        UnityEngine.GUI.color = colIndicator;

                        UnityEngine.GUI.DrawTexture(rectIndicator, UnityEngine.Texture2D.whiteTexture);

                        UnityEngine.GUI.color = UnityEngine.Color.white;
                    }
                }

                UnityEngine.GUI.EndClip();

                if (bNeedsScrollbar)
                {
                    float flMaxScroll = flFullContentHeight - flVisibleHeight;

                    UnityEngine.Rect rectScrollbarBg = new UnityEngine.Rect(rectList.x + flItemsWidth + 1f, rectList.y + 2f, SCROLLBAR_WIDTH, flVisibleHeight);
                    UnityEngine.Color colScrollbarBg = new UnityEngine.Color(0f, 0f, 0f, 0.3f * flItemAlpha);

                    UnityEngine.GUI.color = colScrollbarBg;

                    UnityEngine.GUI.DrawTexture(rectScrollbarBg, UnityEngine.Texture2D.whiteTexture);

                    float flVisibleRatio = flVisibleHeight / flFullContentHeight;
                    float flThumbHeight = UnityEngine.Mathf.Max(rectScrollbarBg.height * flVisibleRatio, 20f);
                    float flScrollRatio = flMaxScroll > 0f ? flScrollOffset / flMaxScroll : 0f;
                    float flThumbY = rectScrollbarBg.y + (rectScrollbarBg.height - flThumbHeight) * flScrollRatio;

                    UnityEngine.Rect rectThumb = new UnityEngine.Rect(rectScrollbarBg.x + 2f, flThumbY, SCROLLBAR_WIDTH - 4f, flThumbHeight);

                    bool bThumbHover = rectThumb.Contains(v2MousePosGlobal);

                    UnityEngine.Color colThumb;

                    if (s_bDraggingScrollbar || bThumbHover)
                    {
                        colThumb = theme.DropdownAccent;
                        colThumb.a = flItemAlpha;
                    }

                    else
                    {
                        colThumb = theme.DropdownAccent;
                        colThumb.a = 0.6f * flItemAlpha;
                    }

                    UnityEngine.GUI.color = colThumb;

                    UnityEngine.GUI.DrawTexture(rectThumb, UnityEngine.Texture2D.whiteTexture);

                    UnityEngine.GUI.color = UnityEngine.Color.white;
                }
            }
        }

        #endregion
    }
}