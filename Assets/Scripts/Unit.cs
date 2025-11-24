using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private Cell _cell;

    public event Action<Cell, Unit> OnMoveEndCallback;

    public void OnPointerClick(PointerEventData eventData) => _cell.OnPointerClick(eventData);
    
    public void OnPointerExit(PointerEventData eventData) => _cell.OnPointerExit(eventData);

    public void OnPointerEnter(PointerEventData eventData) => _cell.OnPointerExit(eventData);

    public void Move(Cell cell)
    {
        _cell = cell;
    }

    public void SetCell(Cell cell)
    {
        _cell = cell;
    }
}
