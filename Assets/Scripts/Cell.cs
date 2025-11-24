using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private MeshRenderer focusMesh;
    private MeshRenderer selectMesh;
    private Unit _unit;

    public event Action<Cell> OnPointerClickEvent;

    private void Awake()
    {
        var focus   = transform.Find("Focus");
        var select  = transform.Find("Select");

        focusMesh   = focus.gameObject.GetComponent<MeshRenderer>();
        selectMesh  = select.gameObject.GetComponent<MeshRenderer>();
    }

    public void SetSelect(Material material)
    {
        selectMesh.enabled = true;
    }

    public void ResetSelect()
    {
        selectMesh.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        focusMesh.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        focusMesh.enabled = false;
    }
    
    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }
}
