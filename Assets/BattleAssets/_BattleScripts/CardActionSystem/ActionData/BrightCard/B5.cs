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
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainShield(shieldValue);
        }
        else
        {
            CardActionManager.Instance.GainShield(shieldValue);
        }
        
        // CardActionManager.Instance.GainShield(shieldValue);

        // Debug.Log($"B5_played; GainShield: {shieldValue}");
    }
}
