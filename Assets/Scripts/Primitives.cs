using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeighbourType
{
    ForwardRight, Forward, ForwardLeft,
    Left, Right,
    BackwardRight, Backward, BackwardLeft,
}

public enum Team
{
    White, Black
}

public enum Type
{
    Checker, Queen
}

public enum GameStatus
{
    Error = 0,
    Lock = 1,
    Unlock = 2,
    Select = 3,
    Move = 4,
    Attack = 5,
    Confirm = 6
}

public enum GameEvent
{
    Empty   = 0,
    Select  = 1, //Выбор какой-то клетки
    Cancel  = 2, //Отмена последнего действия
    Confirm = 3, //Подтвержение последнего действия
    NewTurn = 4, //Новый ход
}

