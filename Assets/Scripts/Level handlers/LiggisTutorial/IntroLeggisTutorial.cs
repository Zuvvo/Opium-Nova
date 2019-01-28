using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class IntroLeggisTutorial : MonoBehaviour
{
    public TextMeshProUGUI PlanetInfoStartText;
    public GameObject LiggisBackground;

    public Image[] ImagesToFadeIn;
    public DialogController ConversationController;
    public ScreenClick ScreenClick;

    private bool _introStarted;

    private string _text1 = "Year 3082, 4th age";
    private string _text2 = "Liggis, Center of Galaxy";
    private string _text3 = "Marhium, Planetary Defense Base";

    private string _finalText = "";

    private WaitForSeconds _startWaitTime = new WaitForSeconds(1.5f);
    private WaitForSeconds _enterWaitTime = new WaitForSeconds(1.2f);
    private WaitForSeconds _charWaitTime = new WaitForSeconds(.03f);
    private WaitForSeconds _fadeOutTickTime = new WaitForSeconds(.05f);
    private WaitForSeconds _afterStartTextTime = new WaitForSeconds(1.5f);
    private float _fadeInTime = 2f;
    private Coroutine _startTextCoroutine;

    public void Init()
    {
        MoveController.IsPlayerBlocked = true;
        _introStarted = true;
        _startTextCoroutine = StartCoroutine(ShowIntroStartText());
    }

    private IEnumerator ShowIntroStartText()
    {
        yield return _startWaitTime;
        for (int i = 0; i < _text1.Length; i++)
        {
            _finalText += _text1[i];
            PlanetInfoStartText.text = _finalText;
            yield return _charWaitTime;
        }

        yield return _enterWaitTime;
        _finalText += "\n";
        PlanetInfoStartText.text = _finalText;

        for (int i = 0; i < _text2.Length; i++)
        {
            _finalText += _text2[i];
            PlanetInfoStartText.text = _finalText;
            yield return _charWaitTime;
        }

        yield return _enterWaitTime;
        _finalText += "\n";
        PlanetInfoStartText.text = _finalText;

        for (int i = 0; i < _text3.Length; i++)
        {
            _finalText += _text3[i];
            PlanetInfoStartText.text = _finalText;
            yield return _charWaitTime;
        }
        yield return _afterStartTextTime;
        PlanetInfoStartText.CrossFadeAlpha(0, _fadeInTime, true);
        ImagesToFadeIn[0].CrossFadeAlpha(0, _fadeInTime, true);
        yield return new WaitForSeconds(_fadeInTime);
        LiggisBackground.SetActive(false);
        ConversationController.StartConversation();
      //  StaticTagFinder.CoroutineHeleper.StartFadeIn(ImagesToFadeIn, ()=> StartDialogScene());
        StopCoroutine(_startTextCoroutine);
        _startTextCoroutine = null;
    }

    private void StartDialogScene()
    {
        LiggisBackground.SetActive(false);
        Debug.Log("START DIALOG");
    }
}