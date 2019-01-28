using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiMenuWindowBase : MonoBehaviour
{
    private bool _isOpen;

    public bool IsWindowOpen()
    {
        return _isOpen;
    }

    public void CloseWindow()
    {
        _isOpen = false;
        gameObject.SetActive(false);
        StaticTagFinder.UiMenuWindowManager.UnregisterWindow(this);
        CloseThisWindow();
    }

    public void OpenWindow()
    {
        _isOpen = true;
        gameObject.SetActive(true);
        StaticTagFinder.UiMenuWindowManager.RegisterWindow(this);
        OpenThisWindow();
    }

    protected abstract void CloseThisWindow();
    protected abstract void OpenThisWindow();

}