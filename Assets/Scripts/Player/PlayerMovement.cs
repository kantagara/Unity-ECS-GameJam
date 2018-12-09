using UnityEngine;
using Unity.Entities;

public class PlayerMovement : ComponentSystem
{

    public struct Filter
    {
        public Rigidbody rb;
        public InputComponent values;
    }



    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        foreach (var entity in GetEntities<Filter>())
        {
            var moveVector = new Vector3(entity.values.Horizontal, entity.values.Vertical,0);
            var movePosition = entity.rb.position + moveVector.normalized * 3 * deltaTime;
            entity.rb.MovePosition(movePosition);
        }
    }
}
