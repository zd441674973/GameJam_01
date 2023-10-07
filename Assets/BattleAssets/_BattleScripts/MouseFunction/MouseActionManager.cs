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
        DestoryFromOpponentHand,
        DiscardPlayerHand,
        SwitchAttribute,



    }


    [SerializeField] MouseInteractionState _mouseInteractionState;
    public MouseInteractionState MouseState { get { return _mouseInteractionState; } set { _mouseInteractionState = value; } }

    public event Action CardHasBeenPlayed;

    Vector2 _offset;
    Vector2 _cardSlotPosition;

    int _drawFromOpponentHandCount;
    public int DrawFromOpponentHandCount { get { return _drawFromOpponentHandCount; } set { _drawFromOpponentHandCount = value; } }

    int _destoryFromOpponentHandCount;
    public int DestoryFromOpponentHandCount { get { return _destoryFromOpponentHandCount; } set { _destoryFromOpponentHandCount = value; } }

    int _discardPlayerHandCount;
    public int DiscardPlayerHandCount { get { return _discardPlayerHandCount; } set { _discardPlayerHandCount = value; } }


    RaycastHit2D CurrentDraggedCard() => MouseToWorld.Instance.GetMouseRaycastHit2D();
    public Transform CurrentPlayedCard() => CurrentDraggedCard().transform;
    bool IsPlayerCard() => CurrentPlayedCard().GetComponent<CardData>().IsInPlayerHand;



    void Start()
    {
        _mouseInteractionState = MouseInteractionState.Default;
        Debug.Log("MouseState: Default");


        CardActionManager.Instance.DrawOpponentHandEvent += DrawFromOpponentHandEvent;
        CardActionManager.Instance.DestoryOpponentCardEvent += DestoryFromOpponentHandEvent;
        CardActionManager.Instance.DiscardPlayerHandEvent += DiscardPlayerHandEvent;

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

                BattleUIManager.Instance.UpdateDrawCardFromEnemyHandUIText(_drawFromOpponentHandCount);

                BattleUIManager.Instance.SetEndTurnButtonFunction(false);

                if (CurrentDraggedCard()) if (IsPlayerCard()) return;

                SelectOpponentHandCard();

                break;



            case MouseInteractionState.DestoryFromOpponentHand:

                BattleUIManager.Instance.UpdateDestoryEnemyHandUIText(_destoryFromOpponentHandCount);

                BattleUIManager.Instance.SetEndTurnButtonFunction(false);

                if (CurrentDraggedCard()) if (IsPlayerCard()) return;

                DestoryOpponentHandCard();

                break;



            case MouseInteractionState.DiscardPlayerHand:

                BattleUIManager.Instance.UpdateDiscardPlayerHandUIText(_discardPlayerHandCount);

                BattleUIManager.Instance.SetEndTurnButtonFunction(false);

                if (CurrentDraggedCard()) if (!IsPlayerCard()) return;

                DiscardPlayerHandCard();


                break;



            case MouseInteractionState.SwitchAttribute:

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








    #region DrawFromOpponentHand

    void DrawFromOpponentHandEvent()
    {
        BattleUIManager.Instance.SetDrawCardFromEnemyHandUI(true);
        BattleUIManager.Instance.SetSkipButtonTransform(true);
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

        SkipCurrentState();
    }

    #endregion








    #region DestoryFromOpponentHand

    void DestoryFromOpponentHandEvent()
    {
        BattleUIManager.Instance.SetDestoryEnemyHandUI(true);
        BattleUIManager.Instance.SetSkipButtonTransform(true);
        _mouseInteractionState = MouseInteractionState.DestoryFromOpponentHand;
    }

    void DestoryOpponentHandCard()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CurrentDraggedCard()) return;


        Debug.Log(CurrentPlayedCard());

        LevelManager.Instance.PlayerDestoryEnemyHandCard(CurrentPlayedCard());

        _destoryFromOpponentHandCount -= 1;
        if (_destoryFromOpponentHandCount > 0) return;

        SkipCurrentState();
    }

    #endregion








    #region DiscardState

    void DiscardPlayerHandEvent()
    {
        BattleUIManager.Instance.SetDiscardPlayerHandUI(true);
        //BattleUIManager.Instance.SetSkipButtonTransform(true);
        _mouseInteractionState = MouseInteractionState.DiscardPlayerHand;
    }

    void DiscardPlayerHandCard()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CurrentDraggedCard()) return;


        Debug.Log(CurrentPlayedCard());

        LevelManager.Instance.PlayerDiscardCard(CurrentPlayedCard());

        _discardPlayerHandCount -= 1;
        if (_discardPlayerHandCount > 0) return;

        SkipCurrentState();
    }







    #endregion


    public void SkipCurrentState()
    {
        BattleUIManager.Instance.SetDrawCardFromEnemyHandUI(false);
        BattleUIManager.Instance.SetDestoryEnemyHandUI(false);
        BattleUIManager.Instance.SetDiscardPlayerHandUI(false);

        BattleUIManager.Instance.SetSkipButtonTransform(false);

        _mouseInteractionState = MouseInteractionState.Default;
    }


















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
