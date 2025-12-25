using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSharedData : ISharedData
{
    public bool Lock { get; set; }
    public GameEvent Event { get; set; }
    public GameStatus Status { get; set; }
    public Unit Destination { get; set; }
    public Cell Target { get; set; }
}
