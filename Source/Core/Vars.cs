namespace Hot_Lava_Cheat.Source.Core
{
    public class Vars
    {
        public struct sTab
        {
            public struct sMain
            {
                public static bool bEnable_ExperienceTo60LVL;
                public static bool bEnableGodMode;
                public static bool bMultiplied100X_Currency;
                public static bool bUnlock_WorldsAndToys;
                public static bool bGetAllCards;
                public static float flGravityMultiplier;
                public static float flVelocityMultiplier;
                public static bool bEnableBhop;
                public static UnityEngine.KeyCode kcBhopKey = UnityEngine.KeyCode.Space;
                public static int iBhopMode = 0;
            }

            public struct sMR
            {
                public static UnityEngine.KeyCode kcRecordKey = UnityEngine.KeyCode.R;
                public static bool bRecordKeyToggle = false;
                public static UnityEngine.KeyCode kcPlaybackKey = UnityEngine.KeyCode.T;
                public static bool bPlaybackKeyToggle = false;
                public static UnityEngine.KeyCode kcRewindKey = UnityEngine.KeyCode.Y;
                public static UnityEngine.KeyCode kcApplyFramesKey = UnityEngine.KeyCode.U;
                public static UnityEngine.KeyCode kcInitNewRecordKey = UnityEngine.KeyCode.I;
                public static UnityEngine.KeyCode kcRewindSpeedUpKey = UnityEngine.KeyCode.UpArrow;
                public static UnityEngine.KeyCode kcRewindSpeedDownKey = UnityEngine.KeyCode.DownArrow;
                public static UnityEngine.KeyCode kcRewindForwardKey = UnityEngine.KeyCode.RightArrow;
                public static UnityEngine.KeyCode kcRewindBackwardKey = UnityEngine.KeyCode.LeftArrow;
                public static int iRecordDelayFrames = 24;
                public static float flRecordSlowmotion = 0f;
                public static int iRewindSpeedIndex = 3;
                public static bool bTeleportPlaybackToStart = false;
            }

            public struct sOther
            {
                public static int iLanguage;
                public static int iTheme;
            }
        }
        
        public struct sGame
        {
            public static bool bIn;
            public static bool bPaused;
        }

        public static Movement.Bhop bhop = new Movement.Bhop();

        public struct sKeybindStates
        {
            public static bool bRecordActive;
            public static bool bPlaybackActive;
        }
    }
}