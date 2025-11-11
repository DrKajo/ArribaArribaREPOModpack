using HarmonyLib;
using UnityEngine;

namespace ArribaArribaMod
{
    /// <summary>
    /// Example patches using Harmony
    /// Harmony allows you to modify existing game methods without changing the game's code
    /// </summary>
    [HarmonyPatch]
    public class ExamplePatches
    {
        // Example 1: Prefix patch
        // Runs BEFORE the original method
        // Return false to skip the original method, true to run it
        
        /*
        [HarmonyPatch(typeof(TargetClassName), "MethodName")]
        [HarmonyPrefix]
        private static bool ExamplePrefix()
        {
            Plugin.Log.LogInfo("Prefix patch running before original method");
            return true; // Return true to run the original method, false to skip it
        }
        */

        // Example 2: Postfix patch
        // Runs AFTER the original method
        // Can access the return value with __result parameter
        
        /*
        [HarmonyPatch(typeof(TargetClassName), "MethodName")]
        [HarmonyPostfix]
        private static void ExamplePostfix(ref ReturnType __result)
        {
            Plugin.Log.LogInfo("Postfix patch running after original method");
            // You can modify the return value here
            // __result = newValue;
        }
        */

        // Example 3: Patch with access to instance and parameters
        
        /*
        [HarmonyPatch(typeof(TargetClassName), "MethodName")]
        [HarmonyPrefix]
        private static void ExamplePatchWithParams(TargetClassName __instance, ParameterType parameterName)
        {
            Plugin.Log.LogInfo($"Method called with parameter: {parameterName}");
            // __instance gives you access to the instance of the class
            // You can access its fields and methods
        }
        */

        // Example 4: Transpiler patch (Advanced)
        // Modifies the IL code of the method
        // Use this for more complex modifications
        
        /*
        [HarmonyPatch(typeof(TargetClassName), "MethodName")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> ExampleTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            
            // Modify the IL instructions here
            
            return codes.AsEnumerable();
        }
        */

        // Example 5: Patching a property getter/setter
        
        /*
        [HarmonyPatch(typeof(TargetClassName), "PropertyName", MethodType.Getter)]
        [HarmonyPostfix]
        private static void PropertyGetterPatch(ref PropertyType __result)
        {
            Plugin.Log.LogInfo("Property getter accessed");
        }
        */

        // Tips for finding what to patch:
        // 1. Use dnSpy or ILSpy to decompile Assembly-CSharp.dll
        // 2. Look for the classes and methods you want to modify
        // 3. Create patches targeting those methods
        // 4. Use Plugin.Log.LogInfo() to debug your patches
        
        // Common R.E.P.O classes you might want to patch:
        // - PlayerController (player movement and actions)
        // - GameManager (game state and flow)
        // - ItemController (item interactions)
        // - UIManager (user interface)
        // - NetworkManager (multiplayer/networking)
    }
}
