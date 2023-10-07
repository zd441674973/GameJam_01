using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D22 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int damage1 = Random.Range(_card.HealthToOpponentMax, _card.HealthToOpponentMin);
        Debug.Log("D22_Damage1: " + damage1);
        int damage2 = Random.Range(_card.HealthToOpponentMax, _card.HealthToOpponentMin);
        Debug.Log("D22_Damage2: " + damage1);
        int totalDamage = damage1 + damage2;

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(totalDamage);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(totalDamage * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(totalDamage);
        }
    }
}
