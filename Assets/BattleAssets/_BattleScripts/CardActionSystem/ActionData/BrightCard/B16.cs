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
            int discardCount = CardActionManager.Instance.DiscardBrightHandCard();
            //Debug.Log("DiscardCount: " + discardCount);

            int darkBonusCount = LevelManager.Instance.GetTotalDarkDamageBuff();
            //int brightBonusCount = CardActionManager.Instance.GetCurrentBrightEnergy();
            //Debug.Log("B_count: " + brightBonusCount);

            CardActionManager.Instance.DamageOpponent(-(darkBonusCount * 3 * discardCount * _brightBonus));

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            int discardCount = CardActionManager.Instance.DiscardBrightHandCard();
            //Debug.Log("DiscardCount: " + discardCount);

            int darkBonusCount = LevelManager.Instance.GetTotalDarkDamageBuff();
            //int brightBonusCount = CardActionManager.Instance.GetCurrentBrightEnergy();
            //Debug.Log("B_count: " + brightBonusCount);

            CardActionManager.Instance.DamageOpponent(-(darkBonusCount * 3 * discardCount));

        }
        else
        {

            int discardCount = CardActionManager.Instance.DiscardBrightHandCard();
            Debug.Log("DiscardCount: " + discardCount);

            int darkBonusCount = LevelManager.Instance.GetTotalDarkDamageBuff();
            //int brightBonusCount = CardActionManager.Instance.GetCurrentBrightEnergy();
            //Debug.Log("B_count: " + brightBonusCount);

            CardActionManager.Instance.DamageOpponent(-(darkBonusCount * 3 * discardCount));
        }

    }
}
