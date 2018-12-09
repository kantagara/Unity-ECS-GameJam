using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct EnemyMovement : IComponentData
{
    public float Speed;
}

public class EnemyMovementComponent : ComponentDataWrapper<EnemyMovement>
{
}
