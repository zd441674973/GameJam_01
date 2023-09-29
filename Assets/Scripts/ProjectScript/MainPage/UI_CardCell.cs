using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_CardCell : BasePanel
{
    private Card cardCellInfo;
    public TMP_Text cardName;
    public TMP_Text CardNumber;

    public override void ShowMe()
    {
        base.ShowMe();
        
    }

    protected override void Awake()
    {
        base.Awake();
        //����������������Ƴ����¼������д���
        EventTrigger trigger = GetControl<Image>("Image_Cover").gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(MouseEnterCardCell);

        //����һ������Ƴ����¼������
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(MouseExitCardCell);

        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);
    }

    public void MouseEnterCardCell(BaseEventData data)
    {
        if (cardCellInfo == null)
            return;
        //��ʾ���
        UIManager.GetInstance().ShowPanel<UI_CardDetail>("Card_Detail", E_UI_Layer.Top, (panel) =>
        {
            //�첽���ؽ���������λ����Ϣ
            //������Ϣ
            panel.InitInfo(cardCellInfo);
            //����λ��
            panel.transform.position = GetControl<Image>("Image_Cover").transform.position;
        });
    }
    public void MouseExitCardCell(BaseEventData data)
    {
        if (cardCellInfo == null)
            return;
        //�������
        UIManager.GetInstance().HidePanel("Card_Detail");
    }

    /// <summary>
    /// ��ʼ������
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {
        this.cardCellInfo = info;
        //����id�õ�������Ϣ
        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);

        //Card cardData = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards[info.CardID];
        //����ͼ��
        //GetControl<Image>("Image_Card").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + itemData.Icon);
        //��������
        cardName.text = cardData.CardName;
        //����
        CardNumber.text = info.PlayerOwnedNumber.ToString();

    }
}
