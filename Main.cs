using System;
using System.Reflection;
using MelonLoader;
using UnityEngine;
using BuildShit = MLConsoleViewer.BuildShit;
using Main = MLConsoleViewer.Main;
using System.Collections;
using ReModAPI;
using UnhollowerRuntimeLib;

#region Info & Namespace
[assembly: AssemblyDescription(BuildShit.Description)]
[assembly: AssemblyCopyright($"Created by {BuildShit.Author}, Copyright © 2022")]
[assembly: MelonInfo(typeof(Main), BuildShit.Name, BuildShit.Version, BuildShit.Author, BuildShit.DownloadLink)]
[assembly: MelonGame("VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]

namespace MLConsoleViewer;

public static class BuildShit
{
    public const string Name = "MLConsoleViewer";
    public const string Author = "Neeko & Lunar (Asset) - PC Port. Penny & Davi - Original. OG - Benacle.";
    public const string Version = "2.0.0";
    public const string DownloadLink = "https://github.com/PennyBunny/VRCMods/";
    public const string Description = "A standalone mod that adds a tab to your quick menu that has a simple copy of your console!";
}
#endregion

public class Main : MelonMod
{
    public static readonly string platform = "pc"; //quest or pc
    private static readonly MelonLogger.Instance Log = new(BuildShit.Name, ConsoleColor.DarkYellow);
    private static MelonPreferences_Category _mlConsoleViewer;
    public static MelonPreferences_Entry<int> _fontSize;
    public static MelonPreferences_Entry<int> MaxLines;
    public static MelonPreferences_Entry<int> MaxChars;
    public static MelonPreferences_Entry<bool> TimeStamp;
    public static GameObject UserInterface;

    public override void OnApplicationStart()
    {
        ConsoleManager.AttachTrackers();
        _mlConsoleViewer = MelonPreferences.CreateCategory("MLConsoleViewer", "MLConsoleViewer");
        _fontSize = _mlConsoleViewer.CreateEntry("fontSize", 19, "Font Size",
            "Font size of the text in your console tab");
        MaxLines = _mlConsoleViewer.CreateEntry("maxLines", 150, "Max Displayed Lines",
            "Defines the limit in which your console starts discarding old lines");
        MaxChars = _mlConsoleViewer.CreateEntry("maxChars", 1000, "Max Characters per Log",
            "Defines the limit of characters per log, printing only part of it if length is greater (will break if way too high because TextMesh limits)");
        TimeStamp = _mlConsoleViewer.CreateEntry("timeStamp", true, "Time Stamp",
            "Sets whether logs show time stamps or not");
        Log.Msg("MLConsoleViewer Loaded");
        
        startWaitForUI();
    }

    public override void OnPreferencesSaved()
    {
        if (UI.Text == null)
            return;
        UI.Text.fontSize = _fontSize.Value;
    }
    private void startWaitForUI()
    {
        ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
        MelonCoroutines.Start(WaitForUI());
    }
        
    IEnumerator WaitForUI()
    {
        while (ReferenceEquals(VRCUiManager.field_Private_Static_VRCUiManager_0, null)) yield return null; // wait till  isnt null
        foreach (var GameObjects in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (GameObjects.name.Contains("UserInterface"))
            {
                UserInterface = GameObjects;
            }
        }
        while (ReferenceEquals(MenuEx.Instance, null)) yield return null;
        
        BundleManager.Init(); // NO idea why values get reset onAppStart
        UI.BuildTab();
    }
}