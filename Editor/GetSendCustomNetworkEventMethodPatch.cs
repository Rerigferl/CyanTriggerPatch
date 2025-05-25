#if VRC_SDK_3_8_1
using System;
using System.Reflection;
using HarmonyLib;
using UnityEditor;
using VRC.Udon.Common.Interfaces;

namespace GetSendCustomNetworkEventMethodPatch
{
    [InitializeOnLoad]
    internal static class GetSendCustomNetworkEventMethodPatch
    {
        static GetSendCustomNetworkEventMethodPatch()
        {
            var harmony = new Harmony("Numeira.GetSendCustomNetworkEventMethodPatch");
            harmony.Patch(typeof(Type).GetMethod("GetMethod", new[] { typeof(string) }), prefix: new HarmonyMethod(typeof(GetSendCustomNetworkEventMethodPatch), nameof(GetMethodProxy2)));
        }

        private static bool GetMethodProxy2( string name, Type __instance, ref MethodInfo __result)
        {
            if (name is not "SendCustomNetworkEvent")
                return true;
            __result = __instance.GetMethod(name, new[] { typeof(NetworkEventTarget), typeof(string) });
            return false;
        }
    }
}
#endif