namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(UnityEngine.Input))]
    internal class Input
    {
        [HarmonyLib.HarmonyPatch("GetKey", new System.Type[] { typeof(UnityEngine.KeyCode) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKey_KeyCode_Prefix(UnityEngine.KeyCode key, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameKey(key))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            if (key == (UnityEngine.KeyCode)277)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetKey", new System.Type[] { typeof(string) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKey_String_Prefix(string name, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            if (name == "insert")
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetKeyDown", new System.Type[] { typeof(UnityEngine.KeyCode) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKeyDown_KeyCode_Prefix(UnityEngine.KeyCode key, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameKey(key))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            if (key == (UnityEngine.KeyCode)277)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetKeyDown", new System.Type[] { typeof(string) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKeyDown_String_Prefix(string name, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            if (name == "insert")
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetKeyUp", new System.Type[] { typeof(UnityEngine.KeyCode) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKeyUp_KeyCode_Prefix(UnityEngine.KeyCode key, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameKey(key))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            if (key == (UnityEngine.KeyCode)277)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetKeyUp", new System.Type[] { typeof(string) })]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetKeyUp_String_Prefix(string name, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            if (name == "insert")
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetMouseButton")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetMouseButton_Prefix(int button, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameMouseButton(button))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetMouseButtonDown")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetMouseButtonDown_Prefix(int button, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameMouseButton(button))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetMouseButtonUp")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetMouseButtonUp_Prefix(int button, ref bool __result)
        {
            if (NS_Core.Utils.ShouldBlockGameMouseButton(button))
            {
                __result = false;
                return false;
            }

            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetAxis")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetAxis_Prefix(string axisName, ref float __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = 0f;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetAxisRaw")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetAxisRaw_Prefix(string axisName, ref float __result)
        {
            if (NS_Core.Movement.Record.IsPlaying)
            {
                if (axisName == "Horizontal")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.x;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"GetAxisRaw({axisName}) during playback frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }

                else if (axisName == "Vertical")
                {
                    __result = NS_Core.Movement.Record.PendingPlaybackFrame.moveInput.y;

                    if (NS_Core.Movement.Record.CurrentFrame <= 5 || NS_Core.Movement.Record.CurrentFrame == 50)
                        NS_Core.Logger.Log($"GetAxisRaw({axisName}) during playback frame {NS_Core.Movement.Record.CurrentFrame}: returning {__result}");

                    return false;
                }
            }

            if (NS_Core.Movement.Record.IsRewinding)
            {
                if (axisName == "Horizontal")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.x;
                    return false;
                }

                else if (axisName == "Vertical")
                {
                    __result = NS_Core.Movement.Record.CurrentRewindFrame.moveInput.y;
                    return false;
                }
            }

            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = 0f;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetButton")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetButton_Prefix(string buttonName, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetButtonDown")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetButtonDown_Prefix(string buttonName, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetButtonUp")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetButtonUp_Prefix(string buttonName, ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI || NS_Core.Movement.Record.IsRecording)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("anyKey", HarmonyLib.MethodType.Getter)]
        [HarmonyLib.HarmonyPrefix]
        private static bool AnyKey_Prefix(ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("anyKeyDown", HarmonyLib.MethodType.Getter)]
        [HarmonyLib.HarmonyPrefix]
        private static bool AnyKeyDown_Prefix(ref bool __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = false;

            return false;
        }

        [HarmonyLib.HarmonyPatch("GetTouch")]
        [HarmonyLib.HarmonyPrefix]
        private static bool GetTouch_Prefix(int index, ref UnityEngine.Touch __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = default(UnityEngine.Touch);

            return false;
        }

        [HarmonyLib.HarmonyPatch("touches", HarmonyLib.MethodType.Getter)]
        [HarmonyLib.HarmonyPrefix]
        private static bool Touches_Prefix(ref UnityEngine.Touch[] __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = new UnityEngine.Touch[0];

            return false;
        }

        [HarmonyLib.HarmonyPatch("touchCount", HarmonyLib.MethodType.Getter)]
        [HarmonyLib.HarmonyPrefix]
        private static bool TouchCount_Prefix(ref int __result)
        {
            if (!NS_Visuals.GUIManager.bShowGUI)
                return true;

            __result = 0;

            return false;
        }
    }
}