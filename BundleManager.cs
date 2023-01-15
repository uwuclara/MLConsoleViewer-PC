using System.IO;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;


namespace MLConsoleViewer;

internal static class BundleManager
{
    public static GameObject ConsolePrefab;
    public static Sprite icon;

    public static void Init()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MLConsoleViewer.console-" + Main.platform);
        using var memoryStream = new MemoryStream((int)stream!.Length);
        stream.CopyTo(memoryStream);
        
        var bundle = AssetBundle.LoadFromMemory_Internal(memoryStream.ToArray(), 0);
        ConsolePrefab = bundle.LoadAsset_Internal("assets/bundledassets/console/console.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>(); //HardCoding asset
        icon = bundle.LoadAsset_Internal("assets/bundledassets/console/console.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
        bundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        icon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
    }
}