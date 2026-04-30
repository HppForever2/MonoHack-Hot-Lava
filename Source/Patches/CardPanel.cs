namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.UI.CardPanel))]
    internal class CardPanel
    {
        [HarmonyLib.HarmonyPrefix]
        [HarmonyLib.HarmonyPatch("SetCards")]
        private static void SetCards_Prefix(Klei.HotLava.UI.CardPanel __instance, ref ulong count, ref ulong total)
        {
            if (!NS_Core.Vars.sTab.sMain.bGetAllCards)
                return;
            
            count = total;

            if (__instance.m_CardCount != null)
                __instance.m_CardCount.text = count.ToString() + "/" + total.ToString();
        }

        [HarmonyLib.HarmonyPatch("AddCards")]
        [HarmonyLib.HarmonyPrefix]
        private static bool AddCards_Prefix(Klei.HotLava.UI.CardPanel __instance, ulong currentCards, ulong newCards, ulong totalCards, Klei.HotLava.Inventory.eCardType card_type)
        {
            return !NS_Core.Vars.sTab.sMain.bGetAllCards;
        }
    }
}