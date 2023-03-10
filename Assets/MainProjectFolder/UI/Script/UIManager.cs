using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private UI_View _startingView;

    [SerializeField] private UI_View[] _view;

    private UI_View _currentView;

    private readonly Stack<UI_View> _viewStack = new Stack<UI_View>();

    public static T GetView<T>() where T : UI_View
    {
        for(int i = 0; i < Instance._view.Length; i++)
        {
            if (Instance._view[i] is T tTiew)
            {
                return tTiew;
            }
        }
        return null;
    }

    public static void Show<T>(bool remember = true) where T : UI_View
    {
        for (int i = 0; i < Instance._view.Length; i++) 
        {
            if (Instance._view[i] is T)
            {
                if(Instance._currentView != null)
                {
                    if (remember)
                    {
                        Instance._viewStack.Push(Instance._currentView);
                    }

                    Instance._currentView.Hide();
                }

                Instance._view[i].Show();

                Instance._currentView = Instance._view[i];
            }
        }
    }

    public static void Show(UI_View v, bool remember = true)
    {
        if(Instance._currentView != null)
        {
            if (remember)
            {
                Instance._viewStack.Push(Instance._currentView);
            }

            Instance._currentView.Hide();
        }

        v.Show();

        Instance._currentView = v;
    }

    public static void ShowLast()
    {
        if (Instance._viewStack.Count != 0) 
        {
            Show(Instance._viewStack.Pop(), false);
        }
    }
}
