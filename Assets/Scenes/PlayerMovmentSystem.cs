using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class PlayerMovmentSystem : ComponentSystem
{
    public float Horizontal;
    
    private struct Filter
    {
        public Rigidbody Rigidbody;
        public InputComponent InputComponent;
    }
  

    protected override void OnUpdate()
    {
        
        var time = Time.deltaTime;
        foreach (var entity in GetEntities<Filter>())
        {
            var moveVector = new Vector3(entity.InputComponent.Horizontal, 0, entity.InputComponent.Vertical);
            var movePosition = entity.Rigidbody.position + moveVector.normalized * 3 * time;

            entity.Rigidbody.MovePosition(movePosition);


        }
    }
}
