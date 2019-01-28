using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarShipButton : MonoBehaviour
{
    public Button Button;
    public Hangar Hangar;

    public Ship ShipOnButton;

    private void Start()
    {
        Button.onClick.AddListener(() => SelectShip());
    }

    private void SelectShip()
    {
        Debug.Log("Selected ship: " + ShipOnButton.Name);
        Hangar.SelectShip(ShipOnButton, Button);
    }

    public void Click()
    {
        SelectShip();
    }
}