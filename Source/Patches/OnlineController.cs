namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch]
    public class OnlineController
    {
        [HarmonyLib.HarmonyPatch("Klei.HotLava.Online.OnlineController, Assembly-CSharp", "Update")]
        [HarmonyLib.HarmonyPostfix]
        private static void Update_Postfix(object __instance, ref bool ___m_ButtonUp, ref bool ___m_ButtonDown, ref bool ___m_ButtonLeft, ref bool ___m_ButtonRight, ref bool ___m_ButtonWalk, ref bool ___m_ButtonCrouch, ref bool ___m_ButtonJump, ref bool ___m_ButtonAction)
        {
            Klei.HotLava.Character.PlayerController player = ((UnityEngine.Component)__instance).GetComponent<Klei.HotLava.Character.PlayerController>();

            if (player == null || !player.IsMine)
                return;

            if (NS_Core.Movement.Record.HasVisualInputFrame)
            {
                NS_Core.Movement.Record.Frame frame = NS_Core.Movement.Record.VisualInputFrame;

                ___m_ButtonUp = frame.moveInput.y > 0.1f;
                ___m_ButtonDown = frame.moveInput.y < -0.1f;
                ___m_ButtonLeft = frame.moveInput.x < -0.1f;
                ___m_ButtonRight = frame.moveInput.x > 0.1f;
                ___m_ButtonWalk = false;
                ___m_ButtonCrouch = frame.crouch;
                ___m_ButtonJump = frame.jump || frame.landingJump || UnityEngine.Mathf.Abs(frame.scrollWheel) > 0f;
                ___m_ButtonAction = frame.action;

                return;
            }

            if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                UnityEngine.Vector2 input = NS_Core.Movement.Record.GetAutoApproachInput(player);

                ___m_ButtonUp = input.y > 0.1f;
                ___m_ButtonDown = input.y < -0.1f;
                ___m_ButtonLeft = input.x < -0.1f;
                ___m_ButtonRight = input.x > 0.1f;
                ___m_ButtonWalk = false;
                ___m_ButtonCrouch = false;
                ___m_ButtonJump = false;
                ___m_ButtonAction = false;

                return;
            }

            if (NS_Core.Vars.sTab.sMain.bEnableBhop && !player.Grounded)
                ___m_ButtonJump = false;
        }
    }
}