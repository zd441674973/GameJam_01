using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int shieldValue = CardActionManager.Instance.GetShieldValue();
        CardActionManager.Instance.DealDamage(-shieldValue);

        Debug.Log("B3_played; Damage: " + shieldValue);
    }
}
