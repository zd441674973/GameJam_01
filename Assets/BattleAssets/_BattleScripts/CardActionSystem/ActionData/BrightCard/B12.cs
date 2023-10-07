using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B12 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int currentBrightEnergy = CardActionManager.Instance.GetCurrentBrightEnergy();

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DestoryOpponentCard(currentBrightEnergy * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentDestoryCard(currentBrightEnergy);
        }
        else
        {
            CardActionManager.Instance.DestoryOpponentCard(currentBrightEnergy);
        }

    }
}
