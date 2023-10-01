using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MouseDrag : MonoBehaviour
{
    //public static event Action CardIsPlayed;

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
            PlayerCardIsPlayed();
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






    void PlayerCardIsPlayed()
    {
        Transform currentCard = CurrentDraggedCard().transform;

        UpdateEnergyBar(currentCard);
        AddCardToDiscardPile(currentCard);

        currentCard.gameObject.SetActive(false);

        //CardIsPlayed?.Invoke();

        Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);

        CardActionManager.Instance.CardIsPlayed = true;
    }

    void AddCardToDiscardPile(Transform transform) => CardDeckManager.Instance.GetPlayerDiscardDeck().Add(transform);
    void UpdateEnergyBar(Transform transform)
    {
        if (transform.CompareTag("BrightCard"))
        {
            EnergySystem.Instance.EnergyBarCalculation("Bright", 1);
            EnergySystem.Instance.EnergyBarCalculation("Dark", -1);
        }

        if (transform.CompareTag("DarkCard"))
        {
            EnergySystem.Instance.EnergyBarCalculation("Bright", -1);
            EnergySystem.Instance.EnergyBarCalculation("Dark", 1);
        }
    }


}
