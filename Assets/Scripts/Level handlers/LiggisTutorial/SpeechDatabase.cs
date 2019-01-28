using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeechDatabase", menuName = "DB/SpeechDB")]
public class SpeechDatabase : ScriptableObject
{
    public List<SpeechData> Speeches;

    public SpeechData GetSpeech(int id)
    {
        for (int i = 0; i < Speeches.Count; i++)
        {
            if(Speeches[i].Id == id)
            {
                SpeechData speech = new SpeechData()
                {
                    Id = Speeches[i].Id,
                    Text = Speeches[i].Text
                };
                return speech;
            }
        }
        Debug.LogWarning("Cant find speech with id " + id);
        return null;
    }

    public string GetSpeechText(int id)
    {
        for (int i = 0; i < Speeches.Count; i++)
        {
            if(Speeches[i].Id == id)
            {
                return Speeches[i].Text;
            }
        }
        Debug.LogWarning("Cant find speech with id " + id);
        return null;
    }

}