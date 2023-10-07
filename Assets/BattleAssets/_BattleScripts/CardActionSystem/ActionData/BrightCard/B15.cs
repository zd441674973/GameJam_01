using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B15 : CardAction
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
            CardActionManager.Instance.GainHealth(currentBrightEnergy * 2 * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainHealth(currentBrightEnergy * 2);
        }
        else
        {
            CardActionManager.Instance.GainHealth(currentBrightEnergy * 2);
        }

    }
}
