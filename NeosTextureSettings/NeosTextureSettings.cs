using HarmonyLib;
using NeosModLoader;

namespace NeosTextureSettings
{
    public class NeosTextureSettings : NeosMod
    {
        public override string Name => "NeosTextureSettings";
        public override string Author => "Raemien";
        public override string Version => "0.1.0";
        public override string Link => "https://github.com/Raemien/NeosTextureSettings";

        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<bool> ANDROID_FIXES = new ModConfigurationKey<bool>("Android Compatibility Fixes", "Fix Android/Quest Compatibility", () => Config.IsAndroid(), !Config.IsAndroid());
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<TextureLimit> MASTER_TEX_LIMIT = new ModConfigurationKey<TextureLimit>("Master Texture Limit", Config.GetMasterLimitDesc(), () => Config.DefaultSizeForPlatform());
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<bool> LIMIT_CUBEMAPS = new ModConfigurationKey<bool>("Limit Affects Cubemaps", "Apply limit to Cubemaps", () => false);
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<bool> USE_COMPRESSED = new ModConfigurationKey<bool>("Ignore Uncompressed Flag", "Always ask for compressed textures", () => false, Config.IsAndroid());

        public override void OnEngineInit()
        {
            Config._config = GetConfiguration();
            Harmony harmony = new Harmony("net.raemien.NeosTextureSettings");
            harmony.PatchAll();
        }
    }
}