using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B11 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {

        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DestoryOpponentCard(_card.DestroyEnemyHandCard * _brightBonus);
        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.OpponentDestoryCard(_card.GetEnemyHandCard);
        }
        else
        {
            CardActionManager.Instance.DestoryOpponentCard(_card.DestroyEnemyHandCard);
        }

    }
}
