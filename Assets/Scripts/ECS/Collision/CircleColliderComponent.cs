using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct CircleCollider : IComponentData
{
    public float Radius;
}

public class CircleColliderComponent : ComponentDataWrapper<CircleCollider>
{
}