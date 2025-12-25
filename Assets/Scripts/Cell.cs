using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private MeshRenderer focusMesh;
    [SerializeField] private MeshRenderer selectMesh;
    public Unit Unit { get; set; }    

    public event Action<Cell> OnPointerClickEvent;
    public bool IsEmpty => Unit == null;

    public void SetSelect(Material material)
    {
        selectMesh.enabled = true;
        selectMesh.material = material;
    }

    public void ResetSelect()
    {
        selectMesh.enabled = false;
    }

    //Событие через Рейкастер
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);
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
    
}
