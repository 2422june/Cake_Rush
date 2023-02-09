using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    GameObject _root;
    public GameObject Root
    {
        get
        {
            if(_root == null)
                _root = new GameObject { name = "@UI_Root" };
            
            return _root;
        }
    }
    public UI_Scene SceneUI { get; set; }
    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    int order = 10;

    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if(sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T popup = go.GetOrAddComponent<T>();
        popupStack.Push(popup);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T sceneUI = go.GetOrAddComponent<T>();
        SceneUI = sceneUI;
        return sceneUI;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;

        if(popupStack.Peek() == popup)
            ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Managers.Resource.Destory(SceneUI.gameObject);
        SceneUI = null;
    }
}
