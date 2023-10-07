using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B13 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainBrightEnergy(2 * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainDarkEnergy(2);
        }
        else
        {
            CardActionManager.Instance.GainBrightEnergy(2);
        }

    }
}
