using System;
using MLConsoleViewer;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using Object = UnityEngine.Object;

namespace ReModAPI.QuickMenu
{
    public class ReIconButton
    {
        public event Action OnOpen;
        public event Action OnClose;
        public UIPage UiPage { get; }
      

        public ReIconButton(ReMenuPage menu, Sprite icon)
        {
            var toinst = Main.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;         
            inst.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            var btn = inst.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            
            btn.onClick.AddListener((UnityEngine.Events.UnityAction)menu.Open);
            var listener = inst.AddComponent<EnableDisableListener>();
            listener.OnEnableEvent += () => OnOpen?.Invoke();
            listener.OnDisableEvent += () => OnClose?.Invoke();

        }

        public void Open()
        {
            MenuEx.MenuStateCtrl.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(UiPage.field_Public_String_0);
            OnOpen?.Invoke();
        }

    }
}