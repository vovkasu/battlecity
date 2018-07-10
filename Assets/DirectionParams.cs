using System;
using UnityEngine;

[Serializable]
public class DirectionParams
{
    public string DirectionName;
    public Axis Axis;
    public float AxisValue;
    public Sprite Sprite;
    public Vector2 MoveDirection;
    public Transform BulletPosition;
    public float BulletZRotation;
    public Vector2 BulletColliderSize;
}