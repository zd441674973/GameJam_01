using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B5 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int shieldValue = CardActionManager.Instance.GetShieldValue();

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainShield(shieldValue * _brightBonus);
            Debug.Log("B5_GainShield_BF: " + shieldValue * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainShield(shieldValue);
            Debug.Log("B5_OpGainShield_DF: " + shieldValue);
        }
        else
        {
            CardActionManager.Instance.GainShield(shieldValue);
            Debug.Log("B5_OpGainShield_R: " + shieldValue);
        }
    }
}
