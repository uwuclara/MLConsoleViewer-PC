using ReModAPI;
using ReModAPI.QuickMenu;
using UnityEngine;

namespace ReMod.Core.Managers
{
    public class UiManager
    {
        public IButtonPage MainMenu { get; }
        public IButtonPage TargetMenu { get; }
        public IButtonPage LaunchPad { get; }

        public UiManager(string menuName, Sprite menuSprite)
        {
            MainMenu = new ReMenuPage(menuName, true);
            ReTabButton.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);
        }
    }
}
