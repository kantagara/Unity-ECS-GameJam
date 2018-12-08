using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class WaveGeneratorSystem : ComponentSystem
{
    ComponentGroup m_Spawners;
    private float _time;
    
    
    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(WaveGenerator), typeof(Position));
    }
    
    protected override void OnUpdate()
    {
        var uniqueSpawners = new List<WaveGenerator>(2);
        EntityManager.GetAllUniqueSharedComponentData(uniqueSpawners);
        var spawner = uniqueSpawners[1];
        
        m_Spawners.SetFilter(spawner);
        
        if (Time.time - _time > spawner.time)
        {
            var spawnedCubeEntity = EntityManager.Instantiate(spawner.Prefab);

            // Set the position of the newly spawned cube to the origin.
            var position = new Position
            {
                Value = new float3
                {
                    x = UnityEngine.Random.Range(-2f, 2f),
                    y = UnityEngine.Random.Range(-2f, 2f),
                    z = UnityEngine.Random.Range(-2f, 2f)
                }
            };
            EntityManager.SetComponentData(spawnedCubeEntity, position);
            _time = Time.time;
        }
         
    }
}
