using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D19 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int damageOpponent = Random.Range(_card.HealthToOpponentMax, _card.HealthToOpponentMin);
        int damageSelf = Random.Range(_card.HealthToSelfMax, _card.HealthToSelfMin);

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(damageSelf);
            CardActionManager.Instance.DamageSelf(damageOpponent);
            Debug.Log("D19_DamageOpponent_DF: " + damageSelf);
            Debug.Log("D19_DamageSelf_DF: " + damageOpponent);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(damageOpponent * _brightBonus);
            CardActionManager.Instance.DamageSelf(damageSelf * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(damageOpponent);
            CardActionManager.Instance.DamageSelf(damageSelf);

            Debug.Log("D19_DamageOpponent_R: " + damageSelf);
            Debug.Log("D19_DamageSelf_R: " + damageOpponent);
        }
    }
}
