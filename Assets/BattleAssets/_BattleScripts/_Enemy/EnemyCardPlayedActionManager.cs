using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardPlayedActionManager : MonoBehaviour
{
    public static EnemyCardPlayedActionManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    bool _isActionSuccess;
    public bool IsActionSuccess { get { return _isActionSuccess; } set { _isActionSuccess = value; } }

    // public event Action SkipCardEvent;




    void Start()
    {
        EnemyController.Instance.EnemyCardIsPlayed += EnemyCardIsPlayedEvent;
    }

    void EnemyCardIsPlayedEvent()
    {
        Transform currentCard = EnemyController.Instance.GetCurrentEnemyCard();

        DealCardPlayedExtraDamage(currentCard);

        DiscardCard(currentCard);

        PlayCardAction(currentCard);


        // Add your play music/animation/effects function here

        // Use this 2 functions to wait for certian amout of time to excute the rest methods.
        /*
        CustomTimer.Instance.WaitforTime(float time);
        bool condition = CustomTimer.Instance.TimesUp();
        */


        CheckDiscardedCardType(currentCard);

    }

    void DiscardCard(Transform card) => LevelManager.Instance.EnemyDiscardCard(card);
    void CheckDiscardedCardType(Transform card) => LevelManager.Instance.CheckOrigianlAttributes(card);
    void DealCardPlayedExtraDamage(Transform transform) => CardActionManager.Instance.DealCardPlayedExtraDamage(transform);

    void PlayCardAction(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }



    // if (!_isActionSuccess)
    // {
    //     Debug.Log("Action Failed");
    //     SkipCardEvent?.Invoke();
    //     return;
    // }

}
