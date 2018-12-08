using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public class PlayerShootingSystem : JobComponentSystem
{
    private struct PlayerShootingJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityCommandBuffer;
        public bool IsFiring;

        public void Execute(int index)
        {
            if (!IsFiring) return;
            EntityCommandBuffer.SetComponent(EntityArray[index].Index, new Firing());

        }
    }
        private struct Data
        {
            public readonly int Length;
            public EntityArray Entites;
            public ComponentDataArray<Weapon> Weapons;
            public SubtractiveComponent<Firing> Firings;
        }
        [Inject] private Data _data;
        [Inject] private PlayerShootinBarrier _barriers;


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new PlayerShootingJob {
            EntityArray = _data.Entites,
            EntityCommandBuffer = _barriers.CreateCommandBuffer().ToConcurrent(),
            IsFiring = Input.GetButton("Fire1")
            }.Schedule(_data.Length, 64, inputDeps);
    }
    
}
public class PlayerShootinBarrier: BarrierSystem
{

}
