using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct EnemyMovement : IComponentData
{
    public float2 Speed;
}

public class EnemyMovementComponent : ComponentDataWrapper<EnemyMovement>
{
}
