using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B9 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int currentDarkEnergy = CardActionManager.Instance.GetCurrentDarkEnergy();





        if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {

            if (currentDarkEnergy >= 3)
                CardActionManager.Instance.DrawCard(2 * _brightBonus);

            else CardActionManager.Instance.DrawCard(1 * _brightBonus);

        }
        else if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            if (currentDarkEnergy >= 3)
                CardActionManager.Instance.OpponentDrawCard(2);

            else CardActionManager.Instance.OpponentDrawCard(1);
        }
        else
        {
            if (currentDarkEnergy >= 3)
                CardActionManager.Instance.DrawCard(2);

            else CardActionManager.Instance.DrawCard(1);

        }

    }
}
