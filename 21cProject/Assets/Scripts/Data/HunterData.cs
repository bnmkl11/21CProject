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
    /// ��Ʈ�� �ε���.
    /// </summary>
    public int INDEX;

    /// <summary>
    /// �ѱ���.
    /// </summary>
    public kGRADE GRADE;

    /// <summary>
    /// ġ����, ���ݰ�.
    /// </summary>
    public int POWER;

    /// <summary>
    /// ����.
    /// </summary>
    public int GUILD;


}