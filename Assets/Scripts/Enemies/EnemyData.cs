using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData  : MonoBehaviour
{
    public string Name;
    public int Id;
    public int Hp;
    public Sprite Sprite;
    public int Dps;
    public int Experience;

    public string Statistics
    {
        get
        {
            return string.Format("Hp: {0} \nDps: {1} \nExp: {2}", Hp, Dps, Experience);
        }
    }
}