using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B5 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int shieldValue = CardActionManager.Instance.GetShieldValue();
        CardActionManager.Instance.GainShield(shieldValue);

        Debug.Log($"B5_played; GainShield: {shieldValue}");
    }
}
