using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var mouseHit = MouseToWorld.Instance.GetMouseRaycastHit2D();

        if (!mouseHit.collider) return;

        var test = mouseHit.collider.transform.name;

        Debug.Log(test);

    }
}
