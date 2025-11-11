using BepInEx.Configuration;

namespace ArribaArribaMod.Utils
{
    /// <summary>
    /// Manages configuration options for the mod
    /// This is an optional utility class to help organize configuration
    /// </summary>
    public static class ConfigManager
    {
        // Add your configuration entries here
        public static ConfigEntry<bool> ExampleBool;
        public static ConfigEntry<float> ExampleFloat;
        public static ConfigEntry<string> ExampleString;

        /// <summary>
        /// Initialize all configuration options
        /// Call this from Plugin.Awake()
        /// </summary>
        public static void Initialize(ConfigFile config)
        {
            Plugin.Log.LogInfo("Initializing configuration...");

            // Example boolean configuration
            ExampleBool = config.Bind(
                "Features",
                "ExampleFeature",
                true,
                "Description of what this feature does"
            );

            // Example float configuration
            ExampleFloat = config.Bind(
                "Values",
                "ExampleMultiplier",
                1.5f,
                new ConfigDescription(
                    "Description of this value",
                    new AcceptableValueRange<float>(0.1f, 10f)  // Min and max values
                )
            );

            // Example string configuration
            ExampleString = config.Bind(
                "General",
                "ExampleText",
                "Default text",
                "Description of this text setting"
            );

            Plugin.Log.LogInfo("Configuration initialized successfully.");
        }

        /// <summary>
        /// Example method to validate configuration values
        /// </summary>
        public static bool ValidateConfig()
        {
            bool isValid = true;

            // Add validation logic here
            if (ExampleFloat.Value < 0)
            {
                Plugin.Log.LogWarning("ExampleMultiplier cannot be negative, resetting to default.");
                ExampleFloat.Value = 1.5f;
                isValid = false;
            }

            return isValid;
        }
    }
}
