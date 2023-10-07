using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        CardActionManager.Instance.GainShield(card.SheildToSelf);

        CardActionManager.Instance.DrawCard(card.DrawCardFromLabrary);

        Debug.Log($"B4_played; GainShield: {card.SheildToSelf}; DarwCard: {card.DrawCardFromLabrary};");
    }
}
