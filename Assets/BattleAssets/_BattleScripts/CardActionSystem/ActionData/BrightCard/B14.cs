using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B14 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.AttributeSwitch(1 * _brightBonus);
            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentAttributeSwitch(1);
            CardActionManager.Instance.OpponentDrawCard(_card.DrawCardFromLabrary);
        }
        else
        {
            CardActionManager.Instance.AttributeSwitch(1);
            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary);
        }

    }
}
