using BaseX;
using FrooxEngine;

namespace NeosTextureSettings.Helpers
{
    internal class StaticTextureHelper
    {
        public static void ModifyDescriptor(StaticTexture2D __instance)
        {
            int master_limit = (int)Config.GetValue(NeosTextureSettings.MASTER_TEX_LIMIT);
            int? current_size = __instance.MaxSize.Value;

            if (ForceCompression) __instance.Uncompressed.SetClientside(false);

            if (master_limit != 0)
            {
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                __instance.MaxSize.SetClientside(newval);
            };
        }

        public static void ModifyDescriptor(StaticCubemap __instance)
        {
            int master_limit = (int)Config.GetValue(NeosTextureSettings.MASTER_TEX_LIMIT);
            int? current_size = __instance.MaxSize.Value;

            if (master_limit != 0)
            {
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                __instance.MaxSize.SetClientside(newval);
            };
        }
        private static bool ApplyAndroidFixes => Config.GetValue(NeosTextureSettings.ANDROID_FIXES);
        private static bool ForceCompression => ApplyAndroidFixes || Config.GetValue(NeosTextureSettings.USE_COMPRESSED);
    }
}
