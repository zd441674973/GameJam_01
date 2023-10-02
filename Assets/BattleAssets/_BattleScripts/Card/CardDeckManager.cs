using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    public static CardDeckManager Instance;
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
    [SerializeField] List<Transform> _playerDeck;
    [SerializeField] List<Transform> _playerHandCard;
    [SerializeField] List<Transform> _playerDiscardPile;

    [Header("ENEMY DATA")]
    [SerializeField] List<Transform> _enemyDeck;
    [SerializeField] List<Transform> _enemyHandCard;
    [SerializeField] List<Transform> _enemyDiscardPile;


    void Start()
    {

    }










    public Transform GenerateCard(Transform card, Transform cardSlot) => Instantiate(card, cardSlot);
    
    public List<Transform> GetPlayerDeck() => _playerDeck;
    public List<Transform> GetPlayerHandCard() => _playerHandCard;
    public List<Transform> GetPlayerDiscardDeck() => _playerDiscardPile;

    public List<Transform> GetEnemyDeck() => _enemyDeck;
    public List<Transform> GetEnemyHandCard() => _enemyHandCard;
    public List<Transform> GetEnemyDiscardDeck() => _enemyDiscardPile;
}
