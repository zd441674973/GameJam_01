using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_AwardPanel : BasePanel
{
    public UI_CardDetail NewcardSlot1;
    public UI_CardDetail NewcardSlot2;
    public UI_CardDetail NewcardSlot3;
    public Image NewcardSlot1Cover1;
    public Image NewcardSlot2Cover2;
    public Image NewcardSlot3Cover3;

    public override void ShowMe()
    {
        base.ShowMe();
    }

    protected override void Awake()
    {
        base.Awake();
        //监听鼠标移入和鼠标移出的事件，进行处理
        EventTrigger trigger1 = NewcardSlot1Cover1.gameObject.AddComponent<EventTrigger>();

        //申明一个鼠标进入的事件类对象
        EventTrigger.Entry enter1 = new EventTrigger.Entry();
        enter1.eventID = EventTriggerType.PointerEnter;
        enter1.callback.AddListener(EnterNewCardSlot1);
        //申明一个鼠标移出的事件类对象
        EventTrigger.Entry exit1 = new EventTrigger.Entry();
        exit1.eventID = EventTriggerType.PointerExit;
        exit1.callback.AddListener(ExitNewCardSlot1);

        trigger1.triggers.Add(enter1);
        trigger1.triggers.Add(exit1); 

        ////////////////////////////////////////////////////////////////////////////////////
        EventTrigger trigger2 = NewcardSlot2Cover2.gameObject.AddComponent<EventTrigger>();

        //申明一个鼠标进入的事件类对象
        EventTrigger.Entry enter2 = new EventTrigger.Entry();
        enter2.eventID = EventTriggerType.PointerEnter;
        enter2.callback.AddListener(EnterNewCardSlot2);
        //申明一个鼠标移出的事件类对象
        EventTrigger.Entry exit2 = new EventTrigger.Entry();
        exit2.eventID = EventTriggerType.PointerExit;
        exit2.callback.AddListener(ExitNewCardSlot2);

        trigger2.triggers.Add(enter2);
        trigger2.triggers.Add(exit2);

        ////////////////////////////////////////////////////////////////////////////////////
        EventTrigger trigger3 = NewcardSlot3Cover3.gameObject.AddComponent<EventTrigger>();

        //申明一个鼠标进入的事件类对象
        EventTrigger.Entry enter3 = new EventTrigger.Entry();
        enter3.eventID = EventTriggerType.PointerEnter;
        enter3.callback.AddListener(EnterNewCardSlot3);
        //申明一个鼠标移出的事件类对象
        EventTrigger.Entry exit3 = new EventTrigger.Entry();
        exit3.eventID = EventTriggerType.PointerExit;
        exit3.callback.AddListener(ExitNewCardSlot3);

        trigger3.triggers.Add(enter3);
        trigger3.triggers.Add(exit3);
    }

    public void EnterNewCardSlot1(BaseEventData data)
    {
        NewcardSlot1.gameObject.transform.localScale = new Vector3(2, 2, 2);
    }
    public void EnterNewCardSlot2(BaseEventData data)
    {
        NewcardSlot2.gameObject.transform.localScale = new Vector3(2, 2, 2);
    }
    public void EnterNewCardSlot3(BaseEventData data)
    {
        NewcardSlot3.gameObject.transform.localScale = new Vector3(2, 2, 2);
    }

    public void ExitNewCardSlot1(BaseEventData data)
    {
        NewcardSlot1.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void ExitNewCardSlot2(BaseEventData data)
    {
        NewcardSlot2.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void ExitNewCardSlot3(BaseEventData data)
    {
        NewcardSlot3.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
