using Elements.Core;
using FrooxEngine;

namespace ResoniteTextureSettings.Helpers
{
    internal class StaticTextureHelper
    {
        public static void ModifyDescriptor(StaticTexture2D texture)
        {
            int master_limit = (int)Config.GetValue<TextureLimit>(ResoniteTextureSettings.MasterTexLimit);
            int? current_size = texture.MaxSize.Value;

            if (ForceCompression) texture.Uncompressed.SetClientside(false);

            if (master_limit != 0)
            {
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                texture.MaxSize.SetClientside(newval);
            };
        }

        public static void ModifyDescriptor(StaticCubemap texture)
        {
            int master_limit = (int)Config.GetValue(ResoniteTextureSettings.MasterTexLimit);
            int? current_size = texture.MaxSize.Value;

            if (master_limit != 0)
            {
                int newval = (current_size == null) ? master_limit : MathX.Min((int)current_size, master_limit);
                texture.MaxSize.SetClientside(newval);
            };
        }
        private static bool ApplyAndroidFixes => Config.GetValue(ResoniteTextureSettings.AndroidFixes);
        private static bool ForceCompression => ApplyAndroidFixes || Config.GetValue(ResoniteTextureSettings.UseCompressed);
    }
}
