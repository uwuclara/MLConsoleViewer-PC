using System.Linq;
using MelonLoader;
using ReMod.Core.Managers;
using ReModAPI;
using ReModAPI.QuickMenu;
using ReModAPI.Wings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using Object = UnityEngine.Object;

namespace MLConsoleViewer;

public static class UI
{
    private static ReCategoryPage _consoleTab;
    private static GameObject _mlMenu, _consolePrefab;
    public static TextMeshProUGUI Text;
    private static ScrollRect _scrollRect;
    private static ReMirroredWingMenu _mlcvWingMenu;
    private static UiManager _uiManager;
    
    public static void BuildTab()
    {

        _uiManager = new UiManager("MLConsoleViewer", null);
        
        _uiManager.MainMenu.AddMenuPage("MLConsoleViewer", "MLConsoleViewer", null);
        var mainmenu = _uiManager.MainMenu.GetMenuPage("MLConsoleViewer");
        new ReIconButton(mainmenu, BundleManager.icon);
        
        var tabbies = MenuEx.MenuTabs;
        tabbies.Find("Page_MLConsoleViewer").gameObject.SetActive(false);

        
        
        _mlMenu = MenuEx.MenuParent.Find(UiElement.GetCleanName("Menu_MLConsoleViewer")).gameObject;
        _consolePrefab = Object.Instantiate(BundleManager.ConsolePrefab, _mlMenu.transform);
        _consolePrefab.transform.localPosition = new Vector3(0, -42, 0);
        
        Tools.SetLayerRecursively(_consolePrefab, LayerMask.NameToLayer("InternalUI"));
        
        _scrollRect = _consolePrefab.GetComponentInChildren<ScrollRect>(true);
        Text = _consolePrefab.transform.Find("Scroll View/Viewport/Content/")
                         .GetComponentInChildren<TextMeshProUGUI>(true);
        
        
        _mlcvWingMenu = ReMirroredWingMenu.Create("MLCV", "Open's up the MLConsoleViewer wing menu", null);
        _mlcvWingMenu.AddButton("Clear Logs", "Clears all the logs in MLCV", () =>
        {
            Text.text = "";
        }, null, false);
        _mlcvWingMenu.AddToggle("Time Stamps", "Toggles Time Stamps", b =>
        {
            Main.TimeStamp.Value = b;
        }, Main.TimeStamp.Value);
        
        _mlcvWingMenu.AddButton("Font Size ++(1)", "Font Size ++(1)", () =>
        {
            if (Main._fontSize.Value >= 30) return;
            Settings.updateConfig(0, 1);
        });
        _mlcvWingMenu.AddButton("Font Size --(1)", "Font Size --(1)", () =>
        {
            if (Main._fontSize.Value <= 1) return;
            Settings.updateConfig(0, -1);
        });
        
        _mlcvWingMenu.AddButton("Max Lines ++(10)", "Max Lines ++(10)", () =>
        {
            if (Main.MaxLines.Value >= 500) return;
            Settings.updateConfig(1, 10);
        });
        _mlcvWingMenu.AddButton("Max Lines --(10)", "Max Lines --(10)", () =>
        {
            if (Main.MaxLines.Value <= 10) return;
            Settings.updateConfig(1, -10);
        });
        
        _mlcvWingMenu.AddButton("Max Chars ++(100)", "Max Chars ++(100)", () =>
        {
            if (Main.MaxChars.Value >= 5000) return;
            Settings.updateConfig(2, 100);
        });
        
        _mlcvWingMenu.AddButton("Max Chars --(100)", "Max Chars --(100)", () =>
        {
            if (Main.MaxChars.Value <= 100) return;
            Settings.updateConfig(2, -100);
        });
        
        
        
        foreach (var i in ConsoleManager.Cached)
            AppendText(i);
        MelonPreferences.Save();
    }

    #region Text Appending
    private static int _lineNum;
    public static void AppendText(string txt)
    {
        if (_lineNum >= Main.MaxLines.Value)
            Text.text = GetReducedStr(Text.text, Main.MaxLines.Value);
        else
            _lineNum++;
        Text.text += txt;
    }
    private static string GetReducedStr(string content, int nthIndex)
    {
        var index = 0;
        nthIndex = content.Count(occ => occ == '\n') - nthIndex + 1;
        
        if (nthIndex < 0)
            return content;
        
        for (; nthIndex != 0; nthIndex--)
            index = content.IndexOf('\n', index) + 1;
        
        return content.Substring(index);
    }
    #endregion
}