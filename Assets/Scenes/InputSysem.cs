using Unity.Entities;
using UnityEngine;

public class InputSysem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<InputComponent> inputComponents;
    }
    [Inject] private  Data _data;

    protected override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        for (int i = 0; i < _data.Length; i++)
        {
            _data.inputComponents[i].Horizontal = horizontal;
            _data.inputComponents[i].Vertical = vertical;
        }
    }
}
