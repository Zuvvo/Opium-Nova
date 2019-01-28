using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public UiMenuButton MenuButtonPrefab;

    public Transform UiHolder;

    public UiMenuButton CrateUiMenuButton(UnityEngine.Events.UnityAction action)
    {
        UiMenuButton menuButton = Instantiate(MenuButtonPrefab, UiHolder);
        menuButton.Button.onClick.AddListener(() => action());
        return menuButton;
    }

    public void Test()
    {
        CrateUiMenuButton(() => Message());
    }

    public void ShowDebug()
    {
        Debug.Log(GetInstanceID());
    }

    private void Message()
    {
        Debug.Log("XX");
    }
}