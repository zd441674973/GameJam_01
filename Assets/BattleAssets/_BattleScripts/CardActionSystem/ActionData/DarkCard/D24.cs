using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D24 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int gainDarkEnergyValue = 2;

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainDarkEnergy(gainDarkEnergyValue);
            Debug.Log("D24_GainDarkEnergy_DF: " + gainDarkEnergyValue);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainDarkEnergy(gainDarkEnergyValue * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.GainDarkEnergy(gainDarkEnergyValue);
            Debug.Log("D24_GainDarkEnergy_R: " + gainDarkEnergyValue);
        }
    }
}
