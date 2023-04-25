using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolData
{
    public enum kTOOL_TYPE
    {
        None = -1,
        READING_GLASSESS,
    }
    
    public int INDEX;

    public string IMAGE_NAME;

    public kTOOL_TYPE TOOLTYPE;
}