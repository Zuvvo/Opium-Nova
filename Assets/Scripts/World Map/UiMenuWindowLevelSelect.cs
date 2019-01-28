using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMenuWindowLevelSelect : UiMenuWindowBase
{
    public UiMoreInfo UiMoreInfo;
    public Button MapButtonPrefab;
    public Transform MapButtonsHolder;
    public Animator Animator;
    public UiMenuWindowMapSelect WorldSelect;

    public GameObject[] Panels;

    public Text MissionName;
    public Text MissionDescription;
    public Image EnemyImage;
    public Image RewardImage;
    public Text EnemyNameText;
    public Text RewardNameText;
    public Text EnemyCountText;
    public Text RewardCountText;

    public List<EnemyData> EnemiesOnLevel;

    private EnemyData _currentEnemyInfo;
    private int _currentEnemyIndex = 0;
    
    private int _currentResourceIndex = 0;

    private Map _currentMap;
    private LevelData _selectedLevel;
    private List<Button> _levelButtons = new List<Button>();

    public void LoadSelectedLevel()
    {
        PanelsSetActive(true);
        LoadLevelData(_selectedLevel);
    }

    public void LoadMap(Map map)
    {
        PanelsSetActive(false);
        gameObject.SetActive(true);
        _currentMap = map;
        InitLevelButtons();
      //  LoadLevelInfo(map.Missions[0].LevelInfo);
    }

    public void EnemyMoreInfoButton()
    {
        // PanlesSetActive(false);
        StaticTagFinder.UiMapNavigation.Animator.SetTrigger("Hidding");
        UiMoreInfo.Init(EnemiesOnLevel[_currentEnemyIndex]);
    }

    public void PrepareForBattleButton()
    {
        CloseWindow();
        StaticTagFinder.UiMenuWindowManager.GearSelect.OpenWindow();
    }

    public void LoadMoreInfoPanel()
    {
        PanelsSetActive(false);
    }

    public void LoadLevelData(LevelData levelInfo)
    {
        _selectedLevel = levelInfo;
        StaticTagFinder.Player.PlayerSelects.SelectLevel(levelInfo.Id);
        Animator.SetTrigger("MapNavigation");
        _currentEnemyIndex = 0;
        _currentResourceIndex = 0;
        MissionName.text = levelInfo.MissionName;
        MissionDescription.text = levelInfo.MissionDescription;
        EnemiesOnLevel = levelInfo.Enemies;
        LoadEnemy(EnemiesOnLevel[_currentEnemyIndex]);
        Debug.Log("load level");
        Animator.enabled = false;
        Animator.enabled = true;
        // załadowanie resource
    }

    public void LoadEnemyAnimation()
    {
        LoadEnemy(EnemiesOnLevel[_currentEnemyIndex]);
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
    }

    public void PreviousEnemyButton()
    {
        _currentEnemyIndex--;
        if (_currentEnemyIndex < 0)
        {
            _currentEnemyIndex = EnemiesOnLevel.Count - 1;
        }
        //  LoadEnemy(EnemiesOnLevel[_currentEnemyIndex]);
    }

    public void NextEnemyButton()
    {
        _currentEnemyIndex++;
        if(_currentEnemyIndex == EnemiesOnLevel.Count)
        {
            _currentEnemyIndex = 0;
        }
       // LoadEnemy(EnemiesOnLevel[_currentEnemyIndex]);
    }

    public void PreviousRewardButton()
    {
    //    Debug.Log(ResourcesOnLevel.Count);
    }

    public void NextRewardButton()
    {
     //   Debug.Log(ResourcesOnLevel.Count);
    }

    public void EnemiesMoreInfoButton()
    {
        Debug.Log("EnemiesMoreInfoButton()");
    }
    public void RewardsMoreInfoButton()
    {
        Debug.Log("RewardsMoreInfoButton()");
    }

    private void LoadEnemy(EnemyData enemyInfo)
    {
        _currentEnemyInfo = enemyInfo;
        EnemyImage.sprite = enemyInfo.Sprite;
        EnemyNameText.text = enemyInfo.Name;
        EnemyCountText.text = string.Format("{0}/{1}", EnemiesOnLevel.IndexOf(enemyInfo) + 1, EnemiesOnLevel.Count);

    }

    private void InitLevelButtons()
    {
        for (int i = 0; i < _currentMap.Missions.Count; i++)
        {
            if (i >= _levelButtons.Count)
            {
                Button levelButton;
                levelButton = Instantiate(MapButtonPrefab, MapButtonsHolder);
                _levelButtons.Add(levelButton);
            }
        }
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            if(i < _currentMap.Missions.Count)
            {
                LevelData level = _currentMap.Missions[i].LevelData;
                _levelButtons[i].onClick.RemoveAllListeners();
                _levelButtons[i].onClick.AddListener(() => LoadLevelData(level));
                _levelButtons[i].interactable = !level.IsBlocked;
                Text text = _levelButtons[i].GetComponentInChildren<Text>(true);
                text.text = level.MissionName;
                _levelButtons[i].gameObject.SetActive(true);
            }
            else
            {
                _levelButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void PanelsSetActive(bool state)
    {
        Debug.Log("PanlesSetActive: " + state);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(state);
        }
    }

    public void ReActivateWindow()
    {
        gameObject.SetActive(true);
    }

    protected override void CloseThisWindow()
    {

    }

    protected override void OpenThisWindow()
    {

    }
}