using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D21 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int damage = Random.Range(_card.HealthToOpponentMax, _card.HealthToOpponentMin);
        int shieldValue = CardActionManager.Instance.GetOpponentShieldValue();
        int difference = damage + shieldValue;
        if (difference >= 0) difference = 0;

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(damage);
            CardActionManager.Instance.OpponentGainHealth(-difference);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(damage * _brightBonus);
            CardActionManager.Instance.GainHealth(-difference * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(damage);
            CardActionManager.Instance.GainHealth(-difference);
        }
    }
}
