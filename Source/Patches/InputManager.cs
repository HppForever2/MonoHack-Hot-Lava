namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(TeamUtility.IO.InputManager))]
    internal class InputManager
    {
        [HarmonyLib.HarmonyPatch("GetAxis")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetAxis_Prefix(string name, ref float __result)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                if (NS_Core.Movement.Record.ShouldUsePreparedRecordInput)
                {
                    if (name == "Horizontal" || name == "_Horizontal")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.moveInput.x;
                        return false;
                    }

                    else if (name == "Vertical" || name == "_Vertical")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.moveInput.y;
                        return false;
                    }
                }

                if (name == "Horizontal" || name == "_Horizontal" || name == "Vertical" || name == "_Vertical")
                {
                    __result = 0f;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.x;
                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.y;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                UnityEngine.Vector2 autoApproachInput = NS_Core.Movement.Record.GetAutoApproachInput();

                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = autoApproachInput.x;
                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = autoApproachInput.y;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.x;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetAxis({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.y;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetAxis({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }
            }

            return true;
        }

        [HarmonyLib.HarmonyPatch("GetAxisRaw")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetAxisRaw_Prefix(string name, ref float __result)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                if (NS_Core.Movement.Record.ShouldUsePreparedRecordInput)
                {
                    if (name == "Horizontal" || name == "_Horizontal")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.moveInput.x;
                        return false;
                    }

                    else if (name == "Vertical" || name == "_Vertical")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.moveInput.y;
                        return false;
                    }

                    else if (name == "Zoom")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.scrollWheel;
                        return false;
                    }
                }

                if (name == "Horizontal" || name == "_Horizontal" || name == "Vertical" || name == "_Vertical" || name == "Zoom")
                {
                    __result = 0f;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.x;
                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.y;
                    return false;
                }

                else if (name == "Zoom")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.scrollWheel;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                UnityEngine.Vector2 autoApproachInput = NS_Core.Movement.Record.GetAutoApproachInput();

                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = autoApproachInput.x;
                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = autoApproachInput.y;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (name == "Horizontal" || name == "_Horizontal")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.x;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetAxisRaw({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "Vertical" || name == "_Vertical")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.y;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetAxisRaw({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "Zoom")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.scrollWheel;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50 || UnityEngine.Mathf.Abs(__result) > 0f)
                        NS_Core.Logger.Log($"InputManager.GetAxisRaw({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }
            }

            return true;
        }

        [HarmonyLib.HarmonyPatch("GetButton")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetButton_Prefix(string name, ref bool __result)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                if (NS_Core.Movement.Record.ShouldUsePreparedRecordInput)
                {
                    if (name == "Jump")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.jump;
                        return false;
                    }

                    else if (name == "Crouch")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.crouch;
                        return false;
                    }

                    else if (name == "Action")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.action;
                        return false;
                    }

                    else if (name == "HoldDirection")
                    {
                        __result = NS_Core.Movement.Record.PreparedRecordFrame.holdDirection;
                        return false;
                    }
                }

                if (name == "Jump" || name == "Crouch" || name == "Action" || name == "HoldDirection")
                {
                    __result = false;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                if (name == "Jump")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.jump;
                    return false;
                }

                else if (name == "Crouch")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.crouch;
                    return false;
                }

                else if (name == "Action")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.action;
                    return false;
                }

                else if (name == "HoldDirection")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.holdDirection;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (name == "Jump")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.jump;
                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButton({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");
                    return false;
                }

                else if (name == "Crouch")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.crouch;
                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButton({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");
                    return false;
                }

                else if (name == "Action")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.action;
                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButton({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");
                    return false;
                }

                else if (name == "HoldDirection")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.holdDirection;
                    return false;
                }
            }

            return true;
        }

        [HarmonyLib.HarmonyPatch("GetButtonDown")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetButtonDown_Prefix(string name, ref bool __result)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                if (NS_Core.Movement.Record.ShouldUsePreparedRecordInput)
                {
                    __result = false;
                    return false;
                }

                if (name == "Jump" || name == "Crouch" || name == "Action" || name == "HoldDirection")
                {
                    __result = false;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                if (name == "Jump")
                {
                    __result = NS_Core.Movement.Record.IsRewindJumpDown;
                    return false;
                }

                else if (name == "Crouch")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.crouch;
                    return false;
                }

                else if (name == "Action")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.action;
                    return false;
                }

                else if (name == "HoldDirection")
                {
                    __result = false;
                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (name == "Jump")
                {
                    __result = NS_Core.Movement.Record.IsPendingJumpDown;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButtonDown({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "Crouch")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.crouch;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButtonDown({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "Action")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.action;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"InputManager.GetButtonDown({name}) frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (name == "HoldDirection")
                {
                    __result = false;
                    return false;
                }
            }

            return true;
        }
    }
}