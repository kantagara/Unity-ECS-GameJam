using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class BulletSystem : JobComponentSystem
{
    [BurstCompile]
    public struct BulletJobSystem : IJobProcessComponentData<Position, Target, Bullet>
    {
        public float dT;

        public void Execute(ref Position data0, ref Target data2, ref Bullet data3)
        {
           
            data2.Position.z = 0;
            data0.Value += data2.Position;
            //var rot = math.forward(data2.Value);
            //data0.Value += new Vector3(1f, 0f, 0f);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new BulletJobSystem() { dT = Time.deltaTime }.Schedule(this, inputDeps);
    }

   
    
    
}
