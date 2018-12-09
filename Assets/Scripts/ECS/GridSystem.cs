using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class GridSystem : JobComponentSystem
{

    private ComponentGroup _componentGroup;
    
    struct GridSystemJob : IJobParallelFor
    {
        public EntityArray Array;
        public void Execute(int index)
        {
            
        }
    }


    protected override void OnStartRunning()
    {
        _componentGroup = GetComponentGroup(typeof(Destroyable));
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new GridSystemJob
        {
            Array = _componentGroup.GetEntityArray()
        }.Schedule(_componentGroup.GetEntityArray().Length, 64, inputDeps);
    }
}
