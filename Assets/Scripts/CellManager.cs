using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class CellManager : MonoBehaviour
{
    private Dictionary<CellNeighbour, Cell> _neighbours;
    private Cell[] _cells;

    public Action<Cell> OnCellClicked;

    public void Awake()
    {
        _cells = FindObjectsOfType<Cell>();
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
                    //var check = _neighbours.TryGetValue(key, out var cell);
                    if (Vector3.Distance(source, destination) <= 1)
                    {
                        _neighbours.Add(key, _cells[j]);
                    }
                }
            }
        }
        ;
        var units = FindObjectsOfType<Unit>();
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

    struct CellNeighbour
    {
        public NeighbourType neighbourType;
        public Cell cell;
        public CellNeighbour(NeighbourType neighbourType, Cell cell)
        {
            this.neighbourType = neighbourType;
            this.cell = cell;
        }
    }
}
