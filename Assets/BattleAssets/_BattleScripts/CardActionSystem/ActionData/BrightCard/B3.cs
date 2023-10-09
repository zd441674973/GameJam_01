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


        int shieldValue = CardActionManager.Instance.GetShieldValue();

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(-shieldValue * _brightBonus);
            Debug.Log("B3_OpponentDamage_BF: " + -shieldValue * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(-shieldValue);
            Debug.Log("B3_OpponentDamage_DF: " + -shieldValue);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(-shieldValue);
            Debug.Log("B3_OpponentDamage_R: " + -shieldValue);
        }
    }
}
