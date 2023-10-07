using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int darkBonusCount = LevelManager.Instance.GetDarkBonus();
        int currentBrightEnergy = CardActionManager.Instance.GetCurrentBrightEnergy();

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(currentBrightEnergy * 2);
            CardActionManager.Instance.DamageSelf(darkBonusCount * 2);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(darkBonusCount * 2 * _brightBonus);
            CardActionManager.Instance.DamageSelf(currentBrightEnergy * 2 * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(darkBonusCount * 2);
            CardActionManager.Instance.DamageSelf(currentBrightEnergy * 2);
        }
    }
}
