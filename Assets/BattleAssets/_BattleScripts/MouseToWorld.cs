using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWorld : MonoBehaviour
{
    public static MouseToWorld Instance;

    [SerializeField]
    LayerMask _mouseLayerMask;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }


    public Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    public RaycastHit2D GetMouseRaycastHit2D() => Physics2D.Raycast(GetMousePosition(), Vector2.zero, float.MaxValue, _mouseLayerMask);


}
