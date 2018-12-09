using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using System;

[Serializable]
public struct Bullet: IComponentData
{
    public float Speed;
}


public class BulletComponent : ComponentDataWrapper<Bullet> { }