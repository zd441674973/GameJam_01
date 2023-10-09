using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B0 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void TakeAction()
    {
        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(_card.HealthToOpponentMin * _brightBonus);
            Debug.Log("B0_Dealdamage_BF: " + _card.HealthToOpponentMin * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(_card.HealthToOpponentMin);
            Debug.Log("B0_DealdamageToOpponent_DF: " + _card.HealthToOpponentMin);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(_card.HealthToOpponentMin);
            Debug.Log("B0_Dealdamage_R: " + _card.HealthToOpponentMin);
        }
    }
}
