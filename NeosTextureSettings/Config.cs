using CodeX;
using FrooxEngine;
using NeosModLoader;
using System.Reflection;

namespace NeosTextureSettings
{
    public enum AndroidTextureFormat
    {
        ETC2 = 96,
        ASTC = 128
    }

    public enum ASTC_BlockSize
    {
        _4x4 = 1,
        _5x5,
        _6x6,
        _8x8,
        _10x10,
        _12x12
    }

    public enum TextureLimit
    {
        Unlimited,
        _128 = 128,
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096,
    }

    public class Config
    {
        internal static ModConfiguration _config;

        public static ref ModConfiguration Fetch() => ref _config;

        public static T GetValue<T>(ModConfigurationKey<T> key)
        {
            return _config.GetValue(key);
        }

        public static bool IsAndroid()
        {
            string path = Assembly.GetExecutingAssembly().CodeBase;
            return path.Contains("ModData/com.Solirax.Neos");
        }

        public static TextureLimit DefaultSizeForPlatform()
        {
            if (IsAndroid()) return TextureLimit._512;
            else return TextureLimit.Unlimited;
        }

        public static string GetMasterLimitDesc()
        {
            string description = "Master Texture Limit";

            TextureLimit plat_limit = DefaultSizeForPlatform();
            if (plat_limit != TextureLimit.Unlimited)
            {
                description += string.Format(" (Default: {0})", (int)plat_limit);
            }
            return description;
        }


        internal static AndroidTextureFormat GetDefaultFormat()
        {
            var engine = Engine.Current;
            if (engine == null) return AndroidTextureFormat.ETC2;

            if (engine.SystemInfo.SupportsTextureFormat(TextureFormat.ASTC_8x8)) return AndroidTextureFormat.ASTC;
            return AndroidTextureFormat.ETC2;
        }

        public static TextureFormat GetNativeTextureFormat()
        {
            AndroidTextureFormat format = _config.GetValue(NeosTextureSettings.TARGET_FORMAT);
            if (format == AndroidTextureFormat.ASTC)
            {
                int astc_modifier = (int)format + (int)_config.GetValue(NeosTextureSettings.ASTC_BLOCK_SIZE);
                return (TextureFormat)astc_modifier;
            }
            return (TextureFormat)format;
        }
    }
}
