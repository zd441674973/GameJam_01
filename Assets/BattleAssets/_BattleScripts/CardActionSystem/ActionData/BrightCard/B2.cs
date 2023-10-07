using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class B2 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        CardActionManager.Instance.GainHealth(card.HealthToSelfMin);

        CardActionManager.Instance.DrawCard(card.DrawCardFromLabrary);

        Debug.Log("B2_Played; DrawCard: " + card.DrawCardFromLabrary);
    }
}
