namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.Character.PlayerController))]
    internal class PlayerController
    {
        private static int iLastForcedScrollFrame = -1;
        private static float flPreviousAccumulatedBunnyHop = 0f;
        private static float flPreviousAccumulatedBunnyHopBonus = 0f;

        private static bool IsBhopBindActive()
        {
            if (NS_Core.Utils.IsGameplayInputBlocked())
                return false;

            UnityEngine.KeyCode key = NS_Core.Vars.sTab.sMain.kcBhopKey;
            return NS_Core.Utils.GetKeyState(key);
        }

        private static bool TryGetRecordedUpdateFrame(out NS_Core.Movement.Record.Frame frame)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                frame = NS_Core.Movement.Record.PreparedRecordFrame;
                return true;
            }

            if (NS_Core.Movement.Record.IsRewinding && NS_Core.Movement.Record.HasCurrentRewindFrame)
            {
                frame = NS_Core.Movement.Record.CurrentRewindFrame;
                return true;
            }

            if (NS_Core.Movement.Record.IsPlaying && NS_Core.Movement.Record.HasVisualInputFrame)
            {
                frame = NS_Core.Movement.Record.VisualInputFrame;
                return true;
            }

            frame = default(NS_Core.Movement.Record.Frame);
            return false;
        }

        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPrefix]
        private static void Update_Prefix(Klei.HotLava.Character.PlayerController __instance, ref bool ___m_JumpInput, ref bool ___m_IsJumpHeld)
        {
            if (!__instance.IsMine)
                return;

            if (!NS_Core.Vars.sTab.sMain.bEnableBhop || !IsBhopBindActive() || NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsRewinding || NS_Core.Movement.Record.IsAutoApproaching || NS_Visuals.GUIManager.bShowGUI)
                NS_Core.Vars.bhop.Reset();

            if (NS_Core.Movement.Record.IsRecording)
                NS_Core.Movement.Record.CaptureLiveInput();

            if (!NS_Core.Movement.Record.IsPlaying || !NS_Core.Movement.Record.HasPendingPlaybackFrame)
                return;

            if (NS_Core.Movement.Record.PendingPlaybackFrameIndex == 0)
                iLastForcedScrollFrame = -1;

            NS_Core.Movement.Record.Frame frame = NS_Core.Movement.Record.PendingPlaybackFrame;

            bool scrollJump = UnityEngine.Mathf.Abs(frame.scrollWheel) > 0f;

            if (!scrollJump)
                return;

            if (iLastForcedScrollFrame == NS_Core.Movement.Record.PendingPlaybackFrameIndex)
                return;

            iLastForcedScrollFrame = NS_Core.Movement.Record.PendingPlaybackFrameIndex;

            ___m_IsJumpHeld = false;
            ___m_JumpInput = false;
            __instance.ScrollWheelJumpPressed = false;

            NS_Core.Logger.Log($"[UPDATE SCROLL EDGE] frame {NS_Core.Movement.Record.PendingPlaybackFrameIndex} scroll={frame.scrollWheel}");
        }

        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPostfix]
        private static void Update_Postfix(Klei.HotLava.Character.PlayerController __instance, ref UnityEngine.Vector2 ___m_CachedMouseInput, ref float ___m_ActionPressedRampValue)
        {
            if (!__instance.IsMine)
                return;

            if (!TryGetRecordedUpdateFrame(out NS_Core.Movement.Record.Frame frame))
                return;

            ___m_CachedMouseInput = frame.mouseInput;
            ___m_ActionPressedRampValue = frame.actionPressedRampValue;
        }

        [HarmonyLib.HarmonyPatch("UpdateLanding")]
        [HarmonyLib.HarmonyPostfix]
        private static void UpdateLanding_Postfix(Klei.HotLava.Character.PlayerController __instance, ref bool ___m_JumpInput, ref bool ___m_IsJumpHeld, ref float ___m_LastJumpPressedTime)
        {
            if (!__instance.IsMine)
                return;

            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (!__instance.JustLanded || !__instance.AcceptingInput || __instance.Climbing || __instance.Modifier == null || !__instance.Modifier.CanJump)
                    return;

                if (!NS_Core.Movement.Record.HasBufferedPlaybackLandingJump)
                    return;

                ___m_LastJumpPressedTime = UnityEngine.Time.time;
                ___m_IsJumpHeld = true;
                ___m_JumpInput = true;

                return;
            }

            if (!NS_Core.Vars.sTab.sMain.bEnableBhop || !IsBhopBindActive() || NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsRewinding || NS_Core.Movement.Record.IsAutoApproaching || NS_Visuals.GUIManager.bShowGUI)
            {
                NS_Core.Vars.bhop.Reset();
                return;
            }

            if (!__instance.JustLanded || !__instance.AcceptingInput || __instance.Climbing || __instance.Modifier == null || !__instance.Modifier.CanJump)
                return;

            NS_Core.Vars.bhop.PrepareSpeedRestore(__instance);
            NS_Core.Movement.Record.NotifySyntheticJump();

            ___m_LastJumpPressedTime = UnityEngine.Time.time;
            ___m_IsJumpHeld = true;
            ___m_JumpInput = true;
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPrefix]
        private static void UpdateMovement_BunnyHopSlowmo_Prefix(Klei.HotLava.Character.PlayerController __instance, ref float ___m_AccumulatedBunnyHop, ref float ___m_AccumulatedBunnyHopBonus)
        {
            if (!__instance.IsMine)
                return;

            flPreviousAccumulatedBunnyHop = ___m_AccumulatedBunnyHop;
            flPreviousAccumulatedBunnyHopBonus = ___m_AccumulatedBunnyHopBonus;
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPrefix]
        private static void UpdateMovement_Prefix(Klei.HotLava.Character.PlayerController __instance, ref bool ___m_JumpInput, ref bool ___m_IsJumpHeld, ref float ___m_LastJumpPressedTime)
        {
            if (!__instance.IsMine || NS_Visuals.GUIManager.bShowGUI)
                return;

            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                NS_Core.Movement.Record.UpdatePreparedRecording(__instance);
                return;
            }

            if (NS_Core.Movement.Record.IsRecording)
                NS_Core.Movement.Record.RecordFrame(__instance);

            if (NS_Core.Movement.Record.IsAutoApproaching)
                NS_Core.Movement.Record.AutoApproachUpdate(__instance);

            if (NS_Core.Movement.Record.IsPlaying)
                NS_Core.Movement.Record.PlaybackFrame(__instance);

            if (NS_Core.Movement.Record.IsRewinding)
                NS_Core.Movement.Record.UpdateRewind(__instance);
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPostfix]
        private static void UpdateMovement_BunnyHopSlowmo_Postfix(Klei.HotLava.Character.PlayerController __instance, ref float ___m_AccumulatedBunnyHop, ref float ___m_AccumulatedBunnyHopBonus, ref float ___m_CurrentHopDirection, ref float ___m_BunnyHopModifier, ref bool ___m_JustJumped, Klei.HotLava.Character.PlayerController.MovementSettings ___movementSettings)
        {
            if (!__instance.IsMine || ___movementSettings == null)
                return;

            if (NS_Core.Movement.Record.IsRecordSlowmotionActive)
            {
                float flCompensationMultiplier = NS_Core.Movement.Record.RecordSlowmotionCompensationMultiplier;

                if (flCompensationMultiplier > 1.001f)
                {
                    if (___m_AccumulatedBunnyHop > flPreviousAccumulatedBunnyHop)
                    {
                        float flDelta = ___m_AccumulatedBunnyHop - flPreviousAccumulatedBunnyHop;
                        ___m_AccumulatedBunnyHop = UnityEngine.Mathf.Min(___movementSettings.MaxBunnyHop, flPreviousAccumulatedBunnyHop + flDelta * flCompensationMultiplier);
                    }

                    if (___m_AccumulatedBunnyHopBonus > flPreviousAccumulatedBunnyHopBonus)
                    {
                        float flDeltaBonus = ___m_AccumulatedBunnyHopBonus - flPreviousAccumulatedBunnyHopBonus;
                        ___m_AccumulatedBunnyHopBonus = UnityEngine.Mathf.Min(___movementSettings.MaxBunnyHopBonus, flPreviousAccumulatedBunnyHopBonus + flDeltaBonus * flCompensationMultiplier);
                    }
                }
            }

            if (NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsRewinding)
                NS_Core.Movement.Record.ApplyRecordedBunnyHopRuntimeState(___movementSettings, ref ___m_AccumulatedBunnyHop, ref ___m_AccumulatedBunnyHopBonus, ref ___m_CurrentHopDirection, ref ___m_BunnyHopModifier, ref ___m_JustJumped);

            if (NS_Core.Movement.Record.IsRecording)
                NS_Core.Movement.Record.RefreshRecordedBunnyHopState(__instance, ___m_CurrentHopDirection, ___m_JustJumped);
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPostfix]
        private static void UpdateMovement_HoldModes_Postfix(Klei.HotLava.Character.PlayerController __instance)
        {
            if (!__instance.IsMine || NS_Visuals.GUIManager.bShowGUI)
                return;

            NS_Core.Movement.Record.HoldAfterMovement(__instance);
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPostfix]
        private static void ReducedGravity_Postfix(ref float ___m_GravityModifier)
        {
            ___m_GravityModifier -= NS_Core.Vars.sTab.sMain.flGravityMultiplier;
            NS_Core.Vars.sGame.bIn = true;
        }

        [HarmonyLib.HarmonyPatch("UpdateMovement")]
        [HarmonyLib.HarmonyPostfix]
        private static void IncreasedVelocity_Postfix(ref UnityEngine.Rigidbody ___m_RigidBody)
        {
            ___m_RigidBody.velocity = new UnityEngine.Vector3(___m_RigidBody.velocity.x * (1f + NS_Core.Vars.sTab.sMain.flVelocityMultiplier / 100f), ___m_RigidBody.velocity.y, ___m_RigidBody.velocity.z * (1f + NS_Core.Vars.sTab.sMain.flVelocityMultiplier / 100f));
        }

        [HarmonyLib.HarmonyPatch("KillWithLava")]
        [HarmonyLib.HarmonyPrefix]
        private static bool KillWithLava_Prefix(Klei.HotLava.Character.PlayerController __instance, Klei.HotLava.LavaInstance.eType lava_type)
        {
            return !NS_Core.Vars.sTab.sMain.bEnableGodMode;
        }

        [HarmonyLib.HarmonyPatch("BroadcastKilled")]
        [HarmonyLib.HarmonyPrefix]
        private static bool BroadcastKilled_Prefix(Klei.HotLava.Character.PlayerController __instance, Klei.HotLava.Enums.eDeathReason reason)
        {
            return !NS_Core.Vars.sTab.sMain.bEnableGodMode || reason == Klei.HotLava.Enums.eDeathReason.OutOfBounds || reason == Klei.HotLava.Enums.eDeathReason.GoToCheckpoint || reason == Klei.HotLava.Enums.eDeathReason.Restart;
        }

        [HarmonyLib.HarmonyPatch("get_ActionPressedRampValue")]
        [HarmonyLib.HarmonyPrefix]
        private static bool ActionPressedRampValue_Prefix(Klei.HotLava.Character.PlayerController __instance, ref float __result)
        {
            if (!__instance.IsMine)
                return true;

            if (!TryGetRecordedUpdateFrame(out NS_Core.Movement.Record.Frame frame))
                return true;

            __result = frame.actionPressedRampValue;
            return false;
        }

        [HarmonyLib.HarmonyPatch("GetCachedMouseInput")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetCachedMouseInput_Prefix(Klei.HotLava.Character.PlayerController __instance, ref UnityEngine.Vector2 __result)
        {
            if (!__instance.IsMine)
                return true;

            if (!TryGetRecordedUpdateFrame(out NS_Core.Movement.Record.Frame frame))
                return true;

            __result = frame.mouseInput;
            return false;
        }

        [HarmonyLib.HarmonyPatch("get_AutoGrab")]
        [HarmonyLib.HarmonyPrefix]
        private static bool AutoGrab_Prefix(Klei.HotLava.Character.PlayerController __instance, ref bool __result)
        {
            if (!__instance.IsMine || !NS_Core.Movement.Record.IsRewinding)
                return true;

            __result = false;
            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.ScoreBoard))]
    internal class ScoreBoardPatch
    {
        [HarmonyLib.HarmonyPatch("SetSpeed")]
        [HarmonyLib.HarmonyPrefix]
        private static void SetSpeed_Prefix(ref float speed, ref float acceleration)
        {
            if (!NS_Core.Movement.Record.IsPausedRewind)
                return;

            if (!(NS_Core.Binds.GetLocalPlayer() is Klei.HotLava.Character.PlayerController player) || !player.IsMine)
                return;

            speed = NS_Core.Movement.Record.CurrentRewindFrameSpeed;
            acceleration = player.ForwardSpeed > 0.001f ? speed / player.ForwardSpeed : 0f;
        }
    }
}