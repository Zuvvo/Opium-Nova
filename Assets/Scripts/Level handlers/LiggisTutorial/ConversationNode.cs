using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationNode : MonoBehaviour
{
    public bool IsEndNode
    {
        get
        {
            if(NextNode != null || Answers != null || Answers.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
    public int Id;
    public int CharacterId;
    public ConversationNode NextNode;
    public List<ConversationAnswer> Answers;

    public bool IsAnwerNode()
    {
        if(Answers != null)
        {
            return Answers.Count > 0;
        }
        return false;
    }
}