using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCommunique : MonoBehaviour
{

    public Text Text;
    public void SetCommunique(string info)
    {
        gameObject.SetActive(true);
        Text.text = info;
    }
}