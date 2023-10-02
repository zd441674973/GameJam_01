using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardData : MonoBehaviour
{


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

    void Start()
    {
        Card card = GameDataControl.GetInstance().GetCardInfo(_cardID);
        _cardName.text = card.CardName;
        _cardDescription.text = card.Description;

    }

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
