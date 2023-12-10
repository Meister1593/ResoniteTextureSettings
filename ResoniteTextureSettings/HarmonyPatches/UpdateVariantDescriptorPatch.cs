using Elements.Assets;
using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;
using ResoniteTextureSettings.Helpers;

namespace ResoniteTextureSettings.HarmonyPatches
{
    [HarmonyPatch(typeof(StaticTextureProvider<Texture2D, Bitmap2D, BitmapMetadata, FrooxEngine.Texture2DVariantDescriptor>), "UpdateVariantDescriptor")]
    internal class UpdateVariantDescriptorPatch
    {
        private static bool Prefix(object __instance)
        {
            bool limit_cubemaps = Config.GetValue(ResoniteTextureSettings.LimitCubemaps);
            var texture_type = __instance.GetType();

            if (texture_type == typeof(StaticTexture2D))
            {
                StaticTextureHelper.ModifyDescriptor(__instance as StaticTexture2D);
                return true;
            }

            if (texture_type == typeof(StaticCubemap) && limit_cubemaps)
            {
                StaticTextureHelper.ModifyDescriptor(__instance as StaticCubemap);
                return true;
            }

            ResoniteMod.Debug("[ResoniteTextureSettings] Unknown StaticTexture Type: " + texture_type.Name);

            return true;
        }
    }
}
