using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardData : MonoBehaviour
{

    [Header("Card Attributes")]
    [SerializeField] bool _isBrightCard;
    public bool IsBrightCard { get { return _isBrightCard; } set { _isBrightCard = value; } }
    [SerializeField] Transform _brightCard;
    [SerializeField] Transform _darkCard;



    [Header("Card Info")]
    [SerializeField] bool _isInPlayerHand;
    public bool IsInPlayerHand
    {
        get { return _isInPlayerHand; }
        set { _isInPlayerHand = value; }
    }

    [SerializeField] int _cardID;
    [SerializeField] TextMeshPro _cardName;
    [SerializeField] TextMeshPro _cardDescription;
    Card card;

    void Start()
    {
        card = GameDataControl.GetInstance().GetCardInfo(_cardID);
        //List<Card> cardList = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
        _cardName.text = card.CardName;
        _cardDescription.text = card.Description;
    }

    void Update()
    {
        AttributeUpdate();


    }

    void AttributeUpdate()
    {
        if (_isBrightCard)
        {
            _brightCard.gameObject.SetActive(true);
            _darkCard.gameObject.SetActive(false);
        }
        else
        {
            _brightCard.gameObject.SetActive(false);
            _darkCard.gameObject.SetActive(true);
        }
    }




    public Card GetCard() => card;










    // cardInfo
    // int CardID;
    // string CardName;
    // string Type;
    // int PlayerOwnedNumber;
    // int ElectricEnergyEfficiencyChange;
    // int MagicEnergyEfficiencyChange;
    // int HealthToOpponentMin;
    // int HealthToOpponentMax;
    // int NumberOfAttack;
    // int HealthToSelfMin;
    // int HealthToSelfMax;
    // int SheildToEnemy;
    // int SheildToSelf;
    // int DrawCardFromLabrary;
    // int DrawCardFromEnemyLabrary;
    // int GetEnemyHandCard;
    // int DestroyEnemyHandCard;
    // string Description;
    // string Rarity;





}
