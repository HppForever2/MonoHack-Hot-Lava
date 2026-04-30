namespace Hot_Lava_Cheat.Source.Core
{
    public static class Binds
    {
        private static Klei.HotLava.Character.PlayerController cachedPlayer;
        private static float flLastPlayerSearchTime = -10f;
        private static bool bInitialized = false;
        private static bool bLastRewindKeyState = false;
        private static bool bLastApplyKeyState = false;
        private static bool bLastInitNewRecordKeyState = false;
        private static bool bLastRewindSpeedUpKeyState = false;
        private static bool bLastRewindSpeedDownKeyState = false;

        public static void Initialize()
        {
            if (bInitialized)
                return;

            NS_Core.KeybindSystem.RegisterKeybind(() => NS_Core.Vars.sTab.sMR.kcRecordKey, () => NS_Core.Vars.sTab.sMR.bRecordKeyToggle, HandleRecordKeybind, "Record");
            NS_Core.KeybindSystem.RegisterKeybind(() => NS_Core.Vars.sTab.sMR.kcPlaybackKey, () => NS_Core.Vars.sTab.sMR.bPlaybackKeyToggle, HandlePlaybackKeybind, "Playback");

            bInitialized = true;
        }

        public static void Update()
        {
            bool bBlocked = NS_Core.Utils.IsGameplayInputBlocked();

            if (bBlocked)
            {
                ResetManualStates();
                NS_Core.Movement.Record.SetRewindDirection(0);

                return;
            }

            UpdateManualActionBinds();
            UpdateRewindControlBinds();
        }

        public static Klei.HotLava.Character.PlayerController GetLocalPlayer()
        {
            if (cachedPlayer != null && cachedPlayer.IsMine)
                return cachedPlayer;

            if (UnityEngine.Time.time - flLastPlayerSearchTime < 0.25f)
                return null;

            flLastPlayerSearchTime = UnityEngine.Time.time;
            Klei.HotLava.Character.PlayerController[] players = UnityEngine.Object.FindObjectsOfType<Klei.HotLava.Character.PlayerController>();

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].IsMine)
                {
                    cachedPlayer = players[i];

                    return cachedPlayer;
                }
            }

            return null;
        }

        private static bool TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player)
        {
            player = GetLocalPlayer();
            return player != null;
        }

        private static bool CanStartRecord()
        {
            return !NS_Core.Movement.Record.IsPreparingRecord && !NS_Core.Movement.Record.IsRecording && !NS_Core.Movement.Record.IsPlaying && !NS_Core.Movement.Record.IsAutoApproaching;
        }

        private static bool CanStartPlayback()
        {
            return !NS_Core.Movement.Record.IsPreparingRecord && !NS_Core.Movement.Record.IsRecording && !NS_Core.Movement.Record.IsPlaying && !NS_Core.Movement.Record.IsAutoApproaching && !NS_Core.Movement.Record.IsRewinding;
        }

        private static void HandleRecordKeybind(bool bPressed)
        {
            if (NS_Core.Vars.sTab.sMR.bRecordKeyToggle)
            {
                if (!bPressed || !TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player))
                    return;

                if (NS_Core.Movement.Record.IsRecordStartRangePlayback)
                {
                    NS_Core.Movement.Record.StopRecording();
                    return;
                }

                if (!NS_Core.Movement.Record.IsPreparingRecord && !NS_Core.Movement.Record.IsRecording)
                {
                    NS_Core.Movement.Record.StartRecording(player);
                    return;
                }

                NS_Core.Movement.Record.StopRecording();
                return;
            }

            if (bPressed)
            {
                if (!TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player) || !CanStartRecord())
                    return;

                NS_Core.Movement.Record.StartRecording(player);
                return;
            }

            if (NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsRecording || NS_Core.Movement.Record.IsRecordStartRangePlayback)
                NS_Core.Movement.Record.StopRecording();
        }

        private static void HandlePlaybackKeybind(bool bPressed)
        {
            if (NS_Core.Vars.sTab.sMR.bPlaybackKeyToggle)
            {
                if (!bPressed || !TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player))
                    return;

                if (NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsRecording || NS_Core.Movement.Record.IsRewinding)
                    return;

                if (!NS_Core.Movement.Record.IsPlaying && !NS_Core.Movement.Record.IsAutoApproaching)
                    NS_Core.Movement.Record.StartPlayback(player);

                else
                    NS_Core.Movement.Record.StopPlayback();

                return;
            }

            if (bPressed)
            {
                if (!TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player) || !CanStartPlayback())
                    return;

                NS_Core.Movement.Record.StartPlayback(player);
                return;
            }

            if (NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsAutoApproaching)
                NS_Core.Movement.Record.StopPlayback();
        }

        private static void UpdateManualActionBinds()
        {
            bool bRewindKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindKey);

            if (bRewindKeyState && !bLastRewindKeyState && TryGetLocalPlayer(out Klei.HotLava.Character.PlayerController player))
                NS_Core.Movement.Record.ToggleRewind(player);

            bLastRewindKeyState = bRewindKeyState;

            bool bApplyKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcApplyFramesKey);

            if (bApplyKeyState && !bLastApplyKeyState)
                NS_Core.Movement.Record.ApplyStagedFrames();

            bLastApplyKeyState = bApplyKeyState;

            bool bInitNewRecordKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcInitNewRecordKey);

            if (bInitNewRecordKeyState && !bLastInitNewRecordKeyState)
                NS_Core.Movement.Record.InitNewRecord();

            bLastInitNewRecordKeyState = bInitNewRecordKeyState;
        }

        private static void UpdateRewindControlBinds()
        {
            if (!NS_Core.Movement.Record.IsRewinding)
            {
                NS_Core.Movement.Record.SetRewindDirection(0);

                bLastRewindSpeedUpKeyState = false;
                bLastRewindSpeedDownKeyState = false;

                return;
            }

            bool bForward = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindForwardKey);
            bool bBackward = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindBackwardKey);
            int iDirection = 0;

            if (bForward != bBackward)
                iDirection = bForward ? 1 : -1;

            NS_Core.Movement.Record.SetRewindDirection(iDirection);

            bool bSpeedUp = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindSpeedUpKey);

            if (bSpeedUp && !bLastRewindSpeedUpKeyState)
                NS_Core.Movement.Record.IncreaseRewindSpeed();

            bLastRewindSpeedUpKeyState = bSpeedUp;

            bool bSpeedDown = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindSpeedDownKey);

            if (bSpeedDown && !bLastRewindSpeedDownKeyState)
                NS_Core.Movement.Record.DecreaseRewindSpeed();

            bLastRewindSpeedDownKeyState = bSpeedDown;
        }

        private static void ResetManualStates()
        {
            bLastRewindKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindKey);
            bLastApplyKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcApplyFramesKey);
            bLastInitNewRecordKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcInitNewRecordKey);
            bLastRewindSpeedUpKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindSpeedUpKey);
            bLastRewindSpeedDownKeyState = NS_Core.Utils.GetKeyState(NS_Core.Vars.sTab.sMR.kcRewindSpeedDownKey);
        }
    }
}