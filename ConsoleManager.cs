using System;
using System.Collections;
using System.Collections.Generic;
using MelonLoader;

namespace MLConsoleViewer;

public static class ConsoleManager
{
    public static readonly HashSet<string> Cached = new();
    public static void AttachTrackers()
    {
        MelonLogger.MsgCallbackHandler += OnLog;
        MelonLogger.WarningCallbackHandler += (callingMod, logText) => OnLog(true, callingMod, logText);
        MelonLogger.ErrorCallbackHandler += (callingMod, logText) => OnLog(false, callingMod, logText);
    }
    private static void OnLog(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string content) =>
        PrintOrCacheStr((Main.TimeStamp.Value ? $"[<color=green>{CurrTime}</color>] " : "") + // Adds time stamp if MelonPref == true
                        (string.IsNullOrEmpty(callingMod) ? "" : $"[<color={HexStrings[melonColor]}>{callingMod}</color>] ") + // Adds colored calling mod tag if not empty/null
                        $"<color={HexStrings[txtColor]}>{LimitContentLength(content)}</color>\n"); // Adds colored text
    private static void OnLog(bool isWarn, string callingMod, string content) =>
        PrintOrCacheStr($"<color={HexStrings[isWarn ? ConsoleColor.Yellow : ConsoleColor.Red]}>" + // Adds color
                        (Main.TimeStamp.Value ? $"[{CurrTime}] " : "") + // Adds time stamp if MelonPref == true
                        (string.IsNullOrEmpty(callingMod) ? "" : $"[{callingMod}] ") + // Adds calling mod tag if not empty/null
                        $"{LimitContentLength(content)}</color>\n"); // Adds text and finishes color
    private static void PrintOrCacheStr(string result)
    {
        if (!UI.Text)
            Cached.Add(result);
        else
        {
            UI.AppendText(result);
        }
    }
    private static string LimitContentLength(string content) =>
        content.Length > Main.MaxChars.Value ? content.Substring(0, Main.MaxChars.Value - 6) + " (...)" : content;
    private static string CurrTime => DateTime.Now.AddMilliseconds(-1.0).ToString("HH:mm:ss.fff");
    private static readonly Hashtable HexStrings = new()
    {
        {ConsoleColor.Black, "#000000"},
        {ConsoleColor.DarkBlue, "#00008b"},
        {ConsoleColor.DarkGreen, "#008b00"},
        {ConsoleColor.DarkCyan, "#008b8b"},
        {ConsoleColor.DarkRed, "#8b0000"},
        {ConsoleColor.DarkMagenta, "#8b008b"},
        {ConsoleColor.DarkYellow, "#8b8b00"},
        {ConsoleColor.Gray, "#c0c0c0"},
        {ConsoleColor.DarkGray, "#8b8b8b"},
        {ConsoleColor.Blue, "#0000ff"},
        {ConsoleColor.Green, "#00ff00"},
        {ConsoleColor.Cyan, "#00ffff"},
        {ConsoleColor.Red, "#ff0000"},
        {ConsoleColor.Magenta, "#ff00ff"},
        {ConsoleColor.Yellow, "#ffff00"},
        {ConsoleColor.White, "#ffffff"}
    };
}