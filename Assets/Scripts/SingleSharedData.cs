using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSharedData : ISharedData
{
    public bool Lock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GameEvent Event { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GameStatus Status { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Unit Destination { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Cell Target { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
