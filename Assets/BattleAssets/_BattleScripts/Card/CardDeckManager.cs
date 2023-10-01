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

    //[SerializeField] Transform[] _playerDeck;
    [SerializeField] List<Transform> _playerDeck;
    [SerializeField] List<Transform> _playerCardInHand;
    [SerializeField] List<Transform> _playerDiscardPile;


    void Start()
    {

    }










    public Transform GenerateCard(Transform card, Transform cardSlot) => Instantiate(card, cardSlot);
    public List<Transform> GetPlayerDeck() => _playerDeck;
    public List<Transform> GetPlayerCardInHand() => _playerCardInHand;
    public List<Transform> GetPlayerDiscardDeck() => _playerDiscardPile;
}
