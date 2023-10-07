using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class B2 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {


        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainHealth(_card.HealthToSelfMin * _brightBonus);

            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainHealth(_card.HealthToSelfMin);

            CardActionManager.Instance.OpponentDrawCard(_card.DrawCardFromLabrary);
        }
        else
        {
            CardActionManager.Instance.GainHealth(_card.HealthToSelfMin);

            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary);
        }
    }
}
