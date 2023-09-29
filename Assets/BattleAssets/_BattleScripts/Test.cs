using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        if (TurnSystem.Instance.IsPlayerTurn()) return;

        TurnSystem.Instance.NextTurn();

        // var mouseHit = MouseToWorld.Instance.GetMouseRaycastHit2D();

        // if (!mouseHit.collider) return;

        // var test = mouseHit.collider.transform.name;

        // Debug.Log(test);

    }
}
