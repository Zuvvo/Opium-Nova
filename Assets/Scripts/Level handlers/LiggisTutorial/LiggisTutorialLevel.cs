using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiggisTutorialLevel : LevelHandler
{
    public IntroLeggisTutorial IntroTutorialPrefab;

    private IntroLeggisTutorial _introTutorial;

    private void Start()
    {
        InstantiateUiTutorialPrefab();
    }

    private void InstantiateUiTutorialPrefab()
    {
        _introTutorial = Instantiate(IntroTutorialPrefab, StaticTagFinder.GameUI.MainCanvas.transform);
        _introTutorial.Init();
    }





    public override void OnEnemyStateChanged(Enemy enemy, EnemyBehaviourState state)
    {

    }

    protected override void LevelInit()
    {

    }

    protected override void SpawnEnemies()
    {

    }

    protected override void StartWave()
    {

    }

}
