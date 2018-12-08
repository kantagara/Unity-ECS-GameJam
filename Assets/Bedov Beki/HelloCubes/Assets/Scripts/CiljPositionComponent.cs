using System;
using Unity.Entities;
using Unity.Mathematics;

// Serializable attribute is for editor support.
[Serializable]
public struct CiljPosition : IComponentData
{
    public float3 Value; 
}

// ComponentDataWrapper is for creating a Monobehaviour representation of this component (for editor support).
public class CiljPositionComponent : ComponentDataWrapper<CiljPosition> { }