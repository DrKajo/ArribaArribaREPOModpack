using UnityEngine;
using System.Collections.Generic;

namespace ArribaArribaMod.Utils
{
    /// <summary>
    /// Utility helper methods for common modding tasks
    /// </summary>
    public static class ModHelpers
    {
        /// <summary>
        /// Find a GameObject by name in the scene
        /// </summary>
        public static GameObject FindGameObject(string name)
        {
            return GameObject.Find(name);
        }

        /// <summary>
        /// Find all GameObjects with a specific tag
        /// </summary>
        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            return GameObject.FindGameObjectsWithTag(tag);
        }

        /// <summary>
        /// Get a component from a GameObject safely
        /// </summary>
        public static T GetComponentSafe<T>(GameObject obj) where T : Component
        {
            if (obj == null)
            {
                Plugin.Log.LogWarning("Attempted to get component from null GameObject");
                return null;
            }

            T component = obj.GetComponent<T>();
            if (component == null)
            {
                Plugin.Log.LogWarning($"Component {typeof(T).Name} not found on {obj.name}");
            }

            return component;
        }

        /// <summary>
        /// Log all active GameObjects in the scene (useful for debugging)
        /// </summary>
        public static void LogAllGameObjects()
        {
            Plugin.Log.LogInfo("=== Active GameObjects in Scene ===");
            foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
            {
                if (obj.activeInHierarchy)
                {
                    Plugin.Log.LogInfo($"GameObject: {obj.name} - Tag: {obj.tag}");
                }
            }
            Plugin.Log.LogInfo("=== End of GameObject List ===");
        }

        /// <summary>
        /// Print the hierarchy of a GameObject and its children
        /// </summary>
        public static void LogGameObjectHierarchy(GameObject obj, int depth = 0)
        {
            if (obj == null) return;

            string indent = new string(' ', depth * 2);
            Plugin.Log.LogInfo($"{indent}- {obj.name}");

            foreach (Transform child in obj.transform)
            {
                LogGameObjectHierarchy(child.gameObject, depth + 1);
            }
        }

        /// <summary>
        /// Get all components on a GameObject
        /// </summary>
        public static void LogAllComponents(GameObject obj)
        {
            if (obj == null)
            {
                Plugin.Log.LogWarning("Cannot log components of null GameObject");
                return;
            }

            Plugin.Log.LogInfo($"=== Components on {obj.name} ===");
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                Plugin.Log.LogInfo($"- {component.GetType().Name}");
            }
            Plugin.Log.LogInfo("=== End of Component List ===");
        }

        /// <summary>
        /// Convert a color from hex string to Unity Color
        /// </summary>
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace("#", "");
            
            if (hex.Length != 6 && hex.Length != 8)
            {
                Plugin.Log.LogError($"Invalid hex color string: {hex}");
                return Color.white;
            }

            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = hex.Length == 8 ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) : (byte)255;

            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Convert Unity Color to hex string
        /// </summary>
        public static string ColorToHex(Color color)
        {
            Color32 color32 = color;
            return $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}{color32.a:X2}";
        }
    }
}
