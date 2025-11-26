using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayCommand 
{
    IEnumerable<Cell>   Variants { get; }
    void Interact(Cell cell);
}
