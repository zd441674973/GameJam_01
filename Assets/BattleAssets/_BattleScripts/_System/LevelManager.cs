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


    [SerializeField] Transform _playerCard;
    [SerializeField] Transform _EnemyCard;


    [SerializeField] List<Transform> _playerSlots;
    [SerializeField] List<bool> _isPlayerSlotsEmpty;

    [SerializeField] int _playerHandCardMax;


    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] Transform _battleArea;




    void Start()
    {
        DrawCard();
        EnemyDrawCard();

        TurnSystem.Instance.OnTurnChanged += UpdatePlayerMaxHandCardCount;
        TurnSystem.Instance.OnTurnChanged += DrawCard;
        TurnSystem.Instance.OnTurnChanged += EnemyDrawCard;




    }

    void Update()
    {

    }





    void DrawCard()
    {
        Debug.Log("Draw Card");

        foreach (var slot in _playerSlots)
        {
            // check if all slots have card
            int slotChildCount = slot.GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) continue;

            // Check if it is the first turn
            if (TurnSystem.Instance.GetTurnIndex() == 0)
            {
                if (HandCardCount() > 3) return;
            }
            else
            {
                if (HandCardCount() == _playerHandCardMax) return;
            }








            var playerDeck = CardDeckManager.Instance.GetPlayerDeck();
            Transform card = playerDeck[Random.Range(0, playerDeck.Count)];
            CardDeckManager.Instance.GenerateCard(card, slot);
            CardDeckManager.Instance.GetPlayerHandCard().Add(card);
            playerDeck.Remove(card);



        }


    }

    void EnemyDrawCard()
    {
        foreach (var slot in _enemySlots) CardDeckManager.Instance.GenerateCard(_EnemyCard, slot);
    }

    public void PlayerCardSlotCheck()
    {
        for (int i = 0; i < _playerSlots.Count; i++)
        {
            int slotChildCount = _playerSlots[i].GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) continue;
            _isPlayerSlotsEmpty[i] = true;
            CardDeckManager.Instance.GetPlayerHandCard()[i] = null;
        }
    }



    int HandCardCount() // Check how many cards in hand
    {
        int ValuableHandCards = 0;
        var handCardList = CardDeckManager.Instance.GetPlayerHandCard();
        for (int i = 0; i < handCardList.Count; i++)
        {
            if (!handCardList[i]) continue;
            ValuableHandCards++;
        }
        Debug.Log(ValuableHandCards);
        return ValuableHandCards;
    }

    void UpdatePlayerMaxHandCardCount()
    {
        //Player draw 2 cards each turn
        _playerHandCardMax = HandCardCount() + 2;
    }

    // void TurnCheck()
    // {
    //     switch (TurnSystem.Instance.GetTurnIndex())
    //     {
    //         case 0:
    //             if (HandCardCount() > 4) return;
    //             break;

    //         default:
    //             if (HandCardCount() > 8) return;
    //             break;
    //     }
    // }






    public Transform GetBattleArea() => _battleArea;
    // public int GetCurrentHandCardCount() => HandCardCount();
    // public void CurrentMaxHandCardCount(int count) => _playerHandCardMax = count;

}
