using System;
using System.Collections;
using System.Collections.Generic;
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

    enum EnemyTurnState
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
    [SerializeField] Transform _turnStartText;



    [Header("DEBUG ONLY")]
    [SerializeField] bool _isActive;
    [SerializeField] int _cardIndex;
    [SerializeField] EnemyTurnState _enemyTurnState;
    Transform _currentCard;

    public event Action EnemyCardIsPlayed;

    public Transform GetCurrentEnemyCard() => _currentCard;



    void Start()
    {
        _enemyTurnState = EnemyTurnState.Null;
        TurnSystem.Instance.OnPlayerTurnFinished += EnemyTurnBegin;
    }
    void Update()
    {
        CardIndexLimitCheck(EnemyHandCard());
        //Test();

        if (TurnSystem.Instance.IsPlayerTurn()) return;
        if (!_isActive) return;

        switch (_enemyTurnState)
        {
            case EnemyTurnState.TurnStart:
                EnemyWaiting(EnemyTurnState.CardPlaying);
                break;
            case EnemyTurnState.CardPlaying:
                _turnStartText.gameObject.SetActive(false);
                MultipleCardMovement(EnemyHandCard());
                break;

            case EnemyTurnState.CardPresenting:
                EnemyWaiting(EnemyTurnState.UpdatingCard);
                break;

            case EnemyTurnState.UpdatingCard:
                EnemyUpdatingCard();
                StartTimer(_watingTime);
                _enemyTurnState = EnemyTurnState.Waiting;
                break;

            case EnemyTurnState.Waiting:
                EnemyWaiting(EnemyTurnState.CardPlaying);
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

    void EnemyTurnBegin()
    {
        _enemyTurnState = EnemyTurnState.TurnStart;
        _turnStartText.gameObject.SetActive(true);
        StartTimer(_watingTime);
        _isActive = true;
    }


    void EnemyWaiting(EnemyTurnState state)
    {
        if (IsTimesUp()) _enemyTurnState = state;
    }

    void EnemyUpdatingCard()
    {
        _currentCard.gameObject.SetActive(false);
        EnemyCardIsPlayed?.Invoke();
        _cardIndex++;
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
