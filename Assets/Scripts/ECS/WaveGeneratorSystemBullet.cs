using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveGeneratorSystemBullet : ComponentSystem
{
    ComponentGroup m_Spawners;
    ComponentGroup player;
    float time;
    
    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(WaveGeneratorBullet), typeof(Position));
        player = GetComponentGroup(typeof(Player),typeof(Position));
    }
    
    
    protected override void OnUpdate()
    {
        var uniqueSpawners = new List<WaveGeneratorBullet>(2);
        EntityManager.GetAllUniqueSharedComponentData(uniqueSpawners);
        var spawner = uniqueSpawners[1];



        m_Spawners.SetFilter(spawner);
        //if (Time.time - time <= spawner.time)
        //{
        //    return;
        //}
        if (Input.GetMouseButton(0))
        {
            //Instanciranje metka
            var spawnedCubeEntity = EntityManager.Instantiate(spawner.Prefab);
            var randomPosition = spawner.Positions[Random.Range(0, spawner.Positions.Length)];

            var position = new Position
            {
                Value = spawner.Positions[0].position
            };

            EntityManager.SetComponentData(spawnedCubeEntity, position);

            Position PlayerPosition = EntityManager.GetComponentData<Position>(player.GetEntityArray()[0]);

            var mouse_pos = Input.mousePosition;
            var layerMask = LayerMask.GetMask("Floor");
            var point = Camera.main.ScreenPointToRay (mouse_pos);
            RaycastHit hit;
            Debug.Log("mousePoint" + point);
                //if (Physics.Raycast(cameraRay, out hit, 100, layerMask))
                //{
                   mouse_pos = Camera.main.ScreenToWorldPoint(point.origin);

                     float2 inPerspective = 0f;
            inPerspective.x = mouse_pos.x - PlayerPosition.Value.x;


            inPerspective.y = mouse_pos.y - PlayerPosition.Value.y;
            Debug.Log("pos" + position.Value);
            Debug.Log("inPerspective" + inPerspective);
            //    //mouse_pos.z = 0f;
            //}

            var object_pos = EntityManager.GetComponentData<Position>(spawnedCubeEntity).Value;

            //mouse_pos.y = mouse_pos.y - object_pos.y;
            //var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            var angle = Mathf.Atan2(inPerspective.x, inPerspective.y)  * Mathf.Rad2Deg;

            var rotation = new Rotation
            {
                Value = quaternion.Euler(new float3(0, 0, angle))
            };

            EntityManager.SetComponentData(spawnedCubeEntity, rotation);

            var target = new Target
            {
                Position = object_pos
            };

            EntityManager.SetComponentData(spawnedCubeEntity, target);
        }
    }
         
    
}
