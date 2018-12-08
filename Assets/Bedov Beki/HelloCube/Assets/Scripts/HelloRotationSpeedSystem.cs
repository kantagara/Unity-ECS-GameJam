using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// This system updates all entities in the scene with both a HelloRotationSpeed and Rotation component.
public class HelloRotationSpeedSystem : JobComponentSystem
{
    [BurstCompile]
    struct HelloRotationSpeedJob : IJobProcessComponentData<Rotation, HelloRotationSpeed>
    {
        public float dT;

        // Rotate something about its up vector at the speed given by HelloRotationSpeed.
        public void Execute(ref Rotation rotation, [ReadOnly]ref HelloRotationSpeed rotSpeed)
        {
            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotSpeed.Value * dT));
        }
    }

    // OnUpdate runs on the main thread.
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new HelloRotationSpeedJob() { dT = Time.deltaTime };
        return job.Schedule(this, inputDependencies);
    }
}
