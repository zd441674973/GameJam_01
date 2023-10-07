using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardPlayedActionManager : MonoBehaviour
{

    void Start()
    {
        MouseActionManager.Instance.CardHasBeenPlayed += PlayerCardIsPlayed;
    }




    void PlayerCardIsPlayed()
    {

        Transform currentCard = MouseActionManager.Instance.CurrentPlayedCard();

        currentCard.gameObject.SetActive(false);

        UpdateHandCardCount();

        DiscardCard(currentCard);

        UpdateEnergyBar(currentCard);

        PlayCardAction(currentCard);

        Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);

        UpdateHandCardCount();

    }

    void UpdateHandCardCount() => LevelManager.Instance.UpdatePlayerHandCardCount();
    void DiscardCard(Transform card) => LevelManager.Instance.PlayerDiscardCard(card);


    void UpdateEnergyBar(Transform transform)
    {
        // if (transform.CompareTag("BrightCard"))
        // {
        //     EnergySystem.Instance.EnergyBarCalculation("Bright", 1);
        //     EnergySystem.Instance.EnergyBarCalculation("Dark", -1);
        // }

        // if (transform.CompareTag("DarkCard"))
        // {
        //     EnergySystem.Instance.EnergyBarCalculation("Bright", -1);
        //     EnergySystem.Instance.EnergyBarCalculation("Dark", 1);
        // }

        CardData cardData = transform.GetComponent<CardData>();
        if (cardData.IsBrightCard)
        {
            // EnergySystem.Instance.EnergyBarCalculation("Bright", 1);
            // EnergySystem.Instance.EnergyBarCalculation("Dark", -1);
            EnergySystem.Instance.GainBrightEnergy(1);
        }
        if (!cardData.IsBrightCard)
        {
            // EnergySystem.Instance.EnergyBarCalculation("Bright", -1);
            // EnergySystem.Instance.EnergyBarCalculation("Dark", 1);
            EnergySystem.Instance.GainDarkEnergy(1);
        }




    }

    void PlayCardAction(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }


}
