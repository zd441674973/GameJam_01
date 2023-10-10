using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D18 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int health = Random.Range(_card.HealthToSelfMax, _card.HealthToSelfMin);

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainHealth(health);
            Debug.Log("D18_HealthSelf_DF: " + health);

        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.GainHealth(health * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.GainHealth(health);
            Debug.Log("D18_HealthSelf_R: " + health);
        }
    }
}
