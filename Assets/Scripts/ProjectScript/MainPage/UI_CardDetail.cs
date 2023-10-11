using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardDetail : BasePanel
{
    public TMP_Text cardName;
    public TMP_Text cardDes;
    public Image cardImage;

    /// <summary>
    /// ��ʼ��Tips�����Ϣ
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {

        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);
        //�����ӵ�е����е�����Ϣ�в��ң����е��ߵ�id���ڵ�ǰ��ʾ��tips�ĵ���id
        cardImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + cardData.CardName);
        //����
        cardName.text = cardData.CardName;

        cardDes.text = cardData.Description;
    }
}
