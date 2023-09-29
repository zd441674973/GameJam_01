using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_CardDetail : BasePanel
{
    public TMP_Text cardName;
    /// <summary>
    /// 初始化Tips面板信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {

        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);
        //在玩家拥有的所有道具信息中查找，其中道具的id等于当前显示的tips的道具id
        //GetControl<Image>("ImageIcon").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + itemData.Icon);
        //名称
        cardName.text = cardData.CardName;
    }
}
