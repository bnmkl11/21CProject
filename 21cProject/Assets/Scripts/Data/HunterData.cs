using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HunterData
{
    public enum kGRADE
    { 
        None = 0,
        F = 1,
        D = 2

    }

    /// <summary>
    /// 스트링 인덱스.
    /// </summary>
    public int INDEX;

    /// <summary>
    /// 한국어.
    /// </summary>
    public kGRADE GRADE;

    /// <summary>
    /// 치유계, 공격계.
    /// </summary>
    public int POWER;

    /// <summary>
    /// 길드명.
    /// </summary>
    public int GUILD;


}