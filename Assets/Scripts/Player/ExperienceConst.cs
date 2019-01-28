using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceConst
{
    public static float[] exp = new float[10]
    {
        0,
        100,
        400,
        1000,
        2000,
        5000,
        10000,
        20000,
        50000,
        100000
    };

    public static float ExpNeededForLevel(int level)
    {
        return exp[level - 1];
    }
}