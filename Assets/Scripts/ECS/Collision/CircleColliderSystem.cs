
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CircleColliderSystem : JobComponentSystem
{
   private ComponentGroup _componentGroup;

   
   //[BurstCompile]
   struct CircleColliderJob : IJobProcessComponentDataWithEntity<Position, CircleCollider, Tag>
   {
      [ReadOnly]
      public ComponentDataArray<Position> PositionArray;
      [ReadOnly]
      public ComponentDataArray<CircleCollider> CircleColliderArray;

      [ReadOnly] public ComponentDataArray<Tag> TagArray;

      [NativeDisableParallelForRestriction]
      public BufferFromEntity<CollisionList> Buffer;

      public void Execute(Entity entity, int index, [ReadOnly]ref Position position,
         [ReadOnly]ref CircleCollider circleCollider, [ReadOnly] ref Tag data2)
      {
         Buffer[entity].Clear();
         for (var i = 0; i < PositionArray.Length; i++)
         {
            var samePosition = position.Value == PositionArray[i].Value;
            if(samePosition.x && samePosition.y && samePosition.z) continue;

            if (math.sqrt(math.pow((position.Value.x - PositionArray[i].Value.x), 2) +
                          math.pow((position.Value.y - PositionArray[i].Value.y), 2)) <
                circleCollider.Radius + CircleColliderArray[i].Radius)
            {
               Buffer[entity].Add(new CollisionList()
               {
                  Position =  PositionArray[i],
                  Tag = TagArray[i]
               });
            }

         }

        }
   }

   protected override void OnStartRunning()
   {
      _componentGroup = GetComponentGroup(typeof(CircleCollider), typeof(Position), typeof(Tag));
      for (int i = 0; i < _componentGroup.GetEntityArray().Length; i++)
      {
         EntityManager.AddBuffer<CollisionList>(_componentGroup.GetEntityArray()[i]);
      }
   }

   protected override JobHandle OnUpdate(JobHandle inputDeps)
   {
      return new CircleColliderJob(){
            PositionArray =  _componentGroup.GetComponentDataArray<Position>(), 
            CircleColliderArray = _componentGroup.GetComponentDataArray<CircleCollider>(),
            TagArray =  _componentGroup.GetComponentDataArray<Tag>(),
            Buffer = GetBufferFromEntity<CollisionList>()
         }
         .Schedule(this, inputDeps);
   }
}
