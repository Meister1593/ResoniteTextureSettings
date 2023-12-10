using HarmonyLib;
using ResoniteModLoader;

namespace ResoniteTextureSettings
{
    public class ResoniteTextureSettings : ResoniteMod
    {
        public override string Name => "ResoniteTextureSettings";
        public override string Author => "Raemien + PLYSHKA";
        public override string Version => "1.0.1";
        public override string Link => "https://github.com/PLYSHKA/ResoniteTextureSettings";

        // [AutoRegisterConfigKey]
        // internal static readonly ModConfigurationKey<bool> AndroidFixes = new("Android Compatibility Fixes", "Fix Android/Quest Compatibility", () => Config.IsAndroid(), !Config.IsAndroid());
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<TextureLimit> MasterTexLimit = new("Master Texture Limit", Config.GetMasterLimitDesc(), () => Config.DefaultSizeForPlatform());
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<bool> LimitCubemaps = new("Limit Affects Cubemaps", "Apply limit to Cubemaps", () => false);
        [AutoRegisterConfigKey]
        internal static readonly ModConfigurationKey<bool> UseCompressed = new("Ignore Uncompressed Flag", "Strictly enforce texture compression", () => false, Config.IsAndroid());

        public override void OnEngineInit()
        {
            Config._config = GetConfiguration();
            new Harmony("net.raemien.plyshka.ResoniteTextureSettings").PatchAll();
        }
    }
}