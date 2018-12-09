using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private struct Filter
    {
        public Transform Transform;
        public RotationComponent rotationComponent;
    }
    protected override void OnUpdate()
    {
        var mouse_pos = Input.mousePosition;
        var cameraRay = Camera.main.ScreenPointToRay(mouse_pos);
        mouse_pos.z = 5.23f; //The distance between the camera and object

       
        foreach (var entity in   GetEntities<Filter>())
        {

            var object_pos = Camera.main.WorldToScreenPoint(entity.Transform.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            entity.rotationComponent.Value = Quaternion.Euler(entity.rotationComponent.Value.x, entity.rotationComponent.Value.y,angle); 

        }
        
    }
}
