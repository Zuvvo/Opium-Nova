using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogBox : MonoBehaviour
{
    public DialogController DialogController;
    public Image Portrait;
    public Image[] FadingImages;
    public TextMeshProUGUI DialogText;
    public AnswerButton[] AnswerButtons;

    private float _fadeInTime = 0.5f;
    private WaitForSeconds _fadeInWaitTime = new WaitForSeconds(0.5f);
    private WaitForSeconds _charWaitTime = new WaitForSeconds(.02f);
    private string _conversationText;
    private string _actualText;
    private ConversationNode _actualNode;

    private Action _onScreenClick;

    private bool _loading;
    [SerializeField]
    private bool _isAnswerNode;
    
    private void OnDestroy()
    {
        if(_onScreenClick != null)
        {
            _onScreenClick -= ScreenClick;
        }
    }

    public void Activate()
    {
        _loading = true;
        _onScreenClick += ScreenClick;
        DialogController.ScreenClick.Action.Register(_onScreenClick);
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _onScreenClick -= ScreenClick;
        DialogController.ScreenClick.Action.Unregister(_onScreenClick);
        gameObject.SetActive(false);
    }

    private void ScreenClick()
    {
        if (_isAnswerNode || !DialogController.IsNodeActive(_actualNode))
        {
            return;
        }

        if(_loading)
        {
            Debug.Log("click. fill text. " + gameObject.name);
            _actualText = _conversationText;
            DialogText.text = _actualText;
        }
        else
        {
            Debug.Log("click. next node. " + gameObject.name);
            DialogController.NextNode();
        }
        DialogController.HideClickAnywhereInfo();
    }

    public void Init(ConversationNode node)
    {
        _actualNode = node;
        DialogCharacter character = DialogController.GetCharacterWithId(node.CharacterId);
        Portrait.sprite = character.Portrait;
        DialogController.SetActiveBox(this);

        if (node.IsEndNode)
        {
            EndDialogue();
        }

        if (node.Answers != null && node.Answers.Count > 0)
        {
            _isAnswerNode = true;
            InitAnswerButtons(node);
        }
        else
        {
            _isAnswerNode = false;
            InitSpeech(node);
        }
    }

    private void EndDialogue()
    {
        Debug.Log("END");
    }

    private IEnumerator ShowBox()
    {
        ButtonsSetActive(false, AnswerButtons.Length);
        HideText();
        for (int i = 0; i < FadingImages.Length; i++)
        {
            FadingImages[i].CrossFadeAlpha(1, _fadeInTime, true);
        }
        DialogText.CrossFadeAlpha(1, _fadeInTime, true);
        yield return _fadeInWaitTime;
        for (int i = 0; i < _conversationText.Length; i++)
        {
            if(_actualText != _conversationText)
            {
                _actualText += _conversationText[i];
                DialogText.text = _actualText;
                yield return _charWaitTime;
            }
        }
        DialogController.ScreenClick.Activate();
        DialogController.ShowClickAnywhereInfo();
        _loading = false;
    }

    private void InitAnswerButtons(ConversationNode node)
    {
        HideText();
        ButtonsSetActive(true, node.Answers.Count);
        for (int i = 0; i < node.Answers.Count; i++)
        {
            ConversationAnswer answer = node.Answers[i];
            AnswerButtons[i].Button.onClick.RemoveAllListeners();
            AnswerButtons[i].Text.text = StaticTagFinder.SpeechDB.GetSpeechText(answer.Id);
            AnswerButtons[i].Button.onClick.AddListener(() => DialogController.InitDialog(answer.NextNode));
        }
        DialogController.ScreenClick.Deactivate();
        Activate();
        _loading = true;
    }

    private void HideText()
    {
        _actualText = string.Empty;
        DialogText.text = _actualText;
    }

    private void ButtonsSetActive(bool state, int count)
    {
        if (state)
        {
            for (int i = 0; i < count; i++)
            {
                AnswerButtons[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < AnswerButtons.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void InitSpeech(ConversationNode node)
    {
        _conversationText = StaticTagFinder.SpeechDB.GetSpeechText(node.Id);
        Activate();
        DialogController.ScreenClick.Activate();
        StartCoroutine(ShowBox());
    }
}
