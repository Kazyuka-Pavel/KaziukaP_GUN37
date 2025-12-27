using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Zenject;

//Паттерн комманд

public class CheckerCommand : IGameplayCommand
// Правила для шашек
{ 
    //public HashSet<Cell> Cells { get; private set; } = new HashSet<Cell>();
    public Dictionary<Cell, List<Cell>> CellsDictionary { get; private set; } = new Dictionary<Cell, List<Cell>>(); //массив Cells - это списаок ячеек на пути

    [Inject] private ISharedData _data;
    [Inject] private ITurn _turn;
    [Inject] private CellPaletteSettings _settings;
    [Inject] private Battlefield _battlefield;
    [Inject] private SignalBus _signal;
    [Inject] private UnitGameSettings _queenSettings;

    public void Calculate(Unit unit)
    {;
        var isQueen = (unit.Settings == _queenSettings); //Есди дам, то можно ходить в любом напрвлении
        var directions = isQueen
            ? stackalloc NeighbourType[]
            {
                NeighbourType.ForwardLeft, NeighbourType.ForwardRight,
                NeighbourType.BackwardLeft,NeighbourType.BackwardRight
            }
            : unit.Team is Team.White
                ? stackalloc NeighbourType[]
                {
                    NeighbourType.BackwardLeft,NeighbourType.BackwardRight
                }
                : stackalloc NeighbourType[]
                {
                    NeighbourType.ForwardLeft,NeighbourType.ForwardRight
                };
        for (int i = 0; i < directions.Length; i++)
        {
            CheckDirection(unit, isQueen, directions[i]);
        }
    }

    private void CheckDirection(Unit unit, bool isQueen, NeighbourType direction)
    {
        var cell = unit.Cell;
        var cells = new List<Cell>();
        //Ищем клетку, если есть - дальше, если нет - выход
        while (_battlefield.TryGet(cell, direction, out var target))
        {
            if (target.Unit == null) //если на клетке нет юнита, то можно туда сходить
            {
                //Cells.Add(target);
                if (!CellsDictionary.ContainsKey(target)) CellsDictionary.Add(target, cells);                
                if (isQueen)
                {
                    cells.Add(target);
                    cell = target;                    
                    continue;
                }
                return;
            }

            var other = target.Unit;
            if (other.Team == unit.Team) //если занята своим же, выход
                return;

            cells.Add(target);
            cell = target;
            if (!_battlefield.TryGet(cell, direction, out target)) return;
            if (target.Unit != null)
                return;
            //Cells.Add(target);
            if (!CellsDictionary.ContainsKey(target)) CellsDictionary.Add(target, cells);
        }
    }


    public void Interact(Cell cell)
    {
        switch (_data.Status)
        {
            case GameStatus.Select:
                if (cell.Unit != null && cell.Unit.Team == _turn.Current)
                {
                    _data.Destination = cell.Unit;
                    _data.Status = GameStatus.Move;
                    Calculate(cell.Unit);
                    _signal.Fire(GameEvent.Select);
                }
                break;
            case GameStatus.Move:
                //if (Cells.Contains(cell))
                if (CellsDictionary.ContainsKey(cell))                        
                {
                    _data.Target = cell;
                    _data.Cells.Clear();
                    if (CellsDictionary.TryGetValue(cell,out List<Cell> value))
                        _data.Cells.AddRange(value);
                    _signal.Fire(GameEvent.Confirm);
                }
                break;
        }              
    }

    private readonly struct CellsVariables : IEquatable<CellsVariables>
    {        
        private readonly Cell _cell;
        private readonly Cell[] _cells;
        public CellsVariables(Cell cell, Cell[] cells)
            => (_cell, _cells) = (cell, cells);
        public bool Equals(CellsVariables other)
            => Equals(_cell, other._cell);
        public override bool Equals(object obj)
            => obj is CellsVariables other && Equals(other);

        public override int GetHashCode()
            => unchecked(HashCode.Combine(_cell, _cells) - 13);

    }

}


