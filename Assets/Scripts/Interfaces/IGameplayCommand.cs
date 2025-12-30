using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayCommand 
{
    //public HashSet<Cell> Cells { get; }
    public Dictionary<Cell, List<Cell>> CellsDictionaryWalk { get; }
    public Dictionary<Cell, List<Cell>> CellsDictionaryAttack { get; }
    public HashSet<Unit> UnitsAttack { get;  }
    void Interact (Cell cell);
}
