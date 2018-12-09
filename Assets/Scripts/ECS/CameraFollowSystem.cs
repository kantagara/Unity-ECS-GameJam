
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CameraFollowSystem : ComponentSystem
{
    private ComponentGroup _componentGroup;

    private float3 _offset;
    
    protected override void OnStartRunning()
    {
        _componentGroup = GetComponentGroup(typeof(Player), typeof(Position));
        var player = _componentGroup.GetEntityArray()[0];

        _offset = op_Subtraction(UnityEngine.Camera.main.transform.position,
            EntityManager.GetComponentData<Position>(player).Value);
    }

    private static float3 op_Subtraction(float3 x, float3 y)
    {
        return new float3(x.x - y.x, x.y -y.y, x.z - y.z);
    }


    protected override void OnUpdate()
    {
        var player = _componentGroup.GetEntityArray()[0];
       
        UnityEngine.Camera.main.transform.position = EntityManager.GetComponentData<Position>(player).Value + _offset;
    }
}
