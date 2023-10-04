using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayedActionManager : MonoBehaviour
{

    void Start()
    {
        MouseDragManager.Instance.CardHasBeenPlayed += PlayerCardIsPlayed;
    }




    void PlayerCardIsPlayed()
    {

        Transform currentCard = MouseDragManager.Instance.CurrentPlayedCard();

        currentCard.gameObject.SetActive(false);

        UpdateHandCardCount();

        AddCardToDiscardPile(currentCard);

        UpdateEnergyBar(currentCard);

        PlayCardAction(currentCard);

        Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);

        UpdateHandCardCount();

    }

    void UpdateHandCardCount()
    {
        // int currentHand = LevelManager.Instance.GetCurrentHandCardCount();
        // LevelManager.Instance.SetCurrentHandCardCount(currentHand -= 1);

        LevelManager.Instance.PlayerEmptySlotCheck();
        LevelManager.Instance.UpdateHandCardCount();
    }

    void AddCardToDiscardPile(Transform transform)
    {
        CardDiscardPile.Instance.GetPlayerDiscardDeck().Add(transform);
        Transform discardPile = CardDiscardPile.Instance.GetComponent<Transform>();
        transform.SetParent(discardPile);
    }

    void UpdateEnergyBar(Transform transform)
    {
        if (transform.CompareTag("BrightCard"))
        {
            EnergySystem.Instance.EnergyBarCalculation("Bright", 1);
            EnergySystem.Instance.EnergyBarCalculation("Dark", -1);
        }

        if (transform.CompareTag("DarkCard"))
        {
            EnergySystem.Instance.EnergyBarCalculation("Bright", -1);
            EnergySystem.Instance.EnergyBarCalculation("Dark", 1);
        }
    }

    void PlayCardAction(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }


}
