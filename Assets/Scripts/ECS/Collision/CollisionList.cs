using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[Serializable]
public struct CollisionList: IBufferElementData
{
    public Tag Tag;
    public Position Position;
}

