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

        public UiManager(string menuName, Sprite menuSprite, bool createTargetMenu = true, bool createMainMenu = true)
        {
            MainMenu = new ReMenuPage(menuName, true);
            ReTabButton.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);

            if (createTargetMenu)
            {
                var localMenu = new ReCategoryPage(MenuEx.SelectedUserLocal.transform);
                TargetMenu = localMenu.AddCategory($"{menuName}");
            }
            if (createMainMenu)
            {
                var localMenu = new ReCategoryPage(MenuEx.DashboardMenu.transform);
                LaunchPad = localMenu.AddCategory($"{menuName}");
            }
        }
    }


}
