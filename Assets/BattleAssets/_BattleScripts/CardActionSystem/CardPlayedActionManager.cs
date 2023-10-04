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

        //UpdateMaxPlayerHandCard();

        UpdateEnergyBar(currentCard);

        AddCardToDiscardPile(currentCard);

        CardPlayed(currentCard);

        currentCard.gameObject.SetActive(false);

        Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);


    }

    void AddCardToDiscardPile(Transform transform) => CardDeckManager.Instance.GetPlayerDiscardDeck().Add(transform);

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

    void CardPlayed(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }

    void UpdateMaxPlayerHandCard() => LevelManager.Instance.PlayerMaxHandCard -= 1;
}
