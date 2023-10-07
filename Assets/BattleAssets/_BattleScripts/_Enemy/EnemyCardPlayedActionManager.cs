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



    void Start()
    {
        EnemyController.Instance.EnemyCardIsPlayed += EnemyCardIsPlayedEvent;
    }

    void EnemyCardIsPlayedEvent()
    {
        Transform currentCard = EnemyController.Instance.GetCurrentEnemyCard();

        DiscardCard(currentCard);

        PlayCardAction(currentCard);





    }

    void DiscardCard(Transform card) => LevelManager.Instance.EnemyDiscardCard(card);

    // void AddCardToDiscardPile(Transform transform)
    // {
    //     CardDiscardPile.Instance.GetEnemyDiscardDeck().Add(transform);
    //     Transform discardPile = CardDiscardPile.Instance.GetComponent<Transform>();
    //     transform.SetParent(discardPile);
    // }

    void PlayCardAction(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }


}
