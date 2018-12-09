using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class DestroySystem : ComponentSystem
{
    private ComponentGroup _componentGroup;

    private NativeList<Entity> nativeList;
    protected override void OnStartRunning()
    {
        nativeList = new NativeList<Entity>(Allocator.Temp);
    }


    protected override void OnUpdate()
    {
        _componentGroup = GetComponentGroup(typeof(Position), typeof(Tag), typeof(Destroyable), typeof(CircleCollider));

        var array = _componentGroup.GetEntityArray();
        nativeList = new NativeList<Entity>(Allocator.Temp);
        
        for (var i = 0; i < array.Length; i++)
        {
            var buffer = EntityManager.GetBuffer<CollisionList>(array[i]);
            var tag = EntityManager.GetComponentData<Tag>(array[i]).Name;
            var destroyable = EntityManager.GetComponentData<Destroyable>(array[i]);
            
            

            for (int j = 0; j < buffer.Length; j++)
            {
            
            if (tag == TagType.Enemy && buffer[j].Tag.Name == TagType.Enemy)
            {
                //destroyable.ToDestroy = 1;
            }
       

            if (tag == TagType.Enemy && buffer[j].Tag.Name == TagType.Bullet)
            {
                destroyable.ToDestroy = 1;
            }

            if (tag == TagType.Player && buffer[j].Tag.Name == TagType.Enemy)
            {
                destroyable.ToDestroy = 1;
            }

        }
        if(destroyable.ToDestroy == 1)
            nativeList.Add(array[i]);
    

    }

    for(int i = 0; i < nativeList.Length; i++){
        if(EntityManager.Exists(nativeList[i]))
        EntityManager.DestroyEntity(nativeList[i]);
     }
    
    nativeList.Clear();
    nativeList.Dispose();
}
}
