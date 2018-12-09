using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemyMovementSystem : JobComponentSystem
{

    private GameObject Player;
    [BurstCompile]
    struct HelloMovementSpeedJob : IJobProcessComponentDataWithEntity<Position, EnemyMovement>
    {
        public float dT;
        public float3 ciljPozicija;
        [NativeDisableParallelForRestriction]
        public BufferFromEntity<CollisionList> CollisionList;

     

        public void Execute(Entity entity, int index, ref Position Position, ref EnemyMovement movementSpeed)
        {
            
            
            float moveSpeed = movementSpeed.Speed * dT;

            float3 rezultujuca = ciljPozicija - Position.Value;
            rezultujuca = JedinicniVektor(rezultujuca);
                
            if (CollisionList.Exists(entity))
            {
                var buffer = CollisionList[entity];

                for (int i = 0; i < buffer.Length; i++)
                {
                   if(buffer[i].Tag.Name == TagType.Enemy) {

                    rezultujuca += JedinicniVektor(Position.Value - (buffer[i].Position.Value));
                   }
                } 
            }

            float3 jedinicniVektorPlayer = JedinicniVektor(rezultujuca);

            Position.Value = Position.Value + moveSpeed * rezultujuca; // * razlika;
        }
        
        
        private float3 JedinicniVektor(float3 target)
        {
            float delilac = 0.001f; 
            if (Mathf.Abs(target.x) < 0.1) {
                delilac = 0.1f;
            }
            else {
                delilac = target.x;
            }
            float rez = Mathf.Abs((1 / delilac));

            float3 jedinicniVektorPlayer = new float3(target.x * rez, target.y * rez, 0);

            return jedinicniVektorPlayer;
        }
    }


    
    protected override void OnStartRunning()
    {
        Player = GameObject.FindWithTag("Player");
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new HelloMovementSpeedJob()
        {
            dT = Time.deltaTime, 
            ciljPozicija = Player.transform.position,
            CollisionList = GetBufferFromEntity<CollisionList>()
        };
        return job.Schedule(this, inputDeps);
    }
}
