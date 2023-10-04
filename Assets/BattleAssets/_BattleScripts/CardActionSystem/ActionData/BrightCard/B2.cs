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
        int currentCardCount = LevelManager.Instance.GetCurrentHandCardCount();
        //LevelManager.Instance.PlayerDrawCard(currentCardCount + card.DrawCardFromLabrary);
        Debug.Log("B2: " + currentCardCount);
        LevelManager.Instance.PlayerDrawCard(currentCardCount + 1);

        Debug.Log("B2_Played");
    }
}
