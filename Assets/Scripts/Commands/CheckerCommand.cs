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
    public Dictionary<Cell, List<Cell>> CellsDictionary { get; private set; } = new Dictionary<Cell, List<Cell>>(); //массив Cells - это списаок ячеек на пути

    private ISharedData _data;                  //injecred
    private ITurn _turn;                        //injecred
    private CellPaletteSettings _settings;      //injecred
    private Battlefield _battlefield;           //injecred
    private SignalBus _signal;                  //injecred
    private UnitGameSettings _queenSettings;    //injecred

    [Inject]
    private void Construct(Battlefield battlefield, ISharedData data, SignalBus signal, UnitGameSettings queenSettings, CellPaletteSettings settings, ITurn turn)
    {
        (_battlefield, _data, _signal, _queenSettings, _settings, _turn) = (battlefield, data, signal, queenSettings, settings, turn);
        _signal.Subscribe<GameEvent>(Callback); //Подпись на событие GameEvent
    }

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
        if (!isQueen)
        {
            var directionsForward =
                unit.Team is Team.White
                ? stackalloc NeighbourType[]
                {
                    NeighbourType.ForwardLeft,NeighbourType.ForwardRight
                }
                : stackalloc NeighbourType[]
                {
                    NeighbourType.BackwardLeft,NeighbourType.BackwardRight
                };
            for (int i = 0; i < directionsForward.Length; i++)
            {
                CheckDirection(unit, isQueen, directionsForward[i], true);
            }
        }        
    }

    private void CheckDirection(Unit unit, bool isQueen, NeighbourType direction,bool OnlyWithUnit = false)
    {
        var cell = unit.Cell;
        var cells = new List<Cell>();
        //Ищем клетку, если есть - дальше, если нет - выход
        while (_battlefield.TryGet(cell, direction, out var target))
        {
            if (target.Unit == null) //если на клетке нет юнита, то можно туда сходить
            {
                //Cells.Add(target);
                if ((!CellsDictionary.ContainsKey(target)) && (!OnlyWithUnit)) CellsDictionary.Add(target, cells);                
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
        if (_data.Event is GameEvent.Play) return;
        switch (_data.Event)
        {
            case GameEvent.SelectUnit:
                if (cell.Unit != null && cell.Unit.Team == _turn.Current)
                {
                    _data.Destination = cell.Unit;                    
                    Calculate(cell.Unit);
                    _signal.Fire(GameEvent.SelectUnit);
                    _data.Event = GameEvent.SelectCell;
                }
                break;
            case GameEvent.SelectCell:
                //if (Cells.Contains(cell))
                if (CellsDictionary.ContainsKey(cell))                        
                {
                    _data.Target = cell;
                    _data.Cells.Clear();
                    if (CellsDictionary.TryGetValue(cell,out List<Cell> value))
                        _data.Cells.AddRange(value);
                    _signal.Fire(GameEvent.SelectCell);
                }
                break;
        }              
    }
    private void Callback(GameEvent arg)
    {
        switch (arg)
        {
            case GameEvent.End:      
                if (_data.Target.IsLast) _data.Destination.SetQueen(_queenSettings);
                _signal.Fire(GameEvent.NewTurn);
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


