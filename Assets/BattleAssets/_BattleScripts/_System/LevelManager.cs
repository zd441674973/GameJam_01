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
    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] Transform _battleArea;




    void Start()
    {
        DrawCard();

        TurnSystem.Instance.OnTurnChanged += DrawCard;




    }

    void Update()
    {



    }





    void DrawCard()
    {

        foreach (var slot in _playerSlots)
        {
            // check if all slots have card
            int slotChildCount = slot.GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount > 0) return;


            var playerDeck = CardDeckManager.Instance.GetPlayerDeck();
            Transform card = playerDeck[Random.Range(0, playerDeck.Count)];
            CardDeckManager.Instance.GenerateCard(card, slot);
            playerDeck.Remove(card);
            //AssignPlayerCardInHand(card);

        }

        foreach (var slot in _enemySlots) CardDeckManager.Instance.GenerateCard(_EnemyCard, slot);
    }

    public void PlayerCardSlotCheck()
    {
        for (int i = 0; i < _playerSlots.Count; i++)
        {
            int slotChildCount = _playerSlots[i].GetComponentsInChildren<BoxCollider2D>().Length;
            if (slotChildCount < 1) _isPlayerSlotsEmpty[i] = true;
        }
    }






    public Transform GetBattleArea() => _battleArea;

}
