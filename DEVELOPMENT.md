# Development Guide

This guide provides detailed information for developing R.E.P.O mods using this template.

## Table of Contents
- [Understanding BepInEx](#understanding-bepinex)
- [Understanding Harmony](#understanding-harmony)
- [Common Patterns](#common-patterns)
- [Debugging](#debugging)
- [Best Practices](#best-practices)
- [Advanced Topics](#advanced-topics)

## Understanding BepInEx

BepInEx is a plugin framework for Unity games. It allows you to load custom code into the game without modifying game files.

### Plugin Lifecycle

1. **Awake()** - Called when the plugin is loaded
   - Initialize your mod here
   - Setup configuration
   - Apply Harmony patches
   
2. **OnDestroy()** - Called when the plugin is being unloaded
   - Clean up resources
   - Unpatch Harmony patches

### Plugin Attributes

```csharp
[BepInPlugin(GUID, Name, Version)]  // Required - identifies your plugin
[BepInProcess("REPO.exe")]          // Optional - only load for specific exe
[BepInDependency("other.mod.guid")] // Optional - require other mods
[BepInIncompatibility("bad.mod")]   // Optional - incompatible mods
```

## Understanding Harmony

Harmony is a library for patching .NET methods at runtime. It's the core tool for modding.

### Patch Types

#### 1. Prefix Patches
Runs **before** the original method. Can prevent original from running.

```csharp
[HarmonyPatch(typeof(ClassName), "MethodName")]
[HarmonyPrefix]
private static bool MyPrefix(ClassName __instance)
{
    // Your code here
    return true;  // true = run original, false = skip original
}
```

#### 2. Postfix Patches
Runs **after** the original method. Can modify return values.

```csharp
[HarmonyPatch(typeof(ClassName), "MethodName")]
[HarmonyPostfix]
private static void MyPostfix(ref ReturnType __result)
{
    // Modify __result
}
```

#### 3. Transpiler Patches
Modifies the IL code directly (advanced).

```csharp
[HarmonyPatch(typeof(ClassName), "MethodName")]
[HarmonyTranspiler]
private static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
{
    // Modify IL instructions
    return instructions;
}
```

#### 4. Finalizer Patches
Like a finally block - always runs, even if the method throws an exception.

```csharp
[HarmonyPatch(typeof(ClassName), "MethodName")]
[HarmonyFinalizer]
private static void MyFinalizer(Exception __exception)
{
    if (__exception != null)
    {
        Plugin.Log.LogError($"Method threw: {__exception}");
    }
}
```

### Special Parameter Names

Harmony recognizes special parameter names in your patches:

- `__instance` - The instance of the class (for instance methods)
- `__result` - The return value (in Postfix only)
- `__state` - Share data between Prefix and Postfix
- `__originalMethod` - MethodBase of the original method
- `__exception` - Exception thrown (in Finalizer only)
- Any original parameter name - Gets that parameter's value

### Patching Different Method Types

**Regular Methods:**
```csharp
[HarmonyPatch(typeof(ClassName), "MethodName")]
```

**Overloaded Methods (specify parameter types):**
```csharp
[HarmonyPatch(typeof(ClassName), "MethodName", new Type[] { typeof(int), typeof(string) })]
```

**Properties:**
```csharp
[HarmonyPatch(typeof(ClassName), "PropertyName", MethodType.Getter)]
[HarmonyPatch(typeof(ClassName), "PropertyName", MethodType.Setter)]
```

**Constructors:**
```csharp
[HarmonyPatch(typeof(ClassName), MethodType.Constructor)]
```

**Generic Methods:**
```csharp
[HarmonyPatch(typeof(ClassName), "GenericMethod")]
[HarmonyPatch(new Type[] { typeof(T) })]  // Specify generic type
```

## Common Patterns

### Pattern 1: Modifying Player Stats

```csharp
[HarmonyPatch(typeof(PlayerStats), "GetMaxHealth")]
[HarmonyPostfix]
private static void IncreaseMaxHealth(ref float __result)
{
    __result *= Plugin.HealthMultiplier.Value;
}
```

### Pattern 2: Adding Custom Behavior

```csharp
[HarmonyPatch(typeof(Player), "Update")]
[HarmonyPostfix]
private static void CustomUpdate(Player __instance)
{
    if (Input.GetKeyDown(KeyCode.F))
    {
        // Custom F key behavior
    }
}
```

### Pattern 3: Preventing Actions

```csharp
[HarmonyPatch(typeof(ItemController), "DropItem")]
[HarmonyPrefix]
private static bool PreventDrop(Item item)
{
    if (item.isQuestItem)
    {
        Plugin.Log.LogInfo("Cannot drop quest items!");
        return false;  // Prevent dropping
    }
    return true;  // Allow normal behavior
}
```

### Pattern 4: Sharing State Between Patches

```csharp
[HarmonyPatch(typeof(Combat), "DealDamage")]
[HarmonyPrefix]
private static void StoreDamage(float damage, out float __state)
{
    __state = damage;  // Store original damage
}

[HarmonyPatch(typeof(Combat), "DealDamage")]
[HarmonyPostfix]
private static void LogDamageChange(float __state, float damage)
{
    Plugin.Log.LogInfo($"Damage changed from {__state} to {damage}");
}
```

## Debugging

### 1. Use Logging Extensively

```csharp
Plugin.Log.LogInfo($"Value: {someValue}");
Plugin.Log.LogWarning("Something might be wrong");
Plugin.Log.LogError("Something is definitely wrong");
```

### 2. Check if References are Null

```csharp
if (instance == null)
{
    Plugin.Log.LogError("Instance is null!");
    return;
}
```

### 3. Try-Catch Blocks

```csharp
try
{
    // Your code
}
catch (Exception ex)
{
    Plugin.Log.LogError($"Error: {ex.Message}\n{ex.StackTrace}");
}
```

### 4. Conditional Compilation

```csharp
#if DEBUG
    Plugin.Log.LogInfo("Debug info");
#endif
```

### 5. Using dnSpy for Live Debugging

1. Build your mod in Debug mode
2. Attach dnSpy to REPO.exe
3. Set breakpoints in your mod code
4. Step through code execution

## Best Practices

### 1. Null Checks
Always check for null references:
```csharp
if (__instance == null || __instance.component == null)
{
    Plugin.Log.LogWarning("Null reference detected");
    return;
}
```

### 2. Configuration
Make your mod configurable:
```csharp
public static ConfigEntry<bool> EnableFeature;

EnableFeature = Config.Bind("General", "EnableFeature", true, "Enable this feature");

if (!EnableFeature.Value) return;  // Check before running
```

### 3. Compatibility
Check for other mods:
```csharp
if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("other.mod.guid"))
{
    // Adjust behavior for compatibility
}
```

### 4. Performance
- Don't patch Update() unless necessary
- Cache frequently accessed values
- Use static fields sparingly
- Avoid creating new objects in hot paths

### 5. Error Handling
Wrap risky code in try-catch:
```csharp
[HarmonyPostfix]
private static void SafePatch()
{
    try
    {
        // Your code
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"Error in patch: {ex}");
    }
}
```

## Advanced Topics

### 1. Accessing Private Fields/Methods

Use Harmony's Traverse:
```csharp
var traverse = Traverse.Create(__instance);
var privateField = traverse.Field("privateFieldName").GetValue<Type>();
traverse.Method("PrivateMethod", param1, param2).GetValue();
```

Or use reflection:
```csharp
var field = typeof(ClassName).GetField("fieldName", 
    BindingFlags.NonPublic | BindingFlags.Instance);
var value = field.GetValue(__instance);
```

### 2. Custom Unity Components

Add custom components to GameObjects:
```csharp
public class CustomBehavior : MonoBehaviour
{
    void Update()
    {
        // Custom behavior
    }
}

// Attach to a GameObject
var component = gameObject.AddComponent<CustomBehavior>();
```

### 3. Coroutines

Run asynchronous operations:
```csharp
private static IEnumerator MyCoroutine()
{
    yield return new WaitForSeconds(1.0f);
    // Do something after 1 second
}

// Start the coroutine
MonoBehaviour.StartCoroutine(MyCoroutine());
```

### 4. Networking Patches (Photon)

Be careful with networked methods:
```csharp
[HarmonyPatch(typeof(NetworkedClass), "RPCMethod")]
[HarmonyPrefix]
private static void NetworkPatch(PhotonView photonView)
{
    if (!photonView.IsMine)
    {
        // Only run for owner
        return;
    }
}
```

### 5. Asset Bundles

Load custom assets:
```csharp
var bundle = AssetBundle.LoadFromFile("path/to/bundle");
var asset = bundle.LoadAsset<GameObject>("assetName");
```

### 6. IL Code Manipulation (Transpilers)

```csharp
[HarmonyTranspiler]
private static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
{
    var codes = new List<CodeInstruction>(instructions);
    
    for (int i = 0; i < codes.Count; i++)
    {
        // Find specific instruction
        if (codes[i].opcode == OpCodes.Ldc_I4 && (int)codes[i].operand == 100)
        {
            codes[i].operand = 200;  // Change value from 100 to 200
        }
    }
    
    return codes.AsEnumerable();
}
```

## Common Issues and Solutions

### Issue: Patch Not Running
**Solution:** Check that:
- Class name is correct
- Method name is correct (case-sensitive)
- Method signature matches
- Harmony.PatchAll() was called

### Issue: NullReferenceException
**Solution:**
- Add null checks
- Verify GameObject/Component exists
- Check timing of patch execution

### Issue: Method Not Found
**Solution:**
- Use dnSpy to verify method name
- Check for method overloads
- Specify parameter types if overloaded

### Issue: Infinite Loop
**Solution:**
- Don't patch methods that your patch calls
- Use Prefix return false instead of replacing behavior

### Issue: Compatibility Issues
**Solution:**
- Use BepInDependency/BepInIncompatibility
- Check for other mods before patching
- Make patches as specific as possible

## Resources

- **Harmony Documentation**: https://harmony.pardeike.net/
- **BepInEx Documentation**: https://docs.bepinex.dev/
- **Unity Scripting Reference**: https://docs.unity3d.com/ScriptReference/
- **dnSpy**: https://github.com/dnSpy/dnSpy
- **BepInEx Discord**: https://discord.gg/MpFEDAg

## Getting Help

1. Check BepInEx logs for errors
2. Use dnSpy to inspect game code
3. Search for similar issues on GitHub
4. Ask in the modding Discord
5. Read Harmony documentation

Happy modding!
