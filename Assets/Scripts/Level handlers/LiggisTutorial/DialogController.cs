using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public DialogBox DialogBoxLeft;
    public DialogBox DialogBoxRight;

    public DialogCharacter[] DialogCharacters;
    public ConversationNode StartNode;

    public ScreenClick ScreenClick;

    public GameObject ClickAnywhereInfo;

    [SerializeField]
    private ConversationNode _actualNode;
    private DialogBox _activeBox;

    private static bool _clickedAnywhere = false;

    public void InitDialog(ConversationNode node)
    {
        if (_activeBox != null && _actualNode != null && _actualNode.IsAnwerNode())
        {
            _activeBox.Deactivate();
        }
        _actualNode = node;
        if (node.CharacterId == 0)
        {
            DialogBoxLeft.Init(node);
        }
        else if(node.CharacterId == 1)
        {
            DialogBoxRight.Init(node);
        }
    }

    public void SetActiveBox(DialogBox box)
    {
        _activeBox = box;
    }

    public void NextNode()
    {
        Debug.Log("Next node");
        if(_actualNode != null)
        {
            InitDialog(_actualNode.NextNode);
        }
    }

    public void StartConversation()
    {
        InitDialog(StartNode);
    }

    public void ShowClickAnywhereInfo()
    {
        if (!_clickedAnywhere && ClickAnywhereInfo != null && ClickAnywhereInfo.activeSelf)
        {
            ClickAnywhereInfo.SetActive(true);
        }
    }

    public void HideClickAnywhereInfo()
    {
        if (!_clickedAnywhere && ClickAnywhereInfo != null && ClickAnywhereInfo.activeSelf)
        {
            ClickAnywhereInfo.SetActive(false);
            _clickedAnywhere = true;
        }
    }

    public DialogCharacter GetCharacterWithId(int id)
    {
        for (int i = 0; i < DialogCharacters.Length; i++)
        {
            if(DialogCharacters[i].Id == id)
            {
                return DialogCharacters[i];
            }
        }
        return null;
    }

    public bool IsNodeActive(ConversationNode node)
    {
        return _actualNode == node;
    }
}
