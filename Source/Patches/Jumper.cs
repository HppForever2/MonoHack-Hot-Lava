namespace Hot_Lava_Cheat.Source.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(Klei.HotLava.Character.Jumper))]
    internal class Jumper
    {
        [HarmonyLib.HarmonyPatch("Jump")]
        [HarmonyLib.HarmonyPostfix]
        private static void Jump_Postfix(ref UnityEngine.Rigidbody ___m_RigidBody, ref Klei.HotLava.Character.PlayerController ___m_Player)
        {
            if (___m_Player == null || !___m_Player.IsMine)
                return;

            if (!NS_Core.Vars.bhop.TryConsumeSpeedRestore(out float flFlatSpeed))
                return;

            if (flFlatSpeed <= 0f)
                return;

            UnityEngine.Vector3 velocity = ___m_RigidBody.velocity;
            UnityEngine.Vector3 flatVelocity = new UnityEngine.Vector3(velocity.x, 0f, velocity.z);

            if (flatVelocity.sqrMagnitude > 0f)
                flatVelocity = flatVelocity.normalized * flFlatSpeed;

            else
                flatVelocity = ___m_Player.transform.forward * flFlatSpeed;

            velocity.x = flatVelocity.x;
            velocity.z = flatVelocity.z;
            ___m_RigidBody.velocity = velocity;
        }
    }
}