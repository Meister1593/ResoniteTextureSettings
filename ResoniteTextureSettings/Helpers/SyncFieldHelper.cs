using FrooxEngine;
using System.Reflection;

namespace ResoniteTextureSettings.Helpers
{
    internal static class SyncFieldHelper
    {
        public static void SetClientside<T>(this Sync<T> sync, T value)
        {
            var field_internal = sync.GetType().GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
            field_internal.SetValue(sync, value);
        }
    }
}
