using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B16 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {

        }
        else
        {
            CardActionManager.Instance.DiscardBrightHandCard(out int discardCount);
            int darkBonusCount = LevelManager.Instance.GetTotalDarkDamageBuff();
            CardActionManager.Instance.DamageOpponent(-(darkBonusCount * discardCount));
        }

    }
}
