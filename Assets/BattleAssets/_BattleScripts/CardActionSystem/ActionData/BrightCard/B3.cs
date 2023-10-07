using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        // int shieldValue = CardActionManager.Instance.GetShieldValue();
        // CardActionManager.Instance.DamageOpponent(-shieldValue);

        // Debug.Log("B3_played; Damage: " + shieldValue);

        int shieldValue = CardActionManager.Instance.GetShieldValue();

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(-shieldValue * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            //int shieldValue = CardActionManager.Instance.GetShieldValue();
            CardActionManager.Instance.DamageSelf(-shieldValue);
        }
        else
        {
            //int shieldValue = CardActionManager.Instance.GetShieldValue();
            CardActionManager.Instance.DamageOpponent(-shieldValue);
        }
    }
}
