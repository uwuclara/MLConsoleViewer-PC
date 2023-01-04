using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MLConsoleViewer;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace ReModAPI
{
    public static class MenuEx
    {
        public static IEnumerable<Type> TryGetTypes(this Assembly asm)
        {
            IEnumerable<Type> enumerable;
            try
            {
                enumerable = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                try
                {
                    enumerable = asm.GetExportedTypes();
                }
                catch
                {
                    enumerable = ex.Types.Where((Type t) => t != null);
                }
            }
            catch
            {
                enumerable = Enumerable.Empty<Type>();
            }
            return enumerable;
        }

        private static VRC.UI.Elements.QuickMenu _quickMenuInstance;

        public static VRC.UI.Elements.QuickMenu Instance
        {
            get
            {
                if (_quickMenuInstance == null)
                {
                    _quickMenuInstance = Main.UserInterface.GetComponentInChildren<VRC.UI.Elements.QuickMenu>(true);
                }
                return _quickMenuInstance;
            }
        }


        private static Transform _menuParent;

        public static Transform MenuParent
        {
            get
            {
                if (_menuParent == null)
                {
                    _menuParent = Instance.transform.Find("CanvasGroup/Container/Window/QMParent");
                }
                return _menuParent;
            }
        }

     
        private static Transform _menuTabs;

        public static Transform MenuTabs
        {
            get
            {
                if (_menuTabs == null)
                {
                    _menuTabs = Instance.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
                }
                return _menuTabs;
            }
        }

        private static MenuStateController _menuStateCtrl;

        public static MenuStateController MenuStateCtrl
        {
            get
            {
                if (_menuStateCtrl == null)
                {
                    _menuStateCtrl = Instance.transform.GetComponent<MenuStateController>();
                }

                return _menuStateCtrl;
            }
        }

        private static SelectedUserMenuQM _selectedUserLocal;

        public static SelectedUserMenuQM SelectedUserLocal
        {
            get
            {
                if (_selectedUserLocal == null)
                {
                    _selectedUserLocal = Instance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
                }

                return _selectedUserLocal;
            }
        }

        private static Transform _dashboardMenu;
        public static Transform DashboardMenu
        {
            get
            {
                if (_dashboardMenu == null)
                {
                    _dashboardMenu = MenuParent.Find("Menu_Dashboard");
                }
                return _dashboardMenu;
            }
        }
    

        private static Transform _notificationMenu;
        public static Transform NotificationMenu
        {
            get
            {
                if (_notificationMenu == null)
                {
                    _notificationMenu = MenuParent.Find("Menu_Notifications");
                }
                return _notificationMenu;
            }
        }
        private static Transform _hereMenu;
        public static Transform HereMenu
        {
            get
            {
                if (_hereMenu == null)
                {
                    _hereMenu = MenuParent.Find("Menu_Here");
                }
                return _hereMenu;
            }
        }
        private static Transform _cameraMenu;
        public static Transform CameraMenu
        {
            get
            {
                if (_cameraMenu == null)
                {
                    _cameraMenu = MenuParent.Find("Menu_Camera");
                }
                return _cameraMenu;
            }
        }
        private static Transform _audiosettingsMenu;
        public static Transform AudioSettingsMenu
        {
            get
            {
                if (_audiosettingsMenu == null)
                {
                    _audiosettingsMenu = MenuParent.Find("Menu_AudioSettings");
                }
                return _audiosettingsMenu;
            }
        }
        private static Transform _settingsMenu;
        public static Transform SettingsMenu
        {
            get
            {
                if (_settingsMenu == null)
                {
                    _settingsMenu = MenuParent.Find("Menu_Settings");
                }
                return _settingsMenu;
            }
        }
        private static Transform _devtoolsMenu;
        public static Transform DevToolsMenu
        {
            get
            {
                if (_devtoolsMenu == null)
                {
                    _devtoolsMenu = MenuParent.Find("Menu_DevTools");
                }
                return _devtoolsMenu;
            }
        }

        private static GameObject _leftWing;
        private static GameObject _rightWing;

        public static GameObject LeftWing
        {
            get
            {
                if (_leftWing == null)
                {
                    _leftWing = Instance.transform.Find("CanvasGroup/Container/Window/Wing_Left").gameObject;
                }
                return _leftWing;
            }
        }

        public static GameObject RightWing
        {
            get
            {
                if (_rightWing == null)
                {
                    _rightWing = Instance.transform.Find("CanvasGroup/Container/Window/Wing_Right").gameObject;
                }
                return _rightWing;
            }
        }
        public static Transform WingMenuContent(this GameObject wing) =>
            wing.transform.Find("Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");


        private static Sprite _onIconSprite;
        public static Sprite OnIconSprite
        {
            get
            {
                if (_onIconSprite == null)
                {
                    _onIconSprite = Instance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<Image>().sprite;
                }
                return _onIconSprite;
            }
        }

        private static Sprite _offIconSprite;
        public static Sprite OffIconSprite
        {
            get
            {
                if (_offIconSprite == null)
                {
                    _offIconSprite = TogglePrefab.transform.Find("Icon_Off").GetComponent<Image>().sprite;
                }
                return _offIconSprite;
            }
        }

        private static GameObject _togglePrefab;
        public static GameObject TogglePrefab
        {
            get
            {
                if (_togglePrefab == null)
                {
                    _togglePrefab = Instance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect").GetComponent<ScrollRect>().content
                        .Find("Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject;
                }
                return _togglePrefab;
            }
        }

        private static GameObject _sliderPrefab;
        public static GameObject SliderPrefab
        {
            get
            {
                if (_sliderPrefab == null)
                {
                    _sliderPrefab = Instance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_AudioSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Audio/VolumeSlider_Master").gameObject;
                }
                return _sliderPrefab;
            }
        }
    }

   
}
