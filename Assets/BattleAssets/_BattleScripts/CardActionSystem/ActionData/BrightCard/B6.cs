using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B6 : CardAction
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
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainHealth(_card.HealthToSelfMin);
        }
        else
        {
            CardActionManager.Instance.GainHealth(_card.HealthToSelfMin);
        }


        // CardActionManager.Instance.GainShield(_card.SheildToSelf);

        // Debug.Log($"B6_played; GainShield: {_card.SheildToSelf}");
    }
}
