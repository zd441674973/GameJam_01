using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B10 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.PlayerDrawOpponentHand(_card.GetEnemyHandCard * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentDrawPlayerHand(_card.GetEnemyHandCard);
        }
        else
        {
            CardActionManager.Instance.PlayerDrawOpponentHand(_card.GetEnemyHandCard);
        }

    }
}
