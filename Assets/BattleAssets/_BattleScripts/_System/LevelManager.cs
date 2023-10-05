using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }



    [Header("PLAYER DATA")]
    [SerializeField] List<Transform> _playerSlots;
    [SerializeField] List<bool> _isPlayerSlotsEmpty;
    [SerializeField] int _playerHandCardMax;
    int _playerHandLimit = 8;
    [SerializeField] int _handCardCount;
    [SerializeField] int _drawCardCount;




    [Header("ENEMY DATA")]
    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] List<bool> _isEnemySlotsEmpty;
    [SerializeField] int _enemyHandCardMax;






    [Header("GENERAL DATA")]
    [SerializeField] Transform _battleArea;



    public int PlayerMaxHandCard { get { return _playerHandCardMax; } set { _playerHandCardMax = value; } }
    public int GetCurrentHandCardCount() => _handCardCount;
    public void SetCurrentHandCardCount(int value) => _handCardCount = value;
    public Transform GetBattleArea() => _battleArea;



    void Start()
    {
        // PlayerDrawCard();
        // EnemyDrawCard();

        StartGameMaxHandCard();
        DrawCard();
        PlayerEmptySlotCheck();
        UpdateHandCardCount();


        TurnSystem.Instance.OnEnemyTurnFinished += OnEnemyTurnFinishedEvent;

    }

    void Update()
    {
        UpdatePlayerHandCardCount();

        // SlotEmptnessCheck(_playerSlots, _isPlayerSlotsEmpty, PlayerHandCard());
        // SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, EnemyHandCard());

        CardOwnerCheck(_playerSlots);
        CardOwnerCheck(_enemySlots);

        // _handCardCount = PlayerHandCardCount();
    }


    List<Transform> PlayerDeck() => CardDeckManager.Instance.GetPlayerDeck();
    List<Transform> PlayerHandCard() => CardDeckManager.Instance.GetPlayerHandCard();

    List<Transform> EnemyDeck() => CardDeckManager.Instance.GetEnemyDeck();
    List<Transform> EnemyHandCard() => CardDeckManager.Instance.GetEnemyHandCard();


    void DrawCard()
    {
        DrawCardLogic(_playerSlots, PlayerDeck(), PlayerHandCard(), _isPlayerSlotsEmpty, _playerHandCardMax);

        DrawCardLogic(_enemySlots, EnemyDeck(), EnemyHandCard(), _isEnemySlotsEmpty, _enemyHandCardMax);
    }

    void OnEnemyTurnFinishedEvent()
    {
        PlayerEmptySlotCheck();
        UpdateHandCardCount();
        UpdatePlayerMaxHandCardCount();
        DrawCard();
    }



    public void PlayerDrawCard(int maxCardDrawnCount) => DrawCardLogic(_playerSlots, PlayerDeck(), PlayerHandCard(), _isPlayerSlotsEmpty, maxCardDrawnCount);
    public void EnemyDrawCard(int maxCardDrawnCount) => DrawCardLogic(_enemySlots, EnemyDeck(), EnemyHandCard(), _isEnemySlotsEmpty, maxCardDrawnCount);
    public void PlayerEmptySlotCheck() => SlotEmptnessCheck(_playerSlots, _isPlayerSlotsEmpty, PlayerHandCard());
    public void EnemyEmptySlotCheck() => SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, EnemyHandCard());
    public void UpdateHandCardCount() => _handCardCount = PlayerHandCardCount();
    public void UpdatePlayerHandCardCount()
    {
        PlayerEmptySlotCheck();
        UpdateHandCardCount();
    }





    int SlotChildCount(List<Transform> slotList, int index)
    {
        int slotChildCount = slotList[index].GetComponentsInChildren<BoxCollider2D>().Length;
        return slotChildCount;
    }
    void DrawCardLogic(List<Transform> slotList, List<Transform> deckList, List<Transform> handCardList, List<bool> isEmptySlotList, int maxCardDrawnCount)
    {
        Debug.Log("DRAW CARD: Draw card");

        for (int i = 0; i < slotList.Count; i++)
        {
            int slotChildCount = SlotChildCount(slotList, i);
            if (slotChildCount > 0) continue;

            if (slotList == _playerSlots)
            {
                if (PlayerHandCardCount() == maxCardDrawnCount) return;
            }

            Transform card = deckList[Random.Range(0, deckList.Count)];
            CardDeckManager.Instance.GenerateCard(card, slotList[i]);

            var playerCard = slotList[i].GetComponentInChildren<BoxCollider2D>().transform;
            handCardList[i] = playerCard;

            isEmptySlotList[i] = false;

            deckList.Remove(card);
        }
    }
    void SlotEmptnessCheck(List<Transform> slot, List<bool> isEmptySlot, List<Transform> handCard)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            int slotChildCount = SlotChildCount(slot, i);
            if (slotChildCount > 0) continue;

            isEmptySlot[i] = true;
            handCard[i] = null;
        }
    }

    int PlayerHandCardCount() // Check how many cards in player hand
    {
        int avaliableHandCards = 0;
        var handCardList = PlayerHandCard();
        for (int i = 0; i < handCardList.Count; i++)
        {
            if (!handCardList[i]) continue;
            avaliableHandCards++;
        }
        return avaliableHandCards;
    }

    void UpdatePlayerMaxHandCardCount()
    {
        //Player draw 2 cards each turn
        _playerHandCardMax = PlayerHandCardCount() + _drawCardCount;
        if (_playerHandCardMax > _playerHandLimit) _playerHandCardMax = _playerHandLimit;


    }

    void StartGameMaxHandCard()
    {
        if (TurnSystem.Instance.GetTurnIndex() == 0) _playerHandCardMax = 4;
    }


    void CardOwnerCheck(List<Transform> slotList)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            int slotChildCount = SlotChildCount(slotList, i);
            if (slotChildCount < 1) continue;

            Transform card = slotList[i].GetComponentInChildren<BoxCollider2D>().transform;

            if (slotList == _playerSlots) card.GetComponent<CardData>().IsInPlayerHand = true;
            if (slotList == _enemySlots) card.GetComponent<CardData>().IsInPlayerHand = false;
        }
    }





    // public int GetCurrentHandCardCount() => HandCardCount();
    // public void CurrentMaxHandCardCount(int count) => _playerHandCardMax = count;


    // void PlayerCardSlotCheck()
    // {
    //     for (int i = 0; i < _playerSlots.Count; i++)
    //     {
    //         int slotChildCount = _playerSlots[i].GetComponentsInChildren<BoxCollider2D>().Length;
    //         if (slotChildCount > 0) continue;
    //         _isPlayerSlotsEmpty[i] = true;
    //         CardDeckManager.Instance.GetPlayerHandCard()[i] = null;
    //     }
    // }


    // void PlayerDrawCard()
    // {

    //     for (int i = 0; i < _playerSlots.Count; i++)
    //     {
    //         int slotChildCount = SlotChildCount(_playerSlots, i);
    //         if (slotChildCount > 0) continue;

    //         if (TurnSystem.Instance.GetTurnIndex() == 0)
    //         {
    //             if (PlayerHandCardCount() > 3) return;
    //         }
    //         else
    //         {
    //             if (PlayerHandCardCount() == _playerHandCardMax) return;
    //         }

    //         var playerDeck = CardDeckManager.Instance.GetPlayerDeck();
    //         Transform card = playerDeck[Random.Range(0, playerDeck.Count)];
    //         CardDeckManager.Instance.GenerateCard(card, _playerSlots[i]);

    //         var playerCard = _playerSlots[i].GetComponentInChildren<BoxCollider2D>().transform;
    //         CardDeckManager.Instance.GetPlayerHandCard()[i] = playerCard;

    //         _isPlayerSlotsEmpty[i] = false;

    //         playerDeck.Remove(card);
    //     }


    // }

    // void EnemyDrawCard()
    // {
    //     for (int i = 0; i < _enemySlots.Count; i++)
    //     {
    //         int slotChildCount = SlotChildCount(_enemySlots, i);
    //         if (slotChildCount > 0) continue;

    //         var enemyDeck = CardDeckManager.Instance.GetEnemyDeck();
    //         Transform card = enemyDeck[Random.Range(0, enemyDeck.Count)];
    //         CardDeckManager.Instance.GenerateCard(card, _enemySlots[i]);

    //         var enemyCard = _enemySlots[i].GetComponentInChildren<BoxCollider2D>().transform;
    //         CardDeckManager.Instance.GetEnemyHandCard()[i] = enemyCard;

    //         _isEnemySlotsEmpty[i] = false;

    //         enemyDeck.Remove(card);
    //     }
    // }

}
