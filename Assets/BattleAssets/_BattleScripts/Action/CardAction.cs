using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    [SerializeField] protected bool _cardIsPlayed;
    public bool CardIsPlayed
    { get { return _cardIsPlayed; } set { _cardIsPlayed = value; } }

    void Update()
    {
        if (!_cardIsPlayed) return;

        TakeAction();





        Debug.Log("Card is playing");
        _cardIsPlayed = false;

    }


    void TakeAction()
    {
        throw new System.NotImplementedException();
    }









}
