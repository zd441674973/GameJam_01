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
    [SerializeField] int _playerHandCardCount;
    [SerializeField] int _playerHandCardMax;
    int _playerHandLimit = 8;
    [SerializeField] int _playerEachTurnDrawCardCount;
    [SerializeField] bool _isPlayerDeckEmpty;




    [Header("ENEMY DATA")]
    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] List<bool> _isEnemySlotsEmpty;
    [SerializeField] int _enemyHandCardCount;
    [SerializeField] int _enemyHandCardMax;
    [SerializeField] bool _isEnemyDeckEmpty;






    [Header("GENERAL DATA")]
    [SerializeField] Transform _battleArea;
    [SerializeField] int _totalDarkDamageBuff;
    [SerializeField] int _darkEnergyBeginBuff;



    //[SerializeField] bool _isDeckShuffled;





    void Start()
    {
        // PlayerDrawCard();
        // EnemyDrawCard();
        _playerEachTurnDrawCardCount = 2;

        StartGameMaxHandCard();
        DrawCard();
        UpdatePlayerHandCardCount();
        UpdateEnemyHandCardCount();


        TurnSystem.Instance.OnEnemyTurnFinished += OnEnemyTurnFinishedEvent;

        _isPlayerDeckEmpty = false;
        _isEnemyDeckEmpty = false;


    }

    void Update()
    {
        UpdatePlayerHandCardCount();
        UpdateEnemyHandCardCount();

        // SlotEmptnessCheck(_playerSlots, _isPlayerSlotsEmpty, PlayerHandCard());
        // SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, EnemyHandCard());

        CardOwnerCheck(_playerSlots);
        CardOwnerCheck(_enemySlots);

        // _handCardCount = PlayerHandCardCount();

        DarkEnergyBonusCalculation();

        // UpdatePlayerEmptyList();
        // UpdateEnemyEmptyList();
    }


    List<Transform> PlayerDeck() => CardDeckManager.Instance.GetPlayerDeck();
    List<Transform> PlayerHandCard() => CardDeckManager.Instance.GetPlayerHandCard();
    List<Transform> PlayerDiscardPile() => CardDiscardPile.Instance.GetPlayerDiscardDeck();

    List<Transform> EnemyDeck() => CardDeckManager.Instance.GetEnemyDeck();
    List<Transform> EnemyHandCard() => CardDeckManager.Instance.GetEnemyHandCard();
    List<Transform> EnemyDiscardPile() => CardDiscardPile.Instance.GetEnemyDiscardDeck();



    public int GetPlayerCurrentHandCardCount() => _playerHandCardCount;
    public int GetEnemyCurrentHandCardCount() => _enemyHandCardCount;

    public Transform GetBattleArea() => _battleArea;
    public Transform GetDiscardPile() => CardDiscardPile.Instance.GetComponent<Transform>();


    public int GetDarkBeginBuff() => _darkEnergyBeginBuff;
    public int SetDarkBeginBuff(int value) => _darkEnergyBeginBuff = value;


    public int GetTotalDarkDamageBuff() => _totalDarkDamageBuff;









    public void PlayerDrawCard(int maxCardDrawnCount) => DrawCardLogic(_playerSlots, PlayerDeck(), PlayerHandCard(), _isPlayerSlotsEmpty, maxCardDrawnCount, PlayerDiscardPile());
    public void EnemyDrawCard(int maxCardDrawnCount) => DrawCardLogic(_enemySlots, EnemyDeck(), EnemyHandCard(), _isEnemySlotsEmpty, maxCardDrawnCount, EnemyDiscardPile());








    public void PlayerDrawFromEnemyDeck(int maxCardDrawnCount) => DrawCardLogic(_playerSlots, EnemyDeck(), PlayerHandCard(), _isPlayerSlotsEmpty, maxCardDrawnCount, EnemyDiscardPile());
    public void EnemyDrawFromPlayerDeck(int maxCardDrawnCount) => DrawCardLogic(_enemySlots, PlayerDeck(), EnemyHandCard(), _isEnemySlotsEmpty, maxCardDrawnCount, PlayerDiscardPile());








    public void PlayerDrawFromEnemyHand(Transform cardDrawn) => DrawFromOpponentHandCard(_playerSlots, cardDrawn, PlayerHandCard(), _isPlayerSlotsEmpty);
    public void EnemyDrawFromPlayerHandFunctionSet(int drawCardCount)
    {
        //Random player hand cards based on drawCardCount
        List<Transform> randomCards = RandomPlayerHandCardList(drawCardCount);


        //Check if there is enough empty slots in hand
        //Add to enemy hand
        if (CountEnemyHandEmptySlot() < randomCards.Count)
        {
            for (int i = 0; i < CountEnemyHandEmptySlot(); i++)
            {
                Transform card = randomCards[Random.Range(0, randomCards.Count)];
                EnemyDrawFromPlayerHand(card);
            }

        }
        else
        {
            foreach (Transform card in randomCards)
            {
                EnemyDrawFromPlayerHand(card);
            }
        }
    }
    void EnemyDrawFromPlayerHand(Transform cardDrawn) => DrawFromOpponentHandCard(_enemySlots, cardDrawn, EnemyHandCard(), _isEnemySlotsEmpty);
    List<Transform> RandomPlayerHandCardList(int drawCardCount)
    {
        List<Transform> randomCardList = new List<Transform>();

        List<Transform> playerCurrentHandCardList = new List<Transform>();
        for (int i = 0; i < PlayerHandCard().Count; i++)
        {
            if (!PlayerHandCard()[i]) continue;
            playerCurrentHandCardList.Add(PlayerHandCard()[i]);
        }

        // Player does not have enough card in hand to be drawn by enemy
        if (playerCurrentHandCardList.Count <= drawCardCount)
        {
            foreach (Transform card in playerCurrentHandCardList)
            {
                randomCardList.Add(card);
            }
        }
        else
        // Player have enough card in hand to be drawn by enemy
        {
            for (int i = 0; i < drawCardCount; i++)
            {
                int randomNumb = Random.Range(0, playerCurrentHandCardList.Count);
                Transform card = playerCurrentHandCardList[randomNumb];
                randomCardList.Add(card);
            }
        }
        return randomCardList;
    }

    int CountEnemyHandEmptySlot()
    {
        int count = 0;
        SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, EnemyHandCard());
        for (int i = 0; i < _isEnemySlotsEmpty.Count; i++)
        {
            if (!_isEnemySlotsEmpty[i]) continue;
            else count++;
        }
        return count;
    }







    public void PlayerDiscardCard(Transform card) => AddCardToDiscardPile(card);
    public void EnemyDiscardCard(Transform card) => AddCardToDiscardPile(card);






    public void PlayerDestoryEnemyHandCard(Transform card) => EnemyDiscardCard(card);
    public void EnemyDestoryPlayerHandCard(int destoryCardCount)
    {
        List<Transform> randomCards = RandomPlayerHandCardList(destoryCardCount);

        Debug.Log("Destory cards: " + randomCards.Count);

        for (int i = 0; i < randomCards.Count; i++)
        {
            PlayerDiscardCard(randomCards[i]);
        }
    }







    public void EnemyDiscardMultipleRandomHandCard(int discardCardCount)
    {
        List<Transform> randomCardList = CurrentHandCardList(EnemyHandCard());

        for (int i = 0; i < discardCardCount; i++)
        {
            Transform randomDiscardCard = randomCardList[Random.Range(0, randomCardList.Count)];
            EnemyDiscardCard(randomDiscardCard);
        }
    }
    List<Transform> CurrentHandCardList(List<Transform> handCardList)
    {
        List<Transform> currentHandCardList = new List<Transform>();
        for (int i = 0; i < handCardList.Count; i++)
        {
            if (!handCardList[i]) continue;
            currentHandCardList.Add(handCardList[i]);
        }
        return currentHandCardList;
    }


    List<Transform> CurrentBrightHandCardList(List<Transform> handCardList)
    {
        List<Transform> brightCardList = new List<Transform>();
        foreach (Transform card in handCardList)
        {
            if (card.GetComponent<CardData>().IsBrightCard) brightCardList.Add(card);
        }
        return brightCardList;
    }
    List<Transform> CurrentDarkHandCardList(List<Transform> handCardList)
    {
        List<Transform> darkCardList = new List<Transform>();
        foreach (Transform card in handCardList)
        {
            if (!card.GetComponent<CardData>().IsBrightCard) darkCardList.Add(card);
        }
        return darkCardList;
    }









    public void DiscardPlayerCurrentBrightHandCard(out int discardCount)
    {
        List<Transform> cardList = CurrentBrightHandCardList(CurrentHandCardList(PlayerHandCard()));
        foreach (Transform card in cardList)
        {
            AddCardToDiscardPile(card);
        }
        discardCount = cardList.Count;
    }
    public void DiscardEnemyCurrentBrightHandCard(out int discardCount)
    {
        List<Transform> cardList = CurrentBrightHandCardList(CurrentHandCardList(EnemyHandCard()));
        foreach (Transform card in cardList)
        {
            AddCardToDiscardPile(card);
        }
        discardCount = cardList.Count;
    }
    public void DiscardPlayerCurrentDarkHandCard()
    {
        List<Transform> cardList = CurrentDarkHandCardList(CurrentHandCardList(PlayerHandCard()));
        foreach (Transform card in cardList)
        {
            AddCardToDiscardPile(card);
        }
    }
    public void DiscardEnemyCurrentDarkHandCard()
    {
        List<Transform> cardList = CurrentDarkHandCardList(CurrentHandCardList(EnemyHandCard()));
        foreach (Transform card in cardList)
        {
            AddCardToDiscardPile(card);
        }
    }












    public void SwitchCardAttribute(Transform card)
    {
        CardData cardData = card.GetComponent<CardData>();
        cardData.IsAttributeSwitched = !cardData.IsAttributeSwitched;
    }
    public void EnemySwitchCardAttribute(int switchCardCount)
    {
        List<Transform> randomCardList = CurrentHandCardList(EnemyHandCard());
        for (int i = 0; i < switchCardCount; i++)
        {
            Transform randomSwitchCard = randomCardList[Random.Range(0, randomCardList.Count)];
            SwitchCardAttribute(randomSwitchCard);
        }

    }

    public void CheckOrigianlAttributes(Transform card)
    {
        if (card.CompareTag("BrightCard")) card.GetComponent<CardData>().IsBrightCard = true;
        if (card.CompareTag("DarkCard")) card.GetComponent<CardData>().IsBrightCard = false;
    }



















    public void UpdatePlayerHandCardCount()
    {
        SlotEmptnessCheck(_playerSlots, _isPlayerSlotsEmpty, PlayerHandCard());
        _playerHandCardCount = HandCardCount(PlayerHandCard());
    }
    public void UpdateEnemyHandCardCount()
    {
        SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, EnemyHandCard());
        _enemyHandCardCount = HandCardCount(EnemyHandCard());
    }











    void DrawCard()
    {
        DrawCardLogic(_playerSlots, PlayerDeck(), PlayerHandCard(), _isPlayerSlotsEmpty, _playerHandCardMax, PlayerDiscardPile());

        DrawCardLogic(_enemySlots, EnemyDeck(), EnemyHandCard(), _isEnemySlotsEmpty, _enemyHandCardMax, EnemyDiscardPile());
    }

    void OnEnemyTurnFinishedEvent()
    {
        UpdatePlayerHandCardCount();
        UpdateEnemyHandCardCount();
        UpdatePlayerMaxHandCardCount();
        DrawCard();
    }



    int SlotChildCount(List<Transform> slotList, int index)
    {
        int slotChildCount = slotList[index].GetComponentsInChildren<BoxCollider2D>().Length;
        return slotChildCount;
    }
    void DrawCardLogic(List<Transform> slotList,
        List<Transform> deckList,
        List<Transform> handCardList,
        List<bool> isEmptySlotList,
        int maxCardDrawnCount,
        List<Transform> discardPile)
    {
        Debug.Log("DRAW CARD: Draw card");


        for (int i = 0; i < slotList.Count; i++)
        {
            int slotChildCount = SlotChildCount(slotList, i);
            if (slotChildCount > 0) continue;

            if (slotList == _playerSlots)
            {
                if (HandCardCount(PlayerHandCard()) == maxCardDrawnCount) return;
            }
            if (slotList == _enemySlots)
            {
                if (HandCardCount(EnemyHandCard()) == maxCardDrawnCount) return;
            }

            if (deckList.Count < 1)
            {
                ShuffleFromDiscardPileToDeck(discardPile, deckList);
            }

            // draw from the deck
            Transform card = deckList[Random.Range(0, deckList.Count)];

            card.SetParent(slotList[i]);
            card.position = slotList[i].position;
            discardPile.Remove(card);

            card.gameObject.SetActive(true);

            var drawnCard = slotList[i].GetComponentInChildren<BoxCollider2D>().transform;

            handCardList[i] = drawnCard;

            isEmptySlotList[i] = false;

            deckList.Remove(card);

        }
    }





    void ShuffleFromDiscardPileToDeck(List<Transform> discardPile, List<Transform> deckList)
    {
        foreach (var item in discardPile)
        {
            deckList.Add(item);
        }

        discardPile.Clear();
    }









    void DrawFromOpponentHandCard(List<Transform> selfSlotList, Transform drawnCard, List<Transform> selfHandCardList, List<bool> selfIsEmptySlotList)
    {

        for (int i = 0; i < selfSlotList.Count; i++)
        {
            int slotChildCount = SlotChildCount(selfSlotList, i);
            if (slotChildCount > 0) continue;

            Transform playerEmptySlot = selfSlotList[i];
            drawnCard.SetParent(playerEmptySlot);
            drawnCard.position = playerEmptySlot.position;
            selfHandCardList[i] = drawnCard;
            selfIsEmptySlotList[i] = false;
            break;
        }
    }

    void AddCardToDiscardPile(Transform card)
    {
        card.gameObject.SetActive(false);
        bool isPlayerCard = card.GetComponent<CardData>().IsPlayerCard;

        if (isPlayerCard) PlayerDiscardPile().Add(card);
        if (!isPlayerCard) EnemyDiscardPile().Add(card);

        card.SetParent(GetDiscardPile());

        UpdatePlayerHandCardCount();
        UpdateEnemyHandCardCount();

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

    int HandCardCount(List<Transform> handCardList)
    {
        int avaliableHandCards = 0;
        //var handCardList = PlayerHandCard();
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
        _playerHandCardMax = HandCardCount(PlayerHandCard()) + _playerEachTurnDrawCardCount;
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





    void DarkEnergyBonusCalculation()
    {
        int darkEnergy = EnergySystem.Instance.GetCurrentDarkEnergy();
        _totalDarkDamageBuff = darkEnergy + _darkEnergyBeginBuff;
    }


















    #region Ghost Code





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

    // int PlayerHandCardCount() // Check how many cards in player hand
    // {
    //     int avaliableHandCards = 0;
    //     var handCardList = PlayerHandCard();
    //     for (int i = 0; i < handCardList.Count; i++)
    //     {
    //         if (!handCardList[i]) continue;
    //         avaliableHandCards++;
    //     }
    //     return avaliableHandCards;
    // }

    // int HandCardCount(List<Transform> handCardList) // Check how many cards in player hand
    // {
    //     int avaliableHandCards = 0;
    //     for (int i = 0; i < handCardList.Count; i++)
    //     {
    //         if (!handCardList[i]) continue;
    //         avaliableHandCards++;
    //     }
    //     return avaliableHandCards;
    // }
    #endregion
}
