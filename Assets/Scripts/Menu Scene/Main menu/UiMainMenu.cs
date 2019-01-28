using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    public Canvas Canvas;
    public GameObject GamSizingHax;

    public GameObject HangarPanel;
    public GameObject WorldMapPanel;
    public GameObject OptionsPanel;

    public Hangar Hangar;
    
    public Background Background;
    
    public List<GameObject> ShipPreviews = new List<GameObject>();

    public Text LevelText;
    public Text SpeedText;
    public Text DifficultyText;
    public Text HardnessText;
    public Text ShipNameText;

    private GameManager _GM;

    [HideInInspector]
    public List<UiMenuButton> UiMainMenuButtons;

    public void StartGame()
    {
        if (StaticTagFinder.Player.IsAnyLevelUnlocked())
        {
            StaticTagFinder.UiMenuWindowManager.MapSelect.OpenWindow();
        }
        else
        {
            Debug.Log("init tutorial");
        }
    }

    public void ExitGame()
    {
       // Canvas.scaleFactor = Canvas.scaleFactor * 1.1f;
       // GamSizingHax.transform.position += new Vector3(200, 200, 0);
         Application.Quit();
    }

    public void OpenHangar()
    {
        OptionsPanel.SetActive(false);
        WorldMapPanel.SetActive(false);
        Hangar.Init();
    }

    public void OpenOptions()
    {
        OptionsPanel.SetActive(true);
        WorldMapPanel.SetActive(false);
    }

    public void OpenWorldMap()
    {
        OptionsPanel.SetActive(false);
        WorldMapPanel.SetActive(true);
    }

    public void OpenAchivments()
    {
        Debug.LogError("not implemented yet");
    }

    public void SelectShip()
    {
        
    }

    public void Test()
    {
        float time = Time.realtimeSinceStartup;
        StaticTagFinder.GetT<UiManager>().ShowDebug();
        Debug.Log(Time.realtimeSinceStartup - time);
    }

    public void Test2()
    {
        float time = Time.realtimeSinceStartup;
        StaticTagFinder.UiManager.ShowDebug();
        Debug.Log(Time.realtimeSinceStartup - time);
    }
}