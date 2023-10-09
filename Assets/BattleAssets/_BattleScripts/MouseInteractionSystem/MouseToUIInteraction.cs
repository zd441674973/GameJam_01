using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseToUIInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public DragAndDropVisualMode dragAndDropVisualMode => throw new System.NotImplementedException();


    public void OnPointerDown(PointerEventData eventData)
    {
        MouseActionManager.Instance.CurrentDraggingCard = transform.GetComponentInParent<Transform>().gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(transform.GetComponentInParent<Transform>().gameObject);

    }


    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
