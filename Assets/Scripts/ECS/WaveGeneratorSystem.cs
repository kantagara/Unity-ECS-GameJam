using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveGeneratorSystem : ComponentSystem
{
    ComponentGroup m_Spawners;
    private GameObject _gameObject;
    float time;
    
    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(WaveGenerator), typeof(Position));
        _gameObject = GameObject.Find("");
    }
    
    
    protected override void OnUpdate()
    {
        var uniqueSpawners = new List<WaveGenerator>(2);
        EntityManager.GetAllUniqueSharedComponentData(uniqueSpawners);
        var spawner = uniqueSpawners[1];
        
        m_Spawners.SetFilter(spawner);
        if (Time.time - time <= spawner.time)
        {
            return;
        }

        var spawnedCubeEntity = EntityManager.Instantiate(spawner.Prefab);
            var randomPosition = spawner.Positions[Random.Range(0, spawner.Positions.Length)];

        time = Time.time;

        // Set the position of the newly spawned cube to the origin.
        var position = new Position
            {
                Value = randomPosition.position
            };
            EntityManager.SetComponentData(spawnedCubeEntity, position);
        
    }
         
    
}
