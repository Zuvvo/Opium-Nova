using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationAnswer : MonoBehaviour
{
    public int Id;
    public ConversationNode NextNode;
    public List<ConversationAnswer> Answers;
}
