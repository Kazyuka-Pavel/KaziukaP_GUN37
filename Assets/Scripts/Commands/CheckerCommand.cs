using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Zenject;

//Паттерн комманд

public class CheckerCommand : IGameplayCommand
// Правила для шашек
{
    public IEnumerable<Cell> Variants { get; } //список выделейнных клеток доя отображения воможных ходов    
    public HashSet<Cell> Cells { get; private set; }

    [Inject] private ISharedData _data;
    [Inject] private ITurn _turn;
    [Inject] private CellPaletteSettings _settings;
    [Inject] private Battlefield _battlefield;
    [Inject] private SignalBus _signal;               

    public void Calculate(Unit unit)
    {
        //var isQueen = (unit.Power & UnitPower.Queen) == UnitPower.Queen; //Есди дам, то можно ходить в любом напрвлении
        //var directions = isQueen
        //    ? stackalloc NeighbourType[]
        //    {
        //        NeighbourType.ForwardLeft, NeighbourType.ForwardRight,
        //        NeighbourType.BackwardLeft,NeighbourType.BackwardRight
        //    }
        //    : unit.Team is Team.White
        //        ? stackalloc NeighbourType[]
        //        {
        //            NeighbourType.ForwardLeft,NeighbourType.ForwardRight
        //        }
        //        : stackalloc NeighbourType[]
        //        {
        //            NeighbourType.BackwardLeft,NeighbourType.BackwardRight
        //        };

        //for (int i =0; iMax = directions.Length; i < iMax; i++)
        //{
        //    CheckDirection(unit, isQueen, directions[i]);
        //}
    }

    private void CheckDirection(Unit unit, bool isQueen, NeighbourType direction)
    {
        var cell = unit.Cell;
        //Ищем клетку, если есть - дальше, если нет - выход
        while (_battlefield.TryGet(cell, direction, out var target))
        {
            //if (target.Unit == null) //если на клетке нет юнита, то можно туда сходить
            //{
            //    Cells.Add(target);
            //    if (isQueen)
            //    {
            //        cell = target;
            //        continue;
            //    }
            //    return;
            //}

            //var other = target.Unit;
            //if (other.Team == unit.Team) //если занята своим же, выход
            //    return;

            //cell = target;
            //if (!_battlefield.TryGet(cell, direction, out target)) return;
            //if (target.Unit != null)
            //    return;
            //Cells.Add(target);
        }
    }


    public void Interact(Cell cell)
    {
        if (_data.Status == GameStatus.Select)
        {
            if (cell.Unit != null && cell.Unit.Team == _turn.Current)
            {
                _data.Destination = cell.Unit;
                _signal.Fire(GameEvent.Select);
            }                        
        }
        //cell.SetSelect(_settings.SelectCell);        
    }

}


