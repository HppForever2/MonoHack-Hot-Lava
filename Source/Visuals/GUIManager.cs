namespace HotLava_Cheat.Source.Visuals
{
    public static class GUIManager
    {
        public static UnityEngine.Rect WindowRect = new UnityEngine.Rect(20f, 20f, 720f, 520f);

        public static int iSelectedTab = 0;
        public static int iHoveredTab = -1;

        public static bool bShowGUI = false;
        public static bool bFirstShow = true;

        public static string[] strTabsEN = new string[] { "★ Main", "▶ MR", "● Other" };
        public static string[] strTabsRU = new string[] { "★ Главное", "▶ MR", "● Остальное" };

        public static void Render(Base baseInstance)
        {
            if (!bShowGUI)
                return;

            if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown)
            {
                UnityEngine.Vector2 mouseScreenPos = UnityEngine.Event.current.mousePosition;

                if (!WindowRect.Contains(mouseScreenPos))
                    NS_Visuals.Render.Controls.CloseAllDropdowns();
            }

            UnityEngine.GUI.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
            UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(0f, 0f, (float)UnityEngine.Screen.width, (float)UnityEngine.Screen.height), Textures.T2D.OverlayTexture);
            UnityEngine.GUI.color = UnityEngine.Color.white;

            Styles.Initialize();

            if (bFirstShow)
            {
                WindowRect.x = ((float)UnityEngine.Screen.width - WindowRect.width) / 2f;
                WindowRect.y = ((float)UnityEngine.Screen.height - WindowRect.height) / 2f;

                bFirstShow = false;
            }

            else
            {
                if (WindowRect.x < 0f)
                    WindowRect.x = 0f;

                if (WindowRect.x + WindowRect.width > (float)UnityEngine.Screen.width)
                    WindowRect.x = (float)UnityEngine.Screen.width - WindowRect.width;

                if (WindowRect.y < 0f)
                    WindowRect.y = 0f;

                if (WindowRect.y + WindowRect.height > (float)UnityEngine.Screen.height)
                    WindowRect.y = (float)UnityEngine.Screen.height - WindowRect.height;
            }

            WindowRect = UnityEngine.GUI.Window(0, WindowRect, new UnityEngine.GUI.WindowFunction(baseInstance.WindowFunction), "", Styles.GS.WindowStyle);
        }

        public static void WindowFunction(int iWindowID)
        {
            NS_Visuals.Render.Controls.BeginFrame();

            UnityEngine.GUILayout.BeginVertical(System.Array.Empty<UnityEngine.GUILayoutOption>());

            NS_Visuals.Render.GUI.Logo();
            UnityEngine.GUILayout.Space(10f);

            NS_Visuals.Render.Tabs.RenderTabs();
            UnityEngine.GUILayout.Space(10f);

            switch (iSelectedTab)
            {
                case 0:
                    NS_Visuals.Render.Tabs.CharacterTab();
                    break;
                case 1:
                    NS_Visuals.Render.Tabs.MRTab();
                    break;
                case 2:
                    NS_Visuals.Render.Tabs.OtherTab();
                    break;
            }

            UnityEngine.GUILayout.EndVertical();

            if (WindowRect.x + WindowRect.width > (float)UnityEngine.Screen.width)
                WindowRect.x = (float)UnityEngine.Screen.width - WindowRect.width;

            if (WindowRect.y + WindowRect.height > (float)UnityEngine.Screen.height)
                WindowRect.y = (float)UnityEngine.Screen.height - WindowRect.height;

            NS_Visuals.Render.Controls.RenderDropdownOverlay();
            NS_Visuals.Render.Tabs.RenderModal();
            NS_Visuals.Render.Controls.EndFrame();

            if (!NS_Visuals.Render.Tabs.IsMRModalOpen())
                UnityEngine.GUI.DragWindow();
        }
    }
}