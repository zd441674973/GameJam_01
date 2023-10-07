using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    public enum EnemyTurnState
    {
        Null,
        TurnStart,
        CardPlaying,
        CardPresenting,
        UpdatingCard,
        Waiting,
        TurnFinished,
    }

    [Header("NEED EDITING")]
    [SerializeField] Transform _battleSlot;
    [SerializeField] float _watingTime;



    [Header("DEBUG ONLY")]
    [SerializeField] bool _isActive;
    [SerializeField] int _cardIndex;
    [SerializeField] EnemyTurnState _enemyTurnState;
    Transform _currentCard;

    public event Action EnemyCardIsPlayed;


    public Transform GetCurrentEnemyCard() => _currentCard;


    public EnemyTurnState GetEnemyTurnState() => _enemyTurnState;
    public void SetEnemyTurnState(EnemyTurnState state) => _enemyTurnState = state;




    void Start()
    {
        _enemyTurnState = EnemyTurnState.Null;
        TurnSystem.Instance.OnPlayerTurnFinished += EnemyTurnBegin;
    }
    void Update()
    {
        CardIndexLimitCheck(EnemyHandCard());

        HandCardCountCheck();

        //Test();

        if (TurnSystem.Instance.IsPlayerTurn()) return;
        if (!_isActive) return;

        switch (_enemyTurnState)
        {
            case EnemyTurnState.TurnStart:
                EnemyStateTransitTo(EnemyTurnState.CardPlaying);
                break;


            case EnemyTurnState.CardPlaying:
                BattleUIManager.Instance.SetEnemyTurnStartUI(false);
                MultipleCardMovement(EnemyHandCard());
                break;


            case EnemyTurnState.CardPresenting:
                EnemyStateTransitTo(EnemyTurnState.UpdatingCard);
                break;


            case EnemyTurnState.UpdatingCard:
                EnemyUpdatingCard();
                StartTimer(_watingTime);
                _enemyTurnState = EnemyTurnState.Waiting;
                break;


            case EnemyTurnState.Waiting:
                EnemyStateTransitTo(EnemyTurnState.CardPlaying);
                break;


            case EnemyTurnState.TurnFinished:
                TurnSystem.Instance.NextTurn();
                _isActive = false;
                break;

        }

    }

    void StartTimer(float timer) => CustomTimer.Instance.WaitforTime(timer);
    bool IsTimesUp() => CustomTimer.Instance.TimesUp();
    List<Transform> EnemyHandCard() => CardDeckManager.Instance.GetEnemyHandCard();
    void CardIndexLimitCheck(List<Transform> cardlist)
    {
        if (_cardIndex > cardlist.Count - 1)
        {
            _cardIndex = 0;
            _enemyTurnState = EnemyTurnState.TurnFinished;
        }
    }

    void HandCardCountCheck()
    {
        LevelManager.Instance.UpdateEnemyHandCardCount();
    }

    void EnemyTurnBegin()
    {
        _enemyTurnState = EnemyTurnState.TurnStart;
        BattleUIManager.Instance.SetEnemyTurnStartUI(true);
        StartTimer(_watingTime);
        _isActive = true;
    }


    void EnemyStateTransitTo(EnemyTurnState state)
    {
        if (IsTimesUp()) _enemyTurnState = state;
    }

    void EnemyUpdatingCard()
    {
        _currentCard.gameObject.SetActive(false);
        EnemyCardIsPlayed?.Invoke();
        if (IsDarwnCard(_currentCard)) _cardIndex = 0;
        else _cardIndex++;
    }

    bool IsDarwnCard(Transform card)
    {
        int slotChildCount = card.GetComponentsInChildren<BoxCollider2D>().Length;
        if (slotChildCount > 0) return true;
        else return false;
    }

    void MultipleCardMovement(List<Transform> cardlist)
    {
        var card = cardlist[_cardIndex];

        if (!card)
        {
            _cardIndex++;
            return;
        }

        CardMovement(card);
    }

    void CardMovement(Transform card)
    {
        var speed = 6f;
        var stopDistance = 0.1f;
        var distance = Vector2.Distance(_battleSlot.transform.position, card.transform.position);
        var direction = (_battleSlot.transform.position - card.transform.position).normalized;

        _currentCard = card;

        if (distance > stopDistance)
        {
            card.transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            StartTimer(_watingTime);
            _enemyTurnState = EnemyTurnState.CardPresenting;
        }

    }




    // Option 1: Use countDownTime here
    // _countDownTime -= Time.deltaTime;

    // if (_countDownTime < 0)
    // {
    //     card.gameObject.SetActive(false);
    //     _countDownTime = _timer;
    //     _cardIndex++;
    //     return;
    // }

    // Option 2: Use Timer class to countDownTime
    // if (Timer.Instance.TimesUp())
    // {
    //     card.gameObject.SetActive(false);
    //     _cardIndex++;
    //     return;
    // }

}
