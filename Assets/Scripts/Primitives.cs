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
    Control     = 1, //Проверка после начала хода, есть ли обязатльеные атаки  
    SelectUnit  = 2, //Выбор игрока                         //Battlefield 
    SelectCell  = 3, //Выбор какой-то клетки                //Battlefield 
    Cancel      = 4, //Отмена последнего действия           //BattleController - откат выделении и выбора
    Confirm     = 5, //Подтвержение последнего действия     //PlayerController - предвижение шашек и механика поглощения
    Play        = 6, //Подтвержение последнего действия     //PlayerController - проигрывание передвижения
    End         = 7, //Конец хода                           //CheckerCommand   - завершения механики передвижения шашек (контроль преобрзования в дамки)
    NewTurn     = 8, //Новый ход                            //BattleController - откат выделении и выбора и переход к другой стороне
    StartTurn   = 9, //Начало нового хода                   //CheckerCommand    - откат выделении и выбора и переход к другой стороне

}

