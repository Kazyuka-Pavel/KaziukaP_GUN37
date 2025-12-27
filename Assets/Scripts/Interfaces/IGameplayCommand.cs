using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayCommand 
{
    //public HashSet<Cell> Cells { get; }
    public Dictionary<Cell, List<Cell>> CellsDictionary { get; }

    void Interact (Cell cell);
}
