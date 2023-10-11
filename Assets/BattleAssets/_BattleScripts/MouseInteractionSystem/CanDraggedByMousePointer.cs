using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanDraggedByMousePointer : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MouseActionManager.Instance.CurrentDraggingCard = transform.GetComponentInParent<Transform>().gameObject;


        _startHover = false;
        BattleUIManager.Instance.SetCardInspectorUI(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        _startHover = true;
        WaitForTime(0.1f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _startHover = false;
        BattleUIManager.Instance.SetCardInspectorUI(false);
    }



    bool _startHover;
    void WaitForTime(float time) => CustomTimer.Instance.WaitforTime(time);
    bool TimeIsUp() => CustomTimer.Instance.TimesUp();

    void Update()
    {
        if (!_startHover) return;
        if (!TimeIsUp()) return;

        BattleUIManager.Instance.SetCardInspectorUI(true);
        SetUpInspectorInfo();



    }

    void SetUpInspectorInfo()
    {
        CardData cardData = transform.GetComponent<CardData>();
        CardInspection.Instance.SetCardImage(ResMgr.GetInstance().Load<Sprite>("Sprites/" + cardData.GetCard().CardName));
        CardInspection.Instance.SetCardName(cardData.GetCard().CardName);
        CardInspection.Instance.SetCardDescription(cardData.GetCard().Description);

    }

}
