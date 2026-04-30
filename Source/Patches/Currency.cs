namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch]
    internal class Currency
    {
        private static System.Reflection.MethodBase TargetMethod()
        {
            return HarmonyLib.AccessTools.Method(HarmonyLib.AccessTools.TypeByName("Klei.HotLava.Inventory.Currency"), "EarnCurrency", null, null);
        }

        [HarmonyLib.HarmonyPrefix]
        private static void EarnCurrency_Prefix(ref int amount)
        {
            amount = NS_Core.Vars.sTab.sMain.bMultiplied100X_Currency ? System.Math.Abs(amount) * 100 : amount;
        }
    }
}