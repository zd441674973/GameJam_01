using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B6 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        CardActionManager.Instance.GainShield(card.SheildToSelf);

        Debug.Log($"B6_played; GainShield: {card.SheildToSelf}");
    }
}
