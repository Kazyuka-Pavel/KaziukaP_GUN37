using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    public Team Team { get; private set; }
    public Cell Cell {  get; private set; }

    public event Action OnMoveEndCallback;

    public void OnPointerClick(PointerEventData eventData) => Cell.OnPointerClick(eventData);
    
    public void OnPointerExit(PointerEventData eventData) => Cell.OnPointerExit(eventData);

    public void OnPointerEnter(PointerEventData eventData) => Cell.OnPointerExit(eventData);

    public void Move(Cell cell)
    {
        StartCoroutine(OnMove(cell));
    }

    private float _speed = 1f;

    [SerializeField]
    private UnitGameSettings _settings;

    private IEnumerator OnMove(Cell cell)
    {
        var source = transform;

        var start = source.position;
        var end = cell.transform.position;
        var time = Vector3.Distance(start, end) / _speed;
        var delta = 0f;
        while (delta < time)
        {
            source.position = Vector3.Lerp(start, end, delta / time);
            delta += Time.deltaTime;
            yield return null;
        }

        Cell = cell;
        OnMoveEndCallback?.Invoke();
    }

    public void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal object Select(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
