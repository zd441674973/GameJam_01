using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using UnityEngine;
using static UnityEditor.Progress;

public class UI_CardLibrary : BasePanel
{
    public Transform grid;
    private List<UI_CardCell> cardlist = new List<UI_CardCell>();

    public override void ShowMe()
    {
        base.ShowMe();
        ShowCards();
    }

    private void ShowCards()
    {
        List<Card> Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

        // ¸üÐÂÄÚÈÝ
        for (int i = 0; i < cardlist.Count; ++i)
        {
            Destroy(cardlist[i].gameObject);
        }
        cardlist.Clear();

        for (int i = 0; i < Playercards.Count; ++i)
        {
            UI_CardCell cardCell = ResMgr.GetInstance().Load<GameObject>("UI/Card_UI").GetComponent<UI_CardCell>();
            cardCell.InitInfo(Playercards[i]);
            cardCell.transform.SetParent(grid, false);
            cardlist.Add(cardCell);

        }
    }
}
