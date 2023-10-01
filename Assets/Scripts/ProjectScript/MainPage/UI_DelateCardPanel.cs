using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DelateCardPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_DelateCard = false;
    }
}
