using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMenuButton : MonoBehaviour
{
    public Button Button;
    public Text ButtonText;

    public void ButtonSetBlock(bool state)
    {
        Button.interactable = state;
        if (state)
        {
            Button.image.color = new Color(1, 1, 1, 1);
            ButtonText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            Button.image.color = new Color(1, 1, 1, 112 / 255);
            ButtonText.color = new Color(1, 1, 40/255, 60/255);
        }
    }

    private void RegisterButton()
    {
        List<UiMenuButton> uiButtons = StaticTagFinder.UiMainMenu.UiMainMenuButtons;
        if (!uiButtons.Contains(this))
        {
            uiButtons.Add(this);
        }
    }
}