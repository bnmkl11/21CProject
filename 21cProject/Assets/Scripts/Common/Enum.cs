using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum kLAYER
{
    Character = 8,
    Ground,
    Monster,
    Bullet,
    END
}

public enum kITEM
{
    Dia = 100,
    Gold = 101,
    Power = 102

}

public enum kBLOCK_TYPE
{
    None,
    End
}

public enum kSTAT_TYPE
{
    Attack,
    Health,
    HPRegen,
    Mana,
    ManaRegen,
    Defence,
    MagicDefence,
    Move,
    Morale,
}


public enum kUIEVENT
{
    None,
    BeginDarg,
    Drag,
    ExitDrag,
    Click,
    Down,
    Up,
    End
}


public enum kCAMERA_TYPE
{
    None,
    FixedToCharacter,
    end
}

public enum kSCENE_UI_TYPE
{
    None,
    Title,
    Lobby,
    Dead,
    Victory,
    Loading,
    Inventory,
}
public enum kSCNENE_TYPE
{
    None,
    Loading,
    Main,
    Lobby,
    Shop
}

#region Sound
public enum kSFX
{
    None,
    Click,
    END
}

public enum kBGM
{
    None,
    Title,
    Lobby,
    Stage,
    Shop,
    Ending,
    End
}
#endregion

