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

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentGainShield(_card.SheildToSelf);

            CardActionManager.Instance.OpponentDrawCard(_card.DrawCardFromLabrary);
        }
        else
        {
            CardActionManager.Instance.GainShield(_card.SheildToSelf);

            CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary);
        }

        // CardActionManager.Instance.GainShield(_card.SheildToSelf);

        // CardActionManager.Instance.DrawCard(_card.DrawCardFromLabrary);

        // Debug.Log($"B4_played; GainShield: {_card.SheildToSelf}; DarwCard: {_card.DrawCardFromLabrary};");
    }
}
