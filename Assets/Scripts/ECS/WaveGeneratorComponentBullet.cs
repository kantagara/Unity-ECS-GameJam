
using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct WaveGeneratorBullet : ISharedComponentData
{
    public GameObject Prefab;
    public float time;
    public Transform[] Positions;
}

public class WaveGeneratorComponentBullet : SharedComponentDataWrapper<WaveGeneratorBullet>
{
    
}
