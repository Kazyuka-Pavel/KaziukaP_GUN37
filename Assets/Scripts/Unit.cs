using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [field: SerializeField]    public Team Team { get; private set; }
    [field: SerializeField]    public UnitGameSettings Settings { get; private set; }
    [SerializeField] private MeshRenderer cylinderMesh;
    [SerializeField] private MeshRenderer queenMesh;
    [Inject] private UnitsSettings _unitsSettings;

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

    private void Awake()
    {
        ResetSelect();
    }    

    public void SetSelect(Material material)
    {
        cylinderMesh.material = material;
        queenMesh.material = material;
    }

    public void ResetSelect()
    {
        cylinderMesh.material = _unitsSettings[Team].Material;
        queenMesh.material = _unitsSettings[Team].Material;
    }
}
