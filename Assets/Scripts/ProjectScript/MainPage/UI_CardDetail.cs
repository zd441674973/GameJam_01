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
    /// ��ʼ��Tips�����Ϣ
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {

        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);
        //�����ӵ�е����е�����Ϣ�в��ң����е��ߵ�id���ڵ�ǰ��ʾ��tips�ĵ���id
        //GetControl<Image>("ImageIcon").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + itemData.Icon);
        //����
        cardName.text = cardData.CardName;
    }
}
