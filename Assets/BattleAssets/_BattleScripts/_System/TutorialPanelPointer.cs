using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanelPointer : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");

        if (TutorialSystem.Instance.TutorialIndex != TutorialSystem.Instance.TutorialPanelList.Count)
        {

            TutorialSystem.Instance.TutorialPanelList[TutorialSystem.Instance.TutorialIndex].gameObject.SetActive(false);

            TutorialSystem.Instance.TutorialIndex++;

        }

        TutorialSystem.Instance.SetBrightMaxPanel(false);
        TutorialSystem.Instance.SetDarkMaxPanel(false);
        TutorialSystem.Instance.SetHONGXIPanel(false);
        TutorialSystem.Instance.SetBackGroundPanel(false);


    }
}
