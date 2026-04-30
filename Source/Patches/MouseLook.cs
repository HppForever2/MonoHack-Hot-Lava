namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.MouseLook))]
    internal class MouseLook
    {
        private static readonly System.Reflection.FieldInfo fiCharacterTargetRot = HarmonyLib.AccessTools.Field(typeof(Klei.HotLava.MouseLook), "m_CharacterTargetRot");
        private static readonly System.Reflection.FieldInfo fiCameraTargetRot = HarmonyLib.AccessTools.Field(typeof(Klei.HotLava.MouseLook), "m_CameraTargetRot");
        private static Klei.HotLava.MouseLook lastMouseLook;
        private static UnityEngine.Transform lastCharacterTransform;
        private static UnityEngine.Transform lastCameraTransform;

        [HarmonyLib.HarmonyPatch("GetMouseInput")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetMouseInput_Prefix(Klei.HotLava.MouseLook __instance, ref UnityEngine.Vector2 __result)
        {
            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                __result = UnityEngine.Vector2.zero;
                return false;
            }

            if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                __result = UnityEngine.Vector2.zero;
                return false;
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                __result = UnityEngine.Vector2.zero;
                return false;
            }

            if (NS_Core.Movement.Record.IsPlaying)
            {
                __result = NS_Core.Movement.Record.PendingPlaybackFrame.mouseInput;

                if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame % 50 == 0 || (__result.sqrMagnitude > 0.0001f && NS_Core.Movement.Record.CurrentFrame % 10 == 0))
                    NS_Core.Logger.Log($"[PLAYBACK MOUSE] frame {NS_Core.Movement.Record.PendingPlaybackFrameIndex} input=({__result.x}, {__result.y})");

                return false;
            }

            if (NS_Core.Movement.Record.ShouldKeepCameraAngles)
            {
                float lookX = TeamUtility.IO.InputManager.GetAxisRaw("LookHorizontal", TeamUtility.IO.PlayerID.One);
                float lookY = TeamUtility.IO.InputManager.GetAxisRaw("LookVertical", TeamUtility.IO.PlayerID.One);

                if (UnityEngine.Mathf.Abs(lookX) > 0.0001f || UnityEngine.Mathf.Abs(lookY) > 0.0001f)
                {
                    if (__instance == lastMouseLook && lastCharacterTransform != null && lastCameraTransform != null)
                        __instance.Reset(lastCharacterTransform, lastCameraTransform);

                    NS_Core.Movement.Record.DisableKeepCameraAngles();
                    return true;
                }

                __result = UnityEngine.Vector2.zero;
                return false;
            }

            return true;
        }

        [HarmonyLib.HarmonyPatch("LookRotation", new System.Type[] { typeof(UnityEngine.Transform), typeof(UnityEngine.Transform), typeof(UnityEngine.Vector2), typeof(float), typeof(float), typeof(float) })]
        [HarmonyLib.HarmonyPostfix]
        private static void LookRotation_Postfix(Klei.HotLava.MouseLook __instance, UnityEngine.Transform character, UnityEngine.Transform camera)
        {
            lastMouseLook = __instance;
            lastCharacterTransform = character;
            lastCameraTransform = camera;

            if (NS_Core.Movement.Record.IsRecording)
                NS_Core.Movement.Record.CaptureLiveCameraAngles(character, camera);

            if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                NS_Core.Movement.Record.ApplyAutoApproachCamera(character, camera);
                SyncInternalTargets(__instance, character, camera);
            }

            else if (NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsRewinding || NS_Core.Movement.Record.ShouldKeepCameraAngles)
            {
                NS_Core.Movement.Record.ApplyCameraAngles(character, camera);
                SyncInternalTargets(__instance, character, camera);
            }
        }

        private static void SyncInternalTargets(Klei.HotLava.MouseLook mouseLook, UnityEngine.Transform character, UnityEngine.Transform camera)
        {
            fiCharacterTargetRot?.SetValue(mouseLook, character.localRotation);
            fiCameraTargetRot?.SetValue(mouseLook, camera.localRotation);
        }
    }
}