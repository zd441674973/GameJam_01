using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTitleScene : MonoBehaviour
{
    Transform panelToHide;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener("InBattleScene", HideCanvas);

        EventCenter.GetInstance().AddEventListener("NotInBattleScene", ShowCanvas);
    }

    private void OnDestroy()
    {

        EventCenter.GetInstance().RemoveEventListener("InBattleScene", HideCanvas);

        EventCenter.GetInstance().RemoveEventListener("NotInBattleScene", ShowCanvas);
    }

    private void Start()
    {

        Canvas mainCanvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        Debug.Log(mainCanvas.gameObject.name);
        panelToHide = mainCanvas.transform.Find("Bot");
    }

    private void HideCanvas()
    {

            // ����Ƿ��ҵ����Ӽ���������
            if (panelToHide != null)
            {
                panelToHide.gameObject.SetActive(false);
            }
        
    }

    private void ShowCanvas()
    {

        // ����Ƿ��ҵ����Ӽ���������
        if (panelToHide != null)
        {
            panelToHide.gameObject.SetActive(true);
        }

    }
}
