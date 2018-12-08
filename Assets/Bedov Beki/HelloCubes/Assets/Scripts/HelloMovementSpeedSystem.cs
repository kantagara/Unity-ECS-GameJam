using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// This system updates all entities in the scene with both a HelloMovementSpeed and Position component.
public class HelloMovementSpeedSystem : JobComponentSystem
{
    [BurstCompile]
    struct HelloMovementSpeedJob : IJobProcessComponentData<Position, HelloMovementSpeed>
    {
        public float dT;
        public float3 ciljPozicija;

        // Move something in the +X direction at the speed given by HelloMovementSpeed.
        // If this thing's X position is more than 2x its speed, reset X position to 0.
        public void Execute(ref Position Position, [ReadOnly]ref HelloMovementSpeed movementSpeed)
        {
            float moveSpeed = movementSpeed.Value * dT;
            Position.Value = Position.Value + moveSpeed * (ciljPozicija-Position.Value) ;

        }
    }

    private ComponentGroup _componentGroup;
    private GameObject _player;
    protected override void OnStartRunning()
    {
        _componentGroup = GetComponentGroup(typeof(CiljPosition), typeof(Position));;
        _player = GameObject.Find("");
        
    }

    // OnUpdate runs on the main thread.
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new HelloMovementSpeedJob() { dT = Time.deltaTime, ciljPozicija = 
             _player.transform.position};
        return job.Schedule(this, inputDependencies);
    }
}
