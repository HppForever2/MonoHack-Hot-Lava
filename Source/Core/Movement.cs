namespace Hot_Lava_Cheat.Source.Core
{
    public class Movement
    {
        public class Bhop
        {
            private bool bPendingSpeedRestore = false;
            private float flPendingFlatSpeed = 0f;
            private float flPendingSpeedRestoreExpireTime = 0f;

            public void Reset()
            {
                bPendingSpeedRestore = false;
                flPendingFlatSpeed = 0f;
                flPendingSpeedRestoreExpireTime = 0f;
            }

            public void PrepareSpeedRestore(Klei.HotLava.Character.PlayerController player)
            {
                if (player == null || !player.IsMine)
                    return;

                bPendingSpeedRestore = true;
                flPendingFlatSpeed = player.FlatSpeed;
                flPendingSpeedRestoreExpireTime = UnityEngine.Time.time + UnityEngine.Time.fixedDeltaTime * 1.5f;
            }

            public bool TryConsumeSpeedRestore(out float flFlatSpeed)
            {
                if (!bPendingSpeedRestore || UnityEngine.Time.time > flPendingSpeedRestoreExpireTime)
                {
                    Reset();

                    flFlatSpeed = 0f;
                    return false;
                }

                flFlatSpeed = flPendingFlatSpeed;
                Reset();

                return true;
            }
        }

        public class Record
        {
            public struct Frame
            {
                public UnityEngine.Vector3 position;
                public UnityEngine.Vector3 velocity;
                public UnityEngine.Quaternion rotation;
                public UnityEngine.Vector2 cameraAngles;
                public UnityEngine.Vector2 mouseInput;
                public bool jump;
                public bool landingJump;
                public bool crouch;
                public bool action;
                public float scrollWheel;
                public UnityEngine.Vector2 moveInput;
                public float bunnyHopPercent;
                public float bunnyHopBonusPercent;
                public float bunnyHopModifier;
                public float bunnyHopDirection;
                public bool justJumped;
                public bool holdDirection;
                public float actionPressedRampValue;
                public int gameModeIndex;
                public int subGameModeIndex;
                public int segmentIndex;
                public bool sectionTransition;
                public bool teleportTransition;
            }

            private static readonly System.Collections.Generic.List<Frame> recordedFrames = new System.Collections.Generic.List<Frame>();
            private static readonly System.Collections.Generic.List<Frame> stagedFrames = new System.Collections.Generic.List<Frame>();
            private static readonly float[] arrRewindSpeeds = new float[] { 0f, 0.25f, 0.5f, 1f, 2f, 4f, 8f, 16f };

            private static bool isPreparingRecord = false;
            private static bool isRecording = false;
            private static bool isPlaying = false;
            private static bool isRewinding = false;
            private static bool isAutoApproaching = false;
            private static bool shouldKeepCameraAngles = false;
            private static bool bRangePlaybackActive = false;
            private static bool bRangePlaybackStartsPreparedRecord = false;
            private static bool lastJumpState = false;
            private static int iPendingPlaybackCancelCount = 0;
            private static bool bIssuedPlaybackCancelForSectionRouteChange = false;
            private static bool bPendingRecordedSyntheticJump = false;
            private static bool bHasLiveRecordedCameraAngles = false;
            private static bool bHasAppliedPlaybackCameraAngles = false;
            private static bool bHasAppliedAutoApproachCameraAngles = false;
            private static bool bHasPreparedRecordFrame = false;
            private static bool bAppliedRecordSlowmotion = false;
            private static bool bPreparedRecordUsesRecordedInput = false;
            private static bool bRecordedFramesHaveBunnyHopState = false;

            private static int playbackFrame = 0;
            private static int playbackTargetFrame = 0;
            private static int rewindFrameIndex = 0;
            private static int editResumeFrameIndex = -1;
            private static int stagedBaseFrameIndex = -1;
            private static int stagedSegmentIndex = 0;
            private static int recordDelayRemainingFrames = 0;
            private static int rewindDirection = 0;

            private static float currentDesync = 0f;
            private static float flPendingRecordedScrollWheel = 0f;
            private static float flLastPlaybackFrameTime = -1f;
            private static float flRewindFrameAccumulator = 0f;
            private static float flPreviousTimeScale = 1f;
            private static float flCenterNotificationUntil = -1f;

            private static string strRecordedLevelName = string.Empty;
            private static string strRecordedPathId = string.Empty;
            private static string strRecordedPathDisplay = string.Empty;
            private static string strRecordedSectionDisplay = string.Empty;
            private static string strRecordFileName = string.Empty;
            private static string strPendingRecordLevelName = string.Empty;
            private static string strPendingRecordPathId = string.Empty;
            private static string strPendingRecordPathDisplay = string.Empty;
            private static string strPendingRecordSectionDisplay = string.Empty;
            private static string strCenterNotification = string.Empty;

            private static UnityEngine.Vector3 recordStartPosition;
            private static UnityEngine.Vector2 recordStartCameraAngles;
            private static UnityEngine.Vector2 liveRecordedCameraAngles;
            private static UnityEngine.Vector2 lastAppliedPlaybackCameraAngles;
            private static UnityEngine.Vector2 lastAppliedAutoApproachCameraAngles;
            private static UnityEngine.Vector2 lastCameraAngles;
            private static Frame currentPlaybackFrame;
            private static Frame preparedRecordFrame;

            private static readonly System.Reflection.PropertyInfo piInfoCurrentLevelName = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.Info), "CurrentLevelName");
            private static readonly System.Reflection.PropertyInfo piInfoCurrentLevelMetaData = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.Info), "CurrentLevelMetaData");
            private static readonly System.Reflection.PropertyInfo piInfoCurrentGameMode = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.Info), "CurrentGameMode");
            private static readonly System.Reflection.PropertyInfo piInfoCurrentGameModeIndex = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.Info), "CurrentGameModeIndex");
            private static readonly System.Reflection.PropertyInfo piInfoSubGameModeIndex = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.Info), "SubGameModeIndex");
            private static readonly System.Reflection.MethodInfo miInfoGetGameModeName = HarmonyLib.AccessTools.Method(typeof(Klei.HotLava.Game.Info), "GetGameModeName", new System.Type[] { typeof(Klei.HotLava.LevelMetaData), typeof(Klei.HotLava.Game.GameMode), typeof(bool) });
            private static readonly System.Reflection.MethodInfo miInfoSetCurrentGameMode = HarmonyLib.AccessTools.Method(typeof(Klei.HotLava.Game.Info), "SetCurrentGameMode", new System.Type[] { typeof(int), typeof(int) });
            private static readonly System.Reflection.PropertyInfo piGameModeId = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Game.GameMode), "ID");

            public static bool IsPreparingRecord => isPreparingRecord;
            public static bool IsRecording => isRecording;
            public static bool IsPlaying => isPlaying;
            public static bool IsRewinding => isRewinding;
            public static bool IsRangePlayback => isPlaying && bRangePlaybackActive;
            public static bool IsRecordStartRangePlayback => IsRangePlayback && bRangePlaybackStartsPreparedRecord;
            public static bool ShouldKeepCameraAngles => shouldKeepCameraAngles;
            public static bool IsAutoApproaching => isAutoApproaching;

            public static int CurrentFrame
            {
                get
                {
                    if (isRecording)
                        return stagedFrames.Count;

                    if (isPlaying)
                        return playbackFrame;

                    if (isRewinding)
                        return rewindFrameIndex + 1;

                    return recordedFrames.Count;
                }
            }

            public static int TotalFrames => recordedFrames.Count;
            public static int EditableFramesCount => GetEditableFramesCount();
            public static int StagedFramesCount => stagedFrames.Count;
            public static int RecordDelayRemainingFrames => UnityEngine.Mathf.Max(0, recordDelayRemainingFrames);
            public static int RecordDelayTotalFrames => UnityEngine.Mathf.Max(0, NS_Core.Vars.sTab.sMR.iRecordDelayFrames);
            public static float RecordSlowmotionScale => 1f - UnityEngine.Mathf.Clamp(NS_Core.Vars.sTab.sMR.flRecordSlowmotion, 0f, 0.70f);
            public static float CurrentRewindSpeed => GetConfiguredRewindSpeed();
            public static int RewindFramesCount => recordedFrames.Count;
            public static UnityEngine.Vector3 RecordStartPosition => recordedFrames.Count > 0 ? recordedFrames[0].position : recordStartPosition;
            public static Frame CurrentPlaybackFrame => currentPlaybackFrame;
            public static bool HasCurrentRewindFrame => isRewinding && recordedFrames.Count > 0;
            public static Frame CurrentRewindFrame => HasCurrentRewindFrame ? GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1)) : currentPlaybackFrame;
            public static bool IsPausedRewind => isRewinding && rewindDirection == 0 && recordedFrames.Count > 0;
            public static float CurrentRewindFrameSpeed => IsPausedRewind ? new UnityEngine.Vector2(CurrentRewindFrame.velocity.x, CurrentRewindFrame.velocity.z).magnitude : 0f;
            public static bool HasVisualInputFrame => (recordedFrames.Count > 0 && (isPlaying || isRewinding)) || (isPreparingRecord && bHasPreparedRecordFrame);

            public static Frame VisualInputFrame
            {
                get
                {
                    if (isRewinding)
                        return CurrentRewindFrame;

                    if (isPreparingRecord && bHasPreparedRecordFrame)
                        return preparedRecordFrame;

                    if (isPlaying)
                        return recordedFrames[UnityEngine.Mathf.Clamp(playbackFrame - 1, 0, recordedFrames.Count - 1)];

                    return currentPlaybackFrame;
                }
            }

            public static bool IsJumpDown => isPlaying && currentPlaybackFrame.jump && !lastJumpState;
            public static bool IsRewindJumpDown => isRewinding && rewindFrameIndex >= 0 && rewindFrameIndex < recordedFrames.Count && GetRewindFrame(rewindFrameIndex).jump && !(rewindFrameIndex > 0 && GetRewindFrame(rewindFrameIndex - 1).jump);
            public static float CurrentDesync => currentDesync;
            public static bool HasPendingPlaybackFrame => isPlaying && playbackFrame >= 0 && playbackFrame < recordedFrames.Count;
            public static int PendingPlaybackFrameIndex => playbackFrame;
            public static Frame PendingPlaybackFrame => HasPendingPlaybackFrame ? recordedFrames[playbackFrame] : currentPlaybackFrame;
            public static bool HasBufferedPlaybackLandingJump => HasPlaybackLandingJumpBuffered();
            public static bool IsPendingJumpDown => isPlaying && playbackFrame >= 0 && playbackFrame < recordedFrames.Count && recordedFrames[playbackFrame].jump && !(playbackFrame > 0 && recordedFrames[playbackFrame - 1].jump);
            public static int CurrentSegment => GetCurrentSegmentNumber();
            public static int TotalSegments => GetCurrentTotalSegmentCount();
            public static string RecordedPathDisplay => strRecordedPathDisplay;
            public static string RecordedPathFileDisplay => GetRecordedPathFileDisplay();
            public static string RecordedSectionDisplay => strRecordedSectionDisplay;
            public static string CurrentPathDisplay => GetCurrentPathDisplay();
            public static string CurrentSectionDisplay => GetCurrentSectionDisplayZeroBased();
            public static int PlaybackTargetFrame => playbackTargetFrame;
            public static bool ShouldUsePreparedRecordInput => isPreparingRecord && bHasPreparedRecordFrame && bPreparedRecordUsesRecordedInput;
            public static Frame PreparedRecordFrame => preparedRecordFrame;
            public static bool HasCenterNotification => !string.IsNullOrWhiteSpace(strCenterNotification) && UnityEngine.Time.unscaledTime < flCenterNotificationUntil;
            public static string CenterNotificationText => HasCenterNotification ? strCenterNotification : string.Empty;
            public static bool HasOverlayState => recordedFrames.Count > 0 || stagedFrames.Count > 0 || isPreparingRecord || isRecording || isPlaying || isRewinding || isAutoApproaching || HasCenterNotification;
            public static bool HasRecordedBunnyHopState => bRecordedFramesHaveBunnyHopState;

            public static void StartRecording(Klei.HotLava.Character.PlayerController player)
            {
                if (player == null || !player.IsMine)
                    return;

                if ((isPlaying && !bRangePlaybackStartsPreparedRecord) || isAutoApproaching)
                {
                    NS_Core.Logger.Log("Cannot start recording while playback or auto-approach is active");
                    return;
                }

                if (recordedFrames.Count > 0 && !IsCurrentLevelMatchingRecording())
                {
                    NS_Core.Logger.Log($"Cannot start recording edit: recorded level = {strRecordedLevelName}, current level = {GetCurrentLevelName()}");
                    return;
                }

                bool bStartedFromRewind = isRewinding;

                if (isRewinding)
                {
                    editResumeFrameIndex = GetClampedEditResumeFrameIndex();
                    StopRewind(false);
                }

                int iStartFrameIndex = -1;

                if (!isPreparingRecord && !isRecording && GetEditableFramesCount() > 0)
                {
                    editResumeFrameIndex = !bStartedFromRewind && stagedFrames.Count > 0 ? GetStagedRestartFrameIndex() : GetClampedEditResumeFrameIndex();
                    iStartFrameIndex = editResumeFrameIndex;

                    TrimStagedFramesForEditIndex(iStartFrameIndex);

                    if (!bStartedFromRewind && stagedFrames.Count > 0)
                        NS_Core.Logger.Log($"Restarting staged recording from frame {iStartFrameIndex}");
                }

                flPendingRecordedScrollWheel = 0f;
                bPendingRecordedSyntheticJump = false;
                bHasLiveRecordedCameraAngles = false;
                shouldKeepCameraAngles = false;
                iPendingPlaybackCancelCount = 0;
                bIssuedPlaybackCancelForSectionRouteChange = false;
                strPendingRecordLevelName = GetCurrentLevelName();
                strPendingRecordPathId = GetCurrentPathId();
                strPendingRecordPathDisplay = GetCurrentPathDisplay();
                strPendingRecordSectionDisplay = GetCurrentSectionDisplayZeroBased();

                if (GetEditableFramesCount() > 0)
                {
                    iStartFrameIndex = iStartFrameIndex >= 0 ? iStartFrameIndex : GetClampedEditResumeFrameIndex();

                    if (stagedFrames.Count <= 0 && recordedFrames.Count > 0)
                        stagedBaseFrameIndex = iStartFrameIndex;

                    stagedSegmentIndex = GetEditableFrame(iStartFrameIndex).segmentIndex + 1;
                    BeginPreparedRecordingFromFrame(player, GetEditableFrame(iStartFrameIndex), iStartFrameIndex, true);

                    return;
                }

                stagedBaseFrameIndex = -1;
                stagedSegmentIndex = 0;
                BeginPreparedRecordingFromFrame(player, BuildLiveFrame(player), -1, true);
            }

            public static void StopRecording()
            {
                if (bRangePlaybackStartsPreparedRecord && bRangePlaybackActive)
                {
                    isPlaying = false;
                    bRangePlaybackActive = false;
                    bRangePlaybackStartsPreparedRecord = false;
                    playbackFrame = 0;
                    playbackTargetFrame = 0;
                    shouldKeepCameraAngles = false;

                    RestoreRecordSlowmotion();
                    NS_Core.Logger.Log("Record seek cancelled");

                    return;
                }

                bool bWasPreparingRecord = isPreparingRecord;
                bool bWasRecording = isRecording;
                int iStagedFrameCount = stagedFrames.Count;

                isPreparingRecord = false;
                isRecording = false;
                shouldKeepCameraAngles = false;
                bHasPreparedRecordFrame = false;
                bPreparedRecordUsesRecordedInput = false;
                recordDelayRemainingFrames = 0;
                flPendingRecordedScrollWheel = 0f;
                bPendingRecordedSyntheticJump = false;

                RestoreRecordSlowmotion();

                if (!bWasRecording)
                {
                    stagedFrames.Clear();
                    stagedBaseFrameIndex = -1;
                }

                if (bWasPreparingRecord && !bWasRecording)
                {
                    NS_Core.Logger.Log("Record prepare cancelled");
                    return;
                }

                NS_Core.Logger.Log($"Recording stopped. Staged frames: {iStagedFrameCount}");
            }

            public static void InitNewRecord()
            {
                Clear();
                ShowCenterNotification(NS_Core.Utils.Lang.InEN() ? "NEW RECORD INITIALIZED" : "\u041D\u041E\u0412\u0410\u042F \u0417\u0410\u041F\u0418\u0421\u042C \u0418\u041D\u0418\u0426\u0418\u0410\u041B\u0418\u0417\u0418\u0420\u041E\u0412\u0410\u041D\u0410");

                NS_Core.Logger.Log("New record initialized");
            }

            public static void SaveRecord()
            {
                SaveRecord(GetSuggestedSaveName());
            }

            public static void SaveRecord(string strName)
            {
                System.Collections.Generic.List<Frame> framesToSave = BuildEditableFrameSnapshot();

                if (framesToSave.Count <= 0)
                {
                    ShowCenterNotification(NS_Core.Utils.Lang.GetStr("NOTHING TO SAVE", "НЕЧЕГО СОХРАНЯТЬ"));
                    NS_Core.Logger.Log("SaveRecord aborted: no editable frames");

                    return;
                }

                try
                {
                    string strFolderPath = GetCurrentRecordFolderPath();
                    string strLevelName = !string.IsNullOrWhiteSpace(strRecordedLevelName) ? strRecordedLevelName : (!string.IsNullOrWhiteSpace(strPendingRecordLevelName) ? strPendingRecordLevelName : GetCurrentLevelName());
                    string strPathId = !string.IsNullOrWhiteSpace(strRecordedPathId) ? strRecordedPathId : (!string.IsNullOrWhiteSpace(strPendingRecordPathId) ? strPendingRecordPathId : GetCurrentPathId());
                    string strPathDisplay = !string.IsNullOrWhiteSpace(strRecordedPathDisplay) ? strRecordedPathDisplay : (!string.IsNullOrWhiteSpace(strPendingRecordPathDisplay) ? strPendingRecordPathDisplay : GetCurrentPathDisplay());
                    string strSectionDisplay = !string.IsNullOrWhiteSpace(strRecordedSectionDisplay) ? strRecordedSectionDisplay : (!string.IsNullOrWhiteSpace(strPendingRecordSectionDisplay) ? strPendingRecordSectionDisplay : GetCurrentSectionDisplayZeroBased());

                    UnityEngine.Vector3 vecStartPosition = framesToSave[0].position;
                    UnityEngine.Vector2 vecStartAngles = framesToSave[0].cameraAngles;

                    if (!System.IO.Directory.Exists(strFolderPath))
                        System.IO.Directory.CreateDirectory(strFolderPath);

                    string strSafeName = SanitizeRecordFilePart(strName);

                    if (string.IsNullOrWhiteSpace(strSafeName))
                        strSafeName = "record";

                    string strFilePath = System.IO.Path.Combine(strFolderPath, $"{strSafeName}_save.mr");

                    using (System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))

                    using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs, System.Text.Encoding.UTF8))
                    {
                        bw.Write("FMR4");
                        bw.Write(strLevelName ?? string.Empty);
                        bw.Write(strPathId ?? string.Empty);
                        bw.Write(strPathDisplay ?? string.Empty);
                        bw.Write(strSectionDisplay ?? string.Empty);
                        bw.Write(vecStartPosition.x);
                        bw.Write(vecStartPosition.y);
                        bw.Write(vecStartPosition.z);
                        bw.Write(vecStartAngles.x);
                        bw.Write(vecStartAngles.y);
                        bw.Write(framesToSave.Count);

                        for (int i = 0; i < framesToSave.Count; i++)
                        {
                            Frame frame = framesToSave[i];

                            bw.Write(frame.position.x);
                            bw.Write(frame.position.y);
                            bw.Write(frame.position.z);
                            bw.Write(frame.velocity.x);
                            bw.Write(frame.velocity.y);
                            bw.Write(frame.velocity.z);
                            bw.Write(frame.rotation.x);
                            bw.Write(frame.rotation.y);
                            bw.Write(frame.rotation.z);
                            bw.Write(frame.rotation.w);
                            bw.Write(frame.cameraAngles.x);
                            bw.Write(frame.cameraAngles.y);
                            bw.Write(frame.mouseInput.x);
                            bw.Write(frame.mouseInput.y);
                            bw.Write(frame.jump);
                            bw.Write(frame.landingJump);
                            bw.Write(frame.crouch);
                            bw.Write(frame.action);
                            bw.Write(frame.scrollWheel);
                            bw.Write(frame.moveInput.x);
                            bw.Write(frame.moveInput.y);
                            bw.Write(frame.bunnyHopPercent);
                            bw.Write(frame.bunnyHopBonusPercent);
                            bw.Write(frame.bunnyHopModifier);
                            bw.Write(frame.bunnyHopDirection);
                            bw.Write(frame.justJumped);
                            bw.Write(frame.holdDirection);
                            bw.Write(frame.actionPressedRampValue);
                            bw.Write(frame.gameModeIndex);
                            bw.Write(frame.subGameModeIndex);
                            bw.Write(frame.segmentIndex);
                            bw.Write(frame.sectionTransition);
                            bw.Write(frame.teleportTransition);
                        }
                    }

                    ShowCenterNotification(NS_Core.Utils.Lang.InEN() ? $"SAVED {framesToSave.Count} FRAMES" : $"СОХРАНЕНО {framesToSave.Count} КАДРОВ");
                    strRecordFileName = System.IO.Path.GetFileName(strFilePath);

                    NS_Core.Logger.Log($"Record saved to {strFilePath}. Frames={framesToSave.Count}");
                }

                catch (System.Exception ex)
                {
                    ShowCenterNotification(NS_Core.Utils.Lang.GetStr("SAVE ERROR", "ОШИБКА СОХРАНЕНИЯ"));
                    NS_Core.Logger.Log($"SaveRecord error: {ex}");
                }
            }

            public static bool LoadRecord(string strFilePath)
            {
                if (string.IsNullOrWhiteSpace(strFilePath) || !System.IO.File.Exists(strFilePath))
                {
                    ShowCenterNotification(NS_Core.Utils.Lang.GetStr("SAVE NOT FOUND", "СОХРАНЕНИЕ НЕ НАЙДЕНО"));
                    return false;
                }

                try
                {
                    string strMagic;
                    string strLoadedLevelName;
                    string strLoadedPathId;
                    string strLoadedPathDisplay;
                    string strLoadedSectionDisplay;

                    UnityEngine.Vector3 vecStartPosition;
                    UnityEngine.Vector2 vecStartCameraAngles;

                    System.Collections.Generic.List<Frame> loadedFrames = new System.Collections.Generic.List<Frame>();

                    bool bHasBunnyHopState;

                    using (System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))

                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(fs, System.Text.Encoding.UTF8))
                    {
                        strMagic = br.ReadString();

                        if (string.Equals(strMagic, "FMR4", System.StringComparison.Ordinal) || string.Equals(strMagic, "HLMR1", System.StringComparison.Ordinal))
                            bHasBunnyHopState = true;

                        else
                            throw new System.IO.InvalidDataException("Invalid MR file magic");

                        strLoadedLevelName = br.ReadString();
                        strLoadedPathId = br.ReadString();
                        strLoadedPathDisplay = br.ReadString();
                        strLoadedSectionDisplay = br.ReadString();
                        vecStartPosition = new UnityEngine.Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                        vecStartCameraAngles = new UnityEngine.Vector2(br.ReadSingle(), br.ReadSingle());

                        int iFrameCount = br.ReadInt32();

                        if (iFrameCount < 0)
                            throw new System.IO.InvalidDataException("Negative frame count");

                        for (int i = 0; i < iFrameCount; i++)
                        {
                            Frame frame = new Frame();

                            frame.position = new UnityEngine.Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                            frame.velocity = new UnityEngine.Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                            frame.rotation = new UnityEngine.Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                            frame.cameraAngles = new UnityEngine.Vector2(br.ReadSingle(), br.ReadSingle());
                            frame.mouseInput = new UnityEngine.Vector2(br.ReadSingle(), br.ReadSingle());
                            frame.jump = br.ReadBoolean();
                            frame.landingJump = br.ReadBoolean();
                            frame.crouch = br.ReadBoolean();
                            frame.action = br.ReadBoolean();
                            frame.scrollWheel = br.ReadSingle();
                            frame.moveInput = new UnityEngine.Vector2(br.ReadSingle(), br.ReadSingle());
                            frame.bunnyHopPercent = bHasBunnyHopState ? br.ReadSingle() : 0f;
                            frame.bunnyHopBonusPercent = bHasBunnyHopState ? br.ReadSingle() : 0f;
                            frame.bunnyHopModifier = bHasBunnyHopState ? br.ReadSingle() : 0f;

                            frame.bunnyHopDirection = br.ReadSingle();
                            frame.justJumped = br.ReadBoolean();
                            frame.holdDirection = br.ReadBoolean();
                            frame.actionPressedRampValue = br.ReadSingle();

                            frame.gameModeIndex = br.ReadInt32();
                            frame.subGameModeIndex = br.ReadInt32();
                            frame.segmentIndex = br.ReadInt32();
                            frame.sectionTransition = br.ReadBoolean();
                            frame.teleportTransition = br.ReadBoolean();

                            loadedFrames.Add(frame);
                        }
                    }

                    Clear();

                    if (loadedFrames.Count > 0)
                        recordedFrames.AddRange(loadedFrames);

                    RebuildTransitions(recordedFrames);
                    NormalizeSegmentIndices(recordedFrames);

                    bRecordedFramesHaveBunnyHopState = bHasBunnyHopState;
                    strRecordedLevelName = strLoadedLevelName ?? string.Empty;
                    strRecordedPathId = strLoadedPathId ?? string.Empty;
                    strRecordedPathDisplay = strLoadedPathDisplay ?? string.Empty;
                    strRecordedSectionDisplay = strLoadedSectionDisplay ?? string.Empty;
                    recordStartPosition = recordedFrames.Count > 0 ? recordedFrames[0].position : vecStartPosition;
                    recordStartCameraAngles = recordedFrames.Count > 0 ? recordedFrames[0].cameraAngles : vecStartCameraAngles;
                    lastCameraAngles = recordStartCameraAngles;
                    lastAppliedPlaybackCameraAngles = recordStartCameraAngles;
                    currentPlaybackFrame = recordedFrames.Count > 0 ? recordedFrames[0] : new Frame();
                    editResumeFrameIndex = recordedFrames.Count > 0 ? recordedFrames.Count - 1 : -1;
                    rewindFrameIndex = editResumeFrameIndex >= 0 ? editResumeFrameIndex : 0;
                    strRecordFileName = System.IO.Path.GetFileName(strFilePath);

                    ShowCenterNotification(NS_Core.Utils.Lang.InEN() ? $"LOADED {recordedFrames.Count} FRAMES" : $"ЗАГРУЖЕНО {recordedFrames.Count} КАДРОВ");
                    NS_Core.Logger.Log($"Record loaded from {strFilePath}. Frames={recordedFrames.Count}, route={GetRecordedRouteDisplay()}");

                    return true;
                }

                catch (System.Exception ex)
                {
                    ShowCenterNotification(NS_Core.Utils.Lang.GetStr("LOAD ERROR", "ОШИБКА ЗАГРУЗКИ"));
                    NS_Core.Logger.Log($"LoadRecord error: {ex}");

                    return false;
                }
            }

            public static string[] GetAvailableRecordFiles()
            {
                try
                {
                    string strFolderPath = GetCurrentCourseRecordFolderPath();

                    if (!System.IO.Directory.Exists(strFolderPath))
                        return System.Array.Empty<string>();

                    string[] arrFiles = System.IO.Directory.GetFiles(strFolderPath, "*_save.mr", System.IO.SearchOption.TopDirectoryOnly);

                    System.Array.Sort(arrFiles, System.StringComparer.OrdinalIgnoreCase);

                    return arrFiles;
                }

                catch (System.Exception ex)
                {
                    NS_Core.Logger.Log($"GetAvailableRecordFiles error: {ex}");
                    return System.Array.Empty<string>();
                }
            }

            public static string GetSuggestedSaveName()
            {
                return "record";
            }

            public static void ShowNotification(string strText)
            {
                ShowCenterNotification(strText);
            }

            public static void UpdatePreparedRecording(Klei.HotLava.Character.PlayerController player)
            {
                if (!isPreparingRecord || player == null || !player.IsMine || !bHasPreparedRecordFrame)
                    return;

                HoldPreparedRecordFrame(player);

                if (recordDelayRemainingFrames > 0)
                    recordDelayRemainingFrames--;

                if (recordDelayRemainingFrames > 0)
                    return;

                isPreparingRecord = false;
                isRecording = true;
                shouldKeepCameraAngles = false;
                flPendingRecordedScrollWheel = 0f;
                bPendingRecordedSyntheticJump = false;
                bHasLiveRecordedCameraAngles = false;

                stagedFrames.Clear();
                NS_Core.Logger.Log("Prepared recording switched to active recording");
            }

            private static void BeginPreparedRecordingFromFrame(Klei.HotLava.Character.PlayerController player, Frame frame, int iFrameIndex, bool bUseDelay)
            {
                preparedRecordFrame = frame;
                bHasPreparedRecordFrame = true;
                bPreparedRecordUsesRecordedInput = iFrameIndex >= 0;
                recordDelayRemainingFrames = bUseDelay ? UnityEngine.Mathf.Max(0, NS_Core.Vars.sTab.sMR.iRecordDelayFrames) : 0;
                isPreparingRecord = true;
                isRecording = false;
                isPlaying = false;
                bRangePlaybackActive = false;
                bRangePlaybackStartsPreparedRecord = false;
                playbackTargetFrame = 0;
                recordStartCameraAngles = preparedRecordFrame.cameraAngles;
                lastCameraAngles = preparedRecordFrame.cameraAngles;
                shouldKeepCameraAngles = true;

                stagedFrames.Clear();
                ApplyRecordSlowmotion();
                ApplyFrameSnapshot(player, preparedRecordFrame, iFrameIndex, "RECORD PREP", true, true);

                if (recordDelayRemainingFrames <= 0)
                {
                    isPreparingRecord = false;
                    isRecording = true;
                    shouldKeepCameraAngles = false;

                    NS_Core.Logger.Log($"Recording started immediately with slowmo={RecordSlowmotionScale:F2}, editFrame={editResumeFrameIndex}, totalMainFrames={recordedFrames.Count}");
                    return;
                }

                NS_Core.Logger.Log($"Record prepare started. Delay={recordDelayRemainingFrames} frames, slowmo={RecordSlowmotionScale:F2}, editFrame={editResumeFrameIndex}, totalMainFrames={recordedFrames.Count}");
            }

            private static bool StartRangePlaybackToFrame(Klei.HotLava.Character.PlayerController player, int iTargetFrameIndex, bool bBeginPreparedRecordAfterRange, string strReason)
            {
                if (player == null || !player.IsMine || recordedFrames.Count == 0)
                    return false;

                iTargetFrameIndex = UnityEngine.Mathf.Clamp(iTargetFrameIndex, 0, recordedFrames.Count - 1);
                int iAnchorFrameIndex = FindReplayAnchorFrameIndex(iTargetFrameIndex);

                isPreparingRecord = false;
                isRecording = false;
                isPlaying = true;
                isRewinding = false;
                isAutoApproaching = false;
                shouldKeepCameraAngles = true;
                bRangePlaybackActive = true;
                bRangePlaybackStartsPreparedRecord = bBeginPreparedRecordAfterRange;
                playbackFrame = iAnchorFrameIndex + 1;
                playbackTargetFrame = iTargetFrameIndex;
                rewindDirection = 0;
                flRewindFrameAccumulator = 0f;
                flLastPlaybackFrameTime = UnityEngine.Time.time;
                currentDesync = 0f;
                iPendingPlaybackCancelCount = 0;
                bIssuedPlaybackCancelForSectionRouteChange = false;

                ApplyFrameSnapshot(player, recordedFrames[iAnchorFrameIndex], iAnchorFrameIndex, strReason, true, true);
                lastCameraAngles = recordedFrames[iAnchorFrameIndex].cameraAngles;

                return true;
            }

            public static void NotifySyntheticJump()
            {
                if (!isRecording)
                    return;

                bPendingRecordedSyntheticJump = true;
            }

            public static void CaptureLiveInput()
            {
                if (!isRecording)
                    return;

                float scrollWheel = TeamUtility.IO.InputManager.GetAxisRaw("Zoom", TeamUtility.IO.PlayerID.One);

                if (UnityEngine.Mathf.Abs(scrollWheel) > 0f)
                    flPendingRecordedScrollWheel = scrollWheel;
            }

            public static void RecordFrame(Klei.HotLava.Character.PlayerController player)
            {
                if (!isRecording || player == null || !player.IsMine)
                    return;

                if (recordedFrames.Count == 0 && stagedBaseFrameIndex < 0)
                    bRecordedFramesHaveBunnyHopState = true;

                float scrollWheel = flPendingRecordedScrollWheel;
                flPendingRecordedScrollWheel = 0f;
                bool syntheticJump = bPendingRecordedSyntheticJump;
                bPendingRecordedSyntheticJump = false;
                bool jumpButtonDown = TeamUtility.IO.InputManager.GetButtonDown("Jump", TeamUtility.IO.PlayerID.One);
                bool jumpButton = TeamUtility.IO.InputManager.GetButton("Jump", TeamUtility.IO.PlayerID.One);

                UnityEngine.Vector2 moveInput = player.GetCachedInput();
                UnityEngine.Vector2 mouseInput = player.GetCachedMouseInput();

                bool jumpPressed = jumpButton;
                bool crouchPressed = TeamUtility.IO.InputManager.GetButton("Crouch", TeamUtility.IO.PlayerID.One);
                bool actionPressed = TeamUtility.IO.InputManager.GetButton("Action", TeamUtility.IO.PlayerID.One);
                bool holdDirectionPressed = TeamUtility.IO.InputManager.GetButton("HoldDirection", TeamUtility.IO.PlayerID.One);

                UnityEngine.Vector3 velocity = player.RigidBody.velocity;

                int gameModeIndex = GetCurrentGameModeIndex();
                int subGameModeIndex = GetCurrentSubGameModeIndex();
                bool sectionTransition = stagedFrames.Count > 0 && (stagedFrames[stagedFrames.Count - 1].subGameModeIndex != subGameModeIndex || stagedFrames[stagedFrames.Count - 1].gameModeIndex != gameModeIndex);
                bool teleportTransition = stagedFrames.Count > 0 && UnityEngine.Vector3.Distance(stagedFrames[stagedFrames.Count - 1].position, player.RigidBody.position) > 5f;

                UnityEngine.Vector2 cameraAngles = bHasLiveRecordedCameraAngles ? liveRecordedCameraAngles : new UnityEngine.Vector2(player.CameraRotation.eulerAngles.x, player.CameraRotation.eulerAngles.y);

                Frame frame = new Frame
                {
                    position = player.RigidBody.position,
                    velocity = velocity,
                    rotation = player.transform.rotation,
                    cameraAngles = cameraAngles,
                    mouseInput = mouseInput,
                    jump = jumpPressed,
                    landingJump = syntheticJump,
                    crouch = crouchPressed,
                    action = actionPressed,
                    scrollWheel = scrollWheel,
                    moveInput = moveInput,
                    bunnyHopPercent = player.BunnyHopPercent,
                    bunnyHopBonusPercent = player.BunnyHopBonusPercent,
                    bunnyHopModifier = player.BunnyHopModifier,
                    bunnyHopDirection = UnityEngine.Mathf.Abs(player.BunnyHopModifier) > 0.0001f ? UnityEngine.Mathf.Sign(player.BunnyHopModifier) : 0f,
                    justJumped = player.JustJumped,
                    holdDirection = holdDirectionPressed,
                    actionPressedRampValue = player.ActionPressedRampValue,
                    gameModeIndex = gameModeIndex,
                    subGameModeIndex = subGameModeIndex,
                    segmentIndex = stagedSegmentIndex,
                    sectionTransition = sectionTransition,
                    teleportTransition = teleportTransition
                };

                if (sectionTransition || teleportTransition)
                {
                    int iPreviousSection = stagedFrames.Count > 0 ? stagedFrames[stagedFrames.Count - 1].subGameModeIndex : subGameModeIndex;
                    int iPreviousGameMode = stagedFrames.Count > 0 ? stagedFrames[stagedFrames.Count - 1].gameModeIndex : gameModeIndex;

                    NS_Core.Logger.Log($"[RECORD TRANSITION] Frame {stagedFrames.Count}: gameMode {iPreviousGameMode}->{gameModeIndex}, section {iPreviousSection}->{subGameModeIndex}, teleport={teleportTransition}, pos={frame.position}, vel={frame.velocity}");
                }

                if (stagedFrames.Count < 10 || stagedFrames.Count % 50 == 0)
                {
                    NS_Core.Logger.Log($"[RECORD] Frame {stagedFrames.Count}:");
                    NS_Core.Logger.Log($"  Position: {frame.position}");
                    NS_Core.Logger.Log($"  Velocity: {frame.velocity}");
                    NS_Core.Logger.Log($"  Input - moveInput: ({frame.moveInput.x}, {frame.moveInput.y}), jump: {frame.jump}, landingJump: {frame.landingJump} (btnDown:{jumpButtonDown}, synthetic:{syntheticJump}, scroll:{scrollWheel}), crouch: {frame.crouch}, action: {frame.action}");
                    NS_Core.Logger.Log($"  Camera - angles: ({frame.cameraAngles.x}, {frame.cameraAngles.y}), mouseInput: ({frame.mouseInput.x}, {frame.mouseInput.y}), gameMode: {frame.gameModeIndex}, section: {frame.subGameModeIndex}, transition: {frame.sectionTransition}, teleport: {frame.teleportTransition}");
                }

                stagedFrames.Add(frame);
                editResumeFrameIndex = GetEditableLastFrameIndex();
            }

            public static void RefreshRecordedBunnyHopState(Klei.HotLava.Character.PlayerController player, float flCurrentHopDirection, bool bJustJumped)
            {
                if (!isRecording || player == null || !player.IsMine || stagedFrames.Count <= 0)
                    return;

                int iLastFrameIndex = stagedFrames.Count - 1;

                Frame frame = stagedFrames[iLastFrameIndex];

                frame.bunnyHopPercent = player.BunnyHopPercent;
                frame.bunnyHopBonusPercent = player.BunnyHopBonusPercent;
                frame.bunnyHopModifier = player.BunnyHopModifier;
                frame.bunnyHopDirection = flCurrentHopDirection;
                frame.justJumped = bJustJumped;
                frame.holdDirection = player.HoldDirectionPressed;
                frame.actionPressedRampValue = player.ActionPressedRampValue;

                stagedFrames[iLastFrameIndex] = frame;
                bRecordedFramesHaveBunnyHopState = true;
            }

            public static void ApplyRecordedBunnyHopRuntimeState(Klei.HotLava.Character.PlayerController.MovementSettings movementSettings, ref float flAccumulatedBunnyHop, ref float flAccumulatedBunnyHopBonus, ref float flCurrentHopDirection, ref float flBunnyHopModifier, ref bool bJustJumped)
            {
                if (movementSettings == null)
                    return;

                Frame frame;

                if (isPreparingRecord && bHasPreparedRecordFrame)
                    frame = preparedRecordFrame;

                else if (isRewinding && recordedFrames.Count > 0)
                    frame = GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1));

                else if (isPlaying && recordedFrames.Count > 0)
                    frame = currentPlaybackFrame;

                else
                    return;

                flAccumulatedBunnyHop = UnityEngine.Mathf.Clamp(frame.bunnyHopPercent * movementSettings.MaxBunnyHop, 0f, movementSettings.MaxBunnyHop);
                flAccumulatedBunnyHopBonus = UnityEngine.Mathf.Clamp(frame.bunnyHopBonusPercent * movementSettings.MaxBunnyHopBonus, 0f, movementSettings.MaxBunnyHopBonus);
                flCurrentHopDirection = frame.bunnyHopDirection;
                flBunnyHopModifier = frame.bunnyHopModifier;
                bJustJumped = frame.justJumped;
            }

            public static void ApplyStagedFrames()
            {
                if (isPreparingRecord || isRecording || isPlaying || isAutoApproaching)
                    return;

                int iEditableFramesCount = GetEditableFramesCount();

                if (iEditableFramesCount == 0)
                    return;

                int iApplyFrameIndex;

                if (stagedFrames.Count > 0 && !isRewinding)
                    iApplyFrameIndex = GetEditableLastFrameIndex();

                else
                    iApplyFrameIndex = UnityEngine.Mathf.Clamp(isRewinding ? rewindFrameIndex : GetClampedEditResumeFrameIndex(), 0, iEditableFramesCount - 1);

                int iMainFramesToKeep;
                int iStagedFramesToApply = 0;
                bool bDidChange;

                if (stagedFrames.Count > 0)
                {
                    int iBaseCount = stagedBaseFrameIndex >= 0 ? UnityEngine.Mathf.Clamp(stagedBaseFrameIndex + 1, 0, recordedFrames.Count) : 0;

                    if (iApplyFrameIndex < iBaseCount)
                    {
                        iMainFramesToKeep = UnityEngine.Mathf.Clamp(iApplyFrameIndex + 1, 0, recordedFrames.Count);
                        iStagedFramesToApply = 0;
                    }

                    else
                    {
                        iMainFramesToKeep = iBaseCount;
                        iStagedFramesToApply = UnityEngine.Mathf.Clamp(iApplyFrameIndex - iBaseCount + 1, 0, stagedFrames.Count);
                    }
                }

                else
                    iMainFramesToKeep = UnityEngine.Mathf.Clamp(UnityEngine.Mathf.Min(iApplyFrameIndex + 1, recordedFrames.Count), 0, recordedFrames.Count);

                bDidChange = iMainFramesToKeep != recordedFrames.Count;

                int iAppliedFrames = iStagedFramesToApply;

                if (iStagedFramesToApply > 0)
                    bDidChange = true;

                if (recordedFrames.Count == 0 && stagedFrames.Count > 0)
                {
                    for (int i = 0; i < iStagedFramesToApply; i++)
                        recordedFrames.Add(stagedFrames[i]);

                    strRecordedLevelName = strPendingRecordLevelName;
                    strRecordedPathId = strPendingRecordPathId;
                    strRecordedPathDisplay = strPendingRecordPathDisplay;
                    strRecordedSectionDisplay = strPendingRecordSectionDisplay;
                }

                else if (recordedFrames.Count > 0 || iMainFramesToKeep > 0)
                {
                    if (recordedFrames.Count > iMainFramesToKeep)
                        recordedFrames.RemoveRange(iMainFramesToKeep, recordedFrames.Count - iMainFramesToKeep);

                    for (int i = 0; i < iStagedFramesToApply; i++)
                        recordedFrames.Add(stagedFrames[i]);
                }

                RebuildTransitions(recordedFrames);
                NormalizeSegmentIndices(recordedFrames);

                stagedFrames.Clear();
                stagedBaseFrameIndex = -1;

                if (recordedFrames.Count > 0)
                    recordStartPosition = recordedFrames[0].position;

                editResumeFrameIndex = recordedFrames.Count > 0 ? recordedFrames.Count - 1 : -1;

                ShowCenterNotification($"{GetAppliedFramesText(iAppliedFrames, recordedFrames.Count)}");
                NS_Core.Logger.Log($"Applied staged frames. Applied={iAppliedFrames}, total={recordedFrames.Count}, nextEditFrame={editResumeFrameIndex}, applyFrame={iApplyFrameIndex}");

                if (bDidChange && recordedFrames.Count > 0)
                {
                    rewindFrameIndex = UnityEngine.Mathf.Clamp(editResumeFrameIndex, 0, recordedFrames.Count - 1);

                    if (NS_Core.Binds.GetLocalPlayer() is Klei.HotLava.Character.PlayerController player && player.IsMine)
                    {
                        if (!StartRangePlaybackToFrame(player, rewindFrameIndex, false, "APPLY PREVIEW"))
                            ApplyFrameSnapshot(player, recordedFrames[rewindFrameIndex], rewindFrameIndex, "APPLY SNAP", true, true);
                    }
                }
            }

            public static void StartPlayback(Klei.HotLava.Character.PlayerController player)
            {
                if (recordedFrames.Count == 0)
                {
                    NS_Core.Logger.Log("Cannot start playback: no frames recorded");
                    return;
                }

                if (player == null || !player.IsMine)
                    return;

                if (isRecording || isPreparingRecord || isRewinding)
                {
                    NS_Core.Logger.Log("Cannot start playback while recording prepare/record or rewind is active");
                    return;
                }

                if (NS_Core.Vars.sTab.sMR.bTeleportPlaybackToStart)
                {
                    if (!IsCurrentLevelMatchingRecording())
                    {
                        NS_Core.Logger.Log($"Cannot teleport playback to start: recorded level = {strRecordedLevelName}, current level = {GetCurrentLevelName()}");
                        return;
                    }
                }

                else if (!IsCurrentPathMatchingRecording())
                {
                    NS_Core.Logger.Log($"Cannot start playback: recorded path = {GetRecordedRouteDisplay()}, current path = {GetCurrentRouteDisplay()}");
                    return;
                }

                float distanceToStart = UnityEngine.Vector3.Distance(player.RigidBody.position, recordStartPosition);
                float velocity = player.RigidBody.velocity.magnitude;

                NS_Core.Logger.Log($"StartPlayback: distance to start = {distanceToStart}, velocity = {velocity}");

                if (NS_Core.Vars.sTab.sMR.bTeleportPlaybackToStart)
                {
                    BeginPlaybackFromStart(player, true);
                    return;
                }

                if (distanceToStart >= 5.0f)
                {
                    NS_Core.Logger.Log($"Cannot start playback: too far from start position ({distanceToStart}m)");
                    return;
                }

                if (distanceToStart > 0.1f || velocity > 0.1f)
                {
                    isAutoApproaching = true;
                    isPlaying = false;
                    isRecording = false;
                    isPreparingRecord = false;
                    bRangePlaybackActive = false;
                    bRangePlaybackStartsPreparedRecord = false;
                    playbackTargetFrame = 0;
                    playbackFrame = 0;
                    iPendingPlaybackCancelCount = 0;
                    bIssuedPlaybackCancelForSectionRouteChange = false;

                    NS_Core.Logger.Log($"Auto-approach started. Distance: {distanceToStart}, velocity: {velocity}");

                    return;
                }

                BeginPlaybackFromStart(player, false);
            }

            public static void StopPlayback()
            {
                bool bWasPlaying = isPlaying;
                bool bWasAutoApproaching = isAutoApproaching;
                int iStoppedPlaybackFrame = playbackFrame;

                isPlaying = false;
                isAutoApproaching = false;
                bRangePlaybackActive = false;
                bRangePlaybackStartsPreparedRecord = false;
                playbackFrame = 0;
                playbackTargetFrame = 0;
                flLastPlaybackFrameTime = -1f;
                currentDesync = 0f;
                iPendingPlaybackCancelCount = 0;
                bIssuedPlaybackCancelForSectionRouteChange = false;

                if (bWasPlaying)
                {
                    if (bHasAppliedPlaybackCameraAngles)
                        lastCameraAngles = lastAppliedPlaybackCameraAngles;

                    else if (recordedFrames.Count > 0)
                        lastCameraAngles = recordedFrames[UnityEngine.Mathf.Clamp(iStoppedPlaybackFrame - 1, 0, recordedFrames.Count - 1)].cameraAngles;
                }

                else if (bWasAutoApproaching && bHasAppliedAutoApproachCameraAngles)
                    lastCameraAngles = lastAppliedAutoApproachCameraAngles;

                shouldKeepCameraAngles = bWasPlaying || (bWasAutoApproaching && bHasAppliedAutoApproachCameraAngles);
                bHasAppliedPlaybackCameraAngles = false;
                bHasAppliedAutoApproachCameraAngles = false;

                if (!bWasPlaying && !bWasAutoApproaching)
                    shouldKeepCameraAngles = false;

                if (bWasAutoApproaching && !bWasPlaying)
                    NS_Core.Logger.Log("Auto-approach cancelled");

                else
                    NS_Core.Logger.Log($"Playback stopped. Last camera angles: pitch={lastCameraAngles.x}, yaw={lastCameraAngles.y}");
            }

            public static void ToggleRewind(Klei.HotLava.Character.PlayerController player)
            {
                if (isRewinding)
                {
                    StopRewind(true);
                    return;
                }

                StartRewind(player);
            }

            public static void StartRewind(Klei.HotLava.Character.PlayerController player)
            {
                if (player == null || !player.IsMine || recordedFrames.Count == 0)
                    return;

                if (isPreparingRecord || isRecording || isPlaying || isAutoApproaching)
                {
                    NS_Core.Logger.Log("Cannot start rewind while recording or playback is active");
                    return;
                }

                if (!IsCurrentLevelMatchingRecording())
                {
                    NS_Core.Logger.Log($"Cannot start rewind: recorded level = {strRecordedLevelName}, current level = {GetCurrentLevelName()}");
                    return;
                }

                isRewinding = true;
                rewindDirection = 0;
                flRewindFrameAccumulator = 0f;
                rewindFrameIndex = GetRewindStartFrameIndex(player);

                player.StopReaching();
                ApplyFrameSnapshot(player, GetRewindFrame(rewindFrameIndex), rewindFrameIndex, "REWIND START", true, true);

                lastCameraAngles = GetRewindFrame(rewindFrameIndex).cameraAngles;
                shouldKeepCameraAngles = true;

                NS_Core.Logger.Log($"Rewind started at frame {rewindFrameIndex}/{recordedFrames.Count}, speed={GetConfiguredRewindSpeed():F2}");
            }

            public static void StopRewind(bool bKeepCamera)
            {
                if (!isRewinding)
                    return;

                if (recordedFrames.Count > 0)
                    editResumeFrameIndex = UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1);

                if (bKeepCamera && recordedFrames.Count > 0)
                {
                    lastCameraAngles = GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1)).cameraAngles;
                    shouldKeepCameraAngles = true;
                }

                else
                    shouldKeepCameraAngles = false;

                isRewinding = false;
                rewindDirection = 0;
                flRewindFrameAccumulator = 0f;

                NS_Core.Logger.Log($"Rewind stopped at frame {editResumeFrameIndex}");
            }

            public static void SetRewindDirection(int iDirection)
            {
                rewindDirection = UnityEngine.Mathf.Clamp(iDirection, -1, 1);
            }

            public static void IncreaseRewindSpeed()
            {
                NS_Core.Vars.sTab.sMR.iRewindSpeedIndex = UnityEngine.Mathf.Clamp(NS_Core.Vars.sTab.sMR.iRewindSpeedIndex + 1, 0, arrRewindSpeeds.Length - 1);
                NS_Core.Logger.Log($"Rewind speed increased to {GetConfiguredRewindSpeed():F2}");
            }

            public static void DecreaseRewindSpeed()
            {
                NS_Core.Vars.sTab.sMR.iRewindSpeedIndex = UnityEngine.Mathf.Clamp(NS_Core.Vars.sTab.sMR.iRewindSpeedIndex - 1, 0, arrRewindSpeeds.Length - 1);
                NS_Core.Logger.Log($"Rewind speed decreased to {GetConfiguredRewindSpeed():F2}");
            }

            public static void UpdateRewind(Klei.HotLava.Character.PlayerController player)
            {
                if (!isRewinding || player == null || !player.IsMine || recordedFrames.Count == 0)
                    return;

                Frame currentFrame = GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1));

                if (rewindDirection == 0 || GetConfiguredRewindSpeed() <= 0f)
                {
                    HoldRewindFrame(player, currentFrame, rewindFrameIndex);
                    return;
                }

                flRewindFrameAccumulator += GetConfiguredRewindSpeed();

                int iSteps = UnityEngine.Mathf.FloorToInt(flRewindFrameAccumulator);

                if (iSteps <= 0)
                {
                    HoldRewindFrame(player, currentFrame, rewindFrameIndex);
                    return;
                }

                flRewindFrameAccumulator -= iSteps;

                for (int i = 0; i < iSteps; i++)
                {
                    int iNextFrameIndex = UnityEngine.Mathf.Clamp(rewindFrameIndex + rewindDirection, 0, recordedFrames.Count - 1);

                    if (iNextFrameIndex == rewindFrameIndex)
                    {
                        rewindDirection = 0;
                        flRewindFrameAccumulator = 0f;

                        break;
                    }

                    rewindFrameIndex = iNextFrameIndex;
                    currentFrame = GetRewindFrame(rewindFrameIndex);

                    ApplyFrameSnapshot(player, currentFrame, rewindFrameIndex, "REWIND", true, true);
                }

                HoldRewindFrame(player, currentFrame, rewindFrameIndex);
            }

            public static void HoldAfterMovement(Klei.HotLava.Character.PlayerController player)
            {
                if (player == null || !player.IsMine)
                    return;

                if (isPreparingRecord && bHasPreparedRecordFrame)
                {
                    HoldPreparedRecordFrame(player);
                    return;
                }

                if (isRewinding && recordedFrames.Count > 0)
                    HoldRewindFrame(player, GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1)), rewindFrameIndex);
            }

            public static void PlaybackFrame(Klei.HotLava.Character.PlayerController player)
            {
                if (!isPlaying || player == null || !player.IsMine || recordedFrames.Count == 0)
                    return;

                int iPlaybackEndFrameExclusive = bRangePlaybackActive ? UnityEngine.Mathf.Clamp(playbackTargetFrame + 1, 0, recordedFrames.Count) : recordedFrames.Count;

                if (playbackFrame >= iPlaybackEndFrameExclusive)
                {
                    if (bRangePlaybackActive)
                        CompleteRangePlayback(player);

                    else
                        StopPlayback();

                    return;
                }

                currentPlaybackFrame = recordedFrames[playbackFrame];
                flLastPlaybackFrameTime = UnityEngine.Time.time;

                float preCorrectionDesync = UnityEngine.Vector3.Distance(player.RigidBody.position, currentPlaybackFrame.position);
                bool bTransitionFrame = currentPlaybackFrame.sectionTransition || currentPlaybackFrame.teleportTransition || NeedsGameModeSync(currentPlaybackFrame);

                if (bTransitionFrame)
                {
                    SyncPlaybackTransition(player, currentPlaybackFrame, playbackFrame);
                    preCorrectionDesync = 0f;
                }

                else
                    ApplyPlaybackCorrection(player, currentPlaybackFrame, preCorrectionDesync, playbackFrame);

                TryQueuePlaybackCancelForSectionRouteChange();

                UnityEngine.Vector3 currentPos = player.RigidBody.position;
                UnityEngine.Vector3 recordedPos = currentPlaybackFrame.position;

                currentDesync = UnityEngine.Vector3.Distance(currentPos, recordedPos);

                UnityEngine.Vector2 currentCameraAngles = new UnityEngine.Vector2(player.CameraRotation.eulerAngles.x, player.CameraRotation.eulerAngles.y);

                float pitchDiff = UnityEngine.Mathf.Abs(UnityEngine.Mathf.DeltaAngle(currentCameraAngles.x, currentPlaybackFrame.cameraAngles.x));
                float yawDiff = UnityEngine.Mathf.Abs(UnityEngine.Mathf.DeltaAngle(currentCameraAngles.y, currentPlaybackFrame.cameraAngles.y));

                if (playbackFrame < 10 || playbackFrame % 50 == 0 || currentDesync > 0.12f || currentPlaybackFrame.sectionTransition || currentPlaybackFrame.teleportTransition)
                {
                    UnityEngine.Vector3 currentVel = player.RigidBody.velocity;
                    UnityEngine.Vector2 currentMouseInput = player.GetCachedMouseInput();

                    NS_Core.Logger.Log($"[DESYNC CHECK] Frame {playbackFrame}:");
                    NS_Core.Logger.Log($"  Position - Current: {currentPos}, Recorded: {recordedPos}, PreDiff: {preCorrectionDesync:F3}, Diff: {currentDesync:F3}");
                    NS_Core.Logger.Log($"  Velocity - Current: {currentVel}, Recorded: {currentPlaybackFrame.velocity}");
                    NS_Core.Logger.Log($"  Input - moveInput: ({currentPlaybackFrame.moveInput.x}, {currentPlaybackFrame.moveInput.y}), jump: {currentPlaybackFrame.jump}, landingJump: {currentPlaybackFrame.landingJump}, crouch: {currentPlaybackFrame.crouch}, gameMode: {currentPlaybackFrame.gameModeIndex}, section: {currentPlaybackFrame.subGameModeIndex}, transition: {currentPlaybackFrame.sectionTransition}, teleport: {currentPlaybackFrame.teleportTransition}, grounded: {player.Grounded}, surfing: {player.Surfing}");
                    NS_Core.Logger.Log($"  Camera - current: ({currentCameraAngles.x}, {currentCameraAngles.y}), recorded: ({currentPlaybackFrame.cameraAngles.x}, {currentPlaybackFrame.cameraAngles.y}), diff: ({pitchDiff:F3}, {yawDiff:F3}), mouseCurrent: ({currentMouseInput.x}, {currentMouseInput.y}), mouseRecorded: ({currentPlaybackFrame.mouseInput.x}, {currentPlaybackFrame.mouseInput.y})");
                }

                if (UnityEngine.Mathf.Abs(currentPlaybackFrame.scrollWheel) > 0f)
                    NS_Core.Logger.Log($"[PLAYBACK FRAME] frame {playbackFrame} scroll={currentPlaybackFrame.scrollWheel} jump={currentPlaybackFrame.jump} landingJump={currentPlaybackFrame.landingJump} section={currentPlaybackFrame.subGameModeIndex}");

                bool wasJumping = playbackFrame > 0 ? recordedFrames[playbackFrame - 1].jump : false;
                bool isJumping = currentPlaybackFrame.jump;

                if (isJumping != wasJumping)
                    NS_Core.Logger.Log($"Frame {playbackFrame}: Jump state changed from {wasJumping} to {isJumping}, IsJumpDown will be {isJumping && !wasJumping}");

                lastJumpState = wasJumping;
                playbackFrame++;

                if (bRangePlaybackActive && playbackFrame >= iPlaybackEndFrameExclusive)
                    CompleteRangePlayback(player);
            }

            public static void ApplyCameraAngles(UnityEngine.Transform character, UnityEngine.Transform camera)
            {
                if (isPlaying && recordedFrames.Count > 0)
                {
                    if (playbackFrame <= 0)
                    {
                        Frame startFrame = recordedFrames[0];

                        character.localRotation = UnityEngine.Quaternion.Euler(0f, startFrame.cameraAngles.y, 0f);
                        camera.localRotation = UnityEngine.Quaternion.Euler(startFrame.cameraAngles.x, 0f, 0f);

                        lastAppliedPlaybackCameraAngles = startFrame.cameraAngles;
                        bHasAppliedPlaybackCameraAngles = true;

                        return;
                    }

                    if (playbackFrame - 1 >= 0 && playbackFrame - 1 < recordedFrames.Count)
                    {
                        int currentFrameIndex = playbackFrame - 1;

                        Frame frame = recordedFrames[currentFrameIndex];

                        float pitch = frame.cameraAngles.x;
                        float yaw = frame.cameraAngles.y;

                        if (playbackFrame >= 0 && playbackFrame < recordedFrames.Count && flLastPlaybackFrameTime >= 0f)
                        {
                            Frame nextFrame = recordedFrames[playbackFrame];

                            float alpha = UnityEngine.Mathf.Clamp01((UnityEngine.Time.time - flLastPlaybackFrameTime) / UnityEngine.Mathf.Max(UnityEngine.Time.fixedDeltaTime, 0.0001f));

                            pitch = UnityEngine.Mathf.LerpAngle(frame.cameraAngles.x, nextFrame.cameraAngles.x, alpha);
                            yaw = UnityEngine.Mathf.LerpAngle(frame.cameraAngles.y, nextFrame.cameraAngles.y, alpha);
                        }

                        character.localRotation = UnityEngine.Quaternion.Euler(0f, yaw, 0f);
                        camera.localRotation = UnityEngine.Quaternion.Euler(pitch, 0f, 0f);

                        lastAppliedPlaybackCameraAngles = new UnityEngine.Vector2(pitch, yaw);
                        bHasAppliedPlaybackCameraAngles = true;

                        if (playbackFrame == 1 || playbackFrame % 50 == 0)
                            NS_Core.Logger.Log($"Applied camera angles frame {playbackFrame}: pitch={pitch}, yaw={yaw}");
                    }

                    return;
                }

                if (isRewinding && recordedFrames.Count > 0)
                {
                    Frame frame = GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1));

                    character.localRotation = UnityEngine.Quaternion.Euler(0f, frame.cameraAngles.y, 0f);
                    camera.localRotation = UnityEngine.Quaternion.Euler(frame.cameraAngles.x, 0f, 0f);

                    lastAppliedPlaybackCameraAngles = frame.cameraAngles;
                    bHasAppliedPlaybackCameraAngles = true;

                    return;
                }

                if (shouldKeepCameraAngles)
                {
                    character.localRotation = UnityEngine.Quaternion.Euler(0f, lastCameraAngles.y, 0f);
                    camera.localRotation = UnityEngine.Quaternion.Euler(lastCameraAngles.x, 0f, 0f);
                }
            }

            public static void DisableKeepCameraAngles()
            {
                shouldKeepCameraAngles = false;
                NS_Core.Logger.Log("Camera angle lock disabled by mouse movement");
            }

            public static void Clear()
            {
                recordedFrames.Clear();
                stagedFrames.Clear();
                isPreparingRecord = false;
                isRecording = false;
                isPlaying = false;
                isRewinding = false;
                bRangePlaybackActive = false;
                bRangePlaybackStartsPreparedRecord = false;
                playbackFrame = 0;
                playbackTargetFrame = 0;
                rewindFrameIndex = 0;
                editResumeFrameIndex = -1;
                stagedBaseFrameIndex = -1;
                stagedSegmentIndex = 0;
                isAutoApproaching = false;
                recordDelayRemainingFrames = 0;
                flPendingRecordedScrollWheel = 0f;
                bPendingRecordedSyntheticJump = false;
                bHasLiveRecordedCameraAngles = false;
                bHasAppliedPlaybackCameraAngles = false;
                bHasAppliedAutoApproachCameraAngles = false;
                flLastPlaybackFrameTime = -1f;
                shouldKeepCameraAngles = false;
                strRecordedLevelName = string.Empty;
                strRecordedPathId = string.Empty;
                strRecordedPathDisplay = string.Empty;
                strRecordedSectionDisplay = string.Empty;
                strRecordFileName = string.Empty;
                strPendingRecordLevelName = string.Empty;
                strPendingRecordPathId = string.Empty;
                strPendingRecordPathDisplay = string.Empty;
                strPendingRecordSectionDisplay = string.Empty;
                strCenterNotification = string.Empty;
                flCenterNotificationUntil = -1f;
                iPendingPlaybackCancelCount = 0;
                bIssuedPlaybackCancelForSectionRouteChange = false;
                bHasPreparedRecordFrame = false;
                bPreparedRecordUsesRecordedInput = false;
                bRecordedFramesHaveBunnyHopState = false;
                rewindDirection = 0;
                flRewindFrameAccumulator = 0f;

                RestoreRecordSlowmotion();
            }

            public static bool IsCurrentPathMatchingRecording()
            {
                if (string.IsNullOrEmpty(strRecordedPathId))
                    return true;

                return string.Equals(strRecordedPathId, GetCurrentPathId(), System.StringComparison.Ordinal);
            }

            public static bool IsCurrentLevelMatchingRecording()
            {
                if (string.IsNullOrWhiteSpace(strRecordedLevelName))
                    return true;

                return string.Equals(strRecordedLevelName, GetCurrentLevelName(), System.StringComparison.Ordinal);
            }

            public static void AutoApproachUpdate(Klei.HotLava.Character.PlayerController player)
            {
                if (!isAutoApproaching || player == null || !player.IsMine)
                    return;

                float distanceToStart = UnityEngine.Vector3.Distance(player.RigidBody.position, recordStartPosition);
                float flatDistanceToStart = GetFlatDistanceToStart(player);
                float velocity = player.RigidBody.velocity.magnitude;

                if (flatDistanceToStart < 0.12f || (flatDistanceToStart < 0.28f && velocity < 0.12f))
                {
                    player.RigidBody.velocity = UnityEngine.Vector3.zero;
                    player.RigidBody.position = recordStartPosition;
                    isAutoApproaching = false;
                    isPlaying = true;
                    playbackFrame = 0;
                    shouldKeepCameraAngles = true;
                    lastAppliedPlaybackCameraAngles = recordedFrames[0].cameraAngles;
                    bHasAppliedPlaybackCameraAngles = false;
                    flLastPlaybackFrameTime = UnityEngine.Time.time;
                    iPendingPlaybackCancelCount = 0;
                    bIssuedPlaybackCancelForSectionRouteChange = false;

                    NS_Core.Logger.Log($"Auto-approach complete. Distance3D: {distanceToStart}, flatDistance: {flatDistanceToStart}, velocity: {velocity}. Starting playback. Total frames: {recordedFrames.Count}");
                    return;
                }

                if (flatDistanceToStart < 0.45f && velocity > 0.45f)
                    player.RigidBody.velocity = UnityEngine.Vector3.Lerp(player.RigidBody.velocity, UnityEngine.Vector3.zero, 0.18f);

                if (playbackFrame % 50 == 0)
                    NS_Core.Logger.Log($"Auto-approaching: distance = {distanceToStart}, flatDistance = {flatDistanceToStart}, velocity = {velocity}");

                playbackFrame++;
            }

            public static UnityEngine.Vector2 GetAutoApproachInput(Klei.HotLava.Character.PlayerController player)
            {
                if (!isAutoApproaching || player == null)
                    return UnityEngine.Vector2.zero;

                float distanceToStart = GetFlatDistanceToStart(player);
                float velocity = player.RigidBody.velocity.magnitude;

                if (distanceToStart < 0.12f)
                    return UnityEngine.Vector2.zero;

                float inputStrength;

                if (distanceToStart > 1.5f)
                    inputStrength = 1f;

                else if (distanceToStart > 0.9f)
                    inputStrength = UnityEngine.Mathf.Lerp(0.75f, 1f, UnityEngine.Mathf.InverseLerp(0.9f, 1.5f, distanceToStart));

                else if (distanceToStart > 0.45f)
                    inputStrength = UnityEngine.Mathf.Lerp(0.55f, 0.75f, UnityEngine.Mathf.InverseLerp(0.45f, 0.9f, distanceToStart));

                else
                    inputStrength = UnityEngine.Mathf.Lerp(0.4f, 0.55f, UnityEngine.Mathf.InverseLerp(0.18f, 0.45f, distanceToStart));

                float maxComfortVelocity;

                if (distanceToStart > 1.5f)
                    maxComfortVelocity = 2.4f;

                else if (distanceToStart > 0.9f)
                    maxComfortVelocity = 1.7f;

                else if (distanceToStart > 0.45f)
                    maxComfortVelocity = 0.95f;

                else
                    maxComfortVelocity = 0.45f;

                if (velocity > maxComfortVelocity)
                    inputStrength *= 0.15f;

                else if (velocity > maxComfortVelocity * 0.75f)
                    inputStrength *= 0.45f;

                return new UnityEngine.Vector2(0f, inputStrength);
            }

            public static UnityEngine.Vector2 GetAutoApproachInput()
            {
                return GetAutoApproachInput(NS_Core.Binds.GetLocalPlayer());
            }

            public static void ApplyAutoApproachCamera(UnityEngine.Transform character, UnityEngine.Transform camera)
            {
                if (!isAutoApproaching)
                    return;

                UnityEngine.Vector3 directionToStart = recordStartPosition - character.position;
                directionToStart.y = 0f;

                if (directionToStart.sqrMagnitude > 0.001f)
                {
                    float targetYaw = UnityEngine.Mathf.Atan2(directionToStart.x, directionToStart.z) * UnityEngine.Mathf.Rad2Deg;
                    character.localRotation = UnityEngine.Quaternion.Euler(0f, targetYaw, 0f);

                    lastAppliedAutoApproachCameraAngles = new UnityEngine.Vector2(0f, targetYaw);
                    bHasAppliedAutoApproachCameraAngles = true;
                }

                camera.localRotation = UnityEngine.Quaternion.Euler(0f, 0f, 0f);

                lastAppliedAutoApproachCameraAngles.x = 0f;
                bHasAppliedAutoApproachCameraAngles = true;
            }

            public static void CaptureLiveCameraAngles(UnityEngine.Transform character, UnityEngine.Transform camera)
            {
                if (!isRecording || character == null || camera == null)
                    return;

                liveRecordedCameraAngles = new UnityEngine.Vector2(camera.localRotation.eulerAngles.x, character.localRotation.eulerAngles.y);
                bHasLiveRecordedCameraAngles = true;
            }

            private static void SyncPlaybackTransition(Klei.HotLava.Character.PlayerController player, Frame frame, int frameIndex)
            {
                SyncToFrameGameMode(frame, frameIndex, "PLAYBACK");

                player.RigidBody.position = frame.position;
                player.transform.rotation = frame.rotation;
                player.RigidBody.velocity = frame.velocity;
                player.RigidBody.angularVelocity = UnityEngine.Vector3.zero;
                lastAppliedPlaybackCameraAngles = frame.cameraAngles;
                bHasAppliedPlaybackCameraAngles = true;

                NS_Core.Logger.Log($"[PLAYBACK TRANSITION] Frame {frameIndex}: path={GetCurrentPathId()}, gameMode={frame.gameModeIndex}, section={frame.subGameModeIndex}, teleport={frame.teleportTransition}, pos={frame.position}, vel={frame.velocity}");
            }

            private static void CompleteRangePlayback(Klei.HotLava.Character.PlayerController player)
            {
                int iCompletedFrame = UnityEngine.Mathf.Clamp(playbackTargetFrame, 0, recordedFrames.Count - 1);
                Frame frame = recordedFrames[iCompletedFrame];

                editResumeFrameIndex = iCompletedFrame;
                currentPlaybackFrame = frame;
                lastCameraAngles = frame.cameraAngles;
                shouldKeepCameraAngles = true;
                isPlaying = false;
                bRangePlaybackActive = false;
                playbackFrame = 0;
                playbackTargetFrame = 0;
                flLastPlaybackFrameTime = -1f;

                NS_Core.Logger.Log($"Range playback completed at frame {iCompletedFrame}");

                if (bRangePlaybackStartsPreparedRecord)
                {
                    bRangePlaybackStartsPreparedRecord = false;
                    BeginPreparedRecordingFromFrame(player, frame, iCompletedFrame, false);

                    return;
                }

                bRangePlaybackStartsPreparedRecord = false;
            }

            private static void ApplyPlaybackCorrection(Klei.HotLava.Character.PlayerController player, Frame frame, float preCorrectionDesync, int frameIndex)
            {
                float velocityDiff = UnityEngine.Vector3.Distance(player.RigidBody.velocity, frame.velocity);

                if (velocityDiff > 0.05f)
                {
                    float velocityBlend = player.Surfing ? 0.6f : (!player.Grounded ? 0.45f : 0.2f);
                    player.RigidBody.velocity = UnityEngine.Vector3.Lerp(player.RigidBody.velocity, frame.velocity, velocityBlend);
                }

                if (preCorrectionDesync > 0.6f)
                    player.RigidBody.position = UnityEngine.Vector3.Lerp(player.RigidBody.position, frame.position, 0.5f);

                if (preCorrectionDesync > 0.12f || velocityDiff > 0.4f)
                    NS_Core.Logger.Log($"[PLAYBACK CORRECTION] Frame {frameIndex}: preDesync={preCorrectionDesync:F3}, velocityDiff={velocityDiff:F3}, grounded={player.Grounded}, surfing={player.Surfing}, section={frame.subGameModeIndex}");
            }

            private static void HoldPreparedRecordFrame(Klei.HotLava.Character.PlayerController player)
            {
                ClearReplayVictoryState(player);

                player.RigidBody.position = preparedRecordFrame.position;
                player.RigidBody.velocity = preparedRecordFrame.velocity;
                player.transform.rotation = preparedRecordFrame.rotation;

                currentPlaybackFrame = preparedRecordFrame;

                player.RigidBody.angularVelocity = UnityEngine.Vector3.zero;
            }

            private static void HoldRewindFrame(Klei.HotLava.Character.PlayerController player, Frame frame, int frameIndex)
            {
                player.StopReaching();

                ClearReplayVictoryState(player);
                SyncToFrameGameMode(frame, frameIndex, "REWIND HOLD", false);

                player.RigidBody.position = frame.position;
                player.transform.rotation = frame.rotation;
                player.RigidBody.velocity = rewindDirection == 0 ? UnityEngine.Vector3.zero : frame.velocity;
                currentPlaybackFrame = frame;
                player.RigidBody.angularVelocity = UnityEngine.Vector3.zero;
                lastAppliedPlaybackCameraAngles = frame.cameraAngles;
                bHasAppliedPlaybackCameraAngles = true;
            }

            private static void ApplyFrameSnapshot(Klei.HotLava.Character.PlayerController player, Frame frame, int frameIndex, string strReason, bool bQueueCancel, bool bUseFrameVelocity)
            {
                ClearReplayVictoryState(player);
                SyncToFrameGameMode(frame, frameIndex, strReason, bQueueCancel);

                player.RigidBody.position = frame.position;
                player.transform.rotation = frame.rotation;
                player.RigidBody.velocity = bUseFrameVelocity ? frame.velocity : UnityEngine.Vector3.zero;
                player.RigidBody.angularVelocity = UnityEngine.Vector3.zero;
                currentPlaybackFrame = frame;
                lastAppliedPlaybackCameraAngles = frame.cameraAngles;
                bHasAppliedPlaybackCameraAngles = true;

                if ((strReason != "REWIND" && strReason != "RANGE PLAYBACK") || frameIndex < 5 || frameIndex % 50 == 0 || frame.sectionTransition || frame.teleportTransition)
                    NS_Core.Logger.Log($"[{strReason}] Frame {frameIndex}: pos={frame.position}, vel={(bUseFrameVelocity ? frame.velocity : UnityEngine.Vector3.zero)}, gameMode={frame.gameModeIndex}, section={frame.subGameModeIndex}");
            }

            private static void SyncToFrameGameMode(Frame frame, int frameIndex, string strReason, bool bQueueCancel = true)
            {
                if (!NeedsGameModeSync(frame))
                    return;

                miInfoSetCurrentGameMode?.Invoke(null, new object[] { frame.gameModeIndex, frame.subGameModeIndex });

                if (bQueueCancel)
                    QueuePendingCancel($"{strReason} frame={frameIndex} gameMode={frame.gameModeIndex} section={frame.subGameModeIndex}");
            }

            private static bool NeedsGameModeSync(Frame frame)
            {
                return GetCurrentGameModeIndex() != frame.gameModeIndex || GetCurrentSubGameModeIndex() != frame.subGameModeIndex;
            }

            private static float GetFlatDistanceToStart(Klei.HotLava.Character.PlayerController player)
            {
                UnityEngine.Vector3 delta = recordStartPosition - player.RigidBody.position;
                delta.y = 0f;

                return delta.magnitude;
            }

            private static int FindReplayAnchorFrameIndex(int iTargetFrameIndex)
            {
                iTargetFrameIndex = UnityEngine.Mathf.Clamp(iTargetFrameIndex, 0, recordedFrames.Count - 1);

                if (iTargetFrameIndex <= 0)
                    return 0;

                int iTargetSegmentIndex = recordedFrames[iTargetFrameIndex].segmentIndex;

                for (int i = iTargetFrameIndex; i > 0; i--)
                {
                    if (recordedFrames[i - 1].segmentIndex != iTargetSegmentIndex)
                        return i;
                }

                for (int i = iTargetFrameIndex - 1; i >= 0; i--)
                {
                    if (recordedFrames[i].sectionTransition || recordedFrames[i].teleportTransition)
                        return i;
                }

                return 0;
            }

            private static bool HasPlaybackLandingJumpBuffered()
            {
                if (!isPlaying || recordedFrames.Count == 0)
                    return false;

                if (currentPlaybackFrame.landingJump)
                    return true;

                if (playbackFrame >= 0 && playbackFrame < recordedFrames.Count && recordedFrames[playbackFrame].landingJump)
                    return true;

                int nextFrame = playbackFrame + 1;

                if (nextFrame >= 0 && nextFrame < recordedFrames.Count && recordedFrames[nextFrame].landingJump)
                    return true;

                return false;
            }

            public static bool ConsumePendingPlaybackCancel()
            {
                if (iPendingPlaybackCancelCount <= 0)
                    return false;

                iPendingPlaybackCancelCount--;
                return true;
            }

            private static void QueuePendingCancel(string strReason)
            {
                iPendingPlaybackCancelCount = UnityEngine.Mathf.Max(iPendingPlaybackCancelCount, 24);
                NS_Core.Logger.Log($"[MR CANCEL] {strReason}, remaining={iPendingPlaybackCancelCount}");
            }

            private static void TryQueuePlaybackCancelForSectionRouteChange()
            {
                if (!isPlaying || bIssuedPlaybackCancelForSectionRouteChange || !string.IsNullOrWhiteSpace(strRecordedSectionDisplay))
                    return;

                string currentSectionDisplay = GetCurrentSectionDisplayZeroBased();

                if (string.IsNullOrWhiteSpace(currentSectionDisplay))
                    return;

                string currentPathDisplay = GetCurrentPathDisplay();

                if (string.Equals(currentPathDisplay, strRecordedPathDisplay, System.StringComparison.Ordinal))
                    return;

                QueuePendingCancel($"PLAYBACK ROUTE CHANGE frame={playbackFrame} recordedPath={strRecordedPathDisplay} currentPath={currentPathDisplay} currentSection={currentSectionDisplay}");

                bIssuedPlaybackCancelForSectionRouteChange = true;

                NS_Core.Logger.Log($"[PLAYBACK CANCEL] Injecting route-change cancel. recordedPath={strRecordedPathDisplay}, currentPath={currentPathDisplay}, currentSection={currentSectionDisplay}, frame={playbackFrame}");
            }

            private static void BeginPlaybackFromStart(Klei.HotLava.Character.PlayerController player, bool bTeleportedToStart)
            {
                ApplyFrameSnapshot(player, recordedFrames[0], 0, "PLAYBACK START", true, true);

                currentDesync = 0f;
                isPlaying = true;
                isRecording = false;
                isPreparingRecord = false;
                bRangePlaybackActive = false;
                bRangePlaybackStartsPreparedRecord = false;
                playbackTargetFrame = 0;
                isAutoApproaching = false;
                playbackFrame = 0;
                shouldKeepCameraAngles = true;
                recordStartPosition = recordedFrames[0].position;
                recordStartCameraAngles = recordedFrames[0].cameraAngles;
                lastCameraAngles = recordedFrames[0].cameraAngles;
                lastAppliedPlaybackCameraAngles = recordedFrames[0].cameraAngles;
                bHasAppliedPlaybackCameraAngles = false;
                flLastPlaybackFrameTime = UnityEngine.Time.time;
                iPendingPlaybackCancelCount = 0;
                bIssuedPlaybackCancelForSectionRouteChange = false;

                NS_Core.Logger.Log($"{(bTeleportedToStart ? "Playback teleported to start" : "Playback started immediately")}. Total frames: {recordedFrames.Count}");
            }

            private static void ApplyRecordSlowmotion()
            {
                if (bAppliedRecordSlowmotion)
                    return;

                flPreviousTimeScale = UnityEngine.Time.timeScale;

                float scale = RecordSlowmotionScale;

                UnityEngine.Time.timeScale = scale;
                bAppliedRecordSlowmotion = true;
            }

            private static void RestoreRecordSlowmotion()
            {
                if (!bAppliedRecordSlowmotion)
                    return;

                UnityEngine.Time.timeScale = flPreviousTimeScale <= 0f ? 1f : flPreviousTimeScale;
                bAppliedRecordSlowmotion = false;
            }

            private static float GetConfiguredRewindSpeed()
            {
                int iIndex = UnityEngine.Mathf.Clamp(NS_Core.Vars.sTab.sMR.iRewindSpeedIndex, 0, arrRewindSpeeds.Length - 1);
                return arrRewindSpeeds[iIndex];
            }

            private static int GetEditableFramesCount()
            {
                if (stagedFrames.Count <= 0)
                    return recordedFrames.Count;

                int iBaseCount = stagedBaseFrameIndex >= 0 ? UnityEngine.Mathf.Clamp(stagedBaseFrameIndex + 1, 0, recordedFrames.Count) : 0;
                return iBaseCount + stagedFrames.Count;
            }

            private static int GetEditableLastFrameIndex()
            {
                int iEditableFramesCount = GetEditableFramesCount();
                return iEditableFramesCount > 0 ? iEditableFramesCount - 1 : -1;
            }

            private static Frame GetEditableFrame(int iFrameIndex)
            {
                if (stagedFrames.Count <= 0)
                    return recordedFrames[iFrameIndex];

                int iBaseCount = stagedBaseFrameIndex >= 0 ? UnityEngine.Mathf.Clamp(stagedBaseFrameIndex + 1, 0, recordedFrames.Count) : 0;

                if (iFrameIndex < iBaseCount)
                    return recordedFrames[iFrameIndex];

                return stagedFrames[iFrameIndex - iBaseCount];
            }

            private static int GetStagedRestartFrameIndex()
            {
                if (stagedFrames.Count <= 0)
                    return GetClampedEditResumeFrameIndex();

                if (stagedBaseFrameIndex >= 0)
                    return UnityEngine.Mathf.Clamp(stagedBaseFrameIndex, 0, UnityEngine.Mathf.Max(0, recordedFrames.Count - 1));

                return 0;
            }

            private static void TrimStagedFramesForEditIndex(int iEditFrameIndex)
            {
                if (stagedFrames.Count <= 0)
                    return;

                int iBaseCount = stagedBaseFrameIndex >= 0 ? UnityEngine.Mathf.Clamp(stagedBaseFrameIndex + 1, 0, recordedFrames.Count) : 0;

                if (iEditFrameIndex < iBaseCount)
                {
                    stagedFrames.Clear();
                    stagedBaseFrameIndex = iEditFrameIndex;

                    return;
                }

                int iKeepStagedCount = UnityEngine.Mathf.Clamp(iEditFrameIndex - iBaseCount + 1, 0, stagedFrames.Count);

                if (stagedFrames.Count > iKeepStagedCount)
                    stagedFrames.RemoveRange(iKeepStagedCount, stagedFrames.Count - iKeepStagedCount);
            }

            private static int GetClampedEditResumeFrameIndex()
            {
                int iEditableFramesCount = GetEditableFramesCount();

                if (iEditableFramesCount <= 0)
                    return 0;

                if (editResumeFrameIndex < 0)
                    editResumeFrameIndex = iEditableFramesCount - 1;

                return UnityEngine.Mathf.Clamp(editResumeFrameIndex, 0, iEditableFramesCount - 1);
            }

            private static void NormalizeSegmentIndices(System.Collections.Generic.List<Frame> frames)
            {
                if (frames.Count <= 0)
                    return;

                int iNormalizedSegmentIndex = 0;
                int iPreviousRawSegmentIndex = frames[0].segmentIndex;

                for (int i = 0; i < frames.Count; i++)
                {
                    Frame frame = frames[i];

                    if (i > 0 && frame.segmentIndex != iPreviousRawSegmentIndex)
                    {
                        iNormalizedSegmentIndex++;
                        iPreviousRawSegmentIndex = frame.segmentIndex;
                    }

                    frame.segmentIndex = iNormalizedSegmentIndex;
                    frames[i] = frame;
                }
            }

            private static int CountSegmentsInRecordedFrames()
            {
                if (recordedFrames.Count <= 0)
                    return 0;

                int iSegments = 1;
                int iPreviousSegmentIndex = recordedFrames[0].segmentIndex;

                for (int i = 1; i < recordedFrames.Count; i++)
                {
                    if (recordedFrames[i].segmentIndex == iPreviousSegmentIndex)
                        continue;

                    iPreviousSegmentIndex = recordedFrames[i].segmentIndex;
                    iSegments++;
                }

                return iSegments;
            }

            private static int CountSegmentsInEditableFrames()
            {
                int iEditableFramesCount = GetEditableFramesCount();

                if (iEditableFramesCount <= 0)
                    return 0;

                int iSegments = 1;
                int iPreviousSegmentIndex = GetEditableFrame(0).segmentIndex;

                for (int i = 1; i < iEditableFramesCount; i++)
                {
                    int iCurrentSegmentIndex = GetEditableFrame(i).segmentIndex;

                    if (iCurrentSegmentIndex == iPreviousSegmentIndex)
                        continue;

                    iPreviousSegmentIndex = iCurrentSegmentIndex;
                    iSegments++;
                }

                return iSegments;
            }

            private static int GetCurrentTotalSegmentCount()
            {
                if (isRewinding)
                    return CountSegmentsInRecordedFrames();

                if (isRecording || isPreparingRecord || stagedFrames.Count > 0)
                    return CountSegmentsInEditableFrames();

                return CountSegmentsInRecordedFrames();
            }

            private static int GetCurrentSegmentNumber()
            {
                int iCurrentSegmentNumber = 0;

                if (isRecording)
                    iCurrentSegmentNumber = stagedSegmentIndex + 1;

                else if (isPreparingRecord)
                {
                    if (GetEditableFramesCount() > 0)
                        iCurrentSegmentNumber = GetEditableFrame(GetClampedEditResumeFrameIndex()).segmentIndex + 1;

                    else
                        iCurrentSegmentNumber = stagedSegmentIndex + 1;
                }

                else if (isPlaying && recordedFrames.Count > 0)
                    iCurrentSegmentNumber = currentPlaybackFrame.segmentIndex + 1;

                else if (isRewinding && recordedFrames.Count > 0)
                    iCurrentSegmentNumber = GetRewindFrame(UnityEngine.Mathf.Clamp(rewindFrameIndex, 0, recordedFrames.Count - 1)).segmentIndex + 1;

                else if (stagedFrames.Count > 0)
                    iCurrentSegmentNumber = stagedFrames[stagedFrames.Count - 1].segmentIndex + 1;

                else if (recordedFrames.Count > 0)
                    iCurrentSegmentNumber = recordedFrames[recordedFrames.Count - 1].segmentIndex + 1;

                int iTotalSegments = GetCurrentTotalSegmentCount();

                if (iTotalSegments > 0)
                    return UnityEngine.Mathf.Clamp(iCurrentSegmentNumber, 0, iTotalSegments);

                return iCurrentSegmentNumber;
            }

            private static void RebuildTransitions(System.Collections.Generic.List<Frame> frames)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    Frame frame = frames[i];

                    if (i <= 0)
                    {
                        frame.sectionTransition = false;
                        frame.teleportTransition = false;
                    }

                    else
                    {
                        Frame previous = frames[i - 1];

                        frame.sectionTransition = previous.gameModeIndex != frame.gameModeIndex || previous.subGameModeIndex != frame.subGameModeIndex;
                        frame.teleportTransition = UnityEngine.Vector3.Distance(previous.position, frame.position) > 5f;
                    }

                    frames[i] = frame;
                }
            }

            private static Frame BuildLiveFrame(Klei.HotLava.Character.PlayerController player)
            {
                return new Frame
                {
                    position = player.RigidBody.position,
                    velocity = player.RigidBody.velocity,
                    rotation = player.transform.rotation,
                    cameraAngles = new UnityEngine.Vector2(player.CameraRotation.eulerAngles.x, player.CameraRotation.eulerAngles.y),
                    mouseInput = player.GetCachedMouseInput(),
                    jump = TeamUtility.IO.InputManager.GetButton("Jump", TeamUtility.IO.PlayerID.One),
                    landingJump = false,
                    crouch = TeamUtility.IO.InputManager.GetButton("Crouch", TeamUtility.IO.PlayerID.One),
                    action = TeamUtility.IO.InputManager.GetButton("Action", TeamUtility.IO.PlayerID.One),
                    scrollWheel = 0f,
                    moveInput = player.GetCachedInput(),
                    bunnyHopPercent = player.BunnyHopPercent,
                    bunnyHopBonusPercent = player.BunnyHopBonusPercent,
                    bunnyHopModifier = player.BunnyHopModifier,
                    bunnyHopDirection = UnityEngine.Mathf.Abs(player.BunnyHopModifier) > 0.0001f ? UnityEngine.Mathf.Sign(player.BunnyHopModifier) : 0f,
                    justJumped = player.JustJumped,
                    holdDirection = player.HoldDirectionPressed,
                    actionPressedRampValue = player.ActionPressedRampValue,
                    gameModeIndex = GetCurrentGameModeIndex(),
                    subGameModeIndex = GetCurrentSubGameModeIndex(),
                    segmentIndex = 0,
                    sectionTransition = false,
                    teleportTransition = false
                };
            }

            private static Frame GetRewindFrame(int iFrameIndex)
            {
                return recordedFrames[iFrameIndex];
            }

            private static int GetClampedRecordedEditResumeFrameIndex()
            {
                if (recordedFrames.Count <= 0)
                    return 0;

                if (editResumeFrameIndex < 0)
                    return recordedFrames.Count - 1;

                return UnityEngine.Mathf.Clamp(editResumeFrameIndex, 0, recordedFrames.Count - 1);
            }

            private static int GetRewindStartFrameIndex(Klei.HotLava.Character.PlayerController player)
            {
                int iTargetFrameIndex = GetClampedRecordedEditResumeFrameIndex();

                if (ShouldBackstepFinalRewindStart(player, iTargetFrameIndex))
                {
                    int iBackstepFrameIndex = GetRewindBackstepStartFrameIndex();

                    NS_Core.Logger.Log($"Rewind tail backstep: target={iTargetFrameIndex}, start={iBackstepFrameIndex}, total={recordedFrames.Count}, nearFinish={IsFinalRecordedFrameNearFinishCheckpoint()}");
                    return iBackstepFrameIndex;
                }

                return iTargetFrameIndex;
            }

            private static bool ShouldBackstepFinalRewindStart(Klei.HotLava.Character.PlayerController player, int iTargetFrameIndex)
            {
                if (recordedFrames.Count <= 1)
                    return false;

                if (iTargetFrameIndex < recordedFrames.Count - 1)
                    return false;

                return true;
            }

            private static int GetRewindBackstepStartFrameIndex()
            {
                return UnityEngine.Mathf.Max(0, recordedFrames.Count - 50);
            }

            private static float GetFlatDistance(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
            {
                return UnityEngine.Vector2.Distance(new UnityEngine.Vector2(a.x, a.z), new UnityEngine.Vector2(b.x, b.z));
            }

            private static bool IsFinalRecordedFrameNearFinishCheckpoint()
            {
                if (recordedFrames.Count <= 0 || !Klei.HotLava.LevelSingleton.IsValidToAccessFromOnDisable(false))
                    return false;

                Klei.HotLava.ConsumablePriorityPoint finishCheckpoint = Klei.HotLava.Singleton<Klei.HotLava.LevelSingleton>.Instance.FinishCheckpoint();

                if (finishCheckpoint == null)
                    return false;

                return GetFlatDistance(recordedFrames[recordedFrames.Count - 1].position, finishCheckpoint.GetSpawnPosition()) <= 5f;
            }

            private static void ClearReplayVictoryState(Klei.HotLava.Character.PlayerController player)
            {
            }

            private static System.Collections.Generic.List<Frame> BuildEditableFrameSnapshot()
            {
                System.Collections.Generic.List<Frame> frames = new System.Collections.Generic.List<Frame>();

                int iFrameCount = GetEditableFramesCount();

                for (int i = 0; i < iFrameCount; i++)
                    frames.Add(GetEditableFrame(i));

                return frames;
            }

            private static string GetCurrentRecordFolderPath()
            {
                return BuildRecordFolderPath(ExtractRecordWorldName(), ExtractRecordCourseName());
            }

            private static string GetCurrentCourseRecordFolderPath()
            {
                return BuildRecordFolderPath(GetCurrentWorldName().ToUpperInvariant(), GetCurrentCourseName().ToUpperInvariant());
            }

            private static string BuildRecordFolderPath(string strWorldName, string strCourseName)
            {
                string strBaseFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Furion HotLava", "Movement recorder");

                return System.IO.Path.Combine(strBaseFolder, SanitizeRecordFilePart(strWorldName), SanitizeRecordFilePart(strCourseName));
            }

            private static string ExtractRecordWorldName()
            {
                string strPathDisplay = !string.IsNullOrWhiteSpace(strRecordedPathDisplay) ? strRecordedPathDisplay : (!string.IsNullOrWhiteSpace(strPendingRecordPathDisplay) ? strPendingRecordPathDisplay : GetCurrentPathDisplay());

                int iSeparator = strPathDisplay.IndexOf('/');

                if (iSeparator <= 0)
                    return GetCurrentWorldName();

                return strPathDisplay.Substring(0, iSeparator);
            }

            private static string ExtractRecordCourseName()
            {
                string strPathDisplay = !string.IsNullOrWhiteSpace(strRecordedPathDisplay) ? strRecordedPathDisplay : (!string.IsNullOrWhiteSpace(strPendingRecordPathDisplay) ? strPendingRecordPathDisplay : GetCurrentPathDisplay());

                int iSeparator = strPathDisplay.IndexOf('/');

                if (iSeparator < 0 || iSeparator >= strPathDisplay.Length - 1)
                    return GetCurrentCourseName();

                return strPathDisplay.Substring(iSeparator + 1);
            }

            private static string SanitizeRecordFilePart(string strValue)
            {
                if (string.IsNullOrWhiteSpace(strValue))
                    return "UNKNOWN";

                char[] arrInvalid = System.IO.Path.GetInvalidFileNameChars();
                System.Text.StringBuilder sb = new System.Text.StringBuilder(strValue.Trim());

                for (int i = 0; i < sb.Length; i++)
                {
                    if (System.Array.IndexOf(arrInvalid, sb[i]) >= 0)
                        sb[i] = '_';
                }

                return sb.ToString().Trim();
            }

            private static string GetRecordedPathFileDisplay()
            {
                string strPathDisplay = !string.IsNullOrWhiteSpace(strRecordedPathDisplay) ? strRecordedPathDisplay : strPendingRecordPathDisplay;

                if (string.IsNullOrWhiteSpace(strPathDisplay))
                    return string.Empty;

                if (string.IsNullOrWhiteSpace(strRecordFileName))
                    return strPathDisplay;

                return $"{strPathDisplay}/{strRecordFileName}";
            }

            private static void ShowCenterNotification(string strText)
            {
                strCenterNotification = strText;
                flCenterNotificationUntil = UnityEngine.Time.unscaledTime + 2.2f;
            }

            private static string GetAppliedFramesText(int iAppliedFrames, int iTotalFrames)
            {
                return NS_Core.Utils.Lang.InEN()
                    ? $"APPLIED {iAppliedFrames} FRAMES | TOTAL {iTotalFrames} FRAMES"
                    : $"\u041F\u0420\u0418\u041C\u0415\u041D\u0415\u041D\u041E {iAppliedFrames} \u041A\u0410\u0414\u0420\u041E\u0412 | \u0412\u0421\u0415\u0413\u041E {iTotalFrames} \u041A\u0410\u0414\u0420\u041E\u0412";
            }

            private static string GetCurrentPathId()
            {
                return $"{GetCurrentLevelName()}/{GetCurrentGameModeId()}/{GetCurrentSectionPathId()}";
            }

            private static string GetCurrentPathDisplay()
            {
                return $"{GetCurrentWorldName().ToUpperInvariant()}/{GetCurrentCourseName().ToUpperInvariant()}";
            }

            private static string GetRecordedRouteDisplay()
            {
                if (string.IsNullOrWhiteSpace(strRecordedSectionDisplay))
                    return strRecordedPathDisplay;

                return $"{strRecordedPathDisplay} | {strRecordedSectionDisplay}";
            }

            private static string GetCurrentRouteDisplay()
            {
                string pathDisplay = GetCurrentPathDisplay();
                string sectionDisplay = GetCurrentSectionDisplayZeroBased();

                if (string.IsNullOrWhiteSpace(sectionDisplay))
                    return pathDisplay;

                return $"{pathDisplay} | {sectionDisplay}";
            }

            private static string GetCurrentLevelName()
            {
                try
                {
                    string levelName = piInfoCurrentLevelName?.GetValue(null, null) as string;
                    return string.IsNullOrWhiteSpace(levelName) ? "UNKNOWN_WORLD" : levelName;
                }

                catch
                {
                    return "UNKNOWN_WORLD";
                }
            }

            private static Klei.HotLava.LevelMetaData GetCurrentLevelMetaData()
            {
                try
                {
                    return piInfoCurrentLevelMetaData?.GetValue(null, null) as Klei.HotLava.LevelMetaData;
                }

                catch
                {
                    return null;
                }
            }

            private static Klei.HotLava.Game.GameMode GetCurrentGameMode()
            {
                try
                {
                    return piInfoCurrentGameMode?.GetValue(null, null) as Klei.HotLava.Game.GameMode;
                }

                catch
                {
                    return null;
                }
            }

            private static string GetCurrentWorldName()
            {
                Klei.HotLava.LevelMetaData meta = GetCurrentLevelMetaData();

                if (meta != null && !string.IsNullOrWhiteSpace(meta.NiceLevelName))
                    return meta.NiceLevelName;

                return GetCurrentLevelName();
            }

            private static string GetCurrentGameModeId()
            {
                Klei.HotLava.Game.GameMode gameMode = GetCurrentGameMode();

                if (gameMode == null)
                    return "NO_COURSE";

                string id = piGameModeId?.GetValue(gameMode, null) as string;
                return string.IsNullOrWhiteSpace(id) ? "NO_COURSE" : id;
            }

            private static string GetCurrentSectionPathId()
            {
                int subGameModeIndex = GetCurrentSubGameModeIndex();
                return subGameModeIndex < 0 ? "NO_SECTION" : subGameModeIndex.ToString();
            }

            private static string GetCurrentCourseName()
            {
                Klei.HotLava.LevelMetaData meta = GetCurrentLevelMetaData();
                Klei.HotLava.Game.GameMode gameMode = GetCurrentGameMode();

                if (gameMode == null)
                    return "NO_COURSE";

                if (miInfoGetGameModeName != null)
                {
                    string courseName = miInfoGetGameModeName.Invoke(null, new object[] { meta, gameMode, true }) as string;

                    if (!string.IsNullOrWhiteSpace(courseName))
                        return courseName;
                }

                if (meta != null)
                {
                    string translatedName = meta.GetTranslatedName(gameMode);

                    if (!string.IsNullOrWhiteSpace(translatedName))
                        return translatedName;
                }

                return GetCurrentGameModeId();
            }

            private static string GetCurrentSectionDisplayZeroBased()
            {
                int totalSections = GetCurrentVisibleSectionCount();

                if (totalSections <= 1)
                    return string.Empty;

                int currentSection = GetCurrentVisibleSectionNumberZeroBased(totalSections);

                if (currentSection < 0)
                    return string.Empty;

                return $"\u0421\u0415\u041A\u0426\u0418\u042F \u041A\u0423\u0420\u0421\u0410: {currentSection}/{totalSections}";
            }

            private static int GetCurrentVisibleSectionNumberZeroBased(int iTotalSections)
            {
                if (iTotalSections <= 0)
                    return 0;

                int iSubGameModeIndex = GetCurrentSubGameModeIndex();

                if (iSubGameModeIndex < 0)
                    return 0;

                Klei.HotLava.Game.GameMode objGameMode = GetCurrentGameMode();
                int iCurrentSection = iSubGameModeIndex;

                if (objGameMode != null && objGameMode.m_Type == Klei.HotLava.Game.GameMode.eType.ALL_COURSES)
                {
                    if (iSubGameModeIndex <= 0)
                        iCurrentSection = 0;

                    else if (iSubGameModeIndex >= iTotalSections)
                        iCurrentSection = iTotalSections;

                    else
                        iCurrentSection = iSubGameModeIndex - 1;
                }

                return UnityEngine.Mathf.Clamp(iCurrentSection, 0, iTotalSections);
            }

            private static int GetCurrentVisibleSectionCount()
            {
                Klei.HotLava.LevelMetaData objMeta = GetCurrentLevelMetaData();

                int iGameModeIndex = GetCurrentGameModeIndex();

                if (objMeta == null || objMeta.m_GameModes == null || iGameModeIndex < 0 || iGameModeIndex >= objMeta.m_GameModes.Length || objMeta.m_GameModes[iGameModeIndex] == null)
                    return 0;

                int iTotalSections = objMeta.m_GameModes[iGameModeIndex].Length;

                Klei.HotLava.Game.GameMode objGameMode = GetCurrentGameMode();

                if (objGameMode != null && objGameMode.m_Type == Klei.HotLava.Game.GameMode.eType.ALL_COURSES)
                    iTotalSections -= 1;

                return iTotalSections;
            }

            private static int GetCurrentGameModeIndex()
            {
                try
                {
                    object value = piInfoCurrentGameModeIndex?.GetValue(null, null);
                    return value is int index ? index : -1;
                }

                catch
                {
                    return -1;
                }
            }

            private static int GetCurrentSubGameModeIndex()
            {
                try
                {
                    object value = piInfoSubGameModeIndex?.GetValue(null, null);
                    return value is int index ? index : -1;
                }

                catch
                {
                    return -1;
                }
            }
        }
    }
}
