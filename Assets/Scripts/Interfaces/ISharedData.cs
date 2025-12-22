using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISharedData
{
    bool Lock { get; set; } // то может делать
    GameEvent Event { get; set; }
    GameStatus Status { get; set; }

    Unit Destination {  get; set; } //ѕерсонаж выбранный
    Cell Target { get; set; } //ячейка цели
}
