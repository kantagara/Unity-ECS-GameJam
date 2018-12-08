using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ComponentSystems run on the main thread. Use these when you have to do work that cannot be called from a job.
public class HelloSpawnerSystem : ComponentSystem
{
    ComponentGroup m_Spawners;

    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(HelloSpawner), typeof(Position));
    }

    protected override void OnUpdate()
    {
        // Need a list because slot 0 is for the default value of SharedComponentData.
        // It's only there because of API limitations. We only care about the SharedComponentData in slot 1.
        var uniqueSpawners = new List<HelloSpawner>(2);

        // Get all the spawners in the scene.
        // In this case, there's only 1, but you could potentially have several spawners with, for example, different prefabs.
        EntityManager.GetAllUniqueSharedComponentData(uniqueSpawners);
        HelloSpawner spawner = uniqueSpawners[1];

        // Filter the component group so we're only looking at entities with shared component data we set in the editor.
        // In this case, it's only 1 entity, and it corresponds to the CubeSpawner game object in the scene.
        m_Spawners.SetFilter(spawner);

        // Create an entity from the prefab set on the spawner component.
        // This can't be called from a job, which is why we're doing this in a ComponentSystem on the main thread.
        Entity spawnedCubeEntity = EntityManager.Instantiate(spawner.prefab);

        // Set the position of the newly spawned cube to the origin.
        var position = new Position
        {
            Value = float3.zero
        };
        EntityManager.SetComponentData(spawnedCubeEntity, position);

        // Destroy the spawner so this system only runs once.
        var spawnerEntity = m_Spawners.GetEntityArray()[0];
        EntityManager.DestroyEntity(spawnerEntity);
    }
}
