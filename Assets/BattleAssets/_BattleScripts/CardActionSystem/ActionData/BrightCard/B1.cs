using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        CardActionManager.Instance.GainShield(card.SheildToSelf);
        Debug.Log("B1_Played");
    }
}
