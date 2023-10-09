using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanDraggedByMousePointer : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MouseActionManager.Instance.CurrentDraggingCard = transform.GetComponentInParent<Transform>().gameObject;
    }

}
