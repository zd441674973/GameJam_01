using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainShield(_card.SheildToSelf * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainShield(_card.SheildToSelf);
        }
        else
        {
            CardActionManager.Instance.GainShield(_card.SheildToSelf);
        }
    }
}
