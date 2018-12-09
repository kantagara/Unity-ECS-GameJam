using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotationSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> RotationComponent;
        public ComponentArray<Rigidbody> Rigidbody;

    }

    [Inject] private Data _data;
    protected override void OnUpdate()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            var rotation = _data.RotationComponent[i].Value;
            _data.Rigidbody[i].MoveRotation(rotation);
        }
    }
}
