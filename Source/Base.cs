namespace HotLava_Cheat.Source
{
    [BepInEx.BepInPlugin("HotLava.Cheat", "Furion", NS_Core.Main.strCheatVersion)]
    public class Base : BepInEx.BaseUnityPlugin
    {
        private readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("HotLava.Cheat");
        private static Base Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            NS_Core.Logger.Init();
            NS_Core.Main.InitGameVersion();

            this.harmony.PatchAll(typeof(Base));
            this.harmony.PatchAll(typeof(NS_Patches.PlayerController));
            this.harmony.PatchAll(typeof(NS_Patches.CharacterStatistic));
            this.harmony.PatchAll(typeof(NS_Patches.PauseMenu));
            this.harmony.PatchAll(typeof(NS_Patches.Input));
            this.harmony.PatchAll(typeof(NS_Patches.InputManager));
            this.harmony.PatchAll(typeof(NS_Patches.Jumper));
            this.harmony.PatchAll(typeof(NS_Patches.LevelIntroPatch));
            this.harmony.PatchAll(typeof(NS_Patches.MouseLook));
            this.harmony.PatchAll(typeof(NS_Patches.SubSplashScreenPatch));
            this.harmony.PatchAll(typeof(NS_Patches.UIKeyboardPatch));
            this.harmony.PatchAll(typeof(NS_Patches.OnlineController));
            this.harmony.PatchAll(typeof(NS_Patches.ScoreBoardPatch));
            this.harmony.PatchAll(typeof(NS_Patches.Currency));
            this.harmony.PatchAll(typeof(NS_Patches.Statistics));
            this.harmony.PatchAll(typeof(NS_Patches.MainMenu));
            this.harmony.PatchAll(typeof(NS_Patches.CardPanel));
            this.harmony.PatchAll(typeof(NS_Patches.CursorIconPatch));

            NS_Visuals.Textures.CreateAll();
            NS_Core.Config.Load();

            if (!System.IO.File.Exists(NS_Core.Config.GetConfigFilePath()))
                NS_Core.Config.Save();

            NS_Visuals.Render.Game.Initialize();
            NS_Core.Binds.Initialize();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Insert))
                NS_Visuals.GUIManager.bShowGUI = !NS_Visuals.GUIManager.bShowGUI;

            NS_Core.Utils.UpdateCursorState();
            NS_Core.KeybindSystem.ProcessKeybinds();
            NS_Core.Binds.Update();
        }

        private void OnGUI()
        {
            NS_Visuals.GUIManager.Render(this);
            NS_Visuals.Render.Game.Render();
        }

        public void WindowFunction(int iWindowID)
        {
            NS_Visuals.GUIManager.WindowFunction(iWindowID);
        }
    }
}