using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemyMovementSystem : JobComponentSystem
{
    [BurstCompile]
    struct HelloMovementSpeedJob : IJobProcessComponentData<Position, EnemyMovement>
    {
        public float dT;
        
        public void Execute(ref Position position, ref EnemyMovement enemyMovement)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = position.Value;

            position.Value = (pos - new float3{x = mousePosition.x, y = mousePosition.y, z = mousePosition.z}) 
                             * dT;
             
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new HelloMovementSpeedJob() {dT = Time.deltaTime};
        return job.Schedule(this, inputDeps);
    }
}
