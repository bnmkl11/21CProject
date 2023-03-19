using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ī�޶� ������ ���������� ������Ʈ��
/// </summary>
public interface ISlotIcon
{
    // ���� �����ΰ�? ���� �����ε� �� ���� ���� �ʿ䰡 ��� bool ���� ��
    bool IsFade { get; set; }
    void EnterDrag();
    void ExitCollisionToCameraRay();
}

/// <summary>
/// ī�޶� ������ ���������� ������Ʈ��
/// </summary>
public interface ICameraObstacle
{
    // ���� �����ΰ�? ���� �����ε� �� ���� ���� �ʿ䰡 ��� bool ���� ��
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
