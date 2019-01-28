using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuWindowManager : MonoBehaviour
{
    public UiMenuWindowMapSelect MapSelect;
    public UiMenuWindowLevelSelect LevelSelect;
    public UiMenuWindowGearSelect GearSelect;




    public List<UiMenuWindowBase> OpenWindows = new List<UiMenuWindowBase>();

    public void RegisterWindow(UiMenuWindowBase window)
    {
        OpenWindows.Add(window);
    }

    public void UnregisterWindow(UiMenuWindowBase window)
    {
        OpenWindows.Remove(window);
    }

    public void CloseAllWindows()
    {
        for (int i = 0; i < OpenWindows.Count; i++)
        {
            OpenWindows[i].CloseWindow();
        }
    }
}
