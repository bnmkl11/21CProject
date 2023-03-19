using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 카메라에 닿으면 투명해지는 오브젝트용
/// </summary>
public interface ISlotIcon
{
    // 투명 상태인가? 투명 상태인데 또 투명 만들 필요가 없어서 bool 변수 둠
    bool IsFade { get; set; }
    void EnterDrag();
    void ExitCollisionToCameraRay();
}

/// <summary>
/// 카메라에 닿으면 투명해지는 오브젝트용
/// </summary>
public interface ICameraObstacle
{
    // 투명 상태인가? 투명 상태인데 또 투명 만들 필요가 없어서 bool 변수 둠
    bool IsFade { get; set; }
    void OnEnterCollisionToCameraRay();
    void OnExitCollisionToCameraRay();
}

public interface IPoolingObject
{
    bool IsInPool { get; set; }
    int StartCount { get; set; }
    void Initialize();
    void DisposeObject();
    void UpdateObject();
}
