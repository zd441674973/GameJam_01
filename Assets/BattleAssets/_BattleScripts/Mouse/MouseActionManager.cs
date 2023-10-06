using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MouseActionManager : MonoBehaviour
{


    public static MouseActionManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }



    public enum MouseInteractionState
    {
        Default,
        DrawFromOpponentHand,

    }


    [SerializeField] MouseInteractionState _mouseInteractionState;
    public MouseInteractionState MouseState { get { return _mouseInteractionState; } set { _mouseInteractionState = value; } }

    public event Action CardHasBeenPlayed;

    Vector2 _offset;
    Vector2 _cardSlotPosition;

    int _drawFromOpponentHandCount;
    public int DrawFromOpponentHandCount { get { return _drawFromOpponentHandCount; } set { _drawFromOpponentHandCount = value; } }


    RaycastHit2D CurrentDraggedCard() => MouseToWorld.Instance.GetMouseRaycastHit2D();
    public Transform CurrentPlayedCard() => CurrentDraggedCard().transform;
    bool IsPlayerCard() => CurrentPlayedCard().GetComponent<CardData>().IsInPlayerHand;



    void Start()
    {
        _mouseInteractionState = MouseInteractionState.Default;
        Debug.Log("MouseState: Default");

        CardActionManager.Instance.DrawFromOpponentHand += DrawFromOpponentHandEvent;
    }


    void Update()
    {
        if (!TurnSystem.Instance.IsPlayerTurn()) return;

        switch (_mouseInteractionState)
        {
            case MouseInteractionState.Default:

                BattleUIManager.Instance.SetEndTurnButtonFunction(true);

                DragCard();

                break;

            case MouseInteractionState.DrawFromOpponentHand:

                BattleUIManager.Instance.SetDrawCardFromEnemyHandUIText(_drawFromOpponentHandCount);

                BattleUIManager.Instance.SetEndTurnButtonFunction(false);

                if (CurrentDraggedCard()) if (IsPlayerCard()) return;

                SelectOpponentHandCard();

                break;

        }

    }










    void DragCard()
    {
        if (CurrentDraggedCard()) if (!IsPlayerCard()) return;
        GetCardOffset();
        Dragging();
        ResetDrag();
    }

    void DrawFromOpponentHandEvent()
    {
        BattleUIManager.Instance.SetDrawCardFromEnemyHandUI(true);
        _mouseInteractionState = MouseInteractionState.DrawFromOpponentHand;
    }
    void SelectOpponentHandCard()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CurrentDraggedCard()) return;


        Debug.Log(CurrentPlayedCard());

        LevelManager.Instance.PlayerDrawFromEnemyHand(CurrentPlayedCard());

        _drawFromOpponentHandCount -= 1;
        if (_drawFromOpponentHandCount > 0) return;

        SkipDrawFromOpponentHandState();
    }

    public void SkipDrawFromOpponentHandState()
    {
        BattleUIManager.Instance.SetDrawCardFromEnemyHandUI(false);
        _mouseInteractionState = MouseInteractionState.Default;
    }


    #region DragCardFunction

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
        Vector3 cardPosition = new Vector3((mousePosition - _offset).x, (mousePosition - _offset).y, -5f); // -5f will bring the card to very front
        CurrentDraggedCard().transform.position = cardPosition;

    }

    void ResetDrag()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        if (!CurrentDraggedCard()) return;

        float battleSlotSnapDistance = 1.5f;

        if (Distance() < battleSlotSnapDistance)
        {
            //PlayerCardIsPlayed();

            CardHasBeenPlayed?.Invoke();

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

    #endregion








    // void PlayerCardIsPlayed()
    // {

    //     Transform currentCard = CurrentDraggedCard().transform;

    //     UpdateEnergyBar(currentCard);

    //     AddCardToDiscardPile(currentCard);

    //     //PlayeCardAction(currentCard);
    //     CardPlayed(currentCard);

    //     currentCard.gameObject.SetActive(false);


    //     Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);


    // }

    // void AddCardToDiscardPile(Transform transform) => CardDeckManager.Instance.GetPlayerDiscardDeck().Add(transform);
    // void UpdateEnergyBar(Transform transform)
    // {
    //     if (transform.CompareTag("BrightCard"))
    //     {
    //         EnergySystem.Instance.EnergyBarCalculation("Bright", 1);
    //         EnergySystem.Instance.EnergyBarCalculation("Dark", -1);
    //     }

    //     if (transform.CompareTag("DarkCard"))
    //     {
    //         EnergySystem.Instance.EnergyBarCalculation("Bright", -1);
    //         EnergySystem.Instance.EnergyBarCalculation("Dark", 1);
    //     }
    // }

    // // void PlayeCardAction(Transform transform)
    // // {
    // //     CardAction cardAction = transform.GetComponent<CardAction>();
    // //     cardAction.CardIsPlayed = true;
    // // }

    // void CardPlayed(Transform card)
    // {
    //     CardAction cardAction = card.GetComponent<CardAction>();
    //     cardAction.GetTakeAction();
    // }


}
