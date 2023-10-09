using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B7 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int currentBrightEnergy = CardActionManager.Instance.GetCurrentBrightEnergy();


        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(-currentBrightEnergy * _brightBonus);
            CardActionManager.Instance.DrawOpponentDeck(_card.DrawCardFromEnemyLabrary * _brightBonus);

            Debug.Log("B7_DamageOpponent_BF: " + -currentBrightEnergy * _brightBonus);
            Debug.Log("B7_DrawOpponentDeck_BF: " + _card.DrawCardFromEnemyLabrary * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(-currentBrightEnergy);
            CardActionManager.Instance.DrawCard(_card.DrawCardFromEnemyLabrary);

            Debug.Log("B7_DamageOpponent_DF: " + -currentBrightEnergy);
            Debug.Log("B7_DrawOpponentDeck_DF: " + _card.DrawCardFromEnemyLabrary);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(-currentBrightEnergy);
            CardActionManager.Instance.DrawOpponentDeck(_card.DrawCardFromEnemyLabrary);

            Debug.Log("B7_DamageOpponent_R: " + -currentBrightEnergy);
            Debug.Log("B7_DrawOpponentDeck_R: " + _card.DrawCardFromEnemyLabrary);

        }
    }
}
