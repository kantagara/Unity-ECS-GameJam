using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Target : IComponentData
{
    public float3 Position;
}

public class TargetComponent : ComponentDataWrapper<Target>
{}
