using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainShield(_card.SheildToSelf * _brightBonus);

            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary * _brightBonus);

            Debug.Log("B4_GainShield_BF: " + _card.SheildToSelf * _brightBonus);
            Debug.Log("B4_Drawcard_BF: " + _card.DrawCardFromLabrary * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainShield(_card.SheildToSelf);

            CardActionManager.Instance.OpponentDrawCard(_card.DrawCardFromLabrary);

            Debug.Log("B4_OpGainShield_DF: " + _card.SheildToSelf);
            Debug.Log("B4_OpDrawcard_DF: " + _card.DrawCardFromLabrary);
        }
        else
        {
            CardActionManager.Instance.GainShield(_card.SheildToSelf);

            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary);

            Debug.Log("B4_GainShield_R: " + _card.SheildToSelf);
            Debug.Log("B4_Drawcard_R: " + _card.DrawCardFromLabrary);
        }

    }
}
