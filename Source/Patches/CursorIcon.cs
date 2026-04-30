namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.CursorIcon))]
    internal class CursorIconPatch
    {
        private const float flBunnyHopBarSize = 0.41f;
        private static float flPreviousAirControlAlpha = 0f;
        private static float flPreviousPerfectJumpTime = -1f;
        private static float flRecordedBunnyHopModifier = 0f;
        private static readonly System.Reflection.PropertyInfo piObservedPlayer = HarmonyLib.AccessTools.Property(typeof(Klei.HotLava.Online.State), "ObservedPlayer");
        private static readonly System.Reflection.FieldInfo fiObservedPlayerController = HarmonyLib.AccessTools.Field(typeof(Klei.HotLava.Online.Player), "m_PlayerController");

        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPrefix]
        private static void Update_Prefix(Klei.HotLava.CursorIcon __instance, ref float ___m_PerfectJumpTime)
        {
            flPreviousAirControlAlpha = __instance.m_AirControlContainer != null ? __instance.m_AirControlContainer.alpha : 0f;
            flPreviousPerfectJumpTime = ___m_PerfectJumpTime;
        }

        [HarmonyLib.HarmonyPatch("Update")]
        [HarmonyLib.HarmonyPostfix]
        private static void Update_Postfix(Klei.HotLava.CursorIcon __instance, ref float ___m_PerfectJumpTime, ref float ___m_BunnyHopPercent, ref float ___m_BunnyHopBonusPercent, ref float ___m_BunnyHopModifier, ref bool ___m_IconEnabled, ref bool ___m_LeftBarFlash, ref bool ___m_RightBarFlash, ref float ___m_LastTimeBunnyHopping, ref bool ___m_FadingOut)
        {
            Klei.HotLava.Online.Player observedPlayer = piObservedPlayer != null ? piObservedPlayer.GetValue(null, null) as Klei.HotLava.Online.Player : null;

            Klei.HotLava.Character.PlayerController player = observedPlayer != null && fiObservedPlayerController != null ? fiObservedPlayerController.GetValue(observedPlayer) as Klei.HotLava.Character.PlayerController : null;

            if (player == null || !player.IsMine)
                return;

            if (NS_Core.Movement.Record.HasVisualInputFrame && (NS_Core.Movement.Record.IsPreparingRecord || NS_Core.Movement.Record.IsPlaying || NS_Core.Movement.Record.IsRewinding) && NS_Core.Movement.Record.HasRecordedBunnyHopState)
                ApplyRecordedBunnyHopVisualState(__instance, NS_Core.Movement.Record.VisualInputFrame, ref ___m_BunnyHopPercent, ref ___m_BunnyHopBonusPercent, ref ___m_BunnyHopModifier, ref ___m_IconEnabled, ref ___m_LeftBarFlash, ref ___m_RightBarFlash, ref ___m_LastTimeBunnyHopping, ref ___m_FadingOut);

            else
                flRecordedBunnyHopModifier = 0f;

            if (!NS_Core.Movement.Record.IsRecordSlowmotionActive)
                return;

            float flCompensationMultiplier = NS_Core.Movement.Record.RecordSlowmotionCompensationMultiplier;

            if (flCompensationMultiplier <= 1.001f)
                return;

            if (__instance.m_AirControlContainer != null)
            {
                float flDeltaAlpha = __instance.m_AirControlContainer.alpha - flPreviousAirControlAlpha;

                if (UnityEngine.Mathf.Abs(flDeltaAlpha) > 0.0001f)
                    __instance.m_AirControlContainer.alpha = UnityEngine.Mathf.Clamp01(flPreviousAirControlAlpha + flDeltaAlpha * flCompensationMultiplier);
            }

            if (___m_PerfectJumpTime > flPreviousPerfectJumpTime && flPreviousPerfectJumpTime >= 0f)
                ___m_PerfectJumpTime = flPreviousPerfectJumpTime + (___m_PerfectJumpTime - flPreviousPerfectJumpTime) * flCompensationMultiplier;
        }

        private static void ApplyRecordedBunnyHopVisualState(Klei.HotLava.CursorIcon __instance, NS_Core.Movement.Record.Frame frame, ref float ___m_BunnyHopPercent, ref float ___m_BunnyHopBonusPercent, ref float ___m_BunnyHopModifier, ref bool ___m_IconEnabled, ref bool ___m_LeftBarFlash, ref bool ___m_RightBarFlash, ref float ___m_LastTimeBunnyHopping, ref bool ___m_FadingOut)
        {
            float flBunnyHopPercent = UnityEngine.Mathf.Clamp01(frame.bunnyHopPercent);
            float flBunnyHopBonusPercent = UnityEngine.Mathf.Clamp01(frame.bunnyHopBonusPercent);
            float flDirectionModifier = UnityEngine.Mathf.Abs(frame.bunnyHopModifier) > 0.0001f ? frame.bunnyHopModifier : frame.bunnyHopDirection;

            if (flBunnyHopPercent <= 0f && flBunnyHopBonusPercent <= 0f)
            {
                flRecordedBunnyHopModifier = 0f;

                ___m_LeftBarFlash = false;
                ___m_RightBarFlash = false;

                if (__instance.m_LeftAirControl != null)
                    __instance.m_LeftAirControl.color = new UnityEngine.Color(1f, 1f, 1f, 0.39f);

                if (__instance.m_RightAirControl != null)
                    __instance.m_RightAirControl.color = new UnityEngine.Color(1f, 1f, 1f, 0.39f);
            }

            else if (UnityEngine.Mathf.Abs(flBunnyHopPercent) <= 0.22f && UnityEngine.Mathf.Abs(flDirectionModifier) > 0.0001f)
                flRecordedBunnyHopModifier = flDirectionModifier;

            else if (UnityEngine.Mathf.Abs(flRecordedBunnyHopModifier) <= 0.0001f && UnityEngine.Mathf.Abs(flDirectionModifier) > 0.0001f)
                flRecordedBunnyHopModifier = flDirectionModifier;

            float flBunnyHopModifier = UnityEngine.Mathf.Abs(flRecordedBunnyHopModifier) > 0.0001f ? flRecordedBunnyHopModifier : flDirectionModifier;

            ___m_BunnyHopPercent = flBunnyHopPercent;
            ___m_BunnyHopBonusPercent = flBunnyHopBonusPercent;
            ___m_BunnyHopModifier = flBunnyHopModifier;

            float flMainFill = flBunnyHopPercent > 0f ? flBunnyHopBarSize * flBunnyHopPercent : 0f;
            float flBonusFill = flBunnyHopBonusPercent > 0f ? flBunnyHopBarSize * flBunnyHopBonusPercent : 0f;
            bool bShouldShowBars = UnityEngine.Mathf.Abs(flBunnyHopModifier) > 0.075f || flMainFill > 0f || flBonusFill > 0f;

            if (bShouldShowBars)
            {
                if (__instance.m_AirControlContainer != null)
                    __instance.m_AirControlContainer.gameObject.SetActive(true);

                if (__instance.m_LeftAirControl != null)
                    __instance.m_LeftAirControl.gameObject.SetActive(true);

                if (__instance.m_RightAirControl != null)
                    __instance.m_RightAirControl.gameObject.SetActive(true);

                ___m_IconEnabled = true;
                ___m_LastTimeBunnyHopping = UnityEngine.Time.time;
                ___m_FadingOut = false;
            }

            if (__instance.m_LeftAirControl != null)
                __instance.m_LeftAirControl.fillAmount = flBunnyHopModifier < 0f ? flMainFill : flBonusFill;

            if (__instance.m_RightAirControl != null)
                __instance.m_RightAirControl.fillAmount = flBunnyHopModifier > 0f ? flMainFill : flBonusFill;

            if (__instance.m_LeftAirControl != null && flBunnyHopModifier >= 0f && flBunnyHopBonusPercent <= 0f)
                __instance.m_LeftAirControl.fillAmount = 0f;

            if (__instance.m_RightAirControl != null && flBunnyHopModifier <= 0f && flBunnyHopBonusPercent <= 0f)
                __instance.m_RightAirControl.fillAmount = 0f;

            if (!bShouldShowBars)
            {
                ___m_LeftBarFlash = false;
                ___m_RightBarFlash = false;
            }

            if (__instance.m_AirControlContainer != null)
                __instance.m_AirControlContainer.alpha = bShouldShowBars ? 1f : 0f;
        }
    }
}