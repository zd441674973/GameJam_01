using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{

    [Header("Card Attributes")]
    [SerializeField] bool _isBrightCard;
    public bool IsBrightCard { get { return _isBrightCard; } set { _isBrightCard = value; } }
    // [SerializeField] Transform _brightCard;
    // [SerializeField] Transform _darkCard;

    public Image cardImage;

    [SerializeField] Transform _attributeSwithCard;
    [SerializeField] bool _isAttributeSwitched;
    public bool IsAttributeSwitched { get { return _isAttributeSwitched; } set { _isAttributeSwitched = value; } }


    [Header("Card Info")]
    [SerializeField] bool _isInPlayerHand;
    public bool IsInPlayerHand
    {
        get { return _isInPlayerHand; }
        set { _isInPlayerHand = value; }
    }

    [SerializeField] bool _isPlayerCard;
    public bool IsPlayerCard { get { return _isPlayerCard; } }

    [SerializeField] bool _isCausingDamageCard;
    public bool IsCausingDamageCard { get { return _isCausingDamageCard; } }

    [SerializeField] int _cardID;
    [SerializeField] TextMeshProUGUI _cardName;
    [SerializeField] TextMeshProUGUI _cardDescription;




    Card card;

    public Card GetCard() => card;









    void Start()
    {
        card = GameDataControl.GetInstance().GetCardInfo(_cardID);
        _cardName.text = card.CardName;
        _cardDescription.text = card.Description;
        cardImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + card.CardName);
    }

    void Update()
    {
        AttributeUpdate(_isAttributeSwitched);
        UpdateCardSize(_isInPlayerHand);


    }

    void AttributeUpdate(bool condition)
    {
        if (condition)
        {
            _attributeSwithCard.gameObject.SetActive(true);
        }
        else
        {
            _attributeSwithCard.gameObject.SetActive(false);
        }
    }

    void UpdateCardSize(bool condition)
    {
        if (condition) transform.localScale = new Vector3(1, 1, 1);
        if (!condition) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
