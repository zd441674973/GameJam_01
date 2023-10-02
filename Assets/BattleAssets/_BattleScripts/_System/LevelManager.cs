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




    [Header("ENEMY DATA")]
    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] List<bool> _isEnemySlotsEmpty;
    [SerializeField] int _enemyHandCardMax;






    [Header("GENERAL DATA")]
    [SerializeField] Transform _battleArea;




    void Start()
    {
        PlayerDrawCard();
        EnemyDrawCard();


        TurnSystem.Instance.OnEntireTurnChanged += UpdatePlayerMaxHandCardCount;
        TurnSystem.Instance.OnEntireTurnChanged += PlayerDrawCard;
        TurnSystem.Instance.OnEntireTurnChanged += EnemyDrawCard;





    }

    void Update()
    {
        SlotEmptnessCheck(_playerSlots, _isPlayerSlotsEmpty, CardDeckManager.Instance.GetPlayerHandCard());
        SlotEmptnessCheck(_enemySlots, _isEnemySlotsEmpty, CardDeckManager.Instance.GetEnemyHandCard());

    }





    void PlayerDrawCard()
    {
        Debug.Log("PlayerDrawCard: Draw Card");

        for (int i = 0; i < _playerSlots.Count; i++)
        {
            int slotChildCount = _playerSlots[i].GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) continue;

            if (TurnSystem.Instance.GetTurnIndex() == 0)
            {
                if (PlayerHandCardCount() > 3) return;
            }
            else
            {
                if (PlayerHandCardCount() == _playerHandCardMax) return;
            }

            var playerDeck = CardDeckManager.Instance.GetPlayerDeck();
            Transform card = playerDeck[Random.Range(0, playerDeck.Count)];
            CardDeckManager.Instance.GenerateCard(card, _playerSlots[i]);
            CardDeckManager.Instance.GetPlayerHandCard()[i] = card;
            playerDeck.Remove(card);
        }


    }

    void EnemyDrawCard()
    {
        for (int i = 0; i < _enemySlots.Count; i++)
        {
            int slotChildCount = _enemySlots[i].GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) continue;

            var enemyDeck = CardDeckManager.Instance.GetEnemyDeck();
            Transform card = enemyDeck[Random.Range(0, enemyDeck.Count)];
            CardDeckManager.Instance.GenerateCard(card, _enemySlots[i]);
            CardDeckManager.Instance.GetEnemyHandCard()[i] = card;
            enemyDeck.Remove(card);
        }
    }




    void SlotEmptnessCheck(List<Transform> slot, List<bool> isEmptySlot, List<Transform> handCard)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            int slotChildCount = slot[i].GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) continue;
            isEmptySlot[i] = true;
            handCard[i] = null;
        }
    }

    int PlayerHandCardCount() // Check how many cards in player hand
    {
        int avaliableHandCards = 0;
        var handCardList = CardDeckManager.Instance.GetPlayerHandCard();
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
        _playerHandCardMax = PlayerHandCardCount() + 2;
    }






    public Transform GetBattleArea() => _battleArea;
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

}
