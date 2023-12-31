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

        DealCardPlayedExtraDamage(currentCard);

        UpdateHandCardCount();

        UpdateEnergyBar(currentCard);

        DiscardCard(currentCard);

        PlayCardAction(currentCard);


        // Add your play music/animation/effects function here

        PlayCardMusic(currentCard.GetComponent<CardData>().GetCard().CardName);

        // Use this 2 functions to wait for certian amout of time to excute the rest methods.
        /*
        CustomTimer.Instance.WaitforTime(float time);
        bool condition = CustomTimer.Instance.TimesUp();
        */



        UpdateHandCardCount();

        CheckDiscardedCardType(currentCard);

        Debug.Log("CardIsPlayed: The Card has been played : " + currentCard.name);
    }

    void UpdateHandCardCount() => LevelManager.Instance.UpdatePlayerHandCardCount();
    void DiscardCard(Transform card) => LevelManager.Instance.PlayerDiscardCard(card);
    void CheckDiscardedCardType(Transform card) => LevelManager.Instance.CheckOrigianlAttributes(card);
    void DealCardPlayedExtraDamage(Transform transform) => CardActionManager.Instance.DealCardPlayedExtraDamage(transform);

    void UpdateEnergyBar(Transform transform)
    {

        CardData cardData = transform.GetComponent<CardData>();
        if (cardData.IsBrightCard)
        {
            EnergySystem.Instance.GainBrightEnergy(1, false);
        }
        if (!cardData.IsBrightCard)
        {
            EnergySystem.Instance.GainDarkEnergy(1, false);
        }
    }

    void PlayCardAction(Transform card)
    {
        CardAction cardAction = card.GetComponent<CardAction>();
        cardAction.GetTakeAction();
    }

    void PlayCardMusic(string cardname)
    {
        MusicMgr.GetInstance().PlaySound("BattleSoundEffect/" + cardname, false);
    }

}
