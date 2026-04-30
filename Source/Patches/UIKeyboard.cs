namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.UIKeyboard))]
    public class UIKeyboardPatch
    {
        [HarmonyLib.HarmonyPatch("LateUpdate")]
        [HarmonyLib.HarmonyPostfix]
        private static void LateUpdate_Postfix(Klei.HotLava.UI.UIKeyboard __instance, UnityEngine.UI.Image ___m_Jump)
        {
            if (!NS_Core.Vars.sTab.sMain.bEnableBhop)
                return;

            UnityEngine.Camera camera = UnityEngine.Camera.main;

            if (camera == null)
                return;

            Klei.HotLava.Character.PlayerController player = camera.GetComponentInParent<Klei.HotLava.Character.PlayerController>();

            if (player == null)
                return;

            if (player.IsMine && !player.Grounded)
            {
                ___m_Jump.color = UnityEngine.Color.white;
                ___m_Jump.sprite = __instance.m_DefaultSprite;
            }
        }
    }
}