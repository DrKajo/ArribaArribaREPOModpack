using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ArribaArribaMod
{
    /// <summary>
    /// Main plugin class for the Arriba Arriba mod
    /// This is the entry point of your mod
    /// </summary>
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("REPO.exe")]
    public class Plugin : BaseUnityPlugin
    {
        // Logger for outputting messages to the BepInEx console
        internal static ManualLogSource Log;
        
        // Harmony instance for patching game methods
        private Harmony _harmony;
        
        // Configuration entries - you can add more as needed
        internal static ConfigEntry<bool> EnableFeature;
        internal static ConfigEntry<int> ExampleValue;

        /// <summary>
        /// Awake is called when the plugin is loaded
        /// Use this to initialize your mod
        /// </summary>
        private void Awake()
        {
            // Initialize the logger
            Log = Logger;
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loading...");

            // Setup configuration
            InitializeConfig();

            // Setup Harmony patches
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            
            // Patch all methods in the Patches namespace
            _harmony.PatchAll(typeof(ExamplePatches));
            
            // You can also patch specific classes:
            // _harmony.PatchAll(typeof(YourPatchClass));

            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        /// <summary>
        /// Initialize configuration options
        /// </summary>
        private void InitializeConfig()
        {
            EnableFeature = Config.Bind(
                "General",                          // Section
                "EnableFeature",                    // Key
                true,                               // Default value
                "Enable or disable this feature"    // Description
            );

            ExampleValue = Config.Bind(
                "General",
                "ExampleValue",
                100,
                "An example integer configuration value"
            );

            Log.LogInfo("Configuration loaded successfully.");
        }

        /// <summary>
        /// OnDestroy is called when the plugin is being unloaded
        /// </summary>
        private void OnDestroy()
        {
            // Unpatch all Harmony patches
            _harmony?.UnpatchSelf();
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is unloaded!");
        }
    }
}
