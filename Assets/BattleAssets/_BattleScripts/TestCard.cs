using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestCard : CardAction
{
    void Update()
    {
        //if (!Input.GetMouseButtonDown(1)) return;

        //Debug.Log(GameDataControl.GetInstance().GetCardInfo(0).Description);

        //if (TurnSystem.Instance.IsPlayerTurn()) return;

        //TurnSystem.Instance.NextTurn();

        //Timer.Instance.WaitforTime(10);

        // var mouseHit = MouseToWorld.Instance.GetMouseRaycastHit2D();

        // if (!mouseHit.collider) return;

        // var test = mouseHit.collider.transform.name;

        // Debug.Log(test);

        // if (Input.GetKeyDown(KeyCode.Q)) EnergyBarManager.Instance.EnergyBarCalculation("Bright", 1);
        // if (Input.GetKeyDown(KeyCode.W)) EnergyBarManager.Instance.EnergyBarCalculation("Bright", -1);
        // if (Input.GetKeyDown(KeyCode.A)) EnergyBarManager.Instance.EnergyBarCalculation("Dark", 1);
        // if (Input.GetKeyDown(KeyCode.S)) EnergyBarManager.Instance.EnergyBarCalculation("Dark", -1);

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        //for player
        //CardActionManager.Instance.DrawOpponentHand(card.GetEnemyHandCard);
        //CardActionManager.Instance.DrawOpponentHand(2);

        //for enemy
        //CardActionManager.Instance.DrawOpponentHand(2);

        //Debug.Log($"BTestCardPlayed, DrawplayerHand Action");

        CardActionManager.Instance.AttributeSwitch(4);
        //CardActionManager.Instance.AttributeSwitch(cardData);
        //CardActionManager.Instance.GainBrightEnergy(3);


        //Debug.Log("CardPlayed" + card.CardName);
    }
}
