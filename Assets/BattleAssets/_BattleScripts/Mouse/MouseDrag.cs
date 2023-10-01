using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MouseDrag : MonoBehaviour
{
    [SerializeField] bool _isDargging;

    Vector2 _offset;
    Vector2 _cardSlotPosition;


    void Update()
    {
        if (!TurnSystem.Instance.IsPlayerTurn()) return;
        GetCardOffset();
        Dragging();
        ResetDrag();
    }



    RaycastHit2D CurrentDraggedCard() => MouseToWorld.Instance.GetMouseRaycastHit2D();
    void GetCardOffset()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CurrentDraggedCard()) return;

        Debug.Log(CurrentDraggedCard().transform);

        _cardSlotPosition = CurrentDraggedCard().transform.parent.position;
        Vector2 cardPosition = CurrentDraggedCard().transform.position;
        Vector2 mousePosition = MouseToWorld.Instance.GetMousePosition();
        _offset = mousePosition - cardPosition;
    }

    void Dragging()
    {
        if (!Input.GetMouseButton(0)) return;
        if (!CurrentDraggedCard()) return;

        //Debug.Log(Distance());
        var mousePosition = MouseToWorld.Instance.GetMousePosition();
        CurrentDraggedCard().transform.position = mousePosition - _offset;

    }

    void ResetDrag()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        if (!CurrentDraggedCard()) return;

        float battleSlotSnapDistance = 1.5f;
        if (Distance() < battleSlotSnapDistance)
        {
            Transform currentCard = CurrentDraggedCard().transform;
            CardDeckManager.Instance.GetPlayerDiscardDeck().Add(currentCard);
            CurrentDraggedCard().transform.gameObject.SetActive(false);
            if (currentCard.CompareTag("PlayerCard")) LevelManager.Instance.PlayerCardSlotCheck();

            Debug.Log("The Card has been played : " + currentCard);
            CardActionManager.Instance.CardIsPlayed = true;

            return;
        }

        CurrentDraggedCard().transform.position = _cardSlotPosition;
    }

    float Distance()
    {
        var battleSlotPosition = LevelManager.Instance.GetBattleArea().position;
        var cardPosition = CurrentDraggedCard().transform.position;
        return Vector2.Distance(cardPosition, battleSlotPosition);
    }

}
