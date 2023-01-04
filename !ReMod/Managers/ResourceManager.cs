using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MelonLoader;
using UnityEngine;
using static CRXCResources.CResources;

namespace CRXCResources
{
    public static class CResources
    {
        public static readonly string resourcePath = Path.Combine(Environment.CurrentDirectory, "UserData/path/");
        private static readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        internal static Sprite dummy;

       

        internal static void InitResources()
        {

            MelonLogger.Msg("Initializing resources...");

            if (!Directory.Exists(resourcePath))
                Directory.CreateDirectory(resourcePath);

            if (!Directory.Exists(resourcePath + "/Resources"))

                Directory.CreateDirectory(resourcePath + "/Resources");
            if (!Directory.Exists(resourcePath))
                Directory.CreateDirectory(resourcePath);       

            download();
            LoadResources();

        }

        private static void LoadResources()
        {
            dummy = LoadSprite("path.file");
        }

        public static Sprite LoadSpriteFromDisk(this string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            byte[] array = File.ReadAllBytes(path);
            if (array == null || array.Length == 0)
            {
                return null;
            }
            Texture2D texture2D = new Texture2D(512, 512);
            if (!ImageConversion.LoadImage(texture2D, array)) return null;
            Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0, 0), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
            sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return sprite;
        }

        private static Sprite LoadSprite(string sprite)
        {
            return $"{resourcePath}/Resources/{sprite}".LoadSpriteFromDisk();
        }
        private static void download()
        {
            string dummy64 = "";

            if (!File.Exists(resourcePath + "/path.file")) File.WriteAllBytes(resourcePath + "/path.file", Convert.FromBase64String(dummy64));
     
        }



    }
}
