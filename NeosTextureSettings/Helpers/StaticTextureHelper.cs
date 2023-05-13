using BaseX;
using FrooxEngine;
using System.Reflection;

namespace NeosTextureSettings.Helpers
{
    internal class StaticTextureHelper
    {
        public static void ModifyDescriptor(StaticTexture2D __instance)
        {
            int master_limit = (int)Config.GetValue(NeosTextureSettings.MASTER_TEX_LIMIT);
            int? current_size = __instance.MaxSize.Value;

            if (ForceCompression)
            {
                var field_uncompressed = __instance.Uncompressed.GetType().GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
                field_uncompressed.SetValue(__instance.Uncompressed, false);
            }

            if (master_limit != 0)
            {
                var field_maxsize = __instance.MaxSize.GetType().GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                field_maxsize.SetValue(__instance.MaxSize, newval);
            };
        }

        public static void ModifyDescriptor(StaticCubemap __instance)
        {
            int master_limit = (int)Config.GetValue(NeosTextureSettings.MASTER_TEX_LIMIT);
            int? current_size = __instance.MaxSize.Value;

            if (master_limit != 0)
            {
                var field_maxsize = __instance.MaxSize.GetType().GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                field_maxsize.SetValue(__instance.MaxSize, newval);
            };
        }

        private static bool ApplyAndroidFixes => Config.GetValue(NeosTextureSettings.ANDROID_FIXES);
        private static bool ForceCompression => ApplyAndroidFixes || Config.GetValue(NeosTextureSettings.USE_COMPRESSED);

    }
}
