using MelonLoader;

namespace MLConsoleViewer;

public class Settings
{
    public static void updateConfig(int configIndex, int newValue)
    {
        switch (configIndex)
        {
            case 0:
                var newFontSize = newValue + Main._fontSize.Value;
                Main._fontSize.Value = newFontSize;
                MelonPreferences.Save();
                break;
            case 1:
                var newMaxLines = newValue + Main.MaxLines.Value;
                Main.MaxLines.Value = newMaxLines;
                MelonPreferences.Save();
                break;
            case 2: 
                var newMaxChars = newValue + Main.MaxChars.Value;
                Main.MaxChars.Value = newMaxChars;
                MelonPreferences.Save();
                break;
        }
    }
}