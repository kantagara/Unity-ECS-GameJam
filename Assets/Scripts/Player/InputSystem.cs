using Unity.Entities;
using UnityEngine;

public class InputSystem : ComponentSystem
{

    private struct Data
    {
        //we use Length to acces each impunt component
        public readonly int Length;
        public ComponentArray<InputComponent> InputComponent;
    }

    [Inject] private Data _data; 


    protected override void OnUpdate()
    {
        var horizontal= Input.GetAxis("Horizontal");
        var vertical= Input.GetAxis("Vertical");

        for (int i = 0; i < _data.Length; i++)
        {
            _data.InputComponent[i].Horizontal = horizontal;
            _data.InputComponent[i].Vertical = vertical;

        }
        
    }
}
