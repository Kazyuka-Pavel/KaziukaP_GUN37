using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeighbourType
{
    ForwardRight, Forward, ForwardLeft,
    Left, Right,
    BackwardRight, Backward, BackwardLeft,
    Empty
}

public enum Team
{
    White, Black
}

public enum Type
{
    Checker, Queen
}

public enum GameEvent
{
    Empty       = 0, //Пустой   
    SelectUnit  = 1, //Выбор игрока                         //CheckerCommand 
    SelectCell  = 2, //Выбор какой-то клетки                //CheckerCommand 
    Cancel      = 3, //Отмена последнего действия           //BattleController - откат выделении и выбора
    Confirm     = 4, //Подтвержение последнего действия     //PlayerController - предвижение шашек и механика поглощения
    Play        = 5, //Подтвержение последнего действия     //PlayerController - проигрывание передвижения
    End         = 6, //Конец хода                           //CheckerCommand   - завершения механики передвижения шашек (контроль преобрзования в дамки)
    NewTurn     = 7, //Новый ход                            //BattleController - откат выделении и выбора и переход к другой стороне
    
}

