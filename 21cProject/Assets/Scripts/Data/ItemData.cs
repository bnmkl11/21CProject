using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class ItemData 
{
    public enum kTYPE
    {
        None = 0,
        Top = 1,
        Pants,
        Accessory
    }

    public enum kGRADE
    {
        None = 0,
        F = 1,
        D,
        C
    }


    public int INDEX;

    public kTYPE TYPE;

    public kGRADE GRADE;
}
