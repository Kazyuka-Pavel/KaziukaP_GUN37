using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Battlefield : IDisposable
{
    [Inject]
    private IGameplayCommand _command;
    private CellPaletteSettings _paletters;
    private ISharedData _data;

    public event Action<Cell> OnCellClicked;

    private Dictionary<CellNeighbour, Cell> _neighbours;
    private Cell[] _cells;

    private void Callback()
    {
        //Снимается выделение
        foreach (var cell in _cells)
            cell.ResetSelect();

        //Подсветка выделенного
        if (_data.Destination != null)
            _data.Destination.Cell.SetSelect(_paletters.SelectCell);

        //Выбор материаа, в завсимости от статуса
        var mat = _data.Status switch
        {
            GameStatus.Move => _paletters.MoveCell,
            GameStatus.Attack => _paletters.AttackCell,
            _ => default(Material)
        };
        if (mat != null)
            foreach (var cell in _command.Variants)
                cell.SetSelect(mat);
        if (_data.Target != null)
            _data.Destination.Cell.SetSelect(_paletters.ConfirmCell);
    }

    public Battlefield(SignalBus signal, ISharedData data, CellPaletteSettings palettes)
    {
        _cells = UnityEngine.Object.FindObjectsOfType<Cell>();
        _neighbours = new Dictionary<CellNeighbour, Cell>(_cells.Length * 8);
        var positions = Array.ConvertAll(_cells, t => t.transform.position);
        var distance = 0f;
        for (int i = 0, iMax = _cells.Length; i < iMax; i++)
        {
            _cells[i].OnPointerClickEvent += OnCellClicked;

            for (int j = 0, jMax = _cells.Length; j < jMax; j++)
            {
                if (i == j) continue;
                var source = positions[i];
                var destination = positions[j];

                var forward = destination.z.CompareTo(source.z);
                var right = destination.x.CompareTo(source.x);
                var type = (forward, right) switch
                {
                    (1, 1) => NeighbourType.ForwardRight,
                    (1, 0) => NeighbourType.Forward,
                    (1, -1) => NeighbourType.ForwardLeft,
                    (0, 1) => NeighbourType.Right,
                    (0, -1) => NeighbourType.Left,
                    (-1, 1) => NeighbourType.BackwardRight,
                    (-1, 0) => NeighbourType.Backward,
                    (-1, -1) => NeighbourType.BackwardLeft,
                    _ => default
                };
                if (type != default)
                {
                    var key = new CellNeighbour(type, _cells[i]);
                    var check = _neighbours.TryGetValue(key, out var cell)
                    ? Vector3.Distance(source, cell.transform.position)
                        : float.MaxValue;

                    distance = Vector3.Distance(source, destination);
                    if (distance < check)
                        _neighbours[key] = _cells[i];
                }
            }
        }
        ;
        var units = UnityEngine.Object.FindObjectsOfType<Unit>();
        var positionsj = Array.ConvertAll(units, t => t.transform.position);
        var iMin = 0;
        for (int j = 0, jMax = units.Length; j < jMax; j++)
        {
            for (int i = 0, iMax = _cells.Length; i < iMax; i++)
            {
                var source = positionsj[j];
                var destination = positions[i];
                if (i == 0)
                {
                    iMin = i;
                    continue;
                }
                if (Vector3.Distance(source, destination) < Vector3.Distance(source, positions[iMin]))
                {
                    iMin = i;
                }
            }
            if (iMin != 0)
            {
                units[j].SetCell(_cells[iMin]);
                _cells[iMin].SetUnit(units[j]);
            }
        }
    }

    public void Dispose()
    {
        for (int i = 0, iMax = _cells.Length; i < iMax; i++)
        {
            _cells[0].OnPointerClickEvent -= OnCellClicked;
        }
    }

    /// <summary>
    /// Получаем ячейку и уведомление, что есть значение
    /// </summary>        
    public bool TryGet(Cell source, NeighbourType type, out Cell cell)
    {
        var key = new CellNeighbour(type, source);
        return _neighbours.TryGetValue(key, out cell);
    }

    private readonly struct CellNeighbour : IEquatable<CellNeighbour>
    {
        private readonly NeighbourType _neighbourType;
        private readonly Cell _cell;
        public CellNeighbour(NeighbourType neighbourType, Cell cell)
            => (_neighbourType, _cell) = (neighbourType, cell);
        public bool Equals(CellNeighbour other)
            => _neighbourType == other._neighbourType && Equals(_cell, other._cell);
        public override bool Equals(object obj)
            => obj is CellNeighbour other && Equals(other);
        public override int GetHashCode()
            => unchecked(HashCode.Combine(_neighbourType, _cell) - 13);

    }
}
