using System.Collections;
using System.Collections.Generic;
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
        int currentCardCount = LevelManager.Instance.GetCurrentHandCardCount();
        LevelManager.Instance.PlayerDrawCard(currentCardCount + card.DrawCardFromLabrary);

        Debug.Log("B2_Played");
    }
}
