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
    public Unit Unit { get; private set; }    

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

    //Событие через Рейкастер
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent.Invoke(this);
    }

    //Событие через Рейкастер
    public void OnPointerEnter(PointerEventData eventData)
    {
        focusMesh.enabled = true;
    }

    //Событие через Рейкастер
    public void OnPointerExit(PointerEventData eventData)
    {
        focusMesh.enabled = false;
    }
    
    public void SetUnit(Unit unit)
    {
        Unit = unit;
    }
}
