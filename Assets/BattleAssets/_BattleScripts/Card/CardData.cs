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
    [SerializeField] Transform _brightCard;
    [SerializeField] Transform _darkCard;

    public Image cardImage;

    [Header("Card Info")]
    [SerializeField] bool _isInPlayerHand;
    public bool IsInPlayerHand
    {
        get { return _isInPlayerHand; }
        set { _isInPlayerHand = value; }
    }

    [SerializeField] bool _isPlayerCard;
    public bool IsPlayerCard { get { return _isPlayerCard; } }

    [SerializeField] int _cardID;
    [SerializeField] TextMeshProUGUI _cardName;
    [SerializeField] TextMeshProUGUI _cardDescription;
    Card card;

    void Start()
    {
        card = GameDataControl.GetInstance().GetCardInfo(_cardID);
        _cardName.text = card.CardName;
        _cardDescription.text = card.Description;
        cardImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + card.CardName);
    }

    void Update()
    {
        AttributeUpdate(_isBrightCard);
        UpdateCardSize(_isInPlayerHand);


    }

    void AttributeUpdate(bool condition)
    {
        if (condition)
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

    void UpdateCardSize(bool condition)
    {
        if (condition) transform.localScale = new Vector3(1, 1, 1);
        if (!condition) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
