﻿
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Player : IComponentData
{
   
}

public class PlayerComponent : ComponentDataWrapper<Player>
{
}
