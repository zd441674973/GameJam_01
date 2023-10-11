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
        int darkBonusCount = LevelManager.Instance.GetTotalDarkDamageBuff();
        int currentBrightEnergy = CardActionManager.Instance.GetCurrentBrightEnergy();

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(-currentBrightEnergy * 2);
            CardActionManager.Instance.DamageSelf(-darkBonusCount * 2);
            CardActionManager.Instance.DarkCardPlayedExtraDamage();

            Debug.Log("D20_DamageOpponent_DF: " + -currentBrightEnergy * 2);
            Debug.Log("D20_DamageSelf_DF: " + -darkBonusCount * 2);
            Debug.Log("D20_DarkCardPlayedExtraDamage_DF: ");
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(-darkBonusCount * 2 * _brightBonus);
            CardActionManager.Instance.DamageSelf(-currentBrightEnergy * 2 * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(-darkBonusCount * 2);
            CardActionManager.Instance.DamageSelf(-currentBrightEnergy * 2);


            Debug.Log("D20_DamageOpponent_R: " + -darkBonusCount * 2);
            Debug.Log("D20_DamageSelf_R: " + -currentBrightEnergy * 2);

        }
    }
}
