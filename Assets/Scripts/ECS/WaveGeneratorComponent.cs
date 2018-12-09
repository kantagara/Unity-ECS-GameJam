
using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct WaveGenerator : ISharedComponentData
{
    public GameObject[] Prefab;
    public float time;
    public Transform[] Positions;
}

public class WaveGeneratorComponent : SharedComponentDataWrapper<WaveGenerator>
{
    
}
