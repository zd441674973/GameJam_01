using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B8 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int currentBrightEnergy = CardActionManager.Instance.GetCurrentBrightEnergy();
        int currentDarkEnergy = CardActionManager.Instance.GetCurrentDarkEnergy();


        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DrawCard(currentBrightEnergy * _brightBonus);
            CardActionManager.Instance.DiscardCard(currentDarkEnergy * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.PlayerDrawOpponentHand(currentBrightEnergy);
            CardActionManager.Instance.OpponentDiscardCard(currentDarkEnergy);
        }
        else
        {
            CardActionManager.Instance.DrawCard(currentBrightEnergy);
            CardActionManager.Instance.DiscardCard(currentDarkEnergy);
        }
    }
}
