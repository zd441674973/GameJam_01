using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    //[SerializeField] protected bool _cardIsPlayed;
    // public bool CardIsPlayed
    // { get { return _cardIsPlayed; } set { _cardIsPlayed = value; } }
    protected CardData _cardData;
    protected Card _card;

    protected bool _isBrightEnergyFull;
    protected int _brightBonus; // bonus is 2

    protected bool _isDarkEnergyFull;





    protected virtual void Start()
    {
        _cardData = GetComponent<CardData>();
        _card = _cardData.GetCard();
        _brightBonus = 2;
    }
    protected virtual void Update()
    {
        _isBrightEnergyFull = EnergySystem.Instance.IsBrightEnergyFull();
        _isDarkEnergyFull = EnergySystem.Instance.IsDarkEnergyFull();

    }

    protected abstract void TakeAction();

    public void GetTakeAction() => TakeAction();


}
