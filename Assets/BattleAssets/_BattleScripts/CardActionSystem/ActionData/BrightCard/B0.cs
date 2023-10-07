using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B0 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        CardActionManager.Instance.DealDamage(card.HealthToOpponentMin);
        Debug.Log("B0_Dealdamage");
    }
}
