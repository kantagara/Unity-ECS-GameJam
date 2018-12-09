using System;
using Unity.Entities;

[Serializable]

public struct Destroyable : IComponentData
{
    public int ToDestroy;
}

public class DestroyableComponent : ComponentDataWrapper<Destroyable>
{}
