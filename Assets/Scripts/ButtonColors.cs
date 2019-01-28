using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColors : MonoBehaviour {

    private static ButtonColors _instance;
    public static ButtonColors Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ButtonColors>();
            }
            return _instance;
        }
    }
    [Header("DEFAULT BUTTON COLORS")]
    public ColorBlock ButtonColorBlock;
    [Header("SELECTED BUTTON COLORS")]
    public ColorBlock ButtonSelectedColorBlock;

    [Header("SELECTED IN MAP GEAR SELECT")]
    public Sprite SelectedButtonSpriteGearSelect;
    [Header("NOT SELECTED IN MAP GEAR SELECT")]
    public Sprite NotSelectedButtonSpriteGearSelect;

    [Header("SELECT BUTTONS IN GEAR SELECT")]
    public Sprite SelectedBackgroundSprite;
    public Sprite SelectedSprite;
    public Sprite NotSelectedBackgroundSprite;
    public Sprite NotSelectedSprite;
}
