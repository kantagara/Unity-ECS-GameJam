using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Grid : IComponentData
{
    public float2 Value;
    
}

public class GridComponent : ComponentDataWrapper<Grid>
{
   
}
