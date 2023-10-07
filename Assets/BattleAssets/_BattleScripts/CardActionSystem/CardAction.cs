using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    //[SerializeField] protected bool _cardIsPlayed;
    // public bool CardIsPlayed
    // { get { return _cardIsPlayed; } set { _cardIsPlayed = value; } }
    protected CardData cardData;
    protected Card card;


    protected virtual void Start()
    {
        cardData = GetComponent<CardData>();
        card = cardData.GetCard();
    }

    protected abstract void TakeAction();

    public void GetTakeAction() => TakeAction();


}
